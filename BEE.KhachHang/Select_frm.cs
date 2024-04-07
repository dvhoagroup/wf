using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class Select_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH = 0;
        public string TenKH = "";
        public bool IsPersonal = true;
        public Select_frm()
        {
            InitializeComponent();
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(BEE.ThuVien.Common.PerID, 27);

            return o.SDB.SDBID;
        }

        void LoadData()
        {
            it.KhachHangCls o = new it.KhachHangCls();
            switch (GetAccessData())
            {
                case 1://Tat ca
                    gridControl2.DataSource = o.SelectCompany();
                    gridControl1.DataSource = o.SelectPersonal();
                    break;
                case 2://Theo nhom
                    gridControl2.DataSource = o.SelectComByGroup(BEE.ThuVien.Common.StaffID, BEE.ThuVien.Common.GroupID);
                    gridControl1.DataSource = o.SelectPerByGroup(BEE.ThuVien.Common.StaffID, BEE.ThuVien.Common.GroupID);
                    break;
                case 3://Theo phong ban
                    gridControl2.DataSource = o.SelectComByDeparment(BEE.ThuVien.Common.StaffID, BEE.ThuVien.Common.DepartmentID);
                    gridControl1.DataSource = o.SelectPerByDeparment(BEE.ThuVien.Common.StaffID, BEE.ThuVien.Common.DepartmentID);
                    break;
                case 4://Theo nhan vien
                    gridControl2.DataSource = o.SelectComByStaff(BEE.ThuVien.Common.StaffID);
                    gridControl1.DataSource = o.SelectPerByStaff(BEE.ThuVien.Common.StaffID);
                    break;
                default:
                    gridControl1.DataSource = null;
                    gridControl2.DataSource = null;
                    break;
            }            
            
            lookUpNhomKH1.DataSource = o.NhomKH.Select();
            lookUpNhomKH2.DataSource = o.NhomKH.Select();

            lookUpNhanVien1.DataSource = o.NhanVien.SelectShow();
            lookUpNhanVien2.DataSource = o.NhanVien.SelectShow();
            lookupQuyDanh.DataSource = o.QuyDanh.Select();
        }

        private void Select_frm_Load(object sender, EventArgs e)
        {
            LoadData();
            if (IsPersonal)
                xtraTabControl2.SelectedTabPageIndex = 0;
            else
                xtraTabControl2.SelectedTabPageIndex = 1;
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void ChoiceCustomer()
        {
            if (xtraTabControl2.SelectedTabPageIndex == 0)
            {
                if (gridCaNhan.GetFocusedRowCellValue(colMaKH) != null)
                {
                    MaKH = int.Parse(gridCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
                    TenKH = gridCaNhan.GetFocusedRowCellValue(colHoKH).ToString() + " " + gridCaNhan.GetFocusedRowCellValue(colTenKH).ToString();
                }
                else
                {
                    DialogBox.Infomation("Vui lòng chọn khách hàng. Xin cảm ơn");
                    return;
                }
            }
            else
            {
                if (gridDoanhNghiep.GetFocusedRowCellValue(colMaKH2) != null)
                {
                    MaKH = int.Parse(gridDoanhNghiep.GetFocusedRowCellValue(colMaKH2).ToString());
                    TenKH = gridDoanhNghiep.GetFocusedRowCellValue(colNguoiLienHe2).ToString();
                }
                else
                {
                    DialogBox.Infomation("Vui lòng chọn khách hàng. Xin cảm ơn");
                    return;
                }
            }
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChoiceCustomer();
        }

        private void gridCaNhan_DoubleClick(object sender, EventArgs e)
        {
            ChoiceCustomer();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhachHang_frm frm = new KhachHang_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void gridDoanhNghiep_DoubleClick(object sender, EventArgs e)
        {
            ChoiceCustomer();
        }
    }
}