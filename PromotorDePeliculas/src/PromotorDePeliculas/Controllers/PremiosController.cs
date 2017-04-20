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
    public class PremiosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PremiosController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Premios
        [Authorize]
        public async Task<IActionResult> Index(string ordena,string cadenaBuscada, TipoPremio ? tipoPremio)
        {

            ViewBag.fechaEntrega = string.IsNullOrEmpty(ordena) ? "fechaEntrega desc" : "";
            ViewBag.Pelicula = ordena == "Pelicula" ? "Pelicula desc" : "Pelicula";
            ViewBag.Certamen = ordena == "Certamen" ? "Certamen desc" : "Certamen";
            ViewBag.TipoPremio = ordena == "TipoPremio" ? "TipoPremio desc" : "TipoPremio";
  
            var premios = from d in _context.Premio
                              select d;
            if (tipoPremio != null)
                premios = premios.Where(u => u.TipoPremio == tipoPremio);

            if (!string.IsNullOrEmpty(cadenaBuscada))
                premios = premios.Where(s => s.Pelicula.Titulo.ToUpper().Contains(cadenaBuscada.ToUpper()));
            switch (ordena)
            {

                case "FechaEntrega desc":
                    premios = premios.OrderByDescending(d => d.fechaEntrega);
                    break;
                case "Pelicula":
                    premios = premios.OrderBy(d => d.Pelicula.Titulo);
                    break;
                case "Pelicula desc":
                    premios = premios.OrderByDescending(d => d.Pelicula.Titulo);
                    break;
                case "Certamen":
                    premios = premios.OrderBy(d => d.Certamen.Nombre);
                    break;
                case "Certamen desc":
                    premios = premios.OrderByDescending(d => d.Certamen.Nombre);
                    break;
                case "TipoPremio":
                    premios = premios.OrderBy(d => d.TipoPremio);
                    break;
                case "TipoPremio desc":
                    premios = premios.OrderByDescending(d => d.TipoPremio);
                    break;
               

                default:
                    premios = premios.OrderBy(d => d.fechaEntrega);
                    break;
            }

            var applicationDbContext = premios.Include(p => p.Certamen).Include(p => p.Pelicula);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Premios/Details/5
        public async Task<IActionResult> Details(int? u, int? d)
        {
            if (u == null || d == null)
            {
                return NotFound();
            }

            var premio = await _context.Premio.Include(m => m.Certamen).Include(m=>m.Pelicula)
                .SingleOrDefaultAsync(m => m.PeliculaId == d && m.CertamenId == u);
            if (premio == null)
            {
                return NotFound();
            }

            return View(premio);
        }

        // GET: Premios/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            ViewData["CertamenId"] = new SelectList(_context.Certamen, "Id", "Id");
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Titulo");
            return View();
        }

        // POST: Premios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeliculaId,CertamenId,TipoPremio,fechaEntrega")] Premio premio)
        {
            if (ModelState.IsValid)
            {

                    int numPremios = _context.Premio.Where(u => u.PeliculaId == premio.PeliculaId).Count();

                    Console.WriteLine("" + numPremios);
                    if (numPremios >= 3)
                    {
                        string Alerta = "La-pelicula-tiene-mas-de-3-premios";
                        return RedirectToAction("", new { Alerta });
                    }
                    else
                    {

                    var pelicula = _context.Pelicula.SingleOrDefault(i => i.Id == premio.PeliculaId);
                    var director = _context.Director.SingleOrDefault(n => n.Id == pelicula.DirectorId);

                        int totalPremios = 0;
                        string Alert = "El director tiene mas de tres premios por sus peliculas";

                        foreach (var p in director.Peliculas)
                        {
                            totalPremios += _context.Premio.Where(c => c.PeliculaId == p.Id).Count();
                        }

                        if (totalPremios >= 3)
                        {
                            return RedirectToAction("", new { Alert });
                        }
                        else
                        {
                            _context.Add(premio);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                    }
                }
            
            
            ViewData["CertamenId"] = new SelectList(_context.Certamen, "Id", "Id", premio.CertamenId);
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Titulo", premio.PeliculaId);
            return View(premio);
            
        }

        // GET: Premios/Edit/5
        public async Task<IActionResult> Edit(int? u, int? d)
        {
            if (u == null || d == null)
            {
                return NotFound();
            }

            var premio = await _context.Premio.SingleOrDefaultAsync(m => m.PeliculaId == d && m.CertamenId == u);
            if (premio == null)
            {
                return NotFound();
            }
            ViewData["CertamenId"] = new SelectList(_context.Certamen, "Id", "Id", premio.CertamenId);
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Titulo", premio.PeliculaId);
            return View(premio);
        }

        // POST: Premios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeliculaId,CertamenId,TipoPremio,fechaEntrega")] Premio premio)
        {
            if (id != premio.PeliculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(premio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PremioExists(premio.CertamenId,premio.PeliculaId))
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
            ViewData["CertamenId"] = new SelectList(_context.Certamen, "Id", "Id", premio.CertamenId);
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Titulo", premio.PeliculaId);
            return View(premio);
        }

        // GET: Premios/Delete/5
        public async Task<IActionResult> Delete(int? u, int? d)
        {
            if (u == null || d == null)
            {
                return NotFound();
            }

            var premio = await _context.Premio.SingleOrDefaultAsync(m => m.PeliculaId == d && m.CertamenId == u);
            if (premio == null)
            {
                return NotFound();
            }

            return View(premio);
        }

        // POST: Premios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? u, int? d)
        {
            var premio = await _context.Premio.SingleOrDefaultAsync(m => m.PeliculaId == d && m.CertamenId ==u);
            _context.Premio.Remove(premio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PremioExists(int u, int d)
        {
            return _context.Premio.Any(e => e.PeliculaId == d && e.CertamenId == u);
        }
    }
}
