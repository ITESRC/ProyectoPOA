using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Estrategia
    {
        public Estrategia()
        {
            Uaestrategia = new HashSet<Uaestrategia>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Eliminado { get; set; }
        public int IdObjetivo { get; set; }

        public Objetivo IdObjetivoNavigation { get; set; }
        public ICollection<Uaestrategia> Uaestrategia { get; set; }
    }
}
