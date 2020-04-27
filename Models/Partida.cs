using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Partida
    {
        public Partida()
        {
            Uapartidas = new HashSet<Uapartidas>();
        }

        public short Clave { get; set; }
        public int Capitulo { get; set; }
        public string Concepto { get; set; }
        public bool? Eliminado { get; set; }

        public Capitulo CapituloNavigation { get; set; }
        public ICollection<Uapartidas> Uapartidas { get; set; }
    }
}
