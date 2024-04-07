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
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;

        public frmImport()
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
                    grvKH.Columns.Clear();
                    DevExpress.XtraGrid.Columns.GridColumn col;
                    for (int i = 0; i < tblExcel.Columns.Count; i++)
                    {
                        if (tblExcel.Columns[i].Caption.IndexOf("F") == 0) continue;

                        col = new DevExpress.XtraGrid.Columns.GridColumn();
                        col.Caption = tblExcel.Columns[i].Caption;
                        col.FieldName = tblExcel.Columns[i].ColumnName;
                        col.OptionsColumn.AllowEdit = !(col.FieldName == "Error");
                        if (col.FieldName == "HoTen" | col.FieldName == "DiaChiLL" | col.FieldName == "DiaChiTT" |
                            col.FieldName == "Email" | col.FieldName == "Error")
                            col.Width = 200;
                        else
                            col.Width = 80;
                        col.VisibleIndex = i;
                        grvKH.Columns.Add(col);
                    }
                    gcKH.DataSource = tblExcel;
                }
            }
            else
            {
                gcKH.DataSource = null;
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcKH.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();

            for (int i = 0; i < grvKH.RowCount; i++)
            {
                try
                {
                    grvKH.UnselectRow(i);

                    var hoTen = grvKH.GetRowCellValue(i, "HoTen").ToString().Trim();
                    if(hoTen == "")
                        throw new Exception("Vui lòng nhập họ tên khách hàng");
                    var diDong = grvKH.GetRowCellValue(i, "DiDong").ToString().Trim();
                    //if (diDong == "")
                    //    throw new Exception("Vui lòng nhập số di động");
                    var soCMND = grvKH.GetRowCellValue(i, "CMND").ToString().Trim();
                    //if (soCMND == "")
                    //    throw new Exception("Vui lòng nhập số CMND");

                    var objKH = new BEE.ThuVien.KhachHang();
                    if (hoTen.Split(' ').Length > 1)
                    {
                        objKH.HoKH = hoTen.Substring(0, hoTen.LastIndexOf(' '));
                        objKH.TenKH = hoTen.Substring(hoTen.LastIndexOf(' ') + 1);
                    }
                    else
                    {
                        objKH.HoKH = "";
                        objKH.TenKH = hoTen;
                    }
                    objKH.DiDong = diDong;
                    objKH.DTCD = grvKH.GetRowCellValue(i, "DienThoai").ToString().Trim();
                    var ngaySinh = grvKH.GetRowCellValue(i, "NgaySinh").ToString().Trim().Split('/');
                    if (ngaySinh.Count() > 0)
                    {
                        if (ngaySinh.Count() == 1)
                            objKH.yyyy = ngaySinh[0];
                        else
                        {
                            objKH.dd = ngaySinh[0];
                            objKH.MM = ngaySinh[1];
                            objKH.yyyy = ngaySinh[2];
                        }
                    }

                    objKH.NguyenQuan = grvKH.GetRowCellValue(i, "NoiSinh").ToString().Trim();
                    objKH.SoCMND = soCMND;
                    objKH.NgayCap = getDate(grvKH.GetRowCellValue(i, "NgayCap").ToString().Trim());
                    var ngayCap = grvKH.GetRowCellValue(i, "NgayCap").ToString().Trim().Split('/');
                    if (ngayCap.Count() > 0)
                    {
                        if (ngayCap.Count() == 1)
                            objKH.yyyy2 = ngayCap[0];
                        else
                        {
                            objKH.dd2 = ngayCap[0];
                            objKH.MM2 = ngayCap[1];
                            objKH.yyyy2 = ngayCap[2];
                        }
                    }
                    objKH.NoiCap = grvKH.GetRowCellValue(i, "NoiCap").ToString().Trim();                    
                    objKH.Email = grvKH.GetRowCellValue(i, "Email").ToString().Trim();
                    objKH.MaNV = BEE.ThuVien.Common.StaffID;
                    objKH.IsPersonal = true;
                    objKH.CongTy1 = grvKH.GetRowCellValue(i, "DonViCongTac").ToString().Trim();
                    
                    using (var db = new MasterDataContext())
                    {
                        if (db.KhachHangs.Where(p => p.DiDong == diDong & diDong != "").Count() > 0)
                            throw new Exception("Số di động đã có trong hệ thống");
                        if (db.KhachHangs.Where(p => p.SoCMND == soCMND & soCMND != "").Count() > 0)
                            throw new Exception("[Số CMND] đã có trong hệ thống");
                        var diaChi = grvKH.GetRowCellValue(i, "DiaChiLH").ToString().Trim();
                        var list = diaChi.Split(',');
                        if (list.Length >= 4)
                        {
                            try
                            {
                                objKH.DiaChi = list[0];
                                var tinh = db.Tinhs.Single(p => p.TenTinh == list[3].Trim());
                                objKH.MaTinh2 = tinh.MaTinh;
                                var huyen = db.Huyens.Single(p => p.TenHuyen == list[2].Trim() && p.MaTinh == tinh.MaTinh);
                                objKH.MaHuyen2 = huyen.MaHuyen;
                                var xa = db.Xas.Single(p => p.TenXa == list[1].Trim() && p.MaHuyen == huyen.MaHuyen);
                                objKH.MaXa2 = xa.MaXa;
                            }
                            catch { objKH.DiaChi = list[0];}
                        }
                        else
                            objKH.DiaChi = diaChi;


                        var thuongTru = grvKH.GetRowCellValue(i, "DiaChiTT").ToString().Trim();
                        var list2 = thuongTru.Split(',');
                        if (list2.Length >= 4)
                        {
                            try
                            {
                                objKH.ThuongTru = list2[0];
                                var tinh = db.Tinhs.Single(p => p.TenTinh == list2[3].Trim());
                                objKH.MaTinh = tinh.MaTinh;
                                var huyen = db.Huyens.Single(p => p.TenHuyen == list2[2].Trim() && p.MaTinh == tinh.MaTinh);
                                objKH.MaHuyen = huyen.MaHuyen;
                                var xa = db.Xas.Single(p => p.TenXa == list2[1].Trim() && p.MaHuyen == huyen.MaHuyen);
                                objKH.MaXa = xa.MaXa;
                            }
                            catch { objKH.ThuongTru = list2[0]; }
                        }
                        else
                            objKH.ThuongTru = thuongTru;

                        var quyDanh = db.QuyDanhs.Where(p => p.TenQD == grvKH.GetRowCellValue(i, "QuyDanh").ToString())
                            .Select(p => p.MaQD).ToList();
                        if (quyDanh.Count > 0)
                            objKH.MaQD = quyDanh[0];

                        var duAn = db.DuAns.Where(p => p.TenDA.ToLower() == grvKH.GetRowCellValue(i, "DuAn").ToString()).Select(p => p.MaDA).ToList(); ;
                        if (duAn.Count > 0)
                            objKH.MaDA = duAn[0];

                        var ngheNghiep = db.NgheNghieps.Where(p => p.TenNN.ToLower() == grvKH.GetRowCellValue(i, "NgheNghiep").ToString()).Select(p => p.MaNN).ToList();
                        if (ngheNghiep.Count > 0)
                            objKH.MaNN = ngheNghiep[0];

                        var nhomKH = db.NhomKHs.Where(p => p.TenNKH.ToLower() == grvKH.GetRowCellValue(i, "NhomKhachHang").ToString()).Select(p => p.MaNKH).ToList();
                        if (nhomKH.Count > 0)
                            objKH.MaNKH = nhomKH[0];

                        var loaiBDS = db.LoaiBDs.Where(p => p.TenLBDS.ToLower() == grvKH.GetRowCellValue(i, "LoaiBDS").ToString()).Select(p => p.MaLBDS).ToList();
                        if (loaiBDS.Count > 0)
                            objKH.MaLBDS = loaiBDS[0];

                        var nhanVien = db.NhanViens.Where(p => p.HoTen == grvKH.GetRowCellValue(i, "NhanVien").ToString())
                            .Select(p => p.MaNV).ToList();
                        if (nhanVien.Count > 0)
                            objKH.MaNV = nhanVien[0];

                        db.KhachHangs.InsertOnSubmit(objKH);
                        db.SubmitChanges();

                        grvKH.SelectRow(i);
                    }
                }
                catch(Exception ex)
                {
                    grvKH.SetRowCellValue(i, "Error", ex.Message);
                }
            }

            grvKH.DeleteSelectedRows();

            wait.Close();

            DialogBox.Infomation("Dữ liệu đã được lưu");
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question("Bạn có chắc chắn muốn xóa những dòng này không?") == System.Windows.Forms.DialogResult.Yes)
                grvKH.DeleteSelectedRows();
        }
    }
}