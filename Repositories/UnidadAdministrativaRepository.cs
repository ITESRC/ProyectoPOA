using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoPOA.Models;
using ProyectoPOA.Models.ViewModels;

namespace ProyectoPOA.Repositories
{
    public class UnidadAdministrativaRepository:Repository<Unidadadministrativa>
    {
        //Ordenar por nombre
        public IEnumerable<Unidadadministrativa> ByName()
        {
            var dato = Context.Unidadadministrativa.OrderBy(x => x.Encargado);
            return dato;
        }

        public Unidadadministrativa BySuperior(int id)
        {
            // Variable para buscar el nombre del superior via id_superior enlazando a la tabla de superiores, se necesita la otra tabla
            var dato = Context.Unidadadministrativa.FirstOrDefault(x => x.IdUnidadSuperior == id);
            return dato;
        }

        public IEnumerable<UnidadAdministrativasViewModel> GetUnidadesAdministrativas()
        {
            return Context.Unidadadministrativa.Include(x=>x.IdUnidadSuperiorNavigation).Where(x=>x.Eliminado == false).Select(x=> new UnidadAdministrativasViewModel { Id = x.Id, Clave = x.Clave, Nombre = x.Nombre, NombreEncargado = x.Encargado, NombreSuperior = x.IdUnidadSuperiorNavigation.Encargado});
        }
    }
}