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

namespace BEE.SanPham
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public frmImport()
        {
            InitializeComponent();
        }

        public bool IsSave;
        dip.cmdExcel objExcel;

        MasterDataContext db = new MasterDataContext();

        void SanPham_Update(bdsSanPham sp, bdsSanPham objSP)
        {
            sp.MaLBDS = objSP.MaLBDS;
            sp.MaDA = objSP.MaDA;
            if (sp.MaTT != null)
                sp.MaTT = objSP.MaTT;
            if (objSP.MaHuong != null)
                sp.MaHuong = objSP.MaHuong;
            if (objSP.MaPL != null)
                sp.MaPL = objSP.MaPL;
            if (objSP.MaLD != null)
                sp.MaLD = objSP.MaLD;
            if (objSP.bdsTienIches.Count > 0)
                sp.bdsTienIches = objSP.bdsTienIches;
            if (objSP.bdsHangMucs.Count > 0)
            {
                sp.bdsHangMucs = objSP.bdsHangMucs;
                sp.ThanhTienHM = objSP.ThanhTienHM;
                sp.ThanhTien = sp.ThanhTienXD + sp.ThanhTienKV + sp.ThanhTienHM;
            }
            if (objSP.MaLT != null)
                sp.MaLT = objSP.MaLT;
            sp.DuongRong = objSP.DuongRong;
            sp.NgangKV = objSP.NgangKV;
            sp.DaiKV = objSP.DaiKV;
            sp.SauKV = objSP.SauKV;
            sp.NgangXD = objSP.NgangXD;
            sp.DaiXD = objSP.DaiXD;
            sp.SauXD = objSP.SauXD;
            if (objSP.MaNVKD != null)
                sp.MaNVKD = objSP.MaNVKD;
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            lookDuAn.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
            lookNVKD.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
            lookLoaiTien.DataSource = db.LoaiTiens;
            lookLoaiBDS.DataSource = db.LoaiBDs;
            lookHuong.DataSource = db.PhuongHuongs;
            lookPhapLy.DataSource = db.PhapLies;
            lookLoaiDuong.DataSource = db.LoaiDuongs;
            lookTrangThai.DataSource = db.bdsTrangThais.Where(p => p.MaTT < 3);
        }

        private void itemFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;
                objExcel = new dip.cmdExcel(file.FileName);
                string[] sheets = objExcel.GetExcelSheetNames();
                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));
                itemSheet.EditValue = null;
            }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue != null)
            {
                var wait = DialogBox.WaitingForm();
                DataTable tblSP = new DataTable();
                try
                {
                    tblSP = objExcel.ExcelSelect(itemSheet.EditValue.ToString() + "$").Tables[0];

                    wait.Hide();

                    frmImportUpdate frmUpdate = new frmImportUpdate();
                    frmUpdate.ShowDialog(this);
                    if (frmUpdate.DialogResult != DialogResult.OK) return;

                    wait.Show();
                    var khu = db.Khus.Where(p => p.MaDA == frmUpdate.objSP.MaDA).ToList();
                    lookKhu.DataSource = khu;
                    var phanKhu = db.PhanKhus.Where(p => p.Khu.MaDA == frmUpdate.objSP.MaDA).ToList();
                    lookPhanKhu.DataSource = phanKhu;

                    var ltSP = new List<bdsSanPham>();
                    foreach (DataRow r in tblSP.Rows)
                    {
                        try
                        {
                            bdsSanPham sp = new bdsSanPham();
                            SanPham_Update(sp, frmUpdate.objSP);
                            sp.KyHieu = r["KyHieu"] as string;
                            sp.Khu = khu.SingleOrDefault(p => p.TenKhu == r["Khu"] as string);
                            sp.PhanKhu = phanKhu.SingleOrDefault(p => p.TenPK == r["PhanKhu"] as string);
                            sp.SoTang = Convert.ToByte(r["Tang"] != DBNull.Value ? r["Tang"] : 0);
                            sp.LauSo = Convert.ToByte(r["ViTri"] != DBNull.Value ? r["ViTri"] : 0);
                            sp.MaLo = r["MaLo"] as string;
                            sp.TenDuong = r["Duong"] as string;
                            sp.TenLMK = r["Loai"] as string;
                            sp.DienGiai = r["MoTa"] as string;
                            if (sp.MaHuong == null)
                            {
                                var ltHuong = db.PhuongHuongs.Where(p => p.TenPhuongHuong == r["Huong"].ToString().Trim() as string).Select(p => p.MaPhuongHuong).ToList();
                                if (ltHuong.Count > 0)
                                {
                                    sp.MaHuong = ltHuong[0];
                                }
                            }
                            sp.HeSo = Convert.ToDecimal(r["HeSo"] != DBNull.Value ? r["HeSo"] : 0);
                            sp.DienTichXD = Convert.ToDecimal(r["DienTichXD"] != DBNull.Value ? r["DienTichXD"] : 0);
                            sp.DonGiaXD = Convert.ToDecimal(r["DonGiaXD"] != DBNull.Value ? r["DonGiaXD"] : 0);
                            sp.ThanhTienXD = Convert.ToDecimal(r["ThanhTienXD"] != DBNull.Value ? r["ThanhTienXD"] : 0);
                            sp.DienTichKV = Convert.ToDecimal(r["DienTichKV"] != DBNull.Value ? r["DienTichKV"] : 0);
                            sp.DonGiaKV = Convert.ToDecimal(r["DonGiaKV"] != DBNull.Value ? r["DonGiaKV"] : 0);
                            sp.ThanhTienKV = Convert.ToDecimal(r["ThanhTienKV"] != DBNull.Value ? r["ThanhTienKV"] : 0);
                            if (sp.ThanhTienHM == null) sp.ThanhTienHM = 0;
                            sp.ThanhTien = sp.ThanhTienXD + sp.ThanhTienKV + sp.ThanhTienHM;
                            sp.TyLeMG = Convert.ToDecimal(r["TyLeMG"] != DBNull.Value ? r["TyLeMG"] : 0);
                            sp.PhiMG = sp.ThanhTien * sp.TyLeMG / 100;
                            sp.PhongKhach = Convert.ToByte(r["PhongKhach"] != DBNull.Value ? r["PhongKhach"] : 0);
                            sp.PhongNgu = Convert.ToByte(r["PhongNgu"] != DBNull.Value ? r["PhongNgu"] : 0);
                            sp.PhongTam = Convert.ToByte(r["PhongTam"] != DBNull.Value ? r["PhongTam"] : 0);
                            sp.NgayNhap = DateTime.Now;
                            sp.NgayCN = DateTime.Now;
                            sp.MaNVKT = BEE.ThuVien.Common.StaffID;
                            if (sp.MaNVKD == null) sp.MaNVKD = BEE.ThuVien.Common.StaffID;
                            if (sp.MaTT == null) sp.MaTT = 1;
                            if (sp.MaLT == null) sp.MaLT = 1;
                            ltSP.Add(sp);
                        }
                        catch { }
                    }
                    gcSP.DataSource = ltSP;
                }
                catch { }
                finally
                {
                    tblSP.Dispose();
                    wait.Close();
                }
            }
            else
            {
                gcSP.DataSource = null;
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvSP.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng kéo chọn bất động sản");
                return;
            }

            frmImportUpdate frm = new frmImportUpdate();
            frm.ShowDialog();
            if (frm.DialogResult != DialogResult.OK) return;

            //var objSP = frm.objSP;
            var ltSP = (List<bdsSanPham>)gcSP.DataSource;
            foreach (var i in indexs)
            {
                var sp = (bdsSanPham)grvSP.GetRow(i);
                SanPham_Update(sp, frm.objSP);
            }

            grvSP.RefreshData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question() == DialogResult.No) return;
            grvSP.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var ltSP = (List<bdsSanPham>)gcSP.DataSource;
                db.bdsSanPhams.InsertAllOnSubmit(ltSP);
                db.SubmitChanges();

                gcSP.DataSource = null;
                this.IsSave = true;
                DialogBox.Infomation("Dữ liệu đã được lưu");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            wait.Close();
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}