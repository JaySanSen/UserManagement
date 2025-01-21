using UserManagement.Backend.DTOs;
using UserManagement.Backend.Models;

namespace UserManagement.Backend.Mappers
{
  public static class EmployeeMapper
  {
    public static EmployeeDTO ToEmployeeDTO(this Employee employee)
    {
      return new EmployeeDTO
      {
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        PhoneNumber = employee.PhoneNumber
      };
    }
  }
}