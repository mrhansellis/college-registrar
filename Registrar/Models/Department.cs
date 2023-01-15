using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Registrar.Models
{
  public class Department
  {
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<StudentDepartment> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}