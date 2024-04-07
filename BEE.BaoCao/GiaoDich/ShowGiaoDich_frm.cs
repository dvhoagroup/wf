using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.BaoCao.GiaoDich
{
    public partial class ShowGiaoDich_frm : DevExpress.XtraEditors.XtraForm
    {
        public ShowGiaoDich_frm( int Ma)
        {
            InitializeComponent();
            GiaoDich.PDKGiaoDich_rpt rpt = new BEE.BaoCao.GiaoDich.PDKGiaoDich_rpt(Ma);
            printCtl.PrintingSystem = rpt.PrintingSystem;
            rpt.CreateDocument();
        }

        private void ShowGiaoDich_frm_Load(object sender, EventArgs e)
        {          
            
        }
    }
}