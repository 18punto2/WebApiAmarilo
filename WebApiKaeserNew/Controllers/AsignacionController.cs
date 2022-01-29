
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApiKaeser.Factory;
using WebApiKaeser.Models;

namespace WebApiKaeser.Controllers
{
  public class AsignacionController : ApiController
  {
    private static readonly AsignacionDataBase response = new AsignacionDataBase();

    [HttpGet]
    public IEnumerable<Estados> Get_list_TransaccionesAsignacion()
    {
      return AsignacionController.response.Get_list_TransaccionesAsignacion();
    }

    [HttpPost]
    public Mensaje Set_Crear_Asignacion(
      [FromBody] IngresoActivo Transaccion,
      Guid UsuarioAsignacionCrear)
    {
      return AsignacionController.response.Set_Crear_Asignacion(new List<IngresoActivo>()
      {
        Transaccion
      }, UsuarioAsignacionCrear);
    }
  }
}
