using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Boxinator_API.Models;
using Boxinator_API.Services;
using AutoMapper;
using Boxinator_API.DTOs;
using System.Net;
using Boxinator_API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Boxinator_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class UsersController : ControllerBase
    {
        private readonly BoxinatorDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService= userService;
            _mapper= mapper;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
          return Ok(_mapper.Map<IEnumerable<UserDto>>(await _userService.GetAllUsers()));
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            User.Claims.FirstOrDefault
            try
            {
                return Ok(_mapper.Map<UserDto>(await _userService.GetUserById(id)));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{sub}")]
        public async Task<IActionResult> PutUser(UserPutDto userPutDto)
        {
          
            try
            {
                var user = _mapper.Map<User>(userPutDto);
                await _userService.UpdateUser(user);
            }catch(UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
            
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserCreateDto userCreateDto )
        {
            var user = _mapper.Map<User>(userCreateDto);
            await _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Sub }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }

            return NoContent();
        }

        // GET: api/Users/SubID2
        [HttpGet("usersSub/{sub}")]
        public async Task<ActionResult<UserDto>> GetUserSub(string sub)
        {
            try
            {
                return Ok(_mapper.Map<UserDto>(await _userService.GetUserBySub(sub)));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }

       


        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Sub == "test")).GetValueOrDefault();
        }
    }
}
