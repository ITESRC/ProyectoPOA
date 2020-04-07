using Microsoft.EntityFrameworkCore;
using ProyectoPOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoPOA.Repositories
{
    public class CapitulosRepository:Repository<Capitulo>
    {

        public IEnumerable<Capitulo> GetCapitulos()
        {
            return Context.Capitulo.Include(x => x.Partida).Where(x => x.Eliminado == false).OrderBy(x => x.Id);
        }

        public Boolean Eliminar(Int32 Id)
        {
            Capitulo capitulo = GetCapituloXId(Id);

            if (capitulo != null || capitulo.Eliminado == false)
            {
                if (capitulo.Partida.All(x => x.Eliminado == true))
                {
                    capitulo.Eliminado = true;
                    Save();
                    return true;
                }
                else
                {
                    throw new ArgumentException("El capitulo no se puede eliminar ya que tiene partdias asociadas");
                }
            }
            else
            {
                throw new ArgumentException("El capitulo seleccionado no existe");
            }

        }


        public Boolean Validar(Capitulo cap, out List<String> errores)
        {
            errores = new List<String>();
            Regex clave = new Regex("^[0-9]{4}$");
            Regex nombre = new Regex(@"^([A-ZÜÁÉÍÓÚ][a-züáéíóú]+\s?)+$");

            if (!clave.IsMatch(cap.Clave.ToString()))
            {
                errores.Add("La clave de el captiulo es incorrecta.La clave debe de tener 4 digitos");
            }
            if (String.IsNullOrWhiteSpace(cap.Nombre))
            {
                errores.Add("El nombre del capitulo no puede estar vacio.");
            }
            else
            {
                if (!nombre.IsMatch(cap.Nombre) || cap.Nombre.Length > 45)
                {
                    errores.Add("El nombre de el capitulo no es válido (Máximo de 45 caracteres , no utilice caracteres especiales, respete mayúsculas y minúsculas).");
                }
                else
                {
                    Capitulo capitulo = Context.Capitulo.FirstOrDefault(x => x.Clave == cap.Clave && x.Eliminado == false && x.Id != cap.Id);
                    if (capitulo != null)
                    {
                        errores.Add("La clave del capitulo ya existe.");
                    }
                }
            }
            return errores.Count == 0;
        }

        public Capitulo GetCapituloXId(Int32 Id)
        {
            return Context.Capitulo.Include(x=>x.Partida).FirstOrDefault(x => x.Eliminado == false && x.Id == Id);
        }

    }
}
