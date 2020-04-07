using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Web.Data;
using TimeRecord.Web.Helpers;
using TimeRecord.Web.Models;

namespace TimeRecord.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TripDetailsController : Controller
    {
        private readonly DataContext _context;

        public IImageHelper _imageHelper { get; }
        public IConverterHelper _converterHelper { get; }

        public TripDetailsController(DataContext context, IImageHelper imageHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TripDetails.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripDetailEntity = await _context.TripDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripDetailEntity == null)
            {
                return NotFound();
            }

            return View(tripDetailEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripDetailViewModel tripDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (tripDetailViewModel.VoucherFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(tripDetailViewModel.VoucherFile, "Vouchers");
                }


                _context.Add(_converterHelper.ToTripDetailEntity(tripDetailViewModel, path, true));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripDetailViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripDetailEntity = await _context.TripDetails.FindAsync(id);
            if (tripDetailEntity == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToTripDetailModel(tripDetailEntity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TripDetailViewModel tripDetailViewModel)
        {
            if (id != tripDetailViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var path = tripDetailViewModel.AttachmentPath;
                    if (tripDetailViewModel.VoucherFile != null)
                    {
                        path = await _imageHelper.UploadImageAsync(tripDetailViewModel.VoucherFile, "Vouchers");
                    }

                    _context.Update(_converterHelper.ToTripDetailEntity(tripDetailViewModel, path, false));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripDetailEntityExists(tripDetailViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tripDetailViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripDetailEntity = await _context.TripDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripDetailEntity == null)
            {
                return NotFound();
            }

            return View(tripDetailEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripDetailEntity = await _context.TripDetails.FindAsync(id);
            _context.TripDetails.Remove(tripDetailEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripDetailEntityExists(int id)
        {
            return _context.TripDetails.Any(e => e.Id == id);
        }
    }
}
