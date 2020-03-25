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

        public void Eliminar(Int32 Id)
        {
            Capitulo capitulo = GetById(Id);

            if (capitulo != null)
            {
                if (capitulo.Partida.Count > 0)
                {
                    throw new ArgumentException("El capitulo no se puede eliminar ya que tiene partdias asociadas");
                }
                else
                {
                    capitulo.Eliminado = true;
                    Save();
                }
            }
            else
            {
                throw new ArgumentException("El capitulo seleccionado no existe");
            }

        }


        public List<String> Validar(Capitulo cap)
        {
            List<String> errores = new List<String>();
            Regex clave = new Regex("^[0-9]{5}$");
            Regex nombre = new Regex(@"^([A-ZÜÁÉÍÓÚ][a-züáéíóú]+\s?)+$");

            if (!clave.IsMatch(cap.Clave.ToString("00000")))
            {
                errores.Add("La clave de el captiulo es incorrecta.La clave debe de tener 5 digitos");
            }
            if (String.IsNullOrWhiteSpace(cap.Nombre))
            {
                errores.Add("El nombre del capitulo no puede estar vacio.");
            }
            else
            {
                if (!nombre.IsMatch(cap.Nombre) || cap.Nombre.Length > 45)
                {
                    errores.Add("El nombre de el capitulo no es válido (Máximo de 45 caracteres).");
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

            if (errores.Count > 0) return errores;
            else return null;

        }


        public Capitulo GetCapituloXId(Int32 Id)
        {
            return Context.Capitulo.FirstOrDefault(x => x.Eliminado == false && x.Id == Id);
        }

    }
}
