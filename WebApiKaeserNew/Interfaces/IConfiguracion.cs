using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiKaeser.Models;

namespace WebApiKaeser.Interfaces
{
    interface IConfiguracion
    {
       IEnumerable<Empresa> Get_list_EMPRESA();
       Mensaje Set_Editar_EMPRESA( List<Empresa> empresa,Guid? UsuarioEmpresa);
       IEnumerable<Transaccion> Get_list_transacciones_SinNotificar();
        IEnumerable<Transaccion> Get_list_Asignaciones_SinNotificar();
        IEnumerable<Usuarios> Get_list_emai_autorizados(Guid TRA_ID);
        IEnumerable<Transaccion> Get_list_Informe_Prestamos(Guid? TRA_ID, Guid? RES_ID, int Vencidos, DateTime? Fecha_Inicial, DateTime? Fecha_Final);
        IEnumerable<Mensaje> Set_Transaccion_notificada(Guid TRA_ID);
        IEnumerable<Mensaje> Set_Asignaciones_notificada(Guid RES_ID);
    }
}
