using ProyectoPOA.Models.ViewModels;
using ProyectoPOA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Services
{
    public class UnidadMedidaService
    {
        public IEnumerable<UnidadMedidaIdNomViewModel> GetUnidadMedidaViews()
        {
            UnidadMedidaRepository repos = new UnidadMedidaRepository();
            return repos.GetAll().OrderBy(x => x.Nombre).Where(x => x.Eliminado == false).Select(x => new UnidadMedidaIdNomViewModel { Id = x.Id, Nombre = x.Nombre });
        }

        public IEnumerable<UnidadMedidaIdNomViewModel> GetUnidadMedidaViews(Int32 id)
        {
            UnidadMedidaRepository repos = new UnidadMedidaRepository();
            return repos.GetAll().OrderBy(x => x.Nombre).Where(x => x.Eliminado == false && x.Id != id).Select(x => new UnidadMedidaIdNomViewModel { Id = x.Id, Nombre = x.Nombre });
        }
    }
}
