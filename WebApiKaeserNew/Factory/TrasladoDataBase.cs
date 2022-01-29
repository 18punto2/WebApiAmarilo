// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Factory.TrasladoDataBase
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
  public class TrasladoDataBase : ITraslado
  {
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
    private Logger logger = LogManager.GetCurrentClassLogger();

    public Mensaje Set_Crear_traslado(
      List<TrasladoActivo> NuevoActivo,
      Guid UsuarioTrasladoCrear)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Traslado";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_TTR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_MOT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_AREA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_AREA_ORIGEN_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_AREA_DESTINO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_DOCUMENTO_SAP", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_Doc_Factura", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_OBSERVACIONES", SqlDbType.VarChar);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (TrasladoActivo trasladoActivo in NuevoActivo)
            {
              sqlCommand.Parameters["@TRA_TTR_ID"].Value = (object) trasladoActivo.TRA_TTR_ID;
              sqlCommand.Parameters["@TRA_MOT_ID"].Value = (object) trasladoActivo.TRA_MOT_ID;
              sqlCommand.Parameters["@TRA_AREA_ID"].Value = (object) trasladoActivo.TRA_AREA_ID;
              sqlCommand.Parameters["@TRA_AREA_ORIGEN_ID"].Value = (object) trasladoActivo.TRA_AREA_ORIGEN_ID;
              sqlCommand.Parameters["@TRA_AREA_DESTINO_ID"].Value = (object) trasladoActivo.TRA_AREA_DESTINO_ID;
              sqlCommand.Parameters["@TRA_USUARIO_CREATE_ID"].Value = (object) UsuarioTrasladoCrear;
              sqlCommand.Parameters["@TRA_DOCUMENTO_SAP"].Value = (object) trasladoActivo.TRA_DOCUMENTO_SAP;
              sqlCommand.Parameters["@TRA_Doc_Factura"].Value = (object) trasladoActivo.TRA_Doc_Factura;
              sqlCommand.Parameters["@TRA_TRA_ID"].Value = (object) trasladoActivo.TRA_TRA_ID;
              sqlCommand.Parameters["@TRA_OBSERVACIONES"].Value = (object) trasladoActivo.TRA_OBSERVACIONES;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_Traslado: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Traslado: " + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<Estados> Get_list_TransaccionesTraslado()
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
            sqlCommand.CommandText = "NEGOCIO.Get_list_TransaccionesTraslado";
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
        this.logger.Error<SqlException>("Sql error en Get_list_TransaccionesSalida: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_TransaccionesTraslado: " + ex.Message);
      }
      return (IEnumerable<Estados>) estadosList;
    }

    public Mensaje Set_Editar_Traslado(
      List<IngresoActivo> EditarTrasladoActivo,
      Guid UsuarioEditarTraslado)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Traslado";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_MOT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_DOCUMENTO_SAP", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_Doc_Factura", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_OBSERVACIONES", SqlDbType.VarChar);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (IngresoActivo ingresoActivo in EditarTrasladoActivo)
            {
                sqlCommand.Parameters["@TRA_ID"].Value = ingresoActivo.TRA_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_ID;
                sqlCommand.Parameters["@TRA_MOT_ID"].Value = ingresoActivo.TRA_MOT_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_MOT_ID;
                sqlCommand.Parameters["@TRA_USUARIO_MOD_ID"].Value = (object) UsuarioEditarTraslado;
                sqlCommand.Parameters["@TRA_DOCUMENTO_SAP"].Value = (object) ingresoActivo.TRA_DOCUMENTO_SAP;
                sqlCommand.Parameters["@TRA_Doc_Factura"].Value = (object) ingresoActivo.TRA_Doc_Factura;
                sqlCommand.Parameters["@TRA_OBSERVACIONES"].Value = (object) ingresoActivo.TRA_OBSERVACIONES;
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
        this.logger.Error<SqlException>("Sql error en Set_Traslado_Salida: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Traslado_Salida: " + ex.Message);
      }
      return mensaje;
    }
  }
}
