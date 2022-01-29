// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Transaccion
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Transaccion
  {
    public Guid TRA_ID { get; set; }

    public Guid TRA_TTR_ID { get; set; }

    public string TTR_DESC { get; set; }

    public Guid TRA_CLI_ID { get; set; }

    public string CLI_NOMBRE { get; set; }

    public string TRA_Fecha_Compra { get; set; }

    public Guid EST_ID { get; set; }

    public string EST_DESC { get; set; }

    public string CON_NUMERO_DOC { get; set; }

    public string TRA_DOCUMENTO_SAP { get; set; }

    public string TRA_OBSERVACIONES { get; set; }

    public bool TTR_ES_ENTRADA { get; set; }

    public bool TTR_ES_SALIDA { get; set; }

    public string PRO_NOMBRE { get; set; }

    public string Responsable { get; set; }

    public string TRA_Doc_Factura { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string TRA_Transportador { get; set; }

    public Guid TRA_PRO_ID { get; set; }

    public Guid RES_ID { get; set; }

    public Guid TRA_AREA_ID { get; set; }

    public string ARE_DESC { get; set; }

    public Guid? TRA_CON_ID { get; set; }

    public Decimal TRA_costo { get; set; }

    public string TRA_Fecha_Salida { get; set; }

    public Guid? TRA_MOT_ID { get; set; }

    public string AREA_ORIGEN { get; set; }

    public string AREA_DESTINO { get; set; }

    public string TRA_FECHA_CREATE { get; set; }

    public string USU_LOGIN { get; set; }

    public Guid? ID_ORIGEN { get; set; }

    public Guid? ID_DESTINO { get; set; }

    public DateTime TRA_FECHA_CREATE_DATE { get; set; }

    public string TRA_Fecha_Retorno { get; set; }

    public Guid? TRA_ETA_ID { get; set; }

    public string ETQ_BC { get; set; }

    public string ETQ_CODE { get; set; }

    public string ACT_DESC { get; set; }

    public string ACT_Serial { get; set; }

    public string RES_DOCUMENTO { get; set; }

    public string USU_CORREO { get; set; }

    public DateTime TRA_Fecha_Retorno_date { get; set; }

    public DateTime TRA_Fecha_Estimada_Retorno { get; set; }

    public string TRA_Fecha_Estimada_Retorno_str { get; set; }

    public string TAC_DESC { get; set; }

    public string MOD_DESC { get; set; }

    public string FechaIngreso_str { get; set; }
    }
}
