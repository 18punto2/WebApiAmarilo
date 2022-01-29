// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Factory.ServicioTecnicoDataBase
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
using System.Linq;

namespace WebApiKaeser.Factory
{
  public class ServicioTecnicoDataBase : IServicioTecnico
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();

    public List<Estados> Get_list_ServicioTecnicoEstado()
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
            sqlCommand.CommandText = "NEGOCIO.Get_list_ServicioTecnicoEstado";
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Get_list_ServicioTecnicoEstado: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Get_list_ServicioTecnicoEstado: " + ex.Message);
      }
      return estadosList;
    }

    public List<Estados> Get_list_ServicioTecnicoProceso()
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
            sqlCommand.CommandText = "NEGOCIO.Get_list_ServicioTecnicoProceso";
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Get_list_ServicioTecnicoProceso: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Get_list_ServicioTecnicoProceso: " + ex.Message);
      }
      return estadosList;
    }

    public List<ServicioTecnico> Get_list_ServicioTecnico(
      Guid? STE_USUARIO_SOLICITA,
      Guid? STE_STE_ID,
      DateTime? FECHA_INICIO,
      DateTime? FECHA_FIN,
      short? PARAMETROCONSULTAFECHA,
      Guid? STE_ID,
      Guid? STE_TRA_ID,
      Guid? STE_ACT_ID)
    {
      List<ServicioTecnico> servicioTecnicoList = new List<ServicioTecnico>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_ServicioTecnicov2";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STE_ID", SqlDbType.UniqueIdentifier).Value = (object) STE_ID;
            sqlCommand.Parameters.Add("@STE_USUARIO_SOLICITA", SqlDbType.UniqueIdentifier).Value = (object) STE_USUARIO_SOLICITA;
            sqlCommand.Parameters.Add("@STE_STE_ID", SqlDbType.UniqueIdentifier).Value = (object) STE_STE_ID;
            sqlCommand.Parameters.Add("@FECHA_INICIO", SqlDbType.DateTime).Value = (object) FECHA_INICIO;
            sqlCommand.Parameters.Add("@FECHA_FIN", SqlDbType.DateTime).Value = (object) FECHA_FIN;
            sqlCommand.Parameters.Add("@PARAMETROCONSULTAFECHA", SqlDbType.SmallInt).Value = (object) PARAMETROCONSULTAFECHA;
            sqlCommand.Parameters.Add("@STE_TRA_ID", SqlDbType.UniqueIdentifier).Value = (object) STE_TRA_ID;
            sqlCommand.Parameters.Add("@STE_ACT_ID", SqlDbType.UniqueIdentifier).Value = (object) STE_ACT_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                {
                ServicioTecnico servicioTecnico = new ServicioTecnico();
                servicioTecnico.STE_ID = sqlDataReader.GetGuid(0);
                servicioTecnico.ACT_DESC = sqlDataReader.GetString(1);
                servicioTecnico.TAC_ID = new Guid?(sqlDataReader.GetGuid(2));
                servicioTecnico.TAC_DESC = sqlDataReader.GetString(3);
                servicioTecnico.MOD_ID = new Guid?(sqlDataReader.GetGuid(4));
                servicioTecnico.MOD_DESC = sqlDataReader.GetString(5);
                servicioTecnico.ACT_SERIAL = sqlDataReader.GetString(6);
                try{ servicioTecnico.ARE_DESC = sqlDataReader.GetString(7); } catch (Exception ex){ }
                try{ servicioTecnico.USUARIO_SOLICITA = sqlDataReader.GetString(8); }catch (Exception ex) { }
                try{ servicioTecnico.STE_FECHA_SOLICITUD = sqlDataReader.GetDateTime(9);
                     servicioTecnico.STE_FECHA_SOLICITUD_STR = sqlDataReader.GetDateTime(9).ToString("yyyy/MM/dd"); } catch (Exception ex) {}
                try{ servicioTecnico.USUARIO_INICIA = sqlDataReader.GetString(10);  } catch (Exception ex) { }
                try{ servicioTecnico.STE_FECHA_INICIO = sqlDataReader.GetDateTime(11);
                     servicioTecnico.STE_FECHA_INICIO_STR = sqlDataReader.GetDateTime(11).ToString("yyyy/MM/dd");   }catch (Exception ex) {}
                try{ servicioTecnico.USUARIO_FINALIZA = sqlDataReader.GetString(12); } catch (Exception ex) { }
                try{ servicioTecnico.STE_FECHA_FINALIZA = sqlDataReader.GetDateTime(13);
                    servicioTecnico.STE_FECHA_FINALIZA_STR = sqlDataReader.GetDateTime(13).ToString("yyyy/MM/dd");  }catch (Exception ex) {}
                servicioTecnico.ETQ_BC = sqlDataReader.GetString(14);
                servicioTecnico.ETQ_CODE = sqlDataReader.GetString(15);
                servicioTecnico.STE_ID_ESTADO = sqlDataReader.GetGuid(16);//estado id
                servicioTecnico.STE_DESCRIPCION =sqlDataReader.GetString(17);
                try{ servicioTecnico.STE_PRO_ID = new Guid?(sqlDataReader.GetGuid(18));} catch (Exception ex) { }
                try{ servicioTecnico.PRO_NOMBRE = sqlDataReader.GetString(19);} catch (Exception ex){}
                try{ servicioTecnico.STE_CLI_ID = new Guid?(sqlDataReader.GetGuid(20));} catch (Exception ex) { }
                try{ servicioTecnico.CLI_NOMBRE = sqlDataReader.GetString(21);} catch (Exception ex){}
                try{ servicioTecnico.STE_CODIGO_SAP = sqlDataReader.GetString(22);} catch (Exception ex) { servicioTecnico.STE_CODIGO_SAP = ""; }
                servicioTecnico.STE_INTERNO_EXTERNO = sqlDataReader.GetBoolean(23);
                try{ servicioTecnico.STE_TRA_ID = sqlDataReader.GetGuid(24); }catch (Exception ex){}
                try { servicioTecnico.ACT_EST_ID = sqlDataReader.GetGuid(25); } catch (Exception ex) { }
                try { servicioTecnico.STE_OBSERVACION= sqlDataReader.GetString(26); } catch (Exception ex) { }
                try { servicioTecnico.STE_ACT_ID = sqlDataReader.GetGuid(27); } catch (Exception ex) { }
                servicioTecnicoList.Add(servicioTecnico);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Get_list_ServicioTecnicov2: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Get_list_ServicioTecnicov2: " + ex.Message);
      }
            return servicioTecnicoList;//.OrderByDescending(t => t.STE_FECHA_SOLICITUD).ToList();
    }

    public List<ServicioTecnicoIncidencia> Get_list_ServicioTecnicoIncidencia(
      Guid STI_STE_ID)
    {
      List<ServicioTecnicoIncidencia> tecnicoIncidenciaList = new List<ServicioTecnicoIncidencia>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_ServicioTecnicoIncidencia";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STI_STE_ID", SqlDbType.UniqueIdentifier).Value = (object) STI_STE_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                tecnicoIncidenciaList.Add(new ServicioTecnicoIncidencia()
                {
                  STI_ID = sqlDataReader.GetGuid(0),
                  STP_DESCRIPCION = sqlDataReader.GetString(1),
                  STI_OBSERVACION = sqlDataReader.GetString(2),
                  USUARIO_CREA = sqlDataReader.GetString(3),
                  STI_FECHA_CREA = sqlDataReader.GetDateTime(4),
                  STI_FECHA_CREA_STR = sqlDataReader.GetDateTime(4).ToString("yyyy/MM/dd")
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Get_list_ServicioTecnicoIncidencia: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Get_list_ServicioTecnicoIncidencia: " + ex.Message);
      }
      return tecnicoIncidenciaList;
    }

    public List<ServicioTecnicoImagen> Get_list_ServicioTecnicoImagen(
      Guid STM_STI_ID)
    {
      List<ServicioTecnicoImagen> servicioTecnicoImagenList = new List<ServicioTecnicoImagen>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_ServicioTecnicoImagen";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STM_STI_ID", SqlDbType.UniqueIdentifier).Value = (object) STM_STI_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                servicioTecnicoImagenList.Add(new ServicioTecnicoImagen()
                {
                  STM_ID = sqlDataReader.GetGuid(0),
                  RUTA = sqlDataReader.GetString(1)
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Get_list_ServicioTecnicoImagen: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Get_list_ServicioTecnicoImagen: " + ex.Message);
      }
      return servicioTecnicoImagenList;
    }

    public List<ServicioTecnicoRepuesto> Get_list_ServicioTecnicoRepuesto(
      Guid STR_STE_ID)
    {
      List<ServicioTecnicoRepuesto> servicioTecnicoRepuestoList = new List<ServicioTecnicoRepuesto>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_ServicioTecnicoRepuesto";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STR_STE_ID", SqlDbType.UniqueIdentifier).Value = (object) STR_STE_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                while (sqlDataReader.Read())
                               
                {
                                ServicioTecnicoRepuesto repuesto = new ServicioTecnicoRepuesto();
                                repuesto.STR_ID = sqlDataReader.GetGuid(0);
                                repuesto.STR_CODIGO_REPUESTO = sqlDataReader.GetString(1);
                                repuesto.STR_CANTIDAD = sqlDataReader.GetDouble(2);
                                repuesto.STR_OBSERVACION = sqlDataReader.GetString(3);
                                repuesto.USUARIO_CREA = sqlDataReader.GetString(4);
                                repuesto.STR_FECHA_CREA = sqlDataReader.GetDateTime(5);
                                repuesto.STR_FECHA_CREA_STR = sqlDataReader.GetDateTime(5).ToString("yyyy/MM/dd");
                                servicioTecnicoRepuestoList.Add(repuesto);
                }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Get_list_ServicioTecnicoRepuesto: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Get_list_ServicioTecnicoRepuesto: " + ex.Message);
      }
      return servicioTecnicoRepuestoList;
    }

    public Mensaje Set_ActualizarServicioTecnicoCambioEstado(
      List<ServicioTecnico> ServicioTecnico,
      Guid UsuarioCambioEstado)
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
            sqlCommand.CommandText = "NEGOCIO.Set_ActualizarServicioTecnicoCambioEstado";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TIPO_ACTUALIZACION", SqlDbType.SmallInt);
            sqlCommand.Parameters.Add("@STE_USUARIO", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STE_ARE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (ServicioTecnico servicioTecnico in ServicioTecnico)
            {
              sqlCommand.Parameters["@STE_ID"].Value = (object) servicioTecnico.STE_ID;
              sqlCommand.Parameters["@TIPO_ACTUALIZACION"].Value = (object) servicioTecnico.TIPO_ACTUALIZACION;
              sqlCommand.Parameters["@STE_USUARIO"].Value = (object) UsuarioCambioEstado;
              sqlCommand.Parameters["@STE_ARE_ID"].Value = (object) servicioTecnico.STE_ARE_ID;
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Set_ActualizarServicioTecnicoCambioEstado: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Set_ActualizarServicioTecnicoCambioEstado: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_EliminarServicioTecnico(List<Guid> STE_ID,Guid STE_USU_ID)
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
            sqlCommand.CommandText = "NEGOCIO.Set_EliminarServicioTecnico";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STE_USU_ID", SqlDbType.UniqueIdentifier).Value= STE_USU_ID;
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in STE_ID)
            {
              sqlCommand.Parameters["@STE_ID"].Value = (object) guid;
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Set_EliminarServicioTecnico: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Set_EliminarServicioTecnico: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_CrearServicioTecnicov2(
      List<ServicioTecnico> ServicioTecnico,
      Guid UsuarioCrearServicio)
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
            sqlCommand.CommandText = "NEGOCIO.Set_CrearServicioTecnicov2";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ETQ", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@STE_OBSERVACION", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@STE_PRO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STE_CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STE_USUARIO_SOLICITA", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STE_CODIGO_SAP", SqlDbType.VarChar, (int) byte.MaxValue);
            sqlCommand.Parameters.Add("@STE_INTERNO_EXTERNO", SqlDbType.Bit);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (ServicioTecnico servicioTecnico in ServicioTecnico)
            {
              sqlCommand.Parameters["@ETQ"].Value = (object) servicioTecnico.ETQ_BC;
              sqlCommand.Parameters["@STE_OBSERVACION"].Value = (object) servicioTecnico.STE_OBSERVACION;
              sqlCommand.Parameters["@STE_PRO_ID"].Value = (object) (servicioTecnico.STE_PRO_ID.Equals((object) "00000000-0000-0000-0000-000000000000") ? new Guid?() : servicioTecnico.STE_PRO_ID);
              sqlCommand.Parameters["@STE_CLI_ID"].Value = (object) (servicioTecnico.STE_CLI_ID.Equals((object) "00000000-0000-0000-0000-000000000000") ? new Guid?() : servicioTecnico.STE_CLI_ID);
              sqlCommand.Parameters["@STE_CODIGO_SAP"].Value = (object) servicioTecnico.STE_CODIGO_SAP;
              sqlCommand.Parameters["@STE_INTERNO_EXTERNO"].Value = (object) servicioTecnico.STE_INTERNO_EXTERNO;
              sqlCommand.Parameters["@STE_USUARIO_SOLICITA"].Value = (object) UsuarioCrearServicio;
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Set_CrearServicioTecnicov2: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Set_CrearServicioTecnicov2: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_CrearServicioTecnicoIncidencia(
      List<ServicioTecnicoIncidencia> Incidencia,
      Guid UsuarioCrearServicioIncidencia)
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
            sqlCommand.CommandText = "NEGOCIO.Set_CrearServicioTecnicoIncidencia";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STI_STE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STI_USUARIO_CREA", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STI_OBSERVACION", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@STI_STP_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (ServicioTecnicoIncidencia tecnicoIncidencia in Incidencia)
            {
              sqlCommand.Parameters["@STI_STE_ID"].Value = (object) tecnicoIncidencia.STI_STE_ID;
              sqlCommand.Parameters["@STI_OBSERVACION"].Value = (object) tecnicoIncidencia.STI_OBSERVACION;
              sqlCommand.Parameters["@STI_STP_ID"].Value = (object) tecnicoIncidencia.STI_STP_ID;
              sqlCommand.Parameters["@STI_USUARIO_CREA"].Value = (object) UsuarioCrearServicioIncidencia;
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Set_CrearServicioTecnicoIncidencia: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Set_CrearServicioTecnicoIncidencia: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_EliminarServicioTecnicoIncidente(
      List<Guid> STI_ID,
      Guid UsuarioModificaServicioIncidencia)
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
            sqlCommand.CommandText = "NEGOCIO.Set_EliminarServicioTecnicoIncidente";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STI_USUARIO_MODIFICA", SqlDbType.UniqueIdentifier).Value = (object) UsuarioModificaServicioIncidencia;
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in STI_ID)
            {
              sqlCommand.Parameters["@STI_ID"].Value = (object) guid;
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Set_EliminarServicioTecnicoIncidente: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Set_EliminarServicioTecnicoIncidente: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_CrearServicioTecnicoImagenes(
      List<ServicioTecnicoImagen> NuevaActivoImagen,
      Guid UsuarioActivoImgCrear)
    {
      Mensaje mensaje = new Mensaje();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Set_CrearServicioTecnicoImagenes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STM_STI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STM_IMAGEN", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@STM_USUARIO_CREATE", SqlDbType.UniqueIdentifier).Value = (object) UsuarioActivoImgCrear;
            mensaje.errNumber = -1;
            foreach (ServicioTecnicoImagen servicioTecnicoImagen in NuevaActivoImagen)
            {
              sqlCommand.Parameters["@STM_STI_ID"].Value = (object) servicioTecnicoImagen.STM_STI_ID;
              sqlCommand.Parameters["@STM_IMAGEN"].Value = (object) servicioTecnicoImagen.RUTA;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                if (sqlDataReader.Read())
                  servicioTecnicoImagen.STM_ID = sqlDataReader.GetGuid(0);
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Set_CrearServicioTecnicoImagenes: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Set_CrearServicioTecnicoImagenes: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_EliminarServicioTecnicoImagen(
      List<Guid> STM_ID,
      Guid STM_USUARIO_MODIFICA)
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
            sqlCommand.CommandText = "NEGOCIO.Set_EliminarServicioTecnicoImagen";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STM_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STM_USUARIO_MODIFICA", SqlDbType.UniqueIdentifier).Value = (object) STM_USUARIO_MODIFICA;
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in STM_ID)
            {
              sqlCommand.Parameters["@STM_ID"].Value = (object) guid;
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Set_EliminarServicioTecnicoImagen: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Set_EliminarServicioTecnicoImagen: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_CrearServicioTecnicoRepuesto(
      List<ServicioTecnicoRepuesto> Repuesto,
      Guid USUARIO_REPUESTO_CREA)
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
            sqlCommand.CommandText = "NEGOCIO.Set_CrearServicioTecnicoRepuesto";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STR_STE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STR_CODIGO_REPUESTO", SqlDbType.VarChar, (int) byte.MaxValue);
            sqlCommand.Parameters.Add("@STR_CANTIDAD", SqlDbType.Float);
            sqlCommand.Parameters.Add("@STR_OBSERVACION", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@STR_USUARIO_CREA", SqlDbType.UniqueIdentifier).Value = (object) USUARIO_REPUESTO_CREA;
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (ServicioTecnicoRepuesto servicioTecnicoRepuesto in Repuesto)
            {
              sqlCommand.Parameters["@STR_STE_ID"].Value = (object) servicioTecnicoRepuesto.STR_STE_ID;
              sqlCommand.Parameters["@STR_CODIGO_REPUESTO"].Value = (object) servicioTecnicoRepuesto.STR_CODIGO_REPUESTO;
              sqlCommand.Parameters["@STR_CANTIDAD"].Value = (object) servicioTecnicoRepuesto.STR_CANTIDAD;
              sqlCommand.Parameters["@STR_OBSERVACION"].Value = (object) servicioTecnicoRepuesto.STR_OBSERVACION;
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Set_CrearServicioTecnicoRepuesto: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Set_CrearServicioTecnicoRepuesto: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_EliminarServicioTecnicoRepuesto(
      List<Guid> STM_ID,
      Guid STR_USUARIO_MODIFICA)
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
            sqlCommand.CommandText = "NEGOCIO.Set_EliminarServicioTecnicoRepuesto";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@STR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@STR_USUARIO_MODIFICA", SqlDbType.UniqueIdentifier).Value = (object) STR_USUARIO_MODIFICA;
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in STM_ID)
            {
              sqlCommand.Parameters["@STR_ID"].Value = (object) guid;
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
        ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Set_EliminarServicioTecnicoRepuesto: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        ServicioTecnicoDataBase.logger.Error(ex, "Error en Set_EliminarServicioTecnicoRepuesto: " + ex.Message);
      }
      return mensaje;
    }

        public Mensaje Get_Validar_Activo_ServicioTecnico (string ETQ_BC)
        {
            Mensaje mensaje = new Mensaje();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_Validar_Activo_ServicioTecnico ";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@ETQ", SqlDbType.VarChar, 100);
                        sqlCommand.Parameters["@ETQ"].Value = (object)ETQ_BC;
                       
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
            catch (SqlException ex)
            {
                mensaje.errNumber = -1;
                mensaje.message = ex.Message;
                ServicioTecnicoDataBase.logger.Error<SqlException>("Sql error en Get_Validar_Activo_ServicioTecnico: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                mensaje.errNumber = -2;
                mensaje.message = ex.Message;
                ServicioTecnicoDataBase.logger.Error(ex, "Error en Get_Validar_Activo_ServicioTecnico: " + ex.Message);
            }
            return mensaje;
        }
    }
}
