// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.TipoActivoCaracteristicas
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class TipoActivoCaracteristicas
  {
    public Guid TCA_ID { get; set; }

    public Guid TCA_TAC_ID { get; set; }

    public Guid TCA_CAR_ID { get; set; }

    public string TCA_CAR_NOMBRE { get; set; }

    public bool TCA_ASIGNADO { get; set; }
  }
}
