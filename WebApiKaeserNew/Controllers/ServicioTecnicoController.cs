// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Controllers.ServicioTecnicoController
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using WebApiKaeser.Factory;
using WebApiKaeser.Models;

namespace WebApiKaeser.Controllers
{
  public class ServicioTecnicoController : ApiController
  {
    private static readonly ServicioTecnicoDataBase response = new ServicioTecnicoDataBase();
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();

    [HttpGet]
    public IEnumerable<Estados> Get_list_ServicioTecnicoCmb(short lista)
    {
      return lista == (short) 0 ? (IEnumerable<Estados>) ServicioTecnicoController.response.Get_list_ServicioTecnicoEstado() : (IEnumerable<Estados>) ServicioTecnicoController.response.Get_list_ServicioTecnicoProceso();
    }

    [HttpGet]
    public IEnumerable<ServicioTecnico> Get_list_ServicioTecnico(
      Guid? STE_STE_ID,
      string FECHA_INICIO,
      string FECHA_FIN,
      short? PARAMETROCONSULTAFECHA,
      Guid? STE_USUARIO_SOLICITA,
      Guid? STE_ID,
      Guid? STE_TRA_ID,
      Guid? STE_ACT_ID)
    {
      return (IEnumerable<ServicioTecnico>) ServicioTecnicoController.response.Get_list_ServicioTecnico(STE_USUARIO_SOLICITA, STE_STE_ID, this.helper.Fecha(FECHA_INICIO), this.helper.Fecha(FECHA_FIN), PARAMETROCONSULTAFECHA, STE_ID, STE_TRA_ID, STE_ACT_ID);
    }

    [HttpGet]
    public List<ServicioTecnicoIncidencia> Get_list_ServicioTecnicoIncidencia(
      Guid STI_STE_ID)
    {
      return ServicioTecnicoController.response.Get_list_ServicioTecnicoIncidencia(STI_STE_ID);
    }

    [HttpGet]
    public List<ServicioTecnicoImagen> Get_list_ServicioTecnicoImagen(
      Guid STM_STI_ID)
    {
      return ServicioTecnicoController.response.Get_list_ServicioTecnicoImagen(STM_STI_ID);
    }

    [HttpGet]
    public List<ServicioTecnicoRepuesto> Get_list_ServicioTecnicoRepuesto(
      Guid STR_STE_ID)
    {
      return ServicioTecnicoController.response.Get_list_ServicioTecnicoRepuesto(STR_STE_ID);
    }

    [HttpPost]
    public Mensaje Set_ActualizarServicioTecnicoCambioEstado(
      [FromBody] ServicioTecnico ServicioTecnico,
      Guid UsuarioCambioEstado)
    {
      return ServicioTecnicoController.response.Set_ActualizarServicioTecnicoCambioEstado(new List<ServicioTecnico>()
      {
        ServicioTecnico
      }, UsuarioCambioEstado);
    }

    [HttpPost]
    public Mensaje Set_EliminarServicioTecnico(Guid STE_ID, Guid STE_USU_ID)
    {
      return ServicioTecnicoController.response.Set_EliminarServicioTecnico(new List<Guid>()
      {
        STE_ID
      }, STE_USU_ID);
    }

    [HttpPost]
    public Mensaje Set_CrearServicioTecnicov2(
      [FromBody] ServicioTecnico ServicioTecnico,
      Guid UsuarioCrearServicio)
    {
      return ServicioTecnicoController.response.Set_CrearServicioTecnicov2(new List<ServicioTecnico>()
      {
        ServicioTecnico
      }, UsuarioCrearServicio);
    }

    [HttpPost]
    public Mensaje Set_CrearServicioTecnicoIncidencia(
      [FromBody] ServicioTecnicoIncidencia Incidencia,
      Guid UsuarioCrearServicioIncidencia)
    {
      return ServicioTecnicoController.response.Set_CrearServicioTecnicoIncidencia(new List<ServicioTecnicoIncidencia>()
      {
        Incidencia
      }, UsuarioCrearServicioIncidencia);
    }

    [HttpPost]
    public Mensaje Set_EliminarServicioTecnicoIncidente(
      Guid Incidencia,
      Guid UsuarioModificaServicioIncidencia)
    {
      return ServicioTecnicoController.response.Set_EliminarServicioTecnicoIncidente(new List<Guid>()
      {
        Incidencia
      }, UsuarioModificaServicioIncidencia);
    }

    [HttpPost]
    public async Task<Mensaje> Set_CrearServicioTecnicoImagenes(
      Guid STM_STI_ID,
      Guid UsuarioCrearServicioImagen)
    {
      Mensaje Respuesta = new Mensaje();
      List<ServicioTecnicoImagen> Lista = new List<ServicioTecnicoImagen>();
      ServicioTecnicoImagen NuevaImagen = new ServicioTecnicoImagen();
      NuevaImagen.STM_STI_ID = STM_STI_ID;
      try
      {
        string filePath = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["FileUploadImgLocation"]);
        MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(filePath);
        StreamContent content1 = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
        foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) this.Request.Content.Headers)
          content1.Headers.TryAddWithoutValidation(header.Key, header.Value);
        MultipartFormDataStreamProvider dataStreamProvider = await content1.ReadAsMultipartAsync<MultipartFormDataStreamProvider>(provider);
        string sourceFileName = provider.FileData.Select<MultipartFileData, string>((Func<MultipartFileData, string>) (x => x.LocalFileName)).FirstOrDefault<string>();
        foreach (HttpContent content2 in provider.Contents)
        {
          if (content2.Headers.ContentDisposition.FileName != null)
          {
            string str1 = filePath + ("\\" + content2.Headers.ContentDisposition.FileName.Trim('"'));
            if (!str1.Split('\\')[str1.Split('\\').Length - 1].Equals(""))
            {
              string str2 = str1.Split('\\')[str1.Split('\\').Length - 1];
              NuevaImagen.RUTA = str2.Split('.')[str2.Split('.').Length - 1];
              break;
            }
            break;
          }
        }
        Lista.Add(NuevaImagen);
        Respuesta = ServicioTecnicoController.response.Set_CrearServicioTecnicoImagenes(Lista, UsuarioCrearServicioImagen);
        if (Respuesta.errNumber == 0)
        {
          string str = filePath + ("\\" + (object) NuevaImagen.STM_ID + "." + NuevaImagen.RUTA);
          if (File.Exists(str))
            File.Delete(str);
          File.Move(sourceFileName, str);
        //  Respuesta.data = NuevaImagen.STM_ID.ToString() + "." + NuevaImagen.RUTA;
        }
        filePath = (string) null;
        provider = (MultipartFormDataStreamProvider) null;
      }
      catch (Exception ex)
      {
        Respuesta.errNumber = 1;
        Respuesta.message = ex.Message;
      }
      return Respuesta;
    }

    [HttpPost]
    public Mensaje Set_EliminarServicioTecnicoImagen(
      Guid Imagen,
      Guid UsuarioEliminaServicioImagen)
    {
      return ServicioTecnicoController.response.Set_EliminarServicioTecnicoImagen(new List<Guid>()
      {
        Imagen
      }, UsuarioEliminaServicioImagen);
    }

    [HttpPost]
    public Mensaje Set_CrearServicioTecnicoRepuesto(
      [FromBody] ServicioTecnicoRepuesto Repuesto,
      Guid USUARIO_REPUESTO_CREA)
    {
      return ServicioTecnicoController.response.Set_CrearServicioTecnicoRepuesto(new List<ServicioTecnicoRepuesto>()
      {
        Repuesto
      }, USUARIO_REPUESTO_CREA);
    }

    [HttpPost]
    public Mensaje Set_EliminarServicioTecnicoRepuesto(
      Guid STM_ID,
      Guid STR_USUARIO_MODIFICA)
    {
      return ServicioTecnicoController.response.Set_EliminarServicioTecnicoRepuesto(new List<Guid>()
      {
        STM_ID
      }, STR_USUARIO_MODIFICA);
    }
    [HttpGet]
    public Mensaje Get_Validar_Activo_ServicioTecnico(string ETQ)
    {
        return ServicioTecnicoController.response.Get_Validar_Activo_ServicioTecnico(ETQ);
    }
    }
}
