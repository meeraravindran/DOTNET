using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webapi.Data;
using Webapi.Dtos.User;
using Webapi.Models;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;

        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(
                new User { Name = request.Username }, request.password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authRepo.Login(
                request.Username, request.password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}