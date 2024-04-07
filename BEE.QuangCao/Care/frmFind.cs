using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;

namespace BEE.QuangCao.Care
{
    public partial class frmFind : DevExpress.XtraEditors.XtraForm
    {
        public int SDBID = 0;
        public frmFind()
        {
            InitializeComponent();

            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        private void btnAction_Click(object sender, EventArgs e)
        {
            //var wait = DialogBox.WaitingForm();
            //try
            //{
            //    using (var db = new MasterDataContext())
            //    {
            //        ctlManager2 ctlFind;
            //        var main = (frmMain)this.Owner;
            //        var ctl = main.FindUserControlInTab("Tìm kiếm Khách hàng");
            //        if (ctl != null)
            //            ctlFind = (ctlManager2)ctl;
            //        else
            //        {
            //            ctlFind = new ctlManager2();
            //            ctlFind.Tag = "Tìm kiếm Khách hàng";
            //            main.ShowUserControlInTab(ctlFind);
            //        }

            //        var maDA = lookUpDuAn.Text == "" ? 0 : Convert.ToInt32(lookUpDuAn.EditValue);
            //        var maTinh = lookUpTinh.Text == "" ? 0 : Convert.ToInt32(lookUpTinh.EditValue);
            //        var maHuyen = lookUpHuyen.Text == "" ? 0 : Convert.ToInt32(lookUpHuyen.EditValue);
            //        var maQD = lookUpGioiTinh.Text == "" ? 0 : Convert.ToInt32(lookUpGioiTinh.EditValue);
            //        var maNN = lookUpNgheNghiep2.Text == "" ? 0 : Convert.ToInt32(lookUpNgheNghiep2.EditValue);
            //        var maNKH = lookUpLoaiKH2.Text == "" ? 0 : Convert.ToInt32(lookUpLoaiKH2.EditValue);

            //        switch (SDBID)
            //        {
            //            case 2://Theo phong ban
            //                var list2 = db.KhachHang_marketing(maDA, maHuyen, maTinh, maQD, maNKH, maNN, spinThuNhapTu.Value, spinThuNhapDen.Value, (int)spinThanhVien.Value, (int)spinTuoiTu.Value, (int)spinTuoiDen.Value, BEE.ThuVien.Common.DepartmentID, 0, 0).ToList();

            //        ctlFind.Search(list2);
            //                break;
            //            case 3://Theo nhom
            //                var list3 = db.KhachHang_marketing(maDA, maHuyen, maTinh, maQD, maNKH, maNN, spinThuNhapTu.Value, spinThuNhapDen.Value, (int)spinThanhVien.Value, (int)spinTuoiTu.Value, (int)spinTuoiDen.Value, 0, BEE.ThuVien.Common.GroupID, 0).ToList();

            //        ctlFind.Search(list3);
            //                break;
            //            case 4://Theo nhan vien
            //                var list4 = db.KhachHang_marketing(maDA, maHuyen, maTinh, maQD, maNKH, maNN, spinThuNhapTu.Value, spinThuNhapDen.Value, (int)spinThanhVien.Value, (int)spinTuoiTu.Value, (int)spinTuoiDen.Value, 0, 0, BEE.ThuVien.Common.StaffID).ToList();

            //        ctlFind.Search(list4);
            //                break;
            //            default://Tat ca
            //                var list5 = db.KhachHang_marketing(maDA, maHuyen, maTinh, maQD, maNKH, maNN, spinThuNhapTu.Value, spinThuNhapDen.Value, (int)spinThanhVien.Value, (int)spinTuoiTu.Value, (int)spinTuoiDen.Value, 0, 0, 0).ToList();
            //                ctlFind.Search(list5);
            //                break;
            //        }
            //    }
            //}
            //catch { }
            //finally { wait.Close(); }
        }

        private void frmFind_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookUpTinh.Properties.DataSource = db.Tinhs.ToList();
                lookUpDuAn.Properties.DataSource = db.DuAn_getAllAndBlank();
                lookUpGioiTinh.Properties.DataSource = db.QuyDanh_getAllAndBlank();
                lookUpLoaiKH2.Properties.DataSource = db.NhomKH_getAllAndBlank();
                lookUpNgheNghiep2.Properties.DataSource = db.NgheNghiep_getAllAndBlank();
                lookUpTinh.ItemIndex = 0;
                lookUpDuAn.ItemIndex = 0;
                lookUpGioiTinh.ItemIndex = 0;
                lookUpLoaiKH2.ItemIndex = 0;
                lookUpNgheNghiep2.ItemIndex = 0;
                lookUpHuyen.ItemIndex = 0;
            }
        }

        private void lookUpTinh_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookUpHuyen.Properties.DataSource = db.Huyens.Where(p => p.MaTinh == (byte)lookUpTinh.EditValue).OrderBy(p => p.TenHuyen).ToList();
            }
        }

        private void btnReType_Click(object sender, EventArgs e)
        {
            lookUpTinh.ItemIndex = 0;
            lookUpDuAn.ItemIndex = 0;
            lookUpGioiTinh.ItemIndex = 0;
            lookUpLoaiKH2.ItemIndex = 0;
            lookUpNgheNghiep2.ItemIndex = 0;
            lookUpHuyen.ItemIndex = 0;
            spinTuoiDen.EditValue = 0;
            spinTuoiTu.EditValue = 0;
            spinThanhVien.EditValue = 0;
            spinThuNhapDen.EditValue = 0;
            spinThuNhapTu.EditValue = 0;
        }
    }
}
