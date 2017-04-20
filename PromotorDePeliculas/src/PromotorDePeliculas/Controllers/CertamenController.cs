using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromotorDePeliculas.Data;
using PromotorDePeliculas.Models;
using Microsoft.AspNetCore.Authorization;

namespace PromotorDePeliculas.Controllers
{
    public class CertamenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CertamenController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Certamen
        [Authorize]
        public async Task<IActionResult> Index(string ordena, string cadenaBuscada)
        { 
            ViewBag.Nombre = ordena == "Nombre" ? "Nombre desc" : "Nombre";
            ViewBag.Ciudad = string.IsNullOrEmpty(ordena) ? "Ciudad desc" : "";


            var certamenes = from d in _context.Certamen
                          select d;
            if (!string.IsNullOrEmpty(cadenaBuscada))
                certamenes = certamenes.Where(s => s.Nombre.ToUpper().Contains(cadenaBuscada.ToUpper()));

            switch (ordena)
            {

                case "Ciudad desc":
                    certamenes = certamenes.OrderByDescending(d => d.Id);
                    break;
                case "Nombre":
                    certamenes = certamenes.OrderBy(d => d.Nombre);
                    break;
                case "Nombre desc":
                    certamenes = certamenes.OrderByDescending(d => d.Nombre);
                    break;
             
               
                default:
                    certamenes = certamenes.OrderBy(d => d.Ciudad);
                    break;
            }



            return View(await certamenes.ToListAsync());
        }

        // GET: Certamen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certamen = await _context.Certamen.Include(c=> c.Premios).SingleOrDefaultAsync(m => m.Id == id);
            if (certamen == null)
            {
                return NotFound();
            }

            return View(certamen);
        }

        // GET: Certamen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certamen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ciudad,Nombre")] Certamen certamen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certamen);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(certamen);
        }

        // GET: Certamen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certamen = await _context.Certamen.SingleOrDefaultAsync(m => m.Id == id);
            if (certamen == null)
            {
                return NotFound();
            }
            return View(certamen);
        }

        // POST: Certamen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ciudad,Nombre")] Certamen certamen)
        {
            if (id != certamen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certamen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertamenExists(certamen.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(certamen);
        }

        // GET: Certamen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certamen = await _context.Certamen.SingleOrDefaultAsync(m => m.Id == id);
            if (certamen == null)
            {
                return NotFound();
            }

            return View(certamen);
        }

        // POST: Certamen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certamen = await _context.Certamen.SingleOrDefaultAsync(m => m.Id == id);
            _context.Certamen.Remove(certamen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CertamenExists(int id)
        {
            return _context.Certamen.Any(e => e.Id == id);
        }
    }
}
