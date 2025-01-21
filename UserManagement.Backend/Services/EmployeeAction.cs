using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UserManagement.Backend.Data;
using UserManagement.Backend.DTOs;
using UserManagement.Backend.Interfaces;
using UserManagement.Backend.Mappers;
using UserManagement.Backend.Models;

namespace UserManagement.Backend.Services
{
  public class EmployeeAction : IEmployeeAction
  {

    private readonly EmployeeDBContext _context;
    public EmployeeAction(EmployeeDBContext context)
    {
      _context = context;
    }

    public async Task CreateEmployee(EmployeeDTO employeeDTO)
    {
      var employee = new Employee{
        FirstName = employeeDTO.FirstName,
        LastName = employeeDTO.LastName,
        Email = employeeDTO.Email,
        PhoneNumber = employeeDTO.PhoneNumber
      };
      await _context.AddAsync(employee);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
    {
      var dbList = await _context.Employees.ToListAsync();
      var employees = new List<EmployeeDTO>();
      foreach (var item in dbList)
      {
        employees.Add(item.ToEmployeeDTO());
      }
      return employees;
    }

    public async Task<EmployeeDTO> GetEmployeeByID(int id)
    {
      var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == id);
      return employee.ToEmployeeDTO();
    }
  }
}