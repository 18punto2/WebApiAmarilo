// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Estados
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Estados
  {
    public Guid EST_ID { get; set; }

    public string EST_DESC { get; set; }

    public bool EST_ACTIVE { get; set; }

    public int ESA_ORDER { get; set; }
  }
}
