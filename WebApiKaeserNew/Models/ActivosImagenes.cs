// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.ActivosImagenes
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class ActivosImagenes
  {
    public Guid AIM_ID { get; set; }

    public Guid? AIM_ACT_ID { get; set; }

    public Guid? AIM_TRA_ID { get; set; }

    public string AIM_RUTA { get; set; }

    public string AIM_OBSERVACION { get; set; }

    public bool AIM_ACTIVE { get; set; }
  }
}
