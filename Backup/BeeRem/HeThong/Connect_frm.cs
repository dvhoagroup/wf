using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA
{
    public partial class Connect_frm : DevExpress.XtraEditors.XtraForm
    {
        string ConnectString = "";
        public Connect_frm()
        {
            InitializeComponent(); 
            
            BEE.NgonNgu.Language.TranslateControl(this); 
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                //Nhap chuoi ket noi                
                ConnectString = it.EncDec.Decrypt(txtKey.Text);
                BEE.ThuVien.Common.SqlConnString = ConnectString;
                BEE.ThuVien.Common.Conn = txtKey.Text;
                
                if (!it.CommonCls.TestConnect(ConnectString))
                    DialogBox.Infomation("Kết nối không thành công. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                    this.Hide();
            }
        }

        private void Connect_frm_Load(object sender, EventArgs e)
        {
        }
    }
}