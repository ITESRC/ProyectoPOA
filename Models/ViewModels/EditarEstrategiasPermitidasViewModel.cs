using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class EditarEstrategiasPermitidasViewModel
    {
        public int IdUnidad { get; set; }
        public List<EstrategiaPermitidaViewModel> EstrategiasPermitidas { get; set; }
    }
}
