// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Controllers.SalidasController
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApiKaeser.Factory;
using WebApiKaeser.Models;

namespace WebApiKaeser.Controllers
{
  public class SalidasController : ApiController
  {
    private static readonly SalidasDataBase response = new SalidasDataBase();

    [HttpGet]
    public IEnumerable<Estados> Get_list_TransaccionesSalida()
    {
      return SalidasController.response.Get_list_TransaccionesSalida();
    }

    [HttpPost]
    public Mensaje Set_Crear_Salida([FromBody] IngresoActivo NuevaTipoActivo, Guid UsuarioSalidaCrear)
    {
      return SalidasController.response.Set_Crear_Salida(new List<IngresoActivo>()
      {
        NuevaTipoActivo
      }, UsuarioSalidaCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_Salida(
      [FromBody] IngresoActivo EditarSalidaActivo,
      Guid UsuarioEditarSalida)
    {
      return SalidasController.response.Set_Editar_Salida(new List<IngresoActivo>()
      {
        EditarSalidaActivo
      }, UsuarioEditarSalida);
    }

    [HttpGet]
    public IEnumerable<Estados> Get_list_Motivo(Guid? MOTIVO_ID)
    {
      return SalidasController.response.Get_list_Motivo(MOTIVO_ID);
    }
  }
}
