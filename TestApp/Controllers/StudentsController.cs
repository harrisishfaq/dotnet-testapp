using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestApp.Database;
using TestApp.Migrations;
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
        // List<Student> students = _db.Students.ToList();
        List<Student> students = _db.Students.Include(s => s.Courses).ToList();
        return View(students);
    }
    
    public IActionResult New()
    {
        // var courses = _db.Courses.ToList();
        // return View(courses);
        ViewBag.Courses = new SelectList(_db.Courses.ToList(), "Id", "Title");
        return View();
    }
    
    public IActionResult Edit(int id)
    {
        var student = _db.Students.Include(s => s.Courses).FirstOrDefault(s => s.Id == id);
        if (student == null) return NotFound();

        ViewBag.Courses = new SelectList(_db.Courses.ToList(), "Id", "Title");

        return View(student);
    }

    
    public IActionResult Delete(int id)
    {
        var student = _db.Students.Find(id);
        _db.Students.Remove(student);
        _db.SaveChanges();
        TempData["Success"] = "Student deleted successfully!";
        return RedirectToAction("Index");
    }
    
    public IActionResult Create(Student student, int[] selectedCourses)
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

            if (selectedCourses.Length > 0)
            {
             var courses = _db.Courses.Where(c=> selectedCourses.Contains(c.Id)).ToList();
             new_student.Courses = courses;
             _db.Students.Update(new_student);
             _db.SaveChanges();
            }
            
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
    
    public IActionResult Update(Student student, int[] selectedCourses)
    {
        try
        {
            var existing_student = _db.Students
                .Include(s => s.Courses)  // Important: load current courses
                .FirstOrDefault(s => s.Id == student.Id);

            if (existing_student == null)
            {
                TempData["Error"] = "Student not found!";
                return RedirectToAction("Edit", new { id = student.Id });
            }

            existing_student.FullName = student.FullName;
            existing_student.Email = student.Email;
            existing_student.Age = student.Age;

            // Clear current courses
            existing_student.Courses.Clear();

            if (selectedCourses != null && selectedCourses.Length > 0)
            {
                var courses = _db.Courses.Where(c => selectedCourses.Contains(c.Id)).ToList();

                foreach (var course in courses)
                {
                    existing_student.Courses.Add(course);
                }
            }

            _db.SaveChanges();  // Save all changes at once

            TempData["Success"] = "Student updated successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["Error"] = e.Message;
            Console.WriteLine(e);
            return RedirectToAction("Edit", new { id = student.Id });
        }
    }

}