using Microsoft.EntityFrameworkCore;
using ProyectoPOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoPOA.Repositories
{
    public class ArticulosRepository:Repository<Articulo>
    {
        public Boolean Eliminar(Int32 Id)
        {
            Articulo p = GetPartidaXId(Id);
            if (p != null || p.Eliminado == false)
            {
                p.Eliminado = true;
                Save();
                return true;
            }
            else
            {
                throw new ArgumentException("El articulo seleccionado no existe");
            }

        }

        public IEnumerable<Articulo> GetArticulos()
        {
            return Context.Articulo.Include(x=>x.IdunidadmedidaNavigation).Where(x => x.Eliminado == false).OrderBy(x => x.Descripcion);
        }

        public IEnumerable<Articulo> GetArticulosByDescripcion(String desc)
        {
            return Context.Articulo.Include(x=>x.IdunidadmedidaNavigation).Where(X => X.Descripcion.ToLower().Contains(desc.ToLower())&&X.Eliminado==false);
        }

        public Boolean Validar(Articulo a, out List<String> errores)
        {
            errores = new List<String>();
            Regex precio = new Regex(@"^[-]?\d*[.]?\d+$");
            Regex nombre = new Regex(@"^([A-ZÜÁÉÍÓÚ][a-züáéíóú]+\s?)+$");

            if (String.IsNullOrWhiteSpace(a.CostoUnitario.ToString()))
            {
                errores.Add("El costo unitario no puede estar vacío.");
            }
            else
            {
                if (!precio.IsMatch(a.CostoUnitario.ToString()))
                {
                    errores.Add("El costo unitario es incorrecto.La clave debe de tener 5 digitos.");
                }
            }

            if (String.IsNullOrWhiteSpace(a.Descripcion))
            {
                errores.Add("El nombre el articulo no puede estar vacío.");
            }
            else
            {
                if (!nombre.IsMatch(a.Descripcion) || a.Descripcion.Length > 45)
                {
                    errores.Add("El nombre de el articulo no es válido (Máximo de 45 caracteres , no utilice caracteres especiales, respete mayúsculas y minúsculas).");
                }
                else
                {
                    //Articulo part = Context.Articulo.FirstOrDefault(x => x.Descripcion.Trim().ToUpper() == a.Descripcion.Trim().ToUpper() && x.Eliminado == false && x.Id != a.Id); ;
                    //if (part != null)
                    //{
                    //    errores.Add($"El articulo {part.Descripcion} ya existe.");
                    //}
                }
            }

            if (a.Id != 0)
            {
                if (Context.Unidadmedida.Any(x => x.Id == a.Id && x.Eliminado == true))
                {
                    errores.Add("La unidad de medida no existe o ya ha sido eliminada");
                }
            }

            if (!Context.Unidadmedida.Any(x=>x.Id==a.Idunidadmedida))
            {
                errores.Add("La unidad de medida seleccionada no existe.");
            }

            if (!Context.Partida.Any(x => x.Clave == a.Idpartida))
            {
                errores.Add("La partida seleccionada no existe.");
            }
            return errores.Count == 0;
        }

        public Articulo GetPartidaXId(Int32 Id)
        {
            return Context.Articulo.FirstOrDefault(x => x.Eliminado == false && x.Id == Id);
        }

    }
}
