using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKaeser.Models
{
    public class Dasboard
    {
        public string EVENTO { get; set; }

        public int HOY { get; set; }

        public int AYER { get; set; }
        public string AREA { get; set; }
        public string FECHA { get; set; }
        public double INGRESO { get; set; }
        public double PROCESO { get; set; }
        public double SALIDA { get; set; }

        public string STATUS { get; set; }
        public bool  RETORNABLE { get; set; }
        public double M2 { get; set; }
    }
}