using ProyectoPOA.Models.ViewModels;
using ProyectoPOA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Services
{
    public class PartidasService
    {
        public IEnumerable<NumeroPartidaViewModel> GetPartidas()
        {
            PartidasRepository articulosRepository = new PartidasRepository();
            return articulosRepository.GetAll().Where(x => x.Eliminado == false).Select(x => new NumeroPartidaViewModel { Clave = x.Clave, Nombre = x.Concepto });
        }

        public IEnumerable<NumeroPartidaViewModel> GetPartidas(Int32 Clave)
        {
            PartidasRepository articulosRepository = new PartidasRepository();
            return articulosRepository.GetAll().Where(x => x.Eliminado == false && x.Clave != Clave).Select(x => new NumeroPartidaViewModel { Clave = x.Clave, Nombre = x.Concepto });
        }
    }
}
