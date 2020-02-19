﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Models.ViewModels
{
    public class UnidadAdministrativasViewModel
    {
        public int Id { get; set; }
       
       
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreEncargado { get; set; }
        public string NombreSuperior { get; set; }
    }
}
