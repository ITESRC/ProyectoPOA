using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels.UaPartidasCapitulos
{
    public class UnidadPartidasPermitidasViewModel
    {
        public int IdUnidad { get; set; }
        public string NombreUnidad { get; set; }

        public List<CapituloPermitidoViewModel> CapitulosPermitidos { get; set; }
    }
}
