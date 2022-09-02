using Manager.API.Utilities;
using Manager.Application.DTOs;
using Manager.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserService _userService;

        public AuthController(ITokenGenerator tokenGenerator, IUserService userService)
        {
            _tokenGenerator = tokenGenerator;
            _userService = userService;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> SignIn([FromBody] LoginDto userDto)
        {
            try
            {
                var user = await _userService.GetByEmailAsync(userDto.Login);
                if (user == null)
                    return NotFound();

                if (userDto.Password != user.Password)
                    return BadRequest();

                var token = _tokenGenerator.GenerateToken(userDto);

                return Ok(new ResultViewModel
                {
                    Message = "User Authenticated",
                    Success = true,
                    Data = token
                });

            }
            catch(Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}
