// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Factory.SeguridadDataBase
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
  public class SeguridadDataBase : ISeguridad
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();

    public IEnumerable<Usuarios> Get_validar_usuarios(
      string Login,
      string Password)
    {
      List<Usuarios> usuariosList = new List<Usuarios>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "BASE.Get_validar_usuarios";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@USU_LOGIN", SqlDbType.VarChar, 30);
            sqlCommand.Parameters["@USU_LOGIN"].Value = (object) Login;
            sqlCommand.Parameters.Add("@USU_PASS", SqlDbType.VarChar, 250);
            sqlCommand.Parameters["@USU_PASS"].Value = (object) Password;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Usuarios usuarios = new Usuarios();
                usuarios.USU_ID = sqlDataReader.GetGuid(0);
                usuarios.USU_NOMBRE = sqlDataReader.GetString(1);
                usuarios.USU_IDENTIFICACION = sqlDataReader.GetString(2);
                usuarios.USU_CORREO = sqlDataReader.GetString(3);
                usuarios.USU_ROL_ID = sqlDataReader.GetGuid(4);
                usuarios.USU_ROL_DESC = sqlDataReader.GetString(5);
                usuarios.USU_LOGIN = sqlDataReader.GetString(6);
                usuarios.USU_ACTIVE = sqlDataReader.GetBoolean(7);
                try
                {
                  usuarios.USU_RES_ID = sqlDataReader.GetGuid(8);
                  usuarios.USU_NOMBRE_COMPLETO = sqlDataReader.GetString(9);
                }
                catch
                {
                  usuarios.USU_NOMBRE_COMPLETO = "";
                }
                usuariosList.Add(usuarios);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Get_validar_usuarios: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        SeguridadDataBase.logger.Error(ex, "Error en Get_validar_usuarios: " + ex.Message);
      }
      return (IEnumerable<Usuarios>) usuariosList;
    }

    public List<URL> Get_list_permisosMenu(Guid? ROL_ID)
    {
      List<URL> urlList = new List<URL>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "BASE.Get_list_permisosMenu";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ROL_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@ROL_ID"].Value = (object) ROL_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                URL url = new URL();
                url.URL_LINK = sqlDataReader.GetString(0);
                url.URL_DESC = sqlDataReader.GetString(1);
                try
                {
                  url.URL_ICON = sqlDataReader.GetString(2);
                }
                catch
                {
                  url.URL_ICON = "";
                }
                url.URL_ISACTIVE = sqlDataReader.GetBoolean(3);
                url.URL_ID = sqlDataReader.GetGuid(4);
                try
                {
                  url.URL_PADRE = sqlDataReader.GetString(5);
                }
                catch
                {
                  url.URL_PADRE = "";
                }
                try
                {
                  url.URL_URL_PARENT_ID = sqlDataReader.GetGuid(6);
                }
                catch
                {
                }
                url.URL_PRR_ISACTIVE = sqlDataReader.GetBoolean(7);
                urlList.Add(url);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Get_list_permisosMenu: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        SeguridadDataBase.logger.Error(ex, "Error en Get_list_permisosMenu: " + ex.Message);
      }
      return urlList;
    }

    public List<Rol> Get_list_Roles(Guid? ROL_ID_LISTA,bool? Activos)
    {
      List<Rol> source = new List<Rol>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "BASE.Get_list_Roles";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ROL_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@ROL_ID"].Value = (object) ROL_ID_LISTA;
            sqlCommand.Parameters.Add ("@ROL_ACTIVE", SqlDbType.Bit).Value = (object)Activos;

           using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
                source.Add(new Rol()
                {
                  ROL_ID = sqlDataReader.GetGuid(0),
                  ROL_DESC = sqlDataReader.GetString(1),
                  ROL_ACTIVE = sqlDataReader.GetBoolean(2)
                });
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Get_list_Roles: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        SeguridadDataBase.logger.Error(ex, "Error en Get_list_Roles: " + ex.Message);
      }
      return source.OrderBy<Rol, string>((Func<Rol, string>) (t => t.ROL_DESC)).ToList<Rol>();
    }

    public Mensaje Set_Crear_Roles(List<Rol> NuevoRol, Guid Rol_ID_USUARIO)
    {
      Mensaje mensaje = new Mensaje();
      try
      {
        Guid guid = Rol_ID_USUARIO;
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "BASE.Set_Crear_Roles";
            mensaje.data = (object) " Set_Crear_Roles";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ROL_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@ROL_ACTIVE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@ROL_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = -1;
            foreach (Rol rol in NuevoRol)
            {
              sqlCommand.Parameters["@ROL_DESC"].Value = (object) rol.ROL_DESC;
              sqlCommand.Parameters["@ROL_ACTIVE"].Value = (object) rol.ROL_ACTIVE;
              sqlCommand.Parameters["@ROL_USUARIO_CREATE_ID"].Value = (object) Rol_ID_USUARIO;
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
                sqlCommand.CommandText = "BASE.Set_Asignar_Permiso";
                mensaje.data = (object) " Set_Asignar_Permiso";
                sqlCommand.Parameters.Add("@PRR_ROL_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@PRR_PUR_ID", SqlDbType.UniqueIdentifier);
                if (rol.SELECCIONADOS != null && rol.SELECCIONADOS.Length > 0)
                {
                  string seleccionados = rol.SELECCIONADOS;
                  char[] chArray = new char[1]{ '_' };
                  foreach (string input in seleccionados.Split(chArray))
                  {
                    sqlCommand.Parameters["@PRR_ROL_ID"].Value = (object) guid;
                    sqlCommand.Parameters["@PRR_PUR_ID"].Value = (object) Guid.Parse(input);
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
        SeguridadDataBase.logger.Error<SqlException>("Sql error en : " + ex.Message + mensaje.data.ToString(), ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        SeguridadDataBase.logger.Error(ex, "Error en : " + ex.Message + mensaje.data.ToString());
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Roles(List<Rol> EditarRol, Guid Usuario)
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
            sqlCommand.CommandText = "BASE.Set_Editar_Roles";
            mensaje.data = (object) " Set_Editar_Roles";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ROL_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ROL_DESC", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@ROL_ACTIVE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@ROL_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Rol rol in EditarRol)
            {
              sqlCommand.Parameters["@ROL_ID"].Value = (object) rol.ROL_ID;
              sqlCommand.Parameters["@ROL_DESC"].Value = (object) rol.ROL_DESC;
              sqlCommand.Parameters["@ROL_ACTIVE"].Value = (object) rol.ROL_ACTIVE;
              sqlCommand.Parameters["@ROL_USUARIO_MOD_ID"].Value = (object) Usuario;
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
                sqlCommand.CommandText = "BASE.Set_DesAsignar_Permiso";
                mensaje.data = (object) " Set_DesAsignar_Permiso";
                sqlCommand.Parameters.Add("@PRR_ROL_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters.Add("@PRR_PUR_ID", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters["@PRR_ROL_ID"].Value = (object) rol.ROL_ID;
                sqlCommand.Parameters["@PRR_PUR_ID"].Value = (object) rol.ROL_ID;
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "BASE.Set_Asignar_Permiso";
                if (rol.SELECCIONADOS != null)
                {
                  string seleccionados = rol.SELECCIONADOS;
                  char[] chArray = new char[1]{ '_' };
                  foreach (string input in seleccionados.Split(chArray))
                  {
                    sqlCommand.Parameters["@PRR_ROL_ID"].Value = (object) rol.ROL_ID;
                    sqlCommand.Parameters["@PRR_PUR_ID"].Value = (object) Guid.Parse(input);
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
        SeguridadDataBase.logger.Error<SqlException>("Sql error en : " + ex.Message + mensaje.data.ToString(), ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        SeguridadDataBase.logger.Error(ex, "Error en : " + ex.Message + mensaje.data.ToString());
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Roles(List<Guid> EliminarRol, Guid Usuario)
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
            sqlCommand.CommandText = "BASE.Set_Eliminar_Roles";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ROL_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@ROL_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            try
            {
              mensaje.errNumber = 0;
              mensaje.message = str;
              foreach (Guid guid in EliminarRol)
              {
                sqlCommand.Parameters["@ROL_ID"].Value = (object) guid;
                sqlCommand.Parameters["@ROL_USUARIO_DELETE_ID"].Value = (object) Usuario;
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
            catch (SqlException ex)
            {
              mensaje.errNumber = -1;
              mensaje.message = ex.Message;
            }
            sqlConnection.Close();
          }
        }
      }
      catch (SqlException ex)
      {
        mensaje.errNumber = -1;
        mensaje.message = ex.Message;
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Set_Eliminar_Roles: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        SeguridadDataBase.logger.Error(ex, "Error en Set_Eliminar_Roles: " + ex.Message);
      }
      return mensaje;
    }

    public List<URL> Get_list_permisos(Guid? ROL_ID_PERMISO)
    {
      List<URL> urlList = new List<URL>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "BASE.Get_list_permisos";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ROL_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@ROL_ID"].Value = (object) ROL_ID_PERMISO;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                URL url = new URL();
                url.URL_LINK = sqlDataReader.GetString(0);
                url.URL_DESC = sqlDataReader.GetString(1);
                try
                {
                  url.URL_ICON = sqlDataReader.GetString(2);
                }
                catch
                {
                  url.URL_ICON = "";
                }
                url.URL_ISACTIVE = sqlDataReader.GetBoolean(3);
                url.URL_ID = sqlDataReader.GetGuid(4);
                try
                {
                  url.URL_PADRE = sqlDataReader.GetString(5);
                }
                catch
                {
                  url.URL_PADRE = "";
                }
                try
                {
                  url.URL_URL_PARENT_ID = sqlDataReader.GetGuid(6);
                }
                catch
                {
                }
                try
                {
                  url.PUR_ID = sqlDataReader.GetGuid(7);
                }
                catch
                {
                }
                try
                {
                  url.PER_ORDER = sqlDataReader.GetInt32(8);
                }
                catch
                {
                }
                try
                {
                  url.PER_DESC = sqlDataReader.GetString(9);
                }
                catch
                {
                }
                try
                {
                  url.URL_PRR_ISACTIVE = sqlDataReader.GetBoolean(10);
                }
                catch
                {
                }
                try
                {
                  url.PER_ID = sqlDataReader.GetGuid(12);
                }
                catch
                {
                }
                urlList.Add(url);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Get_list_permisos: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        SeguridadDataBase.logger.Error(ex, "Error en Get_list_permisos: " + ex.Message);
      }
      return urlList;
    }

    public IEnumerable<Usuarios> Get_list_Usuarios(Guid? USU_ID)
    {
      List<Usuarios> source = new List<Usuarios>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "BASE.Get_list_Usuarios";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@USU_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@USU_ID"].Value = (object) USU_ID;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Usuarios usuarios = new Usuarios();
                usuarios.USU_ID = sqlDataReader.GetGuid(0);
                usuarios.USU_NOMBRE = sqlDataReader.GetString(1);
                try
                {
                  usuarios.USU_IDENTIFICACION = sqlDataReader.GetString(2);
                }
                catch
                {
                }
                usuarios.USU_CORREO = sqlDataReader.GetString(3);
                try
                {
                  usuarios.USU_ROL_ID = sqlDataReader.GetGuid(4);
                }
                catch
                {
                }
                try
                {
                  usuarios.USU_ROL_DESC = sqlDataReader.GetString(5);
                }
                catch
                {
                  usuarios.USU_ROL_DESC = "";
                }
                usuarios.USU_LOGIN = sqlDataReader.GetString(6);
                usuarios.USU_ACTIVE = sqlDataReader.GetBoolean(7);
                try
                {
                  usuarios.USU_RES_ID = sqlDataReader.GetGuid(8);
                }
                catch
                {
                }
                usuarios.USU_PASS = sqlDataReader.GetString(9);
                try
                {
                  usuarios.USU_NOMBRE_COMPLETO = sqlDataReader.GetString(10);
                }
                catch
                {
                  usuarios.USU_NOMBRE_COMPLETO = "";
                }
                source.Add(usuarios);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Get_list_Usuarios: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        SeguridadDataBase.logger.Error(ex, "Error en Get_list_Usuarios: " + ex.Message);
      }
      return (IEnumerable<Usuarios>) source.OrderBy<Usuarios, string>((Func<Usuarios, string>) (t => t.USU_NOMBRE)).ToList<Usuarios>();
    }

    public Mensaje Set_Crear_Usuarios(List<Usuarios> NuevoUsuario, Guid UsuarioNuevo)
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
            sqlCommand.CommandText = "BASE.Set_Crear_Usuarios";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@USU_NOMBRE", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@USU_IDENTIFICACION", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@USU_CORREO", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@USU_ROL_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@USU_LOGIN", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@USU_PASS", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@usu_ACTIVE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@USU_USUARIO_CREATE_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@USU_RES_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Usuarios usuarios in NuevoUsuario)
            {
              sqlCommand.Parameters["@USU_NOMBRE"].Value = (object) usuarios.USU_NOMBRE;
              sqlCommand.Parameters["@USU_IDENTIFICACION"].Value = (object) usuarios.USU_IDENTIFICACION;
              sqlCommand.Parameters["@USU_CORREO"].Value = (object) usuarios.USU_CORREO;
              sqlCommand.Parameters["@USU_ROL_ID"].Value = (object) usuarios.USU_ROL_ID;
              sqlCommand.Parameters["@USU_LOGIN"].Value = (object) usuarios.USU_LOGIN;
              sqlCommand.Parameters["@USU_PASS"].Value = (object) usuarios.USU_PASS;
              sqlCommand.Parameters["@usu_ACTIVE"].Value = (object) usuarios.USU_ACTIVE;
              sqlCommand.Parameters["@USU_USUARIO_CREATE_ID"].Value = (object) UsuarioNuevo;
              sqlCommand.Parameters["@USU_RES_ID"].Value = (object) usuarios.USU_RES_ID;
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
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Set_Crear_Usuarios:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        SeguridadDataBase.logger.Error(ex, "Error en Set_Crear_Usuarios :" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Editar_Usuarios(List<Usuarios> EditarUsuario, Guid UsuarioEditar)
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
            sqlCommand.CommandText = "BASE.Set_Editar_Usuarios";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@USU_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@USU_NOMBRE", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@USU_IDENTIFICACION", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@USU_CORREO", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@USU_ROL_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@USU_LOGIN", SqlDbType.VarChar, 30);
            sqlCommand.Parameters.Add("@USU_PASS", SqlDbType.VarChar, 250);
            sqlCommand.Parameters.Add("@USU_ACTIVE", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@USU_USUARIO_MOD_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@USU_RES_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Usuarios usuarios in EditarUsuario)
            {
              sqlCommand.Parameters["@USU_ID"].Value = (object) usuarios.USU_ID;
              sqlCommand.Parameters["@USU_NOMBRE"].Value = (object) usuarios.USU_NOMBRE;
              sqlCommand.Parameters["@USU_IDENTIFICACION"].Value = (object) usuarios.USU_IDENTIFICACION;
              sqlCommand.Parameters["@USU_CORREO"].Value = (object) usuarios.USU_CORREO;
              sqlCommand.Parameters["@USU_ROL_ID"].Value = (object) usuarios.USU_ROL_ID;
              sqlCommand.Parameters["@USU_LOGIN"].Value = (object) usuarios.USU_LOGIN;
              sqlCommand.Parameters["@USU_PASS"].Value = (object) usuarios.USU_PASS;
              sqlCommand.Parameters["@USU_ACTIVE"].Value = (object) usuarios.USU_ACTIVE;
              sqlCommand.Parameters["@USU_USUARIO_MOD_ID"].Value = (object) UsuarioEditar;
              sqlCommand.Parameters["@USU_RES_ID"].Value = (object) usuarios.USU_RES_ID;
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
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Set_Editar_Usuarios :" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        SeguridadDataBase.logger.Error(ex, "Error en Set_Editar_Usuarios :" + ex.Message);
      }
      return mensaje;
    }

    public Mensaje Set_Eliminar_Usuarios(List<Guid> EliminarUsuario, Guid UsuarioEliminar)
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
            sqlCommand.CommandText = "BASE.Set_Eliminar_Usuarios";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@USU_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters.Add("@USU_USUARIO_DELETE_ID", SqlDbType.UniqueIdentifier);
            mensaje.errNumber = 0;
            mensaje.message = str;
            foreach (Guid guid in EliminarUsuario)
            {
              sqlCommand.Parameters["@USU_ID"].Value = (object) guid;
              sqlCommand.Parameters["@USU_USUARIO_DELETE_ID"].Value = (object) UsuarioEliminar;
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
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Set_Eliminar_Usuarios:" + ex.Message, ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        SeguridadDataBase.logger.Error(ex, "Error en Set_Eliminar_Usuarios:" + ex.Message);
      }
      return mensaje;
    }

    public List<Areas> Get_list_UsuariosArea(Guid? USU_ID_AREA)
    {
      Mensaje mensaje = new Mensaje();
      List<Areas> areasList = new List<Areas>();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Get_list_UsuariosArea";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@USU_ID", SqlDbType.UniqueIdentifier);
            sqlCommand.Parameters["@USU_ID"].Value = (object) USU_ID_AREA;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                Areas areas = new Areas();
                areas.ARE_ID = sqlDataReader.GetGuid(0);
                areas.ARE_DESC = sqlDataReader.GetString(1);
                try
                {
                  areas.ARE_ARE_PARENT_ID = new Guid?(sqlDataReader.GetGuid(2));
                }
                catch
                {
                }
                try
                {
                  areas.ARE_DESC_PADRE = sqlDataReader.GetString(3);
                }
                catch
                {
                  areas.ARE_DESC_PADRE = "";
                }
                areas.ARE_ACTIVE = sqlDataReader.GetBoolean(12);
                areasList.Add(areas);
              }
              sqlDataReader.Close();
            }
          }
          sqlConnection.Close();
        }
      }
      catch (SqlException ex)
      {
        SeguridadDataBase.logger.Error<SqlException>("Sql error en Get_list_UsuariosArea: " + ex.Message, ex);
      }
      catch (Exception ex)
      {
        SeguridadDataBase.logger.Error(ex, "Error en Get_list_UsuariosArea: " + ex.Message);
      }
      return areasList;
    }

    public Mensaje Set_Asignar_Area_Usuario(List<UsuarioArea> ListaArea, Guid AreaID)
    {
      Mensaje mensaje = new Mensaje();
      try
      {
        string str = "";
        mensaje.errNumber = 0;
        mensaje.message = str;
        using (SqlConnection sqlConnection = new SqlConnection(this.helper.cnx()))
        {
          using (SqlCommand sqlCommand = new SqlCommand())
          {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "NEGOCIO.Set_Eliminar_UsuariosArea";
            mensaje.data = (object) " Set_Eliminar_UsuariosArea";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@USA_USU_ID", SqlDbType.UniqueIdentifier).Value = (object) AreaID;
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
              sqlCommand.Parameters.Add("@USA_ARE_ID", SqlDbType.UniqueIdentifier);
              foreach (UsuarioArea usuarioArea in ListaArea)
              {
                sqlCommand.CommandText = "NEGOCIO.Set_Crear_UsuariosArea";
                mensaje.data = (object) " Set_Crear_UsuariosArea";
                sqlCommand.Parameters["@USA_ARE_ID"].Value = (object) usuarioArea.USA_ARE_ID;
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
        SeguridadDataBase.logger.Error<SqlException>("Sql error en : " + ex.Message + mensaje.data.ToString(), ex);
      }
      catch (Exception ex)
      {
        mensaje.errNumber = -2;
        mensaje.message = ex.Message;
        SeguridadDataBase.logger.Error(ex, "Error en : " + ex.Message + mensaje.data.ToString());
      }
      return mensaje;
    }
  }
}
