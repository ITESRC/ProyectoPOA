using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Uapartidas
    {
        public int Id { get; set; }
        public int IdUa { get; set; }
        public int IdPartida { get; set; }

        public Partida IdPartidaNavigation { get; set; }
        public Unidadadministrativa IdUaNavigation { get; set; }
    }
}
