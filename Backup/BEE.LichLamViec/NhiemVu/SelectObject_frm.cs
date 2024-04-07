using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.CongViec.NhiemVu
{
    public partial class SelectObject_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaNVu = 0;
        public SelectObject_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectObject_frm_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        void LoadRecive()
        {
            it.NhiemVu_NhanVienCls o = new it.NhiemVu_NhanVienCls();
            o.MaNVu = MaNVu;
            o.MaNV = BEE.ThuVien.Common.StaffID;
            DataTable tblTemp = o.SelectBy2();
            tblTemp.Columns["MaNV"].Unique = true;
            gridControl2.DataSource = tblTemp;
            tblTemp.Dispose();
        }

        void LoadNhanVien()
        {
            //DialogBox.ShowWaitDialog("Vui lòng đợi trong giây lát...", "Hệ thống đang xử lý");
            it.NhanVienCls o = new it.NhanVienCls();
            switch (it.CommonCls.GetAccessData(BEE.ThuVien.Common.PerID, 15))
            {
                case 1://Tat ca                        
                    gridControl1.DataSource = o.SelectObjectAll();
                    break;
                case 2://Theo phong ban
                    o.PhongBan.MaPB = BEE.ThuVien.Common.DepartmentID;
                    gridControl1.DataSource = o.SelectObjectByDepartment();
                    break;
                case 3://Theo nhom
                    o.NKD.MaNKD = BEE.ThuVien.Common.GroupID;
                    gridControl1.DataSource = o.SelectObjectByGroup();
                    break;
                case 4://Theo nhan vien
                    o.MaNV = BEE.ThuVien.Common.StaffID;
                    gridControl1.DataSource = o.SelectObjectBy();
                    break;
                default:
                    gridControl1.DataSource = null;
                    break;
            }

            gridView1.ExpandAllGroups();

            LoadRecive();
            //try
            //{
            //    DialogBox.CloseWaitDialog();
            //}
            //catch { }
            //finally
            //{
            //    o = null;
            //    DialogBox.Dispose();
            //    System.GC.Collect();
            //}
        }

        private void btnRemoveOne_Click(object sender, EventArgs e)
        {            
            if (DialogBox.Question() == DialogResult.Yes)
            {
                int[] Rows = gridView2.GetSelectedRows();
                //string NotDelete = "";
                if (Rows.Length > 0)
                {
                    it.NhiemVu_NhanVienCls o;
                    foreach (int i in Rows)
                    {
                        //if (BEE.ThuVien.Common.StaffName == gridView2.GetRowCellValue(i, colNguoiGiao).ToString().Trim())
                        //{
                            o = new it.NhiemVu_NhanVienCls();
                            o.MaNVu = MaNVu;
                            try
                            {
                                o.MaNV = int.Parse(gridView2.GetRowCellValue(i, colMaNV).ToString());
                                o.Delete();
                                gridView2.DeleteRow(i);
                            }
                            catch
                            {
                                DialogBox.Infomation("Xóa không thành công vì: <Người nhận> đã có phát sinh <Lịch hẹn>\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                                return;
                            }
                        //}
                        //else
                        //    NotDelete += gridView2.GetRowCellValue(i, colHoTen2).ToString() + "\n";
                    }

                    //if (NotDelete != "")
                    //    DialogBox.Infomation(string.Format("Danh sách xóa không thành công:\n{0}", NotDelete));
                }
                else
                    DialogBox.Infomation("Vui lòng chọn <Người nhận> muốn xóa. Xin cảm ơn");
            }            
        }

        private void btnSelectOne_Click(object sender, EventArgs e)
        {
            int[] Rows = gridView1.GetSelectedRows();
            foreach (int i in Rows)
            {
                try
                {
                    if (gridView1.GetRowCellValue(i, colMaNV) != null)
                    {
                        gridView2.AddNewRow();
                        gridView2.SetFocusedRowCellValue(colMaNV2, gridView1.GetRowCellValue(i, colMaNV));
                        gridView2.SetFocusedRowCellValue(colHoTen2, gridView1.GetRowCellValue(i, colHoTen));
                        gridView2.SetFocusedRowCellValue(colNguoiGiao, BEE.ThuVien.Common.StaffName);
                    }
                }
                catch { }
            }
            gridView2.FocusedColumn = colMaNV2;
            gridView2.FocusedRowHandle = 0;
        }

        private void gridView2_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.Exception.Message.IndexOf("Column 'MaNV' is constrained to be unique.") >= 0)
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn người nhận nhiệm vụ, xin cảm ơn.");
                return;
            }

            it.NhiemVu_NhanVienCls o;
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                o = new it.NhiemVu_NhanVienCls();
                o.MaNVu = MaNVu;
                o.MaNV = int.Parse(gridView2.GetRowCellValue(i, colMaNV2).ToString());
                o.MaNVGiao = BEE.ThuVien.Common.StaffID;
                o.Insert();
            }

            DialogBox.Infomation("Dữ liệu đã được cập nhật.");
            this.Close();
        }
    }
}