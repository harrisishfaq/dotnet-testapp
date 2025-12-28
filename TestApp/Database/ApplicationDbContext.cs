using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
      
    }
  
    public DbSet<Course> Courses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Calculus 101", Description = "Calculus description....." },
            new Course { Id = 2, Title = "English 201", Description = "English course description...." }
            );
    }
  
}