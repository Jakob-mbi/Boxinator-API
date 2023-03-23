using AutoMapper;
using Boxinator_API.CustomExceptions;
using Boxinator_API.DTOs.ShipmentDtos;
using Boxinator_API.Services.ShipmentDataAccess.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Boxinator_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]  
    public class ShipmentUserController : ControllerBase
    {
        private readonly IShipmentUserService _shipmentContext;
        private readonly IMapper _mapper;

        public ShipmentUserController(IShipmentUserService shipmentContext, IMapper mapper)
        {
            _shipmentContext = shipmentContext;
            _mapper = mapper;
        }
        string sub = "9e305eb4-7639-422d-9432-a3e001c6c5b7";
        /// <summary>
        /// List of current shipments
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetShipments()
        {
            string subject = sub;
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllShipmentsForAuthenticatedUser(subject)));
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
            string subject = sub;
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllCancelledShipmentsForAuthenticatedUser(subject)));
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
            string subject = sub;
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllCompletedShipmentsForAuthenticatedUser(subject)));
            }
            catch (ShipmentNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }
    }
}
