using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Objetivo
    {
        public Objetivo()
        {
            Estrategia = new HashSet<Estrategia>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Eliminado { get; set; }

        public ICollection<Estrategia> Estrategia { get; set; }
    }
}
