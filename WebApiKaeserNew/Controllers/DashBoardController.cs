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
    public class DashBoardController : ApiController
    {
        private static readonly InformesDataBase response = new InformesDataBase();
        private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
        [HttpGet]
        public IEnumerable<Transaccion> Get_list_Informe_Prestamos(Guid? TAC_ID, Guid? RES_ID,int Vencidos,string Fecha_Inicial, string Fecha_Final)
        {
            return response.Get_list_Informe_Prestamos(TAC_ID,RES_ID,Vencidos,helper.Fecha(Fecha_Inicial), helper.Fecha(Fecha_Final));
        }
        [HttpGet]
        public IEnumerable<Activos> Get_list_Informe_activos(Guid? ARE_ID, Guid? TAC_ID, Guid? EDA_ID, Guid? RES_ID, bool? ES_RETORNABLE, string INGRESO, string SALIDA)
        {
            return response.Get_list_Informe_activos(ARE_ID, TAC_ID, EDA_ID, RES_ID, ES_RETORNABLE, helper.Fecha(INGRESO), helper.Fecha(SALIDA));
        }
        
        [HttpGet]
        public IEnumerable<Dasboard> Get_list_Dashboard_ActivosFijos_Retornable(string TipoA)
        {
            switch (TipoA)
            {
                case "0": return response.Get_list_Dashboard_ActivosFijos();
                case "1": return response.Get_list_Dashboard_Retornables_Diario();
                default : return response.Get_list_Dashboard_Retornables_Status ();
            }
            
        }
        [HttpGet]
        public IEnumerable<Garantias> Get_list_InformesPersonalizados_Garantias_Dotaciones(string TipoB)
        {
            switch (TipoB)
            {
                case "0": return response.Get_list_InformesPersonalizados_Garantias();
                default: return response.Get_list_InformesPersonalizados_Dotaciones();
            }

        }
        [HttpGet]
        public IEnumerable<RentaFlota> Get_list_InformesPersonalizados_EquiposDisponibles_Renta(string TipoC)
        {
            switch (TipoC)
            {
                case "0": return response.Get_list_InformesPersonalizados_EquiposDisponibles();
                default: return response.Get_list_InformesPersonalizados_Renta();
            }

        }
        [HttpGet]
        public List<MetrosCuadrados> Get_list_InformesPersonalizados_MetrosCuadrados(string FechaInicio, string FechaFin, Guid? Area)
        {
            return response.Get_list_InformesPersonalizados_MetrosCuadrados(helper.Fecha(FechaInicio), helper.Fecha(FechaFin), Area);
        }
        [HttpGet]
        public List<ServicioTecnicoInforme> Get_list_InformesPersonalizados_ServicioTecnico(Guid? STE_ID)
        {
            return response.Get_list_InformesPersonalizados_ServicioTecnico(STE_ID);
        }
        [HttpGet]
        public List<Activos> Get_list_Informes_etiquetas(bool? Disponible)
        {
            return response.Get_list_Informes_etiquetas(Disponible);
        }
        [HttpGet]
        public List<ActivoDisponible> Get_list_InformesPersonalizados_ActivosDisponibles()
        {
           return response.Get_list_InformesPersonalizados_ActivosDisponibles();
        }
    }
}
