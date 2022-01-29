using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKaeser.Models
{
    public class ActivosCambio
    {
        public DateTime LEA_FECHA_MOD { get; set; }
        public string LEA_FECHA_MOD_STR { get; set; }
        public Guid LEA_ACT_ID { get; set; }
        public string LEA_CAR_DESC { get; set; }
        public string LEA_AVA_VALOR_OLD { get; set; }
        public string LEA_AVA_VALOR_NEW { get; set; }
        public string LEA_USU_LOGIN { get; set; }
    }
}