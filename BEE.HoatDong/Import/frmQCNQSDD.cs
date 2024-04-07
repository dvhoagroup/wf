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

namespace LandSoft.NghiepVu.Import
{
    public partial class frmQCNQSDD : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;

        public frmQCNQSDD()
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
                    grvGCNQSDD.Columns.Clear();
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
                        grvGCNQSDD.Columns.Add(col);
                    }
                    gcGCNQSDD.DataSource = tblExcel;
                }
            }
            else
            {
                gcGCNQSDD.DataSource = null;
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcGCNQSDD.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();

            for (int i = 0; i < grvGCNQSDD.RowCount; i++)
            {
                try
                {
                    grvGCNQSDD.UnselectRow(i);

                    var soHD = grvGCNQSDD.GetRowCellValue(i, "SoHopDong").ToString();
                    if (soHD == "")
                        throw new Exception("Vui lòng nhập số hợp đồng");
                    
                    var objPT = new hdmbGiayChungNhanQSDD();
                    objPT.SoGCN = grvGCNQSDD.GetRowCellValue(i, "SoGCNQSDD").ToString();
                    objPT.SoSo = grvGCNQSDD.GetRowCellValue(i, "SoSo").ToString();
                    objPT.SoO = grvGCNQSDD.GetRowCellValue(i, "SoO").ToString();
                    objPT.NoiCap = grvGCNQSDD.GetRowCellValue(i, "NoiCap").ToString();
                    objPT.SoThua = grvGCNQSDD.GetRowCellValue(i, "SoThua").ToString();
                    objPT.DiaChiLoDat = grvGCNQSDD.GetRowCellValue(i, "DiaChiLoDat").ToString();
                    objPT.TinhTrangXayDung = grvGCNQSDD.GetRowCellValue(i, "TinhTrangXayDung").ToString();
                    objPT.NgayKyGCN = getDate(grvGCNQSDD.GetRowCellValue(i, "NgayKyGCNQSDD").ToString());
                    if (objPT.NgayKyGCN == null) objPT.NgayKyGCN = DateTime.Now;
                    objPT.NgayGuiThongBao = getDate(grvGCNQSDD.GetRowCellValue(i, "NgayGuiThongBao").ToString());
                    objPT.NgayKyBBNhanGCN = getDate(grvGCNQSDD.GetRowCellValue(i, "NgayKyBBNhanGCNQSDD").ToString());
                    objPT.GhiChu = grvGCNQSDD.GetRowCellValue(i, "GhiChu").ToString();

                    objPT.NgayTao = DateTime.Now;                    
                    objPT.MaNV = Library.Common.StaffID;
                    
                    using (var db = new MasterDataContext())
                    {
                        var objHD = db.HopDongMuaBans.Where(p => p.SoHDMB == soHD).Select(p => p.MaHDMB).ToList();
                        if(objHD.Count <= 0)
                            throw new Exception("Hợp đồng không tồn tại");
                        objPT.MaGCN = objHD[0];

                        db.hdmbGiayChungNhanQSDDs.InsertOnSubmit(objPT);
                        db.SubmitChanges();

                        grvGCNQSDD.SelectRow(i);
                    }
                }
                catch(Exception ex)
                {
                    grvGCNQSDD.SetRowCellValue(i, "Error", ex.Message);
                }
            }

            grvGCNQSDD.DeleteSelectedRows();

            wait.Close();

            DialogBox.Infomation("Dữ liệu đã được lưu");
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvGCNQSDD.DeleteSelectedRows();
        }
    }
}