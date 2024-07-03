using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Farmacie.Models;
using Microsoft.AspNetCore.Authorization;

namespace Farmacie.Controllers
{
    public class FarmacieController : Controller
    {
        private readonly FarmaciaContext _context;

        public FarmacieController(FarmaciaContext context)
        {
            _context = context;
        }

        // GET: Farmacie
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Include(f => f.IdComuneNavigation.Fraziones)
                .ToListAsync();

            return View(farmacie);
        }


        // GET: Farmacie/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Include(f => f.IdComuneNavigation.Fraziones)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (farmacie == null)
            {
                return NotFound();
            }

            return View(farmacie);
        }

        // GET: Farmacie/Create
        [Authorize(Roles = "Manager, Admin")]
        public IActionResult Create()
        {
            ViewData["IdComune"] = new SelectList(_context.Comunes, "Id", "Id");
            return View();
        }

        // POST: Farmacie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codiceidentificativofarmacia,Codfarmaciaassegnatodaasl,Indirizzo,Descrizionefarmacia,Partitaiva,Cap,IdComune,Datainiziovalidita,Datafinevalidita,Descrizionetipologia,Codicetipologia,Latitudine,Longitudine,Localize")] Farmacia farmacia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmacia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdComune"] = new SelectList(_context.Comunes, "Id", "Id", farmacia.IdComune);
            return View(farmacia);
        }

        // GET: Farmacie/Edit/5
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacies.FindAsync(id);
            if (farmacia == null)
            {
                return NotFound();
            }
            ViewData["IdComune"] = new SelectList(_context.Comunes, "Id", "Id", farmacia.IdComune);
            return View(farmacia);
        }

        // POST: Farmacie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codiceidentificativofarmacia,Codfarmaciaassegnatodaasl,Indirizzo,Descrizionefarmacia,Partitaiva,Cap,IdComune,Datainiziovalidita,Datafinevalidita,Descrizionetipologia,Codicetipologia,Latitudine,Longitudine,Localize")] Farmacia farmacia)
        {
            if (id != farmacia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmacia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmaciaExists(farmacia.Id))
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
            ViewData["IdComune"] = new SelectList(_context.Comunes, "Id", "Id", farmacia.IdComune);
            return View(farmacia);
        }

        // GET: Farmacie/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmacia == null)
            {
                return NotFound();
            }

            return View(farmacia);
        }

        // POST: Farmacie/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmacia = await _context.Farmacies.FindAsync(id);
            if (farmacia != null)
            {
                _context.Farmacies.Remove(farmacia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmaciaExists(int id)
        {
            return _context.Farmacies.Any(e => e.Id == id);
        }

        [Authorize]
        [HttpGet]
        public IActionResult SearchByRegione()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SearchByRegione(string regione)
        {
            if (string.IsNullOrEmpty(regione))
            {
                // Se la stringa della regione è vuota o nulla, restituisce una lista vuota o tutte le farmacie a seconda dei requisiti.
                return View("Index", new List<Farmacia>());
            }

            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Where(f => f.IdComuneNavigation.IdProvinciaNavigation.IdRegioneNavigation.Denominazione.Contains(regione))
                .ToListAsync();

            return View("Index", farmacie);
        }

        [Authorize]
        [HttpGet]
        public IActionResult SearchByProvincia()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SearchByProvincia(string provincia)
        {
            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Where(f => f.IdComuneNavigation.IdProvinciaNavigation.Denominazione.Contains(provincia))
                .ToListAsync();

            return View("Index", farmacie);
        }

        [Authorize]
        [HttpGet]
        public IActionResult SearchByComune()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SearchByComune(string comune)
        {
            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Where(f => f.IdComuneNavigation.Denominazione.Contains(comune))
                .ToListAsync();

            return View("Index", farmacie);
        }

        [Authorize]
        [HttpGet]
        public IActionResult SearchByDenominazione()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SearchByDenominazione(string denominazione)
        {
            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Where(f => f.Descrizionefarmacia.Contains(denominazione))
                .ToListAsync();

            return View("Index", farmacie);
        }
    }
}
