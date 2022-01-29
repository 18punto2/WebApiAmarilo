// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Controllers.SeguridadController
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
  public class SeguridadController : ApiController
  {
    private static readonly SeguridadDataBase response = new SeguridadDataBase();
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();

    [HttpGet]
    public IEnumerable<Usuarios> Get_validar_usuarios(
      string Login,
      string Password)
    {
      return SeguridadController.response.Get_validar_usuarios(Login, Password);
    }

    [HttpGet]
    public IEnumerable<Menus> Get_list_permisosMenu(Guid? ROL_ID)
    {
      List<URL> listPermisosMenu = SeguridadController.response.Get_list_permisosMenu(ROL_ID);
      List<Menus> menusList = new List<Menus>();
      foreach (URL url in listPermisosMenu.FindAll((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(Guid.Parse("00000000-0000-0000-0000-000000000000")))))
      {
        URL area = url;
        Menus menus = new Menus();
        menus.id = area.URL_ID;
        menus.text = area.URL_DESC;
        menus.state = new Nodos()
        {
          opened = true,
          selected = area.PRR_ISACTIVE
        };
        menus.Seleccionado = area.PRR_ISACTIVE;
        menus.URL = area.URL_LINK;
        if (listPermisosMenu.Exists((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(area.URL_ID))))
        {
          menus.children = new List<Menus>();
          this.ArmarArbolMenu(menus.children, menus.id, listPermisosMenu);
        }
        menusList.Add(menus);
      }
      return (IEnumerable<Menus>) menusList;
    }

    [HttpGet]
    public IEnumerable<Rol> Get_list_Roles(Guid? ROL_ID_LISTA,bool? Activos)
    {
      return (IEnumerable<Rol>) SeguridadController.response.Get_list_Roles(ROL_ID_LISTA, Activos);
    }

        [HttpGet]
        public IEnumerable<Rol> Get_list_RolesTodos(Guid? ROL_ID_LISTA)
        {
            return (IEnumerable<Rol>)SeguridadController.response.Get_list_Roles(ROL_ID_LISTA, null);
        }

        [HttpPost]
    public Mensaje Set_Crear_Roles([FromBody] Rol NuevoRol, Guid UsuarioRolCrear)
    {
      return SeguridadController.response.Set_Crear_Roles(new List<Rol>()
      {
        NuevoRol
      }, UsuarioRolCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_Roles([FromBody] Rol EditarRol, Guid UsuarioRolEditar)
    {
      return SeguridadController.response.Set_Editar_Roles(new List<Rol>()
      {
        EditarRol
      }, UsuarioRolEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_Roles(Guid EliminarRol, Guid UsuarioRolEliminar)
    {
      return SeguridadController.response.Set_Eliminar_Roles(new List<Guid>()
      {
        EliminarRol
      }, UsuarioRolEliminar);
    }

    [HttpGet]
    public IEnumerable<Menus> Get_list_permisos(Guid? ROL_ID_PERMISO)
    {
      List<URL> listPermisos = SeguridadController.response.Get_list_permisos(ROL_ID_PERMISO);
      List<Menus> menusList = new List<Menus>();
      List<Guid> Existe = new List<Guid>();
      foreach (URL url in listPermisos.FindAll((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(Guid.Parse("00000000-0000-0000-0000-000000000000")))))
      {
        URL area = url;
        if (!Existe.Exists((Predicate<Guid>) (a => a.Equals(area.URL_ID))))
        {
          Menus menus = new Menus();
          menus.id = area.URL_ID;
          menus.text = area.URL_DESC;
          menus.state = new Nodos()
          {
            opened = true,
            selected = area.URL_PRR_ISACTIVE
          };
          menus.Seleccionado = area.URL_PRR_ISACTIVE;
          menus.URL = area.URL_LINK;
          Existe.Add(menus.id);
          if (listPermisos.Exists((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(area.URL_ID))))
          {
            menus.children = new List<Menus>();
            this.ArmarArbolRol(menus.children, menus.id, listPermisos, Existe);
          }
          menusList.Add(menus);
        }
      }
      return (IEnumerable<Menus>) menusList;
    }

    [HttpGet]
    public IEnumerable<URL> Get_list_Acceso(Guid? ROL_ID_PERMISO, Guid? URL_ID)
    {
      return (IEnumerable<URL>) SeguridadController.response.Get_list_permisos(ROL_ID_PERMISO).FindAll((Predicate<URL>) (t =>
      {
        Guid urlId = t.URL_ID;
        Guid? nullable = URL_ID;
        return nullable.HasValue && urlId == nullable.GetValueOrDefault();
      }));
    }

    [HttpGet]
    public IEnumerable<Usuarios> Get_list_Usuarios(Guid? USU_ID)
    {
      return SeguridadController.response.Get_list_Usuarios(USU_ID);
    }

    [HttpPost]
    public Mensaje Set_Crear_Usuarios([FromBody] Usuarios NuevoUsuario, Guid UsuarioCrear)
    {
      return SeguridadController.response.Set_Crear_Usuarios(new List<Usuarios>()
      {
        NuevoUsuario
      }, UsuarioCrear);
    }

    [HttpPost]
    public Mensaje Set_Editar_Usuarios([FromBody] Usuarios EditarUsuario, Guid UsuarioEditar)
    {
      return SeguridadController.response.Set_Editar_Usuarios(new List<Usuarios>()
      {
        EditarUsuario
      }, UsuarioEditar);
    }

    [HttpPost]
    public Mensaje Set_Eliminar_Usuarios(Guid EliminarUsuario, Guid UsuarioEliminar)
    {
      return SeguridadController.response.Set_Eliminar_Usuarios(new List<Guid>()
      {
        EliminarUsuario
      }, UsuarioEliminar);
    }

    [HttpGet]
    public IEnumerable<Menus> Get_list_UsuariosArea(Guid? USU_ID_AREA)
    {
      List<Areas> listUsuariosArea = SeguridadController.response.Get_list_UsuariosArea(USU_ID_AREA);
      List<Menus> menusList = new List<Menus>();
      foreach (Areas areas in listUsuariosArea.FindAll((Predicate<Areas>) (t => !t.ARE_ARE_PARENT_ID.HasValue || t.ARE_ARE_PARENT_ID.Equals((object) Guid.Parse("00000000-0000-0000-0000-000000000000")))))
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
        if (listUsuariosArea.Exists((Predicate<Areas>) (t => t.ARE_ARE_PARENT_ID.Equals((object) area.ARE_ID))))
        {
          menus.children = new List<Menus>();
          this.helper.ArmarArbol(menus.children, menus.id, listUsuariosArea);
        }
        menusList.Add(menus);
      }
      return (IEnumerable<Menus>) menusList;
    }

    [HttpGet]
    public IEnumerable<Menus> Get_list_UsuariosAreaOrigen(Guid? USU_ID_AREA_ORIGEN)
    {
      List<Areas> listUsuariosArea = SeguridadController.response.Get_list_UsuariosArea(USU_ID_AREA_ORIGEN);
      List<Menus> menusList = new List<Menus>();
      List<Areas> ListaFinal = new List<Areas>();
      List<Guid?> ListaAreasPadre = new List<Guid?>();
      foreach (Areas areas in listUsuariosArea)
      {
        Areas area = areas;
        if (!listUsuariosArea.Exists((Predicate<Areas>) (t =>
        {
          Guid? areAreParentId = t.ARE_ARE_PARENT_ID;
          Guid areId = area.ARE_ID;
          if (!areAreParentId.HasValue)
            return false;
          return !areAreParentId.HasValue || areAreParentId.GetValueOrDefault() == areId;
        })) && area.ARE_ACTIVE)
        {
          ListaFinal.Add(area);
          if (!ListaAreasPadre.Exists((Predicate<Guid?>) (a => a.Equals((object) area.ARE_ARE_PARENT_ID))))
            ListaAreasPadre.Add(area.ARE_ARE_PARENT_ID);
        }
      }
      this.helper.AgregarAreaPadre(ListaAreasPadre, listUsuariosArea, ref ListaFinal);
      foreach (Areas areas in ListaFinal.FindAll((Predicate<Areas>) (t => !t.ARE_ARE_PARENT_ID.HasValue || t.ARE_ARE_PARENT_ID.Equals((object) Guid.Parse("00000000-0000-0000-0000-000000000000")))))
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
        if (ListaFinal.Exists((Predicate<Areas>) (t => t.ARE_ARE_PARENT_ID.Equals((object) area.ARE_ID))))
        {
          menus.children = new List<Menus>();
          this.ArmarArbolOrigen(menus.children, menus.id, ListaFinal);
        }
        menusList.Add(menus);
      }
      return (IEnumerable<Menus>) menusList;
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

    private void ArmarArbolMenu(List<Menus> Padre, Guid PadreID, List<URL> Lista)
    {
      List<URL> urlList = Lista;
      foreach (URL url in urlList.FindAll((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(PadreID))))
      {
        URL area = url;
        Menus menus = new Menus();
        menus.id = area.URL_ID;
        menus.text = area.URL_DESC;
        menus.URL = area.URL_LINK;
        menus.state = new Nodos()
        {
          opened = true,
          selected = area.PRR_ISACTIVE
        };
        menus.Seleccionado = area.PRR_ISACTIVE;
        if (Lista.Exists((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(area.URL_ID))))
        {
          menus.children = new List<Menus>();
          this.ArmarArbolMenu(menus.children, menus.id, Lista.FindAll((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(area.URL_ID))));
        }
        Padre.Add(menus);
      }
    }

    private void ArmarArbolRol(
      List<Menus> Padre,
      Guid PadreID,
      List<URL> Lista,
      List<Guid> Existe)
    {
      List<URL> urlList1 = Lista;
      foreach (URL url1 in urlList1.FindAll((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(PadreID))))
      {
        URL area = url1;
        if (!Existe.Exists((Predicate<Guid>) (a => a.Equals(area.URL_ID))))
        {
          Menus Hijo = new Menus();
          Hijo.id = area.URL_ID;
          Hijo.text = area.URL_DESC;
          Hijo.URL = area.URL_LINK;
          Nodos nodos = new Nodos();
          nodos.opened = true;
          nodos.selected = area.URL_PRR_ISACTIVE;
          if (Lista.Exists((Predicate<URL>) (t => t.URL_ID.Equals(Hijo.id))))
          {
            List<URL> urlList2 = Lista;
            foreach (URL url2 in urlList2.FindAll((Predicate<URL>) (t => t.URL_ID.Equals(Hijo.id))))
            {
              if (!url2.URL_PRR_ISACTIVE)
              {
                nodos.selected = false;
                break;
              }
            }
          }
          Hijo.state = nodos;
          Hijo.Seleccionado = area.URL_PRR_ISACTIVE;
          Existe.Add(Hijo.id);
          Hijo.children = new List<Menus>();
          if (Lista.Exists((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(area.URL_ID))))
          {
            this.ArmarArbolRol(Hijo.children, Hijo.id, Lista.FindAll((Predicate<URL>) (t => t.URL_URL_PARENT_ID.Equals(area.URL_ID))), Existe);
          }
          else
          {
            List<URL> urlList2 = Lista;
            foreach (URL url2 in urlList2.FindAll((Predicate<URL>) (t => t.URL_ID.Equals(Hijo.id))))
              Hijo.children.Add(new Menus()
              {
                id = url2.PUR_ID,
                text = url2.PER_DESC,
                URL = url2.URL_LINK,
                state = new Nodos()
                {
                  opened = true,
                  selected = url2.URL_PRR_ISACTIVE
                },
                Seleccionado = url2.URL_PRR_ISACTIVE
              });
          }
          Padre.Add(Hijo);
          Existe.Add(Hijo.id);
        }
      }
    }

    [HttpPost]
    public Mensaje Set_Asignar_Area_Usuario(
      [FromBody] Seleccion seleccion,
      Guid Area_ID_Asignar,
      Guid UsuarioArea)
    {
      List<UsuarioArea> ListaArea = new List<UsuarioArea>();
      if (seleccion.SELECCIONADOS != null)
      {
        string seleccionados = seleccion.SELECCIONADOS;
        char[] chArray = new char[1]{ '_' };
        foreach (string input in seleccionados.Split(chArray))
          ListaArea.Add(new UsuarioArea()
          {
            USA_USU_ID = Area_ID_Asignar,
            USA_ARE_ID = Guid.Parse(input),
            USA_ACTIVE = true
          });
      }
      if (seleccion.NO_SELECCIONADOS != null)
      {
        string noSeleccionados = seleccion.NO_SELECCIONADOS;
        char[] chArray = new char[1]{ '_' };
        foreach (string input in noSeleccionados.Split(chArray))
          ListaArea.Add(new UsuarioArea()
          {
            USA_USU_ID = Area_ID_Asignar,
            USA_ARE_ID = Guid.Parse(input),
            USA_ACTIVE = false
          });
      }
      return SeguridadController.response.Set_Asignar_Area_Usuario(ListaArea, Area_ID_Asignar);
    }
  }
}
