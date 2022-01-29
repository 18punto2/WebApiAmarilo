
using System;
using System.Collections.Generic;
using System.Configuration;
using WebApiKaeser.Models;

namespace WebApiKaeser.Helper
{
  public class Helper
  {
    public string cnx()
    {
      return ConfigurationManager.ConnectionStrings["cnxKaeser"].ConnectionString;
    }

    public DateTime? Fecha(string valor)
    {
      DateTime? nullable;
      switch (valor)
      {
        case "":
          nullable = new DateTime?();
          break;
        case null:
          nullable = new DateTime?();
          break;
        default:
          if (valor.Split('/').Length != 3)
          {
            nullable = new DateTime?();
            break;
          }
          try
          {
                        //nullable = new DateTime?(Convert.ToDateTime("23/12/2010"));
                        //nullable = new DateTime?(Convert.ToDateTime(valor.Split('/')[0] + "/" + valor.Split('/')[1] + "/" + valor.Split('/')[2]));
                        nullable = new DateTime?(Convert.ToDateTime(valor.Split('/')[2] + "-" + valor.Split('/')[1] + "-" + valor.Split('/')[0]));
                        break;
          }
          catch
          {
            nullable = new DateTime?(Convert.ToDateTime(valor.Split('/')[1] + "/" + valor.Split('/')[0] + "/" + valor.Split('/')[2]));
            break;
          }
      }
      return nullable;
    }

    public void ArmarArbol(List<Menus> Padre, Guid PadreArea, List<Areas> ListaAreas)
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
           if (menus.children.Exists(a=>a.Seleccionado))  menus.Seleccionado = true;
        }
        Padre.Add(menus);
      }
    }

    public void AgregarAreaPadre(
      List<Guid?> ListaAreasPadre,
      List<Areas> ListaDondeBuscar,
      ref List<Areas> ListaFinal)
    {
      List<Guid?> ListaAreasPadre1 = new List<Guid?>();
      foreach (Guid? nullable1 in ListaAreasPadre)
      {
        Guid? nullable2 = nullable1;
        Guid area = nullable2.Value;
        if (!ListaFinal.Exists((Predicate<Areas>) (t => t.ARE_ID == area)))
          ListaFinal.Add(ListaDondeBuscar.Find((Predicate<Areas>) (t => t.ARE_ID == area)));
        nullable2 = ListaDondeBuscar.Find((Predicate<Areas>) (t => t.ARE_ID == area)).ARE_ARE_PARENT_ID;
        if (nullable2.HasValue)
        {
          nullable2 = ListaDondeBuscar.Find((Predicate<Areas>) (t => t.ARE_ID == area)).ARE_ARE_PARENT_ID;
          if (!nullable2.Equals((object) Guid.Parse("00000000-0000-0000-0000-000000000000")) && !ListaAreasPadre1.Exists((Predicate<Guid?>) (a => a.Equals((object) ListaDondeBuscar.Find((Predicate<Areas>) (t => t.ARE_ID == area)).ARE_ARE_PARENT_ID))))
            ListaAreasPadre1.Add(ListaDondeBuscar.Find((Predicate<Areas>) (t => t.ARE_ID == area)).ARE_ARE_PARENT_ID);
        }
      }
      if (ListaAreasPadre1.Count <= 0)
        return;
      this.AgregarAreaPadre(ListaAreasPadre1, ListaDondeBuscar, ref ListaFinal);
    }
  }
}
