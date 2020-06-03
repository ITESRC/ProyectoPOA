using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels.UaPartidasCapitulos
{
    public class CapituloPermitidoViewModel
    {
        public int IdCapituloPermitido { get; set; }
        public string NombreCapitulo { get; set; }
        public List<PartidaPermitidaViewModel> PartidasPermitidas { get; set; }
    }
}
