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
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        public int? CustomerID = 0;
        public byte? MaNKH;

        public frmProcess()
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
            if (lookUpNhomKH.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng chọn [Nhóm khách hàng], xin cảm ơn.");
                lookUpNhomKH.Focus();
                return;
            }

            using (MasterDataContext db = new MasterDataContext())
            {
                var objHis = new KhachHang_NhatKy();
                objHis.MaKH = CustomerID;
                objHis.DienGiai = txtDescription.Text;
                objHis.MaNV = BEE.ThuVien.Common.StaffID;
                objHis.MaNKH = Convert.ToByte(lookUpNhomKH.EditValue);

                try
                {
                    db.KhachHang_NhatKies.InsertOnSubmit(objHis);
                    db.SubmitChanges();

                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                catch { }
            }

            this.Close();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                lookUpNhomKH.Properties.DataSource = db.NhomKHs;
                lookUpNhomKH.EditValue = MaNKH;
            }
        }
    }
}
