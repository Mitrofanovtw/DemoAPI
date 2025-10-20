using AutoMapper;
using DemoAPI.Models;
using DemoAPI.Models.DTO;
using DemoAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_repo.GetAllUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _repo.GetUserById(id);

            if (user == null)
                return NotFound();
            else
                return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (user is null)
                return BadRequest("объект пользователя пришел пустым");

            if (String.IsNullOrEmpty(user.Email) ||
                String.IsNullOrEmpty(user.Login))
                return BadRequest("пустое поле почты или логина");

            var newUser = _repo.AddUser(user);

            return CreatedAtAction(nameof(CreateUser),
                new { Id = newUser.Id }, newUser);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (_repo.DeleteUser(id))
                return NoContent();
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User user)
        {
            if (user is null)
                return BadRequest("некорретные данные для обновления");
            if (id != user.Id)
                return BadRequest("несовпадения по id");

            var updateUser = _repo.UpdateUser(id, user);
            return Ok(updateUser);
        }

        [HttpGet("mapped")]
        public ActionResult<IEnumerable<UserDTO>> GetMappedUsers()
        {
            var users = _repo.GetAllUsers();
            var userDtos = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}/mapped")]
        public ActionResult<UserDTO> GetMappedUser(int id)
        {
            var user = _repo.GetUserById(id);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDTO>(user);
            return Ok(userDto);
        }

        [HttpPost("create-validated")]
        public ActionResult<UserDTO> CreateValidatedUser([FromBody] CreateUserDTO createUserDto)
        {
            if (createUserDto is null)
                return BadRequest("объект пользователя пришел пустым");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(createUserDto);
            var newUser = _repo.AddUser(user);

            var userDto = _mapper.Map<UserDTO>(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
        }
    }
}