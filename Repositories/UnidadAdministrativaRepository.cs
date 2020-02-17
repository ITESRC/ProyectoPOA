using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoPOA.Models;
using ProyectoPOA.Models.ViewModels;
using System.Text.RegularExpressions;

namespace ProyectoPOA.Repositories
{
    public class UnidadAdministrativaRepository : Repository<Unidadadministrativa>
    {

        public IEnumerable<UnidadAdministrativasViewModel> GetUnidadesAdministrativas()
        {
            return Context.Unidadadministrativa.Include(x => x.IdUnidadSuperiorNavigation).Where(x => x.Eliminado == false).Select(x => new UnidadAdministrativasViewModel { Id = x.Id, Clave = x.Clave, Nombre = x.Nombre, NombreEncargado = x.Encargado, NombreSuperior = x.IdUnidadSuperiorNavigation.Encargado }).OrderBy(x => x.Nombre);
        }

        //public void Insert(UnidadAdministrativasViewModel vm)
        //{
        //    var idEncargado = Context.Unidadadministrativa.FirstOrDefault(x => x.Encargado == vm.NombreEncargado).IdUnidadSuperior;
        //    Unidadadministrativa c = new Unidadadministrativa { Id = vm.Id, Clave = vm.Clave, Nombre = vm.Nombre, Encargado = vm.NombreEncargado, Eliminado = false, IdUnidadSuperior = idEncargado };
        //    Insert(c);
        //}

        Regex clave = new Regex(@"^[0-9]{4}$");
        Regex nombre = new Regex(@"^[A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ\s]+$");
        Regex nombreEncargado = new Regex(@"^([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\']+[\s])+([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])+[\s]?([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])?$");

        public bool ValidarUnidadAdministrativa(Unidadadministrativa unidad, bool editar)
        {

            if (!clave.IsMatch(unidad.Clave.ToString()))
            {
                throw new Exception("La clave es incorrecta. Debe de ser de 4 digitos.");
            }
            if (string.IsNullOrWhiteSpace(unidad.Nombre))
            {
                throw new Exception("El nombre de la unidad administrativa no debe de ir vacío.");
            }
            if (string.IsNullOrWhiteSpace(unidad.Encargado))
            {
                throw new Exception("El nombre del encargado no debe de ir vacío.");
            }

            if (GetAll().Any(x => (x.Nombre.ToUpper() == unidad.Nombre.ToUpper() && x.Eliminado == false) && editar == false))
            {
                throw new Exception($"La unidad administrativa {unidad.Nombre} ya existe y está activa.");
            }

            if (!nombre.IsMatch(unidad.Nombre))
            {
                throw new Exception($"El nombre de la unidad administrativa no es valido.");
            }

            if (!nombreEncargado.IsMatch(unidad.Encargado))
            {
                throw new Exception($"El nombre del encargado es incorrecto.\nProporcione nombre completo.");
            }

            return true;
        }

        public void EliminarUnidad(int id)
        {
            var unidad = GetById(id);
            if (unidad != null)
            {
                unidad.Eliminado = true;
                Save();
            }
        }

        public IEnumerable<UnidadAdministrativasViewModel> FiltrarUnidades(string datos)
        {
            if (!string.IsNullOrWhiteSpace(datos))
            {
                return Context.Unidadadministrativa.Include(x => x.IdUnidadSuperiorNavigation).Where(x => x.Eliminado == false && (x.Clave.ToString().Contains(datos) || x.Nombre.ToUpper().Contains(datos.ToUpper()) || x.Encargado.ToUpper().Contains(datos.ToUpper()))).Select(x => new UnidadAdministrativasViewModel { Id = x.Id, Clave = x.Clave, Nombre = x.Nombre, NombreEncargado = x.Encargado, NombreSuperior = x.IdUnidadSuperiorNavigation.Encargado }).OrderBy(x => x.Nombre);
            }
            else
            {
                return GetUnidadesAdministrativas();
            }
        }
    }
}