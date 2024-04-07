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


namespace BEE.THUCHI.PhieuChi
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }
        public int? MaKH { get; set; }
        public int? MaGD { get; set; }
        public bool IsBank { get; set; }
        public bool IsSave { get; set; }
        public int? ServiceID { get; set; }
        public int? MaPT { get; set; }
        public string SoPhieu { get; set; }
        public decimal? TienThu { get; set; }
        MasterDataContext db;
        pgcPhieuChi objPT;
        BEE.ThuVien.KhachHang objKH;
        void LoadNhanVienKho()
        {
            var db = new MasterDataContext();
        }
        void PhieuThuLoad()
        {
           
             
        }

        void PhieuThuAddNew()
        {
           
        }

        void PhieuThuEdit()
        {
            //if ((bool)itemEdit.Tag == false)
            //{
            //    DialogBox.Error("Bạn không có quyền sửa");
            //    return;
            //}
            PhieuThuLoad();
            PhieuThuEnable(true);
        }

        void PhieuThuEnable(bool enable)
        {
          
            itemAdd.Enabled = !enable;
            itemEdit.Enabled = !enable;
            itemSave.Enabled = enable;
            itemDelay.Enabled = enable;
        }

        

        public frmEdit()
        {
            InitializeComponent();
            
           // BEE.NGONNGU.Language.TranslateUserControl(this, barManager1);

            this.Load += new EventHandler(frmEdit_Load);

      
           // itemPrevious.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrevious_ItemClick);
            itemNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNext_ItemClick);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemSave_ItemClick);
            itemDelay.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelay_ItemClick);
            itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemClose_ItemClick);
        }

        void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void itemDelay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuThuEnable(false);
        }


        public string CatChuoi(string str)
        {
            var str1 = str.Replace(str.Substring(0, 4), "");
            return str1;
        }
        void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (spinThucThu.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập  số tiềh");
                spinThucThu.Focus();
                return;
            }
            if (txtHoTenKH.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập  khách hàng");
                txtHoTenKH.Focus();
                return;
            }
            objPT.SoPhieu = txtSoPhieu.Text;
            objPT.NgayChi = (DateTime?)dateNgayThu.EditValue;
            objPT.SoTien = (decimal?)spinThucThu.EditValue;
            objPT.MaKH = this.MaKH;
         
            objPT.NguoiNhan = txtNguoiNop.Text;
            objPT.DiaChi = txtDiaChi.Text;
            objPT.DienGiai = txtDienGiai.Text;
            objPT.ChungTuGoc = txtChungTu.Text;
            objPT.MaLoai = (int?)lookUpLoaiChi1.EditValue;
            if (this.MaPT == 0)
            {
                objPT.MaNV = Common.StaffID;
                objPT.MaPGC = this.MaGD;
                db.pgcPhieuChis.InsertOnSubmit(objPT);
            }
            db.SubmitChanges();
            
        }

        void KhachHang_Load()
        {

            txtHoTenKH.EditValue = objKH.HoKH + " " + objKH.TenKH;
            txtDiaChi.EditValue = objKH.ThuongTru;
            txtNguoiNop.EditValue = txtHoTenKH.EditValue;


        }
        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuThuEdit();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuThuAddNew();
        }

        void itemNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

       
        void frmEdit_Load(object sender, EventArgs e)
        {

          //  this.SDBID = Common.Permission(barManager1, 120);
            
            db = new MasterDataContext();
            lookUpNganHang.Properties.DataSource = db.NganHangs;
            //  lookLoaiTien.Properties.DataSource = db.LoaiTiens;
            var listTaiKhoan = db.TaiKhoans;
            lookUpTKCo.Properties.DataSource = listTaiKhoan;
            lookUpTKNo.Properties.DataSource = listTaiKhoan;
            lookUpLoaiChi1.Properties.DataSource = db.pgcLoaiPhieuThuChis.Where(p => p.IsPaid==null);
            // lookUpCompany.Properties.DataSource = db.Companies;

            if (this.ID != null)
            {
                objPT = db.pgcPhieuChis.Single(p => p.MaPC == this.ID);
                txtSoPhieu.EditValue = objPT.SoPhieu;
                dateNgayThu.EditValue = objPT.NgayChi;
                lookUpTKNo.EditValue = objPT.TKNo;
                lookUpTKCo.EditValue = objPT.TKCo;

             //   spinChietKhau.EditValue = objPT.ChietKhau;
                spinThucThu.EditValue = objPT.SoTien;

                spinTyGia.EditValue = objPT.TyGia;

                txtNguoiNop.EditValue = objPT.NguoiNhan;
                txtDiaChi.EditValue = objPT.DiaChi;
                try
                {
                    var obj = db.KhachHangs.SingleOrDefault(p => p.MaKH == objPT.MaKH);
                    txtHoTenKH.EditValue = obj.HoKH + " " + obj.TenKH;
                    objKH = obj;
                }
                catch
                {
                }
                txtDienGiai.EditValue = objPT.DienGiai;
                txtChungTu.EditValue = objPT.ChungTuGoc;

                //cmbHinhThuc.SelectedIndex = objPT.HinhThuc == false ? 0 : 1;
                //lookUpNganHang.EditValue = objPT.MaNH;

                MaKH = objPT.MaKH;

                //cmbOption.SelectedIndex = objPT.pgcPhieuGiuCho.IsRateOutIn.GetValueOrDefault() ? 0 : 1;
                //spinTyLeLS.EditValue = objPT.LaiMuon ?? 0;


                lookUpLoaiChi1.EditValue = objPT.MaLoai;
            }
            else
            {

                cmbHinhThuc.SelectedIndex = 0;
                lookUpNganHang.Enabled = false;

                objPT = new pgcPhieuChi();

                if (MaKH != null)
                {
                    try
                    {
                        var obj = db.KhachHangs.SingleOrDefault(p => p.MaKH == MaKH);
                        txtHoTenKH.EditValue = obj.HoKH + " " + obj.TenKH;
                        objKH = obj;
                    }
                    catch
                    {
                    }
                }
                spinThucThu.EditValue = this.TienThu;
                try
                {
                    var s = db.pgcPhieuThus.OrderByDescending(p => p.MaPT).FirstOrDefault().SoPhieu;
                    // var c = s.Select(
                    var sopc = Convert.ToInt32(s.Substring(3, 7));

                    var Ma = db.DinhDang(6, sopc + 1);
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
        }

        private void itemSave_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (spinThucThu.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập  số tiềh");
                spinThucThu.Focus();
                return;
            }
            if (txtHoTenKH.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập  khách hàng");
                txtHoTenKH.Focus();
                return;
            }
            objPT.SoPhieu = txtSoPhieu.Text;
            objPT.NgayChi = (DateTime?)dateNgayThu.EditValue;
            objPT.SoTien = (decimal?)spinThucThu.EditValue;
            objPT.MaKH = this.MaKH;

            objPT.NguoiNhan = txtNguoiNop.Text;
            objPT.DiaChi = txtDiaChi.Text;
            objPT.DienGiai = txtDienGiai.Text;
            objPT.ChungTuGoc = txtChungTu.Text;
            objPT.MaLoai = (int?)lookUpLoaiChi1.EditValue;
            if (this.ID == null)
            {
                objPT.MaNV = Common.StaffID;
                objPT.MaPGC = this.MaGD;
                db.pgcPhieuChis.InsertOnSubmit(objPT);
            }
            db.SubmitChanges();
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công");
            this.Close();
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

       

    }
}