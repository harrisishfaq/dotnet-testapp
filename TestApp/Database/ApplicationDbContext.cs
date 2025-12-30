using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
      
    }
  
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // DATA SEEDING
        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Calculus 101", Description = "Calculus description....." },
            new Course { Id = 2, Title = "English 201", Description = "English course description...." }
            );
        
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, FullName = "Abcd", Email = "abcd@gmail.com.", Age = 20 },
            new Student { Id = 2, FullName = "Efgh", Email = "efgh@gmail.com.", Age = 21 }
        ); 
        
        // RELATIONSHIP DEFINED
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Courses)
            .WithMany(c => c.Students)
            .UsingEntity(j => j.ToTable("StudentCourses"));
        
    }
  
}