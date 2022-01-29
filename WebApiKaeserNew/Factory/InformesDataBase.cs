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
    public class InformesDataBase : Iinformes
    {
        private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public List<Activos> Get_list_Informe_activos(Guid? ARE_ID, Guid? TAC_ID, Guid? EDA_ID,Guid? RES_ID, bool? ES_RETORNABLE, DateTime? Fecha_Inicial, DateTime? Fecha_Final)
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
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Informe_activos";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@ARE_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@TAC_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@EDA_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@ES_RETORNABLE", SqlDbType.Bit);
                        sqlCommand.Parameters.Add("@FECHA_INI", SqlDbType.DateTime);
                        sqlCommand.Parameters.Add("@FECHA_FIN", SqlDbType.DateTime);
                        sqlCommand.Parameters["@ARE_ID"].Value = (object)ARE_ID;
                        sqlCommand.Parameters["@TAC_ID"].Value = (object)TAC_ID;
                        sqlCommand.Parameters["@EDA_ID"].Value = (object)EDA_ID;
                        sqlCommand.Parameters["@RES_ID"].Value = (object)RES_ID;
                        sqlCommand.Parameters["@ES_RETORNABLE"].Value = (object)ES_RETORNABLE;
                        sqlCommand.Parameters["@FECHA_INI"].Value = (object)Fecha_Inicial;
                        sqlCommand.Parameters["@FECHA_FIN"].Value = (object)Fecha_Final;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Activos activos = new Activos();
                                activos.ACT_ID = sqlDataReader.GetGuid(0);
                                try { activos.ARE_DESC = sqlDataReader.GetString(1); }
                                catch { }
                                activos.ACT_TAC_DESC = sqlDataReader.GetString(2);
                                activos.ACT_MOD_DESC = sqlDataReader.GetString(3);
                                activos.ACT_SERIAL = sqlDataReader.GetString(4);
                                activos.ACT_DESC = sqlDataReader.GetString(5);
                                try { activos.ACT_ETQ_CODE = sqlDataReader.GetString(6); }
                                catch { }
                                activos.ACT_EST_DESC = sqlDataReader.GetString(7);
                                activos.SELECCIONADOS = sqlDataReader.GetString(8); //disponibilidad
                                try { activos.RES_NOMBRE_APELLIDO = sqlDataReader.GetString(9); }
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_Informe_activos: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_Informe_activos: " + ex.Message);
            }
            return source.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<Transaccion> Get_list_Informe_Prestamos(Guid? TAC_ID,Guid? RES_ID,int Vencidos,DateTime? Fecha_Inicial,DateTime? Fecha_Final)
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
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Informe_Prestamos";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@TAC_ID", SqlDbType.UniqueIdentifier).Value = TAC_ID;
                        sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier).Value = RES_ID;
                        sqlCommand.Parameters.Add("@Vencidos", SqlDbType.Int).Value = Vencidos;
                        sqlCommand.Parameters.Add("@Fecha_Inicial", SqlDbType.DateTime).Value = Fecha_Inicial;
                        sqlCommand.Parameters.Add("@Fecha_Final", SqlDbType.DateTime).Value = Fecha_Final;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Transaccion activos = new Transaccion();
                                try { activos.ACT_DESC = sqlDataReader.GetString(0); } catch { }
                                try { activos.TAC_DESC = sqlDataReader.GetString(1); } catch { }
                                try { activos.MOD_DESC = sqlDataReader.GetString(2); } catch { }
                                try { activos.ACT_Serial = sqlDataReader.GetString(3); } catch { }
                                try { activos.ETQ_CODE = sqlDataReader.GetString(4); } catch { }
                                try { activos.TRA_FECHA_CREATE_DATE  = sqlDataReader.GetDateTime(5);
                                    activos.TRA_FECHA_CREATE = sqlDataReader.GetDateTime(5).ToString ("yyyy/MM/dd");
                                } catch { }
                                try { activos.Responsable = sqlDataReader.GetString(6); } catch { }
                                try {
                                    activos.TRA_Fecha_Retorno_date = sqlDataReader.GetDateTime(7);
                                    activos.TRA_Fecha_Retorno = sqlDataReader.GetDateTime(7).ToString("yyyy/MM/dd"); } catch { }                               
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_Informe_Prestamos: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_Informe_Prestamos: " + ex.Message);
            }
            return activosImagenesList;
        }
        public List<Activos> Get_list_Informes_etiquetas(bool? Disponible)
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
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Informes_etiquetas";
                        sqlCommand.CommandType = CommandType.StoredProcedure;                      
                        sqlCommand.Parameters.Add("@Disponible", SqlDbType.Bit);                       
                        sqlCommand.Parameters["@Disponible"].Value = (object)Disponible;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Activos activos = new Activos();
                                try { activos.ACT_ETQ_CODE = sqlDataReader.GetString(0); }
                                catch { }
                                try { activos.ACT_ETQ_BC = sqlDataReader.GetString(1); }
                                catch { }
                                try { activos.ACT_ETQ_EPC = sqlDataReader.GetString(2); }
                                catch { }
                                try { activos.ACT_DESC = sqlDataReader.GetString(3); }
                                catch { }
                                try
                                { activos.ACT_TAC_DESC = sqlDataReader.GetString(4);
                                }
                                catch { }
                                try
                                    { activos.ACT_MOD_DESC = sqlDataReader.GetString(5);
                                }
                                catch { }
                                try
                                        { activos.ACT_SERIAL = sqlDataReader.GetString(6); }
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_Informes_etiquetas: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_Informes_etiquetas: " + ex.Message);
            }
            return source.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<RentaFlota> Get_list_InformesPersonalizados_Renta()
        {
            List<RentaFlota> source = new List<RentaFlota>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_InformesPersonalizados_Renta";
                        sqlCommand.CommandType = CommandType.StoredProcedure;                     
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                RentaFlota activos = new RentaFlota();
                                try { activos.EMR = sqlDataReader.GetString(0); }
                                catch { }
                                try { activos.CodigoSAP = sqlDataReader.GetString(1); }
                                catch { }
                                try { activos.DescripcionActivo = sqlDataReader.GetString(2); }
                                catch { }
                                try { activos.Serial = sqlDataReader.GetString(3); }
                                catch { }
                                try { activos.POTENCIAHP = sqlDataReader.GetString(4); }
                                catch { }
                                try { activos.CFM = sqlDataReader.GetString(5); }
                                catch { }                                
                                try { activos.PRESIONPSI = sqlDataReader.GetString(6); }
                                catch { }
                                try { activos.ParteNumero = sqlDataReader.GetString(7); }
                                catch { }
                                try { activos.AnioFabricacion = sqlDataReader.GetString(8); }
                                catch { }
                                try { activos.SecadorIncluido = sqlDataReader.GetString(9); }
                                catch { }
                                try { activos.CanonMinimoRentaDiaria = sqlDataReader.GetString(10); }
                                catch { }
                                try { activos.CanonMinimoArrendamientoMensual = sqlDataReader.GetString(11); }
                                catch { }
                                try { activos.PrecioVenta = sqlDataReader.GetString(12); }
                                catch { }
                                try { activos.IVA = sqlDataReader.GetString(13); }
                                catch { }
                                try { activos.Ciudad = sqlDataReader.GetString(14); }
                                catch { }
                                try { activos.CLI_NOMBRE = sqlDataReader.GetString(15); }
                                catch { }
                                try { activos.CLI_IDENTIFICACION = sqlDataReader.GetString(16); }
                                catch { }
                                try { activos.Asesor = sqlDataReader.GetString(17); }
                                catch { }
                                try { activos.Condiciones = sqlDataReader.GetString(18); }
                                catch { }
                                try { activos.TRA_FECHA_AUTORIZA = sqlDataReader.GetString(19); }
                                catch { }
                                try { activos.TRA_Fecha_Estimada_Retorno = sqlDataReader.GetString(20); }
                                catch { }
                                try { activos.UltimoPeriodoFacturado = sqlDataReader.GetString(21); }
                                catch { }
                                try { activos.TiempoEquipoSitio = sqlDataReader.GetString(22); }
                                catch { }
                                try { activos.EstadoContrato = sqlDataReader.GetString(23); }
                                catch { }
                                try { activos.EstadoOtroSi = sqlDataReader.GetString(24); }
                                catch { }
                                try { activos.INCREMENTOCANON = sqlDataReader.GetString(25); }
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_InformesPersonalizados_Renta: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_InformesPersonalizados_Renta: " + ex.Message);
            }
            return source;//.OrderBy<RentaFlota, string>((Func<RentaFlota, string>)(a => a.ACT_DESC)).OrderBy<RentaFlota, string>((Func<RentaFlota, string>)(t => t.ACT_TAC_DESC)).ToList<RentaFlota>();
        }
        public List<ServicioTecnicoInforme> Get_list_InformesPersonalizados_ServicioTecnico(Guid? STE_ID)
        {
            List<ServicioTecnicoInforme> source = new List<ServicioTecnicoInforme>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_InformesPersonalizados_ServicioTecnico";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@STE_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters["@STE_ID"].Value = (object)STE_ID;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                ServicioTecnicoInforme activos = new ServicioTecnicoInforme();
                                try { activos.NumeroOrdenTrabajo = sqlDataReader.GetString(0); } catch { }
                                try { activos.CodigoEtiqueta = sqlDataReader.GetString(1); }     catch { }
                                try { activos.DescripcionActivo = sqlDataReader.GetString(2); }  catch { }
                                try { activos.TipoActivo = sqlDataReader.GetString(3); }         catch { }
                                try { activos.Modelo = sqlDataReader.GetString(4); } catch { }
                                try { activos.Serial = sqlDataReader.GetString(5); } catch { }
                                try { activos.ClienteProveedor = sqlDataReader.GetString(6); } catch { }
                                try { activos.UsuarioSolicita = sqlDataReader.GetString(7); } catch { }
                                try { activos.UsuarioInicia = sqlDataReader.GetString(8); } catch { }//disponibilidad
                                try { activos.UsuarioFinaliza = sqlDataReader.GetString(9); } catch { }
                                try { activos.M2 = sqlDataReader.GetString(10); }         catch { }
                                try { activos.RepuestoUtilizado = sqlDataReader.GetString(11); }         catch { }
                                try { activos.TiempoInicioServicio = sqlDataReader.GetString(12);}         catch { }
                                try { activos.TiempoServicio = sqlDataReader.GetString(13);}         catch { }
                                try { activos.FechaSolicitud = sqlDataReader.GetDateTime(14).ToString("yyyy/MM/dd");}         catch { }
                                try { activos.FechaInicial = sqlDataReader.GetDateTime(15).ToString("yyyy/MM/dd");}         catch { } //disponibilidad
                                try { activos.FechaFinalizacion = sqlDataReader.GetDateTime(16).ToString("yyyy/MM/dd");}         catch { }
                                try { activos.TipoTrabajo = sqlDataReader.GetString(17);}         catch { }
                                try { activos.Estado = sqlDataReader.GetString(18);    }         catch { }                            
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_InformesPersonalizados_ServicioTecnico: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_InformesPersonalizados_ServicioTecnico: " + ex.Message);
            }
            return source;//.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<MetrosCuadrados> Get_list_InformesPersonalizados_MetrosCuadrados(DateTime? FechaInicio,DateTime? FechaFin,Guid? Area)
        {
            List<MetrosCuadrados> source = new List<MetrosCuadrados>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_InformesPersonalizados_MetrosCuadrados";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@FechaInicio", SqlDbType.DateTime ).Value =FechaInicio ;
                        sqlCommand.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
                        sqlCommand.Parameters.Add("@Area", SqlDbType.UniqueIdentifier).Value = Area;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                MetrosCuadrados activos = new MetrosCuadrados();
                                try { activos.Area = sqlDataReader.GetString(0); }
                                catch { }
                                try { activos.Fecha = sqlDataReader.GetDateTime(1).ToString ("yyyy/MM/dd"); }
                                catch { }
                                try { activos.Ingreso  = sqlDataReader.GetDouble(2); }
                                catch { }
                                try { activos.Proceso  = sqlDataReader.GetDouble(3); }
                                catch { }
                                activos.Salida  = sqlDataReader.GetDouble(4);
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_InformesPersonalizados_MetrosCuadrados: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_InformesPersonalizados_MetrosCuadrados: " + ex.Message);
            }
            return source;//.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<Garantias> Get_list_InformesPersonalizados_Garantias()
        {
            List<Garantias> source = new List<Garantias>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_InformesPersonalizados_Garantias";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                         using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Garantias activos = new Garantias();
                                try { activos.AreaActivo = sqlDataReader.GetString(0); }
                                catch { }
                                try { activos.DescripcionActivo = sqlDataReader.GetString(1); }
                                catch { }
                                try { activos.TipoActivo = sqlDataReader.GetString(2); }
                                catch { }
                                try { activos.Modelo = sqlDataReader.GetString(3); }
                                catch { }
                                try
                                { activos.ParteNumero = sqlDataReader.GetString(4); }
                                catch { }
                                try
                                { activos.Serial = sqlDataReader.GetString(5); }
                                catch { }
                                try
                                { activos.MaquinaPrimaPN = sqlDataReader.GetString(6); }
                                catch { }
                                try
                                { activos.MaquinaPrimaSerial = sqlDataReader.GetString(7); }
                                catch { }
                                try
                                { activos.NumeroFalla = sqlDataReader.GetString(8); }
                                catch { }
                                try
                                { activos.OS = sqlDataReader.GetString(9); }
                                catch { }
                                try
                                { activos.FechaIngreso = sqlDataReader.GetDateTime(10).ToString ("yyyy/MM/dd"); }
                                catch { }
                                try
                                { activos.CodigoEtiqueta = sqlDataReader.GetString(11); }
                                catch { }
                                try
                                { activos.CodigoBarras = sqlDataReader.GetString(12); }
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_InformesPersonalizados_Garantias: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_InformesPersonalizados_Garantias: " + ex.Message);
            }
            return source;//.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<Garantias> Get_list_InformesPersonalizados_Dotaciones()
        {
            List<Garantias> source = new List<Garantias>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_InformesPersonalizados_Dotaciones";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Garantias activos = new Garantias();
                                try { activos.DescripcionActivo = sqlDataReader.GetString(0); }
                                catch { }
                                try { activos.TipoActivo = sqlDataReader.GetString(1); }
                                catch { }
                                try { activos.Modelo = sqlDataReader.GetString(2); }
                                catch { }
                                try
                                { activos.ParteNumero = sqlDataReader.GetString(3); }
                                catch { }
                                try
                                { activos.Serial = sqlDataReader.GetString(4); }
                                catch { }
                                try
                                { activos.CodigoEtiqueta = sqlDataReader.GetString(5); }
                                catch { }
                                try
                                { activos.CodigoBarras = sqlDataReader.GetString(6); }
                                catch { }
                                try
                                { activos.Usuario  = sqlDataReader.GetString(7); }
                                catch { }
                                try
                                { activos.FechaIngreso = sqlDataReader.GetDateTime(8).ToString("yyyy/MM/dd");}
                                catch { }
                                try
                                { activos.FechaSalida = sqlDataReader.GetDateTime(9).ToString("yyyy/MM/dd"); }
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_InformesPersonalizados_Dotaciones: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_InformesPersonalizados_Dotaciones: " + ex.Message);
            }
            return source;//.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<Dasboard> Get_list_Dashboard_ActivosFijos()
        {
            List<Dasboard> source = new List<Dasboard>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Dashboard_ActivosFijos";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Dasboard activos = new Dasboard();
                                try { activos.EVENTO = sqlDataReader.GetString(0); }
                                catch { }
                                try { activos.HOY = sqlDataReader.GetInt32(1); }
                                catch { }
                                try { activos.AYER = sqlDataReader.GetInt32(2); }
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_Dashboard_ActivosFijos: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_Dashboard_ActivosFijos: " + ex.Message);
            }
            return source;//.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<Dasboard> Get_list_Dashboard_Retornables_Diario()
        {
            List<Dasboard> source = new List<Dasboard>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Dashboard_Retornables_Diario";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Dasboard activos = new Dasboard();
                                try { activos.AREA = sqlDataReader.GetString(0); }
                                catch { }
                                try { activos.FECHA = sqlDataReader.GetDateTime(1).ToString ("yyyy/MM/dd"); }
                                catch { }
                                try { activos.INGRESO = sqlDataReader.GetDouble(2); }
                                catch { }
                                try { activos.PROCESO = sqlDataReader.GetDouble(3); }
                                catch { }
                                try { activos.SALIDA = sqlDataReader.GetDouble(4); }
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_Dashboard_Retornables_Diario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_Dashboard_Retornables_Diario: " + ex.Message);
            }
            return source;//.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<RentaFlota> Get_list_InformesPersonalizados_EquiposDisponibles()
        {
            List<RentaFlota> source = new List<RentaFlota>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_InformesPersonalizados_EquiposDisponibles";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                RentaFlota activos = new RentaFlota();
                                try { activos.EMR  = sqlDataReader.GetString(0); } catch { }
                                try { activos.CodigoSAP = sqlDataReader.GetString(1); }catch { }
                                try { activos.DescripcionActivo = sqlDataReader.GetString(2); } catch { }
                                try { activos.Serial = sqlDataReader.GetString(3); } catch { }
                                try { activos.POTENCIAHP = sqlDataReader.GetString(4); }  catch { }
                                try { activos.CFM = sqlDataReader.GetString(5); } catch { }
                                try { activos.PRESIONPSI = sqlDataReader.GetString(6); } catch { }
                                try { activos.ParteNumero = sqlDataReader.GetString(7); } catch { }
                                try { activos.SecadorIncluido = sqlDataReader.GetString(8); } catch { }
                                try { activos.CanonMinimoRentaDiaria = sqlDataReader.GetString(9); } catch { }
                                try { activos.CanonMinimoArrendamientoMensual = sqlDataReader.GetString(10); } catch { }
                                try { activos.PrecioVenta = sqlDataReader.GetString(11); } catch { }
                                try { activos.IVA = sqlDataReader.GetString(12); } catch { }
                                try { activos.HORASTRABAJO = sqlDataReader.GetString(13); } catch { }// horas de trabajo
                                try { activos.Ciudad = sqlDataReader.GetString(14); } catch { }
                                try { activos.Condiciones = sqlDataReader.GetString(15); } catch { }
                                try { activos.TRA_FECHA_AUTORIZA = sqlDataReader.GetDateTime (16).ToString("yyyy/MM/dd") ; } catch { }//fecha de entrega
                                try { activos.TRA_Fecha_Estimada_Retorno = sqlDataReader.GetDateTime(17).ToString("yyyy/MM/dd"); } catch { }
                                try { activos.EstadoContrato = sqlDataReader.GetString(18); } catch { }
                                try { activos.EstadoOtroSi = sqlDataReader.GetString(19); } catch { }
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_InformesPersonalizados_EquiposDisponibles: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_InformesPersonalizados_EquiposDisponibles: " + ex.Message);
            }
            return source;//.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<ActivoDisponible> Get_list_InformesPersonalizados_ActivosDisponibles()
        {
            List<ActivoDisponible> source = new List<ActivoDisponible>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_InformesPersonalizados_ActivosDisponibles";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                ActivoDisponible activos = new ActivoDisponible();
                                try { activos.ACT_ID = sqlDataReader.GetGuid(0); } catch { }
                                try { activos.ACT_DESC = sqlDataReader.GetString(1); } catch { }
                                try { activos.ACT_TAC_ID = sqlDataReader.GetGuid(2); } catch { }
                                try { activos.TAC_DESC = sqlDataReader.GetString(3); } catch { }
                                try { activos.ACT_MOD_ID = sqlDataReader.GetGuid(4); } catch { }
                                try { activos.MOD_DESC = sqlDataReader.GetString(5); } catch { }
                                try { activos.ACT_EST_ID = sqlDataReader.GetGuid(6); } catch { }
                                try { activos.EST_DESC = sqlDataReader.GetString(7); } catch { }
                                try { activos.ACT_Serial = sqlDataReader.GetString(8); } catch { }
                                try { activos.ACT_ACTIVE = sqlDataReader.GetBoolean(9); } catch { }
                                try { activos.ETQ_BC = sqlDataReader.GetString(10); } catch { }
                                try { activos.ETQ_CODE = sqlDataReader.GetString(11); } catch { }
                                try { activos.ETQ_EPC = sqlDataReader.GetString(12); } catch { }
                                try { activos.ETQ_TID = sqlDataReader.GetGuid(13); } catch { }// horas de trabajo
                                try { activos.RES_ID = sqlDataReader.GetGuid(14); } catch { }
                                try { activos.RES_DOCUMENTO = sqlDataReader.GetString(15); } catch { }
                                try { activos.RES_NOMBRES = sqlDataReader.GetString(16); } catch { }//fecha de entrega
                                try { activos.RES_APELLIDOS = sqlDataReader.GetString(17); } catch { }
                                try { activos.ETA_ID = sqlDataReader.GetGuid(18); } catch { }
                                try { activos.RES_NOMBRE_APELLIDO = sqlDataReader.GetString(19); } catch { }
                                try { activos.ARE_ID = sqlDataReader.GetGuid(20); } catch { }
                                try { activos.ARE_DESC = sqlDataReader.GetString(21); } catch { }
                                try { activos.EDA_ID = sqlDataReader.GetGuid(23); } catch { }//fecha de entrega
                                try { activos.EDA_DESC = sqlDataReader.GetString (24); } catch { }
                                try { activos.P_N = sqlDataReader.GetString(25); } catch { }
                                try { activos.S_N = sqlDataReader.GetString(26); } catch { }
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_InformesPersonalizados_ActivosDisponibles: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_InformesPersonalizados_ActivosDisponibles: " + ex.Message);
            }
            return source;//.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
        public List<Dasboard> Get_list_Dashboard_Retornables_Status()
        {
            List<Dasboard> source = new List<Dasboard>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Dashboard_Retornables_Status";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Dasboard activos = new Dasboard();
                                try { activos.STATUS  = sqlDataReader.GetString(0); } //status
                                catch { }
                                try { activos.RETORNABLE  = sqlDataReader.GetBoolean(1); } // retornable
                                catch { }
                                try { activos.M2  = sqlDataReader.GetDouble(2); } // m2
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
                InformesDataBase.logger.Error<SqlException>("Sql error en Get_list_Dashboard_Retornables_Status: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                InformesDataBase.logger.Error(ex, "Error en Get_list_Dashboard_Retornables_Status: " + ex.Message);
            }
            return source;//.OrderBy<Activos, string>((Func<Activos, string>)(a => a.ACT_DESC)).OrderBy<Activos, string>((Func<Activos, string>)(t => t.ACT_TAC_DESC)).ToList<Activos>();
        }
    }
}