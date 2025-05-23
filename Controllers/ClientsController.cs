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
    public class ClientsController : Controller
    {
        private readonly MedelStoreContext _context;

        public ClientsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients
                .Include(c => c.Adress)
                .Include(c => c.Passport)
                .ToListAsync();
            return View(clients);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Adress)
                .Include(c => c.Passport)
                .FirstOrDefaultAsync(m => m.IdClients == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["AdressId"] = new SelectList(_context.Adresses, "IdAdress", "IdAdress");
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "IdPasports");
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_clients,suname,name,fatherland,date_of_birth,adress_ID,passport_ID")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["adress_ID"] = new SelectList(_context.Adresses, "ID_adress", "street", client.AdressId);
            ViewData["passport_ID"] = new SelectList(_context.Pasports, "ID_pasports", "serial", client.PassportId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["AdressId"] = new SelectList(_context.Adresses, "IdAdress", "IdAdress", client.AdressId);
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "IdPasports", client.PassportId);
            return View(client);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_clients,suname,name,fatherland,date_of_birth,adress_ID,passport_ID")] Client client)
        {
            if (id != client.IdClients)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.IdClients))
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
            ViewData["adress_ID"] = new SelectList(_context.Adresses, "ID_adress", "street", client.AdressId);
            ViewData["passport_ID"] = new SelectList(_context.Pasports, "ID_pasports", "serial", client.PassportId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Adress)
                .Include(c => c.Passport)
                .FirstOrDefaultAsync(m => m.IdClients == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.IdClients == id);
        }
    }
}
