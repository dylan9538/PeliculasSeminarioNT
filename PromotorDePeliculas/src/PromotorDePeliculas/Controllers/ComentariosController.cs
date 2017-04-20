using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromotorDePeliculas.Data;
using PromotorDePeliculas.Models;

namespace PromotorDePeliculas.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Comentarios
        public async Task<IActionResult> Index(string ordena, string cadenaBuscada)
        {
            ViewBag.FechaComentario = string.IsNullOrEmpty(ordena) ? "FechaComentario desc" : "";
            ViewBag.Pelicula = ordena == "Pelicula" ? "Pelicula desc" : "Pelicula";
            ViewBag.Usuario = ordena == "Usuario" ? "Usuario desc" : "Usuario";
            ViewBag.Descripcion = ordena == "Descripcion" ? "Descripcion desc" : "Descripcion";
            ViewBag.Lugar = ordena == "Lugar" ? "Lugar desc" : "Lugar";
            

            var comentarios = from d in _context.Comentario
                          select d;
            if (!string.IsNullOrEmpty(cadenaBuscada))
                comentarios = comentarios.Where(s => s.Pelicula.Titulo.ToUpper().Contains(cadenaBuscada.ToUpper()));
            switch (ordena)
            {

                case "FechaComentario desc":
                    comentarios = comentarios.OrderByDescending(d => d.FechaComentario);
                    break;
                case "Pelicula":
                    comentarios = comentarios.OrderBy(d => d.Pelicula.Titulo);
                    break;
                case "Pelicula desc":
                    comentarios = comentarios.OrderByDescending(d => d.Pelicula.Titulo);
                    break;
                case "Usuario":
                    comentarios = comentarios.OrderBy(d => d.Usuario.Nombre);
                    break;
                case "Usuario desc":
                    comentarios = comentarios.OrderByDescending(d => d.Usuario.Nombre);
                    break;
                case "Descripcion":
                    comentarios = comentarios.OrderBy(d => d.Descripcion);
                    break;
                case "Descripcion desc":
                    comentarios = comentarios.OrderByDescending(d => d.Descripcion);
                    break;
                case "Lugar":
                    comentarios = comentarios.OrderBy(d => d.Lugar);
                    break;
                case "Lugar desc":
                    comentarios = comentarios.OrderByDescending(d => d.Lugar);
                    break;
               
                default:
                    comentarios = comentarios.OrderBy(d => d.FechaComentario);
                    break;
            }



            var applicationDbContext = comentarios.Include(c => c.Pelicula).Include(c => c.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? u, int? d)
        {
            if (u == null || d == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario.SingleOrDefaultAsync(m => m.PeliculaId == d && m.UsuarioId == u);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // GET: Comentarios/Create
        public IActionResult Create()
        {
            ViewData["PeliculaId"] = new SelectList(_context.Set<Pelicula>(), "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Email");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeliculaId,UsuarioId,Descripcion,FechaComentario,Lugar")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
            
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PeliculaId"] = new SelectList(_context.Set<Pelicula>(), "Id", "Id", comentario.PeliculaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Email", comentario.UsuarioId);
            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        public async Task<IActionResult> Edit(int? u, int? d)
        {
            if (u == null || d == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario.SingleOrDefaultAsync(m => m.PeliculaId == d && m.UsuarioId == u);
            if (comentario == null)
            {
                return NotFound();
            }
            ViewData["PeliculaId"] = new SelectList(_context.Set<Pelicula>(), "Id", "Id", comentario.PeliculaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Email", comentario.UsuarioId);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeliculaId,UsuarioId,Descripcion,FechaComentario,Lugar")] Comentario comentario)
        {
            if (id != comentario.PeliculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioExists(comentario.UsuarioId,comentario.PeliculaId))
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
            ViewData["PeliculaId"] = new SelectList(_context.Set<Pelicula>(), "Id", "Id", comentario.PeliculaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Email", comentario.UsuarioId);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public async Task<IActionResult> Delete(int? u, int? d)
        {
            if (u == null || d == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario.SingleOrDefaultAsync(m => m.PeliculaId == d && m.UsuarioId == u);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? u, int? d)
        {
            var comentario = await _context.Comentario.SingleOrDefaultAsync(m => m.PeliculaId == d && m.UsuarioId == u);
            _context.Comentario.Remove(comentario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ComentarioExists(int? u, int? d)
        {
            return _context.Comentario.Any(e => e.PeliculaId == d && e.UsuarioId == u);
        }
    }
}
