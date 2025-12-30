using System.ComponentModel.DataAnnotations;

namespace TestApp.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    
    public ICollection<Student> Students { get; set; } = new List<Student>();

}