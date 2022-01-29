using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApiKaeser.Interfaces;
using WebApiKaeser.Models;

namespace WebApiKaeser.Factory
{
    public class ConfiguracionDataBase: IConfiguracion
    {
        private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
        private Logger logger = LogManager.GetCurrentClassLogger();
        public IEnumerable<Empresa> Get_list_EMPRESA()
        {
            List<Empresa> empresaList = new List<Empresa>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "Base.Get_list_EMPRESA";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                                {  Empresa empresa=new Empresa();
                                try { empresa.EMP_DESC = sqlDataReader.GetString(0); } catch { }
                                try { empresa.EMP_DIRECCION = sqlDataReader.GetString(1); }catch { }
                                try { empresa.EMP_TELEFONO = sqlDataReader.GetString(2); } catch { }
                                try { empresa.EMP_LOGO = sqlDataReader.GetString(3); } catch { }
                                try { empresa.EMP_SMTP_REM  = sqlDataReader.GetString(4); } catch { }
                                try { empresa.EMP_SMTP_NOM_REM = sqlDataReader.GetString(5); } catch { }
                                try { empresa.EMP_SMTP_SERVER = sqlDataReader.GetString(6); } catch { }
                                try { empresa.EMP_SMTP_ENCRYPT = sqlDataReader.GetInt32(7); } catch { }
                                try { empresa.EMP_SMTP_PORT = sqlDataReader.GetInt32(8); } catch { }
                                try { empresa.EMP_SMTP_USER = sqlDataReader.GetString(9); } catch { }
                                try { empresa.EMP_SMTP_PASS = sqlDataReader.GetString(10); } catch { }
                                try { empresa.EMP_NOTIFY_AUTH = sqlDataReader.GetBoolean(11)? "on":"off"; } catch { }
                                try { empresa.EMP_NOTIFY_ASIGN = sqlDataReader.GetBoolean(12) ? "on" : "off"; } catch { }
                                try { empresa.EMP_NOTIFY_INFPREST = sqlDataReader.GetBoolean(13) ? "on" : "off"; } catch { }
                                try { empresa.EMP_CORREOS = sqlDataReader.GetString(14); } catch { }
                                empresaList.Add(empresa);
                                }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                this.logger.Error<SqlException>("Sql error en Get_list_EMPRESA: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error en Get_list_EMPRESA: " + ex.Message);
            }
            return (IEnumerable<Empresa>)empresaList;
        }
        public Mensaje Set_Editar_EMPRESA( List<Empresa> EMPRESA, Guid? Usuario)
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
                        sqlCommand.CommandText = "BASE.Set_Editar_EMPRESA";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@EMP_DESC", SqlDbType.NVarChar);
                        sqlCommand.Parameters.Add("@EMP_DIRECCION", SqlDbType.VarChar);
                        sqlCommand.Parameters.Add("@EMP_TELEFONO", SqlDbType.VarChar);
                        sqlCommand.Parameters.Add("@EMP_LOGO", SqlDbType.VarChar,50);
                        sqlCommand.Parameters.Add("@EMP_SMTP_REM", SqlDbType.VarChar,100);
                        sqlCommand.Parameters.Add("@EMP_SMTP_NOM_REM", SqlDbType.VarChar, 100);
                        sqlCommand.Parameters.Add("@EMP_SMTP_SERVER", SqlDbType.VarChar, 100);
                        sqlCommand.Parameters.Add("@EMP_SMTP_ENCRYPT", SqlDbType.Int);
                        sqlCommand.Parameters.Add("@EMP_SMTP_PORT", SqlDbType.Int);
                        sqlCommand.Parameters.Add("@EMP_SMTP_USER", SqlDbType.VarChar, 100);
                        sqlCommand.Parameters.Add("@EMP_SMTP_PASS", SqlDbType.VarChar, 100);
                        sqlCommand.Parameters.Add("@EMP_NOTIFY_AUTH", SqlDbType.Bit);
                        sqlCommand.Parameters.Add("@EMP_NOTIFY_ASIGN", SqlDbType.Bit);
                        sqlCommand.Parameters.Add("@EMP_NOTIFY_INFPREST", SqlDbType.Bit);
                        sqlCommand.Parameters.Add("@EMP_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@EMP_CORREOS", SqlDbType.VarChar);
                        mensaje.errNumber = 0;
                        mensaje.message = str;
                        foreach (Empresa caracteristica in EMPRESA)
                        {
                            sqlCommand.Parameters["@EMP_DESC"].Value = (object)caracteristica.EMP_DESC;
                            sqlCommand.Parameters["@EMP_DIRECCION"].Value = (object)caracteristica.EMP_DIRECCION;
                            sqlCommand.Parameters["@EMP_TELEFONO"].Value = (object)caracteristica.EMP_TELEFONO;
                            sqlCommand.Parameters["@EMP_LOGO"].Value = (object)caracteristica.EMP_LOGO;
                            sqlCommand.Parameters["@EMP_SMTP_REM"].Value = (object)caracteristica.EMP_SMTP_REM;
                            sqlCommand.Parameters["@EMP_SMTP_NOM_REM"].Value = (object)caracteristica.EMP_SMTP_NOM_REM;
                            sqlCommand.Parameters["@EMP_SMTP_SERVER"].Value = (object)caracteristica.EMP_SMTP_SERVER;
                            sqlCommand.Parameters["@EMP_SMTP_ENCRYPT"].Value = (object)caracteristica.EMP_SMTP_ENCRYPT;
                            sqlCommand.Parameters["@EMP_SMTP_PORT"].Value = (object)caracteristica.EMP_SMTP_PORT;
                            sqlCommand.Parameters["@EMP_SMTP_USER"].Value = (object)caracteristica.EMP_SMTP_USER;
                            sqlCommand.Parameters["@EMP_SMTP_PASS"].Value = (object)caracteristica.EMP_SMTP_PASS;
                            if (caracteristica.EMP_NOTIFY_AUTH != null)
                                sqlCommand.Parameters["@EMP_NOTIFY_AUTH"].Value = caracteristica.EMP_NOTIFY_AUTH.Equals("on") ? true : false;
                            else
                                sqlCommand.Parameters["@EMP_NOTIFY_AUTH"].Value = false;
                            if (caracteristica.EMP_NOTIFY_ASIGN != null)
                                sqlCommand.Parameters["@EMP_NOTIFY_ASIGN"].Value = caracteristica.EMP_NOTIFY_ASIGN.Equals("on") ? true : false;
                            else
                                sqlCommand.Parameters["@EMP_NOTIFY_ASIGN"].Value = false;
                            if (caracteristica.EMP_NOTIFY_INFPREST != null)
                                sqlCommand.Parameters["@EMP_NOTIFY_INFPREST"].Value = caracteristica.EMP_NOTIFY_INFPREST.Equals("on") ? true : false;
                            else
                                sqlCommand.Parameters["@EMP_NOTIFY_INFPREST"].Value = false;
                            sqlCommand.Parameters["@EMP_USUARIO_MOD_ID"].Value = (object)Usuario;
                            sqlCommand.Parameters["@EMP_CORREOS"].Value = (object)caracteristica.EMP_CORREOS;
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
                this.logger.Error<SqlException>("Sql error en Set_Editar_EMPRESA: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                mensaje.errNumber = -2;
                mensaje.message = ex.Message;
                this.logger.Error(ex, "Error en Set_Editar_EMPRESA: " + ex.Message);
            }
            return mensaje;
        }
        public IEnumerable<Transaccion> Get_list_transacciones_SinNotificar()
        {
            List<Transaccion> empresaList = new List<Transaccion>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "Negocio.Get_list_transacciones_SinNotificar";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Transaccion empresa = new Transaccion();
                                try { empresa.TRA_ID = sqlDataReader.GetGuid(0); } catch { }
                                try { empresa.TTR_DESC = sqlDataReader.GetString(1); } catch { }
                                try { empresa.TRA_DOCUMENTO_SAP = sqlDataReader.GetString(2); } catch { }
                                try { empresa.TRA_Fecha_Retorno = sqlDataReader.GetString(3); } catch { }
                                try { empresa.USU_LOGIN = sqlDataReader.GetString(4); } catch { }
                                try { empresa.AREA_ORIGEN = sqlDataReader.GetString(5); } catch { }
                                try { empresa.AREA_DESTINO = sqlDataReader.GetString(6); } catch { }                                
                                empresaList.Add(empresa);
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                this.logger.Error<SqlException>("Sql error en Get_list_transacciones_SinNotificar: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error en Get_list_transacciones_SinNotificar: " + ex.Message);
            }
            return (IEnumerable<Transaccion>)empresaList;
        }
        public IEnumerable<Transaccion> Get_list_Asignaciones_SinNotificar()
        {
            List<Transaccion> empresaList = new List<Transaccion>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Asignaciones_SinNotificar";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Transaccion empresa = new Transaccion();
                                try { empresa.RES_ID = sqlDataReader.GetGuid(0); } catch { }
                                try { empresa.TTR_DESC = sqlDataReader.GetString(1); } catch { }
                                try { empresa.TRA_DOCUMENTO_SAP = sqlDataReader.GetString(2); } catch { }
                                try { empresa.TRA_Fecha_Retorno = sqlDataReader.GetString(3); } catch { }
                                try { empresa.USU_LOGIN = sqlDataReader.GetString(4); } catch { }
                                try { empresa.AREA_ORIGEN = sqlDataReader.GetString(5); } catch { }
                                try { empresa.AREA_DESTINO = sqlDataReader.GetString(6); } catch { }
                                try { empresa.Responsable = sqlDataReader.GetString(7); } catch { }
                                try { empresa.USU_CORREO  = sqlDataReader.GetString(8); } catch { }
                                try { empresa.ACT_DESC = sqlDataReader.GetString(9); } catch { }
                                try { empresa.ACT_Serial = sqlDataReader.GetString(10); } catch { }
                                try { empresa.ETQ_BC  = sqlDataReader.GetString(11); } catch { }
                                empresaList.Add(empresa);
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                this.logger.Error<SqlException>("Sql error en Get_list_Asignaciones_SinNotificar: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error en Get_list_Asignaciones_SinNotificar: " + ex.Message);
            }
            return (IEnumerable<Transaccion>)empresaList;
        }
        public IEnumerable<Usuarios> Get_list_emai_autorizados( Guid TRA_ID)
        {
            List<Usuarios> empresaList = new List<Usuarios>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_emai_autorizados";
                        sqlCommand.Parameters.Add("@TRA_ID", SqlDbType.UniqueIdentifier).Value = TRA_ID;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Usuarios empresa = new Usuarios();
                                try { empresa.USU_NOMBRE = sqlDataReader.GetString(0); } catch { }
                                try { empresa.USU_CORREO = sqlDataReader.GetString(1); } catch { }
                                empresaList.Add(empresa);
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                this.logger.Error<SqlException>("Sql error en Get_list_emai_autorizados: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error en Get_list_emai_autorizados: " + ex.Message);
            }
            return (IEnumerable<Usuarios>)empresaList;
        }
        public IEnumerable<Transaccion> Get_list_Informe_Prestamos(Guid ? TRA_ID,Guid? RES_ID,int Vencidos,DateTime? Fecha_Inicial,DateTime? Fecha_Final)
        {
            List<Transaccion> empresaList = new List<Transaccion>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Informe_Prestamos";
                        sqlCommand.Parameters.Add("@TAC_ID", SqlDbType.UniqueIdentifier).Value = TRA_ID;
                        sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier).Value = RES_ID;
                        sqlCommand.Parameters.Add("@Vencidos", SqlDbType.Int).Value = Vencidos;
                        sqlCommand.Parameters.Add("@Fecha_Inicial", SqlDbType.DateTime).Value = Fecha_Inicial;
                        sqlCommand.Parameters.Add("@Fecha_Final", SqlDbType.DateTime).Value = Fecha_Final;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Transaccion empresa = new Transaccion();
                                try { empresa.ACT_DESC = sqlDataReader.GetString(0); } catch { }
                                try { empresa.TAC_DESC = sqlDataReader.GetString(1); } catch { }
                                try { empresa.MOD_DESC = sqlDataReader.GetString(2); } catch { }
                                try { empresa.ACT_Serial = sqlDataReader.GetString(3); } catch { }
                                try { empresa.ETQ_CODE = sqlDataReader.GetString(4); } catch { }
                                try { empresa.TRA_FECHA_CREATE = sqlDataReader.GetDateTime(5).ToString("yyyy/MM/dd") ; } catch { }
                                try { empresa.Responsable = sqlDataReader.GetString(6); } catch { }
                                try { empresa.TRA_Fecha_Retorno = sqlDataReader.GetDateTime(7).ToString("yyyy/MM/dd"); } catch { }
                                empresaList.Add(empresa);
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                this.logger.Error<SqlException>("Sql error en Get_list_Informe_Prestamos: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error en Get_list_Informe_Prestamos: " + ex.Message);
            }
            return (IEnumerable<Transaccion>)empresaList;
        }
        public IEnumerable<Mensaje> Set_Transaccion_notificada(Guid TRA_ID)
        {
            List<Mensaje> empresaList = new List<Mensaje>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Set_Transaccion_notificada";
                        sqlCommand.Parameters.Add("@TRA_ID", SqlDbType.UniqueIdentifier).Value = TRA_ID;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Mensaje empresa = new Mensaje();
                                try { empresa.errNumber = sqlDataReader.GetInt32(0); } catch { }
                                try { empresa.message = sqlDataReader.GetString(1); } catch { }
                                empresaList.Add(empresa);
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                this.logger.Error<SqlException>("Sql error en Set_Transaccion_notificada: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error en Set_Transaccion_notificada: " + ex.Message);
            }
            return (IEnumerable<Mensaje>)empresaList;
        }
        public IEnumerable<Mensaje> Set_Asignaciones_notificada(Guid RES_ID)
        {
            List<Mensaje> empresaList = new List<Mensaje>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Set_Asignaciones_notificada";
                        sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier).Value = RES_ID;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Mensaje empresa = new Mensaje();
                                try { empresa.errNumber = sqlDataReader.GetInt32(0); } catch { }
                                try { empresa.message = sqlDataReader.GetString(1); } catch { }
                                empresaList.Add(empresa);
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                this.logger.Error<SqlException>("Sql error en Set_Asignaciones_notificada: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error en Set_Asignaciones_notificada: " + ex.Message);
            }
            return (IEnumerable<Mensaje>)empresaList;
        }

    }
}
