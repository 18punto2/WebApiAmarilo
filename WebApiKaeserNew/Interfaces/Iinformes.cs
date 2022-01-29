using System;
using System.Collections.Generic;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
    interface Iinformes
    {
        List<Activos> Get_list_Informe_activos(Guid? ARE_ID, Guid? TAC_ID, Guid? EDA_ID, Guid? RES_ID, bool? ES_RETORNABLE, DateTime? Fecha_Inicial, DateTime? Fecha_Final);

        List<Transaccion> Get_list_Informe_Prestamos(Guid? TAC_ID, Guid? RES_ID, int Vencidos, DateTime? Fecha_Inicial, DateTime? Fecha_Final);

        List<Activos> Get_list_Informes_etiquetas(bool? Disponible);

        List<RentaFlota> Get_list_InformesPersonalizados_Renta();

        List<ServicioTecnicoInforme> Get_list_InformesPersonalizados_ServicioTecnico(Guid? STE_ID);

        List<MetrosCuadrados> Get_list_InformesPersonalizados_MetrosCuadrados(DateTime? FechaInicio, DateTime? FechaFin, Guid? Area);
       
        List<Garantias> Get_list_InformesPersonalizados_Garantias();
        List<Garantias>Get_list_InformesPersonalizados_Dotaciones();
        List<Dasboard> Get_list_Dashboard_ActivosFijos();
        List<Dasboard> Get_list_Dashboard_Retornables_Diario();
        List<RentaFlota> Get_list_InformesPersonalizados_EquiposDisponibles();

        List<Dasboard> Get_list_Dashboard_Retornables_Status();
        List<ActivoDisponible> Get_list_InformesPersonalizados_ActivosDisponibles();
    }
}
