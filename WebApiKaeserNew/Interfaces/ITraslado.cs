// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Interfaces.ITraslado
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
  internal interface ITraslado
  {
    IEnumerable<Estados> Get_list_TransaccionesTraslado();

    Mensaje Set_Crear_traslado(List<TrasladoActivo> NuevoActivo, Guid UsuarioTrasladoCrear);

    Mensaje Set_Editar_Traslado(
      List<IngresoActivo> EditarTrasladoActivo,
      Guid UsuarioEditarTraslado);
  }
}
