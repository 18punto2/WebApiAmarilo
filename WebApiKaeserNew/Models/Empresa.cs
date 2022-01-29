using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKaeser.Models
{
    public class Empresa
    {
       public string EMP_DESC { get; set; }
        public string EMP_DIRECCION { get; set; }
        public string EMP_TELEFONO { get; set; }
        public string EMP_LOGO { get; set; }
        public string EMP_SMTP_REM { get; set; }
        public string EMP_SMTP_NOM_REM { get; set; }
        public string EMP_SMTP_SERVER { get; set; }
        public int EMP_SMTP_ENCRYPT { get; set; }
        public int EMP_SMTP_PORT { get; set; }
        public string EMP_SMTP_USER { get; set; }
        public string EMP_SMTP_PASS { get; set; }
        public string EMP_NOTIFY_AUTH { get; set; }
        public string EMP_NOTIFY_ASIGN { get; set; }
        public string EMP_NOTIFY_INFPREST { get; set; }           
        public string EMP_CORREOS { get; set; }
    }
}