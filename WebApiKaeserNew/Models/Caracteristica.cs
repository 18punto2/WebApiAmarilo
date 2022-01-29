// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Caracteristica
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Caracteristica
  {
    public Guid CAR_ID { get; set; }

    public string CAR_DESC { get; set; }

    public bool CAR_ESMODIFICABLE { get; set; }

    public Guid? CAR_CAR_PADRE_ID { get; set; }

    public string CAR_DESC_PADRE { get; set; }

    public bool CAR_FLAGPADRE { get; set; }
  }
}
