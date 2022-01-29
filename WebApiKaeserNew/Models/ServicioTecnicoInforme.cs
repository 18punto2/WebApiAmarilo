using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKaeser.Models
{
    public class ServicioTecnicoInforme
    {
    public string NumeroOrdenTrabajo { get; set; }
    public string CodigoEtiqueta { get; set; }
    public string DescripcionActivo { get; set; }
    public string TipoActivo { get; set; }
    public string Modelo { get; set; }
    public string Serial { get; set; }
    public string ClienteProveedor { get; set; }
    public string UsuarioSolicita { get; set; }
    public string UsuarioInicia { get; set; }
    public string UsuarioFinaliza { get; set; }
    public string M2 { get; set; }
    public string RepuestoUtilizado { get; set; }
    public string TiempoInicioServicio{ get; set; }
    public string TiempoServicio { get; set; }
    public string FechaSolicitud { get; set; }
    public string FechaInicial{ get; set; }
    public string FechaFinalizacion { get; set; }
    public string TipoTrabajo { get; set; }
    public string Estado { get; set; }
    }
}