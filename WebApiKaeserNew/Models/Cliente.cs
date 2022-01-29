// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Cliente
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Cliente
  {
    public Guid CLI_ID { get; set; }

    public string CLI_IDENTIFICACION { get; set; }

    public string CLI_CODIGO_SAP { get; set; }

    public string CLI_NOMBRE { get; set; }

    public string CLI_DIRECCION { get; set; }

    public string CLI_CONTACTO_NOMBRE { get; set; }

    public string CLI_CONTACTO_TELEFONO { get; set; }

    public string CLI_CONTACTO_CORREO { get; set; }

    public bool CLI_ACTIVE { get; set; }
  }
}
