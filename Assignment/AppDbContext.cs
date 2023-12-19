using Assignment;
using Microsoft.EntityFrameworkCore;

namespace Assignment
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskDetail> TaskDetail { get; set; }

    }
}

