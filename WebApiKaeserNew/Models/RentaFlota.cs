using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKaeser.Models
{
    public class RentaFlota
    {
        public string EMR { get; set; }
        public string CodigoSAP{ get; set; }
        public string DescripcionActivo { get; set; }
        public string Serial { get; set; }
        public string POTENCIAHP { get; set; }
        public string CFM { get; set; }
        public string PRESIONPSI { get; set; }
        public string ParteNumero { get; set; }
        public string AnioFabricacion { get; set; }
        public string SecadorIncluido { get; set; }
        public string CanonMinimoRentaDiaria { get; set; }
        public string CanonMinimoArrendamientoMensual { get; set; }
        public string PrecioVenta { get; set; }
        public string IVA { get; set; }
        public string Ciudad { get; set; }
        public string CLI_NOMBRE { get; set; }
        public string CLI_IDENTIFICACION { get; set; }
        public string Asesor { get; set; }
        public string Condiciones { get; set; }
        public string TRA_FECHA_AUTORIZA { get; set; }
        public string TRA_Fecha_Estimada_Retorno { get; set; }
        public string UltimoPeriodoFacturado{ get; set; }
        public string TiempoEquipoSitio { get; set; }
        public string EstadoContrato { get; set; }
        public string EstadoOtroSi { get; set; }
        public string INCREMENTOCANON { get; set; }
        public string HORASTRABAJO { get; set; }
    }
}