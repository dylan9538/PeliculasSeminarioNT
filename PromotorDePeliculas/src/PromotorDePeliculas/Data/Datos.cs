using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PromotorDePeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotorDePeliculas.Data
{
    public class Datos
    {
        public static void Cargar(IServiceProvider serviceProvider)
        {
            using (var ctx = new ApplicationDbContext(
              serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                ctx.Actor.AddRange(
                 new Actor { Id = 1434, Nombre = "Lora", Direccion = "Car2b", Telefono = "433098", EstadoCivil = EstadoCivil.Soltero, FechaNacimiento = new DateTime(2016, 12, 25) },
                 new Actor { Id = 1435, Nombre = "Duran", Direccion = "Car3b", Telefono = "433054", EstadoCivil = EstadoCivil.Casado, FechaNacimiento = new DateTime(2016, 4, 23) },
                 new Actor { Id = 1436, Nombre = "Camilo", Direccion = "Car7b", Telefono = "435499", EstadoCivil = EstadoCivil.Soltero, FechaNacimiento = new DateTime(2016, 2, 17) }

                 );
                ctx.SaveChanges();


                ctx.Productor.AddRange(
                    new Productor { Id = 1864, Nombre = "Melisa", Direccion = "Direccion1", Telefono = "34345" },
                    new Productor { Id = 1865, Nombre = "Juanes", Direccion = "Direccion2", Telefono = "34348" },
                    new Productor { Id = 1866, Nombre = "Diego", Direccion = "Direccion3", Telefono = "34349" },
                    new Productor { Id = 1867, Nombre = "Nora", Direccion = "Direccion4", Telefono = "34342" }
                    );
                ctx.SaveChanges();

                ctx.Director.AddRange(
                   new Director { Id = 2222, Nombre = "Jean", Direccion = "Direccion5", Telefono = "44478" },
                   new Director { Id = 5555, Nombre = "Piñeros", Direccion = "Direccion6", Telefono = "444577" },
                   new Director { Id = 8888, Nombre = "Lola", Direccion = "Direccion7", Telefono = "543332" },
                   new Director { Id = 9999, Nombre = "Diana", Direccion = "Direccion8", Telefono = "68876" }
                   );
                ctx.SaveChanges();
                ctx.Usuario.AddRange(
                                  new Usuario { Nombre = "Usuario1", Email = "usu1@hotmail.com" },
                                  new Usuario { Nombre = "Usuario2", Email = "usu2@hotmail.com" },
                                  new Usuario { Nombre = "Usuario3", Email = "usu3@hotmail.com" }
                                  );
                ctx.SaveChanges();

                ctx.Certamen.AddRange(
                                  new Certamen { Nombre = "Certamen1", Ciudad = "Cali" },
                                  new Certamen { Nombre = "Certamen2", Ciudad = "Medellin" },
                                  new Certamen { Nombre = "Certamen3", Ciudad = "Cartagena" }
                                  );
                ctx.SaveChanges();

                ctx.Pelicula.AddRange(
                                 new Pelicula { Titulo = "Pelicula1", LugarEstreno = "Colombia", Descripcion = "Mi primera pelicula", Nacionalidad = "Colombiana", FechaEstreno = new DateTime(2016, 2, 4), ProductorId = 1764, DirectorId = 2222 },
                                 new Pelicula { Titulo = "Pelicula2", LugarEstreno = "Estados Unidos", Descripcion = "Mi Segunda pelicula", Nacionalidad = "Estado Unidense", FechaEstreno = new DateTime(2017, 8, 9), ProductorId = 1765, DirectorId = 5555 },
                                 new Pelicula { Titulo = "Pelicula3", LugarEstreno = "Londres", Descripcion = "Mi Tercera pelicula", Nacionalidad = "Europea", FechaEstreno = new DateTime(2018, 12, 6), ProductorId = 1768, DirectorId = 8888 }
                                 );
                ctx.SaveChanges();

                ctx.Comentario.AddRange(
                                new Comentario { Descripcion = "Buena pelicula", Lugar = "Cali", FechaComentario = DateTime.Now, UsuarioId = 1, PeliculaId = 6 },
                                new Comentario { Descripcion = "Mala pelicula", Lugar = "Texas", FechaComentario = DateTime.Now, UsuarioId = 2, PeliculaId = 7 },
                                new Comentario { Descripcion = "Regular pelicula", Lugar = "Londres", FechaComentario = DateTime.Now, UsuarioId = 3, PeliculaId = 8 }
                                );

                ctx.Premio.AddRange(
                 new Premio { TipoPremio = TipoPremio.Oscar, fechaEntrega = new DateTime(2016, 3, 1), CertamenId = 1, PeliculaId = 6 },
                 new Premio { TipoPremio = TipoPremio.Emmy, fechaEntrega = new DateTime(2017, 10, 3), CertamenId = 2, PeliculaId = 7 },
                 new Premio { TipoPremio = TipoPremio.GloboDeoro, fechaEntrega = new DateTime(2018, 12, 9), CertamenId = 3, PeliculaId = 8 }
                 );
                ctx.SaveChanges();

                ctx.Papel.AddRange(
                 new Papel { TipoPapel = "Primario", Descripcion = "Descripcion1", ActorId = 1234, PeliculaId = 6 },
                 new Papel { TipoPapel = "Segundario", Descripcion = "Descripcion2", ActorId = 1254, PeliculaId = 7 },
                  new Papel { TipoPapel = "Protagonista", Descripcion = "Descripcion3", ActorId = 1334, PeliculaId = 8 }
                 );
                ctx.SaveChanges();


            }

        }
       }
}
