using backend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Database
{

    public class UserMessagesContext : DbContext
    {
        static UserMessagesContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Theme> Themes { get; set; }

        public UserMessagesContext(DbContextOptions<UserMessagesContext> options)
    : base(options)
        {

        }
    }
}
