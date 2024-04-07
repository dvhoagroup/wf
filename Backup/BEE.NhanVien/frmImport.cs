using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.NhanVien
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;

        public frmImport()
        {
            InitializeComponent();
        }

        private void frmFull_Load(object sender, EventArgs e)
        {
            
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
                    grvNhanVien.Columns.Clear();
                    DevExpress.XtraGrid.Columns.GridColumn col;
                    for (int i = 0; i < tblExcel.Columns.Count; i++)
                    {
                        if (tblExcel.Columns[i].Caption.IndexOf("F") == 0) continue;

                        col = new DevExpress.XtraGrid.Columns.GridColumn();
                        col.Caption = tblExcel.Columns[i].Caption;
                        col.FieldName = tblExcel.Columns[i].ColumnName;
                        col.Width = 80;
                        col.VisibleIndex = i;
                        grvNhanVien.Columns.Add(col);
                    }
                    gcNhanVien.DataSource = tblExcel;
                }
            }
            else
            {
                gcNhanVien.DataSource = null;
            }
        }

        SqlParameter getNgay(string paraName, string ngay)
        {
            SqlParameter param = new SqlParameter(paraName, SqlDbType.DateTime);
            try
            {
                if (ngay.ToString() != "")
                {
                    int nam = 0;
                    if (int.TryParse(ngay, out nam))
                    {
                        param.Value = new DateTime(nam, 1, 1);
                    }
                    else
                    {
                        string[] strNgay = ngay.Split('/');
                        nam = strNgay[2].Length < 4 ? int.Parse(strNgay[2]) + 2000 : int.Parse(strNgay[2]);
                        param.Value = new DateTime(nam, int.Parse(strNgay[1]), int.Parse(strNgay[0]));
                    }
                }
                else
                {
                    param.Value = DBNull.Value;
                }
            }
            catch
            {
                param.Value = DBNull.Value;
            }

            return param;
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcNhanVien.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();

            using (DataTable tbl = (DataTable)gcNhanVien.DataSource)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    try
                    {
                        if (r["CMND"].ToString() == "") continue;

                        using (SqlCommand sqlCmd = new SqlCommand("NhanVien_Import"))
                        {
                            sqlCmd.Parameters.AddWithValue("HoTen", r["HoTen"]);
                            sqlCmd.Parameters.AddWithValue("TenCVVT", r["ChucDanh"]);
                            sqlCmd.Parameters.AddWithValue("MaSo", r["Code"].ToString());
                            sqlCmd.Parameters.AddWithValue("TenNKD", r["Nhom"]);
                            sqlCmd.Parameters.AddWithValue("HDHT", r["HDHT"].ToString());
                            sqlCmd.Parameters.AddWithValue("CCMG", r["CCMG"].ToString());
                            sqlCmd.Parameters.AddWithValue("NgayCCMG", r["NgayCCMG"].ToString());
                            sqlCmd.Parameters.AddWithValue("Email", r["Email"]);
                            sqlCmd.Parameters.AddWithValue("CMND", r["CMND"].ToString());
                            sqlCmd.Parameters.AddWithValue("NgayCap", r["NgayCap"].ToString());
                            sqlCmd.Parameters.AddWithValue("NoiCap", r["NoiCap"]);
                            sqlCmd.Parameters.AddWithValue("HKTT", r["HKTT"]);
                            sqlCmd.Parameters.AddWithValue("DCLL", r["DCLL"]);
                            sqlCmd.Parameters.AddWithValue("DienThoai", r["DienThoai"].ToString());
                            sqlCmd.Parameters.AddWithValue("MST", r["MST"].ToString());
                            sqlCmd.Parameters.AddWithValue("GioiTinh", r["GioiTinh"]);

                            it.SqlCommon.exeCuteNonQueryPro(sqlCmd);
                        }
                    }
                    catch
                    {
                        if (DialogBox.Question("Đã có lỗi xảy ra. Bạn có muốn tiếp tục không?") == DialogResult.No) break;
                    }
                }
            }

            gcNhanVien.DataSource = null;

            wait.Close();
            wait.Dispose();

            DialogBox.Infomation("Dữ liệu đã được lưu");
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}