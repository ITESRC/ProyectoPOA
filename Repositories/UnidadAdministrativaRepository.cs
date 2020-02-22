using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoPOA.Models;
using System.Text.RegularExpressions;

namespace ProyectoPOA.Repositories
{
    public class UnidadAdministrativaRepository : Repository<Unidadadministrativa>
    {

        public IEnumerable<Unidadadministrativa> GetUnidadesAdministrativas()
        {
            return Context.Unidadadministrativa.Include(x=>x.IdUnidadSuperiorNavigation);
        }

        Regex clave = new Regex("^[0-9]{4}$");
        Regex nombre = new Regex(@"^[A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ\s]+$");
        Regex nombreEncargado = new Regex(@"^([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\']+[\.]?[\s])+([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])+[\s]?([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])?$");

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

        public IEnumerable<Unidadadministrativa> FiltrarUnidades(string datos)
        {
            if (!string.IsNullOrWhiteSpace(datos))
            {
                return Context.Unidadadministrativa.Where(x => x.Eliminado == false && (x.Clave.ToString().Contains(datos) || x.Nombre.ToUpper().Contains(datos.ToUpper()) || x.Encargado.ToUpper().Contains(datos.ToUpper()))).OrderBy(x => x.Nombre).Include(x=>x.IdUnidadSuperiorNavigation);
            }
            else
            {
                return GetUnidadesAdministrativas();
            }
        }

        public List<string> Validar(Unidadadministrativa unidad)
        {
            List<string> errores = new List<string>();

            if (!clave.IsMatch(unidad.Clave.ToString("0000")))
            {
                errores.Add("La clave es incorrecta. Debe de ser de 4 digitos.");
            }
            if (string.IsNullOrWhiteSpace(unidad.Nombre))
            {
                errores.Add("El nombre de la unidad administrativa está vacío.");
            }
            if (!string.IsNullOrWhiteSpace(unidad.Nombre))
            {
                if (!nombre.IsMatch(unidad.Nombre))
                {
                    errores.Add("El nombre de la unidad administrativa no es valido.");
                }

                var unidadAdmin = Context.Unidadadministrativa.Where(x => (x.Nombre.Trim().ToUpper() == unidad.Nombre.Trim().ToUpper() && x.Eliminado == false) && x.Id != unidad.Id).FirstOrDefault();

                if (unidadAdmin!=null)
                {
                    errores.Add($"El nombre de la unidad administrativa {unidad.Nombre} ya existe.");
                }
            }

            if (string.IsNullOrWhiteSpace(unidad.Encargado))
            {
                errores.Add("El nombre del encargado está vacio.");
            }

            var ua = Context.Unidadadministrativa.FirstOrDefault(x => x.Clave == unidad.Clave && x.Eliminado == false && x.Id != unidad.Id) ;

            if (ua != null)
            {
                errores.Add("La clave de la unidad administrativa ya existe.");
            }


            if (!string.IsNullOrWhiteSpace(unidad.Encargado))
            {
                if (!nombreEncargado.IsMatch(unidad.Encargado))
                {
                    errores.Add("El nombre del encargado es incorrecto. Proporcione nombre completo.");
                }
            }

            if (errores.Count > 0)
            {
                return errores;
            }
            else
            {
                return null;
            }

        }
    }
}