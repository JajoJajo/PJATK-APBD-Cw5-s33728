using APBD5Controllers.Data;
using APBD5Controllers.DTOs;
using APBD5Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD5Controllers.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var reservations = DataStorage.Reservations.AsEnumerable();

        if (date.HasValue)
            reservations = reservations.Where(d => d.Date == date.Value);

        if (!string.IsNullOrWhiteSpace(status))
            reservations = reservations.Where(s => s.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (roomId.HasValue)
            reservations = reservations.Where(r => r.RoomId == roomId);

        return Ok(reservations.ToList());
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById(int id)
    {
        var reservation = DataStorage.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation is null)
            return NotFound(new { message = $"Reservation with id {id} not found." });
 
        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult Add([FromBody] ReservationDto reservationDto)
    {
        var room = DataStorage.Rooms.FirstOrDefault(r => r.Id == reservationDto.RoomId);
        if (room is null)
            return NotFound($"Room with id {reservationDto.RoomId} not found.");

        if (!room.IsActive)
            return BadRequest($"Room '{room.Name}' is not active and cannot be reserved.");

        bool hasConflict = DataStorage.Reservations.Any(r =>
            r.RoomId == reservationDto.RoomId &&
            r.Date == reservationDto.Date &&
            r.Status != "cancelled" &&
            r.StartTime < reservationDto.EndTime &&
            reservationDto.StartTime < r.EndTime);

        if (hasConflict)
            return Conflict("The room is already reserved during the requested time slot.");

        var reservation = new Reservation()
        {
            Id = DataStorage.NextReservationId(),
            Date = reservationDto.Date,
            EndTime = reservationDto.EndTime,
            OrganizerName = reservationDto.OrganizerName,
            RoomId = reservationDto.RoomId,
            StartTime = reservationDto.StartTime,
            Status = reservationDto.Status,
            Topic = reservationDto.Topic
        };
        
        DataStorage.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Update(int id, [FromBody] ReservationDto reservationDto)
    {
        var reservation = DataStorage.Reservations.FirstOrDefault(r => r.Id == id);
        
        if (reservation is null)
            return NotFound($"Reservation with id {id} not found.");
        
        bool hasConflict = DataStorage.Reservations.Any(r =>
            r.RoomId == reservationDto.RoomId &&
            r.Date == reservationDto.Date &&
            r.Status != "cancelled" &&
            r.StartTime < reservationDto.EndTime &&
            reservationDto.StartTime < r.EndTime);
        
        if (hasConflict)
            return Conflict("The room is already reserved during the requested time slot.");

        reservation.Date = reservationDto.Date;
        reservation.EndTime = reservationDto.EndTime;
        reservation.OrganizerName = reservationDto.OrganizerName;
        reservation.RoomId = reservationDto.RoomId;
        reservation.StartTime = reservationDto.StartTime;
        reservation.Status = reservationDto.Status;
        reservation.Topic = reservationDto.Topic;

        return Ok(reservation);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = DataStorage.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation is null)
            return NotFound($"Reservation with id {id} not found.");

        DataStorage.Reservations.Remove(reservation);
        return NoContent();
    }
}