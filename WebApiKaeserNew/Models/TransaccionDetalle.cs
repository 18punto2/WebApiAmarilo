// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.TransaccionDetalle
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class TransaccionDetalle
  {
    public Guid DTR_ID { get; set; }

    public Guid ACT_ID { get; set; }

    public string ACT_DESC { get; set; }

    public Guid? TRA_ID { get; set; }

    public string TAC_DESC { get; set; }

    public string MOD_DESC { get; set; }

    public string ETQ_BC { get; set; }

    public string ETQ_CODE { get; set; }

    public string ACT_Serial { get; set; }

    public string EST_DESC { get; set; }

    public string AIM_RUTA { get; set; }

    public Guid? ACT_TAC_ID { get; set; }

    public string ACT_TAC_DESC { get; set; }

    public Guid? ACT_MOD_ID { get; set; }

    public string ACT_MOD_DESC { get; set; }

    public Guid? ACT_EST_ID { get; set; }

    public string ACT_EST_DESC { get; set; }

    public bool ACT_ACTIVE { get; set; }

    public string ACT_ETQ_BC { get; set; }

    public string ACT_ETQ_CODE { get; set; }

    public string ACT_ETQ_EPC { get; set; }

    public string ACT_ETQ_TID { get; set; }

    public bool DTR_RECIBIDO { get; set; }

    public bool DTR_x_RECIBIR { get; set; }
  }
}
