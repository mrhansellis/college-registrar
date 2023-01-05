using System.Collections.Generic;

namespace Registrar.Models
{
  public class Department
  {
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<StudentDepartment> JoinEntities { get; }
  }
}