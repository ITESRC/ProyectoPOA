using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class UnidadEstrategiasPermitidasViewModel
    {
        public int IdUnidad { get; set; }
        public string NombreUnidad { get; set; }
        public List<ObjetivoPermitidoViewModel> ObjetivosPermitidos { get; set; }
    }
}
