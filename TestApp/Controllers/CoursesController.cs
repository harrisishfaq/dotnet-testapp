using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Database;
using TestApp.Models;

namespace TestApp.Controllers;

public class CoursesController : Controller
{
    private readonly ApplicationDbContext _db;
    public CoursesController(ApplicationDbContext db)
    {
        _db = db;
    }
// GET
    /*
    public IActionResult Index()
    {
       // How we can manage multiple models in one view with the help of viewbag
       ViewBag.Courses = _db.Courses.ToList();
        ViewBag.Coursess = _db.Courses.ToList();

       var data = new
       {
           Course = _db.Courses.ToList(),
           Courses = _db.Courses.ToList()
       };
       return View(data);
     }
    */
    
    public IActionResult Index()
    {
        var courses = _db.Courses.Include(c => c.Students).ToList();
       // ViewBag.Cate = _db.Courses.ToList();
        return View(courses);
    }
    
    public IActionResult AddCourse()
    {
        return View();
    }
    
    [HttpGet]
    [Route("/Courses/UpdateCourse")]
    public IActionResult UpdateCourse(int id)
    {
        var course = _db.Courses.Find(id);
        if (course == null)
        {
            TempData["Error"] = "Course not found";
            return RedirectToAction("Index");
        }
        
        return View(course);
    }
    
    [HttpPost]
    [Route("/Courses/Create")]
    public IActionResult Create(Course course)
    {
        try
        {
            if (!ModelState.IsValid)
            {
              //  TempData["Error"] = "Course created failed";
              //  return RedirectToAction("AddCourse");
              throw new Exception("Invalid course data.");
            }
           

            _db.Courses.Add(course);
            _db.SaveChanges();

            TempData["Success"] = "Course created successfully!";
            return RedirectToAction("Index");
        } catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("AddCourse");
        }
    }
    
    public IActionResult Edit(Course course)
    {
        try
        {
            var objCourse = _db.Courses.Find(course.Id);

            if (objCourse == null)
            {
                throw new Exception("Somethign went wrong. Course not found");
            }
            
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid course data.");

            }
            
            objCourse.Title = course.Title;
            objCourse.Description = course.Description;
            _db.Courses.Update(objCourse);
            _db.SaveChanges();
            TempData["Success"] = "Course updated successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("UpdateCourse", new { course.Id });
        }
    }
    
    public IActionResult Delete(int id)
    {
        try
        {
            var objCourse = _db.Courses.Find(id);

            if (objCourse == null)
            {
                throw new Exception("Something went wrong. Course not found");
            }

            var has_enrolled_students = _db.Students.Where(s => s.Courses.Any(c => c.Id == objCourse.Id));

            if (has_enrolled_students.Count() > 0)
            {
                throw new Exception("Something went wrong. Course is assigned to students");
            }

            _db.Courses.Remove(objCourse);
            _db.SaveChanges();
            
            TempData["Success"] = "Course deleted successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index", new { id });
        }
    }
}