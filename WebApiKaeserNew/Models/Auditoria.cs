// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Auditoria
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Auditoria
  {
    public Guid? AUD_ID { get; set; }

    public string AUD_OBSERVACION { get; set; }

    public string USU_LOGIN { get; set; }

    public DateTime AUD_FECHA_CREATE { get; set; }

    public string AUD_FECHA_CREATE_STR { get; set; }

    public DateTime AUD_FECHA_EJECUCION { get; set; }

    public string AUD_FECHA_EJECUCION_STR { get; set; }

    public Guid? ESA_ID { get; set; }

    public string ESA_DESC { get; set; }

    public Guid? AUD_USUARIO_CREATE { get; set; }

    public string LISTATOMAUSUARIO { get; set; }
  }
}
