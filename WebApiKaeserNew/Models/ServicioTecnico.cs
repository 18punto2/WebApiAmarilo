// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.ServicioTecnico
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class ServicioTecnico
  {
    public Guid STE_ID { get; set; }

    public string ACT_DESC { get; set; }

    public Guid? TAC_ID { get; set; }

    public string TAC_DESC { get; set; }

    public Guid? MOD_ID { get; set; }

    public string MOD_DESC { get; set; }

    public string ACT_SERIAL { get; set; }

    public string ARE_DESC { get; set; }

    public string USUARIO_SOLICITA { get; set; }

    public DateTime STE_FECHA_SOLICITUD { get; set; }

    public string STE_FECHA_SOLICITUD_STR { get; set; }

    public string USUARIO_INICIA { get; set; }

    public DateTime STE_FECHA_INICIO { get; set; }

    public string STE_FECHA_INICIO_STR { get; set; }

    public string USUARIO_FINALIZA { get; set; }

    public DateTime STE_FECHA_FINALIZA { get; set; }

    public string STE_FECHA_FINALIZA_STR { get; set; }

    public string ETQ_BC { get; set; }

    public string ETQ_CODE { get; set; }

    public string STE_DESCRIPCION { get; set; }

    public string PRO_NOMBRE { get; set; }

    public string CLI_NOMBRE { get; set; }

    public string STE_CODIGO_SAP { get; set; }

    public bool STE_INTERNO_EXTERNO { get; set; }

    public short TIPO_ACTUALIZACION { get; set; }

    public string STE_OBSERVACION { get; set; }

    public Guid? STE_PRO_ID { get; set; }

    public Guid? STE_CLI_ID { get; set; }

    public Guid STE_TRA_ID { get; set; }

    public Guid STE_ACT_ID { get; set; }

    public Guid STE_ARE_ID { get; set; }
    public Guid ACT_EST_ID { get; set; }
        public Guid STE_ID_ESTADO { get; set; }
    }
}
