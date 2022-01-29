// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Controllers.AuditoriaController
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

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
    public class AuditoriaController : ApiController
    {
        private static readonly AuditoriaDataBase response = new AuditoriaDataBase();
        private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();

        [HttpGet]
        public IEnumerable<Estados> Get_list_estadoAuditoria(string DATOS)
        {
            return DATOS.Equals("0") ? AuditoriaController.response.Get_list_estadoAuditoria() : AuditoriaController.response.Get_listToma();
        }

        [HttpGet]
        public IEnumerable<Auditoria> Get_list_Auditorias(
          Guid? AUD_ID,
          Guid? ESA_ID,
          DateTime? FECHA_INICIAL,
          DateTime? FECHA_FINAL)
        {
            return AuditoriaController.response.Get_list_Auditorias(AUD_ID, ESA_ID, FECHA_INICIAL, FECHA_FINAL);
        }

        private List<Menus> Get_list_AreaAuditoria(List<Areas> lstAreas)
        {
            List<Menus> menusList = new List<Menus>();
            foreach (Areas areas in lstAreas.FindAll((Predicate<Areas>)(t => !t.ARE_ARE_PARENT_ID.HasValue || t.ARE_ARE_PARENT_ID.Equals((object)Guid.Parse("00000000-0000-0000-0000-000000000000")))))
            {
                Areas area = areas;
                Menus menus = new Menus();
                menus.id = area.ARE_ID;
                menus.text = area.ARE_DESC;
                menus.state = new Nodos()
                {
                    opened = true,
                    selected = area.ARE_ACTIVE
                };
                menus.Seleccionado = area.ARE_ACTIVE;
                if (lstAreas.Exists((Predicate<Areas>)(t => t.ARE_ARE_PARENT_ID.Equals((object)area.ARE_ID))))
                {
                    menus.children = new List<Menus>();
                    this.helper.ArmarArbol(menus.children, menus.id, lstAreas);
                    if (menus.children.Exists(a => a.Seleccionado)) menus.Seleccionado = true;
                }
                menusList.Add(menus);
            }
            return menusList;
        }

        [HttpGet]
        public AuditoriaConsultaDetalle Get_list_Auditoria_detalle(Guid? AUD_ID)
        {
            AuditoriaConsultaDetalle auditoriaConsultaDetalle = new AuditoriaConsultaDetalle();
            List<Areas> lstAreas = new List<Areas>();
            List<Responsable> lstResponsable = new List<Responsable>();
            List<TipoActivo> lstTipoActivo = new List<TipoActivo>();
            List<Areas> lstAreasSeleccionada = new List<Areas>();
            auditoriaConsultaDetalle.cabecera = AuditoriaController.response.Get_list_Auditoria_detalle(AUD_ID, ref lstAreas, ref lstResponsable, ref lstTipoActivo);
            auditoriaConsultaDetalle.areas = this.Get_list_AreaAuditoria(lstAreas);
            auditoriaConsultaDetalle.responsable = lstResponsable;
            auditoriaConsultaDetalle.tipoactivo = lstTipoActivo;
            //obtiene la jerarquia ed areas seleccionadas
            List<Guid?> ListaAreasPadre = new List<Guid?>();
            foreach (Areas areas in lstAreas)
            {
                Areas area = areas;
                if (!lstAreas.Exists((Predicate<Areas>)(t =>
                {
                    Guid? areAreParentId = t.ARE_ARE_PARENT_ID;
                    Guid areId = area.ARE_ID;
                    if (!areAreParentId.HasValue)
                        return false;
                    return !areAreParentId.HasValue || areAreParentId.GetValueOrDefault() == areId;
                })) && area.ARE_ACTIVE)
                {
                    if (!ListaAreasPadre.Exists((Predicate<Guid?>)(a => a.Equals((object)area.ARE_ARE_PARENT_ID))))
                        ListaAreasPadre.Add(area.ARE_ARE_PARENT_ID);
                }
            }
            this.helper.AgregarAreaPadre(ListaAreasPadre, lstAreas, ref lstAreasSeleccionada);
            //
            auditoriaConsultaDetalle.areasSeleccionadas = this.Get_list_AreaAuditoria(lstAreasSeleccionada);
            return auditoriaConsultaDetalle;
        }

        [HttpGet]
        public IEnumerable<Activos> Get_list_PrevisualizacionAuditoria(
          Guid? AUD_ID,
          Guid? AUD_USUARIO_CREATE)
        {
            return AuditoriaController.response.Get_list_PrevisualizacionAuditoria(AUD_ID, AUD_USUARIO_CREATE);
        }

        [HttpPost]
        public Mensaje Set_Crear_Detalle_Auditoria(
          [FromBody] AuditoriaDetalle AUDITORIADET,
          Guid UsuarioAuditoriaDetCrear)
        {
            return AuditoriaController.response.Set_Crear_Detalle_Auditoria(new List<AuditoriaDetalle>()
      {
        AUDITORIADET
      }, new Guid?(UsuarioAuditoriaDetCrear));
        }

        [HttpPost]
        public Mensaje Set_Asignar_Auditoria(
          [FromBody] Auditoria AUDITORIAASIG,
          Guid? UsuarioAuditoriaAsignar)
        {
            return AuditoriaController.response.Set_Asignar_Auditoria(new List<Auditoria>()
      {
        AUDITORIAASIG
      }, UsuarioAuditoriaAsignar);
        }

        [HttpGet]
        public List<ActivosImagenes> Get_listar_AuditoriaImagenes(
          Guid? AIM_ATO_ID,
          Guid? AIM_ACT_ID)
        {
            return AuditoriaController.response.Get_listar_AuditoriaImagenes(AIM_ATO_ID, AIM_ACT_ID);
        }

        [HttpGet]
        public List<ActivosImagenes> Get_listar_AuditoriaImagenesV2(
         Guid? AIM_AUD_ID,
         Guid? AIM_ACT_ID)
        {
            return AuditoriaController.response.Get_listar_AuditoriaImagenesV2(AIM_AUD_ID, AIM_ACT_ID);
        }
        [HttpPost]
        public Mensaje Set_Crear_Auditoria(
          [FromBody] AuditoriaDetalle AUDITORIA,
          Guid? UsuarioAuditoriaCrear)
        {
            return AuditoriaController.response.Set_Crear_Auditoria(new List<AuditoriaDetalle>()
      {
        AUDITORIA
      }, UsuarioAuditoriaCrear);
        }

        [HttpGet]
        public IEnumerable<AuditoriaUsuario> Get_list_ListarAuditoriasPorUsuario(
          Guid ATO_USUARIO_TOMA)
        {
            return AuditoriaController.response.Get_list_ListarAuditoriasPorUsuario(ATO_USUARIO_TOMA);
        }

        [HttpPost]
        public Mensaje Set_CapturaAuditoria([FromBody] AuditoriaCaptura AUDITORIACAT, Guid USUARIO_TOMA)
        {
            return AuditoriaController.response.Set_CapturaAuditoria(new List<AuditoriaCaptura>()
      {
        AUDITORIACAT
      }, USUARIO_TOMA);
        }

        [HttpPost]
        public async Task<Mensaje> Set_CrearAuditoriaImagenes(
          Guid AIM_ATO_ID,
          Guid? AIM_ACT_ID,
          Guid UsuarioAuditoriaImgCrear)
        {
            Mensaje Respuesta = new Mensaje();
            List<ActivosImagenes> Lista = new List<ActivosImagenes>();
            ActivosImagenes NuevaImagen = new ActivosImagenes();
            NuevaImagen.AIM_ACT_ID = AIM_ACT_ID;
            NuevaImagen.AIM_TRA_ID = new Guid?(AIM_ATO_ID);
            try
            {
                string filePath = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["FileUploadImgLocation"]);
                MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(filePath);
                StreamContent content1 = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders)this.Request.Content.Headers)
                    content1.Headers.TryAddWithoutValidation(header.Key, header.Value);
                MultipartFormDataStreamProvider dataStreamProvider = await content1.ReadAsMultipartAsync<MultipartFormDataStreamProvider>(provider);
                string sourceFileName = provider.FileData.Select<MultipartFileData, string>((Func<MultipartFileData, string>)(x => x.LocalFileName)).FirstOrDefault<string>();
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
                Respuesta = AuditoriaController.response.Set_CrearAuditoriaImagenes(Lista, UsuarioAuditoriaImgCrear);
                if (Respuesta.errNumber == 0)
                {
                    string str = filePath + ("\\" + (object)NuevaImagen.AIM_ID + "." + NuevaImagen.AIM_RUTA);
                    if (File.Exists(str))
                        File.Delete(str);
                    File.Move(sourceFileName, str);
                }
                filePath = (string)null;
                provider = (MultipartFormDataStreamProvider)null;
            }
            catch (Exception ex)
            {
                Respuesta.errNumber = 1;
                Respuesta.message = ex.Message;
            }
            return Respuesta;
        }

        [HttpPost]
        public async Task<Mensaje> Set_CrearAuditoriaImagenesMobile(
          Guid AIM_ATO_ID,
          Guid? AIM_ACT_ID,
          Guid UsuarioAuditoriaImgMobile)
        {
            Mensaje Respuesta = new Mensaje();
            try
            {
                //Mensaje Respuesta = new Mensaje();
                List<ActivosImagenes> Lista = new List<ActivosImagenes>();
                ActivosImagenes NuevaImagen = new ActivosImagenes();
                NuevaImagen.AIM_ACT_ID = AIM_ACT_ID;
                NuevaImagen.AIM_TRA_ID = new Guid?(AIM_ATO_ID);
                NuevaImagen.AIM_RUTA = "jpg";
                Lista.Add(NuevaImagen);
                string fileuploadPath = ConfigurationManager.AppSettings["FileUploadImgLocation"];
                MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(fileuploadPath);
                StreamContent content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders)this.Request.Content.Headers)
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                MultipartFormDataStreamProvider dataStreamProvider = await content.ReadAsMultipartAsync<MultipartFormDataStreamProvider>(provider);
                byte[] buffer = Convert.FromBase64String(provider.FormData[0].Trim());
                MemoryStream memoryStream = new MemoryStream(buffer, 0, buffer.Length);
                memoryStream.Write(buffer, 0, buffer.Length);
                memoryStream.Seek(0L, SeekOrigin.Begin);
                Respuesta = AuditoriaController.response.Set_CrearAuditoriaImagenes(Lista, UsuarioAuditoriaImgMobile);
                Bitmap bitmap = new Bitmap((Stream)memoryStream);
                Respuesta.data = (object)(NuevaImagen.AIM_ID.ToString() + "." + NuevaImagen.AIM_RUTA);
                if (Respuesta.errNumber == 0)
                { 
                string filename = HostingEnvironment.MapPath(fileuploadPath + "//" + Respuesta.data);
                bitmap.Save(filename, bitmap.RawFormat);
                }
                Lista = (List<ActivosImagenes>)null;
                NuevaImagen = (ActivosImagenes)null;
                fileuploadPath = (string)null;
                provider = (MultipartFormDataStreamProvider)null;
            }
            catch (Exception ex)
            {
                Respuesta.data = (object)ex.Message;
            }
            return Respuesta;
        }

        [HttpPost]
        public Mensaje Set_Cerrar_Toma(Guid ATO_ID)
        {
            return AuditoriaController.response.Set_Cerrar_Toma(ATO_ID);
        }

        [HttpGet]
        public AuditoriaTomaDatos Get_list_TomaDatosPor_Auditoria(Guid? AUD_ID_TOMA)
        {
            return AuditoriaController.response.Get_list_TomaDatosPor_Auditoria(AUD_ID_TOMA);
        }
        [HttpPost]
        public Mensaje Set_Ajuste_Auditoria(Guid AUD_ID, Guid USU_ID)
        {
            return AuditoriaController.response.Set_Ajuste_Auditoria(AUD_ID, USU_ID);
        }
        [HttpPost]
        public Mensaje Set_Liberar_Auditoria(Guid AUD_ID_LIBERAR)
        {
            return AuditoriaController.response.Set_Liberar_Auditoria(AUD_ID_LIBERAR);
        }
        [HttpPost]
        public Mensaje Set_Eliminar_Auditoria(Guid? AUD_ID, Guid? AUD_USUARIO_DELETE_ID)
        {
            return AuditoriaController.response.Set_Eliminar_Auditoria(AUD_ID, AUD_USUARIO_DELETE_ID);
        }
        [HttpPost]
        public Mensaje Set_SeleccionarActivos_Toma([FromBody] SeleccionActivoToma Toma, string seleccion)
        {
            return AuditoriaController.response.Set_SeleccionarActivos_Toma(Toma);
        }
    }
   
}
