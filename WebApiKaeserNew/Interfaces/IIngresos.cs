// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Interfaces.IIngresos
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
  internal interface IIngresos
  {
    IEnumerable<Estados> Get_list_TransaccionesEntrada();

    IEnumerable<Estados> Get_list_EstadosTransacciones(Guid? EST_ID);

    IEnumerable<Transaccion> Get_list_Transacciones(
      Guid? TRA_ID,
      Guid? CON_ID,
      Guid? TTR_ID,
      Guid? EST_ID,
      Guid? CLI_ID,
      string TRA_DOCUMENTO_SAP,
      string CON_NUMERO_DOC,
      DateTime? TRA_FECHA_INICIO,
      DateTime? TRA_FECHA_FINAL,
      bool? TTR_ES_SALIDA,
      bool? TTR_ES_ENTRADA,
      bool? TTR_ES_TRASLADO,
      Guid? TRA_AREA_ORIGEN_ID,
      Guid? TRA_AREA_DESTINO_ID,
      string TRA_Doc_Factura,
      bool? TTR_ES_ASIGNACION);

    Mensaje Set_Crear_Entrada(List<IngresoActivo> NuevaTipoActivo, Guid UsuarioIngresoCrear);

    Mensaje Set_Editar_Entrada(
      List<IngresoActivo> EditarIngresoActivo,
      Guid UsuarioEditarCrear);

    Mensaje Set_Autorizar_Transacciones(
      List<IngresoActivo> AutorizarTransaccion,
      Guid UsuarioAutorizarTransaccion);

    Mensaje Set_Crear_DetalleTransacciones(
      List<TransaccionDetalle> TransaccionDetalle,
      Guid UsuarioTransaccionCrear);

    Mensaje Set_Eliminar_DetalleTransacciones(
      List<TransaccionDetalle> TransaccionDetalle,
      Guid UsuarioTransaccionEliminar);

    Mensaje Set_CancelarTransacciones(
      Guid Transaccion,
      Guid UsuarioTransaccionCancelar,
      string URL_LINK);
  }
}
