using DeepFrees.TechnicianService.MicroService;
using DeepFrees.TechnicianService.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DeepFrees.TechnicianService.Controllers
{
    [Route("Technicianservice/[controller]")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly TechnicianDataContext _TechnicianDataContext;

        public TechnicianController(TechnicianDataContext technicianDataContext)
        {
            _TechnicianDataContext = technicianDataContext;
        }

        [HttpGet("GetTechnicians")]
        public async Task<IActionResult> Get() //This method returns a List of techs
        {
            var WorkTechnicians = await _TechnicianDataContext.GetAsync();

            if (WorkTechnicians != null)
            {
                return Ok(WorkTechnicians);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetTechnicians/{NIC}")]
        public async Task<IActionResult> Get(string NIC) //This method returns a single tech
        {
            var WorkTask1 = await _TechnicianDataContext.GetAsync(NIC);

            if (WorkTask1 != null)
            {
                return Ok(WorkTask1);
            }
            else
            {
                return NotFound();
            }
        }

        //Creation
        [HttpPost("CreateTechnician")]
        public async Task<IActionResult> Post(Technician Technician) //Create a Single techs
        {
            if (Technician != null)
            {
                Technician._id = ObjectId.GenerateNewId();
                await _TechnicianDataContext.CreateAsync(Technician);
                await Console.Out.WriteLineAsync("Working...");
                return Ok();
            }
            else
            {
                await Console.Out.WriteLineAsync("Not Working...");

                return BadRequest();
            }
        }

        //Update
        [HttpPut("UpdateTechnician/{NIC}")]
        public async Task<IActionResult> Update(string NIC, [FromBody] Technician Technician)
        {
            if (Technician != null)
            {
                await _TechnicianDataContext.UpdateAsync(NIC, Technician);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //Delete
        [HttpDelete("DeleteTechnician/{NIC}")]
        public async Task<IActionResult> Delete(string NIC)
        {
            var WorkTask1 = await _TechnicianDataContext.GetAsync(NIC);

            if (WorkTask1 != null)
            {
                await _TechnicianDataContext.RemoveAsync(NIC);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
