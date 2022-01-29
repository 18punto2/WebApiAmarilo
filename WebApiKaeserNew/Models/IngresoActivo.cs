// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.IngresoActivo
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class IngresoActivo
  {
    public Guid? TRA_ID { get; set; }

    public Guid? TRA_TTR_ID { get; set; }

    public Guid? TRA_MOT_ID { get; set; }

    public Guid? TRA_AREA_ID { get; set; }

    public Guid? TRA_AREA_ORIGEN_ID { get; set; }

    public Guid? TRA_AREA_DESTINO_ID { get; set; }

    public Guid? TRA_EST_ID { get; set; }

    public Guid? TRA_ETA_ID { get; set; }

    public Guid? TRA_RES_ID { get; set; }

    public Guid? TRA_CLI_ID { get; set; }

    public Guid? TRA_CON_ID { get; set; }

    public Guid? TRA_USUARIO_CREATE_ID { get; set; }

    public Guid? TRA_PRO_ID { get; set; }

    public string TRA_Fecha_Compra { get; set; }

    public Decimal? TRA_Costo { get; set; }

    public string TRA_DOCUMENTO_SAP { get; set; }

    public string TRA_Doc_Factura { get; set; }

    public string TRA_OBSERVACIONES { get; set; }

    public string TRA_Transportador { get; set; }

    public string URL_LINK { get; set; }

    public string TRA_Fecha_salida { get; set; }

    public string TRA_Fecha_Retorno { get; set; }

    public string TRA_Fecha_Estimada_Retorno { get; set; }
  }
}
