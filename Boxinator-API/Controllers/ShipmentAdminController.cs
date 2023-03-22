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

namespace Boxinator_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentAdminController : ControllerBase
    {
        private readonly IShipmentAdminService _shipmentContext;
        private readonly IMapper _mapper;

        public ShipmentAdminController(IShipmentAdminService shipmentContext, IMapper mapper)
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

        //// GET: api/ShipmentAdmin/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Shipment>> GetShipment(int id)
        //{
        //  if (_context.Shipments == null)
        //  {
        //      return NotFound();
        //  }
        //    var shipment = await _context.Shipments.FindAsync(id);

        //    if (shipment == null)
        //    {
        //        return NotFound();
        //    }

        //    return shipment;
        //}

        //// PUT: api/ShipmentAdmin/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutShipment(int id, Shipment shipment)
        //{
        //    if (id != shipment.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(shipment).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ShipmentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/ShipmentAdmin
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Shipment>> PostShipment(Shipment shipment)
        //{
        //  if (_context.Shipments == null)
        //  {
        //      return Problem("Entity set 'BoxinatorDbContext.Shipments'  is null.");
        //  }
        //    _context.Shipments.Add(shipment);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetShipment", new { id = shipment.Id }, shipment);
        //}

        //// DELETE: api/ShipmentAdmin/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteShipment(int id)
        //{
        //    if (_context.Shipments == null)
        //    {
        //        return NotFound();
        //    }
        //    var shipment = await _context.Shipments.FindAsync(id);
        //    if (shipment == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Shipments.Remove(shipment);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ShipmentExists(int id)
        //{
        //    return (_context.Shipments?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
