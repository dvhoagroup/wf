using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LandSoft.Library;
using System.Data.Linq.SqlClient;

namespace LandSoft.CongVan.received
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? ID;
        MasterDataContext db;
        CongVanDen objCV;
        public frmEdit()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmEdit_Load);
        }

        void frmEdit_Load(object sender, EventArgs e)
        {
            ctlPhongBanCheckListEdit1.LoadData();
            ctlLoaiCongVanDen1.LoadData();
            if (this.ID != null)
                CongVanLoad();
            else
                CongVanAddNew();
        }

        void CongVanAddNew()
        {
            db = new MasterDataContext();
            objCV = new CongVanDen();
            dateNgayNhan.EditValue = DateTime.Now;
            txtSoCV.EditValue = "cvd-" + ((db.CongVanDens.Max(p => (int?)p.ID) ?? 0) + 1);// db.DinhDang(10, (db.bhPhieuThus.Max(p => (int?)p.ID) ?? 0) + 1);
            // txtSoCV.Text=db
            //objPT = new bhPhieuThu();
            //gcChiTiet.DataSource = objPT.bhptChiTiets;
            //txtSoPT.EditValue = db.DinhDang(10, (db.bhPhieuThus.Max(p => (int?)p.ID) ?? 0) + 1);
            //dateNgayThu.EditValue = DateTime.Now;
            //spinTyGia.EditValue = ctlLoaiTienEdit1.GetColumnValue("TyGia");
            //txtNguoiNop.EditValue = null;
            //txtDiaChiPT.EditValue = null;
            //ctlCustomerEdit1.EditValue = null;
            //ctlTKNHEdit1.EditValue = null;
            //PhieuThuEnable(true);
        }

        void CongVanLoad()
        {
            db = new MasterDataContext();
            objCV = db.CongVanDens.Single(p => p.ID == this.ID);
            txtSoCV.Text = objCV.SoCV;
            dateNgayNhan.EditValue = objCV.NgayNhan;
            spinTienDo.Value = (decimal)objCV.TienDo;
            txtNoiDung.Text = objCV.NoiDung;
            ctlLoaiCongVanDen1.EditValue = objCV.MaLCV;
            string huong = "";
            foreach (var pb in objCV.CongVanDen_PhongBans)
                huong += pb.MaPB + ", ";
            huong = huong.TrimEnd(' ').TrimEnd(',');
            ctlPhongBanCheckListEdit1.SetEditValue(huong);
            huong = "";

            foreach (var nv in objCV.CongVanDen_NhanViens)
                huong += nv.MaNV + ", ";
            huong = huong.TrimEnd(' ').TrimEnd(',');
            ctlNhanVienCheckListEdit1.SetEditValue(huong);
            txtDiaChiLuu.Text = objCV.DiaChiLuu;
            txtGhiChu.Text = objCV.GhiChuThem;
            txtNguonNhan.Text = objCV.NguonNhan;
            txtNoiGui.Text = objCV.NoiGui;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void CongVan_Save()
        {
            #region Rang buoc du lieu
            if (txtSoCV.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Số công văn], xin cảm ơn.");
                txtSoCV.Focus();
                return;
            }
            if (ctlLoaiCongVanDen1.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Loại công văn], xin cảm ơn.");
                ctlLoaiCongVanDen1.Focus();
                return;
            }
            if (txtNoiGui.Text.Trim() == null)
            {
                DialogBox.Error("Vui lòng nhập [Nơi gửi], xin cảm ơn.");
                txtNoiGui.Focus();
                return;
            }
            //if (ctlKhachHang1.EditValue == null)
            //{
            //    DialogBox.Error("Vui lòng chọn [Khách hàng], xin cảm ơn.");
            //    ctlKhachHang1.Focus();
            //    return;
            //}
            if (ctlPhongBanCheckListEdit1.EditValue == null || ctlPhongBanCheckListEdit1.EditValue.ToString() == "")
            {
                DialogBox.Error("Vui lòng chọn [Phòng ban nhận] , xin cảm ơn.");
                ctlPhongBanCheckListEdit1.Focus();
                return;
            }
            if (txtNoiDung.Text == txtNoiDung.Properties.NullText || txtNoiDung.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Nội dung], xin cảm ơn.");
                txtNoiDung.Focus();
                return;
            }
            if (txtDiaChiLuu.Text.Trim() == null)
            {
                DialogBox.Error("Vui lòng nhập [Địa chỉ lưu], xin cảm ơn.");
                txtDiaChiLuu.Focus();
                return;
            }
            if (txtNguonNhan.Text.Trim() == null)
            {
                DialogBox.Error("Vui lòng nhập [Nguồn nhận], xin cảm ơn.");
                txtNguonNhan.Focus();
                return;
            }

            #endregion


            objCV.NgayNhan = (DateTime)dateNgayNhan.EditValue;
            objCV.MaLCV = ctlLoaiCongVanDen1.EditValue.ToString();
            objCV.TienDo = (int)spinTienDo.Value;
            objCV.NoiDung = txtNoiDung.Text.Trim();
            objCV.NoiGui = txtNoiGui.Text.ToString().Trim();
            objCV.DiaChiLuu = txtDiaChiLuu.Text.Trim();
            objCV.NguonNhan = txtNguonNhan.Text.Trim();
            objCV.GhiChuThem = txtGhiChu.Text.Trim();
            db.CongVanDen_PhongBans.DeleteAllOnSubmit(objCV.CongVanDen_PhongBans);
            string temp = ctlPhongBanCheckListEdit1.EditValue.ToString();
            objCV.MaPBNhan = ctlPhongBanCheckListEdit1.Text;
            var chuoi = temp.Split(',');
            foreach (var pb in chuoi)
            {
                CongVanDen_PhongBan cvpb = new CongVanDen_PhongBan();
                cvpb.MaCV = objCV.ID;
                cvpb.MaPB = byte.Parse(pb);
                db.CongVanDen_PhongBans.InsertOnSubmit(cvpb);
            }

            db.CongVanDen_NhanViens.DeleteAllOnSubmit(objCV.CongVanDen_NhanViens);
            if (ctlNhanVienCheckListEdit1.EditValue != null)
                temp = ctlNhanVienCheckListEdit1.EditValue.ToString();
            else temp = "";
            objCV.MaNVNhan = ctlNhanVienCheckListEdit1.Text;
            if (temp != "")
            {
                chuoi = temp.Split(',');
                foreach (var nv in chuoi)
                {
                    CongVanDen_NhanVien cvnv = new CongVanDen_NhanVien();
                    cvnv.MaCV = objCV.ID;
                    cvnv.MaNV = byte.Parse(nv);
                    db.CongVanDen_NhanViens.InsertOnSubmit(cvnv);
                }
            }

            if (this.ID == null)
            {

                objCV.MaTT = "1";
                objCV.SoCV = txtSoCV.Text.Trim();
                db.CongVanDens.InsertOnSubmit(objCV);
                objCV.NguoiCN = null;
                objCV.NgayCN = null;
            }
            else
            {

                objCV.NguoiCN = 1; ///////////Nguoi sua
                objCV.NgayCN = DateTime.Now;
                cvdNhatKyXuLy xl = new cvdNhatKyXuLy();
                xl.MaNV = 1;
                xl.NgayXL = DateTime.Now;
                xl.MaTT = objCV.MaTT;
                xl.TienDo = objCV.TienDo;
                xl.DienGiai = "Cập nhật thông tin!";
                objCV.cvdNhatKyXuLies.Add(xl);
            }
            try
            {
                db.SubmitChanges();

                DialogBox.Infomation();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
        private void btnDongY_Click(object sender, EventArgs e)
        {
            CongVan_Save();
        }

        private void ctlPhongBan1_EditValueChanged(object sender, EventArgs e)
        {
            // ctlNhanVien1.MaPB = ctlPhongBan1.EditValue;
            //  ctlNhanVien1.LoadData();
            // ctlNhanVien1.EditValue = null;
        }

        private void ctlNhanVien1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 1)
            //{
            //    ctlNhanVien1.EditValue = null;
            //}
        }

        private void ctlPhongBanCheckListEdit1_EditValueChanged(object sender, EventArgs e)
        {
            var temp = ctlPhongBanCheckListEdit1.EditValue.ToString().Split(',');
            ctlNhanVienCheckListEdit1.MAPB = temp;
            ctlNhanVienCheckListEdit1.LoadData();
            ctlNhanVienCheckListEdit1.EditValue = null;
            //  ctlNhanVien1.LoadData();
            // ctlNhanVien1.EditValue = null;
        }

        private void ctlNhanVienCheckListEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ctlNhanVienCheckListEdit1.EditValue = null;
            }
        }

        private void ctlKhachHang1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 1)
            //    ctlKhachHang1.EditValue = null;
        }
    }
}