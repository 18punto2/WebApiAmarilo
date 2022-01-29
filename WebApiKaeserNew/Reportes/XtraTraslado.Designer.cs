namespace WebApiKaeser.Reportes
{
    partial class XtraTraslado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraTraslado));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.Titulo = new DevExpress.XtraReports.Parameters.Parameter();
            this.repositorio1 = new WebApiKaeser.Reportes.Repositorio();
            this.get_list_AreaTableAdapter = new WebApiKaeser.Reportes.RepositorioTableAdapters.Get_list_AreaTableAdapter();
            this.get_list_TransaccionesTableAdapter1 = new WebApiKaeser.Reportes.RepositorioTableAdapters.Get_list_TransaccionesTableAdapter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.repositorio1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel14,
            this.xrLabel17,
            this.xrLabel16});
            this.Detail.HeightF = 23F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Get_list_Transacciones.TRA_Doc_Factura")});
            this.xrLabel7.Font = new System.Drawing.Font("Calibri", 11F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(112.5833F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Get_list_Transacciones.AREA_DESTINO")});
            this.xrLabel6.Font = new System.Drawing.Font("Calibri", 11F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(257.6874F, 0F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(147.3956F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "xrLabel6";
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Get_list_Transacciones.AREA_ORIGEN")});
            this.xrLabel5.Font = new System.Drawing.Font("Calibri", 11F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(112.5833F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(145F, 23F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "xrLabel5";
            // 
            // xrLabel14
            // 
            this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Get_list_Transacciones.TRA_DOCUMENTO_SAP")});
            this.xrLabel14.Font = new System.Drawing.Font("Calibri", 11F);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(405.0831F, 0F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.SizeF = new System.Drawing.SizeF(129.25F, 23F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel17
            // 
            this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Get_list_Transacciones.TRA_FECHA_CREATE")});
            this.xrLabel17.Font = new System.Drawing.Font("Calibri", 11F);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(630.2498F, 0F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.SizeF = new System.Drawing.SizeF(162.7502F, 23F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel16
            // 
            this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Get_list_Transacciones.EST_DESC")});
            this.xrLabel16.Font = new System.Drawing.Font("Calibri", 11F);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(534.3331F, 0F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.SizeF = new System.Drawing.SizeF(95.91669F, 23F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 22F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2,
            this.xrLabel11,
            this.xrPictureBox1});
            this.ReportHeader.HeightF = 81.87499F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(609.0834F, 10.00001F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(157.0833F, 70.00001F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrLabel11
            // 
            this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.Titulo, "Text", "")});
            this.xrLabel11.Font = new System.Drawing.Font("Calibri", 22F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(177.5F, 22.4167F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.SizeF = new System.Drawing.SizeF(415.4583F, 42.91667F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // Titulo
            // 
            this.Titulo.Description = "Titulo del reporte";
            this.Titulo.Name = "Titulo";
            // 
            // repositorio1
            // 
            this.repositorio1.DataSetName = "Repositorio";
            this.repositorio1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // get_list_AreaTableAdapter
            // 
            this.get_list_AreaTableAdapter.ClearBeforeFill = true;
            // 
            // get_list_TransaccionesTableAdapter1
            // 
            this.get_list_TransaccionesTableAdapter1.ClearBeforeFill = true;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TRA_TTR_ID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 26.04167F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(8.395831F, 0F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.SizeF = new System.Drawing.SizeF(139.2917F, 26F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "TIPO:\r\n";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Get_list_Transacciones.TTR_DESC")});
            this.xrLabel2.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(147.6875F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.SizeF = new System.Drawing.SizeF(274.4167F, 26F);
            this.xrLabel2.StylePriority.UseFont = false;
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLine1,
            this.xrLabel3,
            this.xrLabel8,
            this.xrLabel10,
            this.xrLabel12,
            this.xrLabel13,
            this.xrLine2});
            this.GroupHeader2.HeightF = 22F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(257.6874F, 2F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.SizeF = new System.Drawing.SizeF(147.3956F, 18F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "ÁREA DESTINO";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLine1
            // 
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(809F, 2F);
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.SizeF = new System.Drawing.SizeF(112.5833F, 18F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Nº TRASLADO";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(112.5833F, 2F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.SizeF = new System.Drawing.SizeF(145F, 18F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "ÁREA ORIGEN";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(405.0831F, 2F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.SizeF = new System.Drawing.SizeF(129.25F, 18F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "DOCUMENTO SAP";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(534.3331F, 2F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.SizeF = new System.Drawing.SizeF(95.91669F, 18F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "ESTADO";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(630.2498F, 2F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.SizeF = new System.Drawing.SizeF(162.7502F, 18F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "FECHA DE REGISTRO";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLine2
            // 
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 20F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(809F, 2F);
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrPageInfo1,
            this.xrPageInfo2});
            this.PageFooter.HeightF = 40.625F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Calibri", 9F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(43.85421F, 10F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(126.0417F, 23F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.Text = "Fecha de Impresión:";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Calibri", 9F);
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(169.8958F, 10F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(173.4167F, 23F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.WordWrap = false;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Calibri", 9F);
            this.xrPageInfo2.Format = "Página {0} de {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(479.9999F, 10F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(313F, 23F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(18.75F, 11.87499F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(158.75F, 70F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // XtraTraslado
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.GroupHeader2,
            this.PageFooter});
            this.DataAdapter = this.get_list_TransaccionesTableAdapter1;
            this.DataMember = "Get_list_Transacciones";
            this.DataSource = this.repositorio1;
            this.Margins = new System.Drawing.Printing.Margins(21, 18, 0, 22);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.Titulo});
            this.Version = "14.2";
            ((System.ComponentModel.ISupportInitialize)(this.repositorio1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private Repositorio repositorio1;
        private RepositorioTableAdapters.Get_list_AreaTableAdapter get_list_AreaTableAdapter;
        private RepositorioTableAdapters.Get_list_TransaccionesTableAdapter get_list_TransaccionesTableAdapter1;
        private DevExpress.XtraReports.Parameters.Parameter Titulo;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel14;
        private DevExpress.XtraReports.UI.XRLabel xrLabel17;
        private DevExpress.XtraReports.UI.XRLabel xrLabel16;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
    }
}
