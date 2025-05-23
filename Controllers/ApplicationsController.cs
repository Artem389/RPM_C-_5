using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication2.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly MedelStoreContext _context;

        public ApplicationsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var medelStoreContext = _context.Applications.Include(a => a.Clients).Include(a => a.Mebel).Include(a => a.Staff);
            return View(await medelStoreContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Clients)
                .Include(a => a.Mebel)
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(m => m.IdApplications == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "IdClients");
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel");
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "IdStaff");
            return View();
        }

        // POST: Applications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_applications,date_of_application_submission,application_status,mebel_ID,clients_ID,staff_ID")] WebApplication2.Models.Application applications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["clients_ID"] = new SelectList(_context.Clients, "ID_clients", "suname", applications.ClientsId);
            ViewData["mebel_ID"] = new SelectList(_context.Mebels, "ID_mebel", "product_name", applications.MebelId);
            ViewData["staff_ID"] = new SelectList(_context.Staff, "ID_staff", "suname", applications.StaffId);
            return View(applications);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "IdClients", application.ClientsId);
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", application.MebelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "IdStaff", application.StaffId);
            return View(application);
        }

        // POST: Applications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_applications,date_of_application_submission,application_status,mebel_ID,clients_ID,staff_ID")] WebApplication2.Models.Application applications)
        {
            if (id != applications.IdApplications)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(applications.IdApplications))
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
            ViewData["clients_ID"] = new SelectList(_context.Clients, "ID_clients", "suname", applications.ClientsId);
            ViewData["mebel_ID"] = new SelectList(_context.Mebels, "ID_mebel", "product_name", applications.MebelId);
            ViewData["staff_ID"] = new SelectList(_context.Staff, "ID_staff", "suname", applications.StaffId);
            return View(applications);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Clients)
                .Include(a => a.Mebel)
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(m => m.IdApplications == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.IdApplications == id);
        }
    }
}
