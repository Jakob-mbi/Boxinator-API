using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Boxinator_API.Models;
using Boxinator_API.Services.ShipmentDataAccess.Admin;
using Boxinator_API.CustomExceptions;
using AutoMapper;
using Boxinator_API.DTOs.ShipmentDtos;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Boxinator_API.DTOs.StatusDtos;

namespace Boxinator_API.Controllers
{
    [ApiController]
    //[Authorize(Roles = "ADMIN")]
    [Route("api/v1/admin/shipment")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ShipmentAdminController : ControllerBase
    {
        private readonly IShipmentAdminService _shipmentContext;
        private readonly IMapper _mapper;

        public ShipmentAdminController(IShipmentAdminService shipmentContext, IMapper mapper)
        {
            _shipmentContext = shipmentContext;
            _mapper = mapper;
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
        /// <summary>
        /// List of cancelled shipments
        /// </summary>
        /// <returns></returns>
        [HttpGet("cancelled")]
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetCancelledShipments()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllCancelledShipmentsForAdmin()));
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
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadAllCompletedShipmentsForAdmin()));
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
        /// Get shipments by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetShipmentDTO>> GetShipmentsbyId(int id)
        {
            try
            {
                return Ok(_mapper.Map<GetShipmentDTO>(await _shipmentContext.ReadShipmentByIdAdmin(id)));
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
        /// List of shipments by Customer
        /// </summary>
        /// <returns></returns>
        [HttpGet("customer/{customerid}")]
        public async Task<ActionResult<IEnumerable<GetShipmentDTO>>> GetShipmentsbyCustomer(string customerid)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetShipmentDTO>>(await _shipmentContext.ReadShipmentByCustomer(customerid)));
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
        /// Delete shipments by id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            try
            {
                var shipment = await _shipmentContext.ReadShipmentByIdAdmin(id);
                await _shipmentContext.DeleteShipment(shipment);
            }
            catch (ShipmentNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

            return NoContent();
        }
        /// <summary>
        /// Update shipments by id
        /// </summary>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutShipment(int id, [FromBody] PutShipmentDTO shipment)
        {

            if (id != shipment.Id)
            {
                return BadRequest();
            }
            try
            {
                var obj = _mapper.Map<Shipment>(shipment);
                return Ok(await _shipmentContext.UpdateShipment(obj));
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
        /// Update shipment status by id
        /// </summary>
        /// <returns></returns>
        [HttpPut("status/add/{shipmentid}")]
        public async Task<IActionResult> AddStatusToShipment(int shipmentid, [FromBody] StatusDto shipmentStatus)
        {
            try
            {
                var shippment = await _shipmentContext.ReadShipmentByIdAdmin(shipmentid);
                var status = await _shipmentContext.ReadStatusById(shipmentStatus.Id);
                if(shippment.StatusList.Any(x => x.Id == status.Id)) { throw new StatusAlredyExist(); }
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
            catch(StatusAlredyExist ex) 
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent();
        }
        /// <summary>
        /// Update shipment status by id
        /// </summary>
        /// <returns></returns>
        [HttpPut("status/remove/{shipmentid}")]
        public async Task<IActionResult> RemoveStatusToShipment(int shipmentid, [FromBody] StatusDto shipmentStatus)
        {
            try
            {
                var shippment = await _shipmentContext.ReadShipmentByIdAdmin(shipmentid);
                var status = shippment.StatusList.FirstOrDefault(x => x.Id == shipmentStatus.Id);
                shippment.StatusList.Remove(status);
                await _shipmentContext.UpdateShipment(shippment);
            }
            catch (ShipmentNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            catch(StatusNotFoundException ex)
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
