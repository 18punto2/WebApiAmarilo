// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.CentroCosto
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class CentroCosto
  {
    public Guid CCO_ID { get; set; }

    public string CCO_CODIGO { get; set; }

    public string CCO_DESC { get; set; }

    public bool CCO_ACTIVE { get; set; }

    public string CCO_DESC_ALT { get; set; }
  }
}
