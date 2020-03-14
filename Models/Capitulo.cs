using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Capitulo
    {
        public Capitulo()
        {
            Partida = new HashSet<Partida>();
        }

        public int Id { get; set; }
        public short Clave { get; set; }
        public string Nombre { get; set; }
        public bool? Eliminado { get; set; }

        public ICollection<Partida> Partida { get; set; }
    }
}
