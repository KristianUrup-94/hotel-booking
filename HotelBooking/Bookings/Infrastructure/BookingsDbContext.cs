using Bookings.Entity;
using Microsoft.EntityFrameworkCore;

namespace Rooms.Infrastructure
{
    public class BookingsDbContext : DbContext
    {
        public BookingsDbContext(DbContextOptions<BookingsDbContext> options)
            :base(options) { }

        public DbSet<Booking> Bookings { get; set; }

        public void MockData()
        {
            Bookings.AddRange(GetBookings());
        }

        public List<Booking> GetBookings()
        {
            return new List<Booking> {
                new Booking
                {
                    Id = 1,
                    BookingId = 100001,
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 4
                },
                new Booking
                {
                    Id = 2,
                    BookingId = 100002,
                    Comments = null,
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 1
                },
                new Booking
                {
                    Id = 3,
                    BookingId = 100003,
                    Comments = null,
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 2
                },
                new Booking
                {
                    Id = 4,
                    BookingId = 100004,
                    Comments = "I want some champagne on the bed, together with flowers",
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 8
                },
                new Booking
                {
                    Id = 5,
                    BookingId = 100005,
                    Comments = "I want snacks at the room",
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 7
                },
                new Booking
                {
                    Id = 6,
                    BookingId = 100006,
                    Comments = "I want snacks at the room",
                    From = new DateTimeOffset(2026, 4, 10, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 14, 10, 0,0, TimeSpan.Zero),
                    RoomId = 8
                },
            };
        }
    }
}
