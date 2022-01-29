// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Controllers.TransaccionController
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using ClosedXML.Excel;
using NLog;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
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
  public class TransaccionController : ApiController
  {
    private static readonly TransaccionDataBase response = new TransaccionDataBase();
    private Logger logger = LogManager.GetCurrentClassLogger();

    [HttpGet]
    public IEnumerable<Activos> Get_list_Activos(Guid? Activo_ID)
    {
      return (IEnumerable<Activos>) TransaccionController.response.Get_list_Activos(Activo_ID, (string) null, new Guid?(),null);
    }
    [HttpGet]
    public IEnumerable<Activos> Get_list_Activos_FECHA_MOD(DateTime? FECHA_MOD)
    {
        return (IEnumerable<Activos>)TransaccionController.response.Get_list_Activos(new Guid?(), (string)null, new Guid?(),FECHA_MOD);
    }
    [HttpGet]
    public List<Transaccion> Get_list_Activos_Transacciones( Guid? ACT_ID_TRANSA)
    {
        return (List<Transaccion>)TransaccionController.response.Get_list_Activos_Transacciones(ACT_ID_TRANSA);
    }
    [HttpGet]
    public List<ActivosCambio> Get_list_Activos_Cambios(Guid? ACT_ID_CAMBIO)
    {
        return (List<ActivosCambio>)TransaccionController.response.Get_list_Activos_Cambios (ACT_ID_CAMBIO);
    }
    [HttpGet]
    public IEnumerable<Activos> Get_list_ActivosBarra(string ETQ)
    {
      return (IEnumerable<Activos>) TransaccionController.response.Get_list_Activos(new Guid?(), ETQ, new Guid?(), null);
    }

    [HttpGet]
    public IEnumerable<Activos> Get_list_ActivosResponsable(
      Guid ACT_RES_ID,
      string ETQ)
    {
      return (IEnumerable<Activos>) TransaccionController.response.Get_list_Activos(new Guid?(), ETQ, new Guid?(ACT_RES_ID), null);
    }

    [HttpGet]
    public Mensaje Get_validar_etiquetas(string ETQ_BC, string ETQ_CODE)
    {
      return TransaccionController.response.Get_validar_etiquetas(ETQ_BC, ETQ_CODE);
    }

    [HttpGet]
    public Mensaje Get_Validar_DetalleTransacciones(
      Guid DTR_TRA_ID,
      string ETQ,
      Guid USUARIO_VALIDA)
    {
      return TransaccionController.response.Get_Validar_DetalleTransacciones(DTR_TRA_ID, ETQ, USUARIO_VALIDA);
    }

    [HttpPost]
    public Mensaje Set_Crear_Activos([FromBody] Activos NuevaActivo, Guid UsuarioActivoCrear)
    {
      return TransaccionController.response.Set_Crear_Activos(new List<Activos>()
      {
        NuevaActivo
      }, UsuarioActivoCrear);
    }

    [HttpPost]
    public Mensaje Set_Reasignar_Etiqueta([FromBody] Activos Reasignacion, Guid UsuarioActivoReasignar)
    {
      return TransaccionController.response.Set_Reasignar_Etiqueta(new List<Activos>()
      {
        Reasignacion
      }, UsuarioActivoReasignar);
    }

    [HttpPost]
    public Mensaje Set_Editar_Activos([FromBody] Activos EditarActivo, Guid UsuarioActivoEditar)
    {
      return TransaccionController.response.Set_Editar_Activos(new List<Activos>()
      {
        EditarActivo
      }, UsuarioActivoEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_Activos(Guid EliminarActivo, Guid UsuarioActivoEliminar)
    {
      return TransaccionController.response.Set_Eliminar_Activos(new List<Guid>()
      {
        EliminarActivo
      }, UsuarioActivoEliminar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_ActivosMasivo(
      [FromBody] Seleccion EliminarActivoMasivo,
      Guid UsuarioActivoEliminarMas)
    {
      List<Guid> EliminarActivo = new List<Guid>();
      string seleccionados = EliminarActivoMasivo.SELECCIONADOS;
      char[] chArray = new char[1]{ '_' };
      foreach (string input in seleccionados.Split(chArray))
      {
        Guid guid = Guid.Parse(input);
        EliminarActivo.Add(guid);
      }
      return TransaccionController.response.Set_Eliminar_Activos(EliminarActivo, UsuarioActivoEliminarMas);
    }

    [HttpGet]
    public IEnumerable<ActivosImagenes> Get_list_ActivosImagenes(
      Guid? AIM_ACT_ID,
      Guid? AIM_TRA_ID,
      Guid? AIM_ID)
    {
      return (IEnumerable<ActivosImagenes>) TransaccionController.response.Get_list_ActivosImagenes(AIM_ACT_ID, AIM_TRA_ID, AIM_ID);
    }

    [HttpGet]
    public IEnumerable<TransaccionDetalle> Get_list_DetalleTransacciones(
      Guid? TRA_ID)
    {
      return TransaccionController.response.Get_list_DetalleTransacciones(TRA_ID);
    }

    [HttpPost]
    public async Task<Mensaje> Set_Crear_ActivosImagenes(
      Guid AIM_ACT_ID,
      Guid? AIM_TRA_ID,
      string AIM_OBSERVACION,
      Guid UsuarioActivoImgCrear)
    {
      Mensaje Respuesta = new Mensaje();
      List<ActivosImagenes> Lista = new List<ActivosImagenes>();
      ActivosImagenes NuevaImagen = new ActivosImagenes();
      NuevaImagen.AIM_ACT_ID = new Guid?(AIM_ACT_ID);
      NuevaImagen.AIM_TRA_ID = AIM_TRA_ID;
      NuevaImagen.AIM_OBSERVACION = AIM_OBSERVACION;
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
              NuevaImagen.AIM_RUTA = str2.Split('.')[str2.Split('.').Length - 1];
              break;
            }
            break;
          }
        }
        Lista.Add(NuevaImagen);
        Respuesta = TransaccionController.response.Set_Crear_ActivosImagenes(Lista, UsuarioActivoImgCrear);
        if (Respuesta.errNumber == 0)
        {
          string str = filePath + ("\\" + (object) NuevaImagen.AIM_ID + "." + NuevaImagen.AIM_RUTA);
          if (System.IO.File.Exists(str))
            System.IO.File.Delete(str);
          System.IO.File.Move(sourceFileName, str);
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
    public async Task<Mensaje> Set_Editar_ActivosImagenes(
      Guid AIM_ID,
      Guid AIM_ACT_ID,
      Guid? AIM_TRA_ID,
      string AIM_OBSERVACION,
      Guid UsuarioActivoImgEditar)
    {
      string originalFileName = "";
      Mensaje Respuesta = new Mensaje();
      List<ActivosImagenes> Lista = new List<ActivosImagenes>();
      ActivosImagenes EditarImagen = new ActivosImagenes();
      EditarImagen.AIM_ID = AIM_ID;
      EditarImagen.AIM_ACT_ID = new Guid?(AIM_ACT_ID);
      EditarImagen.AIM_TRA_ID = AIM_TRA_ID;
      EditarImagen.AIM_OBSERVACION = AIM_OBSERVACION;
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
            originalFileName = filePath + ("\\" + content2.Headers.ContentDisposition.FileName.Trim('"'));
            if (!originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1].Equals(""))
            {
              string str = originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1];
              EditarImagen.AIM_RUTA = ((ValueType) EditarImagen.AIM_ID).ToString() + "." + str.Split('.')[str.Split('.').Length - 1];
              break;
            }
            break;
          }
        }
        Lista.Add(EditarImagen);
        Respuesta = TransaccionController.response.Set_Editar_ActivosImagenes(Lista, UsuarioActivoImgEditar);
        if (Respuesta.errNumber == 0)
        {
          if (!originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1].Equals(""))
          {
            originalFileName = filePath + ("\\" + EditarImagen.AIM_RUTA);
            if (System.IO.File.Exists(originalFileName))
              System.IO.File.Delete(originalFileName);
            System.IO.File.Move(sourceFileName, originalFileName);
          }
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
    public Mensaje Set_Eliminar_ActivosImagenes(
      Guid EliminarActivoImagen,
      Guid UsuarioActivoImgEliminar)
    {
      string appSetting = ConfigurationManager.AppSettings["FileUploadImgLocation"];
      Mensaje mensaje1 = new Mensaje();
      Mensaje mensaje2 = TransaccionController.response.Set_Eliminar_ActivosImagenes(new List<Guid>()
      {
        EliminarActivoImagen
      }, UsuarioActivoImgEliminar);
      if (mensaje2.errNumber == 0)
        System.IO.File.Delete(HostingEnvironment.MapPath(appSetting + "//" + mensaje2.data.ToString()));
      return mensaje2;
    }

        [HttpGet]
        public IEnumerable<Transaccion> Get_list_TransaccionesContrato( Guid? CON_ID)
  
        {
            WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();           
            return TransaccionController.response.Get_list_TransaccionesContrato( CON_ID);
        }

        [HttpGet]
    public IEnumerable<Contratos> Get_list_Contratos(
      Guid? CLI_ID,
      Guid? EST_ID,
      string TIPODOC,
      string CON_DOCUMENTO,
      string CON_FECHA_SUSCRIPCION,
      string CON_FECHA_INICIO,
      string CON_FECHA_FINAL,
      Guid? CON_ID)
    {
      WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
      string CON_NUMERO_DOC = (string) null;
      string CON_DOCUMENTO_SAP = (string) null;
      DateTime? CON_FECHA_SUSCRIPCION1 = helper.Fecha(CON_FECHA_SUSCRIPCION);
      DateTime? CON_FECHA_INICIO1 = helper.Fecha(CON_FECHA_INICIO);
      DateTime? CON_FECHA_FINAL1 = helper.Fecha(CON_FECHA_FINAL);
      if (TIPODOC == "1")
        CON_NUMERO_DOC = CON_DOCUMENTO;
      else if (TIPODOC == "2")
        CON_DOCUMENTO_SAP = CON_DOCUMENTO;
      return TransaccionController.response.Get_list_Contratos(CLI_ID, EST_ID, CON_FECHA_SUSCRIPCION1, CON_FECHA_INICIO1, CON_FECHA_FINAL1, CON_NUMERO_DOC, CON_DOCUMENTO_SAP, CON_ID);
    }

    [HttpGet]
    public IEnumerable<ListaDesplegable> Get_list_ContratosCliente(
      Guid? CLI_ID)
    {
      List<ListaDesplegable> listaDesplegableList = new List<ListaDesplegable>();
      foreach (Contratos listContrato in TransaccionController.response.Get_list_Contratos(CLI_ID, new Guid?(), new DateTime?(), new DateTime?(), new DateTime?(), (string) null, (string) null, new Guid?()))
        listaDesplegableList.Add(new ListaDesplegable()
        {
          id = new Guid?(listContrato.CON_ID),
          text = listContrato.CON_NUMERO_DOC
        });
      return (IEnumerable<ListaDesplegable>) listaDesplegableList;
    }

    [HttpPost]
    public Mensaje Set_Crear_Contratos([FromBody] Contratos NuevoContrato, Guid UsuarioContratoCrear)
    {
      return TransaccionController.response.Set_Crear_Contratos(new List<Contratos>()
      {
        NuevoContrato
      }, UsuarioContratoCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_Contratos([FromBody] Contratos EditarContrato, Guid UsuarioContratoEditar)
    {
      return TransaccionController.response.Set_Editar_Contratos(new List<Contratos>()
      {
        EditarContrato
      }, UsuarioContratoEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_Contratos(
      Guid EliminarContrato,
      Guid UsuarioContratoEliminar)
    {
      return TransaccionController.response.Set_Eliminar_Contratos(new List<Guid>()
      {
        EliminarContrato
      }, UsuarioContratoEliminar);
    }

    [HttpGet]
    public IEnumerable<Modelo> Get_list_ModeloxTipoActivo(Guid? MOD_TAC_ID)
    {
      return TransaccionController.response.Get_list_ModeloxTipoActivo(MOD_TAC_ID);
    }

    [HttpGet]
    public IEnumerable<Estados> Get_list_EstadosContratos(Guid? EST_ID)
    {
      return TransaccionController.response.Get_list_EstadosContratos(EST_ID);
    }

    [HttpGet]
    public HttpResponseMessage Get(string PHOTO)
    {
      string appSetting = ConfigurationManager.AppSettings["FileUploadImgLocation"];
      HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
      string str = PHOTO;
      FileStream fileStream = new FileStream(HostingEnvironment.MapPath(appSetting + "//" + str), FileMode.Open);
      Image image = Image.FromStream((Stream) fileStream);
      MemoryStream memoryStream1 = new MemoryStream();
      MemoryStream memoryStream2 = memoryStream1;
      ImageFormat jpeg = ImageFormat.Jpeg;
      image.Save((Stream) memoryStream2, jpeg);
      httpResponseMessage.Content = (HttpContent) new ByteArrayContent(memoryStream1.ToArray());
      httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
      fileStream.Close();
      fileStream.Dispose();
      memoryStream1.Close();
      memoryStream1.Dispose();
      return httpResponseMessage;
    }

    [HttpPost]
    public async Task<Mensaje> Set_Anexar_Imagen(
      Guid? AIM_ACT_ID,
      Guid? AIM_TRA_ID,
      string AIM_OBSERVACION_EDIT,
      Guid UsuarioActivoImgCrear)
    {
      Mensaje Respuesta = new Mensaje();
      try
      {
        List<ActivosImagenes> Lista = new List<ActivosImagenes>();
        ActivosImagenes NuevaImagen = new ActivosImagenes();
        NuevaImagen.AIM_ACT_ID = AIM_ACT_ID;
        NuevaImagen.AIM_TRA_ID = AIM_TRA_ID;
        NuevaImagen.AIM_OBSERVACION = AIM_OBSERVACION_EDIT;
        NuevaImagen.AIM_RUTA = "jpg";
        Lista.Add(NuevaImagen);
        string fileuploadPath = ConfigurationManager.AppSettings["FileUploadImgLocation"];
        MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(fileuploadPath);
        StreamContent content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
        foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) this.Request.Content.Headers)
          content.Headers.TryAddWithoutValidation(header.Key, header.Value);
        MultipartFormDataStreamProvider dataStreamProvider = await content.ReadAsMultipartAsync<MultipartFormDataStreamProvider>(provider);
        byte[] buffer = Convert.FromBase64String(provider.FormData[0].Trim());
        MemoryStream memoryStream = new MemoryStream(buffer, 0, buffer.Length);
        memoryStream.Write(buffer, 0, buffer.Length);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        Respuesta = TransaccionController.response.Set_Crear_ActivosImagenes(Lista, UsuarioActivoImgCrear);
        Bitmap bitmap = new Bitmap((Stream) memoryStream);
        Respuesta.data = (object) (NuevaImagen.AIM_ID.ToString() + "." + NuevaImagen.AIM_RUTA);
        string filename = HostingEnvironment.MapPath(fileuploadPath + "//" + Respuesta.data);
        bitmap.Save(filename, bitmap.RawFormat);
        Lista = (List<ActivosImagenes>) null;
        NuevaImagen = (ActivosImagenes) null;
        fileuploadPath = (string) null;
        provider = (MultipartFormDataStreamProvider) null;
      }
      catch (Exception ex)
      {
        Respuesta.data = (object) ex.Message;
      }
      return Respuesta;
    }

    [HttpPost]
    public async Task<List<CentroCosto>> Set_Leer_Excel_CentroCosto(
      string NOMBREARCHIVOEXCEL)
    {
      List<CentroCosto> Lista = new List<CentroCosto>();
      string rutaFile = "";
      try
      {
        string fileuploadPath = ConfigurationManager.AppSettings["FileUploadArcLocation"];
        MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(fileuploadPath);
        this.logger.Error("Traza 1: " + fileuploadPath);
        StreamContent content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
        foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) this.Request.Content.Headers)
          content.Headers.TryAddWithoutValidation(header.Key, header.Value);
        this.logger.Error("Traza 2: ");
        MultipartFormDataStreamProvider dataStreamProvider = await content.ReadAsMultipartAsync<MultipartFormDataStreamProvider>(provider);
        this.logger.Error("Traza 3: ");
        FileInfo fileInfo1 = (FileInfo) null;
        foreach (MultipartFileData multipartFileData in provider.FileData)
        {
          fileInfo1 = new FileInfo(multipartFileData.LocalFileName);
          rutaFile = fileuploadPath + "\\" + multipartFileData.Headers.ContentDisposition.FileName.Replace("\"", "");
        }
        fileInfo1.CopyTo(rutaFile);
        fileInfo1.Delete();
        FileInfo fileInfo2 = new FileInfo(rutaFile);
        bool flag = true;
        int num = 2;
        string name = "CentroDeCosto";
        if (fileInfo2.Extension.ToLower().Equals(".xls"))
        {
          HSSFWorkbook hssfWorkbook;
          using (FileStream fileStream = new FileStream(rutaFile, FileMode.Open, FileAccess.Read))
          {
            hssfWorkbook = new HSSFWorkbook((Stream) fileStream);
            fileStream.Close();
          }
          ISheet sheet = hssfWorkbook.GetSheet(name);
          if (sheet == null)
          {
            Lista.Add(new CentroCosto()
            {
              CCO_CODIGO = "-1",
              CCO_DESC = "Nombre de hoja invalida"
            });
            fileInfo2.Delete();
            return Lista;
          }
          for (int rownum = 1; rownum <= sheet.LastRowNum; ++rownum)
          {
            if (sheet.GetRow(rownum) != null)
            {
              CentroCosto centroCosto = new CentroCosto();
              try
              {
                centroCosto.CCO_CODIGO = sheet.GetRow(rownum).GetCell(0).NumericCellValue.ToString();
              }
              catch
              {
                try
                {
                  centroCosto.CCO_CODIGO = sheet.GetRow(rownum).GetCell(0).StringCellValue;
                }
                catch
                {
                }
              }
              try
              {
                centroCosto.CCO_DESC = sheet.GetRow(rownum).GetCell(1).StringCellValue;
              }
              catch
              {
              }
              try
              {
                centroCosto.CCO_DESC_ALT = sheet.GetRow(rownum).GetCell(2).StringCellValue;
              }
              catch
              {
              }
              centroCosto.CCO_ACTIVE = true;
              Lista.Add(centroCosto);
            }
          }
        }
        else if (fileInfo2.Extension.ToLower().Equals(".xlsx"))
        {
          XLWorkbook xlWorkbook = new XLWorkbook(rutaFile);
          while (flag)
          {
            try
            {
              CentroCosto centroCosto = new CentroCosto();
              centroCosto.CCO_CODIGO = xlWorkbook.Worksheet(name).Cell("A" + num.ToString()).Value.ToString();
              centroCosto.CCO_DESC = xlWorkbook.Worksheet(name).Cell("B" + num.ToString()).Value.ToString();
              centroCosto.CCO_DESC_ALT = xlWorkbook.Worksheet(name).Cell("C" + num.ToString()).Value.ToString();
              centroCosto.CCO_ACTIVE = true;
              if (centroCosto.CCO_CODIGO.Equals("") && centroCosto.CCO_DESC.Equals("") && centroCosto.CCO_DESC_ALT.Equals(""))
                flag = false;
              else
                Lista.Add(centroCosto);
              ++num;
            }
            catch (Exception ex)
            {
              Lista.Add(new CentroCosto()
              {
                CCO_CODIGO = "-1",
                CCO_DESC = "Nombre de hoja invalida"
              });
              fileInfo2.Delete();
              this.logger.Error(ex, "Error cargar excel: " + ex.Message + ":" + rutaFile);
              return Lista;
            }
          }
        }
        fileInfo2.Delete();
        fileuploadPath = (string) null;
        provider = (MultipartFormDataStreamProvider) null;
      }
      catch (Exception ex)
      {
        CentroCosto centroCosto = new CentroCosto()
        {
          CCO_CODIGO = "-2",
          CCO_DESC = ex.Message
        };
        this.logger.Error(ex, "Cargar excel: " + ex.Message + ":" + rutaFile);
      }
      return Lista;
    }
  }
}
