using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoPOA.Models;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ProyectoPOA.Repositories
{
    public class UnidadAdministrativaRepository : Repository<Unidadadministrativa>
    {

        public IEnumerable<Unidadadministrativa> GetUnidadesAdministrativas()
        {
            return Context.Unidadadministrativa.Where(x=>x.Eliminado==false).Include(x=>x.IdUnidadSuperiorNavigation).OrderBy(x=>x.Nombre);
        }

        public void EliminarUnidad(int id)
        {
            var unidad = GetById(id);
            if (unidad != null)
            {
                if(GetAll().Any(x=>x.IdUnidadSuperior == unidad.Id && x.Eliminado == false))
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

        public List<string> Validar(Unidadadministrativa unidad)
        {   ////Ajustar Mayusculas
            unidad.Nombre= CapitalTitle(unidad.Nombre);
            unidad.Encargado = CapitalTitle(unidad.Encargado);
            List<string> errores = new List<string>();
            Regex clave = new Regex("^[0-9]{4}$");
            Regex nombre = new Regex(@"^([A-ZÜÁÉÍÓÚ][a-züáéíóú]+\s?)+$");
            Regex nombreEncargado = new Regex(@"^([A-ZÜÁÉÍÓÚ][a-züáéíóú\.]+\s?){2,}?$");

            if (!clave.IsMatch(unidad.Clave.ToString("0000")))
            {
                errores.Add("La clave es incorrecta. Debe de ser de 4 digitos.");
            }
            if (string.IsNullOrWhiteSpace(unidad.Nombre))
            {
                errores.Add("El nombre de la unidad administrativa está vacío.");
            }
            else
            {
                if (!nombre.IsMatch(unidad.Nombre) || unidad.Nombre.Trim().Length>60)
                {
                    errores.Add("El nombre de la unidad administrativa no es válido (Máximo de 60 caracteres, no utilice caracteres especiales, respete mayúsculas y minúsculas).");
                }
                else
                {
                    var unidadAdmin = Context.Unidadadministrativa.Where(x => (x.Nombre.Trim().ToUpper() == unidad.Nombre.Trim().ToUpper() && x.Eliminado == false) && x.Id != unidad.Id).FirstOrDefault();

                    if (unidadAdmin != null)
                    {
                        errores.Add($"El nombre de la unidad administrativa {unidad.Nombre} ya existe.");
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(unidad.Encargado))
            {
                errores.Add("El nombre del encargado está vacio.");
            }
            else
            {
                if (!nombreEncargado.IsMatch(unidad.Encargado) || unidad.Encargado.Trim().Length>60)
                {
                    errores.Add("El nombre del encargado no es válido (Máximo de 60 caracteres, respete mayúsculas y minúsculas, proporcione al menos un nombre y un apellido).");
                }
                else
                {
                    var encargado = Context.Unidadadministrativa.FirstOrDefault(x => x.Encargado.Trim().ToUpper() == unidad.Encargado.Trim().ToUpper() && x.Eliminado == false && x.Id != unidad.Id);

                    if (encargado != null)
                    {
                        errores.Add("El encargado proporcionado ya está a cargo de otra Unidad Administrativa.");
                    }
                }
            }

            var ua = Context.Unidadadministrativa.FirstOrDefault(x => x.Clave == unidad.Clave && x.Eliminado == false && x.Id != unidad.Id) ;

            if (ua != null)
            {
                errores.Add("La clave de la unidad administrativa ya existe.");
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

        string CapitalTitle(string text)
        {
            string palabra = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text);
            return palabra;

        }
        public Unidadadministrativa EditarUnidadById(int id)
        {
            return Context.Unidadadministrativa.Where(x => x.Id == id && x.Eliminado == false).FirstOrDefault();
        }
    }
}