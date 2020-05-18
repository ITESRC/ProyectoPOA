using ProyectoPOA.Models;
using ProyectoPOA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Repositories
{
    public class PermitirEstrategiaRepository:Repository<Uaestrategia>
    {
        public UnidadEstrategiasPermitidasViewModel GetEstrategiasPermitidasDeUnidad(int idUnidad)
        {
            UnidadEstrategiasPermitidasViewModel unidadEstrategias = new UnidadEstrategiasPermitidasViewModel();
            var unidad = Context.Unidadadministrativa.Where(x => x.Id == idUnidad).FirstOrDefault();///validar que la unidad no esté eliminada
            if (unidad != null)
            {
                unidadEstrategias.IdUnidad = idUnidad;
                unidadEstrategias.NombreUnidad = unidad.Nombre;
                unidadEstrategias.ObjetivosPermitidos = new List<ObjetivoPermitidoViewModel>();

                var objetivosPermitidos = Context.Uaestrategia.Where(x => x.IdUa == idUnidad).Select(x => x.IdEstrategiaNavigation.IdObjetivoNavigation).Distinct();

                foreach (var objetivo in objetivosPermitidos)
                {
                    ObjetivoPermitidoViewModel o = new ObjetivoPermitidoViewModel()
                    {
                        IdObjetivoPermitido = objetivo.Id,
                        NombreObjetivo = objetivo.Nombre,
                        EstrategiasPermitidas = new List<EstrategiaPermitidaViewModel>()
                    };
                    /// sacar las estrategias relacionadas con ese objetivo
                    var estrategiasPermitidas = Context.Uaestrategia.Where(x => x.IdEstrategiaNavigation.IdObjetivo == o.IdObjetivoPermitido && x.IdUa == idUnidad).Select(x => x.IdEstrategiaNavigation).ToList();

                    foreach (var estrategia in estrategiasPermitidas)
                    {
                        EstrategiaPermitidaViewModel e = new EstrategiaPermitidaViewModel()
                        {
                            IdEstrategia = estrategia.Id,
                            NombreEstrategia = estrategia.Nombre,
                            IdObjetivo = o.IdObjetivoPermitido
                        };
                        o.EstrategiasPermitidas.Add(e);
                    }
                    unidadEstrategias.ObjetivosPermitidos.Add(o);
                }
            }
            return unidadEstrategias;
        }
    }
}
