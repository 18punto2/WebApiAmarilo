using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKaeser.Models
{
    public class ActivoDisponible
    {
        public Guid? ACT_ID { get; set; }
        public string ACT_DESC { get; set; }
        public Guid? ACT_TAC_ID { get; set; }
        public string TAC_DESC{ get; set; }
        public Guid? ACT_MOD_ID { get; set; }
        public string MOD_DESC{ get; set; }
        public Guid? ACT_EST_ID { get; set; }
        public string EST_DESC{ get; set; }
        public string ACT_Serial { get; set; }
        public bool ACT_ACTIVE { get; set; }
        public string ETQ_BC { get; set; }
        public string ETQ_CODE { get; set; }
        public string ETQ_EPC { get; set; }
        public Guid? ETQ_TID { get; set; }
        public Guid? RES_ID { get; set; }
        public string RES_DOCUMENTO { get; set; }
        public string RES_NOMBRES{ get; set; }
        public string RES_APELLIDOS{ get; set; }
        public Guid? ETA_ID { get; set; }
        public string RES_NOMBRE_APELLIDO{ get; set; }
        public Guid? ARE_ID { get; set; }
        public string ARE_DESC{ get; set; }
        //public Guid? ETA_ID { get; set; }
        public Guid? EDA_ID { get; set; }
        public string EDA_DESC{ get; set; }
        public string P_N { get; set; }
        public string S_N { get; set; }
}
}