using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class EstrategiaPermitidaViewModel
    {
        public int IdEstrategia { get; set; }
        public string NombreEstrategia { get; set; }
        public bool Permitida { get; set; }
        public int IdObjetivo { get; set; }
    }
}
