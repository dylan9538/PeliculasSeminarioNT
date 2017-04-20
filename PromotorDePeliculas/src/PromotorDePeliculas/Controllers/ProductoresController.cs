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
    public class ProductoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoresController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Productores
        public IActionResult Index(string ordena, string cadenaBuscada, int? ide, int? peliId)
        {

            var modelo = new ProductorPeliculasPapeles();
            modelo.Productores = _context.Productor.Include(c => c.Peliculas).ThenInclude(s => s.Papeles).OrderBy(c => c.Nombre);

            ViewBag.Id = string.IsNullOrEmpty(ordena) ? "Id desc" : "";
            ViewBag.Nombre = ordena == "Nombre" ? "Nombre desc" : "Nombre";
            ViewBag.Direccion = ordena == "Direccion" ? "Direccion desc" : "Direccion";
            ViewBag.Telefono = ordena == "Telefono" ? "Telefono desc" : "Telefono";

           
            if (!string.IsNullOrEmpty(cadenaBuscada))
               modelo.Productores = modelo.Productores.Where(s => s.Nombre.ToUpper().Contains(cadenaBuscada.ToUpper()));

            switch (ordena)
            {

                case "Id desc":
                    modelo.Productores = modelo.Productores.OrderByDescending(d => d.Id);
                    break;
                case "Direccion":
                    modelo.Productores = modelo.Productores.OrderBy(d => d.Direccion);
                    break;
                case "Direccion desc":
                    modelo.Productores = modelo.Productores.OrderByDescending(d => d.Direccion);
                    break;
                case "Nombre":
                    modelo.Productores = modelo.Productores.OrderBy(d => d.Nombre);
                    break;
                case "Nombre desc":
                    modelo.Productores = modelo.Productores.OrderByDescending(d => d.Nombre);
                    break;
                case "Telefono":
                    modelo.Productores = modelo.Productores.OrderBy(d => d.Telefono);
                    break;
                case "Telefono desc":
                    modelo.Productores = modelo.Productores.OrderByDescending(d => d.Telefono);
                    break;
                default:
                    modelo.Productores = modelo.Productores.OrderBy(d => d.Id);
                    break;
            }

            if (ide != null)
            {
                ViewBag.DirectorId = ide;
                modelo.Peliculas = modelo.Productores.First(c => c.Id == ide).Peliculas;
            }
            if (peliId != null)
            {
                ViewBag.PeliculaId = peliId;
                modelo.Papeles = modelo.Peliculas.First(d => d.Id == peliId).Papeles;
            }

            return View(modelo);
        }

        // GET: Productores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productor = await _context.Productor.Include(c => c.Peliculas).SingleOrDefaultAsync(m => m.Id == id);
            if (productor == null)
            {
                return NotFound();
            }

            return View(productor);
        }

        // GET: Productores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Direccion,Nombre,Telefono")] Productor productor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productor);
        }

        // GET: Productores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productor = await _context.Productor.SingleOrDefaultAsync(m => m.Id == id);
            if (productor == null)
            {
                return NotFound();
            }
            return View(productor);
        }

        // POST: Productores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Direccion,Nombre,Telefono")] Productor productor)
        {
            if (id != productor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductorExists(productor.Id))
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
            return View(productor);
        }

        // GET: Productores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productor = await _context.Productor.SingleOrDefaultAsync(m => m.Id == id);
            if (productor == null)
            {
                return NotFound();
            }

            return View(productor);
        }

        // POST: Productores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productor = await _context.Productor.SingleOrDefaultAsync(m => m.Id == id);
            _context.Productor.Remove(productor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductorExists(int id)
        {
            return _context.Productor.Any(e => e.Id == id);
        }
    }
}
