using API.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        private readonly IMapper _mapper;

        public UserController(IUserService userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO userCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User newUser = _mapper.Map<User>(userCreateDTO);
            User savedUser = await _userServices.Create(newUser);

            var responseUser = _mapper.Map<UserResponseDTO>(savedUser);
            return Ok(responseUser);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDTO userLoginRequestDTO)
        {
            if (!ModelState.IsValid || userLoginRequestDTO == null)
            {
                return BadRequest(ModelState);
            }

            IDictionary<User, string> userToken =
                await _userServices.Login(userLoginRequestDTO.Email, userLoginRequestDTO.Password);

            UserLoginResponseDTO userLoginResponseDTO = new UserLoginResponseDTO
            {
                User = userToken.Keys.First(),
                Token = userToken.Values.First()
            };

            return Ok(userLoginResponseDTO);
        }
    }
}
