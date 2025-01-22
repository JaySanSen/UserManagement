using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;
using UserManagement.Backend.DTOs;
using UserManagement.Backend.Interfaces;

namespace UserManagement.Backend.Contollers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeeController : ControllerBase
  {
    private readonly IEmployeeAction _employeeAction;
    public EmployeeController(IEmployeeAction employeeAction)
    {
      _employeeAction = employeeAction;
    }
    [HttpGet]
    [Route("", Name = "GetAllEmployees")]
    public async Task<IActionResult> GetAllEmployees()
    {
      Log.Information("Getting all employees");
      return Ok(await _employeeAction.GetAllEmployees());
    }
    [HttpGet]
    [Route("{id}", Name = "GetEmployeeByID")]
    public async Task<IActionResult> GetEmployeeByID([FromRoute] int id)
    {
      var employee = await _employeeAction.GetEmployeeByID(id);
      if (employee == null)
      {
        Log.Information($"No results for employee with Employee ID : {id}");
        return NotFound($"Employee with id : {id} is not found");
      }
      return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(EmployeeDTO employeeDTO)
    {
      if (employeeDTO != null && !ModelState.IsValid)
      {
        return BadRequest();
      }
      else
      {
        await _employeeAction.CreateEmployee(employeeDTO);
        return Created();
      }
    }

    [HttpPut]
    [Route("{id}", Name = "UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO employeeDTO)
    {
      var result = await _employeeAction.UpdateEmployee(id, employeeDTO);
      if (result == null)
      {
        return NotFound();
      }
      else
      {
        return NoContent();
      }

    }


  }
}
