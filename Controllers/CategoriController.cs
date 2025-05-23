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
    public class CategoriController : Controller
    {
        private readonly MedelStoreContext _context;

        public CategoriController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Categori
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoris.ToListAsync());
        }

        // GET: Categori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categori = await _context.Categoris
                .FirstOrDefaultAsync(m => m.IdCategori == id);
            if (categori == null)
            {
                return NotFound();
            }

            return View(categori);
        }

        // GET: Categori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categori/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_categori,name_categori")] Categori categori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categori);
        }

        // GET: Categori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categori = await _context.Categoris.FindAsync(id);
            if (categori == null)
            {
                return NotFound();
            }
            return View(categori);
        }

        // POST: Categori/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_categori,name_categori")] Categori categori)
        {
            if (id != categori.IdCategori)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriExists(categori.IdCategori))
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
            return View(categori);
        }

        // GET: Categori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categori = await _context.Categoris
                .FirstOrDefaultAsync(m => m.IdCategori == id);
            if (categori == null)
            {
                return NotFound();
            }

            return View(categori);
        }

        // POST: Categori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categori = await _context.Categoris.FindAsync(id);
            if (categori != null)
            {
                _context.Categoris.Remove(categori);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriExists(int id)
        {
            return _context.Categoris.Any(e => e.IdCategori == id);
        }
    }
}
