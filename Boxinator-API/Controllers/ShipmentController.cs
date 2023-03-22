using AutoMapper;
using Boxinator_API.CustomExceptions;
using Boxinator_API.DTOs.ShipmentDtos;
using Boxinator_API.Services.ShipmentDataAccess.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boxinator_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentAdminService _shipmentContext;
        private readonly IMapper _mapper;

        public ShipmentController(IShipmentAdminService shipmentContext, IMapper mapper)
        {
            _shipmentContext = shipmentContext;
            _mapper = mapper;
        }

        // GET: api/ShipmentAdmin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipmentController>>> GetShipments()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllCurrentShipmentsForAdmin()));
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
