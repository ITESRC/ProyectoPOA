using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoPOA.Models.ViewModels;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Services
{
    public class UnidadesAdministrativasService
    {
        public IEnumerable<SuperiorViewModel> GetSuperiores()
        {
            UnidadAdministrativaRepository repos = new UnidadAdministrativaRepository();
            return repos.GetAll().OrderBy(x => x.Nombre).Where(x => x.IdUnidadSuperior == null && x.Eliminado==false).Select(x => new SuperiorViewModel{Id= x.Id, Nombre = x.Encargado});
        }
    }
}
