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
    public class ExpenseTypesController : Controller
    {
        private readonly DataContext _context;

        public ExpenseTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: ExpenseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpenseTypes.ToListAsync());
        }

        // GET: ExpenseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseTypeEntity = await _context.ExpenseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseTypeEntity == null)
            {
                return NotFound();
            }

            return View(expenseTypeEntity);
        }

        // GET: ExpenseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Active")] ExpenseTypeEntity expenseTypeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseTypeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseTypeEntity);
        }

        // GET: ExpenseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseTypeEntity = await _context.ExpenseTypes.FindAsync(id);
            if (expenseTypeEntity == null)
            {
                return NotFound();
            }
            return View(expenseTypeEntity);
        }

        // POST: ExpenseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Active")] ExpenseTypeEntity expenseTypeEntity)
        {
            if (id != expenseTypeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseTypeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseTypeEntityExists(expenseTypeEntity.Id))
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
            return View(expenseTypeEntity);
        }

        // GET: ExpenseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseTypeEntity = await _context.ExpenseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseTypeEntity == null)
            {
                return NotFound();
            }

            return View(expenseTypeEntity);
        }

        // POST: ExpenseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenseTypeEntity = await _context.ExpenseTypes.FindAsync(id);
            _context.ExpenseTypes.Remove(expenseTypeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseTypeEntityExists(int id)
        {
            return _context.ExpenseTypes.Any(e => e.Id == id);
        }
    }
}
