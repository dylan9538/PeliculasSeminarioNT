using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromotorDePeliculas.Data;
using PromotorDePeliculas.Models;
using PromotorDePeliculas.Models.ModelsVista;

namespace PromotorDePeliculas.Controllers
{
    public class DirectoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DirectoresController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Directores
        public IActionResult Index(string ordena, string cadenaBuscada, int? ide, int? peliId)
        {
            var modelo = new DirectorPeliculasPremios();
            modelo.Directores = _context.Director.Include(c => c.Peliculas).ThenInclude(s => s.Premios).OrderBy(c => c.Nombre);
           
            ViewBag.Id = string.IsNullOrEmpty(ordena) ? "Id desc" : "";
            ViewBag.Nombre = ordena == "Nombre" ? "Nombre desc" : "Nombre";
            ViewBag.Direccion = ordena == "Direccion" ? "Direccion desc" : "Direccion";
            ViewBag.Telefono = ordena == "Telefono" ? "Telefono desc" : "Telefono";

            if (!string.IsNullOrEmpty(cadenaBuscada))
                 modelo.Directores = modelo.Directores.Where(s => s.Nombre.ToUpper().Contains(cadenaBuscada.ToUpper()));

            switch (ordena)
            {

                case "Id desc":
                    modelo.Directores = modelo.Directores.OrderByDescending(d => d.Id);
                    break;
                case "Direccion":
                    modelo.Directores = modelo.Directores.OrderBy(d => d.Direccion);
                    break;
                case "Direccion desc":
                    modelo.Directores = modelo.Directores.OrderByDescending(d => d.Direccion);
                    break;
                case "Nombre":
                    modelo.Directores = modelo.Directores.OrderBy(d => d.Nombre);
                    break;
                case "Nombre desc":
                    modelo.Directores = modelo.Directores.OrderByDescending(d => d.Nombre);
                    break;
                case "Telefono":
                    modelo.Directores = modelo.Directores.OrderBy(d => d.Telefono);
                    break;
                case "Telefono desc":
                    modelo.Directores = modelo.Directores.OrderByDescending(d => d.Telefono);
                    break;
                default:
                    modelo.Directores = modelo.Directores.OrderBy(d => d.Id);
                    break;
            }

            if (ide != null)
            {
                ViewBag.DirectorId = ide;
                modelo.Peliculas = modelo.Directores.First(c => c.Id == ide).Peliculas;
            }
            if (peliId != null)
            {
                ViewBag.PeliculaId = peliId;
                modelo.Premios = modelo.Peliculas.First(d => d.Id == peliId).Premios;
            }


            return View(modelo);
        }

        // GET: Directores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Director.Include(c => c.Peliculas).SingleOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // GET: Directores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Direccion,Nombre,Telefono")] Director director)
        {
            if (ModelState.IsValid)
            {
                _context.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(director);
        }

        // GET: Directores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Director.SingleOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        // POST: Directores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Direccion,Nombre,Telefono")] Director director)
        {
            if (id != director.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(director);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director.Id))
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
            return View(director);
        }

        // GET: Directores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Director.SingleOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Directores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _context.Director.SingleOrDefaultAsync(m => m.Id == id);
            _context.Director.Remove(director);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DirectorExists(int id)
        {
            return _context.Director.Any(e => e.Id == id);
        }
    }
}
