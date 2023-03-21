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
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService= userService;
            _mapper= mapper;
            _configuration= configuration;
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
        [HttpPut()]
        
        public async Task<IActionResult> PutUser(UserPutDto userPutDto)
        {

            if (!Request.Headers.TryGetValue("Authorization", out var token))
            {
                return Unauthorized();
            }
            GetSubClaimFromToken tokenExtractor = new GetSubClaimFromToken();
            var subClaim = await tokenExtractor.ExtractTokenUserSub(token , _configuration);

              if (subClaim != null)
            {
                return Ok($"this is sub : {subClaim}");
            }
            return BadRequest("No sub founded");


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
        /*[HttpGet("usersSub/{sub}")]
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
        }*/


        /*var subClaim = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", StringComparison.InvariantCultureIgnoreCase));
           //var subClaim = HttpContext.User.FindFirstValue("sub");

           if (subClaim != null)
           {
               return Ok($"this is sub : {subClaim.Value}");
           }
           return BadRequest("No sub founded");*/

        /*
                [HttpGet("usersSub")]
                public async Task<ActionResult<UserDto>> GetUserSub()
                {

                    if (!Request.Headers.TryGetValue("Authorization", out var token))
                    {
                        return Unauthorized();
                    }

                    // Extract the JWT token from the Authorization header
                    var jwtToken = token.ToString().Substring("Bearer ".Length).Trim();

                    // Decode the JWT token
                    var handler = new JwtSecurityTokenHandler();
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = , // Set your security key here
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    SecurityToken validatedToken;
                    var claimsPrincipal = handler.ValidateToken(jwtToken, validationParameters, out validatedToken);

                    // Get the sub claim from the JWT token
                    var subClaim = claimsPrincipal.FindFirst("sub")?.Value;

                    try
                    {
                        return Ok(_mapper.Map<UserDto>(await _userService.GetUserBySub(subClaim)));
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
        */


        [HttpGet("usersSub")]
        public async Task<ActionResult<UserDto>> GetUserSub()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var token))
            {
                return Unauthorized();
            }

            // Extract the JWT token from the Authorization header
            var jwtToken = token.ToString().Substring("Bearer ".Length).Trim();

            // Retrieve the JsonWebKeySet from Keycloak instance
            var client = new HttpClient();
            var keyUri = _configuration["JWT:key-uri"];      
            var response = await client.GetAsync(keyUri);
            var responseString = await response.Content.ReadAsStringAsync();
            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);

            // Get the kid claim from the JWT token's header
            var handler = new JwtSecurityTokenHandler();
            var jwtHeader = handler.ReadJwtToken(jwtToken).Header;
            var kid = jwtHeader.Kid;

            // Find the appropriate key from the JsonWebKeySet based on the kid claim
            var key = keys.Keys.FirstOrDefault(k => k.Kid == kid);

            if (key == null)
            {
                return BadRequest("Invalid JWT token");
            }

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JWT:issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:audience"]
            };

            SecurityToken validatedToken;
            /*handler = new JwtSecurityTokenHandler();*/
            var claimsPrincipal = handler.ValidateToken(jwtToken, validationParameters, out validatedToken);

            // Get the sub claim from the JWT token
            var subClaim = claimsPrincipal.FindFirst("sub")?.Value;

            try
            {
                return Ok(_mapper.Map<UserDto>(await _userService.GetUserBySub(subClaim)));
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
