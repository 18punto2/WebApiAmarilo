// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Factory.BaseDataBase
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
  public class BaseDataBase : Ibase
  {
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
    private Logger logger = LogManager.GetCurrentClassLogger();

    public IEnumerable<Caracteristica> Get_list_caracteristica(
      Guid? Caracteristica)
    {
      List<Caracteristica> source = new List<Caracteristica>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_caracteristica";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@Caracteristica", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@Caracteristica"].Value = (object) Caracteristica;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Caracteristica caracteristica = new Caracteristica();
                caracteristica.CAR_ID = sqlDataReader.GetGuid(0);
                caracteristica.CAR_DESC = sqlDataReader.GetString(1);
                caracteristica.CAR_ESMODIFICABLE = sqlDataReader.GetBoolean(2);
                try
                {
                  caracteristica.CAR_CAR_PADRE_ID = new Guid?(sqlDataReader.GetGuid(3));
                }
                catch (Exception ex)
                {
                  this.logger.Error(ex, "Get_list_caracteristica: CAR_CAR_PADRE_ID ");
                }
                try
                {
                  caracteristica.CAR_DESC_PADRE = sqlDataReader.GetString(4);
                }
                catch (Exception ex)
                {
                  this.logger.Error(ex, "Get_list_caracteristica: CAR_DESC_PADRE");
                }
                caracteristica.CAR_FLAGPADRE = sqlDataReader.GetInt32(5) == 1;
                source.Add(caracteristica);
              }
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_caracteristica: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_caracteristica: " + ex.Message);
      }
      return (IEnumerable<Caracteristica>) source.OrderBy<Caracteristica, string>((Func<Caracteristica, string>) (T => T.CAR_DESC));
    }

    public IEnumerable<Caracteristica> Get_list_caracteristicaHija(
      Guid? CaracteristicaPadre)
    {
      List<Caracteristica> caracteristicaList = new List<Caracteristica>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_caracteristicaHija";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@Caracteristica", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@Caracteristica"].Value = (object) CaracteristicaPadre;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
              Caracteristica caracteristica = new Caracteristica();
              caracteristica.CAR_ID = sqlDataReader.GetGuid(0);
              caracteristica.CAR_DESC = sqlDataReader.GetString(1);
              caracteristica.CAR_ESMODIFICABLE = sqlDataReader.GetBoolean(2);
              try
              {
                caracteristica.CAR_CAR_PADRE_ID = new Guid?(sqlDataReader.GetGuid(3));
              }
              catch (Exception ex)
              {
                this.logger.Error(ex, "Get_list_caracteristicaHija: CAR_CAR_PADRE_ID ");
              }
              try
              {
                caracteristica.CAR_DESC_PADRE = sqlDataReader.GetString(4);
              }
              catch (Exception ex)
              {
                this.logger.Error(ex, "Get_list_caracteristicaHija: CAR_DESC_PADRE");
              }
              caracteristica.CAR_FLAGPADRE = sqlDataReader.GetBoolean(5);
              caracteristicaList.Add(caracteristica);
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_caracteristicaHija: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_caracteristicaHija: " + ex.Message);
      }
      return (IEnumerable<Caracteristica>) caracteristicaList;
    }

    public Mensaje Set_Crear_Caracteristica(
      List<Caracteristica> NuevaCaracteristica,
      Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Caracteristica";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CAR_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@CAR_ESMODIFICABLE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@CAR_CAR_PADRE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@RESPONSABLE_CREACION", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Caracteristica caracteristica in NuevaCaracteristica)
            {
              sqlCommand.Parameters["@CAR_DESC"].Value = (object) caracteristica.CAR_DESC;
              sqlCommand.Parameters["@CAR_ESMODIFICABLE"].Value = (object) caracteristica.CAR_ESMODIFICABLE;
              sqlCommand.Parameters["@CAR_CAR_PADRE_ID"].Value = (object) (caracteristica.CAR_CAR_PADRE_ID.Equals((object) "00000000-0000-0000-0000-000000000000") ? new Guid?() : caracteristica.CAR_CAR_PADRE_ID);
              sqlCommand.Parameters["@RESPONSABLE_CREACION"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_Caracteristica: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Caracteristica: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Caracteristica(
      List<Caracteristica> EditarCaracteristica,
      Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Caracteristica";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CAR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CAR_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@CAR_ESMODIFICABLE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@CAR_CAR_PADRE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@RESPONSABLE_MODIFICACION", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Caracteristica caracteristica in EditarCaracteristica)
            {
              sqlCommand.Parameters["@CAR_ID"].Value = (object) caracteristica.CAR_ID;
              sqlCommand.Parameters["@CAR_DESC"].Value = (object) caracteristica.CAR_DESC;
              sqlCommand.Parameters["@CAR_ESMODIFICABLE"].Value = (object) caracteristica.CAR_ESMODIFICABLE;
              Guid? carCarPadreId = caracteristica.CAR_CAR_PADRE_ID;
              Guid guid = Guid.Parse("00000000-0000-0000-0000-000000000000");
              if ((carCarPadreId.HasValue ? (carCarPadreId.HasValue ? (carCarPadreId.GetValueOrDefault() == guid ? 1 : 0) : 1) : 0) != 0)
                sqlCommand.Parameters["@CAR_CAR_PADRE_ID"].Value = (object) null;
              else
                sqlCommand.Parameters["@CAR_CAR_PADRE_ID"].Value = (object) caracteristica.CAR_CAR_PADRE_ID;
              sqlCommand.Parameters["@RESPONSABLE_MODIFICACION"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_Caracteristica: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_Caracteristica: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Caracteristica(
      List<Guid> EliminarCaracteristica,
      Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_Caracteristica";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CAR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@RESPONSABLE_ELIMINACION", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarCaracteristica)
            {
              sqlCommand.Parameters["@CAR_ID"].Value = (object) guid;
              sqlCommand.Parameters["@RESPONSABLE_ELIMINACION"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_Caracteristica: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_Caracteristica: " + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<CaracteristicaValores> Get_list_caracteristicaValor(
      Guid? CaracteristicaID,
      Guid? CaracteristicaValor)
    {
      List<CaracteristicaValores> source = new List<CaracteristicaValores>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_caracteristicaValor";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CaracteristicaID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@CaracteristicaID"].Value = (object) (!CaracteristicaID.HasValue ? CaracteristicaValor : CaracteristicaID);
            sqlCommand.Parameters.Add("@CaracteristicaValor", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@CaracteristicaValor"].Value = (object) (!CaracteristicaID.HasValue ? CaracteristicaID : CaracteristicaValor);
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                CaracteristicaValores caracteristicaValores = new CaracteristicaValores();
                caracteristicaValores.CVA_ID = sqlDataReader.GetGuid(0);
                caracteristicaValores.CVA_CAR_ID = sqlDataReader.GetGuid(1);
                caracteristicaValores.CVA_NOMBRE = sqlDataReader.GetString(2);
                try
                {
                  caracteristicaValores.CVA_CVA_PADRE_ID = new Guid?(sqlDataReader.GetGuid(3));
                }
                catch (Exception ex)
                {
                  this.logger.Error(ex, "Get_list_caracteristicaValor: CVA_CVA_PADRE_ID ");
                }
                caracteristicaValores.CVA_DEFAULT = sqlDataReader.GetBoolean(4);
                source.Add(caracteristicaValores);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_caracteristicaValor: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_caracteristicaValor: " + ex.Message);
      }
      return (IEnumerable<CaracteristicaValores>) source.OrderBy<CaracteristicaValores, string>((Func<CaracteristicaValores, string>) (t => t.CVA_NOMBRE)).ToList<CaracteristicaValores>();
    }

    public Mensaje Set_Crear_CaracteristicaValor(
      List<CaracteristicaValores> NuevaCaracteristicaValor,
      Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_CaracteristicaValor";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CVA_CAR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CVA_NOMBRE", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CVA_CVA_PADRE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CVA_DEFAULT", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@RESPONSABLE_CREACION", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (CaracteristicaValores caracteristicaValores in NuevaCaracteristicaValor)
            {
              sqlCommand.Parameters["@CVA_CAR_ID"].Value = (object) caracteristicaValores.CVA_CAR_ID;
              sqlCommand.Parameters["@CVA_NOMBRE"].Value = (object) caracteristicaValores.CVA_NOMBRE;
              sqlCommand.Parameters["@CVA_CVA_PADRE_ID"].Value = (object) (caracteristicaValores.CVA_CVA_PADRE_ID.Equals((object) "00000000-0000-0000-0000-000000000000") ? new Guid?() : caracteristicaValores.CVA_CVA_PADRE_ID);
              sqlCommand.Parameters["@CVA_DEFAULT"].Value = (object) caracteristicaValores.CVA_DEFAULT;
              sqlCommand.Parameters["@RESPONSABLE_CREACION"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_CaracteristicaValor: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_CaracteristicaValor: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_CaracteristicaValor(
      List<CaracteristicaValores> EditarCaracteristicaValor,
      Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_CaracteristicaValor";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CVA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CVA_CAR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CVA_NOMBRE", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CVA_CVA_PADRE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CVA_DEFAULT", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@RESPONSABLE_MODIFICACION", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (CaracteristicaValores caracteristicaValores in EditarCaracteristicaValor)
            {
              sqlCommand.Parameters["@CVA_ID"].Value = (object) caracteristicaValores.CVA_ID;
              sqlCommand.Parameters["@CVA_CAR_ID"].Value = (object) caracteristicaValores.CVA_CAR_ID;
              sqlCommand.Parameters["@CVA_NOMBRE"].Value = (object) caracteristicaValores.CVA_NOMBRE;
              sqlCommand.Parameters["@CVA_CVA_PADRE_ID"].Value = (object) caracteristicaValores.CVA_CVA_PADRE_ID;
              sqlCommand.Parameters["@CVA_DEFAULT"].Value = (object) caracteristicaValores.CVA_DEFAULT;
              sqlCommand.Parameters["@RESPONSABLE_MODIFICACION"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_CaracteristicaValor: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_CaracteristicaValor: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_CaracteristicaValor(
      List<Guid> EliminarCaracteristicaValor,
      Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_CaracteristicaValor";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CVA_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@RESPONSABLE_ELIMINACION", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarCaracteristicaValor)
            {
              sqlCommand.Parameters["@CVA_ID"].Value = (object) guid;
              sqlCommand.Parameters["@RESPONSABLE_ELIMINACION"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_CaracteristicaValor: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_CaracteristicaValor: " + ex.Message);
      }
      return mensaje;
    }

    public List<Areas> Get_list_Area(Guid? Area, Guid? USU_ID, Guid? RES_ID)
    {
      List<Areas> source = new List<Areas>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Areav2";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ARE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@ARE_ID"].Value = (object) Area;
            sqlCommand.Parameters.Add("@USU_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@USU_ID"].Value = (object) USU_ID;
            sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@RES_ID"].Value = (object) RES_ID;
            //sqlCommand.Parameters.Add("@ES_ST", SqlDbType.Bit);
            //sqlCommand.Parameters["@ES_ST"].Value = (object) ES_ST;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
              Areas areas1 = new Areas();
              areas1.ARE_ID = sqlDataReader.GetGuid(0);
              areas1.ARE_DESC = sqlDataReader.GetString(1) == null ? "" : sqlDataReader.GetString(1);
              try
              {
                Areas areas2 = areas1;
                sqlDataReader.GetGuid(2);
                Guid? nullable = new Guid?(sqlDataReader.GetGuid(2));
                areas2.ARE_ARE_PARENT_ID = nullable;
                areas1.ARE_DESC_PADRE = sqlDataReader.GetString(3) == null ? "" : sqlDataReader.GetString(3);
              }
              catch
              {
              }
              try
              {
                areas1.ARE_DESC_CENTROCOSTO = sqlDataReader.GetString(4) == null ? "" : sqlDataReader.GetString(4);
              }
              catch
              {
              }
              try
              {
                areas1.ARE_DESC_TIPOAREA = sqlDataReader.GetString(5) == null ? "" : sqlDataReader.GetString(5);
              }
              catch
              {
              }
              try
              {
                areas1.ARE_OBSERVACIONES = sqlDataReader.GetString(6) == null ? "" : sqlDataReader.GetString(6);
              }
              catch
              {
              }
              try
              {
                areas1.ARE_CIUDAD = sqlDataReader.GetString(7) == null ? "" : sqlDataReader.GetString(7);
              }
              catch
              {
              }
              try
              {
                areas1.ARE_DIRECCION = sqlDataReader.GetString(8) == null ? "" : sqlDataReader.GetString(8);
              }
              catch
              {
              }
              try
              {
                areas1.ARE_CODIGO_POSTAL = sqlDataReader.GetString(9) == null ? "" : sqlDataReader.GetString(9);
              }
              catch
              {
              }
              try
              {
                Areas areas2 = areas1;
                sqlDataReader.GetGuid(10);
                Guid guid = sqlDataReader.GetGuid(10);
                areas2.ARE_TAR_ID = guid;
              }
              catch
              {
              }
              try
              {
                Areas areas2 = areas1;
                sqlDataReader.GetGuid(11);
                Guid guid = sqlDataReader.GetGuid(11);
                areas2.ARE_CCO_ID = guid;
              }
              catch
              {
              }
              try
              {
                areas1.SELECCIONADO = sqlDataReader.GetInt32(12) != 0;
              }
              catch
              {
              }
            try { areas1.ES_ST = sqlDataReader.GetBoolean(13);
                 }
            catch { }
            try
            {
                areas1.ARE_IS_RENTA = sqlDataReader.GetBoolean(14);
            }
            catch { }
                            
           source.Add(areas1);           
                        }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_Area: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_Area: " + ex.Message);
      }
      return source.ToList<Areas>().OrderBy<Areas, string>((Func<Areas, string>) (t => t.ARE_DESC)).ToList<Areas>().OrderBy<Areas, string>((Func<Areas, string>) (a => a.ARE_CIUDAD)).ToList<Areas>();
    }

    public Mensaje Set_Crear_Area(List<Areas> NuevaArea, Guid Usuario)
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
            sqlCommand.CommandText = "[NEGOCIO].[Set_Crear_AreaV2]";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ARE_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@ARE_ARE_PARENT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_CCO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_TAR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_OBSERVACIONES", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@ARE_CIUDAD", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@ARE_DIRECCION", SqlDbType.VarChar, 150);
            sqlCommand.Parameters.Add("@ARE_CODIGO_POSTAL", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@ARE_IS_SERVICIO_TECNICO", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@ARE_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Areas areas in NuevaArea)
            {
              sqlCommand.Parameters["@ARE_DESC"].Value = (object) areas.ARE_DESC;
              sqlCommand.Parameters["@ARE_ARE_PARENT_ID"].Value = (object) (areas.ARE_ARE_PARENT_ID.Equals((object) "00000000 - 0000 - 0000 - 0000 - 000000000000") ? new Guid?() : areas.ARE_ARE_PARENT_ID);
              sqlCommand.Parameters["@ARE_CCO_ID"].Value = (object) areas.ARE_CCO_ID;
              sqlCommand.Parameters["@ARE_TAR_ID"].Value = (object) areas.ARE_TAR_ID;
              sqlCommand.Parameters["@ARE_OBSERVACIONES"].Value = (object) areas.ARE_OBSERVACIONES;
              sqlCommand.Parameters["@ARE_CIUDAD"].Value = (object) areas.ARE_CIUDAD;
              sqlCommand.Parameters["@ARE_DIRECCION"].Value = (object) areas.ARE_DIRECCION;
              sqlCommand.Parameters["@ARE_CODIGO_POSTAL"].Value = (object) areas.ARE_CODIGO_POSTAL;
                sqlCommand.Parameters["@ARE_IS_SERVICIO_TECNICO"].Value = (object)areas.ES_ST;
                sqlCommand.Parameters["@ARE_USUARIO_CREATE_ID"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_AreaV2: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Area: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Area(List<Areas> EditarArea, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_AreaV2";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ARE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@ARE_ARE_PARENT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_CCO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_TAR_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_OBSERVACIONES", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@ARE_CIUDAD", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@ARE_DIRECCION", SqlDbType.VarChar, 150);
            sqlCommand.Parameters.Add("@ARE_CODIGO_POSTAL", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@ARE_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_IS_SERVICIO_TECNICO", SqlDbType.Bit);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Areas areas in EditarArea)
            {
              sqlCommand.Parameters["@ARE_ID"].Value = (object) areas.ARE_ID;
              sqlCommand.Parameters["@ARE_DESC"].Value = (object) areas.ARE_DESC;
              sqlCommand.Parameters["@ARE_ARE_PARENT_ID"].Value = (object) (areas.ARE_ARE_PARENT_ID.Equals((object) "00000000 - 0000 - 0000 - 0000 - 000000000000") ? new Guid?() : areas.ARE_ARE_PARENT_ID);
              sqlCommand.Parameters["@ARE_CCO_ID"].Value = (object) areas.ARE_CCO_ID;
              sqlCommand.Parameters["@ARE_TAR_ID"].Value = (object) areas.ARE_TAR_ID;
              sqlCommand.Parameters["@ARE_OBSERVACIONES"].Value = (object) areas.ARE_OBSERVACIONES;
              sqlCommand.Parameters["@ARE_CIUDAD"].Value = (object) areas.ARE_CIUDAD;
              sqlCommand.Parameters["@ARE_DIRECCION"].Value = (object) areas.ARE_DIRECCION;
              sqlCommand.Parameters["@ARE_CODIGO_POSTAL"].Value = (object) areas.ARE_CODIGO_POSTAL;
              sqlCommand.Parameters["@ARE_USUARIO_MOD_ID"].Value = (object) Usuario;
            sqlCommand.Parameters["@ARE_IS_SERVICIO_TECNICO"].Value = (object)areas.ES_ST;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_Area: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_Area: " + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Area(List<Guid> EliminarArea, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_Area";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ARE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarArea)
            {
              sqlCommand.Parameters["@ARE_ID"].Value = (object) guid;
              sqlCommand.Parameters["@ARE_USUARIO_DELETE_ID"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_Area: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_Area: " + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<TipoActivo> Get_list_TipoActivo_Area(
      Guid? ID_AreaTipoActivo)
    {
      List<TipoActivo> source = new List<TipoActivo>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "BASE.Get_list_TipoActivo_Area";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ARE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@ARE_ID"].Value = (object) ID_AreaTipoActivo;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                source.Add(new TipoActivo()
                {
                  TAC_ID = sqlDataReader.GetGuid(0),
                  TAC_DESC = sqlDataReader.GetString(1),
                  TAC_ACTIVE = sqlDataReader.GetInt32(3) != 0
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_TipoActivo_Area: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error("Error en Get_list_TipoActivo_Area:" + ex.Message);
      }
      return (IEnumerable<TipoActivo>) source.OrderBy<TipoActivo, string>((Func<TipoActivo, string>) (t => t.TAC_DESC)).ToList<TipoActivo>();
    }

    public IEnumerable<TipoArea> Get_list_TipoArea(string TipoArea)
    {
      List<TipoArea> source = new List<TipoArea>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_TipoArea";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                source.Add(new TipoArea()
                {
                  TAR_ID = sqlDataReader.GetGuid(0),
                  TAR_DESC = sqlDataReader.GetString(1)
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_TipoArea: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error("Error en Get_list_TipoArea:" + ex.Message);
      }
      return (IEnumerable<TipoArea>) source.OrderBy<TipoArea, string>((Func<TipoArea, string>) (t => t.TAR_DESC)).ToList<TipoArea>();
    }

    public Mensaje Set_Mant_Activos(List<TipoActivoArea> MantTipoActivo, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Mant_TipoActivoArea";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TAU_TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TAU_ARE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@RESPONSABLE_CREACION", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TIPO", SqlDbType.Bit);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (TipoActivoArea tipoActivoArea in (IEnumerable<TipoActivoArea>) MantTipoActivo.OrderBy<TipoActivoArea, Guid>((Func<TipoActivoArea, Guid>) (t => t.TAU_TAC_ID)))
            {
              sqlCommand.Parameters["@TAU_TAC_ID"].Value = (object) tipoActivoArea.TAU_TAC_ID;
              sqlCommand.Parameters["@TAU_ARE_ID"].Value = (object) tipoActivoArea.TAU_ARE_ID;
              sqlCommand.Parameters["@RESPONSABLE_CREACION"].Value = (object) Usuario;
              sqlCommand.Parameters["@TIPO"].Value = (object) tipoActivoArea.TAU_ACTIVE;
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
        this.logger.Error<SqlException>("Sql error en Set_Mant_TipoActivoArea: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Mant_TipoActivoArea: " + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<ActivoValoresCaracteristicas> Get_list_activosValor(
      Guid MOD_ID,
      Guid? ACT_ID)
    {
      List<ActivoValoresCaracteristicas> valoresCaracteristicasList = new List<ActivoValoresCaracteristicas>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_activosValor";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ACT_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@MOD_ID"].Value = (object) MOD_ID;
            sqlCommand.Parameters["@ACT_ID"].Value = (object) ACT_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                ActivoValoresCaracteristicas valoresCaracteristicas = new ActivoValoresCaracteristicas();
                try
                {
                  valoresCaracteristicas.AVA_ID = sqlDataReader.GetGuid(0);
                }
                catch
                {
                }
                valoresCaracteristicas.AVA_TAC_ID = sqlDataReader.GetGuid(1);
                valoresCaracteristicas.AVA_TAC_DESC = sqlDataReader.GetString(2);
                valoresCaracteristicas.AVA_TAC_RETORNABLE = sqlDataReader.GetBoolean(3);
                valoresCaracteristicas.AVA_MOD_ID = sqlDataReader.GetGuid(4);
                valoresCaracteristicas.AVA_MOD_DESC = sqlDataReader.GetString(5);
                valoresCaracteristicas.AVA_CAR_ID = sqlDataReader.GetGuid(6);
                valoresCaracteristicas.AVA_CAR_DESC = sqlDataReader.GetString(7);
                valoresCaracteristicas.AVA_CAR_ESMODIFICABLE = sqlDataReader.GetBoolean(8);
                valoresCaracteristicas.AVA_CAR_ESLISTA = sqlDataReader.GetBoolean(9);
                try
                {
                  valoresCaracteristicas.AVA_CAR_CAR_PADRE_ID = sqlDataReader.GetGuid(10);
                }
                catch
                {
                }
                try
                {
                  valoresCaracteristicas.AVA_CVA_ID = sqlDataReader.GetGuid(11);
                }
                catch
                {
                }
                try
                {
                  valoresCaracteristicas.AVA_VALOR = sqlDataReader.GetString(12);
                }
                catch
                {
                }
                valoresCaracteristicasList.Add(valoresCaracteristicas);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_activosValor: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_activosValor: " + ex.Message);
      }
      return (IEnumerable<ActivoValoresCaracteristicas>) valoresCaracteristicasList;
    }

    public IEnumerable<Estados> Get_list_Estados(Guid? EST_ID)
    {
      Mensaje mensaje = new Mensaje();
      List<Estados> source = new List<Estados>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Estados";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@EST_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@EST_ID"].Value = (object) EST_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                source.Add(new Estados()
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
        this.logger.Error<SqlException>("Sql error en Get_list_Estados: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Get_list_Estados: " + ex.Message);
      }
      return (IEnumerable<Estados>) source.OrderBy<Estados, string>((Func<Estados, string>) (t => t.EST_DESC)).ToList<Estados>();
    }

        public IEnumerable<Estados> Get_list_EstadosDisponibilidad()
        {
            Mensaje mensaje = new Mensaje();
            List<Estados> source = new List<Estados>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_EstadosDisponibilidad";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                       using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                                source.Add(new Estados()
                                {
                                    EST_ID = sqlDataReader.GetGuid(0),
                                    EST_DESC = sqlDataReader.GetString(1),                                   
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
                this.logger.Error<SqlException>("Sql error en Get_list_EstadosDisponibilidad: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                mensaje.errNumber = -2;
                mensaje.message = ex.Message;
                this.logger.Error(ex, "Error en Get_list_EstadosDisponibilidad: " + ex.Message);
            }
            return (IEnumerable<Estados>)source.OrderBy<Estados, string>((Func<Estados, string>)(t => t.EST_DESC)).ToList<Estados>();
        }
        public IEnumerable<Modelo> Get_list_Modelo(Guid? Modelos,DateTime? MOD_FECHA_MOD)
    {
      Mensaje mensaje = new Mensaje();
      List<Modelo> source = new List<Modelo>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Modelo";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@MOD_FECHA_MOD", SqlDbType.DateTime);
            sqlCommand.Parameters["@MOD_ID"].Value = (object) Modelos;
            sqlCommand.Parameters["@MOD_FECHA_MOD"].Value = (object)MOD_FECHA_MOD;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                source.Add(new Modelo()
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
        this.logger.Error<SqlException>("Sql error en Get_list_Modelo: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Get_list_Modelo: " + ex.Message);
      }
      return (IEnumerable<Modelo>) source.OrderBy<Modelo, string>((Func<Modelo, string>) (T => T.MOD_DESC));
    }

    public Mensaje Set_Crear_Modelo(List<Modelo> NuevaModelo, Guid Usuario)
    {
      Mensaje mensaje = new Mensaje();
      try
      {
        Guid guid = Usuario;
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Modelo";
            mensaje.data = (object) nameof (Set_Crear_Modelo);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@MOD_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@MOD_TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@MOD_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            foreach (Modelo modelo in NuevaModelo)
            {
              sqlCommand.Parameters["@MOD_DESC"].Value = (object) modelo.MOD_DESC;
              sqlCommand.Parameters["@MOD_TAC_ID"].Value = (object) modelo.MOD_TAC_ID;
              sqlCommand.Parameters["@MOD_USUARIO_CREATE_ID"].Value = (object) Usuario;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                if (sqlDataReader.Read())
                  guid = sqlDataReader.GetGuid(0);
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
                sqlCommand.CommandText = "NEGOCIO.Set_Crear_ModeloValoresCaracteristicas";
                mensaje.data = (object) "Set_Crear_ModeloValoresCaracteristicas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@TAM_TCA_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@TAM_MOD_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@TAM_VALOR", SqlDbType.VarChar, 250);
                sqlCommand.Parameters.Add("@RESPONSABLE_CREACION", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@TAM_CVA_ID", SqlDbType.UniqueIdentifier);
                if (modelo.SELECCIONADOS != null)
                {
                  string seleccionados = modelo.SELECCIONADOS;
                  char[] chArray = new char[1]{ '_' };
                  foreach (string str in seleccionados.Split(chArray))
                  {
                    sqlCommand.Parameters["@TAM_TCA_ID"].Value = (object) Guid.Parse(str.Split('|')[0]);
                    sqlCommand.Parameters["@TAM_MOD_ID"].Value = (object) guid;
                    sqlCommand.Parameters["@TAM_VALOR"].Value = (object) str.Split('|')[1];
                    if (!str.Split('|')[2].Equals(""))
                      sqlCommand.Parameters["@TAM_CVA_ID"].Value = (object) Guid.Parse(str.Split('|')[2]);
                    else
                      sqlCommand.Parameters["@TAM_CVA_ID"].Value = (object) null;
                    sqlCommand.Parameters["@RESPONSABLE_CREACION"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en :" + ex.Message + mensaje.data.ToString(), ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en :" + ex.Message + mensaje.data.ToString());
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Modelo(List<Modelo> EditarModelo, Guid Usuario)
    {
      Mensaje mensaje = new Mensaje();
      try
      {
        string str1 = "";
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Modelo";
            mensaje.data = (object) nameof (Set_Editar_Modelo);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@MOD_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@MOD_TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@MOD_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = -1;
            mensaje.message = str1;
            foreach (Modelo modelo in EditarModelo)
            {
              sqlCommand.Parameters["@MOD_ID"].Value = (object) modelo.MOD_ID;
              sqlCommand.Parameters["@MOD_DESC"].Value = (object) modelo.MOD_DESC;
              sqlCommand.Parameters["@MOD_TAC_ID"].Value = (object) modelo.MOD_TAC_ID;
              sqlCommand.Parameters["@MOD_USUARIO_MOD_ID"].Value = (object) Usuario;
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
                sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_ModeloValoresCaracteristicas";
                mensaje.data = (object) "Set_Eliminar_ModeloValoresCaracteristicas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@TAM_MOD_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters["@TAM_MOD_ID"].Value = (object) modelo.MOD_ID;
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "NEGOCIO.Set_Crear_ModeloValoresCaracteristicas";
                mensaje.data = (object) "Set_Crear_ModeloValoresCaracteristicas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@TAM_TCA_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@TAM_VALOR", SqlDbType.VarChar, 250);
                sqlCommand.Parameters.Add("@RESPONSABLE_CREACION", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@TAM_CVA_ID", SqlDbType.UniqueIdentifier);
                if (modelo.SELECCIONADOS != null)
                {
                  string seleccionados = modelo.SELECCIONADOS;
                  char[] chArray = new char[1]{ '_' };
                  foreach (string str2 in seleccionados.Split(chArray))
                  {
                    sqlCommand.Parameters["@TAM_TCA_ID"].Value = (object) Guid.Parse(str2.Split('|')[0]);
                    sqlCommand.Parameters["@TAM_MOD_ID"].Value = (object) modelo.MOD_ID;
                    sqlCommand.Parameters["@TAM_VALOR"].Value = (object) str2.Split('|')[1];
                    if (!str2.Split('|')[2].Equals(""))
                    {
                      if (!str2.Split('|')[2].Equals("00000000-0000-0000-0000-000000000000"))
                      {
                        sqlCommand.Parameters["@TAM_CVA_ID"].Value = (object) Guid.Parse(str2.Split('|')[2]);
                        goto label_19;
                      }
                    }
                    sqlCommand.Parameters["@TAM_CVA_ID"].Value = (object) null;
label_19:
                    sqlCommand.Parameters["@RESPONSABLE_CREACION"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en :" + ex.Message + mensaje.data.ToString(), ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en :" + ex.Message + mensaje.data.ToString());
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Modelo(List<Guid> EliminarModelo, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_Modelo";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@MOD_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarModelo)
            {
              sqlCommand.Parameters["@MOD_ID"].Value = (object) guid;
              sqlCommand.Parameters["@MOD_USUARIO_DELETE_ID"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_Modelo:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_Modelo:" + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<ModeloValoresCaracteristicas> Get_list_ModeloValoresCaracteristicas(
      Guid TipoActivoID,
      Guid? ModeloID)
    {
      Mensaje mensaje = new Mensaje();
      List<ModeloValoresCaracteristicas> valoresCaracteristicasList = new List<ModeloValoresCaracteristicas>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_ModeloValoresCaracteristicas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@TAC_ID"].Value = (object) TipoActivoID;
            sqlCommand.Parameters["@MOD_ID"].Value = (object) ModeloID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                ModeloValoresCaracteristicas valoresCaracteristicas = new ModeloValoresCaracteristicas();
                try
                {
                  valoresCaracteristicas.TAM_ID = sqlDataReader.GetGuid(0);
                }
                catch
                {
                }
                valoresCaracteristicas.TAM_TCA_ID = sqlDataReader.GetGuid(1);
                try
                {
                  valoresCaracteristicas.TAM_MOD_ID = sqlDataReader.GetGuid(2);
                }
                catch
                {
                }
                try
                {
                  valoresCaracteristicas.TAM_CAR_ID = sqlDataReader.GetGuid(3);
                }
                catch
                {
                }
                try
                {
                  valoresCaracteristicas.TAM_VALOR = sqlDataReader.GetString(4);
                }
                catch
                {
                }
                valoresCaracteristicas.TAM_CARACTERISTICA = sqlDataReader.GetString(5);
                valoresCaracteristicas.TAM_ISLIST = sqlDataReader.GetBoolean(6);
                try
                {
                  valoresCaracteristicas.TAM_CVA_ID = sqlDataReader.GetGuid(7);
                }
                catch
                {
                }
                try
                {
                  valoresCaracteristicas.CAR_ESMODIFICABLE = sqlDataReader.GetBoolean(8);
                }
                catch
                {
                }
                valoresCaracteristicasList.Add(valoresCaracteristicas);
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
        this.logger.Error<SqlException>("Sql error en Get_list_ModeloValoresCaracteristicas:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Get_list_ModeloValoresCaracteristicas:" + ex.Message);
      }
      return (IEnumerable<ModeloValoresCaracteristicas>) valoresCaracteristicasList;
    }

    public IEnumerable<TipoActivo> Get_list_TipoActivo(Guid? TipoActivo,DateTime? TAC_FECHA_MOD)
    {
      List<TipoActivo> source = new List<TipoActivo>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_TipoActivo";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TAC_FECHA_MOD", SqlDbType.DateTime);
            sqlCommand.Parameters["@TAC_ID"].Value = (object) TipoActivo;
            sqlCommand.Parameters["@TAC_FECHA_MOD"].Value = (object)TAC_FECHA_MOD;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                source.Add(new TipoActivo()
                {
                  TAC_ID = sqlDataReader.GetGuid(0),
                  TAC_DESC = sqlDataReader.GetString(1),
                  TAC_RETORNABLE = sqlDataReader.GetBoolean(2)
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_TipoActivo:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_TipoActivo:" + ex.Message);
      }
      return (IEnumerable<TipoActivo>) source.OrderBy<TipoActivo, string>((Func<TipoActivo, string>) (T => T.TAC_DESC));
    }

    public Mensaje Set_Crear_TipoActivo(List<TipoActivo> NuevaTipoActivo, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_TipoActivo";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TAC_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@TAC_RETORNABLE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@TAC_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (TipoActivo tipoActivo in NuevaTipoActivo)
            {
              sqlCommand.Parameters["@TAC_DESC"].Value = (object) tipoActivo.TAC_DESC;
              sqlCommand.Parameters["@TAC_RETORNABLE"].Value = (object) tipoActivo.TAC_RETORNABLE;
              sqlCommand.Parameters["@TAC_USUARIO_CREATE_ID"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_TipoActivo:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_TipoActivo:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_TipoActivo(List<TipoActivo> EditarTipoActivo, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_TipoActivo";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TAC_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@TAC_RETORNABLE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@TAC_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (TipoActivo tipoActivo in EditarTipoActivo)
            {
              sqlCommand.Parameters["@TAC_ID"].Value = (object) tipoActivo.TAC_ID;
              sqlCommand.Parameters["@TAC_DESC"].Value = (object) tipoActivo.TAC_DESC;
              sqlCommand.Parameters["@TAC_RETORNABLE"].Value = (object) tipoActivo.TAC_RETORNABLE;
              sqlCommand.Parameters["@TAC_USUARIO_MOD_ID"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_TipoActivo:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_TipoActivo:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_TipoActivo(List<Guid> EliminarTipoActivo, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_TipoActivo";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TAC_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@TAC_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarTipoActivo)
            {
              sqlCommand.Parameters["@TAC_ID"].Value = (object) guid;
              sqlCommand.Parameters["@TAC_USUARIO_DELETE_ID"].Value = (object) Usuario;
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_TipoActivo:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_TipoActivo:" + ex.Message);
      }
      return mensaje;
    }

    public List<TipoActivoCaracteristicas> Get_list_TipoActivoCaracteristica(
      Guid TipoActivoCaracteristica,
      bool Todo)
    {
      Mensaje mensaje = new Mensaje();
      List<TipoActivoCaracteristicas> activoCaracteristicasList = new List<TipoActivoCaracteristicas>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_TipoActivoCaracteristica";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TipoActivoCaracteristica", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@Todo", SqlDbType.Bit);
            sqlCommand.Parameters["@TipoActivoCaracteristica"].Value = (object) TipoActivoCaracteristica;
            sqlCommand.Parameters["@Todo"].Value = (object) Todo;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                TipoActivoCaracteristicas activoCaracteristicas = new TipoActivoCaracteristicas();
                try
                {
                  activoCaracteristicas.TCA_ID = sqlDataReader.GetGuid(0);
                }
                catch
                {
                }
                activoCaracteristicas.TCA_TAC_ID = sqlDataReader.GetGuid(1);
                activoCaracteristicas.TCA_CAR_ID = sqlDataReader.GetGuid(2);
                activoCaracteristicas.TCA_CAR_NOMBRE = sqlDataReader.GetString(3);
                activoCaracteristicas.TCA_ASIGNADO = sqlDataReader.GetBoolean(4);
                activoCaracteristicasList.Add(activoCaracteristicas);
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
        this.logger.Error<SqlException>("Sql error en Get_list_TipoActivoCaracteristica:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Get_list_TipoActivoCaracteristica:" + ex.Message);
      }
      return activoCaracteristicasList;
    }

    public Mensaje Set_Asociar_TipoActivoCaracteristicas(
      List<TipoActivoCaracteristicas> ListaTipoActivoCarateristica,
      Guid TCA_TAC_ID,
      Guid UsuarioTipoActivoCrear)
    {
      Mensaje mensaje = new Mensaje();
      try
      {
        string str = "Registro creado con éxito";
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_TipoActivoCaracteristicas";
            mensaje.data = (object) "Set_Eliminar_TipoActivoCaracteristicas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TCA_TAC_ID", SqlDbType.UniqueIdentifier).Value = (object) TCA_TAC_ID;
            sqlCommand.Parameters.Add("@TCA_CAR_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            DataSet dataSet = new DataSet();
            foreach (TipoActivoCaracteristicas activoCaracteristicas in ListaTipoActivoCarateristica.FindAll((Predicate<TipoActivoCaracteristicas>) (t => !t.TCA_ASIGNADO)))
            {
              sqlCommand.Parameters["@TCA_CAR_ID"].Value = (object) activoCaracteristicas.TCA_CAR_ID;
              new SqlDataAdapter()
              {
                SelectCommand = sqlCommand
              }.Fill(dataSet);
              mensaje.errNumber = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
              mensaje.message = Convert.ToString(dataSet.Tables[0].Rows[0][1]);
              if (mensaje.errNumber == 0)
                dataSet.Tables[0].Rows.Clear();
              else
                break;
            }
            if (mensaje.errNumber == 0)
            {
              sqlCommand.Parameters.Clear();
              sqlCommand.CommandText = "NEGOCIO.Set_Crear_TipoActivoCaracteristicas";
              mensaje.data = (object) "Set_Crear_TipoActivoCaracteristicas";
              sqlCommand.Parameters.Add("@TCA_TAC_ID", SqlDbType.UniqueIdentifier);
              sqlCommand.Parameters.Add("@TCA_CAR_ID", SqlDbType.UniqueIdentifier);
              sqlCommand.Parameters.Add("@TAC_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier).Value = (object) UsuarioTipoActivoCrear;
              foreach (TipoActivoCaracteristicas activoCaracteristicas in ListaTipoActivoCarateristica.FindAll((Predicate<TipoActivoCaracteristicas>) (t => t.TCA_ASIGNADO)))
              {
                sqlCommand.Parameters["@TCA_TAC_ID"].Value = (object) activoCaracteristicas.TCA_TAC_ID;
                sqlCommand.Parameters["@TCA_CAR_ID"].Value = (object) activoCaracteristicas.TCA_CAR_ID;
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
      }
      catch (SqlException ex)
      {
        mensaje.errNumber = -1;
        mensaje.message = ex.Message;
        this.logger.Error<SqlException>("Sql error en :" + ex.Message + mensaje.data.ToString(), ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en :" + ex.Message + mensaje.data.ToString());
      }
      return mensaje;
    }

    public IEnumerable<Responsable> Get_list_Responsable(
      Guid? Responsable,
      string Buscar,
      Guid? Area_ID)
    {
      List<Responsable> source = new List<Responsable>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Responsables";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@RES_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@RES_ARE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@RES_ID"].Value = (object) Responsable;
            sqlCommand.Parameters["@RES_NOMBRE"].Value = (object) Buscar;
            sqlCommand.Parameters["@RES_ARE_ID"].Value = (object) Area_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Responsable responsable = new Responsable();
                responsable.RES_ID = sqlDataReader.GetGuid(0);
                try
                {
                  responsable.RES_DOCUMENTO = sqlDataReader.GetString(1);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_NOMBRES = sqlDataReader.GetString(2);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_APELLIDOS = sqlDataReader.GetString(3);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_CARGO = sqlDataReader.GetString(4);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_CORREO = sqlDataReader.GetString(5);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_PHOTO_1 = sqlDataReader.GetString(6);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_CELULAR = sqlDataReader.GetString(7);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_TELEFONO = sqlDataReader.GetString(8);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_EXT = sqlDataReader.GetString(9);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_OBSERVACION = sqlDataReader.GetString(10);
                }
                catch
                {
                }
                try
                {
                  responsable.RES_ACTIVE = sqlDataReader.GetBoolean(11);
                }
                catch
                {
                }
                source.Add(responsable);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_Responsables:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_Responsables:" + ex.Message);
      }
      return (IEnumerable<Responsable>) source.OrderBy<Responsable, string>((Func<Responsable, string>) (t => t.RES_NOMBRES)).ToList<Responsable>();
    }

        public IEnumerable<Responsable> Get_list_ResponsableLista(
      Guid? Responsable,
      string Buscar,
      Guid? Area_ID)
        {
            List<Responsable> source = new List<Responsable>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "NEGOCIO.Get_list_Responsables";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters.Add("@RES_NOMBRE", SqlDbType.VarChar, 50);
                        sqlCommand.Parameters.Add("@RES_ARE_ID", SqlDbType.UniqueIdentifier);
                        sqlCommand.Parameters["@RES_ID"].Value = (object)Responsable;
                        sqlCommand.Parameters["@RES_NOMBRE"].Value = (object)Buscar;
                        sqlCommand.Parameters["@RES_ARE_ID"].Value = (object)Area_ID;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                if (sqlDataReader.GetBoolean(11))
                                { 
                                Responsable responsable = new Responsable();
                                responsable.RES_ID = sqlDataReader.GetGuid(0);                               
                                try
                                {
                                    responsable.RES_NOMBRES = sqlDataReader.GetString(2);
                                }
                                catch
                                {
                                }
                                try
                                {
                                    responsable.RES_APELLIDOS = sqlDataReader.GetString(3);
                                }
                                catch
                                {
                                }                                                                
                                source.Add(responsable);
                                }
                            }
                            sqlDataReader.Close();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                this.logger.Error<SqlException>("Sql error en Get_list_ResponsableLista:" + ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error en Get_list_ResponsableLista:" + ex.Message);
            }
            return (IEnumerable<Responsable>)source.OrderBy<Responsable, string>((Func<Responsable, string>)(t => t.RES_NOMBRES)).ToList<Responsable>();
        }
        public IEnumerable<AreaResponsable> Get_list_ResponsablesAreas(
      Guid? Responsable,
      Guid? Area_ID)
    {
      List<AreaResponsable> source = new List<AreaResponsable>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_ResponsablesAreas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ARE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@RES_ID"].Value = (object) Responsable;
            sqlCommand.Parameters["@ARE_ID"].Value = (object) Area_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                AreaResponsable areaResponsable = new AreaResponsable();
                try
                {
                  areaResponsable.RAR_ID = sqlDataReader.GetGuid(0);
                }
                catch
                {
                }
                try
                {
                  areaResponsable.RES_ID = sqlDataReader.GetGuid(1);
                }
                catch
                {
                }
                try
                {
                  areaResponsable.RESPONSABLE = sqlDataReader.GetString(2);
                }
                catch
                {
                }
                try
                {
                  areaResponsable.SELECCIONADO = sqlDataReader.GetBoolean(3);
                }
                catch
                {
                }
                source.Add(areaResponsable);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_ResponsablesAreas:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_ResponsablesAreas:" + ex.Message);
      }
      return (IEnumerable<AreaResponsable>) source.OrderBy<AreaResponsable, string>((Func<AreaResponsable, string>) (t => t.RESPONSABLE)).ToList<AreaResponsable>();
    }

    public Mensaje Set_Mant_AreaResponsable(
      List<AreaResponsable> Mant_AreaResponsable,
      Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_ResponsablesAreas";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@RAR_ID", SqlDbType.UniqueIdentifier);
            foreach (AreaResponsable areaResponsable in Mant_AreaResponsable.FindAll((Predicate<AreaResponsable>) (t => !t.SELECCIONADO)))
            {
              sqlCommand.Parameters["@RAR_ID"].Value = (object) areaResponsable.RAR_ID;
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
            if (mensaje.errNumber == 0)
            {
              sqlCommand.Parameters.Clear();
              sqlCommand.CommandText = "NEGOCIO.Set_Crear_ResponsablesAreas";
              sqlCommand.Parameters.Add("@RAR_RES_ID", SqlDbType.UniqueIdentifier);
              sqlCommand.Parameters.Add("@RAR_ARE_ID", SqlDbType.UniqueIdentifier);
              sqlCommand.Parameters.Add("@RAR_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
              mensaje.errNumber = 0;
              mensaje.message = str;
              foreach (AreaResponsable areaResponsable in Mant_AreaResponsable.FindAll((Predicate<AreaResponsable>) (t => t.SELECCIONADO)))
              {
                sqlCommand.Parameters["@RAR_RES_ID"].Value = (object) areaResponsable.RES_ID;
                sqlCommand.Parameters["@RAR_ARE_ID"].Value = (object) areaResponsable.RAR_ARE_ID;
                sqlCommand.Parameters["@RAR_USUARIO_CREATE_ID"].Value = (object) Usuario;
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
      }
      catch (SqlException ex)
      {
        mensaje.errNumber = -1;
        mensaje.message = ex.Message;
        this.logger.Error<SqlException>("Sql error en Set_Mant_AreaResponsable: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Mant_AreaResponsable: " + ex.Message);
      }
      if (mensaje.message.Equals("") && mensaje.errNumber == 0)
        mensaje.message = "¡Operación exitosa!";
      return mensaje;
    }

    public Mensaje Set_Crear_Responsable(List<Responsable> NuevaResponsable, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Responsables";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@RES_DOCUMENTO", SqlDbType.VarChar, 60);
            sqlCommand.Parameters.Add("@RES_NOMBRES", SqlDbType.VarChar, 60);
            sqlCommand.Parameters.Add("@RES_APELLIDOS", SqlDbType.VarChar, 60);
            sqlCommand.Parameters.Add("@RES_CARGO", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@RES_CORREO", SqlDbType.VarChar, 200);
            sqlCommand.Parameters.Add("@RES_PHOTO", SqlDbType.VarChar, 200);
            sqlCommand.Parameters.Add("@RES_CELULAR", SqlDbType.VarChar, 20);
            sqlCommand.Parameters.Add("@RES_TELEFONO", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@RES_EXT", SqlDbType.VarChar, 5);
            sqlCommand.Parameters.Add("@RES_OBSERVACION", SqlDbType.VarChar, 200);
            sqlCommand.Parameters.Add("@RES_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@RES_ACTIVE", SqlDbType.Bit);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Responsable responsable in NuevaResponsable)
            {
              sqlCommand.Parameters["@RES_DOCUMENTO"].Value = (object) responsable.RES_DOCUMENTO;
              sqlCommand.Parameters["@RES_NOMBRES"].Value = (object) responsable.RES_NOMBRES;
              sqlCommand.Parameters["@RES_APELLIDOS"].Value = (object) responsable.RES_APELLIDOS;
              sqlCommand.Parameters["@RES_CARGO"].Value = (object) responsable.RES_CARGO;
              sqlCommand.Parameters["@RES_CORREO"].Value = (object) responsable.RES_CORREO;
              sqlCommand.Parameters["@RES_PHOTO"].Value = (object) responsable.RES_PHOTO_1;
              sqlCommand.Parameters["@RES_CELULAR"].Value = (object) responsable.RES_CELULAR;
              sqlCommand.Parameters["@RES_TELEFONO"].Value = (object) responsable.RES_TELEFONO;
              sqlCommand.Parameters["@RES_EXT"].Value = (object) responsable.RES_EXT;
              sqlCommand.Parameters["@RES_OBSERVACION"].Value = (object) responsable.RES_OBSERVACION;
              sqlCommand.Parameters["@RES_USUARIO_CREATE_ID"].Value = (object) Usuario;
              sqlCommand.Parameters["@RES_ACTIVE"].Value = (object) responsable.RES_ACTIVE;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                if (sqlDataReader.Read())
                  responsable.RES_ID = sqlDataReader.GetGuid(0);
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_Responsables:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Responsables:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Responsable(List<Responsable> EditarResponsable, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Responsables";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@RES_DOCUMENTO", SqlDbType.VarChar, 60);
            sqlCommand.Parameters.Add("@RES_NOMBRES", SqlDbType.VarChar, 60);
            sqlCommand.Parameters.Add("@RES_APELLIDOS", SqlDbType.VarChar, 60);
            sqlCommand.Parameters.Add("@RES_CARGO", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@RES_CORREO", SqlDbType.VarChar, 200);
            sqlCommand.Parameters.Add("@RES_PHOTO", SqlDbType.VarChar, 200);
            sqlCommand.Parameters.Add("@RES_CELULAR", SqlDbType.VarChar, 20);
            sqlCommand.Parameters.Add("@RES_TELEFONO", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@RES_EXT", SqlDbType.VarChar, 5);
            sqlCommand.Parameters.Add("@RES_OBSERVACION", SqlDbType.VarChar, 200);
            sqlCommand.Parameters.Add("@RES_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@RES_ACTIVE", SqlDbType.Bit);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Responsable responsable in EditarResponsable)
            {
              sqlCommand.Parameters["@RES_ID"].Value = (object) responsable.RES_ID;
              sqlCommand.Parameters["@RES_DOCUMENTO"].Value = (object) responsable.RES_DOCUMENTO;
              sqlCommand.Parameters["@RES_NOMBRES"].Value = (object) responsable.RES_NOMBRES;
              sqlCommand.Parameters["@RES_APELLIDOS"].Value = (object) responsable.RES_APELLIDOS;
              sqlCommand.Parameters["@RES_CARGO"].Value = (object) responsable.RES_CARGO;
              sqlCommand.Parameters["@RES_CORREO"].Value = (object) responsable.RES_CORREO;
              sqlCommand.Parameters["@RES_PHOTO"].Value = (object) responsable.RES_PHOTO_1;
              sqlCommand.Parameters["@RES_CELULAR"].Value = (object) responsable.RES_CELULAR;
              sqlCommand.Parameters["@RES_TELEFONO"].Value = (object) responsable.RES_TELEFONO;
              sqlCommand.Parameters["@RES_EXT"].Value = (object) responsable.RES_EXT;
              sqlCommand.Parameters["@RES_OBSERVACION"].Value = (object) responsable.RES_OBSERVACION;
              sqlCommand.Parameters["@RES_USUARIO_MOD_ID"].Value = (object) Usuario;
              sqlCommand.Parameters["@RES_ACTIVE"].Value = (object) responsable.RES_ACTIVE;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_Responsables:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_Responsables:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Responsable(List<Guid> EliminarResponsable, Guid Usuario)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_Responsables";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@RES_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarResponsable)
            {
              sqlCommand.Parameters["@RES_ID"].Value = (object) guid;
              using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
              {
                if (sqlDataReader.Read())
                {
                  try
                  {
                    //mensaje.data = (object) sqlDataReader.GetString(0);
                    mensaje.errNumber = sqlDataReader.GetInt32(0);
                    mensaje.message = sqlDataReader.GetString(1);
                    }
                  catch
                  {
                  }
                }
                //sqlDataReader.NextResult();
                //if (sqlDataReader.Read())
                //{
                //  mensaje.errNumber = sqlDataReader.GetInt32(0);
                //  mensaje.message = sqlDataReader.GetString(1);
                //}
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_Responsables:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_Responsables:" + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<Cliente> Get_list_Clientes(Guid? CLI_ID, string CLI_DESC, DateTime? CLI_FECHA_MOD = null)
    {
      List<Cliente> source = new List<Cliente>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Clientes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CLI_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@CLI_FECHA_MOD", SqlDbType.DateTime);
            sqlCommand.Parameters["@CLI_ID"].Value = (object) CLI_ID;
            sqlCommand.Parameters["@CLI_DESC"].Value = (object) CLI_DESC;
            sqlCommand.Parameters["@CLI_FECHA_MOD"].Value = (object)CLI_FECHA_MOD;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Cliente cliente = new Cliente();
                cliente.CLI_ID = sqlDataReader.GetGuid(0);
                cliente.CLI_IDENTIFICACION = sqlDataReader.GetString(1);
                cliente.CLI_CODIGO_SAP = sqlDataReader.GetString(2);
                cliente.CLI_NOMBRE = sqlDataReader.GetString(3);
                try
                {
                  cliente.CLI_DIRECCION = sqlDataReader.GetString(4);
                }
                catch
                {
                }
                try
                {
                  cliente.CLI_CONTACTO_NOMBRE = sqlDataReader.GetString(5);
                }
                catch
                {
                }
                try
                {
                  cliente.CLI_CONTACTO_TELEFONO = sqlDataReader.GetString(6);
                }
                catch
                {
                }
                try
                {
                  cliente.CLI_CONTACTO_CORREO = sqlDataReader.GetString(7);
                }
                catch
                {
                }
                cliente.CLI_ACTIVE = sqlDataReader.GetBoolean(8);
                source.Add(cliente);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_Responsables:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_Responsables:" + ex.Message);
      }
      return (IEnumerable<Cliente>) source.OrderBy<Cliente, string>((Func<Cliente, string>) (t => t.CLI_NOMBRE)).ToList<Cliente>();
    }

    public Mensaje Set_Crear_Clientes(List<Cliente> NuevoCliente, Guid UsuarioCliente)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Clientes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CLI_IDENTIFICACION", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@CLI_CODIGO_SAP", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@CLI_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@CLI_DIRECCION", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CLI_CONTACTO_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@CLI_CONTACTO_TELEFONO", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@CLI_CONTACTO_CORREO", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@CLI_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Cliente cliente in NuevoCliente)
            {
              sqlCommand.Parameters["@CLI_IDENTIFICACION"].Value = (object) cliente.CLI_IDENTIFICACION;
              sqlCommand.Parameters["@CLI_CODIGO_SAP"].Value = (object) cliente.CLI_CODIGO_SAP;
              sqlCommand.Parameters["@CLI_NOMBRE"].Value = (object) cliente.CLI_NOMBRE;
              sqlCommand.Parameters["@CLI_DIRECCION"].Value = (object) cliente.CLI_DIRECCION;
              sqlCommand.Parameters["@CLI_CONTACTO_NOMBRE"].Value = (object) cliente.CLI_CONTACTO_NOMBRE;
              sqlCommand.Parameters["@CLI_CONTACTO_TELEFONO"].Value = (object) cliente.CLI_CONTACTO_TELEFONO;
              sqlCommand.Parameters["@CLI_CONTACTO_CORREO"].Value = (object) cliente.CLI_CONTACTO_CORREO;
              sqlCommand.Parameters["@CLI_USUARIO_CREATE_ID"].Value = (object) UsuarioCliente;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_Clientes:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Clientes:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Clientes(
      List<Cliente> EditarCliente,
      Guid UsuarioClienteEditar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Clientes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CLI_IDENTIFICACION", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@CLI_CODIGO_SAP", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@CLI_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@CLI_DIRECCION", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CLI_CONTACTO_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@CLI_CONTACTO_TELEFONO", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@CLI_CONTACTO_CORREO", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@CLI_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Cliente cliente in EditarCliente)
            {
              sqlCommand.Parameters["@CLI_ID"].Value = (object) cliente.CLI_ID;
              sqlCommand.Parameters["@CLI_IDENTIFICACION"].Value = (object) cliente.CLI_IDENTIFICACION;
              sqlCommand.Parameters["@CLI_CODIGO_SAP"].Value = (object) cliente.CLI_CODIGO_SAP;
              sqlCommand.Parameters["@CLI_NOMBRE"].Value = (object) cliente.CLI_NOMBRE;
              sqlCommand.Parameters["@CLI_DIRECCION"].Value = (object) cliente.CLI_DIRECCION;
              sqlCommand.Parameters["@CLI_CONTACTO_NOMBRE"].Value = (object) cliente.CLI_CONTACTO_NOMBRE;
              sqlCommand.Parameters["@CLI_CONTACTO_TELEFONO"].Value = (object) cliente.CLI_CONTACTO_TELEFONO;
              sqlCommand.Parameters["@CLI_CONTACTO_CORREO"].Value = (object) cliente.CLI_CONTACTO_CORREO;
              sqlCommand.Parameters["@CLI_USUARIO_MOD_ID"].Value = (object) UsuarioClienteEditar;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_Clientes:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_Clientes:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Clientes(
      List<Guid> EliminarCliente,
      Guid UsuarioClienteEliminar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_Clientes";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CLI_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CLI_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarCliente)
            {
              sqlCommand.Parameters["@CLI_ID"].Value = (object) guid;
              sqlCommand.Parameters["@CLI_USUARIO_DELETE_ID"].Value = (object) UsuarioClienteEliminar;
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_Clientes:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_Clientes:" + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<Proveedor> Get_list_Proveedor(
      Guid? PRO_ID,
      string PRO_DESC)
    {
      List<Proveedor> source = new List<Proveedor>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_Proveedores";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@PRO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@PRO_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters["@PRO_ID"].Value = (object) PRO_ID;
            sqlCommand.Parameters["@PRO_NOMBRE"].Value = (object) PRO_DESC;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Proveedor proveedor = new Proveedor();
                proveedor.PRO_ID = new Guid?(sqlDataReader.GetGuid(0));
                try
                {
                  proveedor.PRO_IDENTIFICACION = sqlDataReader.GetString(1);
                }
                catch
                {
                }
                try
                {
                  proveedor.PRO_CODIGO_SAP = sqlDataReader.GetString(2);
                }
                catch
                {
                }
                try
                {
                  proveedor.PRO_NOMBRE = sqlDataReader.GetString(3);
                }
                catch
                {
                }
                try
                {
                  proveedor.PRO_DIRECCION = sqlDataReader.GetString(4);
                }
                catch
                {
                }
                try
                {
                  proveedor.PRO_CONTACTO_NOMBRE = sqlDataReader.GetString(5);
                }
                catch
                {
                }
                try
                {
                  proveedor.PRO_CONTACTO_TELEFONO = sqlDataReader.GetString(6);
                }
                catch
                {
                }
                try
                {
                  proveedor.PRO_CONTACTO_CORREO = sqlDataReader.GetString(7);
                }
                catch
                {
                }
                source.Add(proveedor);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_Proveedores:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_Proveedores:" + ex.Message);
      }
      return (IEnumerable<Proveedor>) source.OrderBy<Proveedor, string>((Func<Proveedor, string>) (t => t.PRO_NOMBRE)).ToList<Proveedor>();
    }

    public Mensaje Set_Crear_Proveedor(
      List<Proveedor> NuevoProveedores,
      Guid UsuarioProveedoresCrear)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Crear_Proveedores";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@PRO_IDENTIFICACION", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@PRO_CODIGO_SAP", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@PRO_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@PRO_DIRECCION", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@PRO_CONTACTO_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@PRO_CONTACTO_TELEFONO", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@PRO_CONTACTO_CORREO", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@PRO_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Proveedor nuevoProveedore in NuevoProveedores)
            {
              sqlCommand.Parameters["@PRO_IDENTIFICACION"].Value = (object) nuevoProveedore.PRO_IDENTIFICACION;
              sqlCommand.Parameters["@PRO_CODIGO_SAP"].Value = (object) nuevoProveedore.PRO_CODIGO_SAP;
              sqlCommand.Parameters["@PRO_NOMBRE"].Value = (object) nuevoProveedore.PRO_NOMBRE;
              sqlCommand.Parameters["@PRO_DIRECCION"].Value = (object) nuevoProveedore.PRO_DIRECCION;
              sqlCommand.Parameters["@PRO_CONTACTO_NOMBRE"].Value = (object) nuevoProveedore.PRO_CONTACTO_NOMBRE;
              sqlCommand.Parameters["@PRO_CONTACTO_TELEFONO"].Value = (object) nuevoProveedore.PRO_CONTACTO_TELEFONO;
              sqlCommand.Parameters["@PRO_CONTACTO_CORREO"].Value = (object) nuevoProveedore.PRO_CONTACTO_CORREO;
              sqlCommand.Parameters["@PRO_USUARIO_CREATE_ID"].Value = (object) UsuarioProveedoresCrear;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_Proveedores:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_Proveedores:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Proveedor(
      List<Proveedor> EditarProveedores,
      Guid UsuarioProveedoresEditar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Editar_Proveedores";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@PRO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@PRO_IDENTIFICACION", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@PRO_CODIGO_SAP", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@PRO_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@PRO_DIRECCION", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@PRO_CONTACTO_NOMBRE", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@PRO_CONTACTO_TELEFONO", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@PRO_CONTACTO_CORREO", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@PRO_ACTIVE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@PRO_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Proveedor editarProveedore in EditarProveedores)
            {
              sqlCommand.Parameters["@PRO_ID"].Value = (object) editarProveedore.PRO_ID;
              sqlCommand.Parameters["@PRO_IDENTIFICACION"].Value = (object) editarProveedore.PRO_IDENTIFICACION;
              sqlCommand.Parameters["@PRO_CODIGO_SAP"].Value = (object) editarProveedore.PRO_CODIGO_SAP;
              sqlCommand.Parameters["@PRO_NOMBRE"].Value = (object) editarProveedore.PRO_NOMBRE;
              sqlCommand.Parameters["@PRO_DIRECCION"].Value = (object) editarProveedore.PRO_DIRECCION;
              sqlCommand.Parameters["@PRO_CONTACTO_NOMBRE"].Value = (object) editarProveedore.PRO_CONTACTO_NOMBRE;
              sqlCommand.Parameters["@PRO_CONTACTO_TELEFONO"].Value = (object) editarProveedore.PRO_CONTACTO_TELEFONO;
              sqlCommand.Parameters["@PRO_CONTACTO_CORREO"].Value = (object) editarProveedore.PRO_CONTACTO_CORREO;
              sqlCommand.Parameters["@PRO_ACTIVE"].Value = (object) true;
              sqlCommand.Parameters["@PRO_USUARIO_MOD_ID"].Value = (object) UsuarioProveedoresEditar;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_Proveedores:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_Proveedores:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Proveedor(
      List<Guid> EliminarProveedores,
      Guid UsuarioProveedoresEliminar)
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
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_Proveedores";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@PRO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@PRO_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid eliminarProveedore in EliminarProveedores)
            {
              sqlCommand.Parameters["@PRO_ID"].Value = (object) eliminarProveedore;
              sqlCommand.Parameters["@PRO_USUARIO_DELETE_ID"].Value = (object) UsuarioProveedoresEliminar;
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_Proveedores:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_Proveedores:" + ex.Message);
      }
      return mensaje;
    }

    public IEnumerable<CentroCosto> Get_list_CentroCosto(
      Guid? CentroCosto_ID,
      string CentroCosto_DESC)
    {
      List<CentroCosto> source = new List<CentroCosto>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "BASE.Get_list_CentroCosto";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CCO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CCO_DESC", SqlDbType.VarChar, 100);
            sqlCommand.Parameters["@CCO_ID"].Value = (object) CentroCosto_ID;
            sqlCommand.Parameters["@CCO_DESC"].Value = (object) CentroCosto_DESC;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                CentroCosto centroCosto = new CentroCosto();
                centroCosto.CCO_ID = sqlDataReader.GetGuid(0);
                centroCosto.CCO_CODIGO = sqlDataReader.GetString(1);
                centroCosto.CCO_DESC = sqlDataReader.GetString(2);
                centroCosto.CCO_ACTIVE = sqlDataReader.GetBoolean(3);
                try
                {
                  centroCosto.CCO_DESC_ALT = sqlDataReader.GetString(4);
                }
                catch
                {
                }
                source.Add(centroCosto);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        this.logger.Error<SqlException>("Sql error en Get_list_CentroCosto:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Get_list_CentroCosto:" + ex.Message);
      }
      return (IEnumerable<CentroCosto>) source.OrderBy<CentroCosto, string>((Func<CentroCosto, string>) (t => t.CCO_DESC)).ToList<CentroCosto>();
    }

    public Mensaje Set_Crear_CentroCosto(
      List<CentroCosto> NuevoCentroCosto,
      Guid UsuarioCentroCostoCrear)
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
            sqlCommand.CommandText = "BASE.Set_Crear_CentroCosto";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CCO_CODIGO", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CCO_DESC", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CCO_DESC_ALT", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CCO_ACTIVE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@CCO_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (CentroCosto centroCosto in NuevoCentroCosto)
            {
              sqlCommand.Parameters["@CCO_CODIGO"].Value = (object) centroCosto.CCO_CODIGO;
              sqlCommand.Parameters["@CCO_DESC"].Value = (object) centroCosto.CCO_DESC;
              sqlCommand.Parameters["@CCO_DESC_ALT"].Value = (object) centroCosto.CCO_DESC_ALT;
              sqlCommand.Parameters["@CCO_USUARIO_CREATE_ID"].Value = (object) UsuarioCentroCostoCrear;
              sqlCommand.Parameters["@CCO_ACTIVE"].Value = (object) centroCosto.CCO_ACTIVE;
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
        this.logger.Error<SqlException>("Sql error en Set_Crear_CentroCosto:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Crear_CentroCosto:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_CentroCosto(
      List<CentroCosto> EditarCentroCosto,
      Guid UsuarioCentroCostoEditar)
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
            sqlCommand.CommandText = "BASE.Set_Editar_CentroCosto";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CCO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CCO_CODIGO", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CCO_DESC", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CCO_DESC_ALT", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@CCO_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CCO_ACTIVE", SqlDbType.Bit);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (CentroCosto centroCosto in EditarCentroCosto)
            {
              sqlCommand.Parameters["@CCO_ID"].Value = (object) centroCosto.CCO_ID;
              sqlCommand.Parameters["@CCO_CODIGO"].Value = (object) centroCosto.CCO_CODIGO;
              sqlCommand.Parameters["@CCO_DESC"].Value = (object) centroCosto.CCO_DESC;
              sqlCommand.Parameters["@CCO_DESC_ALT"].Value = (object) centroCosto.CCO_DESC_ALT;
              sqlCommand.Parameters["@CCO_USUARIO_MOD_ID"].Value = (object) UsuarioCentroCostoEditar;
              sqlCommand.Parameters["@CCO_ACTIVE"].Value = (object) centroCosto.CCO_ACTIVE;
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
        this.logger.Error<SqlException>("Sql error en Set_Editar_CentroCosto:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Editar_CentroCosto:" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_CentroCosto(
      List<Guid> EliminarCentroCosto,
      Guid UsuarioCentroCostoEliminar)
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
            sqlCommand.CommandText = "BASE.Set_Eliminar_CentroCosto";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CCO_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@CCO_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarCentroCosto)
            {
              sqlCommand.Parameters["@CCO_ID"].Value = (object) guid;
              sqlCommand.Parameters["@CCO_USUARIO_DELETE_ID"].Value = (object) UsuarioCentroCostoEliminar;
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
        this.logger.Error<SqlException>("Sql error en Set_Eliminar_CentroCosto:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        this.logger.Error(ex, "Error en Set_Eliminar_CentroCosto:" + ex.Message);
      }
      return mensaje;
    }
  }
}
