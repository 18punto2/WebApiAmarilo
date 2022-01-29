using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKaeser.Models
{
    public class Garantias
    {
        public string AreaActivo { get; set; }
        public string DescripcionActivo { get; set; }
        public string TipoActivo { get; set; }
        public string Modelo { get; set; }
        public string ParteNumero { get; set; }
        public string Serial { get; set; }
        public string MaquinaPrimaPN { get; set; }
        public string MaquinaPrimaSerial { get; set; }
        public string NumeroFalla { get; set; }
        public string OS { get; set; }
        public string FechaIngreso { get; set; }
        public string CodigoEtiqueta { get; set; }
        public string CodigoBarras { get; set; }
        public string Usuario { get; set; }
        public string FechaSalida { get; set; }
    }
}