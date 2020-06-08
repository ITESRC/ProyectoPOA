using ProyectoPOA.Models;
using ProyectoPOA.Models.ViewModels;
using ProyectoPOA.Models.ViewModels.UaPartidasCapitulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Repositories
{
    public class PermitirPartidasRepository : Repository<Uapartidas>
    {
        public IEnumerable<SuperiorViewModel> GetSuperiores()
        {
            UnidadAdministrativaRepository repos = new UnidadAdministrativaRepository();
            return repos.GetAll().OrderBy(x => x.Nombre).Where(x => x.IdUnidadSuperior == null && x.Eliminado == false).Select(x => new SuperiorViewModel { Id = x.Id, Nombre = x.Nombre });
        }
        public UnidadPartidasPermitidasViewModel GetPartidasPermitidas(int idUA)
        {
            //var capsByUa = Context.Uapartidas.FirstOrDefault(x => x.IdUa == idUA);
            //var _PartXCaps = Context.Partida.FirstOrDefault(x => x.Clave == capsByUa.IdPartida);
            //var _past=
            //return Context.Uapartidas.Where(x=>x.IdUa==idUA).Select(x => x.IdPartidaNavigation.CapituloNavigation);
            UnidadPartidasPermitidasViewModel unidadPartidas;
            var unidad = Context.Unidadadministrativa.Where(x => x.Id == idUA).FirstOrDefault();
            if (unidad != null)
            {
                unidadPartidas = new UnidadPartidasPermitidasViewModel
                {
                    IdUnidad = idUA,
                    NombreUnidad = unidad.Nombre,
                    CapitulosPermitidos = new List<CapituloPermitidoViewModel>()
                };
                var CapitulosPermitidos = Context.Uapartidas.Where(x => x.IdUa == idUA).Select(x => x.IdPartidaNavigation.CapituloNavigation).Distinct();
                foreach (var capitulo in CapitulosPermitidos)
                {
                    CapituloPermitidoViewModel C = new CapituloPermitidoViewModel
                    {
                        IdCapituloPermitido = capitulo.Id,
                        NombreCapitulo = capitulo.Nombre,
                        PartidasPermitidas = new List<PartidaPermitidaViewModel>()
                    };

                    var PartidasPermitidas = Context.Uapartidas.Where(x => x.IdPartidaNavigation.Capitulo == C.IdCapituloPermitido && x.IdUa == idUA).Select(x => x.IdPartidaNavigation).ToList();
                    foreach (var Partida in PartidasPermitidas)
                    {
                        PartidaPermitidaViewModel P = new PartidaPermitidaViewModel
                        {
                            IdPartida = Partida.Capitulo,
                            NombrePartida = Partida.Concepto,
                            IdCapitulo = C.IdCapituloPermitido
                        };
                        C.PartidasPermitidas.Add(P);
                    }
                    unidadPartidas.CapitulosPermitidos.Add(C);
                }
                return unidadPartidas;
            }
            else
            {
                return null;
            }
        }
    }
}
