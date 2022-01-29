// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Interfaces.ISalidas
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
  internal interface ISalidas
  {
    IEnumerable<Estados> Get_list_TransaccionesSalida();

    IEnumerable<Estados> Get_list_Motivo(Guid? MOTIVO_ID);

    Mensaje Set_Crear_Salida(List<IngresoActivo> NuevaTipoActivo, Guid UsuarioIngresoCrear);

    Mensaje Set_Editar_Salida(
      List<IngresoActivo> EditarSalidaActivo,
      Guid UsuarioEditarSalida);
  }
}
