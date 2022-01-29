// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Responsable
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Responsable
  {
    public Guid RES_ID { get; set; }

    public string RES_DOCUMENTO { get; set; }

    public string RES_NOMBRES { get; set; }

    public string RES_APELLIDOS { get; set; }

    public string RES_CARGO { get; set; }

    public string RES_CORREO { get; set; }

    public string RES_PHOTO_1 { get; set; }

    public string RES_CELULAR { get; set; }

    public string RES_TELEFONO { get; set; }

    public string RES_EXT { get; set; }

    public string RES_OBSERVACION { get; set; }

    public bool RES_ACTIVE { get; set; }

    public bool SELECCIONADO { get; set; }
  }
}
