// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Interfaces.ITransaccion
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
  internal interface ITransaccion
  {
    List<Activos> Get_list_Activos(Guid? Activo_ID, string ETQ, Guid? ACT_RES_ID,DateTime ? FECHA_MOD);

    Mensaje Get_validar_etiquetas(string ETQ_BC, string ETQ_CODE);

    Mensaje Get_Validar_DetalleTransacciones(
      Guid DTR_TRA_ID,
      string ETQ,
      Guid DTR_USUARIO_CREATE_ID);

    Mensaje Set_Crear_Activos(List<Activos> NuevaActivo, Guid UsuarioActivoCrear);

    Mensaje Set_Editar_Activos(List<Activos> EditarActivo, Guid UsuarioActivoEditar);

    Mensaje Set_Reasignar_Etiqueta(List<Activos> Reasignacion, Guid ETA_USUARIO_MOD_ID);

    Mensaje Set_Eliminar_Activos(List<Guid> EliminarActivo, Guid UsuarioActivoEliminar);

    List<ActivosImagenes> Get_list_ActivosImagenes(
      Guid? AIM_ACT_ID,
      Guid? AIM_TRA_ID,
      Guid? AIM_ID);

    Mensaje Set_Crear_ActivosImagenes(
      List<ActivosImagenes> NuevaActivoImagen,
      Guid UsuarioActivoImgCrear);

    Mensaje Set_Editar_ActivosImagenes(
      List<ActivosImagenes> EditarActivoimagen,
      Guid UsuarioActivoImgEditar);

    Mensaje Set_Eliminar_ActivosImagenes(
      List<Guid> EliminarActivoImg,
      Guid UsuarioActivoImgEliminar);

    IEnumerable<Contratos> Get_list_Contratos(
      Guid? CLI_ID,
      Guid? EST_ID,
      DateTime? CON_FECHA_SUSCRIPCION,
      DateTime? CON_FECHA_INICIO,
      DateTime? CON_FECHA_FINAL,
      string CON_NUMERO_DOC,
      string CON_DOCUMENTO_SAP,
      Guid? CON_ID);

    Mensaje Set_Crear_Contratos(List<Contratos> NuevoContrato, Guid UsuarioContratoCrear);

    Mensaje Set_Editar_Contratos(List<Contratos> EditarContrato, Guid UsuarioContratoEditar);

    Mensaje Set_Eliminar_Contratos(List<Guid> EliminarContrato, Guid UsuarioContratoEliminar);

    IEnumerable<Estados> Get_list_EstadosContratos(Guid? EST_ID);

    IEnumerable<TransaccionDetalle> Get_list_DetalleTransacciones(
      Guid? TRA_ID);

    IEnumerable<Modelo> Get_list_ModeloxTipoActivo(Guid? MOD_TAC_ID);
    List<Transaccion> Get_list_Activos_Transacciones(Guid? ACT_ID);
    List<ActivosCambio> Get_list_Activos_Cambios(Guid? ACT_ID);

        IEnumerable<Transaccion> Get_list_TransaccionesContrato(
      Guid? CON_ID);
    }
}
