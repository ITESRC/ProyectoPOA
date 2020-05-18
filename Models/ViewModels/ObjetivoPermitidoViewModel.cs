using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class ObjetivoPermitidoViewModel
    {
        public int IdObjetivoPermitido { get; set; }
        public string NombreObjetivo { get; set; }
        public List<EstrategiaPermitidaViewModel> EstrategiasPermitidas { get; set; }
    }
}
