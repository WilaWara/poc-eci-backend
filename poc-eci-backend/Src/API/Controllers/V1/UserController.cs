using API.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO userCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User newUser = _mapper.Map<User>(userCreateDTO);
            var savedUser = await _userServices.Create(newUser);

            var responseUser = _mapper.Map<UserResponseDTO>(savedUser);
            return CreatedAtAction("Successfully created", new { id = responseUser.Id }, responseUser);
        }
    }
}
