// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Controllers.BaseController
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
  public class BaseController : ApiController
  {
    private static readonly BaseDataBase response = new BaseDataBase();

    [HttpGet]
    public IEnumerable<Caracteristica> Get_list_caracteristica(
      Guid? Caracteristica)
    {
      return BaseController.response.Get_list_caracteristica(Caracteristica);
    }

    [HttpGet]
    public IEnumerable<Caracteristica> Get_list_caracteristicaHija(
      Guid? CaracteristicaPadre)
    {
      return BaseController.response.Get_list_caracteristicaHija(CaracteristicaPadre);
    }

    [HttpPost]
    public Mensaje Set_Crear_Caracteristica(
      [FromBody] Caracteristica NuevaCaracteristica,
      Guid UsuarioCaracteristicaCrear)
    {
      return BaseController.response.Set_Crear_Caracteristica(new List<Caracteristica>()
      {
        NuevaCaracteristica
      }, UsuarioCaracteristicaCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_Caracteristica(
      [FromBody] Caracteristica EditarCaracteristica,
      Guid UsuarioCaracteristicaEditar)
    {
      return BaseController.response.Set_Editar_Caracteristica(new List<Caracteristica>()
      {
        EditarCaracteristica
      }, UsuarioCaracteristicaEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_Caracteristica(
      Guid EliminarCaracteristica,
      Guid UsuarioCaracteristicaEliminar)
    {
      return BaseController.response.Set_Eliminar_Caracteristica(new List<Guid>()
      {
        EliminarCaracteristica
      }, UsuarioCaracteristicaEliminar);
    }

    [HttpGet]
    public IEnumerable<CaracteristicaValores> Get_list_caracteristicaValor(
      Guid? CaracteristicaID,
      Guid? CaracteristicaValor)
    {
      return BaseController.response.Get_list_caracteristicaValor(CaracteristicaID, CaracteristicaValor);
    }

    [HttpPost]
    public Mensaje Set_Crear_CaracteristicaValor(
      [FromBody] CaracteristicaValores NuevaCaracteristica,
      Guid UsuarioCaracteValorCrear)
    {
      return BaseController.response.Set_Crear_CaracteristicaValor(new List<CaracteristicaValores>()
      {
        NuevaCaracteristica
      }, UsuarioCaracteValorCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_CaracteristicaValor(
      [FromBody] CaracteristicaValores EditarCaracteristicaValor,
      Guid UsuarioCaracteValorEditar)
    {
      return BaseController.response.Set_Editar_CaracteristicaValor(new List<CaracteristicaValores>()
      {
        EditarCaracteristicaValor
      }, UsuarioCaracteValorEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_CaracteristicaValor(
      Guid EliminarCaracteristicaValor,
      Guid UsuarioCaracteValorEliminar)
    {
      return BaseController.response.Set_Eliminar_CaracteristicaValor(new List<Guid>()
      {
        EliminarCaracteristicaValor
      }, UsuarioCaracteValorEliminar);
    }

    [HttpGet]
    public IEnumerable<Areas> Get_list_Area(Guid? Area)
    {
      return (IEnumerable<Areas>) BaseController.response.Get_list_Area(Area, new Guid?(), new Guid?());
    }
    void AgregarAreaPadre(List<Areas> listArea, Guid? ID, ref List<Areas> listAreaSertec)
    {
            foreach (Areas areas in listArea.FindAll(t => t.ARE_ID == ID))
            {
                if (!listAreaSertec.Exists(a => a.ARE_ID == ID)) listAreaSertec.Add(areas);
                if (areas.ARE_ARE_PARENT_ID.HasValue)
                {
                    AgregarAreaPadre(listArea, areas.ARE_ARE_PARENT_ID, ref listAreaSertec);
                }
            }
    }

    [HttpGet]
    public List<Menus> Get_list_AreaServicioTecnico(string Sertec)
    {
        List<Areas> listArea = BaseController.response.Get_list_Area(new Guid?(), new Guid?(), new Guid?());
        List<Areas> listAreaSertec = new List<Areas>();
            //solo las areas que son de servicio tecnico
            if (Sertec=="1")
                foreach (Areas areas in listArea.FindAll(t => t.ES_ST))
                {
                    listAreaSertec.Add(areas);
                    if (areas.ARE_ARE_PARENT_ID.HasValue)
                    {
                            AgregarAreaPadre(listArea, areas.ARE_ARE_PARENT_ID, ref listAreaSertec);
                    }
                }
             else if(Sertec == "2")//se mide por metros cuadrados
                     foreach (Areas areas in listArea.FindAll(t => t.ES_ST || t.ARE_IS_RENTA))
                        {
                            listAreaSertec.Add(areas);
                            if (areas.ARE_ARE_PARENT_ID.HasValue)
                            {
                                AgregarAreaPadre(listArea, areas.ARE_ARE_PARENT_ID, ref listAreaSertec);
                            }
                        }
            //
            foreach (Areas areas in listAreaSertec)
      {
        if (!areas.ARE_ARE_PARENT_ID.Equals((object) Guid.Parse("00000000-0000-0000-0000-000000000000")))
        {
          areas.SELECCIONADO = true;
          this.SeleccionarAreaPadre(ref listArea, areas.ARE_ARE_PARENT_ID);
        }
      }
      List<Menus> menusList = new List<Menus>();
      foreach (Areas areas in listAreaSertec.FindAll((Predicate<Areas>) (t => !t.ARE_ARE_PARENT_ID.HasValue || t.ARE_ARE_PARENT_ID.Equals((object) Guid.Parse("00000000-0000-0000-0000-000000000000")))))
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
        if (listAreaSertec.Exists((Predicate<Areas>) (t => t.ARE_ARE_PARENT_ID.Equals((object) area.ARE_ID))))
        {
          menus.children = new List<Menus>();
          this.ArmarArbol(menus.children, menus.id, listAreaSertec);
        }
        menusList.Add(menus);
      }
      return menusList;
    }

    [HttpGet]
    public IEnumerable<Areas> Get_list_AreaxUsuario(Guid? Area, Guid? USU_ID)
    {
      return (IEnumerable<Areas>) BaseController.response.Get_list_Area(Area, USU_ID, new Guid?());
    }

    private void SeleccionarAreaPadre(ref List<Areas> lstdata, Guid? Hija)
    {
      List<Areas> areasList = lstdata;
      foreach (Areas areas in areasList.FindAll((Predicate<Areas>) (a => a.ARE_ID.Equals((object) Hija))))
      {
        areas.SELECCIONADO = true;
        if (!areas.ARE_ARE_PARENT_ID.Equals((object) Guid.Parse("00000000-0000-0000-0000-000000000000")))
          this.SeleccionarAreaPadre(ref lstdata, areas.ARE_ARE_PARENT_ID);
      }
    }

    [HttpGet]
    public List<Menus> Get_list_AreaxResponsable(Guid? Area_Res_ID)
    {
      List<Areas> listArea = BaseController.response.Get_list_Area(new Guid?(), new Guid?(), Area_Res_ID);
      foreach (Areas areas in listArea.FindAll((Predicate<Areas>) (t => t.SELECCIONADO)))
      {
        if (!areas.ARE_ARE_PARENT_ID.Equals((object) Guid.Parse("00000000-0000-0000-0000-000000000000")))
        {
          areas.SELECCIONADO = true;
          this.SeleccionarAreaPadre(ref listArea, areas.ARE_ARE_PARENT_ID);
        }
      }
      listArea.RemoveAll((Predicate<Areas>) (t => !t.SELECCIONADO));
      List<Menus> menusList = new List<Menus>();
      foreach (Areas areas in listArea.FindAll((Predicate<Areas>) (t => !t.ARE_ARE_PARENT_ID.HasValue || t.ARE_ARE_PARENT_ID.Equals((object) Guid.Parse("00000000-0000-0000-0000-000000000000")))))
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
        if (listArea.Exists((Predicate<Areas>) (t => t.ARE_ARE_PARENT_ID.Equals((object) area.ARE_ID))))
        {
          menus.children = new List<Menus>();
          this.ArmarArbol(menus.children, menus.id, listArea);
        }
        menusList.Add(menus);
      }
      return menusList;
    }

    [HttpPost]
    public Mensaje Set_Crear_Area([FromBody] Areas NuevaArea, Guid UsuarioArea)
    {
      return BaseController.response.Set_Crear_Area(new List<Areas>()
      {
        NuevaArea
      }, UsuarioArea);
    }

    [HttpPost]
    public Mensaje Set_Editar_Area([FromBody] Areas EditarArea, Guid UsuarioAreaEditar)
    {
      return BaseController.response.Set_Editar_Area(new List<Areas>()
      {
        EditarArea
      }, UsuarioAreaEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_Area(Guid EliminarArea, Guid UsuarioAreaEliminar)
    {
      return BaseController.response.Set_Eliminar_Area(new List<Guid>()
      {
        EliminarArea
      }, UsuarioAreaEliminar);
    }

    [HttpGet]
    public IEnumerable<TipoActivo> Get_list_TipoActivo_Area(
      Guid? ID_AreaTipoActivo)
    {
      return BaseController.response.Get_list_TipoActivo_Area(ID_AreaTipoActivo);
    }

    [HttpGet]
    public IEnumerable<TipoArea> Get_list_TipoArea(string TipoArea)
    {
      return BaseController.response.Get_list_TipoArea(TipoArea);
    }

    [HttpPost]
    public Mensaje Set_Mant_Activos(
      [FromBody] Seleccion MantTipoActivo,
      Guid AreaUsuario,
      Guid UsuarioArea)
    {
      List<TipoActivoArea> MantTipoActivo1 = new List<TipoActivoArea>();
      if (MantTipoActivo.SELECCIONADOS != null)
      {
        string seleccionados = MantTipoActivo.SELECCIONADOS;
        char[] chArray = new char[1]{ '_' };
        foreach (string input in seleccionados.Split(chArray))
          MantTipoActivo1.Add(new TipoActivoArea()
          {
            TAU_TAC_ID = Guid.Parse(input),
            TAU_ARE_ID = AreaUsuario,
            TAU_ACTIVE = true
          });
      }
      if (MantTipoActivo.NO_SELECCIONADOS != null)
      {
        string noSeleccionados = MantTipoActivo.NO_SELECCIONADOS;
        char[] chArray = new char[1]{ '_' };
        foreach (string input in noSeleccionados.Split(chArray))
          MantTipoActivo1.Add(new TipoActivoArea()
          {
            TAU_TAC_ID = Guid.Parse(input),
            TAU_ARE_ID = AreaUsuario,
            TAU_ACTIVE = false
          });
      }
      return BaseController.response.Set_Mant_Activos(MantTipoActivo1, UsuarioArea);
    }

    [HttpGet]
    public IEnumerable<ActivoValoresCaracteristicas> Get_list_activosValor(
      Guid MOD_ID,
      Guid? ACT_ID)
    {
      return BaseController.response.Get_list_activosValor(MOD_ID, ACT_ID);
    }

    [HttpGet]
    public IEnumerable<Estados> Get_list_Estados(Guid? EST_ID)
    {
      return BaseController.response.Get_list_Estados(EST_ID);
    }
    [HttpGet]
    public IEnumerable<Estados> Get_list_EstadosDisponibilidad(string DISPONIBLE)
    {
        return BaseController.response.Get_list_EstadosDisponibilidad();
    }
    [HttpGet]
    public IEnumerable<Modelo> Get_list_Modelo(Guid? Modelos)
    {
      return BaseController.response.Get_list_Modelo(Modelos,null);
    }
        [HttpGet]
        public IEnumerable<Modelo> Get_list_Modelo_FECHA_MOD(DateTime? MOD_FECHA_MOD)
        {
            return BaseController.response.Get_list_Modelo(new Guid (),MOD_FECHA_MOD);
        }
        [HttpPost]
    public Mensaje Set_Crear_Modelo([FromBody] Modelo NuevaModelo, Guid UsuarioModeloCrear)
    {
      return BaseController.response.Set_Crear_Modelo(new List<Modelo>()
      {
        NuevaModelo
      }, UsuarioModeloCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_Modelo([FromBody] Modelo EditarModelo, Guid UsuarioModeloEditar)
    {
      return BaseController.response.Set_Editar_Modelo(new List<Modelo>()
      {
        EditarModelo
      }, UsuarioModeloEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_Modelo(Guid EliminarModelo, Guid UsuarioModeloEliminar)
    {
      return BaseController.response.Set_Eliminar_Modelo(new List<Guid>()
      {
        EliminarModelo
      }, UsuarioModeloEliminar);
    }

    [HttpGet]
    public IEnumerable<ModeloValoresCaracteristicas> Get_list_ModeloValoresCaracteristicas(
      Guid TipoActivoID,
      Guid? ModeloID)
    {
      return BaseController.response.Get_list_ModeloValoresCaracteristicas(TipoActivoID, ModeloID);
    }

    [HttpGet]
    public IEnumerable<TipoActivo> Get_list_TipoActivo(Guid? TipoActivo)
    {
      return BaseController.response.Get_list_TipoActivo(TipoActivo,null);
    }
    [HttpGet]
    public IEnumerable<TipoActivo> Get_list_TipoActivo_FECHA_MOD(DateTime? TAC_FECHA_MOD)
    {
        return BaseController.response.Get_list_TipoActivo(new Guid (),TAC_FECHA_MOD);
    }
    [HttpPost]
    public Mensaje Set_Crear_TipoActivo([FromBody] TipoActivo NuevaTipoActivo, Guid UsuarioTipoActivo)
    {
      return BaseController.response.Set_Crear_TipoActivo(new List<TipoActivo>()
      {
        NuevaTipoActivo
      }, UsuarioTipoActivo);
    }

    [HttpPost]
    public Mensaje Set_Editar_TipoActivo(
      [FromBody] TipoActivo EditarTipoActivo,
      Guid UsuarioTipoActivoEditar)
    {
      return BaseController.response.Set_Editar_TipoActivo(new List<TipoActivo>()
      {
        EditarTipoActivo
      }, UsuarioTipoActivoEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_TipoActivo(
      Guid EliminarTipoActivo,
      Guid UsuarioTipoActivoEliminar)
    {
      return BaseController.response.Set_Eliminar_TipoActivo(new List<Guid>()
      {
        EliminarTipoActivo
      }, UsuarioTipoActivoEliminar);
    }

    [HttpGet]
    public IEnumerable<Menus> Get_list_TipoActivoCaracteristica(
      Guid TipoActivoCaracteristica,
      bool Todo)
    {
      List<TipoActivoCaracteristicas> activoCaracteristica = BaseController.response.Get_list_TipoActivoCaracteristica(TipoActivoCaracteristica, Todo);
      List<Menus> menusList = new List<Menus>();
      foreach (TipoActivoCaracteristicas activoCaracteristicas in activoCaracteristica.FindAll((Predicate<TipoActivoCaracteristicas>) (t => t.TCA_ID.Equals(Guid.Parse("00000000-0000-0000-0000-000000000000")))))
      {
        TipoActivoCaracteristicas area = activoCaracteristicas;
        Menus menus = new Menus();
        menus.id = area.TCA_CAR_ID;
        menus.text = area.TCA_CAR_NOMBRE;
        menus.state = new Nodos()
        {
          opened = true,
          selected = area.TCA_ASIGNADO
        };
        menus.Seleccionado = area.TCA_ASIGNADO;
        if (activoCaracteristica.Exists((Predicate<TipoActivoCaracteristicas>) (t => t.TCA_ID.Equals(area.TCA_CAR_ID))))
        {
          menus.children = new List<Menus>();
          this.ArmarArbol(menus.children, menus.id, activoCaracteristica);
        }
        menusList.Add(menus);
      }
      return (IEnumerable<Menus>) menusList;
    }

    private void ArmarArbol(
      List<Menus> Padre,
      Guid PadreArea,
      List<TipoActivoCaracteristicas> ListaAreas)
    {
      List<TipoActivoCaracteristicas> activoCaracteristicasList = ListaAreas;
      foreach (TipoActivoCaracteristicas activoCaracteristicas in activoCaracteristicasList.FindAll((Predicate<TipoActivoCaracteristicas>) (t => t.TCA_ID.Equals(PadreArea))))
      {
        TipoActivoCaracteristicas area = activoCaracteristicas;
        Menus menus = new Menus();
        menus.id = area.TCA_CAR_ID;
        menus.text = area.TCA_CAR_NOMBRE;
        menus.state = new Nodos()
        {
          opened = true,
          selected = area.TCA_ASIGNADO
        };
        menus.Seleccionado = area.TCA_ASIGNADO;
        if (ListaAreas.Exists((Predicate<TipoActivoCaracteristicas>) (t => t.TCA_ID.Equals(area.TCA_CAR_ID))))
        {
          menus.children = new List<Menus>();
          this.ArmarArbol(menus.children, menus.id, ListaAreas.FindAll((Predicate<TipoActivoCaracteristicas>) (t => t.TCA_ID.Equals(area.TCA_CAR_ID))));
        }
        Padre.Add(menus);
      }
    }

    [HttpPost]
    public Mensaje Set_Asociar_TipoActivoCaracteristicas(
      [FromBody] Seleccion seleccion,
      Guid ID_TipoActivo_Caracteristica,
      Guid USUARIO_TipoActivo_Caracteristica)
    {
      List<TipoActivoCaracteristicas> ListaTipoActivoCarateristica = new List<TipoActivoCaracteristicas>();
      if (seleccion.SELECCIONADOS != null)
      {
        string seleccionados = seleccion.SELECCIONADOS;
        char[] chArray = new char[1]{ '_' };
        foreach (string input in seleccionados.Split(chArray))
          ListaTipoActivoCarateristica.Add(new TipoActivoCaracteristicas()
          {
            TCA_TAC_ID = ID_TipoActivo_Caracteristica,
            TCA_CAR_ID = Guid.Parse(input),
            TCA_ASIGNADO = true
          });
      }
      if (seleccion.NO_SELECCIONADOS != null)
      {
        string noSeleccionados = seleccion.NO_SELECCIONADOS;
        char[] chArray = new char[1]{ '_' };
        foreach (string input in noSeleccionados.Split(chArray))
        {
          if (seleccion.SELECCIONADOS == null)
            ListaTipoActivoCarateristica.Add(new TipoActivoCaracteristicas()
            {
              TCA_TAC_ID = ID_TipoActivo_Caracteristica,
              TCA_CAR_ID = Guid.Parse(input),
              TCA_ASIGNADO = false
            });
          else if (!((IEnumerable<string>) seleccion.SELECCIONADOS.Split(',')).Contains<string>(input))
            ListaTipoActivoCarateristica.Add(new TipoActivoCaracteristicas()
            {
              TCA_TAC_ID = ID_TipoActivo_Caracteristica,
              TCA_CAR_ID = Guid.Parse(input),
              TCA_ASIGNADO = false
            });
        }
      }
      return BaseController.response.Set_Asociar_TipoActivoCaracteristicas(ListaTipoActivoCarateristica, ID_TipoActivo_Caracteristica, USUARIO_TipoActivo_Caracteristica);
    }

    [HttpGet]
    public IEnumerable<Responsable> Get_list_responsable(Guid? RES_ID)
    {
      return BaseController.response.Get_list_Responsable(RES_ID, (string) null, new Guid?());
    }

    [HttpGet]
    public IEnumerable<Responsable> Get_list_responsableCombo(Guid? RES_ID,string LISTA)
    {            
        return BaseController.response.Get_list_ResponsableLista(RES_ID, (string)null, new Guid?());
    }

        [HttpPost]
    public IEnumerable<ListaDesplegable> Get_list_responsableDesc(
      [FromBody] ListaDesplegable responsable,
      int Tipo,
      Guid? Area_ID)
    {
      List<ListaDesplegable> listaDesplegableList = new List<ListaDesplegable>();
      if (responsable == null)
        responsable = new ListaDesplegable();
      foreach (Responsable responsable1 in BaseController.response.Get_list_Responsable(new Guid?(), responsable.text, Area_ID))
                if (responsable1.RES_ACTIVE ==true )
        listaDesplegableList.Add(new ListaDesplegable()
        {
          id = new Guid?(responsable1.RES_ID),
          text = responsable1.RES_NOMBRES + ", " + responsable1.RES_APELLIDOS
        });
      return (IEnumerable<ListaDesplegable>) listaDesplegableList;
    }

    [HttpPost]
    public async Task<Mensaje> Set_Crear_responsable(
      string RES_DOCUMENTO,
      string RES_NOMBRES,
      string RES_APELLIDOS,
      string RES_CARGO,
      string RES_CORREO,
      string RES_CELULAR,
      string RES_TELEFONO,
      string RES_EXT,
      string RES_OBSERVACION,
      bool RES_ACTIVE,
      Guid UsuarioResponsableCrear)
    {
      Mensaje Respuesta = new Mensaje();
      List<Responsable> Lista = new List<Responsable>();
      Responsable Nuevaresponsable = new Responsable();
      Nuevaresponsable.RES_DOCUMENTO = RES_DOCUMENTO;
      Nuevaresponsable.RES_NOMBRES = RES_NOMBRES;
      Nuevaresponsable.RES_APELLIDOS = RES_APELLIDOS;
      Nuevaresponsable.RES_CARGO = RES_CARGO;
      Nuevaresponsable.RES_CORREO = RES_CORREO;
      Nuevaresponsable.RES_CELULAR = RES_CELULAR;
      Nuevaresponsable.RES_TELEFONO = RES_TELEFONO;
      Nuevaresponsable.RES_EXT = RES_EXT;
      Nuevaresponsable.RES_OBSERVACION = RES_OBSERVACION;
      Nuevaresponsable.RES_ACTIVE = RES_ACTIVE;
      try
      {
        string filePath = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["FileUploadLocation"]);
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
            string str = filePath + ("\\" + content2.Headers.ContentDisposition.FileName.Trim('"'));
            if (!str.Split('\\')[str.Split('\\').Length - 1].Equals(""))
            {
              Nuevaresponsable.RES_PHOTO_1 = str.Split('\\')[str.Split('\\').Length - 1].Split('.')[1];
              break;
            }
            break;
          }
        }
        Lista.Add(Nuevaresponsable);
        Respuesta = BaseController.response.Set_Crear_Responsable(Lista, UsuarioResponsableCrear);
        if (Respuesta.errNumber == 0)
        {
          string str = filePath + ("\\" + (object) Nuevaresponsable.RES_ID + "." + Nuevaresponsable.RES_PHOTO_1);
          if (File.Exists(str))
            File.Delete(str);
          File.Move(sourceFileName, str);
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
    public async Task<Mensaje> Set_Editar_responsable(
      Guid RES_ID,
      string RES_DOCUMENTO,
      string RES_NOMBRES,
      string RES_APELLIDOS,
      string RES_CARGO,
      string RES_CORREO,
      string RES_CELULAR,
      string RES_TELEFONO,
      string RES_EXT,
      string RES_OBSERVACION,
      bool RES_ACTIVE,
      Guid UsuarioResponsableEditar)
    {
      string originalFileName = "";
      Mensaje Respuesta = new Mensaje();
      List<Responsable> Lista = new List<Responsable>();
      Responsable Editarresponsable = new Responsable();
      Editarresponsable.RES_ID = RES_ID;
      Editarresponsable.RES_DOCUMENTO = RES_DOCUMENTO;
      Editarresponsable.RES_NOMBRES = RES_NOMBRES;
      Editarresponsable.RES_APELLIDOS = RES_APELLIDOS;
      Editarresponsable.RES_CARGO = RES_CARGO;
      Editarresponsable.RES_CORREO = RES_CORREO;
      Editarresponsable.RES_CELULAR = RES_CELULAR;
      Editarresponsable.RES_TELEFONO = RES_TELEFONO;
      Editarresponsable.RES_EXT = RES_EXT;
      Editarresponsable.RES_OBSERVACION = RES_OBSERVACION;
      Editarresponsable.RES_ACTIVE = RES_ACTIVE;
      try
      {
        string filePath = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["FileUploadLocation"]);
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
              Editarresponsable.RES_PHOTO_1 = ((ValueType) Editarresponsable.RES_ID).ToString() + "." + originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1].Split('.')[1];
              break;
            }
            break;
          }
        }
        Lista.Add(Editarresponsable);
        Respuesta = BaseController.response.Set_Editar_Responsable(Lista, UsuarioResponsableEditar);
        if (Respuesta.errNumber == 0)
        {
          if (!originalFileName.Split('\\')[originalFileName.Split('\\').Length - 1].Equals(""))
          {
            originalFileName = filePath + ("\\" + Editarresponsable.RES_PHOTO_1);
            if (File.Exists(originalFileName))
              File.Delete(originalFileName);
            File.Move(sourceFileName, originalFileName);
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
    public Mensaje Set_Eliminar_responsable(
      Guid Eliminarresponsable,
      Guid UsuarioResponsableEliminar)
    {
      string appSetting = ConfigurationManager.AppSettings["FileUploadLocation"];
      Mensaje mensaje1 = new Mensaje();
      Mensaje mensaje2 = BaseController.response.Set_Eliminar_Responsable(new List<Guid>()
      {
        Eliminarresponsable
      }, UsuarioResponsableEliminar);
      if (mensaje2.errNumber == 0 && mensaje2.data != null)
        File.Delete(HostingEnvironment.MapPath(appSetting + "//" + Convert.ToString(mensaje2.data)));
      return mensaje2;
    }

    [HttpGet]
    public IEnumerable<AreaResponsable> Get_list_ResponsablesAreas(
      Guid? RES_ID,
      Guid? ARE_ID)
    {
      return BaseController.response.Get_list_ResponsablesAreas(RES_ID, ARE_ID);
    }

    [HttpPost]
    public Mensaje Set_Mant_AreaResponsable(
      [FromBody] Seleccion MantResponsable,
      Guid AREA_ID,
      Guid USUARIO_AREA)
    {
      List<AreaResponsable> Mant_AreaResponsable = new List<AreaResponsable>();
      if (MantResponsable.SELECCIONADOS != null)
      {
        string seleccionados = MantResponsable.SELECCIONADOS;
        char[] chArray = new char[1]{ '_' };
        foreach (string input in seleccionados.Split(chArray))
          Mant_AreaResponsable.Add(new AreaResponsable()
          {
            RES_ID = Guid.Parse(input),
            RAR_ARE_ID = AREA_ID,
            SELECCIONADO = true
          });
      }
      if (MantResponsable.NO_SELECCIONADOS != null)
      {
        string noSeleccionados = MantResponsable.NO_SELECCIONADOS;
        char[] chArray = new char[1]{ '_' };
        foreach (string input in noSeleccionados.Split(chArray))
          Mant_AreaResponsable.Add(new AreaResponsable()
          {
            RAR_ID = Guid.Parse(input),
            RAR_ARE_ID = AREA_ID,
            SELECCIONADO = false
          });
      }
      return BaseController.response.Set_Mant_AreaResponsable(Mant_AreaResponsable, USUARIO_AREA);
    }

    [HttpGet]
    public IEnumerable<Cliente> Get_list_Clientes(Guid? CLI_ID)
    {
      return BaseController.response.Get_list_Clientes(CLI_ID, (string) null);
    }
    [HttpGet]
    public IEnumerable<Cliente> Get_list_Clientes_FECHA_MOD(DateTime? CLI_FECHA_MOD)
    {
        return BaseController.response.Get_list_Clientes(null, (string)null, CLI_FECHA_MOD);
    }
    [HttpPost]
    public IEnumerable<ListaDesplegable> Get_list_ClientesDesc(
      [FromBody] ListaDesplegable cliente,
      string CLIENTE_ID)
    {
      List<ListaDesplegable> listaDesplegableList = new List<ListaDesplegable>();
      if (cliente == null)
        cliente = new ListaDesplegable();
      foreach (Cliente listCliente in BaseController.response.Get_list_Clientes(new Guid?(), cliente.text))
        listaDesplegableList.Add(new ListaDesplegable()
        {
          id = new Guid?(listCliente.CLI_ID),
          text = listCliente.CLI_NOMBRE
        });
      return (IEnumerable<ListaDesplegable>) listaDesplegableList;
    }

    [HttpPost]
    public Mensaje Set_Crear_Clientes([FromBody] Cliente NuevoCliente, Guid UsuarioCliente)
    {
      return BaseController.response.Set_Crear_Clientes(new List<Cliente>()
      {
        NuevoCliente
      }, UsuarioCliente);
    }

    [HttpPost]
    public Mensaje Set_Editar_Clientes([FromBody] Cliente EditarCliente, Guid UsuarioClienteEditar)
    {
      return BaseController.response.Set_Editar_Clientes(new List<Cliente>()
      {
        EditarCliente
      }, UsuarioClienteEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_Clientes(Guid EliminarCliente, Guid UsuarioClienteEliminar)
    {
      return BaseController.response.Set_Eliminar_Clientes(new List<Guid>()
      {
        EliminarCliente
      }, UsuarioClienteEliminar);
    }

    [HttpGet]
    public IEnumerable<Proveedor> Get_list_Proveedor(Guid? PRO_ID)
    {
      return BaseController.response.Get_list_Proveedor(PRO_ID, (string) null);
    }

    [HttpPost]
    public IEnumerable<ListaDesplegable> Get_list_ProveedoressDesc(
      [FromBody] ListaDesplegable proveedor)
    {
      List<ListaDesplegable> listaDesplegableList = new List<ListaDesplegable>();
      if (proveedor == null)
        proveedor = new ListaDesplegable();
      foreach (Proveedor proveedor1 in BaseController.response.Get_list_Proveedor(new Guid?(), proveedor.text))
        listaDesplegableList.Add(new ListaDesplegable()
        {
          id = proveedor1.PRO_ID,
          text = proveedor1.PRO_NOMBRE
        });
      return (IEnumerable<ListaDesplegable>) listaDesplegableList;
    }

    [HttpPost]
    public Mensaje Set_Crear_Proveedor([FromBody] Proveedor NuevoProveedor, Guid UsuarioProveedorCrear)
    {
      return BaseController.response.Set_Crear_Proveedor(new List<Proveedor>()
      {
        NuevoProveedor
      }, UsuarioProveedorCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_Proveedor(
      [FromBody] Proveedor EditarProveedor,
      Guid UsuarioProveedorEditar)
    {
      return BaseController.response.Set_Editar_Proveedor(new List<Proveedor>()
      {
        EditarProveedor
      }, UsuarioProveedorEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_Proveedor(
      Guid EliminarProveedor,
      Guid UsuarioProveedorEliminar)
    {
      return BaseController.response.Set_Eliminar_Proveedor(new List<Guid>()
      {
        EliminarProveedor
      }, UsuarioProveedorEliminar);
    }

    [HttpGet]
    public IEnumerable<CentroCosto> Get_list_CentroCosto(Guid? CENTROCOSTO_ID)
    {
      return BaseController.response.Get_list_CentroCosto(CENTROCOSTO_ID, (string) null);
    }

    [HttpPost]
    public IEnumerable<ListaDesplegable> Get_list_CentroCostoDesc(
      [FromBody] ListaDesplegable CentroCosto,
      string CENTRO_ID)
    {
      List<ListaDesplegable> listaDesplegableList = new List<ListaDesplegable>();
      if (CentroCosto == null)
        CentroCosto = new ListaDesplegable();
      foreach (CentroCosto centroCosto in BaseController.response.Get_list_CentroCosto(new Guid?(), CentroCosto.text))
        listaDesplegableList.Add(new ListaDesplegable()
        {
          id = new Guid?(centroCosto.CCO_ID),
          text = centroCosto.CCO_DESC
        });
      return (IEnumerable<ListaDesplegable>) listaDesplegableList;
    }

    [HttpPost]
    public Mensaje Set_Crear_CentroCosto(
      [FromBody] CentroCosto NuevoCentroCosto,
      Guid UsuarioCentroCostoCrear)
    {
      return BaseController.response.Set_Crear_CentroCosto(new List<CentroCosto>()
      {
        NuevoCentroCosto
      }, UsuarioCentroCostoCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_CentroCosto(
      [FromBody] CentroCosto EditarCentroCosto,
      Guid UsuarioCentroCostoEditar)
    {
      return BaseController.response.Set_Editar_CentroCosto(new List<CentroCosto>()
      {
        EditarCentroCosto
      }, UsuarioCentroCostoEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_CentroCosto(
      Guid EliminarCentroCosto,
      Guid UsuarioCentroCostoEliminar)
    {
      return BaseController.response.Set_Eliminar_CentroCosto(new List<Guid>()
      {
        EliminarCentroCosto
      }, UsuarioCentroCostoEliminar);
    }

    private void ArmarArbol(List<Menus> Padre, Guid PadreArea, List<Areas> ListaAreas)
    {
      List<Areas> areasList = ListaAreas;
      foreach (Areas areas in areasList.FindAll((Predicate<Areas>) (t => t.ARE_ARE_PARENT_ID.Equals((object) PadreArea))))
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
        if (ListaAreas.Exists((Predicate<Areas>) (t => t.ARE_ARE_PARENT_ID.Equals((object) area.ARE_ID))))
        {
          menus.children = new List<Menus>();
          this.ArmarArbol(menus.children, menus.id, ListaAreas);
        }
        Padre.Add(menus);
      }
    }

    private void ArmarArbolOrigen(List<Menus> Padre, Guid PadreArea, List<Areas> ListaAreas)
    {
      List<Areas> areasList = ListaAreas;
      foreach (Areas areas in areasList.FindAll((Predicate<Areas>) (t => t.ARE_ARE_PARENT_ID.Equals((object) PadreArea))))
      {
        Areas area = areas;
        Menus menus = new Menus();
        menus.id = area.ARE_ID;
        menus.text = area.ARE_DESC;
        menus.state = new Nodos()
        {
          opened = true,
          selected = false
        };
        menus.Seleccionado = false;
        if (ListaAreas.Exists((Predicate<Areas>) (t => t.ARE_ARE_PARENT_ID.Equals((object) area.ARE_ID))))
        {
          menus.children = new List<Menus>();
          this.ArmarArbolOrigen(menus.children, menus.id, ListaAreas);
        }
        Padre.Add(menus);
      }
    }
  }
}
