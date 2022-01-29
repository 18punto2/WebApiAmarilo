// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.AuditoriaCaptura
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class AuditoriaCaptura
  {
    public Guid? CAP_ATO_ID { get; set; }
        public Guid? ACP_ETA_ID { get; set; }

        public string ETIQUETA { get; set; }

    public string LISTACARACTERISTICAVALOR { get; set; }

    public DateTime? ACP_FECHA_CAPTURA { get; set; }

    public Guid? ACP_ARE_ID { get; set; }

    public Guid? ACP_RES_ID { get; set; }
  }
}
