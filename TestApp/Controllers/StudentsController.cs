using Microsoft.AspNetCore.Mvc;
using TestApp.Database;
using TestApp.Models;

namespace TestApp.Controllers;

public class StudentsController : Controller
{
    private readonly ApplicationDbContext _db;
    public StudentsController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    // GET
    public IActionResult Index()
    {
        List<Student> students = _db.Students.ToList();
        return View(students);
    }
    
    public IActionResult New()
    {
        return View();
    }
    
    public IActionResult Create(Student student)
    {
        try
        {
            var new_student = new Student
            {
                FullName = student.FullName,
                Age = student.Age,
                Email = student.Email
            };
            
            _db.Students.Add(new_student);
            _db.SaveChanges();
            TempData["Success"] = "Student created successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["Error"] = e.Message;
            Console.WriteLine(e);
            return RedirectToAction("New");
        }
    }
}