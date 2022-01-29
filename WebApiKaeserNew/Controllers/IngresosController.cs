// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Controllers.IngresosController
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
  public class IngresosController : ApiController
  {
    private static readonly IngresoDataBase response = new IngresoDataBase();

    [HttpGet]
    public IEnumerable<Estados> Get_list_tipo_Ingreso()
    {
      return IngresosController.response.Get_list_TransaccionesEntrada();
    }

    [HttpGet]
    public IEnumerable<Estados> Get_list_EstadosTransacciones(Guid? EST_ID)
    {
      return IngresosController.response.Get_list_EstadosTransacciones(EST_ID);
    }

    [HttpGet]
    public IEnumerable<Transaccion> Get_list_Transacciones(
      Guid? TRA_ID,
      Guid? CON_ID,
      Guid? TTR_ID,
      Guid? EST_ID,
      Guid? CLI_ID,
      string TIPODOC,
      string CON_DOCUMENTO,
      string TRA_FECHA_INICIO,
      string TRA_FECHA_FINAL,
      string INGRESO,
      string TRA_Doc_Factura,
      Guid? TRA_AREA_ORIGEN_ID,
      Guid? TRA_AREA_DESTINO_ID)
    {
      WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
      string CON_NUMERO_DOC = (string) null;
      string TRA_DOCUMENTO_SAP = (string) null;
      bool? TTR_ES_SALIDA = new bool?();
      bool? TTR_ES_ENTRADA = new bool?();
      bool? TTR_ES_TRASLADO = new bool?();
      bool? TTR_ES_ASIGNACION = new bool?();
      DateTime? TRA_FECHA_INICIO1 = helper.Fecha(TRA_FECHA_INICIO);
      DateTime? TRA_FECHA_FINAL1 = helper.Fecha(TRA_FECHA_FINAL);
      if (TIPODOC == "1")
        CON_NUMERO_DOC = CON_DOCUMENTO;
      else if (TIPODOC == "2")
        TRA_DOCUMENTO_SAP = CON_DOCUMENTO;
      if (INGRESO.Equals("1"))
        TTR_ES_ENTRADA = new bool?(true);
      else if (INGRESO.Equals("2"))
        TTR_ES_SALIDA = new bool?(true);
      else if (INGRESO.Equals("3"))
        TTR_ES_TRASLADO = new bool?(true);
      else if (INGRESO.Equals("4"))
        TTR_ES_ASIGNACION = new bool?(true);
      return IngresosController.response.Get_list_Transacciones(TRA_ID, CON_ID, TTR_ID, EST_ID, CLI_ID, TRA_DOCUMENTO_SAP, CON_NUMERO_DOC, TRA_FECHA_INICIO1, TRA_FECHA_FINAL1, TTR_ES_SALIDA, TTR_ES_ENTRADA, TTR_ES_TRASLADO, TRA_AREA_ORIGEN_ID, TRA_AREA_DESTINO_ID, TRA_Doc_Factura, TTR_ES_ASIGNACION);
    }

    [HttpPost]
    public Mensaje Set_Crear_Entrada([FromBody] IngresoActivo NuevaTipoActivo, Guid UsuarioIngresoCrear)
    {
      return IngresosController.response.Set_Crear_Entrada(new List<IngresoActivo>()
      {
        NuevaTipoActivo
      }, UsuarioIngresoCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_Entrada(
      [FromBody] IngresoActivo EditarIngresoActivo,
      Guid UsuarioEditarCrear)
    {
      return IngresosController.response.Set_Editar_Entrada(new List<IngresoActivo>()
      {
        EditarIngresoActivo
      }, UsuarioEditarCrear);
    }

    [HttpPost]
    public Mensaje Set_Autorizar_Transacciones(
      Guid TRA_ID,
      string URL_LINK,
      string TRA_OBSERVACIONES,
      Guid UsuarioAutorizarTransaccion)
    {
      return IngresosController.response.Set_Autorizar_Transacciones(new List<IngresoActivo>()
      {
        new IngresoActivo()
        {
          TRA_ID = new Guid?(TRA_ID),
          URL_LINK = URL_LINK,
          TRA_OBSERVACIONES = TRA_OBSERVACIONES
        }
      }, UsuarioAutorizarTransaccion);
    }

    [HttpPost]
    public Mensaje Set_Crear_DetalleTransacciones(
      [FromBody] TransaccionDetalle TransaccionDetalle,
      Guid UsuarioTransaccionCrear)
    {
      return IngresosController.response.Set_Crear_DetalleTransacciones(new List<TransaccionDetalle>()
      {
        TransaccionDetalle
      }, UsuarioTransaccionCrear);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_DetalleTransacciones(
      Guid? TRA_ID,
      string ETQ_BC,
      Guid UsuarioTransaccionEliminar)
    {
      return IngresosController.response.Set_Eliminar_DetalleTransacciones(new List<TransaccionDetalle>()
      {
        new TransaccionDetalle()
        {
          TRA_ID = TRA_ID,
          ETQ_BC = ETQ_BC
        }
      }, UsuarioTransaccionEliminar);
    }

    [HttpPost]
    public Mensaje Set_CancelarTransacciones(
      Guid TRA_ID,
      Guid UsuarioTransaccionCancelar,
      string URL_LINK)
    {
      return IngresosController.response.Set_CancelarTransacciones(TRA_ID, UsuarioTransaccionCancelar, URL_LINK);
    }
  }
}
