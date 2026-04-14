using APBD5Controllers.Models;

namespace APBD5Controllers.Data;

public static class DataStorage
{
    public static List<Room> Rooms { get; } =
    [
        new Room
        {
            Id = 1, Name = "Sala A101", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2, Name = "Sala A203", BuildingCode = "A", Floor = 2, Capacity = 20, HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 3, Name = "Lab B110", BuildingCode = "B", Floor = 1, Capacity = 15, HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 4, Name = "Lab B205", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 5, Name = "Sala C301", BuildingCode = "C", Floor = 3, Capacity = 50, HasProjector = true,
            IsActive = false
        }
    ];

    public static List<Reservation> Reservations { get; } =
    [
        new Reservation
        {
            Id = 1, RoomId = 1, OrganizerName = "Anna Kowalska",
            Topic = "Warsztaty HTTP i REST", Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2, RoomId = 1, OrganizerName = "Marek Wiśniewski",
            Topic = "Szkolenie SQL", Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(14, 0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3, RoomId = 2, OrganizerName = "Katarzyna Nowak",
            Topic = "Design Patterns w C#", Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 30),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4, RoomId = 3, OrganizerName = "Piotr Zając",
            Topic = "Podstawy Dockera", Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(10, 0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 5, RoomId = 4, OrganizerName = "Ewa Dąbrowska",
            Topic = "ASP.NET Core MVC", Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(15, 30),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 6, RoomId = 2, OrganizerName = "Tomasz Krawczyk",
            Topic = "Testowanie jednostkowe xUnit", Date = new DateOnly(2026, 4, 1),
            StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 0),
            Status = "cancelled"
        }
    ];

    private static int _nextRoomId = 6;
    private static int _nextReservationId = 7;
    
    public static int NextRoomId() => _nextRoomId++;
    public static int NextReservationId() => _nextReservationId++;
}