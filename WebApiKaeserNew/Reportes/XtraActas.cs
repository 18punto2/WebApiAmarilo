using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace WebApiKaeser.Reportes
{
    public partial class XtraActas : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraActas()
        {
            InitializeComponent();
        }

        private void DetailReport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (((DevExpress.XtraReports.UI.XtraReportBase)sender).RowCount == 0) DetailReport2.Visible = false;
            else DetailReport2.Visible = true;
        }
    }
}
