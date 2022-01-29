// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Interfaces.Ibase
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
  internal interface Ibase
  {
    IEnumerable<Caracteristica> Get_list_caracteristica(
      Guid? Caracteristica);

    IEnumerable<Caracteristica> Get_list_caracteristicaHija(
      Guid? CaracteristicaPadre);

    Mensaje Set_Crear_Caracteristica([FromBody] List<Caracteristica> NuevaCaracteristica, Guid Usuario);

    Mensaje Set_Editar_Caracteristica(
      [FromBody] List<Caracteristica> EditarCaracteristica,
      Guid Usuario);

    Mensaje Set_Eliminar_Caracteristica([FromBody] List<Guid> EliminarCaracteristica, Guid Usuario);

    IEnumerable<CaracteristicaValores> Get_list_caracteristicaValor(
      Guid? CaracteristicaID,
      Guid? CaracteristicaValor);

    Mensaje Set_Crear_CaracteristicaValor(
      [FromBody] List<CaracteristicaValores> NuevaCaracteristicaValor,
      Guid Usuario);

    Mensaje Set_Editar_CaracteristicaValor(
      [FromBody] List<CaracteristicaValores> EditarCaracteristicaValor,
      Guid Usuario);

    Mensaje Set_Eliminar_CaracteristicaValor(
      [FromBody] List<Guid> EliminarCaracteristicaValor,
      Guid Usuario);

    List<Areas> Get_list_Area(Guid? Area, Guid? USU_ID, Guid? RES_ID);

    Mensaje Set_Crear_Area([FromBody] List<Areas> NuevaArea, Guid Usuario);

    Mensaje Set_Editar_Area([FromBody] List<Areas> EditarArea, Guid Usuario);

    Mensaje Set_Eliminar_Area([FromBody] List<Guid> EliminarArea, Guid Usuario);

    IEnumerable<TipoActivo> Get_list_TipoActivo_Area(Guid? ID_AreaTipoActivo);

    IEnumerable<TipoArea> Get_list_TipoArea(string TipoArea);

    Mensaje Set_Mant_Activos(List<TipoActivoArea> MantTipoActivo, Guid Usuario);

    IEnumerable<ActivoValoresCaracteristicas> Get_list_activosValor(
      Guid MOD_ID,
      Guid? ACT_ID);

    IEnumerable<Estados> Get_list_Estados(Guid? EST_ID);
        IEnumerable<Estados> Get_list_EstadosDisponibilidad();
        IEnumerable<Modelo> Get_list_Modelo(Guid? Modelos, DateTime? MOD_FECHA_MOD);

    Mensaje Set_Crear_Modelo(List<Modelo> NuevaModelo, Guid Usuario);

    Mensaje Set_Editar_Modelo(List<Modelo> EditarModelo, Guid Usuario);

    Mensaje Set_Eliminar_Modelo(List<Guid> EliminarModelo, Guid Usuario);

    IEnumerable<ModeloValoresCaracteristicas> Get_list_ModeloValoresCaracteristicas(
      Guid TipoActivoID,
      Guid? ModeloID);

    IEnumerable<TipoActivo> Get_list_TipoActivo(Guid? TipoActivo, DateTime? TAC_FECHA_MOD);

    Mensaje Set_Crear_TipoActivo(List<TipoActivo> NuevaTipoActivo, Guid Usuario);

    Mensaje Set_Editar_TipoActivo(List<TipoActivo> EditarTipoActivo, Guid Usuario);

    Mensaje Set_Eliminar_TipoActivo(List<Guid> EliminarTipoActivo, Guid Usuario);

    List<TipoActivoCaracteristicas> Get_list_TipoActivoCaracteristica(
      Guid TipoActivoCaracteristica,
      bool Todo);

    Mensaje Set_Asociar_TipoActivoCaracteristicas(
      List<TipoActivoCaracteristicas> ListaTipoActivoCarateristica,
      Guid TCA_TAC_ID,
      Guid UsuarioTipoActivoCrear);

    IEnumerable<Responsable> Get_list_Responsable(
      Guid? responsable,
      string Buscar,
      Guid? Area_ID);

        IEnumerable<Responsable> Get_list_ResponsableLista(
      Guid? responsable,
      string Buscar,
      Guid? Area_ID);
        Mensaje Set_Crear_Responsable(List<Responsable> Nuevaresponsable, Guid Usuario);

    Mensaje Set_Editar_Responsable(List<Responsable> Editarresponsable, Guid Usuario);

    Mensaje Set_Eliminar_Responsable(List<Guid> Eliminarresponsable, Guid Usuario);

    IEnumerable<AreaResponsable> Get_list_ResponsablesAreas(
      Guid? Responsable,
      Guid? Area_ID);

    Mensaje Set_Mant_AreaResponsable(List<AreaResponsable> MantTipoActivo, Guid Usuario);

    IEnumerable<Cliente> Get_list_Clientes(Guid? CLI_ID, string CLI_DESC, DateTime? CLI_FECHA_MOD);
    
    Mensaje Set_Crear_Clientes(List<Cliente> NuevoCliente, Guid UsuarioCliente);

    Mensaje Set_Editar_Clientes(List<Cliente> EditarCliente, Guid UsuarioClienteEditar);

    Mensaje Set_Eliminar_Clientes(List<Guid> EliminarCliente, Guid UsuarioClienteEliminar);

    IEnumerable<Proveedor> Get_list_Proveedor(Guid? PRO_ID, string PRO_DESC);

    Mensaje Set_Crear_Proveedor(List<Proveedor> NuevoProveedor, Guid UsuarioProveedorCrear);

    Mensaje Set_Editar_Proveedor(
      List<Proveedor> EditarProveedor,
      Guid UsuarioProveedorEditar);

    Mensaje Set_Eliminar_Proveedor(
      List<Guid> EliminarProveedor,
      Guid UsuarioProveedorEliminar);

    IEnumerable<CentroCosto> Get_list_CentroCosto(
      Guid? CentroCosto_ID,
      string CentroCosto_DESC);

    Mensaje Set_Crear_CentroCosto(
      List<CentroCosto> NuevoCentroCosto,
      Guid UsuarioCentroCostoCrear);

    Mensaje Set_Editar_CentroCosto(
      List<CentroCosto> EditaCentroCosto,
      Guid UsuarioCentroCostoEditar);

    Mensaje Set_Eliminar_CentroCosto(
      List<Guid> EliminarCentroCosto,
      Guid UsuarioCentroCostoEliminar);
  }
}
