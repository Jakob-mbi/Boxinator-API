using AutoMapper;
using Boxinator_API.CustomExceptions;
using Boxinator_API.DTOs.ShipmentDtos;
using Boxinator_API.DTOs.StatusDtos;
using Boxinator_API.Models;
using Boxinator_API.Services.ShipmentDataAccess.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace Boxinator_API.Controllers
{
    [ApiController]
    [Route("api/v1/shipment")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ShipmentUserController : ControllerBase
    {
        private readonly IShipmentUserService _shipmentContext;
        private readonly IMapper _mapper;
        //private readonly string _sub;

        public ShipmentUserController(IShipmentUserService shipmentContext, IMapper mapper)
        {
            _shipmentContext = shipmentContext;
            _mapper = mapper;
        }
        /// <summary>
        /// List of current shipments
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetShipments()
        {
            
            try
            {
                var sub = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllShipmentsForAuthenticatedUser(sub)));
            }
            catch (ShipmentNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

        }

        /// <summary>
        /// List of cancelled shipments
        /// </summary>
        /// <returns></returns>
        [HttpGet("cancelled")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetCancelledShipments()
        {
            
            try
            {
                var sub = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllCancelledShipmentsForAuthenticatedUser(sub)));
            }
            catch (ShipmentNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

        }
        /// <summary>
        /// List of completed shipments
        /// </summary>
        /// <returns></returns>
        [HttpGet("completed")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetCompletedShipments()
        {
            
            try
            {
                var sub = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllCompletedShipmentsForAuthenticatedUser(sub)));
            }
            catch (ShipmentNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }
        /// <summary>
        /// List of all previous shipments
        /// </summary>
        /// <returns></returns>
        [HttpGet("previous")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetAllPreviousShipments()
        {

            try
            {
                var sub = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllPreviousShipmentsForAuthenticatedUser(sub)));
            }
            catch (ShipmentNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Get shipment by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetShipmentDTO>> GetShipmentbyId(int id)
        {
            try
            {
                var sub = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                return Ok(_mapper.Map<GetShipmentDTO>(await _shipmentContext.ReadShipmentById(id, sub)));
            }
            catch (ShipmentNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

        }
        /// <summary>
        /// create new shipment for registerd users
        /// </summary>
        /// <returns></returns>
        [HttpPost("new")]
        [Authorize]
        public async Task<ActionResult<PostShipmentDTO>> PostShipment([FromBody] PostShipmentDTO newShipment)
        {
            try
            {
                var sub = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                var shipment = _mapper.Map<Shipment>(newShipment);
                shipment.UserSub = sub;
                await _shipmentContext.CreateNewShipment(shipment);

                return CreatedAtAction(nameof(GetShipmentbyId), new { id = shipment.Id }, shipment);
            }
            catch(CountryNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });

            }

        }
        /// <summary>
        /// create new shipment for guest
        /// </summary>
        /// <returns></returns>
        [HttpPost("guest/new")]
        public async Task<ActionResult<PostShipmentDTO>> PostGuestShipment([FromBody] PostGuestShipmentDTO newShipment)
        {
            var shipment = _mapper.Map<Shipment>(newShipment);
            await _shipmentContext.CreateNewShipment(shipment);
            _shipmentContext.SendEmail(newShipment.Email);
            return NoContent();
        }
        /// <summary>
        /// cancelle shipment
        /// </summary>
        /// <returns></returns>
        [HttpPut("{shipmentid}/cancel")]
        [Authorize]
        public async Task<IActionResult> CancelShipment(int shipmentid)
        {
            try
            {
                var sub = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                var shippment = await _shipmentContext.ReadShipmentById(shipmentid,sub);
                var status = await _shipmentContext.ReadStatusById(5);
                if (shippment.StatusList.Any(x => x.Id == status.Id)) { throw new StatusAlredyExist(); }
                shippment.StatusList.Add(status);
                await _shipmentContext.UpdateShipment(shippment);
            }
            catch (ShipmentNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            catch (StatusAlredyExist ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent();
        }
    }
}
