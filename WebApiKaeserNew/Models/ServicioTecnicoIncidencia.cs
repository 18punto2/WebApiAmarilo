// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.ServicioTecnicoIncidencia
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class ServicioTecnicoIncidencia
  {
    public Guid STI_ID { get; set; }

    public string STP_DESCRIPCION { get; set; }

    public string STI_OBSERVACION { get; set; }

    public string USUARIO_CREA { get; set; }

    public DateTime STI_FECHA_CREA { get; set; }

    public string STI_FECHA_CREA_STR { get; set; }

    public Guid STI_STE_ID { get; set; }

    public Guid STI_STP_ID { get; set; }
  }
}
