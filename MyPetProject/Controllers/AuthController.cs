using Microsoft.AspNetCore.Mvc;
using MyPetProject.Repositories;

namespace MyPetProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            var user = await _userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if (user is null) return BadRequest("Wrong username or password.");

            var token = await _tokenHandler.CreateTokenAsync(user);

            return Ok(token);
        }
    }
}
