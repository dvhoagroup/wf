using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.VOIPSETUP
{
    public partial class frmSDT : DevExpress.XtraEditors.XtraForm
    {
        public frmSDT()
        {
            InitializeComponent();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            var sdt = txtSDT.Text;
            // form
            // if (sdt !=  )
            // {
            var frmCall = new Call.frmInCall();
            frmCall.SDT = sdt;
            //      frmCall.Unique = e.uniqueid;
            frmCall.ShowDialog();
            //  }
        }


    }
}