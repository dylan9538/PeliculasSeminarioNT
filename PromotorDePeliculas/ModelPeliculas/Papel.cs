//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModelPeliculas
{
    using System;
    using System.Collections.Generic;
    
    public partial class Papel
    {
        public string Descripcion { get; set; }
        public string TipoPapel { get; set; }
        public int PeliculaId { get; set; }
        public int ActorId { get; set; }
    
        public virtual Pelicula Pelicula { get; set; }
        public virtual Actor Actor { get; set; }
    }
}