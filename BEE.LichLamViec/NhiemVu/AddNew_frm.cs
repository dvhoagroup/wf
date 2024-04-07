using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using System.Linq;
using BEE.ThuVien;

namespace BEEREMA.CongViec.NhiemVu
{
    public partial class AddNew_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public int MaNVu = 0;
        private BEE.ThuVien.NhiemVu objNhv { get; set; }


        MasterDataContext db = new MasterDataContext();

        public AddNew_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void LoadDictionary()
        {
            it.NhiemVu_LoaiCls objLoai = new it.NhiemVu_LoaiCls();
            LookupLoai.Properties.DataSource = objLoai.Select();
            LookupLoai.ItemIndex = 0;

            it.NhiemVu_MucDoCls objMucDo = new it.NhiemVu_MucDoCls();
            lookUpMucDo.Properties.DataSource = objMucDo.Select();
            lookUpMucDo.ItemIndex = 0;

            it.NhiemVu_TinhTrangCls objStatus = new it.NhiemVu_TinhTrangCls();
            lookUpStatus.Properties.DataSource = objStatus.Select();
            lookUpStatus.ItemIndex = 0;
            lookNhanVien.DataSource = db.NhanViens.Where(p=>p.MaTinhTrang == 1).Select(p => new { p.MaNV, p.HoTen, p.NgaySinh, p.PhongBan.TenPB, p.ChucVu.TenCV });
        }

        void LoadData()
        {
            it.NhiemVuCls o = new it.NhiemVuCls(MaNVu);

            lblNguoiCapNhat.Caption = string.Format("Người cập nhật: {0} | {1:dd/MM/yyyy}", BEE.ThuVien.Common.StaffName, DateTime.Now);
            lblNguoiTao.Caption = string.Format("Người tạo: {0} | {1:dd/MM/yyyy}", o.NhanVien.HoTen, o.NgayBD);
            txtDienGiai.Text = o.DienGiai;
            txtTieuDe.Text = o.TieuDe;
            LookupLoai.EditValue = o.LoaiNV.MaLNV;
            //lookTienDo.EditValue = o.TienDo.MaTD;
            lookUpMucDo.EditValue = o.MucDo.MaMD;
            lookUpStatus.EditValue = o.TinhTrang.MaTT;
            if (o.NgayHT.Year != 1)
                dateNgayHT.DateTime = o.NgayHT;
            dateNgayKT.DateTime = o.NgayHH;
            dateNgayBD.DateTime = o.NgayBD;

            spinHoanThanh.EditValue = o.PhanTramHT;
            checkBNhacViec.Checked = o.IsNhac;
            btnRing.Tag = o.Rings;
            if (o.IsNhac)
                dateNhacViec.Enabled = true;
            else
                dateNhacViec.Enabled = false;
            if (o.NgayNhac.Year != 1)
                dateNhacViec.DateTime = o.NgayNhac;
            btnKhachHang.Text = o.KhachHang.HoKH;            
            btnKhachHang.Tag = o.KhachHang.MaKH;
        }

        void LoadDataV2()
        {
            if (MaNVu == 0)
            {
                objNhv = new BEE.ThuVien.NhiemVu();
                db.NhiemVus.InsertOnSubmit(objNhv);
                dateNgayBD.EditValue = dateNgayHT.EditValue = dateNgayKT.EditValue = DateTime.Now;
                lblNguoiCapNhat.Caption = string.Format("Người cập nhật: {0} | {1:dd/MM/yyyy}", BEE.ThuVien.Common.StaffName, DateTime.Now);
                lblNguoiTao.Caption = string.Format("Người tạo: {0} | {1:dd/MM/yyyy}", BEE.ThuVien.Common.StaffName, DateTime.Now);
                dateNhacViec.Enabled = false;
                dateNgayBD.DateTime = dateNgayKT.DateTime = DateTime.Now;
                btnRing.Enabled = false;
            }
            else
            {
                objNhv = db.NhiemVus.FirstOrDefault(p => p.MaNV == MaNVu);
                txtTieuDe.Text = objNhv.TieuDe;
                txtDienGiai.Text = objNhv.DienGiai;
                dateNgayKT.EditValue = objNhv.NgayHH;
                dateNgayHT.EditValue = objNhv.NgayHT;
                dateNgayBD.EditValue = objNhv.NgayBD;
                lookUpMucDo.EditValue = objNhv.MaMD;
                lookUpStatus.EditValue = objNhv.MaTT;
                LookupLoai.EditValue = objNhv.MaLNV;
                checkBNhacViec.Checked = objNhv.IsNhac.GetValueOrDefault();
                lblNguoiCapNhat.Caption = string.Format("Người cập nhật: {0} | {1:dd/MM/yyyy}", BEE.ThuVien.Common.StaffName, DateTime.Now);
                lblNguoiTao.Caption = string.Format("Người tạo: {0} | {1:dd/MM/yyyy}",objNhv.NhanVien.HoTen, objNhv.NgayBD);
                spinHoanThanh.EditValue = objNhv.PhanTramHT ?? 0;
                btnRing.Tag = objNhv.Rings;
                if (objNhv.IsNhac.GetValueOrDefault())
                    dateNhacViec.Enabled = true;
                else
                    dateNhacViec.Enabled = false;
                if (objNhv.NgayNhac.Value.Year != 1)
                    dateNhacViec.DateTime = objNhv.NgayNhac.Value;
                btnKhachHang.Text = objNhv.MaKH == null ? "" : objNhv.KhachHang.IsPersonal.GetValueOrDefault() == true ? objNhv.KhachHang.HoKH + " " + objNhv.KhachHang.TenKH : objNhv.KhachHang.TenCongTy;
                btnKhachHang.Tag = objNhv.KhachHang.MaKH;
                //btnKhachHang.Text = objNhv.kh
              //  btnKhachHang.Tag = o.KhachHang.MaKH;
            }
            gcNhanVien.DataSource = objNhv.NhiemVu_NhanViens;
        
        }

        private void AddNew_frm_Load(object sender, EventArgs e)
        {
            LoadDictionary();
            dateNgayHT.Enabled = false;

            LoadDataV2();
            //if (MaNVu != 0)
            //{
            //    //LoadData();
            //}
            //else
            //{   
            //}
        }

        private void btnSaveClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtTieuDe.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Chủ đề]. Xin cảm ơn.");
                txtTieuDe.Focus();
                return;
            }

            if (dateNgayBD.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Ngày bắt đầu]. Xin cảm ơn.");
                dateNgayBD.Focus();
                return;
            }

            if (dateNgayKT.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Ngày kết thúc]. Xin cảm ơn.");
                dateNgayKT.Focus();
                return;
            }

            if (dateNgayBD.DateTime.CompareTo(dateNgayKT.DateTime) > 0)
            {
                DialogBox.Infomation("[Ngày kết thúc] phải lớn hơn [Ngày bắt đầu]. Vui lòng kiểm tra lại, xin cảm ơn.");
                dateNgayKT.Focus();
                return;
            }

            objNhv.TieuDe = txtTieuDe.Text.Trim();
            objNhv.MaTT = (int?)lookUpStatus.EditValue;
            objNhv.DienGiai = txtDienGiai.Text.Trim();
            if (btnKhachHang.Tag != null)
                objNhv.MaKH = int.Parse(btnKhachHang.Tag.ToString());
            else
                objNhv.MaKH = null;
            objNhv.MaLNV = (int?)LookupLoai.EditValue;
            //o.TienDo.MaTD = (short)lookTienDo.EditValue;
            objNhv.MaMD = (int?)lookUpMucDo.EditValue;
            objNhv.NgayBD = dateNgayBD.DateTime;
            objNhv.NgayHH = dateNgayKT.DateTime;
            objNhv.MaNV = BEE.ThuVien.Common.StaffID;
            objNhv.NguoiCapNhat = BEE.ThuVien.Common.StaffID;
            objNhv.PhanTramHT = Convert.ToSingle(spinHoanThanh.EditValue);
            objNhv.IsNhac = checkBNhacViec.Checked;
            if (dateNhacViec.Text != "")
                objNhv.NgayNhac = dateNhacViec.DateTime;
            if (btnRing.Tag != null)
                objNhv.Rings = btnRing.Tag.ToString();
            else
                objNhv.Rings = "";
            if (MaNVu == 0)
            {
                objNhv.MaNV = Common.StaffID;
                objNhv.NgayTao = db.GetSystemDate();
            }
            else
            {
                objNhv.NgayCapNhat = db.GetSystemDate();
                objNhv.NguoiCapNhat = Common.StaffID;
            }
            try
            {
                db.SubmitChanges();
                IsUpdate = true;
                DialogBox.Infomation("Dữ liệu đã cập nhập.");
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error("Đã có lỗi xảy ra: " + ex.Message);
            }
        }

        private void btnKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                var frm = new BEE.LichLamViec.Find_frm();
                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    btnKhachHang.Tag = frm.MaKH;
                    btnKhachHang.Text = frm.HoTen;
                }
            }
            else
            {
                btnKhachHang.Tag = 0;
                btnKhachHang.Text = "";
            }
        }

        private void btnFinish_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MaNVu != 0)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xác nhận hoàn thành nhiệm vụ này không?") == DialogResult.Yes)
                {
                    it.NhiemVuCls o = new it.NhiemVuCls();
                    o.MaNVu = MaNVu;
                    o.UpdateFinish();

                    LoadData();
                }
            }
        }

        private void chkRemine_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
            {
                dateNhacViec.Enabled = true;
                btnRing.Enabled = true;
            }
            else
            {
                dateNhacViec.Enabled = false;
                btnRing.Enabled = false;
            }
        }

        private void txtTieuDe_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit _New = (TextEdit)sender;
            this.Text = _New.Text + " - Nhiệm vụ";
        }

        private void btnRing_Click(object sender, EventArgs e)
        {
            Rings_frm frm = new Rings_frm();
            if (btnRing.Tag != null)
                frm.Rings = btnRing.Tag.ToString();
            frm.ShowDialog();
            if (frm.IsUpdate)
                btnRing.Tag = frm.Rings;
        }

        private void LookupLoai_EditValueChanged(object sender, EventArgs e)
        {
            //lookTienDo.Properties.DataSource = db.NhiemVu_TienDos.Where(p => p.MaLNV == (byte)LookupLoai.EditValue);
            //lookTienDo.ItemIndex = 0;
        }

        private void gvNhanVien_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var now=db.GetSystemDate();
            gvNhanVien.SetFocusedRowCellValue("NguoiGiao", Common.StaffID);
            gvNhanVien.SetFocusedRowCellValue("MaNV", Common.StaffID);
            gvNhanVien.SetFocusedRowCellValue("NgayGiao", now);
            gvNhanVien.SetFocusedRowCellValue("TuNgay", now);
            gvNhanVien.SetFocusedRowCellValue("DenNgay", now);
        }
    }
}