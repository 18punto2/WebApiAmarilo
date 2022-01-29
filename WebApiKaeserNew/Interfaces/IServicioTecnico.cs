// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Interfaces.IServicioTecnico
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
  internal interface IServicioTecnico
  {
    List<Estados> Get_list_ServicioTecnicoEstado();

    List<Estados> Get_list_ServicioTecnicoProceso();

    List<ServicioTecnico> Get_list_ServicioTecnico(
      Guid? STE_USUARIO_SOLICITA,
      Guid? STE_STE_ID,
      DateTime? FECHA_INICIO,
      DateTime? FECHA_FIN,
      short? PARAMETROCONSULTAFECHA,
      Guid? STE_ID,
      Guid? STE_TRA_ID,
      Guid? STE_ACT_ID);

    List<ServicioTecnicoIncidencia> Get_list_ServicioTecnicoIncidencia(
      Guid STI_STE_ID);

    List<ServicioTecnicoImagen> Get_list_ServicioTecnicoImagen(
      Guid STM_STI_ID);

    List<ServicioTecnicoRepuesto> Get_list_ServicioTecnicoRepuesto(
      Guid STR_STE_ID);

    Mensaje Set_ActualizarServicioTecnicoCambioEstado(
      List<ServicioTecnico> ServicioTecnico,
      Guid UsuarioCambioEstado);

    Mensaje Set_EliminarServicioTecnico(List<Guid> STE_ID,Guid STE_USU_ID);

    Mensaje Set_CrearServicioTecnicov2(
      List<ServicioTecnico> ServicioTecnico,
      Guid UsuarioCrearServicio);

    Mensaje Set_CrearServicioTecnicoIncidencia(
      List<ServicioTecnicoIncidencia> Incidencia,
      Guid UsuarioCrearServicioIncidencia);

    Mensaje Set_EliminarServicioTecnicoIncidente(
      List<Guid> Incidencia,
      Guid UsuarioModificaServicioIncidencia);

    Mensaje Set_CrearServicioTecnicoImagenes(
      List<ServicioTecnicoImagen> ServicioImagen,
      Guid UsuarioCrearServicioImagen);

    Mensaje Set_EliminarServicioTecnicoImagen(
      List<Guid> Imagen,
      Guid UsuarioEliminaServicioImagen);

    Mensaje Set_CrearServicioTecnicoRepuesto(
      List<ServicioTecnicoRepuesto> Repuesto,
      Guid USUARIO_REPUESTO_CREA);

    Mensaje Set_EliminarServicioTecnicoRepuesto(List<Guid> STM_ID, Guid STR_USUARIO_MODIFICA);
    Mensaje Get_Validar_Activo_ServicioTecnico (string ETQ_BC);
  }
}
