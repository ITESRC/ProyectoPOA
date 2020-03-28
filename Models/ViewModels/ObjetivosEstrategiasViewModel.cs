using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class ObjetivosEstrategiasViewModel : Repositories.Repository<Objetivo>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Eliminado { get; set; }
        public List<Estrategia> ListaEstregias
        {
            get
            {
                return Context.Estrategia.Where(x => x.IdObjetivo == Id).OrderBy(x => x.Nombre).ToList();
            }

        }

    }
}
