using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
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
    public class ConfiguracionController : ApiController
    {
        private static readonly ConfiguracionDataBase response = new ConfiguracionDataBase();
        private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
        [HttpGet]
        public IEnumerable<Transaccion> Get_list_Asignaciones_Transacciones_SinNotificar(string tipo)
        {
            switch (tipo)
            {
                case "0": return ConfiguracionController.response.Get_list_Asignaciones_SinNotificar();
                default:  return ConfiguracionController.response.Get_list_transacciones_SinNotificar();
            } 
            
        }
        [HttpGet]
        public IEnumerable<Usuarios> Get_list_emai_autorizados(Guid TRA_ID)
        {
            return ConfiguracionController.response.Get_list_emai_autorizados(TRA_ID);
        }
        [HttpGet]
        public IEnumerable<Empresa> Get_list_EMPRESA()
        {
            return ConfiguracionController.response.Get_list_EMPRESA ();
        }
        [HttpGet]
        public IEnumerable<Transaccion> Get_list_Informe_Prestamos(Guid?TRA_ID, Guid?RES_ID,int Vencidos,string Fecha_Inicial , string Fecha_Final )
        {
            return ConfiguracionController.response.Get_list_Informe_Prestamos (TRA_ID,RES_ID,Vencidos, helper.Fecha (Fecha_Inicial), helper.Fecha (Fecha_Final));
        }
        [HttpGet]
        public IEnumerable<Mensaje> Set_Asignaciones_notificada(string tipo,Guid TRA_RES_ID)
        {
            switch (tipo)
            {
                case "0":return ConfiguracionController.response.Set_Asignaciones_notificada(TRA_RES_ID);
                default: return ConfiguracionController.response.Set_Transaccion_notificada (TRA_RES_ID);
            }
            
        }
        [HttpPost]
        public async Task<Mensaje> Set_EMPRESA_LOGO(string Logo)
        {
            Mensaje Respuesta = new Mensaje();
            Respuesta.errNumber  = 0;
            Respuesta.message = "Operación exitosa";
            try
            {
                string originalFileName = "";                    
                string fileuploadPath = ConfigurationManager.AppSettings["FileUploadArcLocation"];
                MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(fileuploadPath);
                StreamContent content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders)this.Request.Content.Headers)
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                MultipartFormDataStreamProvider dataStreamProvider = await content.ReadAsMultipartAsync<MultipartFormDataStreamProvider>(provider);
                string sourceFileName = provider.FileData.Select<MultipartFileData, string>((Func<MultipartFileData, string>)(x => x.LocalFileName)).FirstOrDefault<string>();
                foreach (HttpContent content2 in provider.Contents)
                {
                    if (content2.Headers.ContentDisposition.FileName != null)
                    {
                        originalFileName = fileuploadPath + ("\\" + content2.Headers.ContentDisposition.FileName.Trim('"'));
                        if (!originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1].Equals(""))
                        {
                            string str = originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1];
                            Logo = str;
                            break;
                        }
                        break;
                    }
                }       
              
                    if (!originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1].Equals(""))
                    {
                        originalFileName = fileuploadPath + ("\\" + Logo);
                        if (System.IO.File.Exists(originalFileName))
                            System.IO.File.Delete(originalFileName);
                        System.IO.File.Move(sourceFileName, originalFileName);
                    }
                              
                fileuploadPath = (string)null;
                provider = (MultipartFormDataStreamProvider)null;
            }
            catch (Exception ex)
            {
                Respuesta.errNumber = -1;
                Respuesta.message = ex.Message;
            }
            return Respuesta;
        }
        [HttpPost]
        public Mensaje Set_Editar_EMPRESA([FromBody] Empresa empresa, Guid? Usuario)
        {
            Mensaje Respuesta = new Mensaje();
            try
            {
                //string originalFileName = "";
                List<Empresa> Lista = new List<Empresa>();

                //string fileuploadPath = ConfigurationManager.AppSettings["FileUploadImgLocation"];
                //MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(fileuploadPath);
                //StreamContent content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                //foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders)this.Request.Content.Headers)
                //    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                //MultipartFormDataStreamProvider dataStreamProvider = await content.ReadAsMultipartAsync<MultipartFormDataStreamProvider>(provider);
                //string sourceFileName = provider.FileData.Select<MultipartFileData, string>((Func<MultipartFileData, string>)(x => x.LocalFileName)).FirstOrDefault<string>();
                //foreach (HttpContent content2 in provider.Contents)
                //{
                //    if (content2.Headers.ContentDisposition.FileName != null)
                //    {
                //        originalFileName = fileuploadPath + ("\\" + content2.Headers.ContentDisposition.FileName.Trim('"'));
                //        if (!originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1].Equals(""))
                //        {
                //            string str = originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1];
                //            empresa.EMP_LOGO = str;
                //            break;
                //        }
                //        break;
                //    }
                //}
                Lista.Add(empresa);
                Respuesta = ConfiguracionController.response.Set_Editar_EMPRESA(Lista, Usuario);
                //if (Respuesta.errNumber == 0)
                //{
                //    if (!originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1].Equals(""))
                //    {
                //        originalFileName = fileuploadPath + ("\\" + empresa.EMP_LOGO);
                //        if (System.IO.File.Exists(originalFileName))
                //            System.IO.File.Delete(originalFileName);
                //        System.IO.File.Move(sourceFileName, originalFileName);
                //    }
                //}
                Lista = (List<Empresa>)null;
                empresa = (Empresa)null;
                //fileuploadPath = (string)null;
                //provider = (MultipartFormDataStreamProvider)null;
            }
            catch (Exception ex)
            {
                Respuesta.data = (object)ex.Message;
            }
            return Respuesta;
        }
    }
}
