using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromotorDePeliculas.Models
{

    public partial class Pelicula
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pelicula()
        {
            this.Comentarios = new HashSet<Comentario>();
            this.Premios = new HashSet<Premio>();
            this.Papeles = new HashSet<Papel>();
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Titulo Pelicula")]
        [Required(ErrorMessage = "Debe digitar el título de la película"), MaxLength(30)]
        [MaxPalabras(5, ErrorMessage = "Son muchas palabras para el título")]
        public string Titulo { get; set; }

        [Display(Name = "Lugar de estreno")]
        public string LugarEstreno { get; set; }
        public string Descripcion { get; set; }

        [SoloLetras(ErrorMessage = "La nacionalidad debe ser de solo letras")]
        [MaxPalabras(1, ErrorMessage = "Son muchas palabras para la nacionalidad")]
        public string Nacionalidad { get; set; }

        [Required, Display(Name = "Fecha de estreno")]
        [DataType(DataType.Date)]
        public System.DateTime FechaEstreno { get; set; }
        public int ProductorId { get; set; }
        public int DirectorId { get; set; }

        public virtual Productor Productor { get; set; }
        public virtual Director Director { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentario> Comentarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Premio> Premios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Papel> Papeles { get; set; }
    }



    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Comentarios = new HashSet<Comentario>();
        }
        
        [Key]
        public int Id { get; set;}

        [Display(Name = "Nombre")]
        [SoloLetras(ErrorMessage = "El nombre debe ser de solo letras")]
        [Required(ErrorMessage = "Debe digitar la cedula"), MaxLength(20)]
        public string Nombre { get; set; }

        [Required, MaxLength(40)]
        [Display(Name = "Correo Electrónico"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }


    public partial class Productor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Productor()
        {
            this.Peliculas = new HashSet<Pelicula>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [SoloNumeros(ErrorMessage = "La cedula debe ser de solo numeros")]
        [Required(ErrorMessage = "Debe digitar la cedula")]
        [Display(Name = "Cedula")]


        public int Id { get; set; }

        [SoloLetras(ErrorMessage = "El nombre debe ser de solo letras")]
        [Required(ErrorMessage = "Debe digitar el nombre"), MaxLength(30)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        [SoloNumeros(ErrorMessage = "El telefono debe ser de solo numeros")]
        public string Telefono { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }

    public partial class Premio
    {

        public TipoPremio TipoPremio { get; set; }
        public int PeliculaId { get; set; }
        public int CertamenId { get; set; }

        [Required, Display(Name = "Fecha de entrega")]
        [DataType(DataType.Date)]
        public DateTime fechaEntrega { get; set; }
        public virtual Pelicula Pelicula { get; set; }
        public virtual Certamen Certamen { get; set; }
    }

    public enum TipoPremio { Oscar, GloboDeoro, Emmy};


    public partial class Papel
    {
        public string Descripcion { get; set; }
        public string TipoPapel { get; set; }
        public int PeliculaId { get; set; }
        public int ActorId { get; set; }

        public virtual Pelicula Pelicula { get; set; }
        public virtual Actor Actor { get; set; }
    }

    public partial class Director
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Director()
        {
            this.Peliculas = new HashSet<Pelicula>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [SoloNumeros(ErrorMessage = "La cedula debe ser de solo numeros")]
        [Required(ErrorMessage = "Debe digitar la cedula")]
        [Display(Name = "Cedula")]
        public int Id { get; set; }

        [SoloLetras(ErrorMessage = "El nombre debe ser de solo letras")]
        [Required(ErrorMessage = "Debe digitar el nombre"), MaxLength(30)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        [SoloNumeros(ErrorMessage = "El telefono debe ser de solo numeros")]
        public string Telefono { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }

    public partial class Comentario
    {
        public string Descripcion { get; set; }
        public string Lugar { get; set; }

        [Required, Display(Name = "Fecha del comenatario")]
        [DataType(DataType.Date)]
        public System.DateTime FechaComentario { get; set; }
        public int UsuarioId { get; set; }
        public int PeliculaId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Pelicula Pelicula { get; set; }
    }


    public partial class Certamen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Certamen()
        {
            this.Premios = new HashSet<Premio>();
        }

        [Key]
        public int Id { get; set; }
        public string Ciudad { get; set; }
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Premio> Premios { get; set; }
    }

    public partial class Actor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Actor()
        {
            this.Papeles = new HashSet<Papel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Debe digitar la cedula")]
        [Display(Name = "Cedula")]
        public int Id { get; set; }

        [SoloLetras(ErrorMessage = "El nombre debe ser de solo letras")]
        [Required(ErrorMessage = "Debe digitar el nombre"), MaxLength(30)]
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        [SoloNumeros(ErrorMessage = "El telefono debe ser de solo numeros")]
        public string Telefono { get; set; }

        public EstadoCivil EstadoCivil { get; set; }

        [Required, Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]

        public System.DateTime FechaNacimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Papel> Papeles { get; set; }
    }
    public enum EstadoCivil { Casado, Soltero, RelacionAbierta }

}
