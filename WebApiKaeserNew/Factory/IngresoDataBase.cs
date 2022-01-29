// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Factory.IngresoDataBase
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebApiKaeser.Interfaces;
using WebApiKaeser.Models;

namespace WebApiKaeser.Factory
{
  public class IngresoDataBase : IIngresos
  {
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
    private Logger logger = LogManager.GetCurrentClassLogger();

    public Mensaje Set_Crear_Entrada(
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Entradav2";
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
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (IngresoActivo ingresoActivo in NuevaTipoActivo)
            {
                sqlCommand.Parameters["@TRA_TTR_ID"].Value = ingresoActivo.TRA_TTR_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_TTR_ID;
                sqlCommand.Parameters["@TRA_MOT_ID"].Value = ingresoActivo.TRA_MOT_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_MOT_ID;
                sqlCommand.Parameters["@TRA_AREA_ID"].Value = ingresoActivo.TRA_AREA_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_AREA_ID;
                sqlCommand.Parameters["@TRA_AREA_ORIGEN_ID"].Value = ingresoActivo.TRA_AREA_ORIGEN_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_AREA_ORIGEN_ID;
                sqlCommand.Parameters["@TRA_AREA_DESTINO_ID"].Value = ingresoActivo.TRA_AREA_DESTINO_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_AREA_DESTINO_ID;
                sqlCommand.Parameters["@TRA_EST_ID"].Value = ingresoActivo.TRA_EST_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_EST_ID;
                sqlCommand.Parameters["@TRA_ETA_ID"].Value = ingresoActivo.TRA_ETA_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_ETA_ID;
                sqlCommand.Parameters["@TRA_RES_ID"].Value = ingresoActivo.TRA_RES_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_RES_ID;
                sqlCommand.Parameters["@TRA_CLI_ID"].Value = ingresoActivo.TRA_CLI_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_CLI_ID;
                sqlCommand.Parameters["@TRA_CON_ID"].Value = ingresoActivo.TRA_CON_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_CON_ID;
                sqlCommand.Parameters["@TRA_PRO_ID"].Value = ingresoActivo.TRA_PRO_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_PRO_ID;
                sqlCommand.Parameters["@TRA_MOT_ID"].Value = ingresoActivo.TRA_MOT_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_MOT_ID;
                sqlCommand.Parameters["@TRA_USUARIO_CREATE_ID"].Value = (object) UsuarioIngresoCrear;
                sqlCommand.Parameters["@TRA_Fecha_Compra"].Value = (object) this.helper.Fecha(ingresoActivo.TRA_Fecha_Compra);
                sqlCommand.Parameters["@TRA_Costo"].Value = (object) ingresoActivo.TRA_Costo;
                sqlCommand.Parameters["@TRA_DOCUMENTO_SAP"].Value = (object) ingresoActivo.TRA_DOCUMENTO_SAP;
                sqlCommand.Parameters["@TRA_Doc_Factura"].Value = (object) ingresoActivo.TRA_Doc_Factura;
                sqlCommand.Parameters["@TRA_OBSERVACIONES"].Value = (object) ingresoActivo.TRA_OBSERVACIONES;
                sqlCommand.Parameters["@TRA_Transportador"].Value = (object) ingresoActivo.TRA_Transportador;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_Entrada: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Entrada: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Entrada(
      List<IngresoActivo> EditarIngresoActivo,
      Guid UsuarioEditarCrear)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Entrada";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_RES_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_CON_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_PRO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TRA_Fecha_Compra", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@TRA_Costo", SqlDbType.Decimal);
            sqlCommand.Parameters.Add("@TRA_DOCUMENTO_SAP", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_Doc_Factura", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@TRA_Transportador", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@TRA_OBSERVACIONES", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@TRA_MOT_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (IngresoActivo ingresoActivo in EditarIngresoActivo)
            {
                sqlCommand.Parameters["@TRA_ID"].Value = ingresoActivo.TRA_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_ID;
                sqlCommand.Parameters["@TRA_RES_ID"].Value = ingresoActivo.TRA_RES_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_RES_ID;
                sqlCommand.Parameters["@TRA_CLI_ID"].Value = ingresoActivo.TRA_CLI_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_CLI_ID;
                sqlCommand.Parameters["@TRA_CON_ID"].Value = ingresoActivo.TRA_CON_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_CON_ID;
                sqlCommand.Parameters["@TRA_PRO_ID"].Value = ingresoActivo.TRA_PRO_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_PRO_ID;
                sqlCommand.Parameters["@TRA_USUARIO_MOD_ID"].Value = (object) UsuarioEditarCrear;
                sqlCommand.Parameters["@TRA_Fecha_Compra"].Value = (object) this.helper.Fecha(ingresoActivo.TRA_Fecha_Compra);
                sqlCommand.Parameters["@TRA_Costo"].Value = (object) ingresoActivo.TRA_Costo;
                sqlCommand.Parameters["@TRA_DOCUMENTO_SAP"].Value = (object) ingresoActivo.TRA_DOCUMENTO_SAP;
                sqlCommand.Parameters["@TRA_Doc_Factura"].Value = (object) ingresoActivo.TRA_Doc_Factura;
                sqlCommand.Parameters["@TRA_Transportador"].Value = (object) ingresoActivo.TRA_Transportador;
                sqlCommand.Parameters["@TRA_OBSERVACIONES"].Value = (object) ingresoActivo.TRA_OBSERVACIONES;
                sqlCommand.Parameters["@TRA_MOT_ID"].Value = ingresoActivo.TRA_MOT_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : ingresoActivo.TRA_MOT_ID;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_Entrada: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_Entrada: " + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<Estados> Get_list_TransaccionesEntrada()
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
            sqlCommand.CommandText = "NEGOCIO.Get_list_TransaccionesEntrada";
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
        this.logger.Error<SqlException>("Sql error en Get_list_TransaccionesEntrada: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_TransaccionesEntrada: " + ex.Message);
      }
      return (IEnumerable<Estados>) estadosList;
    }

    public IEnumerable<Estados> Get_list_EstadosTransacciones(Guid? EST_ID)
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
            sqlCommand.CommandText = "NEGOCIO.Get_list_EstadosTransacciones";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@EST_ID", SqlDbType.UniqueIdentifier).Value = (object) EST_ID;
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
        this.logger.Error<SqlException>("Sql error en Get_list_EstadosTransacciones: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_EstadosTransacciones: " + ex.Message);
      }
      return (IEnumerable<Estados>) estadosList;
    }

    public IEnumerable<Transaccion> Get_list_Transacciones(
      Guid? TRA_ID,
      Guid? CON_ID,
      Guid? TTR_ID,
      Guid? EST_ID,
      Guid? CLI_ID,
      string TRA_DOCUMENTO_SAP,
      string CON_NUMERO_DOC,
      DateTime? TRA_FECHA_INICIO,
      DateTime? TRA_FECHA_FINAL,
      bool? TTR_ES_SALIDA,
      bool? TTR_ES_ENTRADA,
      bool? TTR_ES_TRASLADO,
      Guid? TRA_AREA_ORIGEN_ID,
      Guid? TRA_AREA_DESTINO_ID,
      string TRA_Doc_Factura,
      bool? TTR_ES_ASIGNACION)
    {
      List<Transaccion> source = new List<Transaccion>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Transacciones";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_ID", SqlDbType.UniqueIdentifier).Value = (object) TRA_ID;
            sqlCommand.Parameters.Add("@CON_ID", SqlDbType.UniqueIdentifier).Value = (object) CON_ID;
            sqlCommand.Parameters.Add("@TTR_ID", SqlDbType.UniqueIdentifier).Value = (object) TTR_ID;
            sqlCommand.Parameters.Add("@EST_ID", SqlDbType.UniqueIdentifier).Value = (object) EST_ID;
            bool? nullable = TTR_ES_ASIGNACION;
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) != 0)
              sqlCommand.Parameters.Add("@TRA_RES_ID", SqlDbType.UniqueIdentifier).Value = (object) CLI_ID;
            else
              sqlCommand.Parameters.Add("@CLI_ID", SqlDbType.UniqueIdentifier).Value = (object) CLI_ID;
            sqlCommand.Parameters.Add("@TRA_DOCUMENTO_SAP", SqlDbType.VarChar, 50).Value = (object) TRA_DOCUMENTO_SAP;
            sqlCommand.Parameters.Add("@CON_NUMERO_DOC", SqlDbType.VarChar, 100).Value = (object) CON_NUMERO_DOC;
            sqlCommand.Parameters.Add("@TRA_FECHA_INICIO", SqlDbType.DateTime).Value = (object) TRA_FECHA_INICIO;
            sqlCommand.Parameters.Add("@TRA_FECHA_FINAL", SqlDbType.DateTime).Value = (object) TRA_FECHA_FINAL;
            sqlCommand.Parameters.Add("@TTR_ES_SALIDA", SqlDbType.Bit).Value = (object) TTR_ES_SALIDA;
            sqlCommand.Parameters.Add("@TTR_ES_ENTRADA", SqlDbType.Bit).Value = (object) TTR_ES_ENTRADA;
            sqlCommand.Parameters.Add("@TTR_ES_TRASLADO", SqlDbType.Bit).Value = (object) TTR_ES_TRASLADO;
            sqlCommand.Parameters.Add("@TRA_AREA_ORIGEN_ID", SqlDbType.UniqueIdentifier).Value = (object) TRA_AREA_ORIGEN_ID;
            sqlCommand.Parameters.Add("@TRA_AREA_DESTINO_ID", SqlDbType.UniqueIdentifier).Value = (object) TRA_AREA_DESTINO_ID;
            sqlCommand.Parameters.Add("@TRA_Doc_Factura", SqlDbType.VarChar, 50).Value = (object) TRA_Doc_Factura;
            sqlCommand.Parameters.Add("@TTR_ES_ASIGNACION", SqlDbType.Bit).Value = (object) TTR_ES_ASIGNACION;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Transaccion transaccion1 = new Transaccion();
                transaccion1.TRA_ID = sqlDataReader.GetGuid(0);
                transaccion1.TRA_TTR_ID = sqlDataReader.GetGuid(1);
                try
                {
                  transaccion1.TTR_DESC = sqlDataReader.GetString(2);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_CLI_ID = sqlDataReader.GetGuid(3);
                }
                catch
                {
                }
                try
                {
                  transaccion1.PRO_NOMBRE = sqlDataReader.GetString(13);
                }
                catch
                {
                }
                try
                {
                  transaccion1.CLI_NOMBRE = sqlDataReader.GetString(4);
                }
                catch
                {
                  Transaccion transaccion2 = transaccion1;
                  transaccion2.CLI_NOMBRE = transaccion2.PRO_NOMBRE;
                }
                try
                {
                  transaccion1.TRA_Fecha_Compra = sqlDataReader.GetDateTime(5).ToString("yyyy/MM/dd");
                }
                catch
                {
                }
                try
                {
                  transaccion1.EST_ID = sqlDataReader.GetGuid(6);
                }
                catch
                {
                }
                try
                {
                  transaccion1.EST_DESC = sqlDataReader.GetString(7);
                }
                catch
                {
                }
                try
                {
                  transaccion1.CON_NUMERO_DOC = sqlDataReader.GetString(8);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_DOCUMENTO_SAP = sqlDataReader.GetString(9);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_OBSERVACIONES = sqlDataReader.GetString(10);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TTR_ES_ENTRADA = sqlDataReader.GetBoolean(11);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TTR_ES_SALIDA = sqlDataReader.GetBoolean(12);
                }
                catch
                {
                }
                try
                {
                  transaccion1.Responsable = sqlDataReader.GetString(14);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_Doc_Factura = sqlDataReader.GetString(15);
                }
                catch
                {
                }
                try
                {
                  transaccion1.FechaIngreso = sqlDataReader.GetDateTime(16);
                  transaccion1.FechaIngreso_str= sqlDataReader.GetDateTime(16).ToString("yyyy/MM/dd");
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_Transportador = sqlDataReader.GetString(17);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_PRO_ID = sqlDataReader.GetGuid(18);
                }
                catch
                {
                }
                try
                {
                  transaccion1.RES_ID = sqlDataReader.GetGuid(19);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_AREA_ID = sqlDataReader.GetGuid(20);
                }
                catch
                {
                }
                try
                {
                  transaccion1.ARE_DESC = sqlDataReader.GetString(21);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_CON_ID = new Guid?(sqlDataReader.GetGuid(22));
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_costo = sqlDataReader.GetDecimal(23);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_Fecha_Salida = sqlDataReader.GetDateTime(24).ToString("yyyy/MM/dd");
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_MOT_ID = new Guid?(sqlDataReader.GetGuid(25));
                }
                catch
                {
                }
                try
                {
                  transaccion1.AREA_ORIGEN = sqlDataReader.GetString(26);
                }
                catch
                {
                }
                try
                {
                  transaccion1.AREA_DESTINO = sqlDataReader.GetString(27);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_FECHA_CREATE = sqlDataReader.GetDateTime(28).ToString("yyyy/MM/dd HH:mm");
                  transaccion1.TRA_FECHA_CREATE_DATE = sqlDataReader.GetDateTime(28);
                }
                catch
                {
                }
                try
                {
                  transaccion1.USU_LOGIN = sqlDataReader.GetString(29);
                }
                catch
                {
                }
                try
                {
                  transaccion1.ID_ORIGEN = new Guid?(sqlDataReader.GetGuid(30));
                }
                catch
                {
                }
                try
                {
                  transaccion1.ID_DESTINO = new Guid?(sqlDataReader.GetGuid(31));
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_Fecha_Retorno_date = sqlDataReader.GetDateTime(32);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_Fecha_Retorno = sqlDataReader.GetDateTime(32).ToString("yyyy/MM/dd");
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_ETA_ID = new Guid?(sqlDataReader.GetGuid(33));
                }
                catch
                {
                }
                try
                {
                  transaccion1.ETQ_BC = sqlDataReader.GetString(34);
                }
                catch
                {
                }
                try
                {
                  transaccion1.ETQ_CODE = sqlDataReader.GetString(35);
                }
                catch
                {
                }
                try
                {
                  transaccion1.ACT_DESC = sqlDataReader.GetString(36);
                }
                catch
                {
                }
                try
                {
                  transaccion1.ACT_Serial = sqlDataReader.GetString(37);
                }
                catch
                {
                }
                try
                {
                  transaccion1.RES_DOCUMENTO = sqlDataReader.GetString(38);
                }
                catch
                {
                }
                try
                {
                  transaccion1.USU_CORREO = sqlDataReader.GetString(39);
                }
                catch
                {
                }
                try
                {
                  transaccion1.TRA_Fecha_Estimada_Retorno = sqlDataReader.GetDateTime(40);
                }
                catch
                {
                    try
                        { transaccion1.TRA_Fecha_Estimada_Retorno = transaccion1.TRA_Fecha_Retorno_date; }
                        catch { }
                }
                try
                {
                  transaccion1.TRA_Fecha_Estimada_Retorno_str = transaccion1.TRA_Fecha_Estimada_Retorno.ToString("yyyy/MM/dd");
                }
                catch
                {
                }
                source.Add(transaccion1);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_Transacciones: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_Transacciones: " + ex.Message);
      }
      return (IEnumerable<Transaccion>) source.OrderByDescending<Transaccion, DateTime>((Func<Transaccion, DateTime>) (t => t.TRA_FECHA_CREATE_DATE));
    }

    public Mensaje Set_Autorizar_Transacciones(
      List<IngresoActivo> AutorizarTransaccion,
      Guid UsuarioAutorizarTransaccion)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Autorizar_Transacciones";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@URL_LINK", SqlDbType.VarChar, 200);
            sqlCommand.Parameters.Add("@TRA_OBSERVACIONES", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@TRA_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (IngresoActivo ingresoActivo in AutorizarTransaccion)
            {
              sqlCommand.Parameters["@TRA_ID"].Value = (object) ingresoActivo.TRA_ID;
              sqlCommand.Parameters["@URL_LINK"].Value = (object) ingresoActivo.URL_LINK;
              sqlCommand.Parameters["@TRA_OBSERVACIONES"].Value = (object) ingresoActivo.TRA_OBSERVACIONES;
              sqlCommand.Parameters["@TRA_USUARIO_MOD_ID"].Value = (object) UsuarioAutorizarTransaccion;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
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
        this.logger.Error<SqlException>("Sql error en Set_Autorizar_Transacciones: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Autorizar_Transacciones: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Crear_DetalleTransacciones(
      List<TransaccionDetalle> TransaccionDetalle,
      Guid UsuarioTransaccionCrear)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_DetalleTransacciones";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@DTR_TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ETQ", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@DTR_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (TransaccionDetalle transaccionDetalle in TransaccionDetalle)
            {
              sqlCommand.Parameters["@DTR_TRA_ID"].Value = (object) transaccionDetalle.TRA_ID;
              sqlCommand.Parameters["@ETQ"].Value = (object) transaccionDetalle.ETQ_BC;
              sqlCommand.Parameters["@DTR_USUARIO_CREATE_ID"].Value = (object) UsuarioTransaccionCrear;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_DetalleTransacciones: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_DetalleTransacciones: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_DetalleTransacciones(
      List<TransaccionDetalle> TransaccionDetalle,
      Guid UsuarioTransaccionEliminar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_DetalleTransacciones";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@DTR_TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ETQ", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@DTR_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (TransaccionDetalle transaccionDetalle in TransaccionDetalle)
            {
              sqlCommand.Parameters["@DTR_TRA_ID"].Value = (object) transaccionDetalle.TRA_ID;
              sqlCommand.Parameters["@ETQ"].Value = (object) transaccionDetalle.ETQ_BC;
              sqlCommand.Parameters["@DTR_USUARIO_DELETE_ID"].Value = (object) UsuarioTransaccionEliminar;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_DetalleTransacciones: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_DetalleTransacciones: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_CancelarTransacciones(
      Guid Transaccion,
      Guid UsuarioTransaccionCancelar,
      string URL_LINK)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Cancelar_Transacciones";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_ID", SqlDbType.UniqueIdentifier).Value = (object) Transaccion;
            sqlCommand.Parameters.Add("@TRA_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier).Value = (object) UsuarioTransaccionCancelar;
            sqlCommand.Parameters.Add("@URL_LINK", SqlDbType.VarChar, 50).Value = (object) URL_LINK;
            mensaje.errNumber = 0;
            mensaje.message = str;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              if (sqlDataReader.Read())
              {
                mensaje.errNumber = sqlDataReader.GetInt32(0);
                mensaje.message = sqlDataReader.GetString(1);
              }
              sqlDataReader.Close();
            }
            sqlConnection.Close();
          }
        }
      }
      catch (SqlException ex)
      {
        mensaje.errNumber = -1;
        mensaje.message = ex.Message;
        this.logger.Error<SqlException>("Sql error en Set_Cancelar_Transacciones: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Cancelar_Transacciones: " + ex.Message);
      }
      return mensaje;
    }
  }
}
