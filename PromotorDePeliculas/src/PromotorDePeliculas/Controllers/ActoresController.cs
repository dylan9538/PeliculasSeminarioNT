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
    public class ActoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActoresController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Actores
        [Authorize]
        public async Task<IActionResult> Index(string ordena, string cadenaBuscada, EstadoCivil? estadoCivil)
        {
            ViewBag.Id = string.IsNullOrEmpty(ordena) ? "Id desc" : "";
            ViewBag.Nombre = ordena == "Nombre" ? "Nombre desc" : "Nombre";
            ViewBag.Direccion = ordena == "Direccion" ? "Direccion desc" : "Direccion";
            ViewBag.EstadoCivil = ordena == "EstadoCivil" ? "EstadoCivil desc" : "EstadoCivil";
            ViewBag.FechaNacimiento = ordena == "FechaNacimiento" ? "FechaNacimiento desc" : "FechaNacimiento";
            ViewBag.Telefono = ordena == "Telefono" ? "Telefono desc" : "Telefono";

            var actores = from d in _context.Actor
                             select d;
            if (!string.IsNullOrEmpty(cadenaBuscada))
                actores = actores.Where(s => s.Nombre.ToUpper().Contains(cadenaBuscada.ToUpper()));
            if (estadoCivil != null)
                actores = actores.Where(u => u.EstadoCivil == estadoCivil);

            switch (ordena)
            {

                case "Id desc":
                    actores = actores.OrderByDescending(d => d.Id);
                    break;
                case "Direccion":
                    actores = actores.OrderBy(d => d.Direccion);
                    break;
                case "Direccion desc":
                    actores = actores.OrderByDescending(d => d.Direccion);
                    break;
                case "EstadoCivil":
                    actores = actores.OrderBy(d => d.EstadoCivil);
                    break;
                case "EstadoCivil desc":
                    actores = actores.OrderByDescending(d => d.EstadoCivil);
                    break;
                case "FechaNacimiento":
                    actores = actores.OrderBy(d => d.FechaNacimiento);
                    break;
                case "FechaNacimiento desc":
                    actores = actores.OrderByDescending(d => d.FechaNacimiento);
                    break;
                case "Nombre":
                    actores = actores.OrderBy(d => d.Nombre);
                    break;
                case "Nombre desc":
                    actores = actores.OrderByDescending(d => d.Nombre);
                    break;
                case "Telefono":
                    actores = actores.OrderBy(d => d.Telefono);
                    break;
                case "Telefono desc":
                    actores = actores.OrderByDescending(d => d.Telefono);
                    break;
                default:
                    actores = actores.OrderBy(d => d.Id);
                    break;
            }


            return View(await actores.ToListAsync());
        }

        // GET: Actores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.Include(c => c.Papeles).SingleOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Direccion,EstadoCivil,FechaNacimiento,Nombre,Telefono")] Actor actor)
        {
            if (ModelState.IsValid)
            {



                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        // GET: Actores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.SingleOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Direccion,EstadoCivil,FechaNacimiento,Nombre,Telefono")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
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
            return View(actor);
        }

        // GET: Actores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.SingleOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actor.SingleOrDefaultAsync(m => m.Id == id);
            _context.Actor.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ActorExists(int id)
        {
            return _context.Actor.Any(e => e.Id == id);
        }
    }
}
