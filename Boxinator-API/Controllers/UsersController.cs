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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Newtonsoft.Json;
using Boxinator_API.ExtractToken;
using Microsoft.AspNetCore.SignalR;

namespace Boxinator_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    [EnableCors("_myAllowSpecificOrigins")]
    public class UsersController : ControllerBase
    {
        private readonly BoxinatorDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService= userService;
            _mapper= mapper;
            _configuration= configuration;
        }

        /// <summary>
        /// List of Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }

            if (subject != null)
            {
                return Ok($"this is sub : {subject}");
            }
            /* return BadRequest("No sub found");*/

            var subClaim = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", StringComparison.InvariantCultureIgnoreCase));
            if (subClaim != null)
            {
                return Ok($"this is sub : {subClaim}");
            }
            return BadRequest("No sub found");
           // return Ok(_mapper.Map<IEnumerable<UserDto>>(await _userService.GetAllUsers()));
        }

        /// <summary>
        /// Get User by sub
        /// </summary>
        /// <returns></returns>
        [HttpGet("{sub}")]
        public async Task<ActionResult<UserDto>> GetUser(string sub)
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

        // This endpoint is used to update user information in Account page
        /// <summary>
        /// Update user info
        /// </summary>
        /// <returns></returns>
        [HttpPut()]

        public async Task<IActionResult> PutUser(UserPutDto userPutDto)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _mapper.Map<User>(userPutDto);

            await _userService.UpdateUser(user, subject);


            return NoContent();
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <returns></returns>
        [HttpPost("{roleId}")]
        public async Task<ActionResult<UserDto>> PostUser(int roleId)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
        
            Console.WriteLine(subject + " : " + roleId);
            UserCreateDto userCreateDto = new UserCreateDto();
           
            var user = _mapper.Map<User>(userCreateDto);

            await _userService.AddUser(subject, user, roleId, email);
            return CreatedAtAction(nameof(GetUser), new { id = user.Sub }, user);

        }

        /// <summary>
        /// Delete user by sub
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{sub}")]
        public async Task<IActionResult> DeleteUser(string sub)
        {
            try
            {
                await _userService.DeleteUser(sub);
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

        // This end point is used to retrieve user information into Account page
        /// <summary>
        /// Get user by sub 
        /// </summary>
        /// <returns></returns>

        [HttpGet("usersSub")]
        public async Task<ActionResult<UserDto>> GetUserSub()
        {

            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                return Ok(_mapper.Map<UserDto>(await _userService.GetUserBySub(subject)));
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



        /* private bool UserExists(int id)
         {
             return (_context.Users?.Any(e => e.Sub == id)).GetValueOrDefault();
         }*/
    }
}
