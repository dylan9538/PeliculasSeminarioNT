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
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize]
        // GET: Usuarios
        public async Task<IActionResult> Index(string ordena, string cadenaBuscada)
        {
            ViewBag.Id = string.IsNullOrEmpty(ordena) ? "Id desc" : "";
            ViewBag.Email = ordena == "Email" ? "Email desc" : "Email";
            ViewBag.Nombre = ordena == "Nombre" ? "Nombre desc" : "Nombre";
            var usuarios = from u in _context.Usuario
                           select u;
            if (!string.IsNullOrEmpty(cadenaBuscada))
                usuarios = usuarios.Where(s => s.Nombre.ToUpper().Contains(cadenaBuscada.ToUpper()));

            switch (ordena)
            {

                case "Id desc":
                    usuarios = usuarios.OrderByDescending(u => u.Id);
                    break;
                case "Email":
                    usuarios = usuarios.OrderBy(u => u.Email);
                    break;
                case "Email desc":
                    usuarios = usuarios.OrderByDescending(u => u.Email);
                    break;
                case "Nombre":
                    usuarios = usuarios.OrderBy(u => u.Nombre);
                    break;
                case "Nombre desc":
                    usuarios = usuarios.OrderByDescending(u => u.Nombre);
                    break;
                default:
                    usuarios = usuarios.OrderBy(u => u.Id);
                    break;
            }
            
            return View(await usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.Include(c=>c.Comentarios).SingleOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Nombre")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Nombre")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.Id == id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
