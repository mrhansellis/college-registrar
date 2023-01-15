using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Registrar.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string Name { get; set; }
    public string CourseNumber { get; set; }
    public List<Student> Students { get; set; }
    public List<StudentCourse> BindEntities { get; set; }
    public ApplicationUser User { get; set; }
  }
}