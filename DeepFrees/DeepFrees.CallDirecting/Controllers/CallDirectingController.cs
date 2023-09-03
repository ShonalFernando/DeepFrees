using DeepFrees.CallDirecting.Microservice;
using DeepFrees.CallDirecting.Model;
using DeepFreesAccountsServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.CallDirecting.Controllers
{
    [ApiController]
    [Route("api/callcenter")]
    public class CallDirectingController : Controller
    {
        private readonly DataService _DataService;

        public CallDirectingController(DataService dataService)
        {
            _DataService = dataService;
        }

        //Get UserDetails
        [HttpGet("{EmpID}")]
        public async Task<IActionResult> Get(int EmpID)
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
        public async Task<IActionResult> Post(CallPool CallPool)
        {
            try
            {
                var cps = CallDivetor.CallPoolSolver(CallPool);
                foreach(var cp in cps)
                {
                    await _DataService.CreateAsync(cp);
                }
                return Ok(cps);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        //Account Update
        [HttpPut]
        public async Task<IActionResult> Update(CallPool CallPool)
        {
            try
            {
                var cps = CallDivetor.CallPoolSolver(CallPool);
                foreach (var cp in cps)
                {
                    await _DataService.UpdateAsync(cp.EmpID, cp);
                }
                return Ok(cps);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Account Update
        [HttpDelete]
        public async Task<IActionResult> Delete(string Username)
        {
            return Ok("Not Implimented");
        }
    }
}
