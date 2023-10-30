using DeepFrees.SettingsService.Microservice;
using DeepFrees.SettingsService.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.SettingsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly SettingsDataService _SettingsDataServices;

        public SettingsController(SettingsDataService SettingsDataService)
        {
            _SettingsDataServices = SettingsDataService;
        }

        //Get UserDetails
        [HttpGet("GetSettings")]
        public async Task<IActionResult> Get()
        {
            var sett = await _SettingsDataServices.GetAsync();

            if (sett is null)
            {
                return NotFound();
            }

            return Ok(sett);
        }

        //Get UserDetails
        [HttpPost("PostSettings")]
        public async Task<IActionResult> Post(SettingsModel SettingsModel)
        {
            try
            {
                await _SettingsDataServices.CreateAsync(SettingsModel);

            }
            catch (Exception)
            {

                throw;
            }


            return Ok();
        }

        //Account Update
        [HttpPut("UpdateSettings")]
        public async Task<IActionResult> Update(SettingsModel SettingsModel)
        {
            try
            {
                if (await _SettingsDataServices.GetAsync(SettingsModel._id) != null)
                {
                    //updateduser._id = (await _SettingsDataServices.GetAsync(updateduser.UserName))._id;
                    await _SettingsDataServices.UpdateAsync(SettingsModel._id, SettingsModel);

                    try
                    {
                        return Ok(" Settings Updated Succesfully");
                    }
                    catch (Exception e)
                    {

                        return BadRequest(e);
                    }
                }
                else
                {
                    return BadRequest("Settings Does not Exist");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
