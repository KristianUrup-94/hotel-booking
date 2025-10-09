using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Metadata;
using Users.Entity;

namespace Users.Infrastructure
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            :base(options) { }

        public DbSet<User> Users { get; set; }

        public void MockData()
        {
            Users.AddRange(GetUsers());
            SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public List<User> GetUsers()
        {
            return new List<User> {
                new User
                {
                    FirstName = "Tony",
                    LastName = "Michaelangelo",
                    Address = "Bøfvænget 1"
                },
                new User
                {
                    FirstName = "Bonnie",
                    LastName = "Michaelangelo",
                    Address = "Bøfvænget 1"
                },
                new User
                {
                    FirstName = "Kim",
                    LastName = "Tøffen",
                    Address = "Bøfvænget 4"
                },
            };
        }
    }
}
