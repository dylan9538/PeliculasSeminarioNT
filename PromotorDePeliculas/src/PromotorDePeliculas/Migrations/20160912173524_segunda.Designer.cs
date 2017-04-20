using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PromotorDePeliculas.Data;

namespace PromotorDePeliculas.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160912173524_segunda")]
    partial class segunda
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .HasAnnotation("MaxLength", 6);

                    b.Property<string>("Direccion");

                    b.Property<int>("EstadoCivil");

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Telefono");

                    b.HasKey("Id");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Certamen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ciudad");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Certamen");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Comentario", b =>
                {
                    b.Property<int>("PeliculaId");

                    b.Property<int>("UsuarioId");

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaComentario");

                    b.Property<string>("Lugar");

                    b.HasKey("PeliculaId", "UsuarioId");

                    b.HasIndex("PeliculaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comentario");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Director", b =>
                {
                    b.Property<int>("Id")
                        .HasAnnotation("MaxLength", 6);

                    b.Property<string>("Direccion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Telefono");

                    b.HasKey("Id");

                    b.ToTable("Director");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Papel", b =>
                {
                    b.Property<int>("PeliculaId");

                    b.Property<int>("ActorId");

                    b.Property<string>("Descripcion");

                    b.Property<string>("TipoPapel");

                    b.HasKey("PeliculaId", "ActorId");

                    b.HasIndex("ActorId");

                    b.HasIndex("PeliculaId");

                    b.ToTable("Papel");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<int>("DirectorId");

                    b.Property<DateTime>("FechaEstreno");

                    b.Property<string>("LugarEstreno");

                    b.Property<string>("Nacionalidad");

                    b.Property<int>("ProductorId");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.HasIndex("ProductorId");

                    b.ToTable("Pelicula");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Premio", b =>
                {
                    b.Property<int>("PeliculaId");

                    b.Property<int>("CertamenId");

                    b.Property<int>("TipoPremio");

                    b.Property<DateTime>("fechaEntrega");

                    b.HasKey("PeliculaId", "CertamenId");

                    b.HasIndex("CertamenId");

                    b.HasIndex("PeliculaId");

                    b.ToTable("Premio");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Productor", b =>
                {
                    b.Property<int>("Id")
                        .HasAnnotation("MaxLength", 6);

                    b.Property<string>("Direccion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Telefono");

                    b.HasKey("Id");

                    b.ToTable("Productor");
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PromotorDePeliculas.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PromotorDePeliculas.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PromotorDePeliculas.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Comentario", b =>
                {
                    b.HasOne("PromotorDePeliculas.Models.Pelicula", "Pelicula")
                        .WithMany("Comentarios")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PromotorDePeliculas.Models.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Papel", b =>
                {
                    b.HasOne("PromotorDePeliculas.Models.Actor", "Actor")
                        .WithMany("Papeles")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PromotorDePeliculas.Models.Pelicula", "Pelicula")
                        .WithMany("Papeles")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Pelicula", b =>
                {
                    b.HasOne("PromotorDePeliculas.Models.Director", "Director")
                        .WithMany("Peliculas")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PromotorDePeliculas.Models.Productor", "Productor")
                        .WithMany("Peliculas")
                        .HasForeignKey("ProductorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PromotorDePeliculas.Models.Premio", b =>
                {
                    b.HasOne("PromotorDePeliculas.Models.Certamen", "Certamen")
                        .WithMany("Premios")
                        .HasForeignKey("CertamenId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PromotorDePeliculas.Models.Pelicula", "Pelicula")
                        .WithMany("Premios")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
