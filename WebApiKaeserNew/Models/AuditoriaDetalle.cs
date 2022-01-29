// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.AuditoriaDetalle
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class AuditoriaDetalle
  {
    public Guid? NUMEROAUDITORIA { get; set; }

    public string LISTATIPOACTIVO { get; set; }

    public string LISTAAREA { get; set; }

    public string LISTARESPONSABLE { get; set; }

    public bool VALIDARLISTATIPODATOACTIVO { get; set; }

    public bool VALIDARLISTARESPONSABLE { get; set; }

    public string AUD_FECHA_EJECUCION { get; set; }

    public string AUD_OBSERVACION { get; set; }
  }
}
