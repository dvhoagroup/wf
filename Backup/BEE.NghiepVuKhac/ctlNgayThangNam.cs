using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.NghiepVuKhac
{
    public partial class ctlNgayThangNam : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlNgayThangNam()
        {
            InitializeComponent();
        }

        public string Ngay
        {
            get { return txtNgay.Text.Trim(); }
            set { txtNgay.EditValue = value; }
        }

        public string Thang
        {
            get { return txtThang.Text.Trim(); }
            set { txtThang.EditValue = value; }
        }

        public string Nam
        {
            get { return txtNam.Text.Trim(); }
            set { txtNam.EditValue = value; }
        }
        
        public DateTime? Datetime
        {
            get
            {
                try
                {
                    return new DateTime(int.Parse(this.Nam), int.Parse(this.Thang), int.Parse(this.Ngay));
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value != null && value != DateTime.MinValue)
                {
                    this.Ngay = value.Value.ToString("dd");
                    this.Thang = value.Value.ToString("MM");
                    this.Nam = value.Value.ToString("yyyy");
                }
                else
                {
                    this.Ngay = null;
                    this.Thang = null;
                    this.Nam = null;
                }
            }
        }

        private void txtNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (this.Ngay == "")
                this.Ngay = null;
            else if (this.Ngay.Length == 2)
                txtThang.Focus();
        }

        private void txtThang_EditValueChanged(object sender, EventArgs e)
        {
            if (this.Thang == "") 
                this.Thang = null;
            else if (this.Thang.Length == 2)
                txtNam.Focus();
        }

        private void txtNam_EditValueChanged(object sender, EventArgs e)
        {
            if (this.Nam == "") this.Nam = null;
        }
    }
}
