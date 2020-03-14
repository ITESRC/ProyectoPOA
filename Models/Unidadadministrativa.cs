using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Unidadadministrativa
    {
        public Unidadadministrativa()
        {
            InverseIdUnidadSuperiorNavigation = new HashSet<Unidadadministrativa>();
            Uaestrategia = new HashSet<Uaestrategia>();
            Uapartidas = new HashSet<Uapartidas>();
        }

        public int Id { get; set; }
        public short Clave { get; set; }
        public string Nombre { get; set; }
        public string Encargado { get; set; }
        public bool? Eliminado { get; set; }
        public int? IdUnidadSuperior { get; set; }

        public Unidadadministrativa IdUnidadSuperiorNavigation { get; set; }
        public ICollection<Unidadadministrativa> InverseIdUnidadSuperiorNavigation { get; set; }
        public ICollection<Uaestrategia> Uaestrategia { get; set; }
        public ICollection<Uapartidas> Uapartidas { get; set; }
    }
}
