using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using WebApiKaeser.Reportes;
using WebApiKaeser.Reportes.RepositorioTableAdapters;

namespace WebApiKaeser
{
    public partial class Visor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.Params["Reporte"] != null)               this.Session["Reporte"] = (object)this.Request.Params["Reporte"];
            if (this.Request.Params["CLI_ID"] != null)                this.Session["CLI_ID"] = (object)this.Request.Params["CLI_ID"];
            if (this.Request.Params["EST_ID"] != null)                this.Session["EST_ID"] = (object)this.Request.Params["EST_ID"];
            if (this.Request.Params["CON_DOCUMENTO"] != null)         this.Session["CON_DOCUMENTO"] = (object)this.Request.Params["CON_DOCUMENTO"];
            if (this.Request.Params["TIPODOC"] != null)               this.Session["TIPODOC"] = (object)this.Request.Params["TIPODOC"];
            if (this.Request.Params["CON_FECHA_INICIO"] != null)      this.Session["CON_FECHA_INICIO"] = (object)this.Request.Params["CON_FECHA_INICIO"];
            if (this.Request.Params["CON_FECHA_FINAL"] != null)       this.Session["CON_FECHA_FINAL"] = (object)this.Request.Params["CON_FECHA_FINAL"];
            if (this.Request.Params["TRA_FECHA_INICIO"] != null)      this.Session["TRA_FECHA_INICIO"] = (object)this.Request.Params["TRA_FECHA_INICIO"];
            if (this.Request.Params["TRA_FECHA_FINAL"] != null)       this.Session["TRA_FECHA_FINAL"] = (object)this.Request.Params["TRA_FECHA_FINAL"];
            if (this.Request.Params["FECHA_INICIO"] != null)          this.Session["FECHA_INICIO"] = (object)this.Request.Params["FECHA_INICIO"];
            if (this.Request.Params["FECHA_FIN"] != null)             this.Session["FECHA_FIN"] = (object)this.Request.Params["FECHA_FIN"];
            if (this.Request.Params["FECHA_INICIAL"] != null)         this.Session["FECHA_INICIAL"] = (object)this.Request.Params["FECHA_INICIAL"];
            if (this.Request.Params["FECHA_FINAL"] != null)           this.Session["FECHA_FINAL"] = (object)this.Request.Params["FECHA_FINAL"];
            if (this.Request.Params["FECHAINICIO"] != null)           this.Session["FECHA_INICIAL"] = (object)this.Request.Params["FECHAINICIO"];
            if (this.Request.Params["FECHAFIN"] != null)              this.Session["FECHA_FINAL"] = (object)this.Request.Params["FECHAFIN"];

            if (this.Request.Params["CON_FECHA_SUSCRIPCION"] != null) this.Session["CON_FECHA_SUSCRIPCION"] = (object)this.Request.Params["CON_FECHA_SUSCRIPCION"];
            if (this.Request.Params["INGRESO"] != null)               this.Session["INGRESO"] = (object)this.Request.Params["INGRESO"];
            if (this.Request.Params["SALIDA"] != null)                this.Session["SALIDA"] = (object)this.Request.Params["SALIDA"];
            if (this.Request.Params["TTR_ID"] != null)                this.Session["TTR_ID"] = (object)this.Request.Params["TTR_ID"];
            if (this.Request.Params["TRA_ID"] != null)                this.Session["TRA_ID"] = (object)this.Request.Params["TRA_ID"];
            if (this.Request.Params["Titulo"] != null)                this.Session["Titulo"] = (object)this.Request.Params["Titulo"];
            if (this.Request.Params["ARE_ID"] != null)                this.Session["ARE_ID"] = (object)this.Request.Params["ARE_ID"];
            if (this.Request.Params["AREA_ID"] != null)                this.Session["AREA_ID"] = (object)this.Request.Params["AREA_ID"];
            if (this.Request.Params["TAC_ID"] != null)                this.Session["TAC_ID"] = (object)this.Request.Params["TAC_ID"];
            if (this.Request.Params["EDA_ID"] != null)                this.Session["EDA_ID"] = (object)this.Request.Params["EDA_ID"];
            if (this.Request.Params["RES_ID"] != null)                this.Session["RES_ID"] = (object)this.Request.Params["RES_ID"];
            if (this.Request.Params["ES_RETORNABLE"] != null)         this.Session["ES_RETORNABLE"] = (object)this.Request.Params["ES_RETORNABLE"];
            if (this.Request.Params["DISPONIBLE"] != null)            this.Session["DISPONIBLE"] = (object)this.Request.Params["DISPONIBLE"];
            if (this.Request.Params["VENCIDOS"] != null)              this.Session["VENCIDOS"] = (object)this.Request.Params["VENCIDOS"];
            if (this.Request.Params["AUD_ID"] != null)                this.Session["AUD_ID"] = (object)this.Request.Params["AUD_ID"];
            //servicio tecnico
            if (this.Request.Params["STE_ID"] != null)                this.Session["STE_ID"] = (object)this.Request.Params["STE_ID"];
            if (this.Request.Params["STE_USUARIO_SOLICITA"] != null)  this.Session["STE_USUARIO_SOLICITA"] = (object)this.Request.Params["STE_USUARIO_SOLICITA"];
            if (this.Request.Params["STE_STE_ID"] != null)            this.Session["STE_STE_ID"] = (object)this.Request.Params["STE_STE_ID"];            
            if (this.Request.Params["PARAMETROCONSULTAFECHA"] != null)this.Session["PARAMETROCONSULTAFECHA"] = (object)this.Request.Params["PARAMETROCONSULTAFECHA"];
            if (this.Request.Params["STE_TRA_ID"] != null)            this.Session["STE_TRA_ID"] = (object)this.Request.Params["STE_TRA_ID"];
            if (this.Request.Params["STE_ACT_ID"] != null)            this.Session["STE_ACT_ID"] = (object)this.Request.Params["STE_ACT_ID"];
           
            if (!this.IsPostBack)
                return;
            if (this.Session["Reporte"] == null) return;
            if (this.Session["Reporte"].ToString().Equals("AREAS"))
            {
                Repositorio repositorio = new Repositorio();
                XtraArea xtraArea = new XtraArea();
                Get_list_AreaTableAdapter areaTableAdapter = new Get_list_AreaTableAdapter();
                areaTableAdapter.GetData(new Guid?(), new Guid?(), new Guid?(), null);
                xtraArea.DataAdapter = (object)areaTableAdapter;
                xtraArea.DataSource = (object)repositorio.Get_list_Area;
                xtraArea.DataMember = repositorio.Get_list_Area.TableName;
                xtraArea.DisplayName = "Reporte de Áreas";
                xtraArea.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraArea;
            }
            else if (this.Session["Reporte"].ToString().Equals("CARACTERISTICAS"))
            {
                Repositorio repositorio = new Repositorio();
                XtraCaracteristica xtraCaracteristica = new XtraCaracteristica();
                Get_list_caracteristicaTableAdapter caracteristicaTableAdapter = new Get_list_caracteristicaTableAdapter();
                caracteristicaTableAdapter.GetData(new Guid?());
                xtraCaracteristica.DataAdapter = (object)caracteristicaTableAdapter;
                xtraCaracteristica.DataSource = (object)repositorio.Get_list_caracteristica;
                xtraCaracteristica.DataMember = repositorio.Get_list_caracteristica.TableName;
                xtraCaracteristica.DisplayName = "Reporte de Características";
                xtraCaracteristica.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraCaracteristica;
            }
            else if (this.Session["Reporte"].ToString().Equals("TIPOACTIVO"))
            {
                Repositorio repositorio = new Repositorio();
                XtraTipoDeActivo xtraTipoDeActivo = new XtraTipoDeActivo();
                Get_list_TipoActivoTableAdapter activoTableAdapter = new Get_list_TipoActivoTableAdapter();
                activoTableAdapter.GetData(new Guid?());
                xtraTipoDeActivo.DataAdapter = (object)activoTableAdapter;
                xtraTipoDeActivo.DataSource = (object)repositorio.Get_list_TipoActivo;
                xtraTipoDeActivo.DataMember = repositorio.Get_list_TipoActivo.TableName;
                xtraTipoDeActivo.DisplayName = "Reporte de Tipo de Activos";
                xtraTipoDeActivo.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraTipoDeActivo;
            }
            else if (this.Session["Reporte"].ToString().Equals("MODELOS"))
            {
                Repositorio repositorio = new Repositorio();
                XtraModelo xtraModelo = new XtraModelo();
                Get_list_ModeloTableAdapter modeloTableAdapter = new Get_list_ModeloTableAdapter();
                modeloTableAdapter.GetData(new Guid?());
                xtraModelo.DataAdapter = (object)modeloTableAdapter;
                xtraModelo.DataSource = (object)repositorio.Get_list_Modelo;
                xtraModelo.DataMember = repositorio.Get_list_Modelo.TableName;
                xtraModelo.DisplayName = "Reporte de Modelos";
                xtraModelo.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraModelo;
            }
            else if (this.Session["Reporte"].ToString().Equals("ACTIVOS"))
            {
                Repositorio repositorio = new Repositorio();
                XtraActivos xtraActivos = new XtraActivos();
                Get_list_ActivosTableAdapter activosTableAdapter = new Get_list_ActivosTableAdapter();
                activosTableAdapter.GetData(new Guid?(), (string)null, new Guid?());
                xtraActivos.DataAdapter = (object)activosTableAdapter;
                xtraActivos.DataSource = (object)repositorio.Get_list_Activos;
                xtraActivos.DataMember = repositorio.Get_list_Activos.TableName;
                xtraActivos.DisplayName = "Reporte de Activos";
                xtraActivos.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraActivos;
            }
            else if (this.Session["Reporte"].ToString().Equals("CLIENTES"))
            {
                Repositorio repositorio = new Repositorio();
                XtraClientes xtraClientes = new XtraClientes();
                Get_list_ClientesTableAdapter clientesTableAdapter = new Get_list_ClientesTableAdapter();
                clientesTableAdapter.GetData(new Guid?(), (string)null, (string)null, (string)null, (string)null, (string)null, (string)null, (string)null);
                xtraClientes.DataAdapter = (object)clientesTableAdapter;
                xtraClientes.DataSource = (object)repositorio.Get_list_Clientes;
                xtraClientes.DataMember = repositorio.Get_list_Clientes.TableName;
                xtraClientes.DisplayName = "Reporte de Clientes";
                xtraClientes.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraClientes;
            }
            else if (this.Session["Reporte"].ToString().Equals("CONTRATOS"))
            {
                Repositorio repositorio = new Repositorio();
                XtraContratos xtraContratos = new XtraContratos();
                Get_list_ContratosTableAdapter contratosTableAdapter = new Get_list_ContratosTableAdapter();
                WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
                string CON_NUMERO_DOC = (string)null;
                string CON_DOCUMENTO_SAP = (string)null;
                DateTime? CON_FECHA_SUSCRIPCION = helper.Fecha(this.Session["CON_FECHA_SUSCRIPCION"].ToString());
                DateTime? CON_FECHA_INICIO = helper.Fecha(this.Session["CON_FECHA_INICIO"].ToString());
                DateTime? CON_FECHA_FINAL = helper.Fecha(this.Session["CON_FECHA_FINAL"].ToString());
                if (this.Session["TIPODOC"].ToString() == "1")
                    CON_NUMERO_DOC = this.Session["CON_DOCUMENTO"].ToString();
                else if (this.Session["TIPODOC"].ToString() == "2")
                    CON_DOCUMENTO_SAP = this.Session["CON_DOCUMENTO"].ToString();
                Guid? CLI_ID = new Guid?();
                if (this.Session["CLI_ID"] != null && this.Session["CLI_ID"].ToString() != "")
                    CLI_ID = new Guid?(Guid.Parse(this.Session["CLI_ID"].ToString()));
                Guid? EST_ID = new Guid?();
                if (this.Session["EST_ID"] != null && this.Session["EST_ID"].ToString() != "")
                    EST_ID = new Guid?(Guid.Parse(this.Session["EST_ID"].ToString()));
                contratosTableAdapter.GetData(new Guid?(), (string)null, CON_FECHA_SUSCRIPCION, CON_FECHA_INICIO, CON_FECHA_FINAL, (string)null, (string)null, CLI_ID, CON_DOCUMENTO_SAP, EST_ID, CON_NUMERO_DOC);
                xtraContratos.DataAdapter = (object)contratosTableAdapter;
                xtraContratos.DataSource = (object)repositorio.Get_list_Contratos;
                xtraContratos.DataMember = repositorio.Get_list_Contratos.TableName;
                xtraContratos.DisplayName = "Reporte de Contratos";
                xtraContratos.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraContratos;
            }
            else if (this.Session["Reporte"].ToString().Equals("PROVEEDORES"))
            {
                Repositorio repositorio = new Repositorio();
                XtraProveedor xtraProveedor = new XtraProveedor();
                Get_list_ProveedoresTableAdapter proveedoresTableAdapter = new Get_list_ProveedoresTableAdapter();
                proveedoresTableAdapter.GetData(new Guid?(), (string)null, (string)null, (string)null, (string)null, (string)null, (string)null, (string)null);
                xtraProveedor.DataAdapter = (object)proveedoresTableAdapter;
                xtraProveedor.DataSource = (object)repositorio.Get_list_Proveedores;
                xtraProveedor.DataMember = repositorio.Get_list_Proveedores.TableName;
                xtraProveedor.DisplayName = "Reporte de Proveedores";
                xtraProveedor.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraProveedor;
            }
            else if (this.Session["Reporte"].ToString().Equals("CENTROSCOSTOS"))
            {
                Repositorio repositorio = new Repositorio();
                XtraCentroCosto xtraCentroCosto = new XtraCentroCosto();
                Get_list_CentroCostoTableAdapter costoTableAdapter = new Get_list_CentroCostoTableAdapter();
                costoTableAdapter.GetData(new Guid?(), (string)null, (string)null, (string)null);
                xtraCentroCosto.DataAdapter = (object)costoTableAdapter;
                xtraCentroCosto.DataSource = (object)repositorio.Get_list_CentroCosto;
                xtraCentroCosto.DataMember = repositorio.Get_list_CentroCosto.TableName;
                xtraCentroCosto.DisplayName = "Reporte de Centro de Costos";
                xtraCentroCosto.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraCentroCosto;
            }
            else if (this.Session["Reporte"].ToString().Equals("ROLES"))
            {
                Repositorio repositorio = new Repositorio();
                XtraRoles xtraRoles = new XtraRoles();
                Get_list_RolesTableAdapter rolesTableAdapter = new Get_list_RolesTableAdapter();
                rolesTableAdapter.GetData(new Guid?());
                xtraRoles.DataAdapter = (object)rolesTableAdapter;
                xtraRoles.DataSource = (object)repositorio.Get_list_Roles;
                xtraRoles.DataMember = repositorio.Get_list_Roles.TableName;
                xtraRoles.DisplayName = "Reporte de Roles";
                xtraRoles.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraRoles;
            }
            else if (this.Session["Reporte"].ToString().Equals("USUARIOS"))
            {
                Repositorio repositorio = new Repositorio();
                XtraUsuarios xtraUsuarios = new XtraUsuarios();
                Get_list_UsuariosTableAdapter usuariosTableAdapter = new Get_list_UsuariosTableAdapter();
                usuariosTableAdapter.GetData(new Guid?());
                xtraUsuarios.DataAdapter = (object)usuariosTableAdapter;
                xtraUsuarios.DataSource = (object)repositorio.Get_list_Usuarios;
                xtraUsuarios.DataMember = repositorio.Get_list_Usuarios.TableName;
                xtraUsuarios.DisplayName = "Reporte de Usuarios";
                xtraUsuarios.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraUsuarios;
            }
            else if (this.Session["Reporte"].ToString().Equals("RESPONSABLES"))
            {
                Repositorio repositorio = new Repositorio();
                XtraResponsables xtraResponsables = new XtraResponsables();
                Get_list_ResponsablesTableAdapter responsablesTableAdapter = new Get_list_ResponsablesTableAdapter();
                responsablesTableAdapter.GetData(new Guid?(), (string)null, new Guid?());
                xtraResponsables.DataAdapter = (object)responsablesTableAdapter;
                xtraResponsables.DataSource = (object)repositorio.Get_list_Responsables;
                xtraResponsables.DataMember = repositorio.Get_list_Responsables.TableName;
                xtraResponsables.DisplayName = "Reporte de Responsables";
                xtraResponsables.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraResponsables;
            }
            else if (this.Session["Reporte"].ToString().Equals("TRANSACCIONES"))
            {
                Repositorio repositorio = new Repositorio();
                Get_list_TransaccionesTableAdapter transaccionesTableAdapter = new Get_list_TransaccionesTableAdapter();
                WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
                string str = "";
                string CON_NUMERO_DOC = (string)null;
                string TRA_DOCUMENTO_SAP = (string)null;
                bool? TTR_ES_SALIDA = new bool?();
                bool? TTR_ES_ENTRADA = new bool?();
                bool? TTR_ES_TRASLADO = new bool?();
                bool? TTR_ES_ASIGNACION = new bool?(false);
                DateTime? TRA_FECHA_INICIO = null;
                DateTime? TRA_FECHA_FINAL = null;
                if (Session["TRA_FECHA_INICIO"] != null) TRA_FECHA_INICIO = helper.Fecha(this.Session["TRA_FECHA_INICIO"].ToString());
                if (Session["TRA_FECHA_FINAL"] != null) TRA_FECHA_FINAL = helper.Fecha(this.Session["TRA_FECHA_FINAL"].ToString());
                if (this.Session["TIPODOC"].ToString() == "1")
                    CON_NUMERO_DOC = this.Session["CON_DOCUMENTO"].ToString();
                else if (this.Session["TIPODOC"].ToString() == "2")
                    TRA_DOCUMENTO_SAP = this.Session["CON_DOCUMENTO"].ToString();
                if (this.Session["INGRESO"].ToString().Equals("1"))
                {
                    TTR_ES_ENTRADA = new bool?(true);
                    str = "LISTADO DE INGRESOS";
                }
                else if (this.Session["INGRESO"].ToString().Equals("2"))
                {
                    TTR_ES_SALIDA = new bool?(true);
                    str = "LISTADO DE SALIDAS";
                }
                else if (this.Session["INGRESO"].ToString().Equals("3"))
                {
                    TTR_ES_TRASLADO = new bool?(true);
                    str = "LISTADO DE TRASLADOS";
                }
                else if (this.Session["INGRESO"].ToString().Equals("4"))
                {
                    TTR_ES_ASIGNACION = new bool?(true);
                    str = "LISTADO DE ASIGNACIONES";
                }
                Guid? CLI_ID = new Guid?();
                if (this.Session["CLI_ID"] != null && this.Session["CLI_ID"].ToString() != "")
                    CLI_ID = new Guid?(Guid.Parse(this.Session["CLI_ID"].ToString()));
                Guid? EST_ID = new Guid?();
                if (this.Session["EST_ID"] != null && this.Session["EST_ID"].ToString() != "")
                    EST_ID = new Guid?(Guid.Parse(this.Session["EST_ID"].ToString()));
                Guid? TTR_ID = new Guid?();
                if (this.Session["TTR_ID"] != null && this.Session["TTR_ID"].ToString() != "")
                    TTR_ID = new Guid?(Guid.Parse(this.Session["TTR_ID"].ToString()));

                transaccionesTableAdapter.GetData(new Guid?(), new Guid?(), TTR_ID, EST_ID, CLI_ID, TRA_DOCUMENTO_SAP, CON_NUMERO_DOC, TRA_FECHA_INICIO, TRA_FECHA_FINAL, TTR_ES_SALIDA, TTR_ES_ENTRADA, TTR_ES_TRASLADO, TTR_ES_ASIGNACION, new Guid?(), new Guid?(), (string)null, new Guid?());
                if (this.Session["INGRESO"].ToString().Equals("3"))
                {
                    XtraTraslado xtraTransaccion = new XtraTraslado();
                    xtraTransaccion.Parameters["Titulo"].Value = (object)str;
                    xtraTransaccion.DataAdapter = (object)transaccionesTableAdapter;
                    xtraTransaccion.DataSource = (object)repositorio.Get_list_Transacciones;
                    xtraTransaccion.DataMember = repositorio.Get_list_Transacciones.TableName;
                    xtraTransaccion.DisplayName = str;
                    xtraTransaccion.CreateDocument();
                    this.ASPxDocumentViewer1.Report = (XtraReport)xtraTransaccion;
                }
                else if (this.Session["INGRESO"].ToString().Equals("4"))
                {
                    XtraAsignaciones xtraTransaccion = new XtraAsignaciones();
                    xtraTransaccion.Parameters["Titulo"].Value = (object)str;
                    xtraTransaccion.DataAdapter = (object)transaccionesTableAdapter;
                    xtraTransaccion.DataSource = (object)repositorio.Get_list_Transacciones;
                    xtraTransaccion.DataMember = repositorio.Get_list_Transacciones.TableName;
                    xtraTransaccion.DisplayName = str;
                    xtraTransaccion.CreateDocument();
                    this.ASPxDocumentViewer1.Report = (XtraReport)xtraTransaccion;
                }
                else
                {
                    XtraTransaccion xtraTransaccion = new XtraTransaccion();
                    xtraTransaccion.Parameters["Titulo"].Value = (object)str;
                    xtraTransaccion.DataAdapter = (object)transaccionesTableAdapter;
                    xtraTransaccion.DataSource = (object)repositorio.Get_list_Transacciones;
                    xtraTransaccion.DataMember = repositorio.Get_list_Transacciones.TableName;
                    xtraTransaccion.DisplayName = str;
                    xtraTransaccion.CreateDocument();
                    this.ASPxDocumentViewer1.Report = (XtraReport)xtraTransaccion;
                }
            }
            else if (this.Session["Reporte"].ToString().Equals("ACTA"))
            {
                Repositorio repositorio = new Repositorio();
                XtraActas xtraActas = new XtraActas();
                Get_list_TransaccionesTableAdapter transaccionesTableAdapter1 = new Get_list_TransaccionesTableAdapter();
                Get_list_DetalleTransaccionesTableAdapter transaccionesTableAdapter2 = new Get_list_DetalleTransaccionesTableAdapter();
                Get_list_activosValorTableAdapter valorTableAdapter = new Get_list_activosValorTableAdapter();
                Get_list_ActivosImagenesTableAdapter imagenesTableAdapter = new Get_list_ActivosImagenesTableAdapter();
                WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
                string tituloReporte = "";
                string CON_NUMERO_DOC = (string)null;
                string TRA_DOCUMENTO_SAP = (string)null;
                bool? TTR_ES_SALIDA = new bool?();
                bool? TTR_ES_ENTRADA = new bool?();
                bool? TTR_ES_TRASLADO = new bool?();
                bool? TTR_ES_ASIGNACION = new bool?(false);
                Guid? CON_ID = new Guid?();
                Guid? TTR_ID = new Guid?();
                Guid? EST_ID = new Guid?();
                Guid? CLI_ID = new Guid?();
                DateTime? TRA_FECHA_INICIO = new DateTime?();
                DateTime? TRA_FECHA_FINAL = new DateTime?();
                Guid? TRA_AREA_ORIGEN_ID = new Guid?();
                Guid? TRA_AREA_DESTINO_ID = new Guid?();
                Guid? TRA_RES_ID = new Guid?();

                if (this.Session["INGRESO"].ToString().Equals("1"))
                {
                    TTR_ES_ENTRADA = new bool?(true);
                    tituloReporte = "ACTA DE INGRESOS";
                }
                else if (this.Session["INGRESO"].ToString().Equals("2"))
                {
                    TTR_ES_SALIDA = new bool?(true);
                    tituloReporte = "ACTA DE SALIDAS";
                }
                else if (this.Session["INGRESO"].ToString().Equals("3"))
                {
                    TTR_ES_TRASLADO = new bool?(true);
                    tituloReporte = "ACTA DE TRASLADOS";
                }
                else if (this.Session["INGRESO"].ToString().Equals("4"))
                {
                    TTR_ES_ASIGNACION = new bool?(true);
                    tituloReporte = "ACTA DE ASIGNACIONES";
                }
                Guid? TRA_ID = new Guid?();
                if (this.Session["TRA_ID"] != null && this.Session["TRA_ID"].ToString() != "")
                    TRA_ID = new Guid?(Guid.Parse(this.Session["TRA_ID"].ToString()));
                string reporteTitulo = string.Format("{0} POR {1}", tituloReporte, Session["Titulo"].ToString().ToUpper());
                xtraActas.Parameters["TITULO"].Value = reporteTitulo;
                string appSetting = ConfigurationManager.AppSettings["FileUploadImgLocation"];
                int index = -1;

                foreach (DataRow row in transaccionesTableAdapter1.GetData(TRA_ID, CON_ID, TTR_ID, EST_ID, CLI_ID, TRA_DOCUMENTO_SAP, CON_NUMERO_DOC, TRA_FECHA_INICIO, TRA_FECHA_FINAL, TTR_ES_SALIDA, TTR_ES_ENTRADA, TTR_ES_TRASLADO, TTR_ES_ASIGNACION, TRA_AREA_ORIGEN_ID, TRA_AREA_DESTINO_ID, null, TRA_RES_ID).Rows)
                    repositorio.Tables[repositorio.Get_list_Transacciones.TableName].ImportRow(row);

                foreach (DataRow row1 in transaccionesTableAdapter2.GetData(TRA_ID).Rows)
                {
                    repositorio.Tables[repositorio.Get_list_DetalleTransacciones.TableName].ImportRow(row1);
                    foreach (DataRow row2 in valorTableAdapter.GetData(new Guid?(Guid.Parse(row1["ACT_MOD_ID"].ToString())), new Guid?(Guid.Parse(row1["ACT_ID"].ToString()))).Rows)
                    {
                        row2["ACT_ID"] = (object)Guid.Parse(row1["ACT_ID"].ToString());
                        repositorio.Tables[repositorio.Get_list_activosValor.TableName].ImportRow(row2);
                    }
                    int num = 1;
                    foreach (DataRow row2 in (InternalDataCollectionBase)imagenesTableAdapter.GetData(new Guid?(Guid.Parse(row1["ACT_ID"].ToString())), TRA_ID, new Guid?()).Rows)
                    {
                        row2["AIM_RUTA"] = (object)HostingEnvironment.MapPath(appSetting + "//" + row2["AIM_RUTA"]);
                        if (File.Exists(row2["AIM_RUTA"].ToString()))
                        {
                            FileStream fileStream = new FileStream(row2["AIM_RUTA"].ToString(), FileMode.Open);
                            try
                            {
                                Image image = Image.FromStream((Stream)fileStream);
                                MemoryStream memoryStream1 = new MemoryStream();
                                MemoryStream memoryStream2 = memoryStream1;
                                ImageFormat jpeg = ImageFormat.Jpeg;
                                image.Save((Stream)memoryStream2, jpeg);
                                row2["IMAGEN"] = (object)memoryStream1.ToArray();
                            }
                            catch (Exception ex)
                            {
                            }
                            finally
                            {
                                fileStream.Close();
                                fileStream.Dispose();
                            }
                            switch (num % 4)
                            {
                                case 0:
                                    repositorio.Tables[repositorio.Get_list_ActivosImagenes.TableName].Rows[index]["IMAGEN3"] = row2["IMAGEN"];
                                    break;
                                case 1:
                                    repositorio.Tables[repositorio.Get_list_ActivosImagenes.TableName].ImportRow(row2);
                                    ++index;
                                    break;
                                case 2:
                                    repositorio.Tables[repositorio.Get_list_ActivosImagenes.TableName].Rows[index]["IMAGEN1"] = row2["IMAGEN"];
                                    break;
                                case 3:
                                    repositorio.Tables[repositorio.Get_list_ActivosImagenes.TableName].Rows[index]["IMAGEN2"] = row2["IMAGEN"];
                                    break;
                            }
                            ++num;
                        }
                    }
                }
                foreach (DataRow row in (InternalDataCollectionBase)imagenesTableAdapter.GetData(new Guid?(), TRA_ID, new Guid?()).Rows)
                {
                    row["AIM_RUTA"] = (object)HostingEnvironment.MapPath(appSetting + "//" + row["AIM_RUTA"]);
                    FileStream fileStream = new FileStream(row["AIM_RUTA"].ToString(), FileMode.Open);
                    try
                    {
                        Image image = Image.FromStream((Stream)fileStream);
                        MemoryStream memoryStream1 = new MemoryStream();
                        MemoryStream memoryStream2 = memoryStream1;
                        ImageFormat jpeg = ImageFormat.Jpeg;
                        image.Save((Stream)memoryStream2, jpeg);
                        repositorio.Tables[repositorio.Get_list_Transacciones.TableName].Rows[0]["FIRMA"] = (object)memoryStream1.ToArray();
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                    }
                }
                xtraActas.DataSource = (object)repositorio;
                xtraActas.pcbCabecera.ImageUrl = HostingEnvironment.MapPath(appSetting + "//encabezado_acta_kaeser_2.jpg");
                xtraActas.DisplayName = reporteTitulo;
                xtraActas.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraActas;
            }
            else
                if (this.Session["Reporte"].ToString().Equals("SERTEC"))
            {
                Repositorio repositorio = new Repositorio();
                XtraServicioTecnico xtraServicioTecnico = new XtraServicioTecnico();

                WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
                string PARAMETROCONSULTAFECHA = "0";
                DateTime? FECHA_INICIO = helper.Fecha(this.Session["FECHA_INICIO"].ToString());
                DateTime? FECHA_FINAL = helper.Fecha(this.Session["FECHA_FIN"].ToString());
                PARAMETROCONSULTAFECHA = this.Session["PARAMETROCONSULTAFECHA"].ToString();

                Guid? STE_USUARIO_SOLICITA = new Guid?();
                Guid? STE_ID = new Guid?();
                Guid? STE_STE_ID = new Guid?();
                Guid? STE_TRA_ID = new Guid?();
                Guid? STE_ACT_ID = new Guid?();
                if (this.Session["STE_USUARIO_SOLICITA"] != null && this.Session["STE_USUARIO_SOLICITA"].ToString() != "")
                    STE_USUARIO_SOLICITA = new Guid?(Guid.Parse(this.Session["STE_USUARIO_SOLICITA"].ToString()));

                if (this.Session["STE_ID"] != null && this.Session["STE_ID"].ToString() != "")
                    STE_ID = new Guid?(Guid.Parse(this.Session["STE_ID"].ToString()));

                if (this.Session["STE_STE_ID"] != null && this.Session["STE_STE_ID"].ToString() != "")
                    STE_STE_ID = new Guid?(Guid.Parse(this.Session["STE_STE_ID"].ToString()));

                if (this.Session["STE_TRA_ID"] != null && this.Session["STE_TRA_ID"].ToString() != "")
                    STE_TRA_ID = new Guid?(Guid.Parse(this.Session["STE_TRA_ID"].ToString()));

                if (this.Session["STE_ACT_ID"] != null && this.Session["STE_ACT_ID"].ToString() != "")
                    STE_ACT_ID = new Guid?(Guid.Parse(this.Session["STE_ACT_ID"].ToString()));

                Factory.ServicioTecnicoDataBase datos = new Factory.ServicioTecnicoDataBase();
                foreach (Models.ServicioTecnico item in datos.Get_list_ServicioTecnico(STE_USUARIO_SOLICITA, STE_STE_ID, FECHA_INICIO, FECHA_FINAL, Convert.ToInt16(PARAMETROCONSULTAFECHA), STE_ID, STE_TRA_ID, STE_ACT_ID))
                {
                    DataRow fila = repositorio.Get_list_ServicioTecnico.NewRow();
                    fila["STE_ID"] = item.STE_ID;
                    fila["TAC_DESC"] = item.TAC_DESC;
                    fila["MOD_DESC"] = item.MOD_DESC;
                    fila["ACT_DESC"] = item.ACT_DESC;
                    fila["ACT_Serial"] = item.ACT_SERIAL;
                    if (item.STE_INTERNO_EXTERNO) fila["SOLI_NOMBRE"] = item.CLI_NOMBRE;
                    else fila["SOLI_NOMBRE"] = item.PRO_NOMBRE;
                    fila["USUARIO_SOLICITA"] = item.USUARIO_SOLICITA;
                    fila["estado_desc"] = item.STE_DESCRIPCION;
                    fila["STE_FECHA_SOLCITUD"] = item.STE_FECHA_SOLICITUD_STR;
                    fila["STE_FECHA_INCIO"] = item.STE_FECHA_INICIO_STR;
                    fila["STE_FECHA_FINZALIZA"] = item.STE_FECHA_FINALIZA_STR;
                    repositorio.Get_list_ServicioTecnico.Rows.Add(fila);
                }
                xtraServicioTecnico.DataSource = (object)repositorio.Get_list_ServicioTecnico;
                xtraServicioTecnico.DataMember = repositorio.Get_list_ServicioTecnico.TableName;
                xtraServicioTecnico.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraServicioTecnico;
            }

            else if (this.Session["Reporte"].ToString().Equals("INFORMEACTIVOS"))
            {
                Repositorio repositorio = new Repositorio();
                XtraInformeActivos xtraInformeActivos = new XtraInformeActivos();
                Get_list_Informe_activosTableAdapter responsablesTableAdapter = new Get_list_Informe_activosTableAdapter();
                Guid? AREA_ID = new Guid?();
                if (this.Session["AREA_ID"] != null && this.Session["AREA_ID"].ToString() != "") AREA_ID = new Guid?(Guid.Parse(this.Session["AREA_ID"].ToString()));
                Guid? TAC_ID = new Guid?();
                if (this.Session["TAC_ID"] != null && this.Session["TAC_ID"].ToString() != "") TAC_ID = new Guid?(Guid.Parse(this.Session["TAC_ID"].ToString()));
                Guid? EDA_ID = new Guid?();
                if (this.Session["EDA_ID"] != null && this.Session["EDA_ID"].ToString() != "") EDA_ID = new Guid?(Guid.Parse(this.Session["EDA_ID"].ToString()));
                Guid? RES_ID = new Guid?();
                if (this.Session["RES_ID"] != null && this.Session["RES_ID"].ToString() != "") RES_ID = new Guid?(Guid.Parse(this.Session["RES_ID"].ToString()));
                bool? ES_RETORNABLE = false;
                if (this.Session["ES_RETORNABLE"] != null && this.Session["ES_RETORNABLE"].ToString() != "") ES_RETORNABLE = Convert.ToBoolean(this.Session["ES_RETORNABLE"]);
                WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
                DateTime? FECHA_INICIO = helper.Fecha(this.Session["INGRESO"].ToString());
                DateTime? FECHA_FINAL = helper.Fecha(this.Session["SALIDA"].ToString());
                responsablesTableAdapter.GetData(AREA_ID, TAC_ID, EDA_ID, RES_ID, ES_RETORNABLE, FECHA_INICIO, FECHA_FINAL);
                xtraInformeActivos.DataAdapter = (object)responsablesTableAdapter;
                xtraInformeActivos.DataSource = (object)repositorio.Get_list_Informe_activos;
                xtraInformeActivos.DataMember = repositorio.Get_list_Informe_activos.TableName;
                xtraInformeActivos.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraInformeActivos;
            }
            else if (this.Session["Reporte"].ToString().Equals("INFORMEETIQUETA"))
            {
                Repositorio repositorio = new Repositorio();
                XtraInformeEtiqueta xtraInformeEtiqueta = new XtraInformeEtiqueta();
                Get_list_Informes_EtiquetasTableAdapter responsablesTableAdapter = new Get_list_Informes_EtiquetasTableAdapter();
                bool? DISPONIBLE = false;
                if (this.Session["DISPONIBLE"] != null && this.Session["DISPONIBLE"].ToString() != "") DISPONIBLE = Convert.ToBoolean(this.Session["DISPONIBLE"]);
                responsablesTableAdapter.GetData(DISPONIBLE);
                xtraInformeEtiqueta.DataAdapter = (object)responsablesTableAdapter;
                xtraInformeEtiqueta.DataSource = (object)repositorio.Get_list_Informes_Etiquetas;
                xtraInformeEtiqueta.DataMember = repositorio.Get_list_Informes_Etiquetas.TableName;
                xtraInformeEtiqueta.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraInformeEtiqueta;
            }
            else if (this.Session["Reporte"].ToString().Equals("INFORMEPRESTAMO"))
            {
                Repositorio repositorio = new Repositorio();
                XtraInformePrestamo xtraInformePrestamo = new XtraInformePrestamo();

                WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
                Guid? TAC_ID = new Guid?();
                if (this.Session["TAC_ID"] != null && this.Session["TAC_ID"].ToString() != "") TAC_ID = new Guid?(Guid.Parse(this.Session["TAC_ID"].ToString()));
                Guid? RES_ID = new Guid?();
                if (this.Session["RES_ID"] != null && this.Session["RES_ID"].ToString() != "") RES_ID = new Guid?(Guid.Parse(this.Session["RES_ID"].ToString()));

                DateTime? FECHA_INICIO = helper.Fecha(this.Session["FECHA_INICIAL"].ToString());
                DateTime? FECHA_FINAL = helper.Fecha(this.Session["FECHA_FINAL"].ToString());
                int VENCIDOS = Convert.ToInt32(this.Session["VENCIDOS"]);

                Factory.InformesDataBase datos = new Factory.InformesDataBase();
                foreach (Models.Transaccion item in datos.Get_list_Informe_Prestamos(TAC_ID, RES_ID, VENCIDOS, FECHA_INICIO, FECHA_FINAL))
                {
                    DataRow fila = repositorio.Get_list_Informe_Prestamos.NewRow();
                    fila["ACT_DESC"] = item.ACT_DESC;
                    fila["TAC_DESC"] = item.TAC_DESC;
                    fila["MOD_DESC"] = item.MOD_DESC;
                    fila["ACT_Serial"] = item.ACT_Serial;
                    fila["ETQ_CODE"] = item.ETQ_CODE;
                    fila["TRA_FECHA_CREATE"] = item.TRA_FECHA_CREATE;
                    fila["Responsable"] = item.Responsable;
                    fila["TRA_Fecha_Retorno"] = item.TRA_Fecha_Retorno;
                    repositorio.Get_list_Informe_Prestamos.Rows.Add(fila);
                }
                xtraInformePrestamo.DataSource = (object)repositorio.Get_list_Informe_Prestamos;
                xtraInformePrestamo.DataMember = repositorio.Get_list_Informe_Prestamos.TableName;
                xtraInformePrestamo.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraInformePrestamo;
            }
            else if (this.Session["Reporte"].ToString().Equals("RENTA"))
            {
                Repositorio repositorio = new Repositorio();
                XtraRenta xtraInformeEtiqueta = new XtraRenta();
                Get_list_InformesPersonalizados_RentaTableAdapter Get_list_InformesPersonalizados_Renta = new Get_list_InformesPersonalizados_RentaTableAdapter();
                string appSetting = ConfigurationManager.AppSettings["FileUploadImgLocation"];
                Get_list_InformesPersonalizados_Renta.GetData();
                xtraInformeEtiqueta.DataAdapter = (object)Get_list_InformesPersonalizados_Renta;
                xtraInformeEtiqueta.DataSource = (object)repositorio.Get_list_InformesPersonalizados_Renta;
                xtraInformeEtiqueta.DataMember = repositorio.Get_list_InformesPersonalizados_Renta.TableName;
                xtraInformeEtiqueta.pcbCabecera.ImageUrl = HostingEnvironment.MapPath(appSetting + "//encabezado_acta_kaeser_2.jpg");
                xtraInformeEtiqueta.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraInformeEtiqueta;
            }
            else if (this.Session["Reporte"].ToString().Equals("DEVOLUCION"))
            {
                Repositorio repositorio = new Repositorio();
                XtraDevolucion xtraInformeDevolucion = new XtraDevolucion();
                Get_list_InformesPersonalizados_GarantiasTableAdapter Get_list_InformesPersonalizados_Garantias = new Get_list_InformesPersonalizados_GarantiasTableAdapter();
                Get_list_InformesPersonalizados_Garantias.GetData();
                xtraInformeDevolucion.DataAdapter = (object)Get_list_InformesPersonalizados_Garantias;
                xtraInformeDevolucion.DataSource = (object)repositorio.Get_list_InformesPersonalizados_Garantias;
                xtraInformeDevolucion.DataMember = repositorio.Get_list_InformesPersonalizados_Garantias.TableName;
                xtraInformeDevolucion.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraInformeDevolucion;
            }
            else if (this.Session["Reporte"].ToString().Equals("DOTACIONES"))
            {
                Repositorio repositorio = new Repositorio();
                XtraDotaciones xtraDotaciones = new XtraDotaciones();
                Get_list_InformesPersonalizados_DotacionesTableAdapter Get_list_InformesPersonalizados_Dotaciones = new Get_list_InformesPersonalizados_DotacionesTableAdapter();
                Get_list_InformesPersonalizados_Dotaciones.GetData();
                xtraDotaciones.DataAdapter = (object)Get_list_InformesPersonalizados_Dotaciones;
                xtraDotaciones.DataSource = (object)repositorio.Get_list_InformesPersonalizados_Dotaciones;
                xtraDotaciones.DataMember = repositorio.Get_list_InformesPersonalizados_Dotaciones.TableName;
                xtraDotaciones.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraDotaciones;
            }
            else if (this.Session["Reporte"].ToString().Equals("METROS2"))
            {
                Repositorio repositorio = new Repositorio();
                XtraInformeMetros2 xtraMetros2 = new XtraInformeMetros2();
                Get_list_InformesPersonalizados_MetrosCuadradosTableAdapter Get_list_InformesPersonalizados_Metros2 = new Get_list_InformesPersonalizados_MetrosCuadradosTableAdapter();
                WebApiKaeser.Helper.Helper helper = new WebApiKaeser.Helper.Helper();
                DateTime? FECHA_INICIO = helper.Fecha(this.Session["FECHA_INICIAL"].ToString());
                DateTime? FECHA_FINAL = helper.Fecha(this.Session["FECHA_FINAL"].ToString());
                Guid? ARE_ID = new Guid?();
                if (this.Session["ARE_ID"] != null && this.Session["ARE_ID"].ToString() != "") ARE_ID = new Guid?(Guid.Parse(this.Session["ARE_ID"].ToString()));
                
                Get_list_InformesPersonalizados_Metros2.GetData(FECHA_INICIO, FECHA_FINAL, ARE_ID);
                xtraMetros2.DataAdapter = (object)Get_list_InformesPersonalizados_Metros2;
                xtraMetros2.DataSource = (object)repositorio.Get_list_InformesPersonalizados_MetrosCuadrados;
                xtraMetros2.DataMember = repositorio.Get_list_InformesPersonalizados_MetrosCuadrados.TableName;
                xtraMetros2.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraMetros2;
            }
            else if (this.Session["Reporte"].ToString().Equals("INFORMESERTEC"))
            {
                Repositorio repositorio = new Repositorio();
                XtraInformeSertec xtraSertec = new XtraInformeSertec();
                Get_list_InformesPersonalizados_ServicioTecnicoTableAdapter Get_list_InformeSertec = new Get_list_InformesPersonalizados_ServicioTecnicoTableAdapter();
                Guid? STE_ID = new Guid?();
                if (this.Session["STE_ID"] != null && this.Session["STE_ID"].ToString() != "")
                    STE_ID = new Guid?(Guid.Parse(this.Session["STE_ID"].ToString()));
                Get_list_InformeSertec.GetData(STE_ID);
                xtraSertec.DataAdapter = (object)Get_list_InformeSertec;
                xtraSertec.DataSource = (object)repositorio.Get_list_InformesPersonalizados_ServicioTecnico;
                xtraSertec.DataMember = repositorio.Get_list_InformesPersonalizados_ServicioTecnico.TableName;
                xtraSertec.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraSertec;
            }
            else if (this.Session["Reporte"].ToString().Equals("AUDITORIA"))
            {
               
                Repositorio repositorio = new Repositorio();                 
                XtraAuditoriaNew xtrareporte = new XtraAuditoriaNew();
                Get_list_AuditoriasTableAdapter Get_list_auditoria = new Get_list_AuditoriasTableAdapter();
                Get_list_PrevisualizacionAuditoriaTableAdapter Get_list_auditoriaPW = new Get_list_PrevisualizacionAuditoriaTableAdapter();
                Guid? AUD_ID = new Guid?();
                int Correlativo = 1;
                if (this.Session["AUD_ID"] != null && this.Session["AUD_ID"].ToString() != "")
                    AUD_ID = new Guid?(Guid.Parse(this.Session["AUD_ID"].ToString()));

                Factory.AuditoriaDataBase datos = new Factory.AuditoriaDataBase();

                foreach (DataRow item in Get_list_auditoria.GetData(null, null, null, AUD_ID).Rows)
                {
                    DataRow fila = repositorio.Get_list_Auditorias.NewRow();
                    fila["AUD_ID"] = item["AUD_ID"];
                    fila["AUD_OBSERVACION"] = item["AUD_OBSERVACION"];
                    fila["USU_LOGIN"] = item["USU_LOGIN"];
                    fila["AUD_FECHA_CREATE"] = item["AUD_FECHA_CREATE"];
                    fila["AUD_FECHA_EJECUCION"] = item["AUD_FECHA_EJECUCION"];
                    fila["ESA_DESC"] = item["ESA_DESC"];
                    fila["ESA_ID"] = item["ESA_ID"];
                    repositorio.Get_list_Auditorias.Rows.Add(fila);
                    Models.AuditoriaTomaDatos filaCabecera = datos.Get_list_TomaDatosPor_Auditoria(AUD_ID);
                    foreach (Models.AuditoriaToma filaActivo in filaCabecera.Activos)
                    {
                        DataRow fila1 = repositorio.Ajuste.NewRow();
                        fila1["Grupo"] = "Activos no encontrados";
                        fila1["AUD_ID"] = AUD_ID;
                        fila1["Correlativo"] = Correlativo;
                        fila1["AII_ID"] = filaActivo.ALL_ACT_ID;
                        fila1["ARE_DESC"] = filaActivo.CAR_RES_ARE_DESC;
                        fila1["TAC_DESC"] = filaActivo.TAC_DESC;
                        fila1["MOD_DESC"] = filaActivo.MOD_DESC;
                        fila1["ACT_DESC"] = filaActivo.ACT_DESC;
                        fila1["AII_ACT_SERIAL"] = filaActivo.ALL_ACT_Serial;
                        fila1["AII_SELECCIONADO"] = filaActivo.SELECCIONADO==null?false: filaActivo.SELECCIONADO;
                        fila1["AII_AJUSTADO"] = filaActivo.ACP_AJUSTADO == null ? false : filaActivo.ACP_AJUSTADO;
                        repositorio.Ajuste.Rows.Add(fila1);
                        Correlativo += 1;
                    }
                    foreach (Models.AuditoriaToma filaActivo in filaCabecera.Areas)
                    {
                        DataRow fila1 = repositorio.Ajuste.NewRow();
                        fila1["AUD_ID"] = AUD_ID;
                        fila1["Correlativo"] = Correlativo;
                        fila1["Grupo"] = "Activos con diferencia de ubicación";
                        fila1["AII_ID"] = filaActivo.ALL_ACT_ID;
                        fila1["ARE_DESC"] = filaActivo.CAR_RES_ARE_DESC;
                        fila1["TAC_DESC"] = filaActivo.TAC_DESC;
                        fila1["MOD_DESC"] = filaActivo.MOD_DESC;
                        fila1["ACT_DESC"] = filaActivo.ACT_DESC;
                        fila1["AII_ACT_SERIAL"] = filaActivo.ALL_ACT_Serial;
                        if (filaActivo.TOMA1 != null) fila1["Toma1"] = filaActivo.TOMA1;
                        fila1["AreaToma1"] = filaActivo.VALORRESAREATOMA1;
                        fila1["SeleccionadoToma1"] = filaActivo.SELECCIONADOTOMA1 == null ? false : filaActivo.SELECCIONADOTOMA1;
                        if (filaActivo.TOMA2 != null) fila1["Toma2"] = filaActivo.TOMA2;
                        fila1["AreaToma2"] = filaActivo.VALORRESAREATOMA2;
                        fila1["SeleccionadoToma2"] = filaActivo.SELECCIONADOTOMA2 == null ? false : filaActivo.SELECCIONADOTOMA2;
                        if (filaActivo.TOMADEF != null) fila1["TomaDef"] = filaActivo.TOMADEF;
                        fila1["AreaTomaDef"] = filaActivo.VALORRESAREATOMADEF;
                        fila1["SeleccionadoDef"] = filaActivo.SELECCIONADOTOMADEF == null ? false : filaActivo.SELECCIONADOTOMADEF;
                        fila1["AII_SELECCIONADO"] = filaActivo.SELECCIONADO == null ? false : filaActivo.SELECCIONADO;
                        fila1["AII_AJUSTADO"] = filaActivo.ACP_AJUSTADO == null ? false : filaActivo.ACP_AJUSTADO;
                        repositorio.Ajuste.Rows.Add(fila1);
                        Correlativo += 1;
                    }

                    foreach (Models.AuditoriaToma filaActivo in filaCabecera.Responsables)
                    {
                        DataRow fila1 = repositorio.Ajuste.NewRow();
                        fila1["Grupo"] = "Activos con diferencia de responsables";
                        fila1["AUD_ID"] = AUD_ID;
                        fila1["Correlativo"] = Correlativo;
                        fila1["AII_ID"] = filaActivo.ALL_ACT_ID;
                        fila1["ARE_DESC"] = filaActivo.CAR_RES_ARE_DESC;
                        fila1["TAC_DESC"] = filaActivo.TAC_DESC;
                        fila1["MOD_DESC"] = filaActivo.MOD_DESC;
                        fila1["ACT_DESC"] = filaActivo.ACT_DESC;
                        fila1["AII_ACT_SERIAL"] = filaActivo.ALL_ACT_Serial;
                        if (filaActivo.TOMA1 != null) fila1["Toma1"] = filaActivo.TOMA1;
                        fila1["AreaToma1"] = filaActivo.VALORRESAREATOMA1;
                        fila1["SeleccionadoToma1"] = filaActivo.SELECCIONADOTOMA1 == null ? false : filaActivo.SELECCIONADOTOMA1;
                        if (filaActivo.TOMA2 != null) fila1["Toma2"] = filaActivo.TOMA2;
                        fila1["AreaToma2"] = filaActivo.VALORRESAREATOMA2;
                        fila1["SeleccionadoToma2"] = filaActivo.SELECCIONADOTOMA2 == null ? false : filaActivo.SELECCIONADOTOMA2;
                        if (filaActivo.TOMADEF != null) fila1["TomaDef"] = filaActivo.TOMADEF;
                        fila1["AreaTomaDef"] = filaActivo.VALORRESAREATOMADEF;
                        fila1["SeleccionadoDef"] = filaActivo.SELECCIONADOTOMADEF == null ? false : filaActivo.SELECCIONADOTOMADEF;
                        fila1["AII_SELECCIONADO"] = filaActivo.SELECCIONADO == null ? false : filaActivo.SELECCIONADO;
                        fila1["AII_AJUSTADO"] = filaActivo.ACP_AJUSTADO == null ? false : filaActivo.ACP_AJUSTADO;
                        repositorio.Ajuste.Rows.Add(fila1);
                        Correlativo += 1;
                    }
                    foreach (Models.AuditoriaToma filaActivo in filaCabecera.Caracteristicas)
                    {
                        DataRow fila1 = repositorio.Ajuste.NewRow();
                        fila1["AUD_ID"] = AUD_ID;
                        fila1["Correlativo"] = Correlativo;
                        fila1["Grupo"] = "Ajustar características";
                        fila1["AII_ID"] = filaActivo.ALL_ACT_ID;
                        fila1["ARE_DESC"] = filaActivo.CAR_RES_ARE_DESC;
                        fila1["VALOR"] = filaActivo.VALOR;
                        fila1["TAC_DESC"] = filaActivo.TAC_DESC;
                        fila1["MOD_DESC"] = filaActivo.MOD_DESC;
                        fila1["ACT_DESC"] = filaActivo.ACT_DESC;
                        fila1["AII_ACT_SERIAL"] = filaActivo.ALL_ACT_Serial;
                        //fila1["AII_SELECCIONADO"] = filaActivo.SELECCIONADO;
                        if (filaActivo.TOMA1 != null) fila1["Toma1"] = filaActivo.TOMA1;
                        fila1["AreaToma1"] = filaActivo.VALORRESAREATOMA1;
                        fila1["SeleccionadoToma1"] = filaActivo.SELECCIONADOTOMA1 == null ? false : filaActivo.SELECCIONADOTOMA1;
                        if (filaActivo.TOMA2 != null) fila1["Toma2"] = filaActivo.TOMA2;
                        fila1["AreaToma2"] = filaActivo.VALORRESAREATOMA2;
                        fila1["SeleccionadoToma2"] = filaActivo.SELECCIONADOTOMA2 == null ? false : filaActivo.SELECCIONADOTOMA2;
                        if (filaActivo.TOMADEF != null) fila1["TomaDef"] = filaActivo.TOMADEF;
                        fila1["AreaTomaDef"] = filaActivo.VALORRESAREATOMADEF;
                        fila1["SeleccionadoDef"] = filaActivo.SELECCIONADOTOMADEF == null ? false : filaActivo.SELECCIONADOTOMADEF;
                        fila1["AII_SELECCIONADO"] = filaActivo.SELECCIONADO == null ? false : filaActivo.SELECCIONADO;
                        fila1["AII_AJUSTADO"] = filaActivo.ACP_AJUSTADO == null ? false : filaActivo.ACP_AJUSTADO;
                        repositorio.Ajuste.Rows.Add(fila1);
                        Correlativo += 1;
                    }
                    //foreach (DataRow item1 in Get_list_auditoriaPW.GetData(Guid.Parse ( item["AUD_ID"].ToString ()),null))
                    //{
                    //    DataRow fila1 = repositorio.Get_list_PrevisualizacionAuditoria.NewRow();
                    //    fila1["ACT_ID"] = item1["ACT_ID"];
                    //    fila1["ACT_DESC"] = item1["ACT_DESC"];
                    //    fila1["ACT_Serial"] = item1["ACT_Serial"];
                    //    fila1["EST_DESC"] = item1["EST_DESC"];
                    //    fila1["ARE_DESC"] = item1["ARE_DESC"];
                    //    fila1["TAC_DESC"] = item1["TAC_DESC"];
                    //    fila1["RES_NOMBRE_APELLIDO"] = item1["RES_NOMBRE_APELLIDO"];
                    //    fila1["ETA_ID"] = item1["ETA_ID"];
                    //    fila1["ARE_ID"] = item1["ARE_ID"];
                    //    fila1["RES_ID"] = item1["RES_ID"];
                    //    fila1["ETQ_BC"] = item1["ETQ_BC"];
                    //    fila1["ETQ_CODE"] = item1["ETQ_CODE"];
                    //    fila1["ETQ_EPC"] = item1["ETQ_EPC"];
                    //    fila1["AUD_ID"] = AUD_ID;
                    //    repositorio.Get_list_PrevisualizacionAuditoria.Rows.Add(fila1);
                    //}
                }
               
                xtrareporte.DataSource = (object)repositorio;
                xtrareporte.DataMember = repositorio.Get_list_Auditorias.TableName;
                xtrareporte.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtrareporte;

            }
            else if (this.Session["Reporte"].ToString().Equals("EQUIPOSDISPONIBLES"))
            {
                Repositorio repositorio = new Repositorio();
                XtraReporteEquipoDisponible xtraInformeEquipo = new XtraReporteEquipoDisponible();
                Get_list_InformesPersonalizados_EquiposDisponiblesTableAdapter Get_list_InformesPersonalizados_Equipo = new Get_list_InformesPersonalizados_EquiposDisponiblesTableAdapter();
                string appSetting = ConfigurationManager.AppSettings["FileUploadImgLocation"];
                Get_list_InformesPersonalizados_Equipo.GetData();
                xtraInformeEquipo.DataAdapter = (object)Get_list_InformesPersonalizados_Equipo;
                xtraInformeEquipo.DataSource = (object)repositorio.Get_list_InformesPersonalizados_EquiposDisponibles;
                xtraInformeEquipo.DataMember = repositorio.Get_list_InformesPersonalizados_EquiposDisponibles.TableName;
                xtraInformeEquipo.pcbCabecera.ImageUrl = HostingEnvironment.MapPath(appSetting + "//encabezado_acta_kaeser_2.jpg");
                xtraInformeEquipo.CreateDocument();
                this.ASPxDocumentViewer1.Report = (XtraReport)xtraInformeEquipo;
            }
            else
                this.ASPxDocumentViewer1.ReportTypeName = "WebApiKaeser.Reportes.XtraCaracteristica";

        }
    }
}