using Bookings.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Metadata;

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
            SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public List<Booking> GetBookings()
        {
            return new List<Booking> {
                new Booking
                {
                    BookingNo = 100001,
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 4
                },
                new Booking
                {
                    BookingNo = 100002,
                    Comments = null,
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 1
                },
                new Booking
                {
                    BookingNo = 100003,
                    Comments = null,
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 2
                },
                new Booking
                {
                    BookingNo = 100004,
                    Comments = "I want some champagne on the bed, together with flowers",
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 8
                },
                new Booking
                {
                    BookingNo = 100005,
                    Comments = "I want snacks at the room",
                    From = new DateTimeOffset(2026, 4, 3, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 7, 10, 0,0, TimeSpan.Zero),
                    RoomId = 7
                },
                new Booking
                {
                    BookingNo = 100006,
                    Comments = "I want snacks at the room",
                    From = new DateTimeOffset(2026, 4, 10, 15, 0,0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 4, 14, 10, 0,0, TimeSpan.Zero),
                    RoomId = 8
                },
            };
        }
    }
}
