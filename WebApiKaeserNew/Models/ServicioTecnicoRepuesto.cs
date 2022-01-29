// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.ServicioTecnicoRepuesto
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class ServicioTecnicoRepuesto
  {
    public Guid STR_ID { get; set; }

    public string STR_CODIGO_REPUESTO { get; set; }

    public double STR_CANTIDAD { get; set; }

    public string STR_OBSERVACION { get; set; }

    public string USUARIO_CREA { get; set; }

    public DateTime STR_FECHA_CREA { get; set; }

    public string STR_FECHA_CREA_STR { get; set; }

    public Guid STR_STE_ID { get; set; }
  }
}
