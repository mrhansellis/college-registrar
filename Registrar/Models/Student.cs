using System.Collections.Generic;

namespace Registrar.Models
{
  public class Student
  {
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string EnrollDate { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public List<StudentCourse> JoinEntities { get; set; }
  }
}