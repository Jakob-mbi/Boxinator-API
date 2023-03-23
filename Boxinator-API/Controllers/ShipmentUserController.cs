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

        // GET: api/ShipmentAdmin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetShipments()
        {
            string subject = "e1c3c5df-7f33-4e8f-9c17-ff04627347ee";
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
    }
}
