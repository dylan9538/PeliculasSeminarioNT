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
    public class PapelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PapelsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Papels
        [Authorize]
        public async Task<IActionResult> Index(string ordena, string cadenaBuscada)
        {
            ViewBag.TipoPapel = string.IsNullOrEmpty(ordena) ? "TipoPapel desc" : "";
            ViewBag.Pelicula = ordena == "Pelicula" ? "Pelicula desc" : "Pelicula";
            ViewBag.Actor = ordena == "ActorId" ? "ActorId desc" : "ActorId";
            ViewBag.Descripcion = ordena == "Descripcion" ? "Descripcion desc" : "Descripcion";

            var papeles = from d in _context.Papel
                              select d;

            if (!string.IsNullOrEmpty(cadenaBuscada))
                papeles = papeles.Where(s => s.Actor.Nombre.ToUpper().Contains(cadenaBuscada.ToUpper()));
            switch (ordena)
            {

                case "TipoPapel desc":
                    papeles = papeles.OrderByDescending(d => d.TipoPapel);
                    break;
                case "Pelicula":
                    papeles = papeles.OrderBy(d => d.Pelicula.Titulo);
                    break;
                case "Pelicula desc":
                    papeles = papeles.OrderByDescending(d => d.Pelicula.Titulo);
                    break;
                case "Actor":
                    papeles = papeles.OrderBy(d => d.Actor.Nombre);
                    break;
                case "Actor desc":
                    papeles = papeles.OrderByDescending(d => d.Actor.Nombre);
                    break;
                case "Descripcion":
                    papeles = papeles.OrderBy(d => d.Descripcion);
                    break;
                case "Descripcion desc":
                    papeles = papeles.OrderByDescending(d => d.Descripcion);
                    break;
                

                default:
                    papeles = papeles.OrderBy(d => d.TipoPapel);
                    break;
            }

                    var applicationDbContext = papeles.Include(p => p.Actor).Include(p => p.Pelicula);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Papels/Details/5
        public async Task<IActionResult> Details(int? u, int? d)
        {
            if (u == null || d == null)
            {
                return NotFound();
            }

            var papel = await _context.Papel.SingleOrDefaultAsync(m => m.PeliculaId == d && m.ActorId == u);
            if (papel == null)
            {
                return NotFound();
            }

            return View(papel);
        }

        // GET: Papels/Create
        public IActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "Id");
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id");
            return View();
        }

        // POST: Papels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeliculaId,ActorId,Descripcion,TipoPapel")] Papel papel)
        {
            if (ModelState.IsValid)
            {
                    var pelicula = _context.Pelicula.SingleOrDefault(i => i.Id == papel.PeliculaId);
                    var director = _context.Director.SingleOrDefault(n => n.Id == pelicula.DirectorId);

                          _context.Add(papel);
                          await _context.SaveChangesAsync();
                          return RedirectToAction("Index");
                                            
            }
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "Id", papel.ActorId);
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id", papel.PeliculaId);
            return View(papel);
        }

        // GET: Papels/Edit/5
        public async Task<IActionResult> Edit(int? u, int? d)
        {
            if (u == null || d == null)
            {
                return NotFound();
            }

            var papel = await _context.Papel.SingleOrDefaultAsync(m => m.PeliculaId == d && m.ActorId == u);
            if (papel == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "Id", papel.ActorId);
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id", papel.PeliculaId);
            return View(papel);
        }

        // POST: Papels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeliculaId,ActorId,Descripcion,TipoPapel")] Papel papel)
        {
            if (id != papel.PeliculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(papel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PapelExists(papel.ActorId,papel.PeliculaId))
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
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "Id", papel.ActorId);
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id", papel.PeliculaId);
            return View(papel);
        }

        // GET: Papels/Delete/5
        public async Task<IActionResult> Delete(int? u, int? d)
        {
            if (u == null || d == null)
            {
                return NotFound();
            }

            var papel = await _context.Papel.SingleOrDefaultAsync(m => m.PeliculaId == d && m.ActorId == u);
            if (papel == null)
            {
                return NotFound();
            }

            return View(papel);
        }

        // POST: Papels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? u, int? d)
        {
            var papel = await _context.Papel.SingleOrDefaultAsync(m => m.PeliculaId == d && m.ActorId == u);
            _context.Papel.Remove(papel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PapelExists(int? u, int? d)
        {
            return _context.Papel.Any(e => e.PeliculaId == d && e.ActorId == u);
        }
    }
}
