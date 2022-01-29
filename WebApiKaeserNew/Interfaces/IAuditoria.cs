// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Interfaces.IAuditoria
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
  internal interface IAuditoria
  {
    IEnumerable<Estados> Get_list_estadoAuditoria();

    IEnumerable<Auditoria> Get_list_Auditorias(
      Guid? AUD_ID,
      Guid? ESA_ID,
      DateTime? FECHA_INICIAL,
      DateTime? FECHA_FINAL);

    List<Auditoria> Get_list_Auditoria_detalle(
      Guid? AUD_ID,
      ref List<Areas> lstAreas,
      ref List<Responsable> lstResponsable,
      ref List<TipoActivo> lstTipoActivo);

    IEnumerable<Activos> Get_list_PrevisualizacionAuditoria(
      Guid? AUD_ID,
      Guid? AUD_USUARIO_CREATE);

    Mensaje Set_Crear_Detalle_Auditoria(
      List<AuditoriaDetalle> AUDITORIADET,
      Guid? UsuarioAuditoriaDetCrear);

    IEnumerable<Estados> Get_listToma();

    Mensaje Set_Asignar_Auditoria(
      List<Auditoria> AUDITORIAASIG,
      Guid? UsuarioAuditoriaAsignar);

    List<ActivosImagenes> Get_listar_AuditoriaImagenes(
      Guid? AIM_ATO_ID,
      Guid? AIM_ACT_ID);

        List<ActivosImagenes> Get_listar_AuditoriaImagenesV2(
        Guid? AIM_AUD_ID,
        Guid? AIM_ACT_ID);
    Mensaje Set_Crear_Auditoria(
      List<AuditoriaDetalle> AUDITORIA,
      Guid? UsuarioAuditoriaCrear);

    IEnumerable<AuditoriaUsuario> Get_list_ListarAuditoriasPorUsuario(
      Guid ATO_USUARIO_TOMA);

    Mensaje Set_CapturaAuditoria(List<AuditoriaCaptura> AUDITORIA, Guid USUARIO_TOMA);

    Mensaje Set_CrearAuditoriaImagenes(
      List<ActivosImagenes> NuevaActivoImagen,
      Guid UsuarioAuditoriaImgCrear);

    Mensaje Set_Cerrar_Toma(Guid ATO_ID);

    AuditoriaTomaDatos Get_list_TomaDatosPor_Auditoria(Guid? AUD_ID);
    Mensaje Set_Ajuste_Auditoria(Guid AUD_ID, Guid USU_ID);
    Mensaje Set_Liberar_Auditoria(Guid AUD_ID);
    Mensaje Set_Eliminar_Auditoria(Guid? AUD_ID, Guid? AUD_USUARIO_DELETE_ID);
    Mensaje Set_SeleccionarActivos_Toma(SeleccionActivoToma SELECCION);
    }
}
