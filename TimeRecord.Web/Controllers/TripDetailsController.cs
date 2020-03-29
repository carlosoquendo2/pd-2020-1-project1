using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Web.Data;
using TimeRecord.Web.Data.Entities;

namespace TimeRecord.Web.Controllers
{
    public class TripDetailsController : Controller
    {
        private readonly DataContext _context;

        public TripDetailsController(DataContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("Id,Name,Expense,Currency,Comment,AttachmentPath,Date")] TripDetailEntity tripDetailEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripDetailEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripDetailEntity);
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
            return View(tripDetailEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Expense,Currency,Comment,AttachmentPath,Date")] TripDetailEntity tripDetailEntity)
        {
            if (id != tripDetailEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripDetailEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripDetailEntityExists(tripDetailEntity.Id))
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
            return View(tripDetailEntity);
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
