using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class CapitulosPartidasViewModel
    {
        public Capitulo Capitulo { get; set; }
        public Partida Partida { get; set; }
        public IEnumerable<Capitulo> ListaCapitulos { get; set; }
    }
}
