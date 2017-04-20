using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PromotorDePeliculas.Models;

namespace PromotorDePeliculas.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Premio>().HasKey(r => new { r.PeliculaId, r.CertamenId });
            builder.Entity<Comentario>().HasKey(d => new { d.PeliculaId, d.UsuarioId});
            builder.Entity<Papel>().HasKey(r => new { r.PeliculaId, r.ActorId });
            builder.Entity<Usuario>().HasAlternateKey(u => u.Email);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Certamen> Certamen { get; set; }

        public DbSet<Actor> Actor { get; set; }

        public DbSet<Comentario> Comentario { get; set; }

        public DbSet<Director> Director { get; set; }

        public DbSet<Productor> Productor { get; set; }

        public DbSet<Pelicula> Pelicula { get; set; }

        public DbSet<Premio> Premio { get; set; }

        public DbSet<Papel> Papel { get; set; }



    }
}
