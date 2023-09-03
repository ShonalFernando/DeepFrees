using DeepFrees.EmployeeService.MicroService;
using DeepFrees.EmployeeService.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeAccountsService _EmployeeAccountsService;

        public EmployeeController(EmployeeAccountsService employeeAccountsService)
        {
            _EmployeeAccountsService = employeeAccountsService;
        }

        //Get UserDetails
        [HttpGet("{NIC}")]
        public async Task<IActionResult> Get(string NIC)
        {
            var uacc = await _EmployeeAccountsService.GetAsync(NIC);

            if (uacc is null)
            {
                return NotFound();
            }

            return Ok(uacc);
        }

        //Account Creation
        [HttpPost]
        public async Task<IActionResult> Post(Employee NewEmployee)
        {
            try
            {
                if (await _EmployeeAccountsService.GetAsync(NewEmployee.NIC) == null!)
                {
                    return BadRequest("Employee Already Exist");
                }
                else
                {
                    await _EmployeeAccountsService.CreateAsync(NewEmployee);
                    return Created(nameof(Get), NewEmployee);
                }

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        //Account Update
        [HttpPut]
        public async Task<IActionResult> Update(Employee Employee)
        {
            try
            {
                if (await _EmployeeAccountsService.GetAsync(Employee.NIC) != null)
                {
                    try
                    {
                        return Ok(" Employee Updated Succesfully");
                    }
                    catch (Exception e)
                    {

                        return BadRequest(e);
                    }
                }
                else
                {
                    return NotFound("Employee Does not Exist");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Account Update
        [HttpDelete]
        public async Task<IActionResult> Delete(string NIC)
        {
            var uacc = await _EmployeeAccountsService.GetAsync(NIC);

            if (uacc is null)
            {
                return NotFound("Employee Does not Exist");
            }
            else
            {
                await _EmployeeAccountsService.RemoveAsync(NIC);
                return Ok("Employee Removed");
            }
        }
    }
}
