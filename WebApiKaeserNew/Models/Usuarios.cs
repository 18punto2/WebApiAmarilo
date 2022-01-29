// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Usuarios
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Usuarios
  {
    public Guid USU_ID { get; set; }

    public string USU_NOMBRE { get; set; }

    public string USU_IDENTIFICACION { get; set; }

    public string USU_CORREO { get; set; }

    public Guid USU_ROL_ID { get; set; }

    public string USU_ROL_DESC { get; set; }

    public string USU_LOGIN { get; set; }

    public bool USU_ACTIVE { get; set; }

    public Guid USU_RES_ID { get; set; }

    public string USU_PASS { get; set; }

    public string USU_NOMBRE_COMPLETO { get; set; }

    public Guid USA_ARE_ID { get; set; }
  }
}
