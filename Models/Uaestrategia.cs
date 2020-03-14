using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Uaestrategia
    {
        public int Id { get; set; }
        public int IdEstrategia { get; set; }
        public int IdUa { get; set; }

        public Estrategia IdEstrategiaNavigation { get; set; }
        public Unidadadministrativa IdUaNavigation { get; set; }
    }
}
