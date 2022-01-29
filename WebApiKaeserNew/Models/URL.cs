// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.URL
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class URL
  {
    public Guid URL_ID { get; set; }

    public Guid URL_URL_PARENT_ID { get; set; }

    public string URL_DESC { get; set; }

    public string URL_LINK { get; set; }

    public string URL_ICON { get; set; }

    public bool URL_ISACTIVE { get; set; }

    public string URL_PADRE { get; set; }

    public bool URL_PRR_ISACTIVE { get; set; }

    public Guid PUR_ID { get; set; }

    public int PER_ORDER { get; set; }

    public string PER_DESC { get; set; }

    public bool PRR_ISACTIVE { get; set; }

    public Guid PER_ID { get; set; }
  }
}
