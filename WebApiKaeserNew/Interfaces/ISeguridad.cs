// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Interfaces.ISeguridad
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
  internal interface ISeguridad
  {
    IEnumerable<Usuarios> Get_validar_usuarios(string Login, string Password);

    List<URL> Get_list_permisosMenu(Guid? ROL_ID);

    List<Rol> Get_list_Roles(Guid? ROL_ID_LISTA, bool? Activos);

    Mensaje Set_Crear_Roles(List<Rol> NuevoRol, Guid Rol_ID_USUARIO);

    Mensaje Set_Editar_Roles(List<Rol> EditarRol, Guid Usuario);

    Mensaje Set_Eliminar_Roles(List<Guid> EliminarRol, Guid Usuario);

    List<URL> Get_list_permisos(Guid? ROL_ID_PERMISO);

    IEnumerable<Usuarios> Get_list_Usuarios(Guid? USU_ID);

    Mensaje Set_Crear_Usuarios(List<Usuarios> NuevoUsuario, Guid UsuarioNuevo);

    Mensaje Set_Editar_Usuarios(List<Usuarios> EditarUsuario, Guid UsuarioEditar);

    Mensaje Set_Eliminar_Usuarios(List<Guid> EliminarUsuario, Guid UsuarioEliminar);

    List<Areas> Get_list_UsuariosArea(Guid? USU_ID_AREA);

    Mensaje Set_Asignar_Area_Usuario(List<UsuarioArea> ListaArea, Guid Area_ID_Asignar);
  }
}
