// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.AuditoriaUsuario
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class AuditoriaUsuario
  {
    public Guid? ATO_ID { get; set; }

    public Guid? ATO_AUD_ID { get; set; }

    public short ATO_ORDEN { get; set; }

    public Guid? TOM_ID { get; set; }

    public string TOM_DESCRIPCION { get; set; }

    public string AUD_OBSERVACION { get; set; }

    public string AUD_USUARIO_CREATE { get; set; }

    public DateTime? AUD_FECHA_EJECUCION { get; set; }
        public string AUD_FECHA_EJECUCION_STR { get; set; }
        public string AET_DESCRIPCION { get; set; }
  }
}
