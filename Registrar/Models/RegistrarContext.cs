using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Registrar.Models
{
  public class RegistrarContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Course> Courses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentDepartment> StudentDepartments { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }

    public RegistrarContext(DbContextOptions options) : base(options) { }
  }
}