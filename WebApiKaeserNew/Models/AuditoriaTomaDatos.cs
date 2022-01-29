using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKaeser.Models
{
    public class AuditoriaTomaDatos
    {
        public List<AuditoriaToma> Activos { get; set; }
        public List<AuditoriaToma> Areas { get; set; }

        public List<AuditoriaToma> Responsables { get; set; }

        public List<AuditoriaToma> Caracteristicas { get; set; }
    }
}