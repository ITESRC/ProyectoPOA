using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class ArticulosViewModel
    {
        public Articulo Articulo { get; set; }
        public IEnumerable<Articulo> ListaArticulos { get; set; }
    }
}
