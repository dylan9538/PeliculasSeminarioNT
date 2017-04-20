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
    public class PeliculasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeliculasController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize]
        // GET: Peliculas
        public async Task<IActionResult> Index(string ordena, string cadena)
        {

            ViewBag.FechaEstreno = string.IsNullOrEmpty(ordena) ? "FechaEstreno desc" : "";
            ViewBag.Titulo = ordena == "Titulo" ? "Titulo desc" : "Titulo";
            ViewBag.Id = ordena == "Id" ? "Id desc" : "Id";
            ViewBag.Descripcion = ordena == "Descripcion" ? "Descripcion desc" : "Descripcion";
            ViewBag.LugarEstreno = ordena == "LugarEstreno" ? "LugarEstreno desc" : "LugarEstreno";
            ViewBag.Nacionalidad = ordena == "Nacionalidad" ? "Nacionalidad desc" : "Nacionalidad";
            ViewBag.Productor = ordena == "Productor" ? "Productor desc" : "Productor";
            ViewBag.Director = ordena == "Director" ? "Director desc" : "Director";
            var peliculas = from d in _context.Pelicula
                          select d;
            if (!string.IsNullOrEmpty(cadena))
                peliculas   = peliculas.Where(s => s.Titulo.ToUpper().Contains(cadena.ToUpper()));

            switch (ordena)
            {

                case "FechaEstreno desc":
                    peliculas = peliculas.OrderByDescending(d => d.FechaEstreno);
                    break;
                case "Descripcion":
                    peliculas = peliculas.OrderBy(d => d.Descripcion);
                    break;
                case "Descripcion desc":
                    peliculas = peliculas.OrderByDescending(d => d.Descripcion);
                    break;
                case "Id":
                    peliculas = peliculas.OrderBy(d => d.Id);
                    break;
                case "Id desc":
                    peliculas = peliculas.OrderByDescending(d => d.Id);
                    break;

                case "Titulo":
                    peliculas = peliculas.OrderBy(d => d.Titulo);
                    break;
                case "Titulo desc":
                    peliculas = peliculas.OrderByDescending(d => d.Titulo);
                    break;
                case "Nacionalidad":
                    peliculas = peliculas.OrderBy(d => d.Nacionalidad);
                    break;
                case "Nacionalidad desc":
                    peliculas = peliculas.OrderByDescending(d => d.Nacionalidad);
                    break;
                case "LugarEstreno":
                    peliculas = peliculas.OrderBy(d => d.LugarEstreno);
                    break;
                case "LugarEstreno desc":
                    peliculas = peliculas.OrderByDescending(d => d.LugarEstreno);
                    break;
                case "Director":
                    peliculas = peliculas.OrderBy(d => d.Director.Nombre);
                    break;
                case "Director desc":
                    peliculas = peliculas.OrderByDescending(d => d.Director.Nombre);
                    break;
                case "Productor":
                    peliculas = peliculas.OrderBy(d => d.Productor.Nombre);
                    break;
                case "Productor desc":
                    peliculas = peliculas.OrderByDescending(d => d.Productor.Nombre);
                    break;

                default:
                    peliculas = peliculas.OrderBy(d => d.FechaEstreno);
                    break;
            }

            var applicationDbContext = peliculas.Include(p => p.Director).Include(p => p.Productor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula.Include(c=>c.Comentarios).Include(d => d.Papeles).Include(e => e.Premios).SingleOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "Nombre");
            ViewData["ProductorId"] = new SelectList(_context.Productor, "Id", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,DirectorId,FechaEstreno,LugarEstreno,Nacionalidad,ProductorId,Titulo")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "Id", pelicula.DirectorId);
            ViewData["ProductorId"] = new SelectList(_context.Productor, "Id", "Id", pelicula.ProductorId);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula.SingleOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "Id", pelicula.DirectorId);
            ViewData["ProductorId"] = new SelectList(_context.Productor, "Id", "Id", pelicula.ProductorId);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,DirectorId,FechaEstreno,LugarEstreno,Nacionalidad,ProductorId,Titulo")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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
            ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "Id", pelicula.DirectorId);
            ViewData["ProductorId"] = new SelectList(_context.Productor, "Id", "Id", pelicula.ProductorId);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula.SingleOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pelicula = await _context.Pelicula.SingleOrDefaultAsync(m => m.Id == id);
            _context.Pelicula.Remove(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PeliculaExists(int id)
        {
            return _context.Pelicula.Any(e => e.Id == id);
        }
    }
}
