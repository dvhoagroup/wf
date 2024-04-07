using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace BEE.NhanVien
{
    public partial class NhanVien_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaNV { get; set; }
        public bool IsUpdate = false;
        MasterDataContext db;
        ThuVien.NhanVien objNV;
        //bool error = false;
        public NhanVien_frm()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadInfo()
        {
            lookUpChucVu.Properties.DataSource = db.ChucVus;
            lookUpNganHang.Properties.DataSource = db.NganHangs;
            lookUpNhomKD.Properties.DataSource = db.NhomKinhDoanhs;
            lookUpPhongBan.Properties.DataSource = db.PhongBans;
            lookUpQuyDanh.Properties.DataSource = db.QuyDanhs;
            lookQuanLy2.Properties.DataSource = lookQuanLy1.Properties.DataSource = db.NhanViens.Where(p => p.MaTinhTrang == 1).Select(p => new { p.MaNV, p.MaSo, p.HoTen });

        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.TranslateControl(this);

            lookTinhTrang.Properties.DataSource = db.NhanVien_TinhTrangs.Select(p => new { p.MaTT, p.TenTT });
            LoadInfo();
            //it.NhanVienCls o;
            if (this.MaNV != 0)
            {
                objNV = db.NhanViens.Single(p => p.MaNV == this.MaNV);
                txtHoTen.Text = objNV.HoTen;
                txtDienThoai.Text = objNV.DienThoai;
                txtEmail.Text = objNV.Email;
                txtHoTen.Text = objNV.HoTen;
                txtMaSo.Text = objNV.MaSo;
                txtMaSoThue.Text = objNV.MaTTNCN;
                txtNoiCap.Text = objNV.NoiCap;
                txtNguyenQuan.Text = objNV.HoKhau;
                txtSoCMND.Text = objNV.SoCMND;
                txtSoTaiKhoan.Text = objNV.SoTKNH;
                txtThuongTru.Text = objNV.DiaChi;
                dateNgayVaoLam.EditValue = objNV.NgayVaoLam;
                dateNgayNghiViec.EditValue = objNV.NgayNghiViec;
                txtDienThoai2.Text = objNV.DienThoai2;
                txtDienThoai3.Text = objNV.DienThoai3;
                lookUpChucVu.EditValue = objNV.MaCV;
                lookUpNganHang.EditValue = objNV.MaNH;
                lookUpNhomKD.EditValue = objNV.MaNKD;
                lookUpPhongBan.EditValue = objNV.MaPB;
                lookUpQuyDanh.EditValue = objNV.MaQD;
                lookTinhTrang.EditValue = objNV.MaTinhTrang;
                chkClock.Checked = (bool)objNV.Lock;
                txtNgayCap.Text = objNV.NgayCap;
                lookQuanLy1.EditValue = objNV.MaQL;
                lookQuanLy2.EditValue = objNV.MaQL2;
                dateNgaySinh.EditValue = objNV.NgaySinh;
                spinMuaHoaHong.EditValue = objNV.Rose;
                txtDienThoaiNB.Text = objNV.DienThoaiNB;
                txtDiaChiLH.Text = objNV.DiaChiLL;
                txtGhiChu.Text = objNV.Description;
            }
            else
            {
                objNV = new ThuVien.NhanVien();
                lookTinhTrang.ItemIndex = 0;
                lookUpQuyDanh.ItemIndex = 0;
                lookUpPhongBan.ItemIndex = 0;
                lookUpNhomKD.ItemIndex = 0;
                lookUpNganHang.ItemIndex = 0;
                lookUpChucVu.ItemIndex = 0;
                dateNgayVaoLam.EditValue = DateTime.Now;
                txtMaSo.EditValue = db.DinhDang(8, (db.NhanViens.Max(p => (int?)p.MaNV) ?? 0) + 1);
            }

            txtHoTen.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (lookQuanLy1.EditValue == lookQuanLy2.EditValue)
            {
                DialogBox.Warning("Bạn không thể quản lý chính bạn, vui lòng chọn lại.");
                return;
            }
            if (txtHoTen.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Họ tên], xin cảm ơn");
                txtHoTen.Focus();
                return;
            }
            var maSo = txtMaSo.Text.Trim();
            if (maSo == "")
            {
                DialogBox.Infomation("Vui lòng nhập [Mã số], xin cảm ơn");
                txtMaSo.Focus();
                return;
            }
            else
            {
                var count = db.NhanViens.Where(p => p.MaSo == maSo & p.MaNV != objNV.MaNV).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng [Mã số]. Vui lòng kiểm tra lại!");
                    txtMaSo.Focus();
                    return;
                }
            }

            if (dateNgayVaoLam.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày vào làm!");
                dateNgayVaoLam.Focus();
                return;
            }

            objNV.HoTen = txtHoTen.Text;
            objNV.HoKhau = txtNguyenQuan.Text;
            objNV.Lock = chkClock.Checked;
            objNV.DiaChi = txtThuongTru.Text;
            objNV.DienThoai = txtDienThoai.Text;
            objNV.Email = txtEmail.Text;
            //objNV.ChucVu.MaCV = byte.Parse(lookUpChucVu.EditValue.ToString());
            objNV.MaNKD = (byte?)lookUpNhomKD.EditValue;
            objNV.MaNH = (byte?)lookUpNganHang.EditValue;
            objNV.MaPB = (byte?)lookUpPhongBan.EditValue;
            objNV.MaQD = (byte?)lookUpQuyDanh.EditValue;
            objNV.MaCV = (byte?)lookUpChucVu.EditValue;
            objNV.MaSo = txtMaSo.Text;
            objNV.MaTTNCN = txtMaSoThue.Text;
            objNV.NoiCap = txtNoiCap.Text;
            objNV.NgayCap = txtNgayCap.Text;
            objNV.NgaySinh = (DateTime?)dateNgaySinh.EditValue;
            objNV.SoCMND = txtSoCMND.Text;
            objNV.SoNoiBo = "";
            objNV.SoTKNH = txtSoTaiKhoan.Text;
            objNV.Lock = chkClock.Checked;
            objNV.MaQL = (int?)lookQuanLy1.EditValue;
            objNV.MaQL2 = (int?)lookQuanLy2.EditValue;
            objNV.IsCDT = true;
            objNV.Rose = (decimal?)spinMuaHoaHong.EditValue;
            objNV.DiaChiLL = txtDiaChiLH.Text.Trim();
            objNV.DienThoaiNB = txtDienThoaiNB.Text.Trim();
            //objNV.ImgSignature = ImgSignature;
            //objNV.ImgAvatar = ImgAvatar;
            objNV.Description = txtGhiChu.Text;
            objNV.DienThoai3 = txtDienThoai3.Text.Trim();
            objNV.DienThoai2 = txtDienThoai2.Text.Trim();
            objNV.MaTinhTrang = (byte?)lookTinhTrang.EditValue;
            objNV.NgayNghiViec = (DateTime?)dateNgayNghiViec.EditValue;
            objNV.NgayVaoLam = (DateTime?)dateNgayVaoLam.EditValue;

            if (MaNV == 0)
            {
                objNV.MatKhau = it.CommonCls.MaHoa(txtMaSo.Text.Trim());
                objNV.NgayCN = DateTime.Now;
                db.NhanViens.InsertOnSubmit(objNV);
            }

            db.SubmitChanges();
            this.MaNV = objNV.MaNV;
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void picSignature_DoubleClick(object sender, EventArgs e)
        {
            //var frm = new FTP.frmUploadFile();
            //if (frm.SelectFile(true))
            //{
            //    frm.Folder = "doc/" + DateTime.Now.ToString("yyyy/MM/dd");
            //    frm.ClientPath = frm.ClientPath;
            //    frm.ShowDialog();
            //    if (frm.DialogResult != DialogResult.OK) return;

            //    ImgSignature = frm.FileName;
            //    picSignature.Image = Common.ImageLoad(frm.FileName);
            //}
            //frm.Dispose();
        }

        private void picAvatar_DoubleClick(object sender, EventArgs e)
        {
            //var frm = new FTP.frmUploadFile();
            //if (frm.SelectFile(true))
            //{
            //    frm.Folder = "doc/" + DateTime.Now.ToString("yyyy/MM/dd");
            //    frm.ClientPath = frm.ClientPath;
            //    frm.ShowDialog();
            //    if (frm.DialogResult != DialogResult.OK) return;

            //    picAvatar.Image = Common.ImageLoad(frm.FileName);
            //    ImgAvatar = frm.FileName;
            //}
            //frm.Dispose();
        }

    }
}