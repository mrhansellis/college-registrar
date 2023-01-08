using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Registrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace Registrar.Controllers
{
  public class DepartmentsController : Controller
  {
    private readonly RegistrarContext _db;
    public DepartmentsController(RegistrarContext db)
    {
      _db = db;
    }
    
    public ActionResult Index()
    {
      return View(_db.Departments.ToList());
    }
    
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Department department)
    {
      _db.Departments.Add(department);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Details(int id)
    {
      Department thisDepartment = _db.Departments
        .Include(department => department.JoinEntities)
        .ThenInclude(join => join.Student)
        .FirstOrDefault(department => department.DepartmentId == id);
      return View(thisDepartment);
    }

    public ActionResult AddStudent(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(departments => departments.DepartmentId == id);
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
      return View(thisDepartment);
    }

    [HttpPost]
    public ActionResult AddStudent(Department department, int studentId)
    {
      #nullable enable
      StudentDepartment? joinEntity = _db.StudentDepartments.FirstOrDefault(join => (join.StudentId == studentId && join.DepartmentId == department.DepartmentId));
      #nullable disable
      if (joinEntity == null && studentId != 0)
      {
        _db.StudentDepartments.Add(new StudentDepartment() { StudentId = studentId, DepartmentId = department.DepartmentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = department.DepartmentId });
    }

    public ActionResult Edit(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
      return View(thisDepartment);
    }

    [HttpPost]
    public ActionResult Edit(Department department)
    {
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
      return View(thisDepartment);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
      _db.Departments.Remove(thisDepartment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      StudentDepartment joinEntry = _db.StudentDepartments.FirstOrDefault(entry => entry.StudentDepartmentId == joinId);
      _db.StudentDepartments.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
  }
}