// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Menus
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;

namespace WebApiKaeser.Models
{
  public class Menus
  {
    public Guid id { get; set; }

    public string text { get; set; }

    public string URL { get; set; }

    public Nodos state { get; set; }

    public bool Seleccionado { get; set; }

    public List<Menus> children { get; set; }
  }
}
