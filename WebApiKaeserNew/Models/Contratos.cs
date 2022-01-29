// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Contratos
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Contratos
  {
    public Guid CON_ID { get; set; }

    public Guid CON_CLI_ID { get; set; }

    public string CLI_IDENTIFICACION { get; set; }

    public string CLI_CODIGO_SAP { get; set; }

    public string CLI_NOMBRE { get; set; }

    public string CON_FECHA_INICIO { get; set; }

    public string CON_FECHA_SUSCRIPCION { get; set; }

    public string CON_FECHA_FINAL { get; set; }

    public string CON_OBSERVACION { get; set; }

    public Guid CON_EST_ID { get; set; }

    public string CON_EST_NOMBRE { get; set; }

    public string CON_DOCUMENTO_SAP { get; set; }

    public string CON_NUMERO_DOC { get; set; }
  }
}
