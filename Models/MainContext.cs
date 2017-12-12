using Microsoft.EntityFrameworkCore;
 
namespace Wall.Models
{
    public class MainContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<Comment> comments { get; set; }
    }
}