// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.ActivoValoresCaracteristicas
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class ActivoValoresCaracteristicas
  {
    public Guid AVA_ID { get; set; }

    public Guid AVA_TAC_ID { get; set; }

    public string AVA_TAC_DESC { get; set; }

    public bool AVA_TAC_RETORNABLE { get; set; }

    public Guid AVA_MOD_ID { get; set; }

    public string AVA_MOD_DESC { get; set; }

    public Guid AVA_CAR_ID { get; set; }

    public string AVA_CAR_DESC { get; set; }

    public bool AVA_CAR_ESMODIFICABLE { get; set; }

    public bool AVA_CAR_ESLISTA { get; set; }

    public Guid AVA_CAR_CAR_PADRE_ID { get; set; }

    public Guid AVA_CVA_ID { get; set; }

    public string AVA_VALOR { get; set; }
  }
}
