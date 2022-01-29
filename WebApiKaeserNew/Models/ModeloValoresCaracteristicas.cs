// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.ModeloValoresCaracteristicas
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class ModeloValoresCaracteristicas
  {
    public Guid TAM_ID { get; set; }

    public Guid TAM_TCA_ID { get; set; }

    public Guid TAM_MOD_ID { get; set; }

    public string TAM_VALOR { get; set; }

    public string TAM_CARACTERISTICA { get; set; }

    public bool TAM_ISLIST { get; set; }

    public Guid TAM_CAR_ID { get; set; }

    public Guid TAM_CVA_ID { get; set; }

    public bool CAR_ESMODIFICABLE { get; set; }
  }
}
