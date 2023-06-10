using DeepFreesAccountsServices.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeepFreesAccountsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {
            _AuthService = AuthService;
        }

        //Get UserDetails
        [HttpGet("{Username}")]
        public async Task<ActionResult<UserAccount>> Get(string Username)
        {
            var uacc = await _AuthService.GetAsync(Username);

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
                await _ShoppinzService.CreateAsync(newuser);

                return Created(nameof(Get), newuser);
            }
            catch (Exception e)
            {

                return BadRequest();
            }

        }
    }
