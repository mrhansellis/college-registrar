using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Registrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace Registrar.Controllers
{
  public class StudentsController : Controller
  {
    private readonly RegistrarContext _db;

    public StudentsController(RegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Student> model = _db.Students
                            .Include(student => student.JoinEntities)
                            .ThenInclude(join => join.Department)
                            .ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
        return View(student);
      }
      else
      {
        _db.Students.Add(student);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      // if (student.CourseId == 0)
      // {
      //   return RedirectToAction("Create");
      // }
      // _db.Students.Add(student);
      // _db.SaveChanges();
      // return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Student thisStudent = _db.Students
                            .Include(student => student.Course)
                            .Include(student => student.JoinEntities)
                            .ThenInclude(join => join.Department)
                            .Include(student => student.BindEntities)
                            .ThenInclude(join => join.Course)
                            .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    public ActionResult Edit(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult Edit(Student student)
    {
      _db.Students.Update(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      _db.Students.Remove(thisStudent);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddDepartment(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult AddDepartment(Student student, int departmentId)
    {
      #nullable enable
      StudentDepartment? joinEntity = _db.StudentDepartments.FirstOrDefault(join => (join.DepartmentId == departmentId && join.StudentId == student.StudentId));
      #nullable disable
      if (joinEntity == null && departmentId != 0)
      {
        _db.StudentDepartments.Add(new StudentDepartment() { DepartmentId = departmentId, StudentId = student.StudentId });
      }
      return RedirectToAction("Details", new { id = student.StudentId });
    }

    public ActionResult AddCourse(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult AddCourse(Student student, int courseId)
    {
      #nullable enable
      StudentCourse? bindEntity = _db.StudentCourses.FirstOrDefault(join => (join.CourseId == courseId && join.StudentId == student.StudentId));
      #nullable disable
      if (bindEntity == null && courseId != 0)
      {
        _db.StudentCourses.Add(new StudentCourse() { CourseId = courseId, StudentId = student.StudentId });
      }
      return RedirectToAction("Details", new { id = student.StudentId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      StudentDepartment joinEntry = _db.StudentDepartments.FirstOrDefault(entry => entry.StudentDepartmentId == joinId);
      _db.StudentDepartments.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteBind(int bindId) //maybe bindId
    {
      StudentCourse bindEntry = _db.StudentCourses.FirstOrDefault
      (entry => entry.StudentCourseId == bindId);
      _db.StudentCourses.Remove(bindEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}