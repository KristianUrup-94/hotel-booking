using Microsoft.EntityFrameworkCore;

namespace Rooms
{
    public class RoomsDbContext : DbContext
    {
        public RoomsDbContext(DbContextOptions<RoomsDbContext> options)
            :base(options) { }

        public DbSet<Room> Rooms { get; set; }
    }
}
