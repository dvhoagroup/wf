using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.Test
{
    public partial class Chart_frm : DevExpress.XtraEditors.XtraForm
    {
        public Chart_frm()
        {
            InitializeComponent();
        }

        private void Chart_frm_Load(object sender, EventArgs e)
        {
            
        }

        private void Chart_frm_Shown(object sender, EventArgs e)
        {
            //it.khbhLichThanhToanCls o = new it.khbhLichThanhToanCls();
            //chartControl1.DataSource = o.Select(1);
            double[] a = {60, 40};
            double[] b = {20};
            chartControl1.Series[0].Points[0].Argument = "DIP";
            chartControl1.Series[0].Points[0].Values = a;

            chartControl1.Series[0].Points[1].Argument = "DIP NT";
            chartControl1.Series[0].Points[1].Values.SetValue(40, 0);
        }

        private void chartControl1_Click(object sender, EventArgs e)
        {

        }
    }
}