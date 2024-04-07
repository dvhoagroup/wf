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

namespace LandSoft.CongVan.Send
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? ID;
        MasterDataContext db;
        CongVanDi objCV;
        public frmEdit()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmEdit_Load);
        }
        void frmEdit_Load(object sender, EventArgs e)
        {
            ctlLoaiCongVan1.LoadData();
            //ctlNhanVien1.LoadData();
            //ctlKhachHang1.LoadData();
            //radioGroup2.SelectedIndex = 1;
            if (this.ID != null)
                CongVanLoad();
            else
                CongVanAddNew();
            
        }
        void CongVanAddNew()
        {
            db = new MasterDataContext();
            objCV = new CongVanDi();
            dateNgayGui.EditValue = DateTime.Now;
            txtSoCV.EditValue = "cvd-"+((db.CongVanDis.Max(p => (int?)p.ID)??0)+1);// db.DinhDang(10, (db.bhPhieuThus.Max(p => (int?)p.ID) ?? 0) + 1);
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
            objCV = db.CongVanDis.Single(p => p.ID == this.ID);
            //if (objCV.KhachHang == null) {
            //    radioGroup2.SelectedIndex = 1;
            //ctlNhanVien1.EditValue = objCV.MaNVNhan;
            //}
            //else {
            //    radioGroup2.SelectedIndex = 0;
            //    ctlKhachHang1.EditValue = objCV.MaKHNhan;
            //}
            txtSoCV.Text = objCV.SoCV;
            dateNgayGui.EditValue = objCV.NgayGui;
            spinTienDo.Value = (decimal)objCV.TienDo;
            txtNoiDung.Text = objCV.NoiDung;
            ctlLoaiCongVan1.EditValue = objCV.MaLCV;
            txtHinhThucGui.Text = objCV.HinhThucGui;
            txtMaCan.Text = objCV.MaCan;
            txtNoiLuu.Text = objCV.NoiLuu;
            
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
                DialogBox.Error("Vui lòng nhập số công văn");
                txtSoCV.Focus();
                return;
            }
            if (ctlLoaiCongVan1.EditValue== null)
            {
                DialogBox.Error("Vui lòng chọn loại công văn");
                ctlLoaiCongVan1.Focus();
                return;
            }
            //if (ctlKhachHang1.EditValue == null && ctlNhanVien1.EditValue == null)
            //{
            //    DialogBox.Error("Vui lòng chọn người nhận là khách hàng hoặc nhân viên!");
            //    return;
            //}
            if (txtNoiDung.Text==txtNoiDung.Properties.NullText||txtNoiDung.Text.Trim()=="")
            {
                DialogBox.Error("Vui lòng nội dung công văn");
                txtNoiDung.Focus();
                return;
            }
           if (txtNoiLuu.Text.Trim()=="")
            {
                DialogBox.Error("Vui lòng nhập nơi lưu công văn");
                txtNoiLuu.Focus();
                return;
            }

            //if (radioGroup2.SelectedIndex == 1 && ctlNhanVien1.EditValue == null)
            //{
            //    DialogBox.Error("Vui lòng chọn nhân viên");
            //    ctlNhanVien1.Focus();
            //    return;
            //}
            //else
            //    {
            //        if (radioGroup2.SelectedIndex == 0&&ctlKhachHang1.EditValue == null) {

            //            DialogBox.Error("Vui lòng chọn khách hàng");
            //            ctlKhachHang1.Focus();
            //            return;
            //        }
            //    }
            #endregion

            using (var db = new MasterDataContext())
            {
                CongVanDi obj= new CongVanDi(); 
                if(this.ID!=null)
                obj = db.CongVanDis.Single(p => p.ID == objCV.ID);
                //if (radioGroup2.SelectedIndex == 1)
                //{
                //    //NhanVien nv= db.NhanViens.Single(p=>p.MaNV==);
                //    obj.MaNVNhan = (int)ctlNhanVien1.EditValue;
                //    obj.MaKHNhan = null;
                //}
                //else
                //{
                //    obj.MaNVNhan = null;
                //    obj.MaKHNhan = (int)ctlKhachHang1.EditValue;
                //}
                obj.NgayGui = (DateTime)dateNgayGui.EditValue;
                obj.MaLCV = ctlLoaiCongVan1.EditValue.ToString();
                obj.TienDo = (int)spinTienDo.Value;
                obj.NoiDung = txtNoiDung.Text.Trim();
                obj.MaCan = txtMaCan.Text.Trim();
                obj.HinhThucGui = txtHinhThucGui.Text.Trim();
                obj.NoiLuu = txtNoiLuu.Text.Trim();
                if (this.ID == null)
                {
                    obj.SoCV = txtSoCV.Text.Trim();
                    db.CongVanDis.InsertOnSubmit(obj);
                    obj.MaTT = "1";
                    obj.NguoiCN = null;
                    obj.NgayCN = null;
                    obj.NguoiGui = 1;
                }
                else
                {
                    obj.NguoiCN = 1; ///////////Nguoi sua
                    obj.NgayCN = DateTime.Now;
                    cvNhatKyXuLy xl = new cvNhatKyXuLy();
                    xl.MaNV = 1;
                    xl.NgayXL = DateTime.Now;
                    xl.MaTT = obj.MaTT;
                    xl.TienDo = obj.TienDo;
                    xl.DienGiai = "Cập nhật thông tin!";
                    obj.cvNhatKyXuLies.Add(xl);
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
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            CongVan_Save();
        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (radioGroup2.SelectedIndex == 1)
            //{
            //    ctlKhachHang1.Visible = false;
            //    ctlNhanVien1.Visible = true;
            //}
            //else
            //{
            //    ctlKhachHang1.Visible = true;
            //    ctlNhanVien1.Visible = false;
            //}
        }
    }
}