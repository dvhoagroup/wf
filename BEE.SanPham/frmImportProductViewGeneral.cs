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

namespace BEE.SanPham
{
    public partial class frmImportProductViewGeneral : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;
        public int? ProjectID, ZoneID;

        public frmImportProductViewGeneral()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
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
                    tblExcel.Columns.Add("Error", typeof(string));
                    grvLTT.Columns.Clear();
                    DevExpress.XtraGrid.Columns.GridColumn col;
                    for (int i = 0; i < tblExcel.Columns.Count; i++)
                    {
                        if (tblExcel.Columns[i].Caption.IndexOf("F") == 0) continue;

                        col = new DevExpress.XtraGrid.Columns.GridColumn();
                        col.Caption = tblExcel.Columns[i].Caption;
                        col.FieldName = tblExcel.Columns[i].ColumnName;
                        col.Width = col.FieldName != "Error" ? 80 : 200;
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

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcLTT.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet.");
                return;
            }

            var wait = DialogBox.WaitingForm();

            for (int i = 0; i < grvLTT.RowCount; i++)
            {
                try
                {
                    grvLTT.UnselectRow(i);
                    using (var db = new MasterDataContext())
                    {
                        var obj = new ProductViewGeneral();
                        obj.ZoneID = ZoneID;
                        obj.ProjectID = ProjectID;
                        
                        obj.Col1 = grvLTT.GetRowCellValue(i, "Col1").ToString();
                        obj.Col2 = grvLTT.GetRowCellValue(i, "Col2").ToString();
                        obj.Col3 = grvLTT.GetRowCellValue(i, "Col3").ToString();
                        obj.Col4 = grvLTT.GetRowCellValue(i, "Col4").ToString();
                        obj.Col5 = grvLTT.GetRowCellValue(i, "Col5").ToString();
                        obj.Col6 = grvLTT.GetRowCellValue(i, "Col6").ToString();
                        obj.Col7 = grvLTT.GetRowCellValue(i, "Col7").ToString();
                        obj.Col8 = grvLTT.GetRowCellValue(i, "Col8").ToString();
                        obj.Col9 = grvLTT.GetRowCellValue(i, "Col9").ToString();
                        obj.Col10 = grvLTT.GetRowCellValue(i, "Col10").ToString();

                        obj.Col11 = grvLTT.GetRowCellValue(i, "Col11").ToString();
                        obj.Col12 = grvLTT.GetRowCellValue(i, "Col12").ToString();
                        obj.Col13 = grvLTT.GetRowCellValue(i, "Col13").ToString();
                        obj.Col14 = grvLTT.GetRowCellValue(i, "Col14").ToString();
                        obj.Col15 = grvLTT.GetRowCellValue(i, "Col15").ToString();
                        obj.Col16 = grvLTT.GetRowCellValue(i, "Col16").ToString();
                        obj.Col17 = grvLTT.GetRowCellValue(i, "Col17").ToString();
                        obj.Col18 = grvLTT.GetRowCellValue(i, "Col18").ToString();
                        obj.Col19 = grvLTT.GetRowCellValue(i, "Col19").ToString();
                        obj.Col20 = grvLTT.GetRowCellValue(i, "Col20").ToString();

                        obj.Col21 = grvLTT.GetRowCellValue(i, "Col21").ToString();
                        obj.Col22 = grvLTT.GetRowCellValue(i, "Col22").ToString();
                        obj.Col23 = grvLTT.GetRowCellValue(i, "Col23").ToString();
                        obj.Col24 = grvLTT.GetRowCellValue(i, "Col24").ToString();
                        obj.Col25 = grvLTT.GetRowCellValue(i, "Col25").ToString();
                        obj.Col26 = grvLTT.GetRowCellValue(i, "Col26").ToString();
                        obj.Col27 = grvLTT.GetRowCellValue(i, "Col27").ToString();
                        obj.Col28 = grvLTT.GetRowCellValue(i, "Col28").ToString();
                        obj.Col29 = grvLTT.GetRowCellValue(i, "Col29").ToString();
                        obj.Col30 = grvLTT.GetRowCellValue(i, "Col30").ToString();

                        obj.Col31 = grvLTT.GetRowCellValue(i, "Col31").ToString();
                        obj.Col32 = grvLTT.GetRowCellValue(i, "Col32").ToString();
                        obj.Col33 = grvLTT.GetRowCellValue(i, "Col33").ToString();
                        obj.Col34 = grvLTT.GetRowCellValue(i, "Col34").ToString();
                        obj.Col35 = grvLTT.GetRowCellValue(i, "Col35").ToString();
                        obj.Col36 = grvLTT.GetRowCellValue(i, "Col36").ToString();
                        obj.Col37 = grvLTT.GetRowCellValue(i, "Col37").ToString();
                        obj.Col38 = grvLTT.GetRowCellValue(i, "Col38").ToString();
                        obj.Col39 = grvLTT.GetRowCellValue(i, "Col39").ToString();
                        obj.Col40 = grvLTT.GetRowCellValue(i, "Col40").ToString();

                        obj.Col41 = grvLTT.GetRowCellValue(i, "Col41").ToString();
                        obj.Col42 = grvLTT.GetRowCellValue(i, "Col42").ToString();
                        obj.Col43 = grvLTT.GetRowCellValue(i, "Col43").ToString();
                        obj.Col44 = grvLTT.GetRowCellValue(i, "Col44").ToString();
                        obj.Col45 = grvLTT.GetRowCellValue(i, "Col45").ToString();
                        obj.Col46 = grvLTT.GetRowCellValue(i, "Col46").ToString();
                        obj.Col47 = grvLTT.GetRowCellValue(i, "Col47").ToString();
                        obj.Col48 = grvLTT.GetRowCellValue(i, "Col48").ToString();
                        obj.Col49 = grvLTT.GetRowCellValue(i, "Col49").ToString();
                        obj.Col50 = grvLTT.GetRowCellValue(i, "Col50").ToString();

                        obj.Col51 = grvLTT.GetRowCellValue(i, "Col51").ToString();
                        obj.Col52 = grvLTT.GetRowCellValue(i, "Col52").ToString();
                        obj.Col53 = grvLTT.GetRowCellValue(i, "Col53").ToString();
                        obj.Col54 = grvLTT.GetRowCellValue(i, "Col54").ToString();
                        obj.Col55 = grvLTT.GetRowCellValue(i, "Col55").ToString();
                        obj.Col56 = grvLTT.GetRowCellValue(i, "Col56").ToString();
                        obj.Col57 = grvLTT.GetRowCellValue(i, "Col57").ToString();
                        obj.Col58 = grvLTT.GetRowCellValue(i, "Col58").ToString();
                        obj.Col59 = grvLTT.GetRowCellValue(i, "Col59").ToString();
                        obj.Col60 = grvLTT.GetRowCellValue(i, "Col60").ToString();

                        obj.Col61 = grvLTT.GetRowCellValue(i, "Col61").ToString();
                        obj.Col62 = grvLTT.GetRowCellValue(i, "Col62").ToString();
                        obj.Col63 = grvLTT.GetRowCellValue(i, "Col63").ToString();
                        obj.Col64 = grvLTT.GetRowCellValue(i, "Col64").ToString();
                        obj.Col65 = grvLTT.GetRowCellValue(i, "Col65").ToString();
                        obj.Col66 = grvLTT.GetRowCellValue(i, "Col66").ToString();
                        obj.Col67 = grvLTT.GetRowCellValue(i, "Col67").ToString();
                        obj.Col68 = grvLTT.GetRowCellValue(i, "Col68").ToString();
                        obj.Col69 = grvLTT.GetRowCellValue(i, "Col69").ToString();
                        obj.Col70 = grvLTT.GetRowCellValue(i, "Col70").ToString();

                        obj.Col71 = grvLTT.GetRowCellValue(i, "Col71").ToString();
                        obj.Col72 = grvLTT.GetRowCellValue(i, "Col72").ToString();
                        obj.Col73 = grvLTT.GetRowCellValue(i, "Col73").ToString();
                        obj.Col74 = grvLTT.GetRowCellValue(i, "Col74").ToString();
                        obj.Col75 = grvLTT.GetRowCellValue(i, "Col75").ToString();
                        obj.Col76 = grvLTT.GetRowCellValue(i, "Col76").ToString();
                        obj.Col77 = grvLTT.GetRowCellValue(i, "Col77").ToString();
                        obj.Col78 = grvLTT.GetRowCellValue(i, "Col78").ToString();
                        obj.Col79 = grvLTT.GetRowCellValue(i, "Col79").ToString();
                        obj.Col80 = grvLTT.GetRowCellValue(i, "Col80").ToString();

                        obj.Col81 = grvLTT.GetRowCellValue(i, "Col81").ToString();
                        obj.Col82 = grvLTT.GetRowCellValue(i, "Col82").ToString();
                        obj.Col83 = grvLTT.GetRowCellValue(i, "Col83").ToString();
                        obj.Col84 = grvLTT.GetRowCellValue(i, "Col84").ToString();
                        obj.Col85 = grvLTT.GetRowCellValue(i, "Col85").ToString();
                        obj.Col86 = grvLTT.GetRowCellValue(i, "Col86").ToString();
                        obj.Col87 = grvLTT.GetRowCellValue(i, "Col87").ToString();
                        obj.Col88 = grvLTT.GetRowCellValue(i, "Col88").ToString();
                        obj.Col89 = grvLTT.GetRowCellValue(i, "Col89").ToString();
                        obj.Col90 = grvLTT.GetRowCellValue(i, "Col90").ToString();

                        obj.Col91 = grvLTT.GetRowCellValue(i, "Col91").ToString();
                        obj.Col92 = grvLTT.GetRowCellValue(i, "Col92").ToString();
                        obj.Col93 = grvLTT.GetRowCellValue(i, "Col93").ToString();
                        obj.Col94 = grvLTT.GetRowCellValue(i, "Col94").ToString();
                        obj.Col95 = grvLTT.GetRowCellValue(i, "Col95").ToString();
                        obj.Col96 = grvLTT.GetRowCellValue(i, "Col96").ToString();
                        obj.Col97 = grvLTT.GetRowCellValue(i, "Col97").ToString();
                        obj.Col98 = grvLTT.GetRowCellValue(i, "Col98").ToString();
                        obj.Col99 = grvLTT.GetRowCellValue(i, "Col99").ToString();
                        obj.Col100 = grvLTT.GetRowCellValue(i, "Col100").ToString();

                        db.ProductViewGenerals.InsertOnSubmit(obj);

                        db.SubmitChanges();

                        grvLTT.SelectRow(i);
                    }
                }
                catch(Exception ex)
                {
                    grvLTT.SetRowCellValue(i, "Error", ex.Message);
                }
            }

            grvLTT.DeleteSelectedRows();

            wait.Close();

            DialogBox.Infomation("Dữ liệu đã được lưu.");
        }

        decimal GetValueDecimal(string str)
        {
            try
            {
                return Convert.ToDecimal(str.Trim());
            }
            catch { return 0; }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvLTT.DeleteSelectedRows();
        }
    }
}