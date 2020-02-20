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
            List<UnidadAdministrativasViewModel> unidades = Context.Unidadadministrativa.Include(x => x.IdUnidadSuperiorNavigation).Where(x => x.Eliminado == false).Select(x => new UnidadAdministrativasViewModel { Id = x.Id, Clave = x.Clave, Nombre = x.Nombre, NombreEncargado = x.Encargado, NombreUnidadSuperior = x.IdUnidadSuperiorNavigation.Nombre, Eliminar = true}).OrderBy(x => x.Nombre).ToList();
            foreach (var u in unidades)
            {
                if (GetAll().Any(x => x.IdUnidadSuperior == u.Id))
                {
                    var unidad = unidades.Where(x => x.Id == u.Id).FirstOrDefault();
                    unidad.Eliminar = false;
                }
            }
            return unidades;
        }

        Regex clave = new Regex("^[0-9]{4}$");
        Regex nombre = new Regex(@"^[A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ\s]+$");
        Regex nombreEncargado = new Regex(@"^([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\']+[\s])+([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])+[\s]?([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])?$");

        public void ValidarUnidadAdministrativa(Unidadadministrativa unidad)
        {
            string errores = "";

            if (!clave.IsMatch(unidad.Clave))
            {
                errores = errores + "La clave es incorrecta. Debe de ser de 4 digitos.\n";
            }
            if (string.IsNullOrWhiteSpace(unidad.Nombre))
            {
                errores = errores + "El nombre de la unidad administrativa no debe de ir vacío.\n";
            }
            if (string.IsNullOrWhiteSpace(unidad.Encargado))
            {
                errores = errores + "El nombre del encargado no debe de ir vacío.\n";
            }

            if (GetAll().Any(x => x.Clave == unidad.Clave))
            {
                if(unidad.Id == 0)
                {
                    errores = errores + "La clave de la unidad administrativa ya existe.\n";
                }
                else
                {
                    var editar = GetById(unidad.Id);

                    if(editar.Clave != unidad.Clave && GetAll().Any(x => x.Clave == unidad.Clave))
                    {
                        errores = errores + "La clave de la unidad administrativa ya existe.\n";
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(unidad.Nombre))
            {

                if (GetAll().Any(x => (x.Nombre.Trim().ToUpper() == unidad.Nombre.Trim().ToUpper() && x.Eliminado == false)))
                {
                    
                    if (unidad.Id == 0)
                    {
                        errores = errores + $"La unidad administrativa {unidad.Nombre} ya existe y está activa.\n";
                    }
                    else
                    {
                        var editar = GetById(unidad.Id);

                        if (editar.Nombre != unidad.Nombre && GetAll().Any(x => x.Nombre.Trim().ToUpper() == unidad.Nombre.Trim().ToUpper() && x.Eliminado == false))
                        {
                            errores = errores + $"La unidad administrativa {unidad.Nombre} ya existe.\n";
                        }
                    }
                }

                if (!nombre.IsMatch(unidad.Nombre))
                {
                    errores = errores + "El nombre de la unidad administrativa no es valido.\n";
                }
            }

            if (!string.IsNullOrWhiteSpace(unidad.Encargado))
            {
                if (!nombreEncargado.IsMatch(unidad.Encargado))
                {
                    errores = errores + $"El nombre del encargado es incorrecto.\nProporcione nombre completo.\n";
                }
            }

            if (!string.IsNullOrWhiteSpace(errores))
            {
                throw new Exception(errores);
            }

        }

        public void EliminarUnidad(int id)
        {
            var unidad = GetById(id);
            if (unidad != null)
            {
                if(GetAll().Any(x=>x.IdUnidadSuperior == unidad.Id))
                {
                    throw new Exception($"La unidad adminstrativa {unidad.Nombre} no se puede eliminar.");
                }
                else
                {
                    unidad.Eliminado = true;
                    Save();
                }
            }
            else
            {
                throw new Exception("La unidad adminstrativa no existe.");
            }
        }

        public IEnumerable<UnidadAdministrativasViewModel> FiltrarUnidades(string datos)
        {
            if (!string.IsNullOrWhiteSpace(datos))
            {
                return Context.Unidadadministrativa.Include(x => x.IdUnidadSuperiorNavigation).Where(x => x.Eliminado == false && (x.Clave.ToString().Contains(datos) || x.Nombre.ToUpper().Contains(datos.ToUpper()) || x.Encargado.ToUpper().Contains(datos.ToUpper()))).Select(x => new UnidadAdministrativasViewModel { Id = x.Id, Clave = x.Clave, Nombre = x.Nombre, NombreEncargado = x.Encargado, NombreUnidadSuperior = x.IdUnidadSuperiorNavigation.Encargado }).OrderBy(x => x.Nombre);
            }
            else
            {
                return GetUnidadesAdministrativas();
            }
        }
    }
}