using Microsoft.EntityFrameworkCore;
using TestApp.Models;


namespace TestApp.Database;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
      
    }
  
    public DbSet<Course> Courses { get; set; }
  
}