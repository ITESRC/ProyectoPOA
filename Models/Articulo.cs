using System;
using System.Collections.Generic;

namespace ProyectoPOA.Models
{
    public partial class Articulo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoUnitario { get; set; }
        public bool? Eliminado { get; set; }
        public int Idunidadmedida { get; set; }
        public short Idpartida { get; set; }

        public Partida IdpartidaNavigation { get; set; }
        public Unidadmedida IdunidadmedidaNavigation { get; set; }
    }
}
