using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Unidadadministrativa
    {
        public Unidadadministrativa()
        {
            InverseIdUnidadSuperiorNavigation = new HashSet<Unidadadministrativa>();
        }

        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Encargado { get; set; }
        public bool? Eliminado { get; set; }
        public int? IdUnidadSuperior { get; set; }

        public Unidadadministrativa IdUnidadSuperiorNavigation { get; set; }
        public ICollection<Unidadadministrativa> InverseIdUnidadSuperiorNavigation { get; set; }
    }
}
