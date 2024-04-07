using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class frmPhanHoi : DevExpress.XtraEditors.XtraForm
    {
        public int? NhatKyID { get; set; }
        KhachHang_NhatKy_PhanHoi objNhatKyPhanHoi;
        MasterDataContext db;
        public frmPhanHoi()
        {
            InitializeComponent();
            db = new MasterDataContext();
            objNhatKyPhanHoi = new KhachHang_NhatKy_PhanHoi();
        }

        private void btnLuuDong_Click(object sender, EventArgs e)
        {
            objNhatKyPhanHoi.GhiChu = txtNoiDung.Text.Trim();
            objNhatKyPhanHoi.NhatKyID = NhatKyID;
            objNhatKyPhanHoi.MaNV = Common.StaffID;
            objNhatKyPhanHoi.NgayTao = db.GetSystemDate();
            try
            {
                var objNhatKy = db.KhachHang_NhatKies.SingleOrDefault(p => p.ID == NhatKyID);
                objNhatKy.PhanHoi = txtNoiDung.Text.Trim();
                
                if (txtNoiDung.Text == "")
                {
                    DialogBox.Warning("Vui lòng nhập nội dung phản hồi");
                    txtNoiDung.Focus();
                    return;
                }
                db.KhachHang_NhatKy_PhanHois.InsertOnSubmit(objNhatKyPhanHoi);
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                DialogBox.Error("Không thể cập nhật phản hồi");
            }
            
        }

        private void frmPhanHoi_Load(object sender, EventArgs e)
        {
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}