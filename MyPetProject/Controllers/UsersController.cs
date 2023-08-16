using MyPetProject.Models.Domain;
using MyPetProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using MyPetProject.Models.DTO;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace MyPetProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;

        public UsersController(IUserRepository userRepository, ITokenHandler tokenHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
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

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();

            var usersDTO = _mapper.Map<List<Models.DTO.User>>(users);

            return Ok(usersDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user is null) return NotFound();

            var userDTO = _mapper.Map<Models.DTO.User>(user);

            return Ok(userDTO);
        }

        [HttpGet]
        [Route("{name:alpha}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserByName([FromRoute]string name)
        {
            var user = await _userRepository.GetByNameAsync(name);

            if (user is null) return NotFound();

            var userDTO = _mapper.Map<Models.DTO.User>(user);

            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]Models.DTO.AddUserRequest addUserRequest)
        {
            var user = new Models.Domain.User()
            {
                Username = addUserRequest.Username,
                Email = addUserRequest.Email,
                FirstName = addUserRequest.FirstName,
                LastName = addUserRequest.LastName,
                Password = addUserRequest.Password
            };

            user = await _userRepository.AddAsync(user);

            if (user is null) return NotFound();

            var userDTO = _mapper.Map<Models.DTO.User>(user);

            return Ok(userDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _userRepository.DeleteAsync(id);

            if (user is null) return NotFound();

            var userDTO = _mapper.Map<Models.DTO.User>(user);

            return Ok(user);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUserById([FromRoute] Guid id, 
            [FromBody] Models.DTO.UpdateUserRequest updateUserRequest)
        {
            var user = new Models.Domain.User()
            {
                Username = updateUserRequest.Username,
                Email = updateUserRequest.Email,
                FirstName = updateUserRequest.FirstName,
                LastName = updateUserRequest.LastName
            };

            user = await _userRepository.UpdateAsync(id, user);

            if (user is null) return NotFound();

            var userDTO = _mapper.Map<Models.DTO.User>(user);

            return Ok(userDTO);
        }
    }
}
