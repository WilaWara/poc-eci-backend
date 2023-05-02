using API.DTOs;
using Application.CQRS.Commands.Category;
using Application.CQRS.Commands.User;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Service;
using MediatR;
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

        private readonly IMediator _mediator;

        public UserController(
            IUserService userServices, 
            IMapper mapper,
            IMediator mediator)
        {
            _userServices = userServices;
            _mapper = mapper;

            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO userCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //To use with Service
            //User newUser = _mapper.Map<User>(userCreateDTO);
            //User savedUser = await _userServices.Create(newUser);

            CreateUserCommand query = new CreateUserCommand(
                userCreateDTO.Name,
                userCreateDTO.Email,
                userCreateDTO.Password,
                userCreateDTO.Role
            );
            User savedUser = await _mediator.Send(query);

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
