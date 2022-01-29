// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Factory.AuditoriaDataBase
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
  public class AuditoriaDataBase : IAuditoria
  {
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
    private Logger logger = LogManager.GetCurrentClassLogger();

    public IEnumerable<Estados> Get_list_estadoAuditoria()
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
            sqlCommand.CommandText = "NEGOCIO.Get_list_estadoAuditoria";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                estadosList.Add(new Estados()
                {
                  EST_ID = sqlDataReader.GetGuid(0),
                  EST_DESC = sqlDataReader.GetString(1),
                  ESA_ORDER = sqlDataReader.GetInt32(2)
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_estadoAuditoria: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_estadoAuditoria: " + ex.Message);
      }
      return (IEnumerable<Estados>) estadosList;
    }

    public IEnumerable<Estados> Get_listToma()
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
            sqlCommand.CommandText = "NEGOCIO.Get_listToma";
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
        this.logger.Error<SqlException>("Sql error en Get_listToma: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_listToma: " + ex.Message);
      }
      return (IEnumerable<Estados>) estadosList;
    }

    public IEnumerable<Auditoria> Get_list_Auditorias(
      Guid? AUD_ID,
      Guid? ESA_ID,
      DateTime? FECHA_INICIAL,
      DateTime? FECHA_FINAL)
    {
      List<Auditoria> auditoriaList = new List<Auditoria>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Auditorias";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ESA_ID", SqlDbType.UniqueIdentifier).Value = (object) ESA_ID;
            sqlCommand.Parameters.Add("@AUD_ID", SqlDbType.UniqueIdentifier).Value = (object) AUD_ID;
            if (FECHA_INICIAL.HasValue)
              sqlCommand.Parameters.Add("@FECHA_INICIAL", SqlDbType.NVarChar, 8).Value = (object) FECHA_INICIAL.Value.ToString("yyyyMMdd");
            if (FECHA_FINAL.HasValue)
              sqlCommand.Parameters.Add("@FECHA_FINAL", SqlDbType.NVarChar, 8).Value = (object) FECHA_FINAL.Value.ToString("yyyyMMdd");
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                auditoriaList.Add(new Auditoria()
                {
                  AUD_ID = new Guid?(sqlDataReader.GetGuid(0)),
                  AUD_OBSERVACION = sqlDataReader.GetString(1),
                  USU_LOGIN = sqlDataReader.GetString(2),
                  AUD_FECHA_CREATE = sqlDataReader.GetDateTime(3),
                  AUD_FECHA_CREATE_STR = sqlDataReader.GetDateTime(3).ToString("yyyy/MM/dd HH:mm"),
                  AUD_FECHA_EJECUCION = sqlDataReader.GetDateTime(4),
                  AUD_FECHA_EJECUCION_STR = sqlDataReader.GetDateTime(4).ToString("yyyy/MM/dd HH:mm"),
                  ESA_ID = new Guid?(sqlDataReader.GetGuid(5)),
                  ESA_DESC = sqlDataReader.GetString(6)
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_Auditorias: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_Auditorias: " + ex.Message);
      }
      return (IEnumerable<Auditoria>) auditoriaList.OrderByDescending< Auditoria,DateTime>((Func < Auditoria,DateTime>)(t=>t.AUD_FECHA_CREATE ));
            //(IEnumerable<Transaccion>)source.OrderByDescending<Transaccion, DateTime>((Func<Transaccion, DateTime>)(t => t.TRA_FECHA_CREATE_DATE));
        }

    public List<Auditoria> Get_list_Auditoria_detalle(
      Guid? AUD_ID,
      ref List<Areas> lstAreas,
      ref List<Responsable> lstResponsable,
      ref List<TipoActivo> lstTipoActivo)
    {
      List<Auditoria> auditoriaList = new List<Auditoria>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Auditoria_detalle";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AUD_ID", SqlDbType.UniqueIdentifier).Value = (object) AUD_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Auditoria auditoria1 = new Auditoria();
                auditoria1.AUD_ID = sqlDataReader.GetGuid(0);
                auditoria1.AUD_USUARIO_CREATE = sqlDataReader.GetGuid(1);                                         
                auditoria1.AUD_FECHA_CREATE_STR = sqlDataReader.GetDateTime(2).ToString("yyyy/MM/dd HH:mm"); ;
                auditoria1.AUD_FECHA_CREATE = sqlDataReader.GetDateTime(2);               
                auditoria1.AUD_OBSERVACION = sqlDataReader.GetValue(3) == DBNull.Value ? "" : sqlDataReader.GetString(3);                                             
                auditoria1.AUD_FECHA_EJECUCION_STR = sqlDataReader.GetDateTime(4).ToString("yyyy/MM/dd HH:mm");
                auditoria1.ESA_DESC = sqlDataReader.GetString(5);
                auditoria1.USU_LOGIN = sqlDataReader.GetString(6);
                auditoriaList.Add(auditoria1);
              }
              sqlDataReader.NextResult();
              while (sqlDataReader.Read())
              {
                Areas areas1 = new Areas();
                areas1.ARE_ID = sqlDataReader.GetGuid(0);
                areas1.ARE_DESC = sqlDataReader.GetString(1) == null ? "" : sqlDataReader.GetString(1);
                //try
                //{                                  
                  areas1.ARE_ARE_PARENT_ID = sqlDataReader.GetValue(2)==DBNull.Value ? Guid.Parse("00000000-0000-0000-0000-000000000000"):sqlDataReader.GetGuid(2);
                  areas1.ARE_DESC_PADRE = sqlDataReader.GetValue(3) == DBNull.Value ? "" : sqlDataReader.GetString(3);
                //} catch {}                
                  areas1.ARE_DESC_CENTROCOSTO = sqlDataReader.GetValue(4) == DBNull.Value ? "" : sqlDataReader.GetString(4);                
                  areas1.ARE_DESC_TIPOAREA = sqlDataReader.GetValue(5) == DBNull.Value ? "" : sqlDataReader.GetString(5);               
                  areas1.ARE_OBSERVACIONES = sqlDataReader.GetValue(6) == DBNull.Value ? "" : sqlDataReader.GetString(6);               
                  areas1.ARE_CIUDAD = sqlDataReader.GetValue(7) == DBNull.Value ? "" : sqlDataReader.GetString(7);               
                  areas1.ARE_DIRECCION = sqlDataReader.GetValue(8) == DBNull.Value ? "" : sqlDataReader.GetString(8);                
                  areas1.ARE_CODIGO_POSTAL = sqlDataReader.GetValue(9) == DBNull.Value ? "" : sqlDataReader.GetString(9);               
                  areas1.ARE_TAR_ID = sqlDataReader.GetGuid(10);                             
                  areas1.ARE_CCO_ID = sqlDataReader.GetGuid(11);               
                  areas1.ARE_ACTIVE = sqlDataReader.GetInt32 (12)!=0;  
                lstAreas.Add(areas1);              

              }
              sqlDataReader.NextResult();
              while (sqlDataReader.Read())
              {
                Responsable responsable = new Responsable();
                responsable.RES_ID = sqlDataReader.GetGuid(0);
                responsable.RES_DOCUMENTO = sqlDataReader.GetValue(1) == DBNull.Value ? "" : sqlDataReader.GetString(1); 
                responsable.RES_NOMBRES = sqlDataReader.GetValue(2) == DBNull.Value ? "" : sqlDataReader.GetString(2); 
                responsable.RES_APELLIDOS = sqlDataReader.GetValue(3) == DBNull.Value ? "" : sqlDataReader.GetString(3); 
                responsable.RES_CARGO = sqlDataReader.GetValue(4) == DBNull.Value ? "" : sqlDataReader.GetString(4); 
                responsable.RES_CORREO = sqlDataReader.GetValue(5) == DBNull.Value ? "" : sqlDataReader.GetString(5); 
                responsable.RES_PHOTO_1 = sqlDataReader.GetValue(6) == DBNull.Value ? "" : sqlDataReader.GetString(6); 
                responsable.RES_CELULAR = sqlDataReader.GetValue(7) == DBNull.Value ? "" : sqlDataReader.GetString(7); 
                responsable.RES_TELEFONO = sqlDataReader.GetValue(8) == DBNull.Value ? "" : sqlDataReader.GetString(8); 
                responsable.RES_EXT = sqlDataReader.GetValue(9) == DBNull.Value ? "" : sqlDataReader.GetString(9); 
                responsable.RES_OBSERVACION = sqlDataReader.GetValue(10) == DBNull.Value ? "" : sqlDataReader.GetString(10); 
                responsable.RES_ACTIVE = sqlDataReader.GetValue(11) == DBNull.Value ? false:sqlDataReader.GetBoolean(11);
                responsable.SELECCIONADO = sqlDataReader.GetInt32(19) != 0;
                lstResponsable.Add(responsable);
              }
              sqlDataReader.NextResult();
              while (sqlDataReader.Read())
              {
                TipoActivo tipoActivo = new TipoActivo();
                tipoActivo.TAC_ID = sqlDataReader.GetGuid(0);
                tipoActivo.TAC_DESC = sqlDataReader.GetString(1);
                tipoActivo.TAC_RETORNABLE = sqlDataReader.GetBoolean(2);                
                tipoActivo.SELECCIONADO = sqlDataReader.GetValue(11) == DBNull.Value ? false : sqlDataReader.GetInt32(11) != 0;               
                lstTipoActivo.Add(tipoActivo);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_Auditorias: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_Auditorias: " + ex.Message);
      }
      return auditoriaList;
    }

    public IEnumerable<Activos> Get_list_PrevisualizacionAuditoria(
      Guid? AUD_ID,
      Guid? AUD_USUARIO_CREATE)
    {
      List<Activos> activosList = new List<Activos>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_PrevisualizacionAuditoria";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AUD_ID", SqlDbType.UniqueIdentifier).Value = (object) AUD_ID;
            sqlCommand.Parameters.Add("@AUD_USUARIO", SqlDbType.UniqueIdentifier).Value = (object) AUD_USUARIO_CREATE;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())                
                {
                    Activos fila = new Activos();
                    try {fila.ACT_ID = sqlDataReader.GetGuid(0);} catch { }
                    try {fila.ACT_DESC = sqlDataReader.GetString(1);} catch { }
                    try {fila.ACT_SERIAL = sqlDataReader.GetString(2);}catch { }
                    try{fila.ACT_EST_DESC = sqlDataReader.GetString(3);}catch { }
                    try{fila.ARE_DESC = sqlDataReader.GetString(4);} catch { }
                    try{fila.ACT_TAC_DESC = sqlDataReader.GetString(5); } catch { }
                    try{fila.RES_NOMBRE_APELLIDO = sqlDataReader.GetString(6);} catch { }
                    try{fila.ACT_EST_ID = sqlDataReader.GetGuid(7);}catch { }
                    try{fila.ARE_ID = sqlDataReader.GetGuid(8);}catch { }
                    try{fila.RES_ID = sqlDataReader.GetGuid(9); }catch { }
                    try{fila.ACT_ETQ_BC = sqlDataReader.GetString(10);}catch { }
                    try{ fila.ACT_ETQ_CODE = sqlDataReader.GetString(11); }catch { }
                    try{ fila.ACT_ETQ_EPC = sqlDataReader.GetString(12); }catch { }
                 activosList.Add(fila);
                        }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_PrevisualizacionAuditoria: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_PrevisualizacionAuditoria: " + ex.Message);
      }
      return (IEnumerable<Activos>) activosList;
    }

    public Mensaje Set_Crear_Detalle_Auditoria(
      List<AuditoriaDetalle> AUDITORIADET,
      Guid? UsuarioAuditoriaDetCrear)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Detalle_Auditoria";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AUD_USUARIO_CREATE", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@NUMEROAUDITORIA", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@LISTATIPOACTIVO", SqlDbType.VarChar, 12500);
            sqlCommand.Parameters.Add("@LISTAAREA", SqlDbType.VarChar, 12500);
            sqlCommand.Parameters.Add("@LISTARESPONSABLE", SqlDbType.VarChar, 12500);
            sqlCommand.Parameters.Add("@VALIDARLISTATIPODATOACTIVO", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@VALIDARLISTARESPONSABLE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@mostrarGrid", SqlDbType.Bit).Value = (object) true;
            sqlCommand.Parameters.Add("@errNumber", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add("@MENSAJE", SqlDbType.VarChar, 12500).Direction = ParameterDirection.Output;
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (AuditoriaDetalle auditoriaDetalle in AUDITORIADET)
            {
              sqlCommand.Parameters["@errNumber"].Value = (object) 0;
              sqlCommand.Parameters["@AUD_USUARIO_CREATE"].Value = (object) UsuarioAuditoriaDetCrear;
              sqlCommand.Parameters["@NUMEROAUDITORIA"].Value = (object) (auditoriaDetalle.NUMEROAUDITORIA.Equals((object) "00000000 - 0000 - 0000 - 0000 - 000000000000") ? new Guid?() : auditoriaDetalle.NUMEROAUDITORIA);
              sqlCommand.Parameters["@LISTATIPOACTIVO"].Value = (object) auditoriaDetalle.LISTATIPOACTIVO;
              sqlCommand.Parameters["@LISTAAREA"].Value = (object) auditoriaDetalle.LISTAAREA;
              sqlCommand.Parameters["@LISTARESPONSABLE"].Value = (object) auditoriaDetalle.LISTARESPONSABLE;
              sqlCommand.Parameters["@VALIDARLISTATIPODATOACTIVO"].Value = (object) auditoriaDetalle.VALIDARLISTATIPODATOACTIVO;
              sqlCommand.Parameters["@VALIDARLISTARESPONSABLE"].Value = (object) auditoriaDetalle.VALIDARLISTARESPONSABLE;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_Detalle_Auditoria: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Detalle_Auditoria: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Asignar_Auditoria(
      List<Auditoria> AUDITORIAASIG,
      Guid? UsuarioAuditoriaAsignar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Asignar_Auditoria";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AUD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@LISTATOMAUSUARIO", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@AII_USUARIO_CREATE", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Auditoria auditoria in AUDITORIAASIG)
            {
                sqlCommand.Parameters["@AUD_ID"].Value = auditoria.AUD_ID == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : auditoria.AUD_ID;
                sqlCommand.Parameters["@LISTATOMAUSUARIO"].Value = auditoria.LISTATOMAUSUARIO;
                sqlCommand.Parameters["@AII_USUARIO_CREATE"].Value = UsuarioAuditoriaAsignar;
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
        this.logger.Error<SqlException>("Sql error en Set_Asignar_Auditoria: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Asignar_Auditoria: " + ex.Message);
      }
      return mensaje;
    }

    public List<ActivosImagenes> Get_listar_AuditoriaImagenes(
      Guid? AIM_ATO_ID,
      Guid? AIM_ACT_ID)
    {
      List<ActivosImagenes> activosImagenesList = new List<ActivosImagenes>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_listar_AuditoriaImagenes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AIM_ATO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@AIM_ATO_ID"].Value = (object) AIM_ATO_ID;
            sqlCommand.Parameters["@AIM_ACT_ID"].Value = (object) AIM_ACT_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                ActivosImagenes activosImagenes = new ActivosImagenes();
                activosImagenes.AIM_ID = sqlDataReader.GetGuid(0);
                activosImagenes.AIM_ACT_ID = new Guid?(sqlDataReader.GetGuid(1));
                try
                {
                activosImagenes.AIM_RUTA = activosImagenes.AIM_ID.ToString() + "." + sqlDataReader.GetString(3);
                activosImagenes.AIM_OBSERVACION = sqlDataReader.GetString(2);                 
                 }
                catch
                {
                  activosImagenes.AIM_OBSERVACION = "";
                }
                activosImagenesList.Add(activosImagenes);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_listar_AuditoriaImagenes: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_listar_AuditoriaImagenes: " + ex.Message);
      }
      return activosImagenesList;
    }

    public List<ActivosImagenes> Get_listar_AuditoriaImagenesV2(
     Guid? AIM_AUD_ID,
     Guid? AIM_ACT_ID)
        {
            List<ActivosImagenes> activosImagenesList = new List<ActivosImagenes>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_listar_AuditoriaImagenesV2";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@AIM_AUD_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@AIM_ACT_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters["@AIM_AUD_ID"].Value = (object)AIM_AUD_ID;
                        sqlCommand.Parameters["@AIM_ACT_ID"].Value = (object)AIM_ACT_ID;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                ActivosImagenes activosImagenes = new ActivosImagenes();
                                activosImagenes.AIM_ID = sqlDataReader.GetGuid(0);
                                activosImagenes.AIM_ACT_ID = new Guid?(sqlDataReader.GetGuid(1));
                                try
                                {
                                    activosImagenes.AIM_RUTA = activosImagenes.AIM_ID.ToString()+"."+ sqlDataReader.GetString(3);
                                    activosImagenes.AIM_OBSERVACION = sqlDataReader.GetString(5);
                                }
                                catch
                                {
                                    activosImagenes.AIM_OBSERVACION = "";
                                }
                                activosImagenesList.Add(activosImagenes);
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                this.logger.Error<SqlException>("Sql error en Get_listar_AuditoriaImagenesV2: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error en Get_listar_AuditoriaImagenesV2: " + ex.Message);
            }
            return activosImagenesList;
        }
        public Mensaje Set_Crear_Auditoria(
      List<AuditoriaDetalle> AUDITORIA,
      Guid? UsuarioAuditoriaCrear)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Auditoria";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AUD_OBSERVACION", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@AUD_FECHA_EJECUCION", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@LISTATIPOACTIVO", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@LISTAAREA", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@LISTARESPONSABLE", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@VALIDARLISTATIPODATOACTIVO", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@VALIDARLSITARESPONSABLE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@AUD_USUARIO_CREATE", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (AuditoriaDetalle auditorium in AUDITORIA)
            {
              sqlCommand.Parameters["@AUD_FECHA_EJECUCION"].Value = (object) this.helper.Fecha(auditorium.AUD_FECHA_EJECUCION);
              sqlCommand.Parameters["@AUD_OBSERVACION"].Value = (object) auditorium.AUD_OBSERVACION;
              sqlCommand.Parameters["@LISTATIPOACTIVO"].Value = (object) auditorium.LISTATIPOACTIVO;
              sqlCommand.Parameters["@LISTAAREA"].Value = (object) auditorium.LISTAAREA;
              sqlCommand.Parameters["@LISTARESPONSABLE"].Value = (object) auditorium.LISTARESPONSABLE;
              sqlCommand.Parameters["@VALIDARLISTATIPODATOACTIVO"].Value = (object) auditorium.VALIDARLISTATIPODATOACTIVO;
              sqlCommand.Parameters["@VALIDARLSITARESPONSABLE"].Value = (object) auditorium.VALIDARLISTARESPONSABLE;
              sqlCommand.Parameters["@AUD_USUARIO_CREATE"].Value = (object) UsuarioAuditoriaCrear;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_Auditoria: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Auditoria: " + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<AuditoriaUsuario> Get_list_ListarAuditoriasPorUsuario(
      Guid ATO_USUARIO_TOMA)
    {
      List<AuditoriaUsuario> auditoriaUsuarioList = new List<AuditoriaUsuario>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_ListarAuditoriasPorUsuario";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ATO_USUARIO_TOMA", SqlDbType.UniqueIdentifier).Value = (object) ATO_USUARIO_TOMA;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                AuditoriaUsuario auditoriaUsuario = new AuditoriaUsuario();
                auditoriaUsuario.ATO_ID = new Guid?(sqlDataReader.GetGuid(0));
                auditoriaUsuario.ATO_AUD_ID = new Guid?(sqlDataReader.GetGuid(1));
                auditoriaUsuario.ATO_ORDEN = sqlDataReader.GetInt16(2);
                auditoriaUsuario.TOM_ID = new Guid?(sqlDataReader.GetGuid(3));
                auditoriaUsuario.TOM_DESCRIPCION = sqlDataReader.GetString(4);
                try
                {
                  auditoriaUsuario.AUD_OBSERVACION = sqlDataReader.GetString(5);
                  auditoriaUsuario.AUD_USUARIO_CREATE = sqlDataReader.GetString(6);
                }
                catch
                {
                }
                try
                {
                  auditoriaUsuario.AUD_FECHA_EJECUCION = sqlDataReader.GetDateTime(7);
                  auditoriaUsuario.AUD_FECHA_EJECUCION_STR = sqlDataReader.GetDateTime(7).ToString("yyyy/MM/dd");
                  auditoriaUsuario.AET_DESCRIPCION = sqlDataReader.GetString(8);
                }
                catch
                {
                }
                auditoriaUsuarioList.Add(auditoriaUsuario);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_ListarAuditoriasPorUsuario: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_ListarAuditoriasPorUsuario: " + ex.Message);
      }
      return (IEnumerable<AuditoriaUsuario>) auditoriaUsuarioList;
    }

    public Mensaje Set_CapturaAuditoria(List<AuditoriaCaptura> AUDITORIA, Guid USUARIO_TOMA)
    {
      Mensaje mensaje = new Mensaje();
      string str = "";
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Set_CapturaAuditoria";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ACP_ETA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CAP_ATO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@USUARIO_TOMA", SqlDbType.UniqueIdentifier).Value = (object)USUARIO_TOMA;
            sqlCommand.Parameters.Add("@LISTACARACTERISTICAVALOR", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@ACP_ARE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACP_RES_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACP_FECHA_CAPTURA", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@ETQ", SqlDbType.NVarChar);           
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (AuditoriaCaptura auditorium in AUDITORIA)
            {
                sqlCommand.Parameters["@ACP_ETA_ID"].Value = (object) auditorium.ACP_ETA_ID;
                sqlCommand.Parameters["@CAP_ATO_ID"].Value = (object) auditorium.CAP_ATO_ID;
                sqlCommand.Parameters["@LISTACARACTERISTICAVALOR"].Value = (object) auditorium.LISTACARACTERISTICAVALOR;
                sqlCommand.Parameters["@ACP_ARE_ID"].Value = (object)auditorium.ACP_ARE_ID;
                sqlCommand.Parameters["@ACP_RES_ID"].Value = (object)auditorium.ACP_RES_ID;
                sqlCommand.Parameters["@ACP_FECHA_CAPTURA"].Value = (object) auditorium.ACP_FECHA_CAPTURA;
                sqlCommand.Parameters["@ETQ"].Value = (object) auditorium.ETIQUETA;              
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
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Set_CapturaAuditoria: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Set_CapturaAuditoria: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_CrearAuditoriaImagenes(
      List<ActivosImagenes> NuevaActivoImagen,
      Guid UsuarioAuditoriaImgCrear)
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
            sqlCommand.CommandText = "NEGOCIO.Set_CrearAuditoriaImagenes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AIM_ATO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_RUTA", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AIM_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = -1;
            foreach (ActivosImagenes activosImagenes in NuevaActivoImagen)
            {
              sqlCommand.Parameters["@AIM_ATO_ID"].Value = (object) activosImagenes.AIM_TRA_ID;
              sqlCommand.Parameters["@AIM_ACT_ID"].Value = (object) activosImagenes.AIM_ACT_ID;
              sqlCommand.Parameters["@AIM_RUTA"].Value = (object) activosImagenes.AIM_RUTA;
              sqlCommand.Parameters["@AIM_USUARIO_CREATE_ID"].Value = (object) UsuarioAuditoriaImgCrear;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                try { 
                    if (sqlDataReader.Read())
                      activosImagenes.AIM_ID = sqlDataReader.GetGuid(0);
                      sqlDataReader.NextResult();
                        if (sqlDataReader.Read())
                        {
                            mensaje.errNumber = sqlDataReader.GetInt32(0);
                            mensaje.message = sqlDataReader.GetString(1);
                        }
                    }
                 catch
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
        this.logger.Error<SqlException>("Sql error en Set_CrearAuditoriaImagenes: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_CrearAuditoriaImagenes: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Cerrar_Toma(Guid ATO_ID)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Cerrar_Toma";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ATO_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = -1;
            sqlCommand.Parameters["@ATO_ID"].Value = (object) ATO_ID;
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
        this.logger.Error<SqlException>("Sql error en Set_Cerrar_Toma: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Cerrar_Toma: " + ex.Message);
      }
      return mensaje;
    }

    public AuditoriaTomaDatos Get_list_TomaDatosPor_Auditoria(Guid? AUD_ID)
    {
            AuditoriaTomaDatos auditoriaTomaList = new AuditoriaTomaDatos();
            List<AuditoriaToma> lstActivos = new List<AuditoriaToma>();
            List<AuditoriaToma> lstAreas = new List<AuditoriaToma>();
            List<AuditoriaToma> lstResponsable = new List<AuditoriaToma>();
            List<AuditoriaToma> lstCaracteristica = new List<AuditoriaToma>();
            try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_TomaDatosPor_Auditoria";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AUD_ID", SqlDbType.UniqueIdentifier).Value = (object)AUD_ID;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                            while (sqlDataReader.Read())
                            {
                                AuditoriaToma fila = new AuditoriaToma();
                                fila.ALL_ACT_ID = sqlDataReader.GetGuid(0);
                                try { fila.CAR_RES_ARE_DESC = sqlDataReader.GetString(1); } catch { }
                                try { fila.TAC_DESC = sqlDataReader.GetString(2); } catch { }
                                try { fila.MOD_DESC = sqlDataReader.GetString(3); } catch { }
                                try { fila.ACT_DESC = sqlDataReader.GetString(4); } catch { }
                                try { fila.ALL_ACT_Serial = sqlDataReader.GetString(5); } catch { }
                                try { fila.SELECCIONADO = sqlDataReader.GetBoolean(6); } catch { }                               
                                try { fila.ACP_AJUSTADO = sqlDataReader.GetBoolean(7); } catch { }
                                lstActivos.Add(fila);
                            }
                            sqlDataReader.NextResult();
                            while (sqlDataReader.Read())                                
                {
                 AuditoriaToma fila = new AuditoriaToma();
                                fila.ALL_ACT_ID = sqlDataReader.GetGuid(0);
                                try { fila.CAR_RES_ARE_DESC =  sqlDataReader.GetString(1); } catch { }
                                try { fila.TAC_DESC =  sqlDataReader.GetString(2); } catch { }
                                try { fila.MOD_DESC =  sqlDataReader.GetString(3); } catch { }
                                try { fila.ACT_DESC =  sqlDataReader.GetString(4); } catch { }
                                try { fila.ALL_ACT_Serial = sqlDataReader.GetString(5); } catch { }
                                //try { fila.SELECCIONADO = sqlDataReader.GetBoolean(6); } catch { }
                                try { fila.TOMA1 = sqlDataReader.GetGuid(6); } catch { }
                                try { fila.VALORRESAREATOMA1 =  sqlDataReader.GetString(7); } catch { }
                                try { fila.SELECCIONADOTOMA1 =  sqlDataReader.GetBoolean(8); } catch { }
                                try { fila.TOMA2 = sqlDataReader.GetGuid(9); } catch { }
                                try { fila.VALORRESAREATOMA2 =  sqlDataReader.GetString(10); } catch { }
                                try { fila.SELECCIONADOTOMA2 =  sqlDataReader.GetBoolean(11); } catch { }
                                try { fila.TOMADEF = sqlDataReader.GetGuid(12); } catch { }
                                try { fila.VALORRESAREATOMADEF =  sqlDataReader.GetString(13); } catch { }
                                try { fila.SELECCIONADOTOMADEF =  sqlDataReader.GetBoolean(14); } catch { }
                                try { fila.ACP_AJUSTADO = sqlDataReader.GetBoolean(15); } catch { }
                                 lstAreas.Add(fila); 
                                }
                sqlDataReader.NextResult();
                while (sqlDataReader.Read())
                               
                    {
                                AuditoriaToma fila = new AuditoriaToma();
                                fila.ALL_ACT_ID = sqlDataReader.GetGuid(0);
                                try { fila.CAR_RES_ARE_DESC = sqlDataReader.GetString(1); } catch { }
                                try { fila.TAC_DESC = sqlDataReader.GetString(2); } catch { }
                                try { fila.MOD_DESC = sqlDataReader.GetString(3); } catch { }
                                try { fila.ACT_DESC = sqlDataReader.GetString(4); } catch { }
                                try { fila.ALL_ACT_Serial = sqlDataReader.GetString(5); } catch { }
                                try { fila.SELECCIONADO = sqlDataReader.GetBoolean(6); } catch { }
                                try { fila.TOMA1 = sqlDataReader.GetGuid(7); } catch { }
                                try { fila.VALORRESAREATOMA1 = sqlDataReader.GetString(8); } catch { }
                                try { fila.SELECCIONADOTOMA1 = sqlDataReader.GetBoolean(9); } catch { }
                                try { fila.TOMA2 = sqlDataReader.GetGuid(10); } catch { }
                                try { fila.VALORRESAREATOMA2 = sqlDataReader.GetString(11); } catch { }
                                try { fila.SELECCIONADOTOMA2 = sqlDataReader.GetBoolean(12); } catch { }
                                try { fila.TOMADEF = sqlDataReader.GetGuid(13); } catch { }
                                try { fila.VALORRESAREATOMADEF = sqlDataReader.GetString(14); } catch { }
                                try { fila.SELECCIONADOTOMADEF = sqlDataReader.GetBoolean(15); } catch { }
                                try { fila.ACP_AJUSTADO = sqlDataReader.GetBoolean(16); } catch { }
                                lstResponsable.Add(fila);
                            }
                sqlDataReader.NextResult();
                while (sqlDataReader.Read())
                            {
                                AuditoriaToma fila = new AuditoriaToma();
                                fila.ALL_ACT_ID = sqlDataReader.GetGuid(0);
                                try { fila.CAR_RES_ARE_DESC = sqlDataReader.GetString(1); } catch { }
                                try { fila.VALOR = sqlDataReader.GetString(2); } catch { }
                                try { fila.TAC_DESC = sqlDataReader.GetString(3); } catch { }
                                try { fila.MOD_DESC = sqlDataReader.GetString(4); } catch { }
                                try { fila.ACT_DESC = sqlDataReader.GetString(5); } catch { }
                                try { fila.ALL_ACT_Serial = sqlDataReader.GetString(6); } catch { }
                                try { fila.SELECCIONADO = sqlDataReader.GetBoolean(7); } catch { }
                                try { fila.TOMA1 = sqlDataReader.GetGuid(8); } catch { }
                                try { fila.VALORRESAREATOMA1 = sqlDataReader.GetString(9); } catch { }
                                try { fila.SELECCIONADOTOMA1 = sqlDataReader.GetBoolean(10); } catch { }
                                try { fila.TOMA2 = sqlDataReader.GetGuid(11); } catch { }
                                try { fila.VALORRESAREATOMA2 = sqlDataReader.GetString(12); } catch { }
                                try { fila.SELECCIONADOTOMA2 = sqlDataReader.GetBoolean(13); } catch { }
                                try { fila.TOMADEF = sqlDataReader.GetGuid(14); } catch { }
                                try { fila.VALORRESAREATOMADEF = sqlDataReader.GetString(15); } catch { }
                                try { fila.SELECCIONADOTOMADEF = sqlDataReader.GetBoolean(16); } catch { }
                                try { fila.ACP_AJUSTADO = sqlDataReader.GetBoolean(17); } catch { }
                                lstCaracteristica.Add(fila);
                            }
                            sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
                auditoriaTomaList.Activos = lstActivos;
                auditoriaTomaList.Areas = lstAreas;
                auditoriaTomaList.Responsables = lstResponsable;
                auditoriaTomaList.Caracteristicas = lstCaracteristica;
                //auditoriaTomaList.Add(new AuditoriaTomaDatos { Activos = lstActivos, Areas =lstAreas,Responsables =lstResponsable ,Caracteristicas =lstCaracteristica });
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_TomaDatosPor_Auditoria: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_TomaDatosPor_Auditoria: " + ex.Message);
      }
      return  auditoriaTomaList;
    }
    public Mensaje Set_Ajuste_Auditoria(Guid AUD_ID, Guid USU_ID)
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
                        sqlCommand.CommandText = "NEGOCIO.Set_Ajuste_Auditoria";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@AUD_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@USU_ID", SqlDbType.UniqueIdentifier);
                        mensaje.errNumber = -1;
                        mensaje.message = "No se obtuvo resultados del ajuste de auditoria";
                        sqlCommand.Parameters["@AUD_ID"].Value = (object)AUD_ID;
                        sqlCommand.Parameters["@USU_ID"].Value = (object)USU_ID;
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
                this.logger.Error<SqlException>("Sql error en Set_Ajuste_Auditoria: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                mensaje.errNumber = -2;
                mensaje.message = ex.Message;
                this.logger.Error(ex, "Error en Set_Ajuste_Auditoria: " + ex.Message);
            }
            return mensaje;
        }
    public Mensaje Set_Liberar_Auditoria(Guid AUD_ID)
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
                        sqlCommand.CommandText = "NEGOCIO.Set_Liberar_Auditoria";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@AUD_ID", SqlDbType.UniqueIdentifier);                      
                        mensaje.errNumber = -1;
                        mensaje.message = "No se obtuvo resultados de la liberación de auditoria";
                        sqlCommand.Parameters["@AUD_ID"].Value = (object)AUD_ID;                       
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
                this.logger.Error<SqlException>("Sql error en Set_Liberar_Auditoria: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                mensaje.errNumber = -2;
                mensaje.message = ex.Message;
                this.logger.Error(ex, "Error en Set_Liberar_Auditoria: " + ex.Message);
            }
            return mensaje;
        }
   public Mensaje Set_Eliminar_Auditoria(Guid? AUD_ID, Guid? AUD_USUARIO_DELETE_ID)
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
                        sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_Auditoria";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@AUD_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@AUD_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
                        mensaje.errNumber = -1;
                        mensaje.message = "No se obtuvo resultados de eliminar auditoria";
                        sqlCommand.Parameters["@AUD_ID"].Value = (object)AUD_ID;
                        sqlCommand.Parameters["@AUD_USUARIO_DELETE_ID"].Value = (object)AUD_USUARIO_DELETE_ID;
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
                this.logger.Error<SqlException>("Sql error en Set_Eliminar_Auditoria: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                mensaje.errNumber = -2;
                mensaje.message = ex.Message;
                this.logger.Error(ex, "Error en Set_Eliminar_Auditoria: " + ex.Message);
            }
            return mensaje;
        }
   public Mensaje Set_SeleccionarActivos_Toma(SeleccionActivoToma SELECCION)
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
                        sqlCommand.CommandText = "NEGOCIO.Set_SeleccionarActivos_Toma";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@AUD_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@ListaAuditoriaCaptura", SqlDbType.VarChar);
                        sqlCommand.Parameters.Add("@ListaActivosSinCaptura", SqlDbType.VarChar);
                        sqlCommand.Parameters.Add("@ListaAuditoriaCapturaResponsables", SqlDbType.VarChar);
                        sqlCommand.Parameters.Add("@ListaAuditoriaCapturaDetalle", SqlDbType.VarChar);
                        mensaje.errNumber = -1;
                        mensaje.message = "No se obtuvo resultados de seleccionar activos por toma";
                        sqlCommand.Parameters["@AUD_ID"].Value = (object)SELECCION.AUD_ID;
                        sqlCommand.Parameters["@ListaAuditoriaCaptura"].Value = (object)SELECCION.ListaAuditoriaCaptura;
                        sqlCommand.Parameters["@ListaActivosSinCaptura"].Value = (object)SELECCION.ListaActivosSinCaptura;
                        sqlCommand.Parameters["@ListaAuditoriaCapturaResponsables"].Value = (object)SELECCION.ListaAuditoriaCapturaResponsables;
                        sqlCommand.Parameters["@ListaAuditoriaCapturaDetalle"].Value = (object)SELECCION.ListaAuditoriaCapturaDetalle;
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
                this.logger.Error<SqlException>("Sql error en Set_SeleccionarActivos_Toma: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                mensaje.errNumber = -2;
                mensaje.message = ex.Message;
                this.logger.Error(ex, "Error en Set_SeleccionarActivos_Toma: " + ex.Message);
            }
            return mensaje;
        }
    }
}
