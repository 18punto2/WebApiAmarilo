// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.AuditoriaConsultaDetalle
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System.Collections.Generic;

namespace WebApiKaeser.Models
{
  public class AuditoriaConsultaDetalle
  {
    public List<Auditoria> cabecera { get; set; }

    public List<Menus> areas { get; set; }

    public List<Responsable> responsable { get; set; }

    public List<TipoActivo> tipoactivo { get; set; }
    public List<Menus> areasSeleccionadas { get; set; }
    }
}
