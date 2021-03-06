﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoPOA.Models;
using ProyectoPOA.Models.ViewModels;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Services
{
    public class UnidadesAdministrativasService
    {
        public IEnumerable<SuperiorViewModel> GetSuperiores()
        {
            UnidadAdministrativaRepository repos = new UnidadAdministrativaRepository();
            return repos.GetAll().OrderBy(x => x.Nombre).Where(x => x.IdUnidadSuperior == null && x.Eliminado == false).Select(x => new SuperiorViewModel{Id= x.Id, Nombre = x.Nombre});
        }

        public IEnumerable<SuperiorViewModel> GetSuperiores(int id)
        {
            UnidadAdministrativaRepository repos = new UnidadAdministrativaRepository();
            return repos.GetAll().OrderBy(x => x.Nombre).Where(x => (x.IdUnidadSuperior == null && x.Eliminado == false) && x.Id != id).Select(x => new SuperiorViewModel { Id = x.Id, Nombre = x.Nombre });
        }

        public IEnumerable<Unidadadministrativa> GetUnidades()
        {
            UnidadAdministrativaRepository repos = new UnidadAdministrativaRepository();
            return repos.GetAll().Where(x=>x.Eliminado == false);
        }

        public IEnumerable<Unidadadministrativa> GetUnidades(int id)
        {
            UnidadAdministrativaRepository repos = new UnidadAdministrativaRepository();
            return repos.GetAll().Where(x => x.Eliminado == false && x.Id!=id);
        }
    }
}
