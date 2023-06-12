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
        public async Task<ActionResult<UserAccount>> Get(string Username)
        {
            var uacc = await _UserAccountServices.GetAsync(Username);

            if (uacc is null)
            {
                return NotFound();
            }

            return uacc;
        }

        //Account Creation
        [HttpPost]
        public async Task<IActionResult> Post(UserAccount newuser)
        {
            try
            {
                await _UserAccountServices.CreateAsync(newuser);

                return Created(nameof(Get), newuser);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }
    }
}