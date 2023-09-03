using DeepFrees.PayrollServices.MicroService;
using Microsoft.AspNetCore.Mvc;
using PayrollServices.Model;

namespace DeepFrees.PayrollServices.Controllers
{
    [ApiController]
    [Route("api/payroll")]
    public class CallDirectingController : Controller
    {
        private readonly DataService _DataService;

        public CallDirectingController(DataService dataService)
        {
            _DataService = dataService;
        }

        //Get UserDetails
        [HttpGet("{EmpID}")]
        public async Task<IActionResult> Get(string EmpID)
        {
            var uacc = await _DataService.GetAsync(EmpID);

            if (uacc is null)
            {
                return NotFound();
            }

            return Ok(uacc);
        }

        //Account Creation
        [HttpPost]
        public async Task<IActionResult> Post(SallaryModel SallaryModel)
        {
            try
            {
                if(await _DataService.GetAsync(SallaryModel.EmpID) == null)
                {
                    await _DataService.CreateAsync(SallaryModel);
                    return Ok("Sallary Added");
                }
                else
                {
                    return BadRequest("Already Exist");
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Account Update
        [HttpPut]
        public async Task<IActionResult> Update(SallaryModel SallaryModel)
        {
            try
            {
                if (await _DataService.GetAsync(SallaryModel.EmpID) != null)
                {
                    await _DataService.UpdateAsync(SallaryModel.EmpID, SallaryModel);
                    return Ok("Sallary Entity Not Updated");
                }
                else
                {
                    return Ok("Sallary Entity Not Found to Update");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Account Update
        [HttpDelete]
        public async Task<IActionResult> Delete(string EmpID)
        {
            try
            {
                if (await _DataService.GetAsync(EmpID) != null)
                {
                    await _DataService.RemoveAsync(EmpID);
                    return Ok("Sallary Entity Removed");
                }
                else
                {
                    return BadRequest("Sallary Entity Not Found to Remove");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
