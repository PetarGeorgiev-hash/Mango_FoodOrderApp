using Mango.Services.AuthApi.Dto;
using Mango.Services.AuthApi.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDto _responseDto;

        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var result = await _authService.Register(register);
            if(result != null)
            {
                _responseDto.IsSuccess = true;
                _responseDto.Result = result;
            }
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authService.Login(loginDto);
            if(user == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Wrong credentials";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = user;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterDto register)
        {
            var assignSuccessful = await _authService.AssignRole(register.Email, register.Role.ToUpper());
            if(!assignSuccessful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error assigning role";
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }
    }
}
