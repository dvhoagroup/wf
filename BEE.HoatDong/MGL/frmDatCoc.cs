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

namespace BEE.HoatDong.MGL
{
    public partial class frmDatCoc : DevExpress.XtraEditors.XtraForm
    {
        public int MaDC { get; set; }
        public bool IsBan { get; set; }

        private MasterDataContext db;
        private mglDatCoc objDC;
        private BEE.ThuVien.KhachHang objNguoiBan;
        private BEE.ThuVien.KhachHang objNguoiMua;
        
        public frmDatCoc()
        {
            InitializeComponent();
            db = new MasterDataContext();

            var nv = from p in db.NhanViens select new { MaNV = p.MaNV, MaSoNV = p.MaSo, HoTenNV = p.HoTen };
            lookKhaiThac.Properties.DataSource = nv;
            lookBanHang.Properties.DataSource = nv;
            lookLoaiTien.Properties.DataSource = db.LoaiTiens;
            lookLoaiBDS.Properties.DataSource = db.LoaiBDs;
        }

        private void frmDatCoc_Load(object sender, EventArgs e)
        {
            if (this.MaDC > 0)
            {
                objDC = db.mglDatCocs.Single(p => p.MaDC == this.MaDC);
                //Thong tinh phieu
                txtSoPhieu.EditValue = objDC.SoDC;
                dateNgayKy.EditValue = objDC.NgayDC;
                spinPhiMoiGioi.EditValue = objDC.PhiMoiGioi;
                cmbCachTinhPhi.SelectedIndex = Convert.ToInt32(objDC.MaCTP);
                lookKhaiThac.EditValue = objDC.MaNVKT;
                lookBanHang.EditValue = objDC.MaNVBH;
                //Bat dong san
                txtMaSoBDS.EditValue = objDC.MaBDS;
                spinDienTich.EditValue = objDC.DienTich;
                spinGiaBDS.EditValue = objDC.GiaBDS;
                txtDiaChiBDS.EditValue = objDC.DiaChiBDS;
                lookLoaiBDS.EditValue = objDC.MaLBDS;
                lookLoaiTien.EditValue = objDC.MaTyGia;
                //Khach hang ban, cho thue
                objNguoiBan = objDC.KhachHang;
                NguoiBan_Load();
                //Khach hang mua thue
                objNguoiMua = objDC.KhachHang1;
                NguoiMua_Load();
            }
            else
            {
                lookLoaiTien.ItemIndex = 0;
                lookLoaiBDS.ItemIndex = 0;
                string SoDC = "";
                db.mglDatCoc_TaoSoDC(ref SoDC);
                txtSoPhieu.Text = SoDC;
                dateNgayKy.DateTime = DateTime.Now;
            }
        }

        void NguoiBan_Load()
        {
            txtHoTenA.Tag = objNguoiBan.MaKH;
            txtHoTenA.Text = objNguoiBan.HoKH + " " + objNguoiBan.TenKH;
            if ((bool)objNguoiBan.IsYear)
                txtNgaySinhA.Text = objNguoiBan.NgaySinh.Value.Year.ToString();
            else
                txtNgaySinhA.Text = string.Format("{0:dd/MM/yyyy}", objNguoiBan.NgaySinh);
            txtCMNDA.Text = objNguoiBan.SoCMND;
            dateNgayCapA.DateTime = objNguoiBan.NgayCap.Value;
            txtNoiCapA.Text = objNguoiBan.NoiCap;
            txtDiDongA.Text = objNguoiBan.DiDong;
            txtDTCDA.Text = objNguoiBan.DTCD;
            txtDTCQA.Text = objNguoiBan.DTCoQuan;
            txtDCLLA.Text = objNguoiBan.DiaChi;
            txtDCTTA.Text = objNguoiBan.ThuongTru;
        }

        void NguoiMua_Load()
        {
            txtHoTenB.Tag = objNguoiMua.MaKH;
            txtHoTenB.Text = objNguoiMua.HoKH + " " + objNguoiMua.TenKH;
            if ((bool)objNguoiMua.IsYear)
                txtNgaySinhB.Text = objNguoiMua.NgaySinh.Value.Year.ToString();
            else
                txtNgaySinhB.Text = string.Format("{0:dd/MM/yyyy}", objNguoiMua.NgaySinh);
            txtCMNDB.Text = objNguoiMua.SoCMND;
            dateNgayCapB.DateTime = objNguoiMua.NgayCap.Value;
            txtNoiCapB.Text = objNguoiMua.NoiCap;
            txtDiDongB.Text = objNguoiMua.DiDong;
            txtDTCDB.Text = objNguoiMua.DTCD;
            txtDTCQB.Text = objNguoiMua.DTCoQuan;
            txtDCLLB.Text = objNguoiMua.DiaChi;
            txtDCTTB.Text = objNguoiMua.ThuongTru;
        }

        private void txtHoTenA_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                KhachHang.Find_frm frm = new KhachHang.Find_frm();
                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    objNguoiBan = db.KhachHangs.Single(p => p.MaKH == frm.MaKH);
                    NguoiBan_Load();
                }
            }
            else
            {
                txtHoTenA.Tag = null;
                txtHoTenA.Text = "";
                txtNgaySinhA.Text = "";
                txtCMNDA.Text = "";
                dateNgayCapA.EditValue = null;
                txtNoiCapA.Text = "";
                txtDiDongA.Text = "";
                txtDTCDA.Text = "";
                txtDTCQA.Text = "";
                txtDCLLA.Text = "";
                txtDCTTA.Text = "";
            }
        }

        private void txtHoTenB_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                KhachHang.Find_frm frm = new KhachHang.Find_frm();
                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    objNguoiMua = db.KhachHangs.Single(p => p.MaKH == frm.MaKH);
                    NguoiMua_Load();
                }
            }
            else
            {
                txtHoTenB.Tag = null;
                txtHoTenB.Text = "";
                txtNgaySinhB.Text = "";
                txtCMNDB.Text = "";
                dateNgayCapB.EditValue = null;
                txtNoiCapB.Text = "";
                txtDiDongB.Text = "";
                txtDTCDB.Text = "";
                txtDTCQB.Text = "";
                txtDCLLB.Text = "";
                txtDCTTB.Text = "";
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            #region Nguoi ban
            if (txtHoTenA.Text == "")
            {
                DialogBox.Error("Vui lòng nhập họ tên khách hàng");
                txtHoTenA.Focus();
                return;
            }
            if (txtNgaySinhA.Text == "")
            {
                DialogBox.Error("Vui lòng nhập ngày sinh");
                txtNgaySinhA.Focus();
                return;
            }
            if (txtCMNDA.Text == "")
            {
                DialogBox.Error("Vui lòng nhập số CMND");
                txtCMNDA.Focus();
                return;
            }
            if (dateNgayCapA.Text == "")
            {
                DialogBox.Error("Vui lòng nhập ngày cấp");
                dateNgayCapA.Focus();
                return;
            }
            if (txtNoiCapA.Text == "")
            {
                DialogBox.Error("Vui lòng nhập nới cấp");
                txtNoiCapA.Focus();
                return;
            }

            if (txtHoTenA.Tag == null) 
                objNguoiBan = new ThuVien.KhachHang();
            string HoTenKHA = txtHoTenA.Text;
            if (HoTenKHA.Split(' ').Length == 1)
            {
                objNguoiBan.HoKH = "";
                objNguoiBan.TenKH = HoTenKHA;
            }
            else
            {
                objNguoiBan.HoKH = HoTenKHA.Substring(0, HoTenKHA.LastIndexOf(' '));
                objNguoiBan.TenKH = HoTenKHA.Substring(HoTenKHA.LastIndexOf(' ') + 1);
            }

            int NamSinhA = 0;
            if (int.TryParse(txtNgaySinhA.Text, out NamSinhA))
            {
                objNguoiBan.NgaySinh = new DateTime(NamSinhA, 1, 1);
                objNguoiBan.IsYear = true;
            }
            else
            {
                try
                {
                    string[] ngayA = txtNgaySinhA.Text.Split('/');
                    objNguoiBan.NgaySinh = new DateTime(int.Parse(ngayA[2]), int.Parse(ngayA[1]), int.Parse(ngayA[0]));
                    objNguoiBan.IsYear = false;
                }
                catch
                {
                    DialogBox.Error("Ngày sinh không đúng định dạng (dd/MM/yyyy)");
                    txtNgaySinhA.Focus();
                    return;
                }
            }

            objNguoiBan.SoCMND = txtCMNDA.Text;
            objNguoiBan.NgayCap = dateNgayCapA.DateTime;
            objNguoiBan.NoiCap = txtNoiCapA.Text;
            objNguoiBan.DiDong = txtDiDongA.Text;
            objNguoiBan.DTCD = txtDTCDA.Text;
            objNguoiBan.DTCoQuan = txtDTCQA.Text;
            objNguoiBan.DiaChi = txtDCLLA.Text;
            objNguoiBan.ThuongTru = txtDCTTA.Text;
            #endregion

            #region Nguoi mua
            if (txtHoTenB.Text == "")
            {
                DialogBox.Error("Vui lòng nhập họ tên khách hàng");
                txtHoTenB.Focus();
                return;
            }
            if (txtNgaySinhB.Text == "")
            {
                DialogBox.Error("Vui lòng nhập ngày sinh");
                txtNgaySinhB.Focus();
                return;
            }
            if (txtCMNDB.Text == "")
            {
                DialogBox.Error("Vui lòng nhập số CMND");
                txtCMNDB.Focus();
                return;
            }
            if (dateNgayCapB.Text == "")
            {
                DialogBox.Error("Vui lòng nhập ngày cấp");
                dateNgayCapB.Focus();
                return;
            }
            if (txtNoiCapB.Text == "")
            {
                DialogBox.Error("Vui lòng nhập nới cấp");
                txtNoiCapB.Focus();
                return;
            }

            if (txtHoTenB.Tag == null)
                objNguoiMua = new BEE.ThuVien.KhachHang();
            string HoTenKHB = txtHoTenB.Text;
            if (HoTenKHB.Split(' ').Length == 1)
            {
                objNguoiMua.HoKH = "";
                objNguoiMua.TenKH = HoTenKHB;
            }
            else
            {
                objNguoiMua.HoKH = HoTenKHB.Substring(0, HoTenKHB.LastIndexOf(' '));
                objNguoiMua.TenKH = HoTenKHB.Substring(HoTenKHB.LastIndexOf(' ') + 1);
            }

            int NamSinhB = 0;
            if (int.TryParse(txtNgaySinhB.Text, out NamSinhB))
            {
                objNguoiMua.NgaySinh = new DateTime(NamSinhB, 1, 1);
                objNguoiMua.IsYear = true;
            }
            else
            {
                try
                {
                    string[] ngayB = txtNgaySinhB.Text.Split('/');
                    objNguoiMua.NgaySinh = new DateTime(int.Parse(ngayB[2]), int.Parse(ngayB[1]), int.Parse(ngayB[0]));
                    objNguoiMua.IsYear = false;
                }
                catch
                {
                    DialogBox.Error("Ngày sinh không đúng định dạng (dd/MM/yyyy)");
                    txtNgaySinhB.Focus();
                    return;
                }
            }

            objNguoiMua.SoCMND = txtCMNDB.Text;
            objNguoiMua.NgayCap = dateNgayCapB.DateTime;
            objNguoiMua.NoiCap = txtNoiCapB.Text;
            objNguoiMua.DiDong = txtDiDongB.Text;
            objNguoiMua.DTCD = txtDTCDB.Text;
            objNguoiMua.DTCoQuan = txtDTCQB.Text;
            objNguoiMua.DiaChi = txtDCLLB.Text;
            objNguoiMua.ThuongTru = txtDCTTB.Text;
            #endregion

            #region Dat coc
            if (txtMaSoBDS.Text == "")
            {
                DialogBox.Error("Vui lòng nhập mã số BĐS");
                txtMaSoBDS.Focus();
                return;
            }
            if (spinGiaBDS.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập giá mua/thuê");
                spinGiaBDS.Focus();
                return;
            }

            if (dateNgayKy.Text == "")
            {
                DialogBox.Error("Vui lòng nhập ngày ký");
                dateNgayKy.Focus();
                return;
            }

            if (lookKhaiThac.Text == "")
            {
                DialogBox.Error("Vui lòng chọn nhân viên khai thác");
                lookKhaiThac.Focus();
                return;
            }

            if (lookBanHang.Text == "")
            {
                DialogBox.Error("Vui lòng chọn nhân viên bán hàng");
                lookBanHang.Focus();
                return;
            }

            if (this.MaDC <= 0)
            {
                objDC = new mglDatCoc();
                objDC.IsBan = this.IsBan;
            }

            objDC.SoDC = txtSoPhieu.Text;
            objDC.NgayDC = dateNgayKy.DateTime;
            objDC.PhiMoiGioi = spinPhiMoiGioi.Value;
            objDC.MaCTP = Convert.ToByte(cmbCachTinhPhi.SelectedIndex);
            objDC.MaNVKT = (int)lookKhaiThac.EditValue;
            objDC.MaNVBH = (int)lookBanHang.EditValue;
            objDC.MaNV = Common.StaffID;
            objDC.KhachHang = objNguoiBan;
            objDC.KhachHang1 = objNguoiMua;
            objDC.MaBDS = txtMaSoBDS.Text;
            objDC.DienTich = spinDienTich.Value;
            objDC.GiaBDS = spinGiaBDS.Value;
            objDC.MaTyGia = (byte)lookLoaiTien.EditValue;
            objDC.DiaChiBDS = txtDiaChiBDS.Text;
            objDC.MaLBDS = (byte)lookLoaiBDS.EditValue;

            if (this.MaDC <= 0)
            {
                objDC.IsBan = this.IsBan;
                objDC.MaTT = 1;
                db.mglDatCocs.InsertOnSubmit(objDC);
            }
            #endregion

            try
            {
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Đã có lỗi xãy ra, vui lòng thử lại lần nữa");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}