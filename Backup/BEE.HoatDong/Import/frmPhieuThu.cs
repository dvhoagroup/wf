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
using BEE.ThuVien;
using System.Data.Linq.SqlClient;
using BEEREMA;

namespace BEE.HoatDong.Import
{
    public partial class frmPhieuThu : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;

        public frmPhieuThu()
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
                    grvPhieuThu.Columns.Clear();
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
                        grvPhieuThu.Columns.Add(col);
                    }
                    gcPhieuThu.DataSource = tblExcel;
                }
            }
            else
            {
                gcPhieuThu.DataSource = null;
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcPhieuThu.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();

            for (int i = 0; i < grvPhieuThu.RowCount; i++)
            {
                try
                {
                    grvPhieuThu.UnselectRow(i);

                    var soGC = grvPhieuThu.GetRowCellValue(i, "SoGiuCho").ToString();
                    var soDC = grvPhieuThu.GetRowCellValue(i, "SoDatCoc").ToString();
                    var soVV = grvPhieuThu.GetRowCellValue(i, "SoGopVon").ToString();
                    var soHD = grvPhieuThu.GetRowCellValue(i, "SoMuaBan").ToString();
                    var maSP = grvPhieuThu.GetRowCellValue(i, "MaSanPham").ToString();

                    var array = soHD.Split('/');
                    var maHD = array[0];

                    if (soGC == "" & soDC == "" & soVV == "" & soHD == "")
                        throw new Exception("Vui lòng nhập số phiếu, số hợp đồng");

                    if (maSP == "")
                        throw new Exception("Vui lòng nhập [MaSanPham].");
                    
                    var objPT = new pgcPhieuThu();
                    objPT.SoPhieu = grvPhieuThu.GetRowCellValue(i, "SoPT").ToString();
                    objPT.NgayThu = getDate(grvPhieuThu.GetRowCellValue(i, "NgayThu").ToString());
                    if (objPT.NgayThu == null) objPT.NgayThu = DateTime.Now;
                    objPT.TKCo = grvPhieuThu.GetRowCellValue(i, "TKCo").ToString();
                    objPT.TKNo = grvPhieuThu.GetRowCellValue(i, "TKNo").ToString();
                    objPT.DienGiai = grvPhieuThu.GetRowCellValue(i, "DienGiai").ToString();
                    objPT.DotTT = grvPhieuThu.GetRowCellValue(i, "DotTT") != DBNull.Value ?
                        Convert.ToByte(grvPhieuThu.GetRowCellValue(i, "DotTT")) : (byte)0;
                    objPT.TienTT = GetValueDecimal(grvPhieuThu.GetRowCellValue(i, "TienTT").ToString());
                    objPT.PhuThu = GetValueDecimal(grvPhieuThu.GetRowCellValue(i, "PhuThu").ToString());
                    objPT.ChietKhau = GetValueDecimal(grvPhieuThu.GetRowCellValue(i, "ChietKhau").ToString());
                    objPT.LaiSuat = GetValueDecimal(grvPhieuThu.GetRowCellValue(i, "TienLai").ToString());
                    objPT.SoTien = objPT.TienTT + objPT.PhuThu + objPT.LaiSuat - objPT.ChietKhau;
                    objPT.NguoiNop = grvPhieuThu.GetRowCellValue(i, "NguoiNop").ToString();
                    objPT.DiaChi = grvPhieuThu.GetRowCellValue(i, "DiaChi").ToString();
                    objPT.ChungTuGoc = grvPhieuThu.GetRowCellValue(i, "ChungTuGoc").ToString();
                    objPT.HinhThuc = false;
                    objPT.MaNV = ThuVien.Common.StaffID;

                    if (objPT.TienTT <= 0)
                        throw new Exception("Ràng buộc [TienTT] >= 0.");
                    
                    using (var db = new MasterDataContext())
                    {
                        objPT.Company = db.Companies.SingleOrDefault(p => p.TenVT == grvPhieuThu.GetRowCellValue(i, "CongTy").ToString().Trim());
                        objPT.pgcLoaiPhieuThuChi = db.pgcLoaiPhieuThuChis.SingleOrDefault(p => p.Name == grvPhieuThu.GetRowCellValue(i, "LoaiThu").ToString().Trim());

                        //if (db.pgcPhieuThus.Where(p => p.SoPhieu == objPT.SoPhieu).Count() > 0)
                        //    throw new Exception("Phiếu thu đã tồn tại");
                        if (soGC != "")
                            objPT.MaPGC = db.pgcPhieuGiuChos.Where(p => p.SoPhieu == soGC).Select(p => p.MaPGC).ToList()[0];
                        else if (soDC != "")
                            objPT.MaPGC = db.pdcPhieuDatCocs.Where(p => p.SoPhieu == soDC).Select(p => p.MaPDC).ToList()[0];
                        else if (soVV != "")
                            objPT.MaPGC = db.vvbhHopDongs.Where(p => p.SoHDVV == soVV).Select(p => p.MaHDVV).ToList()[0];
                        else
                        {
                            var list = db.HopDongMuaBans.Where(p => SqlMethods.Like(p.SoHDMB, maHD + "/%") & p.pgcPhieuGiuCho.bdsSanPham.KyHieu == maSP).Select(p => new { p.MaHDMB, p.pgcPhieuGiuCho.MaKH }).ToList();
                            if (list.Count > 0)
                            {
                                objPT.MaPGC = list[0].MaHDMB;
                                objPT.MaKH = list[0].MaKH;
                                objPT.MaLoaiTien = 1;
                                objPT.TyGia = 1;
                            }
                            else
                                throw new Exception("[SoMuaBan] hoặc [MaSanPham] không chính xác.");
                        }

                        db.pgcPhieuThus.InsertOnSubmit(objPT);
                        db.SubmitChanges();

                        grvPhieuThu.SelectRow(i);
                    }
                }
                catch(Exception ex)
                {
                    grvPhieuThu.SetRowCellValue(i, "Error", ex.Message);
                }
            }

            grvPhieuThu.DeleteSelectedRows();

            wait.Close();

            DialogBox.Infomation("Dữ liệu đã được lưu");
        }

        decimal GetValueDecimal(string str)
        {
            try
            {
                return Convert.ToDecimal(str);
            }
            catch { }

            return 0;
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}