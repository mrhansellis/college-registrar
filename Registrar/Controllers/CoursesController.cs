using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Registrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace Registrar.Controllers
{
  public class CoursesController : Controller
  {
    private readonly RegistrarContext _db;
    public CoursesController(RegistrarContext db)
    {
      _db = db;
    }
    
    public ActionResult Index()
    {
      List<Course> model = _db.Courses.ToList();
      return View(model);
    }
    
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Course course)
    {
      _db.Courses.Add(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Details(int id)
    {
      Course thisCourse = _db.Courses
        .Include(course => course.Students)
        .ThenInclude(student => student.JoinEntities)
        .ThenInclude(join => join.Department)
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
  }
}