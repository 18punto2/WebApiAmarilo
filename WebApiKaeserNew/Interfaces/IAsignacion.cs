
using System;
using System.Collections.Generic;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
  internal interface IAsignacion
  {
    IEnumerable<Estados> Get_list_TransaccionesAsignacion();

    Mensaje Set_Crear_Asignacion(
      List<IngresoActivo> NuevaTipoActivo,
      Guid UsuarioAsignacionCrear);
  }
}
