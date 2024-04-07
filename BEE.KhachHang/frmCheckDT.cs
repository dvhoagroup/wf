using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class frmCheckDT : DevExpress.XtraEditors.XtraForm
    {
        public frmCheckDT()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtCHK.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập SĐT để kiểm tra!");
                txtCHK.Focus();
                return;
            }
            using (var db = new MasterDataContext())
            {
                var dt = txtCHK.Text.Trim();
                var obj = db.KhachHangs.Where(p => p.DiDong == dt || p.DiDong2 == dt || p.DiDong3 == dt || p.DiDong4 == dt || p.DienThoaiCT == dt).ToList();
                if (obj.Count > 0)
                    DialogBox.Infomation("Số điện thoại này đã có trong hệ thống. Vui lòng kiểm tra lại!");
                else
                    DialogBox.Infomation("Số điện thoại này chưa có trong hệ thống, bạn có thể tiếp tục!");
            }
        }
    }
}
