using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromotorDePeliculas.Data;
using PromotorDePeliculas.Models.ModelsVista;
using Microsoft.AspNetCore.Mvc.Rendering;
using PromotorDePeliculas.Models;
using Microsoft.AspNetCore.Authorization;

namespace PromotorDePeliculas.Controllers
{

    [Authorize]
    public class ConsultasController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ConsultasController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Peliculas y su total de premios ordenadas descendientemente
        public ActionResult PeliculasNPremios()
        {
            var peliPremios = _context.Pelicula.Select(u => new PeliculaNroPremios
            {
                Titulo = u.Titulo,
                TotalPremios = _context.Premio.Where(p => p.Pelicula == u).Count()
            });
            return View(peliPremios.OrderByDescending(l => l.TotalPremios));
        }

        //reparto /actores y papeles
        public ActionResult ActoresPapelesPelicula(int? PeliculaId)
        {
            ViewBag.PeliculaId = new SelectList(_context.Pelicula, "Id", "Titulo");
            if (PeliculaId == null)
            {
                return View("ConsultaReparto");
            }
            var papeles = _context.Papel.Where(e => e.PeliculaId == PeliculaId).Select(a => new ActorPapelPelicula
            {
                Actor = a.Actor.Nombre,
                Papel = a.Descripcion
            });
            return View(papeles);
        }

        //Peliculas mas comentadas.
        public ActionResult PeliculasMasComentadas()
        {
            var peliculas = _context.Pelicula.Select(u => new PeliculasMasComen

            {
                Titulo = u.Titulo,
                comentarios = _context.Comentario.Where(p => p.Pelicula == u).Count()
            }
            ).Take(10);
            return View(peliculas.OrderByDescending(l => l.comentarios));
        }

        //peliculas estrenadas
        public ActionResult PeliculasEstrenadas()
        {
            var peliculasEs = _context.Pelicula.Where(a => a.FechaEstreno <= DateTime.Now).Select(p => new PeliculasEstren
            {
                Titulo = p.Titulo,
                FechaEstreno = p.FechaEstreno
            });

            return View(peliculasEs);

        }


        //Premios de los directores gracias a sus peliculas

        public ActionResult DirectoresPremios()
        {

            var retorno = from p in _context.Premio
                          group p by p.Pelicula.Director into g
                          select new DirectoresPremios
                          {
                              Nombre = g.Key.Nombre,
                              Premios = g
                          };
            return View(retorno);
        }

        //reparto /actores y papeles
        public ActionResult CertamenPeliculasP(int? CertamenId)
        {
            ViewBag.CertamenId = new SelectList(_context.Certamen, "Id", "Nombre");
            if (CertamenId == null)
            {
                return View("ConsultaPremiados");
            }
            var consulta = _context.Premio.Where(e => e.CertamenId == CertamenId).Select(a => new certamenPeliculas
            {
                Nombre = a.Certamen.Nombre,
                Titulo = a.Pelicula.Titulo
            });
            return View(consulta);
        }

    }
}