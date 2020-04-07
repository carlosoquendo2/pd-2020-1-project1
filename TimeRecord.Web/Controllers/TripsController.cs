using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TimeRecord.Web.Data;
using TimeRecord.Web.Data.Entities;
using TimeRecord.Web.Helpers;

namespace TimeRecord.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TripsController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _convertHelper;

        public TripsController(DataContext context, IConverterHelper convertHelper)
        {
            _context = context;
            _convertHelper = convertHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context
                .Trips
                .Include(t => t.TripDetails)
                .OrderBy(t => t.StartDate)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TripEntity tripEntity = await _context
                .Trips
                .Include(t => t.TripDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripEntity == null)
            {
                return NotFound();
            }

            return View(tripEntity);
        }

        public async Task<IActionResult> DetailsVoucher(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TripDetailEntity tripDetailEntity = await _context
                .TripDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripDetailEntity == null)
            {
                return NotFound();
            }

            return View(tripDetailEntity);
        }
    }
}
