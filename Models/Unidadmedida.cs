using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Unidadmedida
    {
        public Unidadmedida()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Eliminado { get; set; }

        public ICollection<Articulo> Articulo { get; set; }
    }
}
