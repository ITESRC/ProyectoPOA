using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class UnidadAdministrativasViewModel
    {
        public int Id { get; set; }
        public int Clave { get; set; }
        public string Encargado { get; set; }
        public string Superior { get; set; }
    }
}
