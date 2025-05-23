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
    public class PostavkisController : Controller
    {
        private readonly MedelStoreContext _context;

        public PostavkisController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Postavkis
        public async Task<IActionResult> Index()
        {
            var medelStoreContext = _context.Postavkis.Include(p => p.Mebel).Include(p => p.Postavchika);
            return View(await medelStoreContext.ToListAsync());
        }

        // GET: Postavkis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postavki = await _context.Postavkis
                .Include(p => p.Mebel)
                .Include(p => p.Postavchika)
                .FirstOrDefaultAsync(m => m.IdPostavki == id);
            if (postavki == null)
            {
                return NotFound();
            }

            return View(postavki);
        }

        // GET: Postavki/Create
        public IActionResult Create()
        {
            ViewData["postavchika_ID"] = new SelectList(_context.Postavhiks, "ID_postavchik", "name");
            ViewData["mebel_ID"] = new SelectList(_context.Mebels, "ID_mebel", "product_name");
            return View();
        }

        // POST: Postavki/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_postavki,postavchika_ID,mebel_ID,date_postavki,Price_postavki,Quantity")] Postavki postavki)
        {
            if (ModelState.IsValid)
            {
                // Проверка на дублирование поставки
                if (_context.Postavkis.Any(p =>
                    p.PostavchikaId == postavki.PostavchikaId &&
                    p.MebelId == postavki.MebelId &&
                    p.DatePostavki == postavki.DatePostavki))
                {
                    ModelState.AddModelError("", "Такая поставка уже существует");
                    ViewData["postavchika_ID"] = new SelectList(_context.Postavhiks, "ID_postavchik", "name", postavki.PostavchikaId);
                    ViewData["mebel_ID"] = new SelectList(_context.Mebels, "ID_mebel", "product_name", postavki.MebelId);
                    return View(postavki);
                }

                _context.Add(postavki);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["postavchika_ID"] = new SelectList(_context.Postavhiks, "ID_postavchik", "name", postavki.PostavchikaId);
            ViewData["mebel_ID"] = new SelectList(_context.Mebels, "ID_mebel", "product_name", postavki.MebelId);
            return View(postavki);
        }

        // GET: Postavkis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postavki = await _context.Postavkis.FindAsync(id);
            if (postavki == null)
            {
                return NotFound();
            }
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", postavki.MebelId);
            ViewData["PostavchikaId"] = new SelectList(_context.Postavhiks, "IdPostavchik", "IdPostavchik", postavki.PostavchikaId);
            return View(postavki);
        }

        // POST: Postavki/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_postavki,postavchika_ID,mebel_ID,date_postavki,Price_postavki,Quantity")] Postavki postavki)
        {
            if (id != postavki.IdPostavki)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postavki);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostavkiExists(postavki.IdPostavki))
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
            ViewData["postavchika_ID"] = new SelectList(_context.Postavhiks, "ID_postavchik", "name", postavki.PostavchikaId);
            ViewData["mebel_ID"] = new SelectList(_context.Mebels, "ID_mebel", "product_name", postavki.MebelId);
            return View(postavki);
        }

        // GET: Postavkis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postavki = await _context.Postavkis
                .Include(p => p.Mebel)
                .Include(p => p.Postavchika)
                .FirstOrDefaultAsync(m => m.IdPostavki == id);
            if (postavki == null)
            {
                return NotFound();
            }

            return View(postavki);
        }

        // POST: Postavkis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postavki = await _context.Postavkis.FindAsync(id);
            if (postavki != null)
            {
                _context.Postavkis.Remove(postavki);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostavkiExists(int id)
        {
            return _context.Postavkis.Any(e => e.IdPostavki == id);
        }
    }
}
