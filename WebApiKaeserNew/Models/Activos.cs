using System;

namespace WebApiKaeser.Models
{
  public class Activos
  {
    public Guid ACT_ID { get; set; }

    public string ACT_DESC { get; set; }

    public Guid ACT_TAC_ID { get; set; }

    public string ACT_TAC_DESC { get; set; }

    public Guid ACT_MOD_ID { get; set; }

    public string ACT_MOD_DESC { get; set; }

    public Guid ACT_EST_ID { get; set; }

    public string ACT_EST_DESC { get; set; }

    public string ACT_SERIAL { get; set; }

    public bool ACT_ACTIVE { get; set; }

    public string ACT_ETQ_BC { get; set; }

    public string ACT_ETQ_CODE { get; set; }

    public string ACT_ETQ_EPC { get; set; }

    public string ACT_ETQ_TID { get; set; }

    public string SELECCIONADOS { get; set; }

    public Guid ETA_ID { get; set; }

    public string ARE_DESC { get; set; }
        public string RES_NOMBRE_APELLIDO { get; set; }
        public Guid ARE_ID { get; set; }
        public Guid RES_ID { get; set; }
        public string PARTE_NUMBER { get; set; }
        public string SERIAL { get; set; }
    }
}

