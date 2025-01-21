using Microsoft.EntityFrameworkCore;
using UserManagement.Backend.Models;

namespace UserManagement.Backend.Data
{
  public class EmployeeDBContext : DbContext
  {
    public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
    {

    }
    public DbSet<Employee> Employees { get; set; }
  }
}
