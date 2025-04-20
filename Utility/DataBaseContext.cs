using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Utility
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<TaskItem> TaskItem { get; set; }
    }
}
