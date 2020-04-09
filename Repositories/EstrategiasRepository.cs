using ProyectoPOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Repositories
{
    public class EstrategiasRepository : Repository<Estrategia>
    {
        public bool DesHabEstrategias(int[] estrategias)
        {
            for (int i = 0; i < estrategias.Length; i++)
            {
                var Habilitar = Context.Estrategia.FirstOrDefault(x => x.Id == estrategias[i]);
                if (Habilitar != null)
                {
                    if (Habilitar.Eliminado == false)
                    {
                        Habilitar.Eliminado = true;
                        Update(Habilitar);
                        return true;
                    }
                    else
                    {
                        Habilitar.Eliminado = false;
                        Update(Habilitar);
                        return false;
                    }
                }
            }
            return false;
        }
        public IEnumerable<Estrategia> GetEstrategiasByObjetivo(int IdObjetivo)
        {
            var estra = Context.Estrategia.Where(x => x.IdObjetivo == IdObjetivo);
            return estra;
        }

    }
}
