using System;

namespace WebApiKaeser.Models
{
    public class SeleccionActivoToma
    {
        public Guid AUD_ID { get; set; }
        public string ListaAuditoriaCaptura { get; set; }
        public string ListaActivosSinCaptura { get; set; }
        public string ListaAuditoriaCapturaResponsables { get; set; }
        public string ListaAuditoriaCapturaDetalle { get; set; }
    }
}