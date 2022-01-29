// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Factory.SalidasDataBase
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
  public class SalidasDataBase : ISalidas
  {
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
    private Logger logger = LogManager.GetCurrentClassLogger();

    public Mensaje Set_Crear_Salida(
      List<IngresoActivo> NuevaTipoActivo,
      Guid UsuarioIngresoCrear)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Salidav2";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_TTR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_MOT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_AREA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_AREA_ORIGEN_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_AREA_DESTINO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_EST_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_ETA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_RES_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_CON_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_PRO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_Fecha_Compra", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@TRA_Costo", SqlDbType.Decimal);
            sqlCommand.Parameters.Add("@TRA_DOCUMENTO_SAP", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_Doc_Factura", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_OBSERVACIONES", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@TRA_Transportador", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@TRA_Fecha_Salida", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@TRA_Fecha_Estimada_Retorno", SqlDbType.DateTime);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (IngresoActivo ingresoActivo in NuevaTipoActivo)
            {
              sqlCommand.Parameters["@TRA_TTR_ID"].Value = (object) ingresoActivo.TRA_TTR_ID;
              sqlCommand.Parameters["@TRA_MOT_ID"].Value = (object) ingresoActivo.TRA_MOT_ID;
              sqlCommand.Parameters["@TRA_AREA_ID"].Value = (object) ingresoActivo.TRA_AREA_ID;
              sqlCommand.Parameters["@TRA_AREA_ORIGEN_ID"].Value = ingresoActivo.TRA_AREA_ORIGEN_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_AREA_ORIGEN_ID;
              sqlCommand.Parameters["@TRA_EST_ID"].Value = ingresoActivo.TRA_EST_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_EST_ID;
              sqlCommand.Parameters["@TRA_ETA_ID"].Value = ingresoActivo.TRA_ETA_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_ETA_ID;
              sqlCommand.Parameters["@TRA_RES_ID"].Value = ingresoActivo.TRA_RES_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_RES_ID;
              sqlCommand.Parameters["@TRA_CLI_ID"].Value = ingresoActivo.TRA_CLI_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_CLI_ID;
              sqlCommand.Parameters["@TRA_CON_ID"].Value = ingresoActivo.TRA_CON_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_CON_ID;
              sqlCommand.Parameters["@TRA_USUARIO_CREATE_ID"].Value = (object) UsuarioIngresoCrear;
              sqlCommand.Parameters["@TRA_PRO_ID"].Value = ingresoActivo.TRA_PRO_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_PRO_ID;
              sqlCommand.Parameters["@TRA_Fecha_Compra"].Value = (object) ingresoActivo.TRA_Fecha_Compra;
              sqlCommand.Parameters["@TRA_Costo"].Value = (object) ingresoActivo.TRA_Costo;
              sqlCommand.Parameters["@TRA_DOCUMENTO_SAP"].Value = (object) ingresoActivo.TRA_DOCUMENTO_SAP;
              sqlCommand.Parameters["@TRA_Doc_Factura"].Value = (object) ingresoActivo.TRA_Doc_Factura;
              sqlCommand.Parameters["@TRA_OBSERVACIONES"].Value = (object) ingresoActivo.TRA_OBSERVACIONES;
              sqlCommand.Parameters["@TRA_Transportador"].Value = (object) ingresoActivo.TRA_Transportador;
              sqlCommand.Parameters["@TRA_Fecha_Salida"].Value = (object) this.helper.Fecha(ingresoActivo.TRA_Fecha_salida);
              sqlCommand.Parameters["@TRA_Fecha_Estimada_Retorno"].Value = (object) this.helper.Fecha(ingresoActivo.TRA_Fecha_Estimada_Retorno);
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_Salida: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Salida: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Salida(
      List<IngresoActivo> EditarSalidaActivo,
      Guid UsuarioEditarSalida)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Salida";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_MOT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_CON_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_PRO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_DOCUMENTO_SAP", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_Doc_Factura", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_OBSERVACIONES", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@TRA_Transportador", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@TRA_Fecha_Salida", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@TRA_Fecha_Estimada_Retorno", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@TRA_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (IngresoActivo ingresoActivo in EditarSalidaActivo)
            {
                sqlCommand.Parameters["@TRA_ID"].Value = ingresoActivo.TRA_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_ID;
                sqlCommand.Parameters["@TRA_MOT_ID"].Value = ingresoActivo.TRA_MOT_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_MOT_ID;
                sqlCommand.Parameters["@TRA_CLI_ID"].Value = ingresoActivo.TRA_CLI_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_CLI_ID;
                sqlCommand.Parameters["@TRA_CON_ID"].Value = ingresoActivo.TRA_CON_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_CON_ID;
                sqlCommand.Parameters["@TRA_PRO_ID"].Value = ingresoActivo.TRA_PRO_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_PRO_ID;
                sqlCommand.Parameters["@TRA_DOCUMENTO_SAP"].Value = (object) ingresoActivo.TRA_DOCUMENTO_SAP;
                sqlCommand.Parameters["@TRA_Doc_Factura"].Value = (object) ingresoActivo.TRA_Doc_Factura;
                sqlCommand.Parameters["@TRA_OBSERVACIONES"].Value = (object) ingresoActivo.TRA_OBSERVACIONES;
                sqlCommand.Parameters["@TRA_Transportador"].Value = (object) ingresoActivo.TRA_Transportador;
                sqlCommand.Parameters["@TRA_Fecha_Salida"].Value = (object) this.helper.Fecha(ingresoActivo.TRA_Fecha_salida);
                sqlCommand.Parameters["@TRA_Fecha_Estimada_Retorno"].Value = (object) this.helper.Fecha(ingresoActivo.TRA_Fecha_Estimada_Retorno);
                sqlCommand.Parameters["@TRA_USUARIO_MOD_ID"].Value = (object) UsuarioEditarSalida;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_Salida: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_Salida: " + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<Estados> Get_list_TransaccionesSalida()
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
            sqlCommand.CommandText = "NEGOCIO.Get_list_TransaccionesSalida";
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
        this.logger.Error(ex, "Error en Get_list_TransaccionesSalida: " + ex.Message);
      }
      return (IEnumerable<Estados>) estadosList;
    }

    public IEnumerable<Estados> Get_list_Motivo(Guid? MOTIVO_ID)
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
            sqlCommand.CommandText = "NEGOCIO.Get_list_Motivo";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TTR_ID", SqlDbType.UniqueIdentifier).Value = (object) MOTIVO_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                estadosList.Add(new Estados()
                {
                  EST_ID = sqlDataReader.GetGuid(0),
                  EST_DESC = sqlDataReader.GetString(2)
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_Motivo: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_Motivo: " + ex.Message);
      }
      return (IEnumerable<Estados>) estadosList;
    }
  }
}
