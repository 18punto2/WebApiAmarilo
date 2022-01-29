// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.CaracteristicaValores
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class CaracteristicaValores
  {
    public Guid CVA_ID { get; set; }

    public Guid CVA_CAR_ID { get; set; }

    public string CVA_NOMBRE { get; set; }

    public Guid? CVA_CVA_PADRE_ID { get; set; }

    public bool CVA_DEFAULT { get; set; }
  }
}
