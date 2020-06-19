using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
            UnidadEstrategiasPermitidasViewModel unidadEstrategias;
            var unidad = Context.Unidadadministrativa.Where(x => x.Id == idUnidad).FirstOrDefault();///validar que la unidad no esté eliminada
            if (unidad != null)
            {
                unidadEstrategias = new UnidadEstrategiasPermitidasViewModel();
                unidadEstrategias.IdUnidad = idUnidad;
                unidadEstrategias.NombreUnidad = unidad.Nombre;
                unidadEstrategias.ObjetivosPermitidos = new List<ObjetivoPermitidoViewModel>();

                var objetivosPermitidos = Context.Uaestrategia.Where(x => x.IdUa == idUnidad).Select(x => x.IdEstrategiaNavigation.IdObjetivoNavigation).Where(x=> x.Eliminado == false).Distinct();

                foreach (var objetivo in objetivosPermitidos)
                {
                    ObjetivoPermitidoViewModel o = new ObjetivoPermitidoViewModel()
                    {
                        IdObjetivoPermitido = objetivo.Id,
                        NombreObjetivo = objetivo.Nombre,
                        EstrategiasPermitidas = new List<EstrategiaPermitidaViewModel>()
                    };
                    /// sacar las estrategias relacionadas con ese objetivo
                    var estrategiasPermitidas = Context.Uaestrategia.Where(x => x.IdEstrategiaNavigation.IdObjetivo == o.IdObjetivoPermitido && x.IdUa == idUnidad).Select(x => x.IdEstrategiaNavigation).Where(x=> x.Eliminado == false).ToList();

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
                return unidadEstrategias;
            }
            else
            {
                return null;
            }
        }
    
        public UnidadEstrategiasPermitidasViewModel GetEstrategiasParaPermitir(int idUnidad)
        {
            UnidadEstrategiasPermitidasViewModel unidadEstrategias;
            var unidad = Context.Unidadadministrativa.Where(x => x.Id == idUnidad).FirstOrDefault();
            if (unidad != null)
            {
                unidadEstrategias = new UnidadEstrategiasPermitidasViewModel();
                unidadEstrategias.IdUnidad = idUnidad;
                unidadEstrategias.NombreUnidad = unidad.Nombre;
                unidadEstrategias.ObjetivosPermitidos = new List<ObjetivoPermitidoViewModel>();

                var objetivos = Context.Objetivo.Where(x => x.Eliminado == false);

                foreach (var objetivo in objetivos)
                {
                    ObjetivoPermitidoViewModel o = new ObjetivoPermitidoViewModel()
                    {
                        IdObjetivoPermitido = objetivo.Id,
                        NombreObjetivo = objetivo.Nombre,
                        EstrategiasPermitidas = new List<EstrategiaPermitidaViewModel>()
                    };
                    var estrategiasPermitidas = Context.Uaestrategia
                        .Where(x => x.IdEstrategiaNavigation.IdObjetivo == o.IdObjetivoPermitido && x.IdUa == idUnidad)
                        .Select(x => x.IdEstrategiaNavigation).ToList();

                    var estrategiasByObjetivo = Context.Estrategia.Where(x => x.IdObjetivo == objetivo.Id && x.Eliminado == false);
                    foreach (var estrategia in estrategiasByObjetivo)
                    {
                        EstrategiaPermitidaViewModel e = new EstrategiaPermitidaViewModel();
                        e.IdEstrategia = estrategia.Id;
                        e.IdObjetivo = o.IdObjetivoPermitido;
                        e.NombreEstrategia = estrategia.Nombre;
                        if(estrategiasPermitidas.Any(x=>x.Id == estrategia.Id))
                        {
                            e.Permitida = true;
                        }
                        o.EstrategiasPermitidas.Add(e);
                    }
                    unidadEstrategias.ObjetivosPermitidos.Add(o);
                }

                return unidadEstrategias;
            }
            else
            {
                return null;
            }
        }

        public UnidadEstrategiasPermitidasViewModel Editar(EditarEstrategiasPermitidasViewModel estrategias)
        {
            if(estrategias.IdUnidad != 0 && estrategias.EstrategiasPermitidas != null)
            {
                var estrategiasPermitidas = GetAll().Where(x => x.IdUa == estrategias.IdUnidad);

                foreach (var estrategia in estrategias.EstrategiasPermitidas.Where(x => x.Permitida == true))
                {
                    if (!estrategiasPermitidas.Any(e => e.IdEstrategia == estrategia.IdEstrategia && e.IdUa == estrategias.IdUnidad))
                    {
                        Insert(new Uaestrategia() { IdEstrategia = estrategia.IdEstrategia, IdUa = estrategias.IdUnidad });
                    }
                }
                foreach (var estrategia in estrategias.EstrategiasPermitidas.Where(x => x.Permitida == false))
                {
                    if (estrategiasPermitidas.Any(e => e.IdEstrategia == estrategia.IdEstrategia && e.IdUa == estrategias.IdUnidad))
                    {
                        var temp = GetAll().Where(e => e.IdEstrategia == estrategia.IdEstrategia && e.IdUa == estrategias.IdUnidad).ToList();
                        foreach (var item in temp)
                        {
                            Delete(item);
                        }
                    }
                }
                return GetEstrategiasPermitidasDeUnidad(estrategias.IdUnidad);
            }
            else
            {
                return null;
            }
        }
    }
}
