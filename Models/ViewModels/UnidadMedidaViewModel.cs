using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class UnidadMedidaViewModel
    {
        public Unidadmedida UnidadMedida { get; set; }
        public IEnumerable<Unidadmedida> ListaUnidadMedida { get; set; }
    }
}
