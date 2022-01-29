// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.AreaResponsable
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class AreaResponsable
  {
    public Guid RAR_ID { get; set; }

    public Guid RES_ID { get; set; }

    public Guid RAR_ARE_ID { get; set; }

    public string RESPONSABLE { get; set; }

    public bool SELECCIONADO { get; set; }
  }
}
