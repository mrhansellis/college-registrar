using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Registrar.Models
{
  public class Student
  {
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string EnrollDate { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Dawg, pick a course! Think about your future dawg.")]
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public List<StudentDepartment> JoinEntities { get; set; }
    public List<StudentCourse> BindEntities { get; set; }
  }
}