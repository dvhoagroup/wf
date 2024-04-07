using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;

namespace BEEREMA.HeThong
{
    public partial class ChangePassword_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool ChangedPass;
        public NhanVien objNv;
        public ChangePassword_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this); 
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtConfirmPass.Text.Trim() != txtNewPass.Text)
            {
                DialogBox.Infomation("Mật khẩu xác nhận không chính xác. Vui lòng kiểm tra lại, xin cảm ơn.");
                txtConfirmPass.Focus();
                return;
            }
            try
            {
                if (txtOldPass.Text.Trim() == it.CommonCls.GiaiMa(objNv.MatKhau))
                {                   
                    it.NhanVienCls o = new it.NhanVienCls();
                    o.MaSo = objNv.MaSo;
                    o.MatKhau = it.CommonCls.MaHoa(txtNewPass.Text);
                    o.MaNV = objNv.MaNV;
                    o.ChangedPass = true;
                    o.ChangePassword(objNv.MaNV);

                    Properties.Settings.Default.Password = txtNewPass.Text;
                    Properties.Settings.Default.Save();
                    this.ChangedPass = true;
                    DialogBox.Infomation("Dữ liệu đã được cập nhật.");
                    this.Close();
                }
                else
                    DialogBox.Infomation("Mật khẩu cũ không đúng. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
            catch
            {
                DialogBox.Infomation("Mật khẩu cũ không đúng. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
        }
    }
}