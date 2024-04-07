using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using LandSoft.Library;
using System.Data.Linq.SqlClient;

namespace LandSoft.NghiepVu.Import
{
    public partial class frmTransfer : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;

        public frmTransfer()
        {
            InitializeComponent();
        }

        DateTime? getDate(string dateText)
        {
            if (dateText == "") return null;
            string[] ns = dateText.Split('/');
            if (ns.Length == 3)
            {
                if (int.Parse(ns[1]) > 12)
                    return new DateTime(int.Parse(ns[2].Substring(0, 4)), int.Parse(ns[0]), int.Parse(ns[1]));
                else
                    return new DateTime(int.Parse(ns[2].Substring(0, 4)), int.Parse(ns[1]), int.Parse(ns[0]));
            }
            else if (ns.Length == 2)
            {
                if (int.Parse(ns[1]) > 12)
                    return new DateTime(DateTime.Now.Year, int.Parse(ns[0]), int.Parse(ns[1]));
                else
                    return new DateTime(DateTime.Now.Year, int.Parse(ns[1]), int.Parse(ns[0]));
            }
            else
            {
                throw new Exception("Định dạng ngày tháng không đúng");
            }
        }

        private void itemExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                using (DataTable tblExcel = objExcel.ExcelSelect(itemSheet.EditValue.ToString() + "$").Tables[0])
                {
                    tblExcel.Columns.Add("Error", typeof(string));
                    grvChuyenNhuong.Columns.Clear();
                    DevExpress.XtraGrid.Columns.GridColumn col;
                    for (int i = 0; i < tblExcel.Columns.Count; i++)
                    {
                        if (tblExcel.Columns[i].Caption.IndexOf("F") == 0) continue;

                        col = new DevExpress.XtraGrid.Columns.GridColumn();
                        col.Caption = tblExcel.Columns[i].Caption;
                        col.FieldName = tblExcel.Columns[i].ColumnName;
                        if (col.FieldName == "DienGiai" | col.FieldName == "NguoiNop" |
                            col.FieldName == "DiaChi" | col.FieldName == "Error")
                            col.Width = 200;
                        else
                            col.Width = 80;
                        col.VisibleIndex = i;
                        grvChuyenNhuong.Columns.Add(col);
                    }
                    gcChuyenNhuong.DataSource = tblExcel;
                }
            }
            else
            {
                gcChuyenNhuong.DataSource = null;
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcChuyenNhuong.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();

            for (int i = 0; i < grvChuyenNhuong.RowCount; i++)
            {
                try
                {
                    grvChuyenNhuong.UnselectRow(i);

                    var soCMND = grvChuyenNhuong.GetRowCellValue(i, "SoCMNDChuyen").ToString();
                    if (soCMND == "") throw new Exception("Vui lòng nhập số CMND bên chuyển");
                    var hoTen = grvChuyenNhuong.GetRowCellValue(i, "HoTenKHChuyen").ToString();
                    if (hoTen == "") throw new Exception("Vui lòng nhập họ tên khách hàng chuyển");
                    var phanLoai = grvChuyenNhuong.GetRowCellValue(i, "PhanLoaiKHChuyen").ToString().ToLower();
                    if (phanLoai == "") throw new Exception("Vui lòng nhập phân loại khách hàng chuyển");

                    var soCMND2 = grvChuyenNhuong.GetRowCellValue(i, "SoCMNDNhan").ToString();
                    if (soCMND2 == "") throw new Exception("Vui lòng nhập số CMND bên nhận");
                    var hoTen2 = grvChuyenNhuong.GetRowCellValue(i, "HoTenKHNhan").ToString();
                    if (hoTen2 == "") throw new Exception("Vui lòng nhập họ tên khách hàng nhận");
                    var phanLoai2 = grvChuyenNhuong.GetRowCellValue(i, "PhanLoaiKHNhan").ToString().ToLower();
                    if (phanLoai2 == "") throw new Exception("Vui lòng nhập phân loại khách hàng nhận");                    

                    var soHD = grvChuyenNhuong.GetRowCellValue(i, "SoHopDong").ToString();
                    if (soHD == "")
                        throw new Exception("Vui lòng nhập số hợp đồng.");
                    
                    var objPT = new cnChuyenNhuong();
                    objPT.SoCN = grvChuyenNhuong.GetRowCellValue(i, "SoBBCN").ToString();
                    objPT.NgayCN = getDate(grvChuyenNhuong.GetRowCellValue(i, "NgayChuyenNhuong").ToString());
                    if (objPT.NgayCN == null) objPT.NgayCN = DateTime.Now;

                    objPT.NgayNhap = DateTime.Now;                    
                    objPT.MaNV = objPT.MaNVN = Library.Common.StaffID;
                    
                    using (var db = new MasterDataContext())
                    {
                        int? maKH = null, maKH2 = null;
                        if (phanLoai == "cá nhân")
                        {
                            var kh = db.KhachHangs.Where(p => p.SoCMND == soCMND & p.HoKH + " " + p.TenKH == hoTen).Select(p => p.MaKH).ToList();
                            if (kh.Count <= 0)
                                throw new Exception("Khách hàng chuyển không tồn tại.");
                            maKH = kh[0];
                        }
                        else
                        {
                            var kh = db.KhachHangs.Where(p => p.MaSoThueCT == soCMND & p.TenCongTy == hoTen).Select(p => p.MaKH).ToList();
                            if (kh.Count <= 0)
                                throw new Exception("Khách hàng chuyển không tồn tại.");
                            maKH = kh[0];
                        }

                        if (phanLoai2 == "cá nhân")
                        {
                            var kh2 = db.KhachHangs.Where(p => p.SoCMND == soCMND2 & p.HoKH + " " + p.TenKH == hoTen2).Select(p => p.MaKH).ToList();
                            if (kh2.Count <= 0)
                                throw new Exception("Khách hàng nhận không tồn tại.");
                            maKH2 = kh2[0];
                        }
                        else
                        {
                            var kh2 = db.KhachHangs.Where(p => p.MaSoThueCT == soCMND2 & p.TenCongTy == hoTen2).Select(p => p.MaKH).ToList();
                            if (kh2.Count <= 0)
                                throw new Exception("Khách hàng nhận không tồn tại.");
                            maKH2 = kh2[0];
                        }

                        var array = soHD.Split('/');
                        var maHD = Convert.ToInt32(array[0]);

                        var maSP = grvChuyenNhuong.GetRowCellValue(i, "MaSanPham").ToString();
                        var objHD = db.HopDongMuaBans.Where(p => SqlMethods.Like(p.SoHDMB, maHD + "/%") & p.pgcPhieuGiuCho.bdsSanPham.KyHieu == maSP).Select(p => p.MaHDMB).ToList();
                        if(objHD.Count <= 0)
                            throw new Exception("Hợp đồng không tồn tại.");
                        objPT.MaPGC = objHD[0];

                        objPT.MaKHCN = maKH;
                        objPT.MaKHNCN = maKH2;
                        objPT.MaLCN = 2;
                        objPT.MaTT = 6;
                        objPT.MaLT = 1;
                        objPT.DienGiai = "";
                        objPT.TienCL = 0;
                        objPT.TienKhac = 0;
                        objPT.GiaTriCN = 0;

                        var objGC = db.pgcPhieuGiuChos.SingleOrDefault(p => p.MaPGC == objHD[0]);
                        if (objGC != null)
                        {
                            objGC.MaKH = objPT.MaKHNCN;
                        }
                        try
                        {
                            objPT.TienCDT = grvChuyenNhuong.GetRowCellValue(i, "DaNop").ToString() == "" ? 0 : Convert.ToDecimal(grvChuyenNhuong.GetRowCellValue(i, "SoBBCN").ToString());
                        }
                        catch { objPT.TienCDT = 0; }
                        try
                        {
                            objPT.PhiChuyenNhuong = grvChuyenNhuong.GetRowCellValue(i, "PhiChuyenNhuong").ToString() == "" ? 0 : Convert.ToDecimal(grvChuyenNhuong.GetRowCellValue(i, "PhiChuyenNhuong").ToString());
                        }
                        catch { objPT.PhiChuyenNhuong = 0; }
                        try
                        {
                            objPT.ConNo = grvChuyenNhuong.GetRowCellValue(i, "ConNo").ToString() == "" ? 0 : Convert.ToDecimal(grvChuyenNhuong.GetRowCellValue(i, "ConNo").ToString());
                        }
                        catch { objPT.ConNo = 0; }
                        try
                        {
                            objPT.NoQuaHan = grvChuyenNhuong.GetRowCellValue(i, "NoQuaHan").ToString() == "" ? 0 : Convert.ToDecimal(grvChuyenNhuong.GetRowCellValue(i, "NoQuaHan").ToString());
                        }
                        catch { objPT.NoQuaHan = 0; }
                        try
                        {
                            objPT.LaiChamNop = grvChuyenNhuong.GetRowCellValue(i, "LaiChamNop").ToString() == "" ? 0 : Convert.ToDecimal(grvChuyenNhuong.GetRowCellValue(i, "LaiChamNop").ToString());
                        }
                        catch { objPT.LaiChamNop = 0; }

                        db.cnChuyenNhuongs.InsertOnSubmit(objPT);
                        db.SubmitChanges();

                        grvChuyenNhuong.SelectRow(i);
                    }
                }
                catch(Exception ex)
                {
                    grvChuyenNhuong.SetRowCellValue(i, "Error", ex.Message);
                }
            }

            grvChuyenNhuong.DeleteSelectedRows();

            wait.Close();

            DialogBox.Infomation("Dữ liệu đã được lưu");
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}