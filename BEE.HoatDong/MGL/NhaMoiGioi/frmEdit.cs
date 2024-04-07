using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.NguoiMG
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int? MaMG { get; set; }
        mglNguoiMoiGioi objMG;
        public bool IsSave = false;
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
           // textEdit1.Text = "<A href='http://beesky.vn/'>giải pháp phần mềm quản lý khách hàng</A>";

            if (MaMG != null)
            {
                objMG = db.mglNguoiMoiGiois.FirstOrDefault(p => p.ID == MaMG);
                txtKhuVuc.Text = objMG.DiaChi;
                txtNguoiMG.Text = objMG.TenMG;
                txtSDT.Text = objMG.SDT;
            }
            else
            {
                objMG = new mglNguoiMoiGioi();
                db.mglNguoiMoiGiois.InsertOnSubmit(objMG);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MaMG == null)
            {
                objMG.NgayNhap = db.GetSystemDate();
                objMG.MaNV = Common.StaffID;
            }
            objMG.DiaChi = txtKhuVuc.Text.Trim();
            objMG.SDT = txtSDT.Text.Trim();
            objMG.TenMG = txtKhuVuc.Text.Trim();
            try
            {
                db.SubmitChanges();
                IsSave = true;
                DialogBox.Infomation("Dữ liệu đã lưu thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error("Dữ liệu không thể lưu: " + ex.Message);
            }
        }
    }
}