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
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;

        public TripsController(
            DataContext context, 
            IConverterHelper 
            converterHelper, 
            IUserHelper userHelper,
            IImageHelper imageHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
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

        // POST: api/Trips/Add
        [ActionName("Add")]
        [HttpPost]
        public async Task<IActionResult> PostTripEntity([FromBody] TripRequest tripRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TripEntity tripEntity = _converterHelper.tripRequestToEntity(tripRequest);
            tripEntity.User = await _userHelper.GetUserAsync(tripRequest.UserEmail);

            _context.Trips.Add(tripEntity);
            var newTrip = await _context.SaveChangesAsync();


            //return CreatedAtAction("GetTripEntity", new { id = tripEntity.Id }, tripEntity);
            return Ok(new Response
            {
                IsSuccess = true,
                Message = "Success",
                Result = tripEntity
            });
        }

        // POST: api/Trips/AddDetail
        [ActionName("AddDetail")]
        [HttpPost]
        public async Task<IActionResult> PostTripDetailEntity([FromBody] TripDetailRequest tripDetailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string voucherPath = string.Empty;
            if (tripDetailRequest.VoucherArray != null && tripDetailRequest.VoucherArray.Length > 0)
            {
                voucherPath = _imageHelper.UploadImage(tripDetailRequest.VoucherArray, "Voucher");
            }

            TripDetailEntity tripDetailEntity = _converterHelper.tripDetailRequestToEntity(tripDetailRequest);
            tripDetailEntity.Date = new DateTime(tripDetailRequest.Year, tripDetailRequest.Mounth, tripDetailRequest.Day, 0, 0, 0);
            tripDetailEntity.AttachmentPath = voucherPath;
            int id = int.Parse(tripDetailRequest.TripId);
            var tripEntity = await _context.Trips.FindAsync(id);
            if (tripEntity == null)
            {
                return NotFound();
            }
            else
            {
                tripDetailEntity.Trip = tripEntity;
            }
            id = int.Parse(tripDetailRequest.ExpenseTypeId);
            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }
            else
            {
                tripDetailEntity.ExpenseType = expenseType;
            }

            _context.TripDetails.Add(tripDetailEntity);
            var newTrip = await _context.SaveChangesAsync();

            Response response = new Response
            {
                IsSuccess = true,
                Message = "Success",
                Result = tripDetailEntity
            };

            return Ok();
        }

        private bool TripEntityExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}