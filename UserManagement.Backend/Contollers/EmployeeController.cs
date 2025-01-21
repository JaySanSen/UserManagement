using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public async Task<IActionResult> GetAllEmployee()
    {
      return Ok(await _employeeAction.GetAllEmployees());
    }
    [HttpGet]
    [Route("{id}", Name = "GetEmployeeByID")]
    public async Task<IActionResult> GetEmployeeByID([FromRoute] int id)
    {
      var employee = await _employeeAction.GetEmployeeByID(id);
      return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(EmployeeDTO employeeDTO)
    {
      if(employeeDTO != null && !ModelState.IsValid){
        return BadRequest();
      }
      else{
        await _employeeAction.CreateEmployee(employeeDTO);
        return Created();
      }
    }


  }
}
