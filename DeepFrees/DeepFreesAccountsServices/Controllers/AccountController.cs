using DeepFreesAccountsServices.Model;
using DeepFreesAccountsServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeepFreesAccountsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserAccountService _UserAccountServices;

        public AccountController(UserAccountService userAccountService)
        {
            _UserAccountServices = userAccountService;
        }

        //Get UserDetails
        [HttpGet("{Username}")]
        public async Task<IActionResult> Get(string Username)
        {
            var uacc = await _UserAccountServices.GetAsync(Username);

            if (uacc is null)
            {
                return NotFound();
            }

            return Ok(uacc);
        }

        //Account Creation
        [HttpPost]
        public async Task<IActionResult> Post(UserAccount newuser)
        {
            try
            {
                if(await _UserAccountServices.GetAsync(newuser.UserName.ToLower()) != null || await _UserAccountServices.GetAsync(newuser.UserName) != null)
                {
                    return BadRequest("Account Already Exist");
                }
                else
                {
                    await _UserAccountServices.CreateAsync(newuser);
                    return Created(nameof(Get), newuser);
                }
                
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        //Account Update
        [HttpPut]
        public async Task<IActionResult> Update(UserAccount updateduser)
        {
            try
            {
                if (await _UserAccountServices.GetAsync(updateduser.UserName) != null)
                {
                    //updateduser._id = (await _UserAccountServices.GetAsync(updateduser.UserName))._id;
                    await _UserAccountServices.UpdateAsync(updateduser.UserName, updateduser);
                    
                    try
                    {
                        return Ok(" Account Updated Succesfully");
                    }
                    catch (Exception e)
                    {

                        return BadRequest(e);
                    }
                }
                else
                {
                    return BadRequest("Account Does not Exist");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Account Update
        [HttpDelete("{Username}")]
        public async Task<IActionResult> Delete(string Username)
        {
            var uacc = await _UserAccountServices.GetAsync(Username);

            if (uacc is null)
            {
                return NotFound();
            }
            else
            {
                await _UserAccountServices.RemoveAsync(Username);
                return Ok("Account Removed");
            }
        }
    }
}