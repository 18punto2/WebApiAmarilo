// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Rol
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Rol
  {
    public Guid ROL_ID { get; set; }

    public string ROL_DESC { get; set; }

    public bool ROL_ACTIVE { get; set; }

    public string SELECCIONADOS { get; set; }

    public string NO_SELECCIONADOS { get; set; }
  }
}
