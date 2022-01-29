// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.TipoActivo
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class TipoActivo
  {
    public Guid TAC_ID { get; set; }

    public string TAC_DESC { get; set; }

    public bool TAC_RETORNABLE { get; set; }

    public bool TAC_ACTIVE { get; set; }

    public bool SELECCIONADO { get; set; }
  }
}
