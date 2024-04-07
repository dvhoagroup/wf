using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.PhieuThu
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }
        public int? MaBC { get; set; }
        public int? MaMT { get; set; }
        public int? GD_ID { get; set; }
        /// <summary>
        /// nguồn thu
        /// </summary>
        public int? MaNT { get; set; }

        MasterDataContext db;
        pgcPhieuThu objPT;
        BEE.ThuVien.KhachHang objKH;

        public frmEdit()
        {
            InitializeComponent();
        }                

        public string CatChuoi(string str)
        {
            var str1 = str.Replace(str.Substring(0, 4), "");
            return str1;
        }
        void KhachHang_Load()
        {
            txtHoTenKH.EditValue = objKH.HoKH + " " + objKH.TenKH;
        }
        void frmEdit_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            var listTaiKhoan = db.TaiKhoans;
            lookUpTKCo.Properties.DataSource = listTaiKhoan;
            lookUpTKNo.Properties.DataSource = listTaiKhoan;
            lkLoaiThu.Properties.DataSource = db.pgcLoaiPhieuThuChis.Where(p => p.IsPaid == true);

            var lstNguonThu = new List<NguonThu>();
            var obj1 = new NguonThu { ID = 1, Name = "Cần bán/cần cho thuê" };
            var obj2 = new NguonThu { ID = 2, Name = "Cần mua/cần thuê" };
            lstNguonThu.Add(obj1);
            lstNguonThu.Add(obj2);

            lookNguonThu.Properties.DataSource = lstNguonThu;
            cmbHinhThuc.SelectedIndex = 0;
            lookNguonThu.EditValue = MaNT;


            var objBc = db.mglbcBanChoThues.FirstOrDefault(p => p.MaBC == this.MaBC);
            if (objBc != null)
            {
                var objD = db.Duongs.FirstOrDefault(p => p.MaDuong == objBc.MaDuong);
                var objX = db.Xas.FirstOrDefault(p => p.MaXa == objBc.MaXa);
                var objH = db.Huyens.FirstOrDefault(p => p.MaHuyen == objBc.MaHuyen);
                var objT = db.Tinhs.FirstOrDefault(p => p.MaTinh == objBc.MaTinh);
                txtDiaChi.Text = objBc.SoNha + " " + objD?.TenDuong + ", " + objX?.TenXa + "," + objH.TenHuyen + ", " + objT.TenTinh;
            }

            if (this.MaMT != null)
            {
                var objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == this.MaMT);
                if (objMT != null)
                {
                    objKH = db.KhachHangs.FirstOrDefault(p => p.MaKH == objMT.MaKH);
                }
            }
            else
            {
                objKH = db.KhachHangs.FirstOrDefault(p => p.MaKH == objBc.MaKH);
            }
            KhachHang_Load();

            objPT = new pgcPhieuThu();
            try
            {
                var s = db.pgcPhieuThus.OrderByDescending(p => p.MaPT).FirstOrDefault().SoPhieu;
                var sopc = Convert.ToInt32(s.Substring(3, 7));

                var Ma = db.DinhDang(5, sopc + 1);
                txtSoPhieu.EditValue = Ma;
            }
            catch
            {
                string soPhieu = "";
                db.pgcPhieuThu_TaoSoPhieu(ref soPhieu);
                txtSoPhieu.EditValue = soPhieu;
            }

            try
            {
                dateNgayThu.EditValue = DateTime.Now;
            }
            catch
            {
            }
        }

        private void txtHoTenKH_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var ID = (int?)txtHoTenKH.EditValue;
                var obj = db.KhachHangs.SingleOrDefault(p => p.MaKH == ID);
                txtNguoiNop.Text = obj.TenKH;
                txtDiaChi.Text = obj.ThuongTru;
            }
            catch
            {
            }
        }

        private void itemAccept_Click(object sender, EventArgs e)
        {
            if (spinThucThu.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập số tiền");
                spinThucThu.Focus();
                return;
            }
            if (txtHoTenKH.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập khách hàng");
                txtHoTenKH.Focus();
                return;
            }
            objPT.SoPhieu = txtSoPhieu.Text;

            objPT.NgayThu = (DateTime?)dateNgayThu.EditValue;
            objPT.SoTien = (decimal?)spinThucThu.EditValue;
            objPT.JRNalCode = txtSoPhieu.Text;
            objPT.NguoiNop = txtNguoiNop.Text;
            objPT.DiaChi = txtDiaChi.Text;
            objPT.DienGiai = txtDienGiai.Text;
            objPT.ChungTuGoc = txtChungTu.Text;
            objPT.MaNT = (int?)lookNguonThu.EditValue;
            objPT.GDId = this.GD_ID;

            if (this.MaMT != null)
            {
                objPT.MaMT = this.MaMT;
            }
            else
            {
                objPT.MaBC = this.MaBC;
            }

            if (this.ID == null)
            {
                objPT.MaNV = Common.StaffID;
                db.pgcPhieuThus.InsertOnSubmit(objPT);
            }
            db.SubmitChanges();
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class NguonThu
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}