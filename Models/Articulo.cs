using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Articulo
    {
        public int IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public string UnidadDeMedida { get; set; }
        public decimal CostoUnitario { get; set; }
        public string NumeroDePartida { get; set; }
    }
}
