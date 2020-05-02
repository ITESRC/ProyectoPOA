using ProyectoPOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoPOA.Repositories
{
    public class PartidasRepository:Repository<Partida>
    {

        //public Boolean Eliminar(Int32 Id)
        //{
        //    //Partida p = GetPartidaXId(Id);
        //    //if (p != null || p.Eliminado == false)
        //    //{
        //    //    p.Eliminado = true;
        //        Save();
        //        return true;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("La partida no existe");
        //    }

        //}

        public Boolean Validar(Partida p, out List<String> errores)
        {
            errores = new List<String>();
            Regex clave = new Regex("^[0-9]{5}$");
            Regex nombre = new Regex(@"^([A-ZÜÁÉÍÓÚ][a-züáéíóú]+\s?)+$");

            if (!clave.IsMatch(p.Clave.ToString()))
            {
                errores.Add("La clave de la partida es incorrecta.La clave debe de tener 5 digitos.");
            }
            if (String.IsNullOrWhiteSpace(p.Concepto))
            {
                errores.Add("El nombre de la partida no puede estar vacio.");
            }
            else
            {
                if (!nombre.IsMatch(p.Concepto) || p.Concepto.Length > 45)
                {
                    errores.Add("El nombre de la partida no es válido (Máximo de 45 caracteres , no utilice caracteres especiales, respete mayúsculas y minúsculas).");
                }
                else
                {
                    //Partida part = Context.Partida.FirstOrDefault(x => x.Concepto.Trim().ToUpper() == p.Concepto.Trim().ToUpper() && x.Eliminado == false && x.Id != p.Id);
                    //if (part != null)
                    //{
                    //    errores.Add($"El concepto {part.Concepto} ya existe.");
                    //}
                }
            }
            if (!Context.Capitulo.Any(x => x.Id == p.Capitulo))
            {
                errores.Add("El capitulo seleccionado no existe.");
            }
            return errores.Count == 0;
        }


        //public Partida GetPartidaXId(Int32 Id)
        //{
        //    return Context.Partida.FirstOrDefault(x => x.Eliminado == false && x.Id == Id);
        //}

    }
}
