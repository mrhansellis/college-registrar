namespace Registrar.Models
{
  public class StudentDepartment
  {
    public int StudentDepartmentId { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
  }
}

//get the revert done. Perhaps make a new migration, perhaps make a new model, perhaps manually delete stuff in ef core. But probably delete student department.cs and create a fresh one.