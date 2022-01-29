// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Factory.AsignacionDataBase
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApiKaeser.Interfaces;
using WebApiKaeser.Models;

namespace WebApiKaeser.Factory
{
  public class AsignacionDataBase : IAsignacion
  {
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
    private Logger logger = LogManager.GetCurrentClassLogger();

    public IEnumerable<Estados> Get_list_TransaccionesAsignacion()
    {
      List<Estados> estadosList = new List<Estados>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_TransaccionesAsignacion";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                estadosList.Add(new Estados()
                {
                  EST_ID = sqlDataReader.GetGuid(0),
                  EST_DESC = sqlDataReader.GetString(1)
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_TransaccionesAsignacion: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_TransaccionesAsignacion: " + ex.Message);
      }
      return (IEnumerable<Estados>) estadosList;
    }

    public Mensaje Set_Crear_Asignacion(List<IngresoActivo> NuevaTipoActivo, Guid UsuarioAsignacionCrear)
    {
      Mensaje mensaje = new Mensaje();
      try
      {
        string str = "";
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Asignacion";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_TTR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_AREA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_RES_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_AREA_DESTINO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_ETA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_Fecha_Retorno", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@TRA_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_OBSERVACIONES", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@TRA_DOCUMENTO_SAP", SqlDbType.VarChar);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (IngresoActivo ingresoActivo in NuevaTipoActivo)
            {
                sqlCommand.Parameters["@TRA_TTR_ID"].Value = ingresoActivo.TRA_TTR_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000"): ingresoActivo.TRA_TTR_ID;
                sqlCommand.Parameters["@TRA_AREA_ID"].Value = ingresoActivo.TRA_AREA_ID==null? Guid.Parse ("00000000-0000-0000-0000-000000000000"):ingresoActivo.TRA_AREA_ID;
                sqlCommand.Parameters["@TRA_RES_ID"].Value = ingresoActivo.TRA_RES_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_RES_ID;
                sqlCommand.Parameters["@TRA_AREA_DESTINO_ID"].Value = ingresoActivo.TRA_AREA_DESTINO_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_AREA_DESTINO_ID;
                sqlCommand.Parameters["@TRA_ETA_ID"].Value = ingresoActivo.TRA_ETA_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_ETA_ID;
                sqlCommand.Parameters["@TRA_Fecha_Retorno"].Value =  helper.Fecha(ingresoActivo.TRA_Fecha_Retorno);
                sqlCommand.Parameters["@TRA_USUARIO_CREATE_ID"].Value =  UsuarioAsignacionCrear;
                sqlCommand.Parameters["@TRA_OBSERVACIONES"].Value = ingresoActivo.TRA_OBSERVACIONES;
                sqlCommand.Parameters["@TRA_DOCUMENTO_SAP"].Value = ingresoActivo.TRA_DOCUMENTO_SAP;

              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                while (sqlDataReader.Read())
                {
                  try
                  {
                    mensaje.data = (object) sqlDataReader.GetGuid(0);
                  }
                  catch
                  {
                    mensaje.errNumber = sqlDataReader.GetInt32(0);
                    mensaje.message = sqlDataReader.GetString(1);
                  }
                }
                sqlDataReader.NextResult();
                if (sqlDataReader.Read())
                {
                  mensaje.errNumber = sqlDataReader.GetInt32(0);
                  mensaje.message = sqlDataReader.GetString(1);
                }
                sqlDataReader.Close();
              }
            }
            sqlConnection.Close();
          }
        }
      }
      catch (SqlException ex)
      {
        mensaje.errNumber = -1;
        mensaje.message = ex.Message;
        this.logger.Error<SqlException>("Sql error en Set_Crear_Asignacion: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Asignacion: " + ex.Message);
      }
      return mensaje;
    }
  }
}
