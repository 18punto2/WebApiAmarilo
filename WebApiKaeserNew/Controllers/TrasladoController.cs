// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Controllers.TrasladoController
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
  public class TrasladoController : ApiController
  {
    private static readonly TrasladoDataBase response = new TrasladoDataBase();

    [HttpGet]
    public IEnumerable<Estados> Get_list_TransaccionesTraslado()
    {
      return TrasladoController.response.Get_list_TransaccionesTraslado();
    }

    [HttpPost]
    public Mensaje Set_Crear_Traslado(
      [FromBody] TrasladoActivo NuevaTipoActivo,
      Guid UsuarioTrasladoCrear)
    {
      return TrasladoController.response.Set_Crear_traslado(new List<TrasladoActivo>()
      {
        NuevaTipoActivo
      }, UsuarioTrasladoCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_Traslado(
      [FromBody] IngresoActivo EditarTrasladoActivo,
      Guid UsuarioEditarTraslado)
    {
      return TrasladoController.response.Set_Editar_Traslado(new List<IngresoActivo>()
      {
        EditarTrasladoActivo
      }, UsuarioEditarTraslado);
    }
  }
}
