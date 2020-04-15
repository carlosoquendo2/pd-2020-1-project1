using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Common.Models;
using TimeRecord.Web.Data;
using TimeRecord.Web.Data.Entities;
using TimeRecord.Web.Helpers;

namespace TimeRecord.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TripsController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: api/Trips/User
        [ActionName("User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripsByUser([FromRoute] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                List<TripEntity> tripEntity = await _context
                                                    .Trips
                                                    .Include(t => t.TripDetails)
                                                    .ThenInclude(td => td.ExpenseType)
                                                    .Include(t => t.User)
                                                    .Where(t => t.User.Id == id)
                                                    .OrderBy(t => t.StartDate)
                                                    .ToListAsync();

                if (tripEntity == null)
                {
                    return NotFound();
                }

                return Ok(_converterHelper.ToTripsResponse(tripEntity));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (ApplicationException aex)
            {
                return StatusCode(500, aex.Message);
            }
        }

        // GET: api/Trips/5
        [ActionName("Trip")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripEntity = await _context
                                    .Trips
                                    .Include(t => t.TripDetails)
                                    .ThenInclude(td => td.ExpenseType)
                                    .Include(t => t.User)
                                    .FirstOrDefaultAsync(t => t.Id == id);

            if (tripEntity == null)
            {
                return NotFound();
            }

            return Ok(_converterHelper.ToTripResponse(tripEntity));
        }

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripEntity([FromRoute] int id, [FromBody] TripEntity tripEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tripEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(tripEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trips
        [HttpPost]
        public async Task<IActionResult> PostTripEntity([FromBody] TripEntity tripEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trips.Add(tripEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTripEntity", new { id = tripEntity.Id }, tripEntity);
        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripEntity = await _context.Trips.FindAsync(id);
            if (tripEntity == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(tripEntity);
            await _context.SaveChangesAsync();

            return Ok(tripEntity);
        }

        private bool TripEntityExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}