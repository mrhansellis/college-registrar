using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Registrar.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Registrar.Controllers
{
  [Authorize]
  public class CoursesController : Controller
  {
    private readonly RegistrarContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CoursesController(UserManager<ApplicationUser> userManager, RegistrarContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    
    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Course> userCourses = _db.Courses
                        .Where(entry => entry.User.Id == currentUser.Id)
                        .ToList();
      return View(userCourses);
    }
    
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async  Task<ActionResult> Create(Course course)
    {
      if(!ModelState.IsValid)
      {
        return View(course);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        course.User = currentUser;
        _db.Courses.Add(course);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
    
    public ActionResult Details(int id)
    {
      Course thisCourse = _db.Courses 
        .Include(course => course.Students)
        .ThenInclude(course => course.BindEntities)
        .Include(student => student.BindEntities)
        .ThenInclude(join => join.Student)
        .ThenInclude(join => join.Course)
        .FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }

    public ActionResult AddStudent( int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(courses => courses.CourseId ==id);
      ViewBag.StudentId = new SelectList(_db.Students,"StudentId", "Name");
      return View(thisCourse);
    }

    [HttpPost]
    public ActionResult AddStudent(Course course, int studentId)
    {
      #nullable enable
      StudentCourse? bindEntity = _db.StudentCourses.FirstOrDefault(join => (join.StudentId == studentId && join.CourseId == course.CourseId));
      #nullable disable
      if (bindEntity == null && studentId != 0)
      {
        _db.StudentCourses.Add(new StudentCourse() { StudentId = studentId, CourseId = course.CourseId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = course.CourseId });
    }

    public ActionResult Edit(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }

    [HttpPost]
    public ActionResult Edit(Course course)
    {
      _db.Courses.Update(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      _db.Courses.Remove(thisCourse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteBind(int bindId)
    {
      StudentCourse bindEntry = _db.StudentCourses.FirstOrDefault(entry => entry.StudentCourseId == bindId);
      _db.StudentCourses.Remove(bindEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}