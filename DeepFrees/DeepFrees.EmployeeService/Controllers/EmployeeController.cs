using Commons.DeepFrees.NetworkConfiguration;
using DeepFrees.EmployeeService.MicroService;
using DeepFrees.EmployeeService.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.EmployeeService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _EmployeeDataService;

        public EmployeeController(DataContext employeeAccountsService)
        {
            _EmployeeDataService = employeeAccountsService;
        }

        [HttpGet("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Employee> Employees = await _EmployeeDataService.GetAsync();
                if (!Employees.Any())
                {
                    return NoContent();
                }
                else
                {
                    return Ok(Employees);
                }
            }
            catch (Exception error)
            {

                return Problem(error.Message);
            }
        }

        [HttpGet("GetEmployee/{NIC}")]
        public async Task<IActionResult> Get(string? NIC)
        {
            try
            {
                if (string.IsNullOrEmpty(NIC))
                {
                    return BadRequest();
                }
                else
                {
                    Employee? Employees = await _EmployeeDataService.GetAsync(NIC);
                    if (Employees == null)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return Ok(Employees);
                    }
                }
            }
            catch (Exception error)
            {

                return Problem(error.Message);
            }
        }

        //Account Creation
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> Post(Employee Employee)
        {
            if(Employee == null || String.IsNullOrEmpty(Employee.NIC))
            {
                return BadRequest();
            }
            else
            {
                if((await _EmployeeDataService.GetAsync(Employee.NIC)) == null)
                {
                    try
                    {
                        await _EmployeeDataService.CreateAsync(Employee);
                        return Created(new Uri("https://" + Ports.EmployeePort + $"api/GetEmployee/{Employee.NIC}"), Employee);
                    }
                    catch (Exception error)
                    {
                        return Problem(error.Message);
                    }
                }
                else
                {
                    return BadRequest($"Employee with {Employee.NIC} already exist");
                }
            }

        }

        //Account Update
        [HttpPut("{NIC}")]
        public async Task<IActionResult> Update(string NIC, [FromBody]Employee Employee)
        {
            try
            {
                if (await _EmployeeDataService.GetAsync(NIC) != null)
                {
                    try
                    {
                        var _id = (await _EmployeeDataService.GetAsync(NIC))._id;
                        Employee._id = _id;
                        await _EmployeeDataService.UpdateAsync(NIC, Employee);
                        return Ok(" Employee Updated Succesfully");
                    }
                    catch (Exception e)
                    {

                        return BadRequest(e.Message);
                    }
                }
                else
                {
                    return NotFound("Employee Does not Exist");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Account Update
        [HttpDelete("DeleteEmployee/{NIC}")]
        public async Task<IActionResult> Delete(string NIC)
        {
            try
            {
                if (string.IsNullOrEmpty(NIC))
                {
                    return BadRequest();
                }
                else
                {
                    Employee? Employees = await _EmployeeDataService.GetAsync(NIC);
                    if (Employees == null)
                    {
                        return NoContent();
                    }
                    else
                    {
                        await _EmployeeDataService.RemoveAsync(NIC);
                        return Ok($"Employee {NIC} Deleted Succefully");
                    }
                }
            }
            catch (Exception error)
            {

                return Problem(error.Message);
            }
        }
    }
}
