using ProyectoPOA.Models;
using ProyectoPOA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Repositories
{
    public class ObjetivoRepository : Repository<Objetivo>
    {
        public IEnumerable<Objetivo> ObjetivoActivos()
        {
            var Activo = Context.Objetivo.Where(x => x.Eliminado == true);
            return Activo;
        }
        public Objetivo GetObjetivoById(int idObjetivo)
        {
            return Context.Objetivo.FirstOrDefault(x => x.Id == idObjetivo && x.Eliminado == true);
        }
        public IEnumerable<Objetivo> ObjetivosInactivos()
        {
            var NoActivo = Context.Objetivo.Where(x => x.Eliminado == true);
            return NoActivo;
        }
        public IEnumerable<ObjetivosEstrategiasViewModel> GetEstrategias()
        {
            ObjetivoRepository repos = new ObjetivoRepository();
            return repos.GetAll().OrderBy(x => x.Nombre).Where(x => x.Eliminado == false).Select(x => new ObjetivosEstrategiasViewModel { Id = x.Id, Nombre = x.Nombre });
        }



        //public string GetEstrategiasByObjetivo(int IdObjetivo)
        //{
        //    var estra = Context.Estrategia.Where(x => x.IdObjetivo == IdObjetivo);
        //    var script = "<select>< option selected = 'selected' value = '' > --Estrategias-- </ option ></ select >";
        //    while (estra != null)
        //    {
        //        script = "<select asp-for='@Model.Id'  asp-items='@(new SelectList(" + estra.ToList() + ", 'Nombre'))'>< option selected = 'selected' value = '' > --Estrategias-- </ option ></ select > ";
        //        return script;
        //    }
        //    return script;


        //}
        public List<string> Validar(int Vo)
        {
            var objetivo = Context.Objetivo.FirstOrDefault(x => x.Id == Vo);
            List<string> errores = new List<string>();
            var repetido = Context.Objetivo.Any(x => x.Id == objetivo.Id && x.Eliminado == false);

            if (repetido)
            {
                errores.Append("No puede haber más de un objetivo igual");
            }

            return errores;
        }


    }
}
