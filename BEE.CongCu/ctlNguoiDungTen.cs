using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.CongCu
{
    public partial class ctlNguoiDungTen : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlNguoiDungTen()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this);
        }

        public void LoadData(int? maPGC)
        {
            if (maPGC == null)
            {
                gcKhachHang.DataSource = null;
            }
            else
            {
                using (var db = new BEE.ThuVien.MasterDataContext())
                {
                    gcKhachHang.DataSource = db.pgcKhachHang_Select(maPGC);
                }
            }
        }
    }
}
