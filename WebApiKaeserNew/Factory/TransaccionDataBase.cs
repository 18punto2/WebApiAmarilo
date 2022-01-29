// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Factory.TransaccionDataBase
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
  public class TransaccionDataBase : ITransaccion
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();

    public List<Activos> Get_list_Activos(Guid? Activo_ID, string ETQ, Guid? ACT_RES_ID,DateTime? FECHA_MOD)
    {
      List<Activos> source = new List<Activos>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_activosV2";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ETQ", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@ACT_RES_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_FECHA_MOD", SqlDbType.DateTime);
            sqlCommand.Parameters["@ACT_ID"].Value = (object) Activo_ID;
            sqlCommand.Parameters["@ETQ"].Value = (object) ETQ;
            sqlCommand.Parameters["@ACT_RES_ID"].Value = (object) ACT_RES_ID;
            sqlCommand.Parameters["@ACT_FECHA_MOD"].Value = (object)FECHA_MOD;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Activos activos = new Activos();
                activos.ACT_ID = sqlDataReader.GetGuid(0);
                activos.ACT_DESC = sqlDataReader.GetString(1);
                activos.ACT_TAC_ID = sqlDataReader.GetGuid(2);
                activos.ACT_TAC_DESC = sqlDataReader.GetString(3);
                activos.ACT_MOD_ID = sqlDataReader.GetGuid(4);
                activos.ACT_MOD_DESC = sqlDataReader.GetString(5);
                activos.ACT_EST_ID = sqlDataReader.GetGuid(6);
                activos.ACT_EST_DESC = sqlDataReader.GetString(7);
                activos.ACT_SERIAL = sqlDataReader.GetString(8);
                try
                { activos.ACT_ACTIVE = sqlDataReader.GetBoolean(9); }
                catch { }
                try { activos.ACT_ETQ_BC = sqlDataReader.GetString(10); }
                catch  { }
                try { activos.ACT_ETQ_CODE = sqlDataReader.GetString(11); }
                catch  {}
                try { activos.ACT_ETQ_EPC = sqlDataReader.GetString(12);  }
                catch { }
                try { activos.ACT_ETQ_TID = sqlDataReader.GetString(13);  }
                catch { }
                try { activos.RES_ID = sqlDataReader.GetGuid(14);}
                catch { }
                try { activos.ETA_ID = sqlDataReader.GetGuid(18); }
                catch (Exception ex) { }
                try { activos.RES_NOMBRE_APELLIDO = sqlDataReader.GetString(19); }
                catch { }
                try { activos.ARE_ID = sqlDataReader.GetGuid(20); }
                catch (Exception ex) { }
                try { activos.ARE_DESC = sqlDataReader.GetString(21); }
                catch { }

                try { activos.PARTE_NUMBER = sqlDataReader.GetString(25); }
                catch { }

                try { activos.SERIAL = sqlDataReader.GetString(26); }
                catch { }

                                source.Add(activos);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_list_activos: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        TransaccionDataBase.logger.Error(ex, "Error en Get_list_activos: " + ex.Message);
      }
      return source.OrderBy<Activos, string>((Func<Activos, string>) (a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>) (t => t.ACT_TAC_DESC)).ToList<Activos>();
    }

    public Mensaje Get_validar_etiquetas(string ETQ_BC, string ETQ_CODE)
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
            sqlCommand.CommandText = "NEGOCIO.Get_validar_etiquetas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ETQ_BC", SqlDbType.VarChar, 100);
            sqlCommand.Parameters["@ETQ_BC"].Value = (object) ETQ_BC;
            sqlCommand.Parameters.Add("@ETQ_CODE", SqlDbType.VarChar, 100);
            sqlCommand.Parameters["@ETQ_CODE"].Value = (object) ETQ_CODE;
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_validar_etiquetas: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Get_validar_etiquetas: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Get_Validar_DetalleTransacciones(
      Guid DTR_TRA_ID,
      string ETQ,
      Guid DTR_USUARIO_CREATE_ID)
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
            sqlCommand.CommandText = "NEGOCIO.Get_Validar_DetalleTransacciones";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@DTR_TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@DTR_TRA_ID"].Value = (object) DTR_TRA_ID;
            sqlCommand.Parameters.Add("@ETQ", SqlDbType.VarChar, 100);
            sqlCommand.Parameters["@ETQ"].Value = (object) ETQ;
            sqlCommand.Parameters.Add("@DTR_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@DTR_USUARIO_CREATE_ID"].Value = (object) DTR_USUARIO_CREATE_ID;
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_Validar_DetalleTransacciones: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Get_Validar_DetalleTransacciones: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Crear_Activos(List<Activos> NuevaActivo, Guid UsuarioActivoCrear)
    {
      Mensaje mensaje = new Mensaje();
      string str1 = "";
      try
      {
        Guid guid = UsuarioActivoCrear;
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Activos";
            str1 = " Set_Crear_Activos";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ACT_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@ACT_TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_EST_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_Serial", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@ETQ_BC", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@ETQ_CODE", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@ACT_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = -1;
            foreach (Activos activos in NuevaActivo)
            {
              sqlCommand.Parameters["@ACT_DESC"].Value = (object) activos.ACT_DESC;
              sqlCommand.Parameters["@ACT_TAC_ID"].Value = (object) activos.ACT_TAC_ID;
              sqlCommand.Parameters["@ACT_MOD_ID"].Value = (object) activos.ACT_MOD_ID;
              sqlCommand.Parameters["@ACT_EST_ID"].Value = (object) activos.ACT_EST_ID;
              sqlCommand.Parameters["@ACT_Serial"].Value = (object) activos.ACT_SERIAL;
              sqlCommand.Parameters["@ETQ_BC"].Value = (object) activos.ACT_ETQ_BC;
              sqlCommand.Parameters["@ETQ_CODE"].Value = (object) activos.ACT_ETQ_CODE;
              sqlCommand.Parameters["@ACT_USUARIO_CREATE_ID"].Value = (object) UsuarioActivoCrear;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                if (sqlDataReader.Read())
                {
                  try
                  {
                    guid = sqlDataReader.GetGuid(2);
                    mensaje.data = (object) sqlDataReader.GetGuid(2);
                    mensaje.errNumber = sqlDataReader.GetInt32(0);
                    mensaje.message = sqlDataReader.GetString(1);
                  }
                  catch (Exception ex)
                  {
                    try
                    {
                      mensaje.errNumber = sqlDataReader.GetInt32(0);
                      mensaje.message = sqlDataReader.GetString(1);
                    }
                    catch
                    {
                    }
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
              if (mensaje.errNumber == 0)
              {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "NEGOCIO.Set_Asignar_ValoresActivos";
                str1 = " Set_Asignar_ValoresActivos";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@MOD_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@ACT_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@CAR_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@CVA_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@VALOR", SqlDbType.VarChar, 250);
                sqlCommand.Parameters.Add("@RESPONSABLE_CREACION", SqlDbType.UniqueIdentifier);
                if (activos.SELECCIONADOS != null)
                {
                  string seleccionados = activos.SELECCIONADOS;
                  char[] chArray = new char[1]{ '_' };
                  foreach (string str2 in seleccionados.Split(chArray))
                  {
                    sqlCommand.Parameters["@MOD_ID"].Value = (object) activos.ACT_MOD_ID;
                    sqlCommand.Parameters["@ACT_ID"].Value = (object) guid;
                    sqlCommand.Parameters["@CAR_ID"].Value = (object) Guid.Parse(str2.Split('|')[0]);
                    if (!str2.Split('|')[2].Trim().Equals(""))
                      sqlCommand.Parameters["@CVA_ID"].Value = (object) Guid.Parse(str2.Split('|')[2]);
                    else
                      sqlCommand.Parameters["@CVA_ID"].Value = (object) null;
                    sqlCommand.Parameters["@VALOR"].Value = (object) str2.Split('|')[1];
                    sqlCommand.Parameters["@RESPONSABLE_CREACION"].Value = (object) UsuarioActivoCrear;
                    sqlCommand.ExecuteNonQuery();
                  }
                }
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en : " + ex.Message + str1, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en : " + ex.Message + str1);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Activos(List<Activos> EditarActivo, Guid UsuarioActivoEditar)
    {
      Mensaje mensaje = new Mensaje();
      string str1 = "";
      try
      {
        string str2 = "";
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Activos";
            str1 = nameof (Set_Editar_Activos);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@ACT_TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_EST_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_Serial", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@ACT_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str2;
            foreach (Activos activos in EditarActivo)
            {
              sqlCommand.Parameters["@ACT_ID"].Value = (object) activos.ACT_ID;
              sqlCommand.Parameters["@ACT_DESC"].Value = (object) activos.ACT_DESC;
              sqlCommand.Parameters["@ACT_TAC_ID"].Value = (object) activos.ACT_TAC_ID;
              sqlCommand.Parameters["@ACT_MOD_ID"].Value = (object) activos.ACT_MOD_ID;
              sqlCommand.Parameters["@ACT_EST_ID"].Value = (object) activos.ACT_EST_ID;
              sqlCommand.Parameters["@ACT_Serial"].Value = (object) activos.ACT_SERIAL;
              sqlCommand.Parameters["@ACT_USUARIO_MOD_ID"].Value = (object) UsuarioActivoEditar;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                if (sqlDataReader.Read())
                {
                  mensaje.errNumber = sqlDataReader.GetInt32(0);
                  mensaje.message = sqlDataReader.GetString(1);
                }
                sqlDataReader.Close();
              }
              if (mensaje.errNumber == 0)
              {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "NEGOCIO.Set_Asignar_ValoresActivos";
                str1 = "Set_Asignar_ValoresActivos";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@MOD_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@ACT_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@CAR_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@CVA_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@VALOR", SqlDbType.VarChar, 250);
                sqlCommand.Parameters.Add("@RESPONSABLE_CREACION", SqlDbType.UniqueIdentifier);
                if (activos.SELECCIONADOS != null)
                {
                  string seleccionados = activos.SELECCIONADOS;
                  char[] chArray = new char[1]{ '_' };
                  foreach (string str3 in seleccionados.Split(chArray))
                  {
                    sqlCommand.Parameters["@MOD_ID"].Value = (object) activos.ACT_MOD_ID;
                    sqlCommand.Parameters["@ACT_ID"].Value = (object) activos.ACT_ID;
                    sqlCommand.Parameters["@CAR_ID"].Value = (object) Guid.Parse(str3.Split('|')[0]);
                    if (!str3.Split('|')[2].Equals(""))
                      sqlCommand.Parameters["@CVA_ID"].Value = (object) Guid.Parse(str3.Split('|')[2]);
                    else
                      sqlCommand.Parameters["@CVA_ID"].Value = (object) null;
                    sqlCommand.Parameters["@VALOR"].Value = (object) str3.Split('|')[1];
                    sqlCommand.Parameters["@RESPONSABLE_CREACION"].Value = (object) UsuarioActivoEditar;
                    sqlCommand.ExecuteNonQuery();
                  }
                }
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en : " + ex.Message + str1, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en : " + ex.Message + str1);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Activos(
      List<Guid> EliminarActivo,
      Guid UsuarioActivoEliminar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_Activos";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarActivo)
            {
              sqlCommand.Parameters["@ACT_ID"].Value = (object) guid;
              sqlCommand.Parameters["@ACT_USUARIO_DELETE_ID"].Value = (object) UsuarioActivoEliminar;
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Set_Eliminar_Activos" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Set_Eliminar_Activos" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Reasignar_Etiqueta(
      List<Activos> Reasignacion,
      Guid ETA_USUARIO_MOD_ID)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Asignar_ActivoEtiquetas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ETQ_BC", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@ETQ_CODE", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ETA_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            foreach (Activos activos in Reasignacion)
            {
              sqlCommand.Parameters["@ETQ_BC"].Value = (object) activos.ACT_ETQ_BC;
              sqlCommand.Parameters["@ETQ_CODE"].Value = (object) activos.ACT_ETQ_CODE;
              sqlCommand.Parameters["@ACT_ID"].Value = (object) activos.ACT_ID;
              sqlCommand.Parameters["@ETA_USUARIO_MOD_ID"].Value = (object) ETA_USUARIO_MOD_ID;
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
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        mensaje.errNumber = -1;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Set_Asignar_ActivoEtiquetas:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Set_Asignar_ActivoEtiquetas: " + ex.Message);
      }
      return mensaje;
    }

    public List<ActivosImagenes> Get_list_ActivosImagenes(
      Guid? AIM_ACT_ID,
      Guid? AIM_TRA_ID,
      Guid? AIM_ID)
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
            sqlCommand.CommandText = "NEGOCIO.Get_list_ActivosImagenes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AIM_ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@AIM_ACT_ID"].Value = (object) AIM_ACT_ID;
            sqlCommand.Parameters["@AIM_TRA_ID"].Value = (object) AIM_TRA_ID;
            sqlCommand.Parameters["@AIM_ID"].Value = (object) AIM_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                ActivosImagenes activosImagenes = new ActivosImagenes();
                activosImagenes.AIM_ID = sqlDataReader.GetGuid(0);
                activosImagenes.AIM_ACT_ID = new Guid?(sqlDataReader.GetGuid(1));
                try
                {
                  activosImagenes.AIM_TRA_ID = new Guid?(sqlDataReader.GetGuid(2));
                }
                catch
                {
                }
                activosImagenes.AIM_RUTA = sqlDataReader.GetString(3);
                try
                {
                  activosImagenes.AIM_OBSERVACION = sqlDataReader.GetString(4);
                }
                catch
                {
                  activosImagenes.AIM_OBSERVACION = "";
                }
                try
                {
                  activosImagenes.AIM_ACTIVE = sqlDataReader.GetBoolean(5);
                }
                catch
                {
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_list_ActivosImagenes: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        TransaccionDataBase.logger.Error(ex, "Error en Get_list_ActivosImagenes: " + ex.Message);
      }
      return activosImagenesList;
    }

        public List<Transaccion> Get_list_Activos_Transacciones(
     Guid? ACT_ID)
        {
            List<Transaccion> activosImagenesList = new List<Transaccion>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Activos_Transacciones";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@ACT_ID", SqlDbType.UniqueIdentifier).Value = ACT_ID;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Transaccion activos= new Transaccion();
                                activos.TRA_ID = sqlDataReader.GetGuid(0);
                                activos.TRA_TTR_ID = sqlDataReader.GetGuid(1);
                                activos.TTR_DESC = sqlDataReader.GetString(2);
                                try{
                                    activos.TRA_CLI_ID =sqlDataReader.GetGuid(3);
                                    activos.CLI_NOMBRE = sqlDataReader.GetString(4);
                                }
                                catch { }                              
                                try { activos.TRA_Fecha_Compra = sqlDataReader.GetDateTime (5).ToString("yyyy/MM/dd");} catch { }
                                try {activos.EST_ID = sqlDataReader.GetGuid(6);
                                    activos.EST_DESC = sqlDataReader.GetString(7);
                                }
                                catch {}
                                try { activos.CON_NUMERO_DOC = sqlDataReader.GetString(8); } catch { }
                                try { activos.TRA_DOCUMENTO_SAP = sqlDataReader.GetString(9); } catch { }
                                try { activos.TRA_OBSERVACIONES = sqlDataReader.GetString(10); } catch { }
                                try { activos.TTR_ES_ENTRADA = sqlDataReader.GetBoolean(11); } catch { }
                                try { activos.TTR_ES_SALIDA = sqlDataReader.GetBoolean(12); } catch { }
                                try { activos.PRO_NOMBRE = sqlDataReader.GetString(13); } catch { }
                                try { activos.Responsable = sqlDataReader.GetString(14); } catch { }
                                try { activos.TRA_Doc_Factura = sqlDataReader.GetString(15); } catch { }
                                try { activos.FechaIngreso = sqlDataReader.GetDateTime(16); } catch { }
                                try { activos.TRA_Transportador = sqlDataReader.GetString(17); } catch { }
                                try { activos.TRA_PRO_ID = sqlDataReader.GetGuid(18); } catch { }
                                try { activos.RES_ID = sqlDataReader.GetGuid(19); } catch { }
                                try { activos.TRA_AREA_ID = sqlDataReader.GetGuid(20);
                                    activos.ARE_DESC = sqlDataReader.GetString(21); } catch { }
                                try { activos.TRA_CON_ID = sqlDataReader.GetGuid(22); } catch { }
                                try { activos.TRA_costo  = sqlDataReader.GetDecimal(23); } catch { }
                                try { activos.TRA_Fecha_Salida = sqlDataReader.GetDateTime(24).ToString("yyyy/MM/dd") ; } catch { }
                                try { activos.TRA_MOT_ID = sqlDataReader.GetGuid(25); } catch { }
                                try { activos.AREA_ORIGEN = sqlDataReader.GetString(26); } catch { }
                                try { activos.AREA_DESTINO = sqlDataReader.GetString(27); } catch { }
                                try { activos.TRA_FECHA_CREATE_DATE = sqlDataReader.GetDateTime(28); } catch { }
                                try { activos.USU_LOGIN = sqlDataReader.GetString(29); } catch { }
                                try { activos.ID_ORIGEN = sqlDataReader.GetGuid(30); } catch { }
                                try { activos.ID_DESTINO = sqlDataReader.GetGuid(31); } catch { }
                                try { activos.TRA_Fecha_Retorno = sqlDataReader.GetDateTime(32).ToString("yyyy/MM/dd") ; } catch { }
                                try { activos.TRA_ETA_ID = sqlDataReader.GetGuid(33); } catch { }
                                try { activos.ETQ_BC = sqlDataReader.GetString(34); } catch { }
                                try { activos.ETQ_CODE= sqlDataReader.GetString(35); } catch { }
                                try { activos.ACT_DESC = sqlDataReader.GetString(36); } catch { }
                                try { activos.ACT_Serial = sqlDataReader.GetString(37); } catch { }
                                try { activos.RES_DOCUMENTO = sqlDataReader.GetString(38); } catch { }
                                try { activos.USU_CORREO = sqlDataReader.GetString(39); } catch { }
                                try { activos.TRA_Fecha_Estimada_Retorno_str = sqlDataReader.GetDateTime(40).ToString("yyyy/MM/dd"); } catch { }
                                try { activos.TAC_DESC = sqlDataReader.GetString(41); } catch { }
                                try { activos.MOD_DESC = sqlDataReader.GetString(42); } catch { }
                                    activosImagenesList.Add(activos);
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_list_Activos_Transacciones: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                TransaccionDataBase.logger.Error(ex, "Error en Get_list_Activos_Transacciones: " + ex.Message);
            }
            return activosImagenesList;
        }

        public List<ActivosCambio> Get_list_Activos_Cambios(Guid? ACT_ID)
        {
            List<ActivosCambio> activosImagenesList = new List<ActivosCambio>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Activos_Cambios";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@ACT_ID", SqlDbType.UniqueIdentifier).Value = ACT_ID;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                ActivosCambio activos = new ActivosCambio();
                                activos.LEA_FECHA_MOD = sqlDataReader.GetDateTime (0);
                                activos.LEA_FECHA_MOD_STR = sqlDataReader.GetDateTime(0).ToString ("yyyy/MM/dd");
                                activos.LEA_ACT_ID = sqlDataReader.GetGuid(1);
                                activos.LEA_CAR_DESC = sqlDataReader.GetString(2);
                                activos.LEA_AVA_VALOR_OLD = sqlDataReader.GetString(3);
                                activos.LEA_AVA_VALOR_NEW = sqlDataReader.GetString(4);
                                activos.LEA_USU_LOGIN = sqlDataReader.GetString(5);
                                activosImagenesList.Add(activos);
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_list_Activos_Cambios: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                TransaccionDataBase.logger.Error(ex, "Error en Get_list_Activos_Cambios: " + ex.Message);
            }
            return activosImagenesList;
        }
        public Mensaje Set_Crear_ActivosImagenes(
      List<ActivosImagenes> NuevaActivoImagen,
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_ActivosImagenes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AIM_ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_RUTA", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AIM_OBSERVACION", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AIM_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = -1;
            foreach (ActivosImagenes activosImagenes in NuevaActivoImagen)
            {
              sqlCommand.Parameters["@AIM_ACT_ID"].Value = (object) activosImagenes.AIM_ACT_ID;
              sqlCommand.Parameters["@AIM_TRA_ID"].Value = (object) activosImagenes.AIM_TRA_ID;
              sqlCommand.Parameters["@AIM_RUTA"].Value = (object) activosImagenes.AIM_RUTA;
              sqlCommand.Parameters["@AIM_OBSERVACION"].Value = (object) activosImagenes.AIM_OBSERVACION;
              sqlCommand.Parameters["@AIM_USUARIO_CREATE_ID"].Value = (object) UsuarioActivoImgCrear;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                if (sqlDataReader.Read())
                  activosImagenes.AIM_ID = sqlDataReader.GetGuid(0);
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Set_Crear_ActivosImagenes: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Set_Crear_ActivosImagenes: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_ActivosImagenes(
      List<ActivosImagenes> EditarActivoimagen,
      Guid UsuarioActivoImgEditar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_ActivosImagenes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AIM_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_RUTA", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AIM_OBSERVACION", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AIM_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (ActivosImagenes activosImagenes in EditarActivoimagen)
            {
              sqlCommand.Parameters["@AIM_ID"].Value = (object) activosImagenes.AIM_ID;
              sqlCommand.Parameters["@AIM_ACT_ID"].Value = (object) activosImagenes.AIM_ACT_ID;
              sqlCommand.Parameters["@AIM_TRA_ID"].Value = (object) activosImagenes.AIM_TRA_ID;
              sqlCommand.Parameters["@AIM_RUTA"].Value = (object) activosImagenes.AIM_RUTA;
              sqlCommand.Parameters["@AIM_OBSERVACION"].Value = (object) activosImagenes.AIM_OBSERVACION;
              sqlCommand.Parameters["@AIM_USUARIO_MOD_ID"].Value = (object) UsuarioActivoImgEditar;
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Set_Editar_ActivosImagenes: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Set_Editar_ActivosImagenes: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_ActivosImagenes(
      List<Guid> EliminarActivoImg,
      Guid UsuarioActivoImgEliminar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_ActivosImagenes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@AIM_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@AIM_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarActivoImg)
            {
              sqlCommand.Parameters["@AIM_ID"].Value = (object) guid;
              sqlCommand.Parameters["@AIM_USUARIO_DELETE_ID"].Value = (object) UsuarioActivoImgEliminar;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                if (sqlDataReader.Read())
                {
                  mensaje.errNumber = sqlDataReader.GetInt32(0);
                  mensaje.message = sqlDataReader.GetString(1);
                try { mensaje.data = (object)sqlDataReader.GetString(2); }
                catch { }
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Set_Eliminar_ActivosImagenes: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Set_Eliminar_ActivosImagenes: " + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<Contratos> Get_list_Contratos(
      Guid? CLI_ID,
      Guid? EST_ID,
      DateTime? CON_FECHA_SUSCRIPCION,
      DateTime? CON_FECHA_INICIO,
      DateTime? CON_FECHA_FINAL,
      string CON_NUMERO_DOC,
      string CON_DOCUMENTO_SAP,
      Guid? CON_ID)
    {
      List<Contratos> source = new List<Contratos>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Contratos";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CON_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@EST_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CON_FECHA_SUSCRIPCION", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@CON_FECHA_INICIO", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@CON_FECHA_FINAL", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@CON_NUMERO_DOC", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@CON_DOCUMENTO_SAP", SqlDbType.VarChar);
            sqlCommand.Parameters["@CON_ID"].Value = (object) CON_ID;
            sqlCommand.Parameters["@CLI_ID"].Value = (object) CLI_ID;
            sqlCommand.Parameters["@EST_ID"].Value = (object) EST_ID;
            sqlCommand.Parameters["@CON_FECHA_SUSCRIPCION"].Value = (object) CON_FECHA_SUSCRIPCION;
            sqlCommand.Parameters["@CON_FECHA_INICIO"].Value = (object) CON_FECHA_INICIO;
            sqlCommand.Parameters["@CON_FECHA_FINAL"].Value = (object) CON_FECHA_FINAL;
            sqlCommand.Parameters["@CON_NUMERO_DOC"].Value = (object) CON_NUMERO_DOC;
            sqlCommand.Parameters["@CON_DOCUMENTO_SAP"].Value = (object) CON_DOCUMENTO_SAP;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Contratos contratos = new Contratos();
                contratos.CON_ID = sqlDataReader.GetGuid(0);
                contratos.CON_CLI_ID = sqlDataReader.GetGuid(1);
                contratos.CLI_IDENTIFICACION = sqlDataReader.GetString(2);
                contratos.CLI_CODIGO_SAP = sqlDataReader.GetString(3);
                contratos.CLI_NOMBRE = sqlDataReader.GetString(4);
                contratos.CON_FECHA_SUSCRIPCION = sqlDataReader.GetDateTime(5).ToString("yyyy/MM/dd");
                contratos.CON_FECHA_INICIO = sqlDataReader.GetDateTime(6).ToString("yyyy/MM/dd");
                contratos.CON_FECHA_FINAL = sqlDataReader.GetDateTime(7).ToString("yyyy/MM/dd");
                try
                {
                  contratos.CON_OBSERVACION = sqlDataReader.GetString(8);
                }
                catch
                {
                }
                contratos.CON_EST_ID = sqlDataReader.GetGuid(9);
                contratos.CON_EST_NOMBRE = sqlDataReader.GetString(10);
                try
                {
                  contratos.CON_DOCUMENTO_SAP = sqlDataReader.GetString(11);
                }
                catch
                {
                }
                try
                {
                  contratos.CON_NUMERO_DOC = sqlDataReader.GetString(12);
                }
                catch
                {
                }
                source.Add(contratos);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_list_Contratos: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        TransaccionDataBase.logger.Error(ex, "Error en Get_list_Contratos: " + ex.Message);
      }
      return (IEnumerable<Contratos>) source.OrderBy<Contratos, string>((Func<Contratos, string>) (t => t.CLI_NOMBRE)).ToList<Contratos>();
    }

    public Mensaje Set_Crear_Contratos(
      List<Contratos> NuevoContrato,
      Guid UsuarioContratoCrear)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Contratos";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CON_CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CON_FECHA_SUSCRIPCION", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@CON_FECHA_INICIO", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@CON_FECHA_FINAL", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@CON_OBSERVACION", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@CON_EST_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CON_DOCUMENTO_SAP", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@CON_NUMERO_DOC", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@CON_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Contratos contratos in NuevoContrato)
            {
              sqlCommand.Parameters["@CON_CLI_ID"].Value = (object) contratos.CON_CLI_ID;
              sqlCommand.Parameters["@CON_FECHA_SUSCRIPCION"].Value = (object) this.helper.Fecha(contratos.CON_FECHA_SUSCRIPCION);
              sqlCommand.Parameters["@CON_FECHA_INICIO"].Value = (object) this.helper.Fecha(contratos.CON_FECHA_INICIO);
              sqlCommand.Parameters["@CON_FECHA_FINAL"].Value = (object) this.helper.Fecha(contratos.CON_FECHA_FINAL);
              sqlCommand.Parameters["@CON_OBSERVACION"].Value = (object) contratos.CON_OBSERVACION;
              sqlCommand.Parameters["@CON_EST_ID"].Value = (object) contratos.CON_EST_ID;
              sqlCommand.Parameters["@CON_DOCUMENTO_SAP"].Value = (object) contratos.CON_DOCUMENTO_SAP;
              sqlCommand.Parameters["@CON_NUMERO_DOC"].Value = (object) contratos.CON_NUMERO_DOC;
              sqlCommand.Parameters["@CON_USUARIO_CREATE_ID"].Value = (object) UsuarioContratoCrear;
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Set_Crear_Contratos: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Set_Crear_Contratos: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Contratos(
      List<Contratos> EditarContrato,
      Guid UsuarioContratoEditar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Contratos";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CON_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CON_CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CON_FECHA_SUSCRIPCION", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@CON_FECHA_INICIO", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@CON_FECHA_FINAL", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@CON_OBSERVACION", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@CON_EST_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CON_DOCUMENTO_SAP", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@CON_NUMERO_DOC", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@CON_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Contratos contratos in EditarContrato)
            {
              sqlCommand.Parameters["@CON_ID"].Value = (object) contratos.CON_ID;
              sqlCommand.Parameters["@CON_CLI_ID"].Value = (object) contratos.CON_CLI_ID;
              sqlCommand.Parameters["@CON_FECHA_SUSCRIPCION"].Value = (object) this.helper.Fecha(contratos.CON_FECHA_SUSCRIPCION);
              sqlCommand.Parameters["@CON_FECHA_INICIO"].Value = (object) this.helper.Fecha(contratos.CON_FECHA_INICIO);
              sqlCommand.Parameters["@CON_FECHA_FINAL"].Value = (object) this.helper.Fecha(contratos.CON_FECHA_FINAL);
              sqlCommand.Parameters["@CON_OBSERVACION"].Value = (object) contratos.CON_OBSERVACION;
              sqlCommand.Parameters["@CON_EST_ID"].Value = (object) contratos.CON_EST_ID;
              sqlCommand.Parameters["@CON_DOCUMENTO_SAP"].Value = (object) contratos.CON_DOCUMENTO_SAP;
              sqlCommand.Parameters["@CON_NUMERO_DOC"].Value = (object) contratos.CON_NUMERO_DOC;
              sqlCommand.Parameters["@CON_USUARIO_MOD_ID"].Value = (object) UsuarioContratoEditar;
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Set_Editar_Contratos: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Set_Editar_Contratos: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Contratos(
      List<Guid> EliminarContrato,
      Guid UsuarioContratoEliminar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_Contratos";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CON_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CON_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarContrato)
            {
              sqlCommand.Parameters["@CON_ID"].Value = (object) guid;
              sqlCommand.Parameters["@CON_USUARIO_DELETE_ID"].Value = (object) UsuarioContratoEliminar;
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Set_Eliminar_Contratos:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Set_Eliminar_Contratos:" + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<Estados> Get_list_EstadosContratos(Guid? EST_ID)
    {
      Mensaje mensaje = new Mensaje();
      List<Estados> estadosList = new List<Estados>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_EstadosContratos";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@EST_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@EST_ID"].Value = (object) EST_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                estadosList.Add(new Estados()
                {
                  EST_ID = sqlDataReader.GetGuid(0),
                  EST_DESC = sqlDataReader.GetString(1),
                  EST_ACTIVE = sqlDataReader.GetBoolean(2)
                });
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_list_EstadosContratos:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Get_list_EstadosContratos:" + ex.Message);
      }
      return (IEnumerable<Estados>) estadosList;
    }

    public IEnumerable<TransaccionDetalle> Get_list_DetalleTransacciones(
      Guid? TRA_ID)
    {
      List<TransaccionDetalle> transaccionDetalleList = new List<TransaccionDetalle>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_DetalleTransacciones";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TRA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@TRA_ID"].Value = (object) TRA_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                TransaccionDetalle transaccionDetalle1 = new TransaccionDetalle();
                try
                {
                  transaccionDetalle1.DTR_ID = sqlDataReader.GetGuid(0);
                }
                catch
                {
                }
                transaccionDetalle1.ACT_ID = sqlDataReader.GetGuid(1);
                transaccionDetalle1.ACT_DESC = sqlDataReader.GetString(2);
                transaccionDetalle1.TRA_ID = new Guid?(sqlDataReader.GetGuid(3));
                transaccionDetalle1.TAC_DESC = sqlDataReader.GetString(4);
                transaccionDetalle1.MOD_DESC = sqlDataReader.GetString(5);
                transaccionDetalle1.ETQ_BC = sqlDataReader.GetString(6);
                transaccionDetalle1.ETQ_CODE = sqlDataReader.GetString(7);
                transaccionDetalle1.ACT_Serial = sqlDataReader.GetString(8);
                transaccionDetalle1.EST_DESC = sqlDataReader.GetString(9);
                transaccionDetalle1.ACT_TAC_ID = new Guid?(sqlDataReader.GetGuid(10));
                transaccionDetalle1.ACT_MOD_ID = new Guid?(sqlDataReader.GetGuid(11));
                transaccionDetalle1.ACT_EST_ID = new Guid?(sqlDataReader.GetGuid(12));
                transaccionDetalle1.ACT_ACTIVE = sqlDataReader.GetBoolean(13);
                transaccionDetalle1.ACT_ETQ_EPC = sqlDataReader.GetString(14);
                transaccionDetalle1.ACT_ETQ_TID = sqlDataReader.GetString(15);
                transaccionDetalle1.DTR_RECIBIDO = sqlDataReader.GetInt32(16) != 0;
                transaccionDetalle1.DTR_x_RECIBIR = sqlDataReader.GetInt32(17) != 0;
                TransaccionDetalle transaccionDetalle2 = transaccionDetalle1;
                transaccionDetalle2.ACT_TAC_DESC = transaccionDetalle2.TAC_DESC;
                TransaccionDetalle transaccionDetalle3 = transaccionDetalle1;
                transaccionDetalle3.ACT_MOD_DESC = transaccionDetalle3.MOD_DESC;
                TransaccionDetalle transaccionDetalle4 = transaccionDetalle1;
                transaccionDetalle4.ACT_EST_DESC = transaccionDetalle4.EST_DESC;
                TransaccionDetalle transaccionDetalle5 = transaccionDetalle1;
                transaccionDetalle5.ACT_ETQ_BC = transaccionDetalle5.ETQ_BC;
                TransaccionDetalle transaccionDetalle6 = transaccionDetalle1;
                transaccionDetalle6.ACT_ETQ_CODE = transaccionDetalle6.ETQ_CODE;
                transaccionDetalleList.Add(transaccionDetalle1);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_list_DetalleTransacciones: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        TransaccionDataBase.logger.Error(ex, "Error en Get_list_DetalleTransacciones: " + ex.Message);
      }
      return (IEnumerable<TransaccionDetalle>) transaccionDetalleList;
    }

    public IEnumerable<Modelo> Get_list_ModeloxTipoActivo(Guid? MOD_TAC_ID)
    {
      Mensaje mensaje = new Mensaje();
      List<Modelo> modeloList = new List<Modelo>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_ModeloXTipoactivo";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@MOD_TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@MOD_TAC_ID"].Value = (object) MOD_TAC_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                modeloList.Add(new Modelo()
                {
                  MOD_ID = sqlDataReader.GetGuid(0),
                  MOD_DESC = sqlDataReader.GetString(1),
                  MOD_TAC_ID = sqlDataReader.GetGuid(2),
                  MOD_TAC_DESC = sqlDataReader.GetString(3)
                });
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
        TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_list_ModeloXTipoactivo: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        TransaccionDataBase.logger.Error(ex, "Error en Get_list_ModeloXTipoactivo: " + ex.Message);
      }
      return (IEnumerable<Modelo>) modeloList;
    }

        public IEnumerable<Transaccion> Get_list_TransaccionesContrato(   
     Guid? CON_ID
    )
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
                        sqlCommand.CommandText = "NEGOCIO.Get_list_TransaccionesV2";
                        sqlCommand.CommandType = CommandType.StoredProcedure;                       
                        sqlCommand.Parameters.Add("@CON_ID", SqlDbType.UniqueIdentifier).Value = (object)CON_ID;
                       
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
                                }
                                try
                                {
                                    transaccion1.TRA_Fecha_Estimada_Retorno_str = sqlDataReader.GetDateTime(40).ToString("yyyy/MM/dd");
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
                TransaccionDataBase.logger.Error<SqlException>("Sql error en Get_list_TransaccionesV2: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                TransaccionDataBase.logger.Error(ex, "Error en Get_list_TransaccioneV2: " + ex.Message);
            }
            return (IEnumerable<Transaccion>)source.OrderByDescending<Transaccion, DateTime>((Func<Transaccion, DateTime>)(t => t.TRA_FECHA_CREATE_DATE));
        }

    }
}
