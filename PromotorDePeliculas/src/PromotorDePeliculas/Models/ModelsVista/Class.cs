using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotorDePeliculas.Models.ModelsVista
{

    public class DirectorPeliculasPremios
    {
        public IEnumerable<Director> Directores { get; set; }
        public IEnumerable<Pelicula> Peliculas { get; set; }
        public IEnumerable<Premio> Premios { get; set; }
    }

    public class ProductorPeliculasPapeles
    {
        public IEnumerable<Productor> Productores { get; set; }
        public IEnumerable<Pelicula> Peliculas { get; set; }
        public IEnumerable<Papel> Papeles { get; set; }
    }

    public class PeliculaNroPremios
    {
        public string Titulo { get; set; }
        public int TotalPremios { get; set; }
    }

    public class PeliculasMasComen
    {
        public string Titulo { get; set; }
        public int comentarios{ get; set; }
    }

    public class PeliculasEstren
    {
        public string Titulo { get; set; }
        public DateTime FechaEstreno{ get; set; }

       
    }

    public class ActorPapelPelicula
    {
        public string Actor { get; set; }
        public string Papel { get; set; }
       
    }

    public  class DirectoresPremios
    {
        public string Nombre { get; set; }

        public IEnumerable<Premio>  Premios { get; set; }

    }


    public class certamenPeliculas
    {
        public string Nombre { get; set; }

        public string Titulo { get; set; }

    }
}
