using ProyectoPOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoPOA.Repositories
{
    public class UnidadMedidaRepository:Repository<Unidadmedida>
    {

        public IEnumerable<Unidadmedida> GetUnidadmedidas() 
        {
            return Context.Unidadmedida.Where(x => x.Eliminado == false).OrderBy(x => x.Nombre);
        }

        public Boolean Eliminar(Int32 Id) 
        {
            Unidadmedida unidadmedida = GetUnidadmedidaById(Id);
            if (unidadmedida!=null)
            {
                unidadmedida.Eliminado = true;
                Save();
                return true;
            }
            else
            {
                throw new ArgumentException("La unidad de medida no existe o ya ha sido eliminada");
            }
        }
        
        public Boolean Validar(Unidadmedida um,out List<String> errores) 
        {
            errores = new List<String>();
            Regex nombre = new Regex(@"^([A-ZÜÁÉÍÓÚ][a-züáéíóú]+\s?)+$");

            if (String.IsNullOrWhiteSpace(um.Nombre))
            {
                errores.Add("La unidad de medidad no puede estar vaciá");
            }
            else
            {
                if (!nombre.IsMatch(um.Nombre)||um.Nombre.Length>45)
                {
                    errores.Add("El nombre de la unidad de medida no es válido (Máximo de 45 caracteres , no utilice caracteres especiales, respete mayúsculas y minúsculas).");
                }
                else
                {
                    Unidadmedida unidadmedida1 = Context.Unidadmedida.FirstOrDefault(x => x.Nombre.Trim().ToUpper() == um.Nombre.Trim().ToUpper() && x.Eliminado == false && x.Id != um.Id);
                    if (unidadmedida1!=null)
                    {
                        errores.Add("La unidad de medida ya existe");
                    }
                }
            }

            return errores.Count == 0;
        }

        public Unidadmedida GetUnidadmedidaById(Int32 Id) 
        {
            return Context.Unidadmedida.FirstOrDefault(x => x.Id == Id && x.Eliminado == false);
        }

    }
}
