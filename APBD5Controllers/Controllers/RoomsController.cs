using Microsoft.AspNetCore.Mvc;
using APBD5Controllers.Data;
using APBD5Controllers.DTOs;
using APBD5Controllers.Models;

namespace APBD5Controllers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? isActive)
    {
        var rooms = DataStorage.Rooms.AsEnumerable();
        
        if (minCapacity.HasValue)
            rooms = rooms.Where(c => c.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            rooms = rooms.Where(p => p.HasProjector);
        
        if (isActive.HasValue)
            rooms = rooms.Where(a => a.IsActive);
        
        return Ok(rooms.ToList());
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById(int id)
    {
        var room = DataStorage.Rooms.FirstOrDefault(e => e.Id == id);

        if (room is null)
            return NotFound($"Room with id {id} does not exist");

        return Ok(room);
    }

    [HttpGet]
    [Route("building/{buildingCode}")]
    public IActionResult GetByBuilding(string buildingCode)
    {
        var rooms =
            DataStorage.Rooms.Where(b => b.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase));

        return Ok(rooms.ToList());
    }

    [HttpPost]
    public IActionResult Add([FromBody] RoomDto roomDto)
    {
        var room = new Room()
        {
            Id = DataStorage.NextRoomId(),
            BuildingCode = roomDto.BuildingCode,
            Capacity = roomDto.Capacity,
            Floor = roomDto.Floor,
            HasProjector = roomDto.HasProjector,
            IsActive = roomDto.IsActive,
            Name = roomDto.Name
        };
        
        DataStorage.Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Update(int id, [FromBody] RoomDto roomDto)
    {
        var room = DataStorage.Rooms.FirstOrDefault(r => r.Id == id);
        if (room is null)
            return NotFound($"Room with id {id} not found.");

        room.BuildingCode = roomDto.BuildingCode;
        room.Capacity = roomDto.Capacity;
        room.Floor = roomDto.Floor;
        room.HasProjector = roomDto.HasProjector;
        room.IsActive = roomDto.IsActive;
        room.Name = roomDto.Name;

        return Ok(room);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = DataStorage.Rooms.FirstOrDefault(r => r.Id == id);
        if (room is null)
            return NotFound($"Room with id {id} not found.");

        var date = DateOnly.FromDateTime(DateTime.Today);
        var time = TimeOnly.FromDateTime(DateTime.Now);
        bool hasFutureReservations = DataStorage.Reservations.Any(t => t.RoomId == id && (t.Date >= date || t.EndTime >= time));

        if (hasFutureReservations)
            return Conflict("Cannot delete room: it has future reservations.");
        
        DataStorage.Rooms.Remove(room);

        return NoContent();
    }
}