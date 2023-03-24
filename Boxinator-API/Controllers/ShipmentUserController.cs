using AutoMapper;
using Boxinator_API.CustomExceptions;
using Boxinator_API.DTOs.ShipmentDtos;
using Boxinator_API.DTOs.StatusDtos;
using Boxinator_API.Models;
using Boxinator_API.Services.ShipmentDataAccess.User;
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
        private readonly string _sub;

        public ShipmentUserController(IShipmentUserService shipmentContext, IMapper mapper)
        {
            _shipmentContext = shipmentContext;
            _mapper = mapper;
            _sub = "9e305eb4-7639-422d-9432-a3e001c6c5b7";
        }
        /// <summary>
        /// List of current shipments
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetShipments()
        {
            
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllShipmentsForAuthenticatedUser(_sub)));
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
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetCancelledShipments()
        {
            
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllCancelledShipmentsForAuthenticatedUser(_sub)));
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
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetCompletedShipments()
        {
            
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllCompletedShipmentsForAuthenticatedUser(_sub)));
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
        public async Task<ActionResult<GetShipmentDTO>> GetShipmentbyId(int id)
        {
            try
            {
                return Ok(_mapper.Map<GetShipmentDTO>(await _shipmentContext.ReadShipmentById(id, _sub)));
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
        public async Task<ActionResult<PostShipmentDTO>> PostShipment([FromBody] PostShipmentDTO newShipment)
        {
            var shipment = _mapper.Map<Shipment>(newShipment);
            shipment.UserSub=_sub;
            await _shipmentContext.CreateNewShipment(shipment);

            return CreatedAtAction(nameof(GetShipmentbyId), new { id = shipment.Id }, shipment); 
            

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

            return NoContent();
        }
        /// <summary>
        /// cancelle shipment
        /// </summary>
        /// <returns></returns>
        [HttpPut("status/cancelled/{shipmentid}")]
        public async Task<IActionResult> AddStatusToShipment(int shipmentid)
        {
            try
            {
                var shippment = await _shipmentContext.ReadShipmentById(shipmentid,_sub);
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
