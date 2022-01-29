// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.AuditoriaToma
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class AuditoriaToma
  {
    public Guid ALL_ACT_ID { get; set; }
    public string CAR_RES_ARE_DESC { get; set; }
    public string VALOR { get; set; }
    public string TAC_DESC { get; set; }
    public string MOD_DESC { get; set; }
    public string ACT_DESC { get; set; }
    public string ALL_ACT_Serial { get; set; }
    public bool? SELECCIONADO { get; set; }
    public Guid? TOMA1 { get; set; }
    public string VALORRESAREATOMA1 { get; set; }
    public bool? SELECCIONADOTOMA1 { get; set; }
    public Guid? TOMA2 { get; set; }
    public string VALORRESAREATOMA2 { get; set; }
    public bool? SELECCIONADOTOMA2 { get; set; }
    public Guid? TOMADEF { get; set; }
    public string VALORRESAREATOMADEF { get; set; }
    public bool? SELECCIONADOTOMADEF { get; set; }
    public bool? ACP_AJUSTADO { get; set; }
    }
}
