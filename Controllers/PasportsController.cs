using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PasportsController : Controller
    {
        private readonly MedelStoreContext _context;

        public PasportsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Pasports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pasports.ToListAsync());
        }

        // GET: Pasports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasport = await _context.Pasports
                .FirstOrDefaultAsync(m => m.IdPasports == id);
            if (pasport == null)
            {
                return NotFound();
            }

            return View(pasport);
        }

        // GET: Pasports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pasport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_pasports,serial,number,date_of_issue")] Pasport pasport)
        {
            if (ModelState.IsValid)
            {
                // Проверка на уникальность паспорта
                if (_context.Pasports.Any(p => p.Serial == pasport.Serial && p.Number == pasport.Number))
                {
                    ModelState.AddModelError("", "Паспорт с такими серией и номером уже существует");
                    return View(pasport);
                }

                _context.Add(pasport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pasport);
        }

        // GET: Pasports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasport = await _context.Pasports.FindAsync(id);
            if (pasport == null)
            {
                return NotFound();
            }
            return View(pasport);
        }

        // POST: Pasport/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_pasports,serial,number,date_of_issue")] Pasport pasport)
        {
            if (id != pasport.IdPasports)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasportExists(pasport.IdPasports))
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
            return View(pasport);
        }

        // GET: Pasports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasport = await _context.Pasports
                .FirstOrDefaultAsync(m => m.IdPasports == id);
            if (pasport == null)
            {
                return NotFound();
            }

            return View(pasport);
        }

        // POST: Pasports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pasport = await _context.Pasports.FindAsync(id);
            if (pasport != null)
            {
                _context.Pasports.Remove(pasport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasportExists(int id)
        {
            return _context.Pasports.Any(e => e.IdPasports == id);
        }
    }
}
