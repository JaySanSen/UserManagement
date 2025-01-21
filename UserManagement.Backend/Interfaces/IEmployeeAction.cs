using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Backend.DTOs;

namespace UserManagement.Backend.Interfaces
{
  public interface IEmployeeAction{
    Task<EmployeeDTO> GetEmployeeByID(int id);
    Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
    Task CreateEmployee(EmployeeDTO employeeDTO);
  }
}