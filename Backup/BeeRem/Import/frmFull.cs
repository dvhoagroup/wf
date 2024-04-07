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
    public partial class frmFull : DevExpress.XtraEditors.XtraForm
    {
        dip.cmdExcel objExcel;

        public frmFull()
        {
            InitializeComponent();
        }

        private void frmFull_Load(object sender, EventArgs e)
        {
            lookDuAn.DataSource = new it.DuAnCls().List();
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
                gridControl1.DataSource = objExcel.ExcelSelect(itemSheet.EditValue.ToString() + "$").Tables[0];
            else
                gridControl1.DataSource = null;
        }

        SqlParameter getNgay(string paraName, string ngay)
        {
            SqlParameter param = new SqlParameter(paraName, SqlDbType.DateTime);
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

            return param;
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemDuAn.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn dự án");
                return;
            }
            if (gridControl1.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            using (DataTable tbl = (DataTable)gridControl1.DataSource)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    try
                    {
                        if (r["CMND"].ToString() == "" | r["MaCH"].ToString() == "") continue;

                        using (SqlCommand sqlCmd = new SqlCommand("ImportFull"))
                        {
                            string HoTenKH = r["HoTenKH"].ToString();
                            sqlCmd.Parameters.AddWithValue("HoKH", HoTenKH.Substring(0, HoTenKH.LastIndexOf(' ')));
                            sqlCmd.Parameters.AddWithValue("TenKH", HoTenKH.Substring(HoTenKH.LastIndexOf(' ') + 1));
                            sqlCmd.Parameters.AddWithValue("MaDA", itemDuAn.EditValue);
                            sqlCmd.Parameters.AddWithValue("Block", r["Block"]);
                            sqlCmd.Parameters.AddWithValue("Tang", r["Tang"]);
                            sqlCmd.Parameters.AddWithValue("MaCH", r["MaCH"]);
                            sqlCmd.Parameters.AddWithValue("DienTich", r["DienTich"]);
                            sqlCmd.Parameters.AddWithValue("DonGia", r["DonGia"]);
                            sqlCmd.Parameters.AddWithValue("GiaTriCK", r["ChietKhau"].ToString() != "" ? r["ChietKhau"] : 0);
                            byte MaDVCK;
                            switch (r["KieuCK"].ToString())
                            {
                                case "%": MaDVCK = 1; break;
                                case "m2": MaDVCK = 3; break;
                                default: MaDVCK = 2; break;
                            }
                            sqlCmd.Parameters.AddWithValue("MaDVCK", MaDVCK);
                            sqlCmd.Parameters.AddWithValue("PhuThu", r["PhuThu"].ToString() != "" ? r["PhuThu"] : 0);
                            sqlCmd.Parameters.Add(getNgay("NgaySinh", r["NgaySinh"].ToString()));
                            int namSinh = 0;
                            sqlCmd.Parameters.AddWithValue("IsYear", int.TryParse(r["NgaySinh"].ToString(), out namSinh));
                            sqlCmd.Parameters.AddWithValue("CMND", r["CMND"].ToString());
                            sqlCmd.Parameters.Add(getNgay("NgayCap", r["NgayCap"].ToString()));
                            sqlCmd.Parameters.AddWithValue("NoiCap", r["NoiCap"]);
                            sqlCmd.Parameters.AddWithValue("DCTT", r["DCTT"]);
                            sqlCmd.Parameters.AddWithValue("DCLL", r["DCLL"]);
                            sqlCmd.Parameters.AddWithValue("DienThoai", r["DienThoai"]);
                            sqlCmd.Parameters.Add(getNgay("NgayDC", r["NgayDC"].ToString()));
                            sqlCmd.Parameters.AddWithValue("SoDC", r["SoDC"].ToString());
                            sqlCmd.Parameters.Add(getNgay("NgayHD", r["NgayHD"].ToString()));
                            sqlCmd.Parameters.AddWithValue("SoHD", r["SoHD"].ToString());
                            sqlCmd.Parameters.AddWithValue("CMNDNV", r["CMNDNV"].ToString());
                            sqlCmd.Parameters.AddWithValue("MaNV", Properties.Settings.Default.StaffID);

                            it.SqlCommon.exeCuteNonQueryPro(sqlCmd);
                        }
                    }
                    catch
                    {
                        if (DialogBox.Question("Đã có lỗi xảy ra. Bạn có muốn tiếp tục không?") == DialogResult.No) break;
                    }
                }
            }

            gridControl1.DataSource = null;

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