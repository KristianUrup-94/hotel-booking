using Microsoft.EntityFrameworkCore;
using Rooms.Entity;

namespace Rooms.Infrastructure
{
    public class RoomsDbContext : DbContext
    {
        public RoomsDbContext(DbContextOptions<RoomsDbContext> options)
            :base(options) 
        {
            MockData();
        }

        public DbSet<Room> Rooms { get; set; }

        // For Mocking
        public void MockData()
        {
            Rooms.AddRange(GetRooms());
            SaveChanges();
        }

        private List<Room> GetRooms()
        {
            return new List<Room> {
                new Room
                {
                    Id = 1,
                    Name = "Room 101",
                    Description = "This is normal room, with 2 separated beds"
                },
                new Room
                {
                    Id = 2,
                    Name = "Room 102",
                    Description = "This is normal room, with 2 separated beds"
                },
                new Room
                {
                    Id = 3,
                    Name = "Room 103",
                    Description = "This is normal room, with 2 separated beds"
                },
                new Room
                {
                    Id = 4,
                    Name = "Room 201",
                    Description = "This is mini suite, with a  queen size bed"
                },
                new Room
                {
                    Id = 5,
                    Name = "Room 304",
                    Description = "This is suite, with a king size bed and a lovely view over Esbjerg City"
                },
                new Room
                {
                    Id = 6,
                    Name = "Room 305",
                    Description = "This is suite, with a king size bed and a lovely view over Esbjerg City"
                },
                new Room
                {
                    Id = 7,
                    Name = "Room 306",
                    Description = "This is double suite, with a king size bed, a queen size bed in separated rooms and a lovely view over Esbjerg City"
                },

                new Room
                {
                    Id = 8,
                    Name = "Room 701",
                    Description = "This is presidential suite, with a king size bed, a queen size bed in separated rooms and a lovely view over Esbjerg City"
                }
            };
        }
    }
}
