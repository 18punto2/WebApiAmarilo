// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.TrasladoActivo
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class TrasladoActivo
  {
    public Guid? TRA_TTR_ID { get; set; }

    public Guid? TRA_MOT_ID { get; set; }

    public Guid? TRA_AREA_ID { get; set; }

    public Guid? TRA_AREA_ORIGEN_ID { get; set; }

    public Guid? TRA_AREA_DESTINO_ID { get; set; }

    public Guid? TRA_USUARIO_CREATE_ID { get; set; }

    public string TRA_DOCUMENTO_SAP { get; set; }

    public string TRA_Doc_Factura { get; set; }

    public Guid? TRA_TRA_ID { get; set; }

    public string TRA_OBSERVACIONES { get; set; }
  }
}
