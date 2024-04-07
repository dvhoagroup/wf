using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.Import
{
    public partial class frmLTT : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;

        public frmLTT()
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
                    grvLTT.Columns.Clear();
                    DevExpress.XtraGrid.Columns.GridColumn col;
                    for (int i = 0; i < tblExcel.Columns.Count; i++)
                    {
                        if (tblExcel.Columns[i].Caption.IndexOf("F") == 0) continue;

                        col = new DevExpress.XtraGrid.Columns.GridColumn();
                        col.Caption = tblExcel.Columns[i].Caption;
                        col.FieldName = tblExcel.Columns[i].ColumnName;
                        col.Width = 80;
                        col.VisibleIndex = i;
                        grvLTT.Columns.Add(col);
                    }
                    gcLTT.DataSource = tblExcel;
                }
            }
            else
            {
                gcLTT.DataSource = null;
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
            if (gcLTT.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();

            for (int i = 0; i < grvLTT.RowCount; i++)
            {
                try
                {
                    if (grvLTT.GetRowCellValue(i, "SoDC").ToString() == "" && grvLTT.GetRowCellValue(i, "SoHD").ToString() == "")
                    {
                        continue;
                    }

                    int lttCount = (grvLTT.Columns.Count - 2) / 3;
                    for (int j = 1; j <= lttCount; j++)
                    {
                        using (SqlCommand sqlCmd = new SqlCommand("Import_LTT"))
                        {
                            sqlCmd.Parameters.AddWithValue("SoDC", grvLTT.GetRowCellValue(i, "SoDC").ToString());
                            sqlCmd.Parameters.AddWithValue("SoHD", grvLTT.GetRowCellValue(i, "SoHD").ToString());
                            sqlCmd.Parameters.AddWithValue("DotTT", j);
                            sqlCmd.Parameters.AddWithValue("TyLeTT", grvLTT.GetRowCellValue(i, "TyleTT" + j));
                            sqlCmd.Parameters.AddWithValue("SoTien", grvLTT.GetRowCellValue(i, "TienTT" + j));
                            sqlCmd.Parameters.Add(getNgay("NgayTT", grvLTT.GetRowCellValue(i, "NgayTT" + j).ToString()));
                            if (sqlCmd.Parameters["NgayTT"].Value == DBNull.Value)
                                sqlCmd.Parameters.AddWithValue("DienGiai", grvLTT.GetRowCellValue(i, "NgayTT" + j).ToString());
                            else
                                sqlCmd.Parameters.AddWithValue("DienGiai", "");
                            it.SqlCommon.exeCuteNonQueryPro(sqlCmd);
                        }
                    }
                }
                catch
                {
                    if (DialogBox.Question("Đã có lỗi xảy ra. Bạn có muốn tiếp tục không?") == DialogResult.No) break;
                }
            }

            gcLTT.DataSource = null;

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