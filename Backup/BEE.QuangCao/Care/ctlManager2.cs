using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.QuangCao.Care
{
    public partial class ctlManager2 : UserControl
    {
        int SDBID = 0;
        public ctlManager2()
        {
            InitializeComponent();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(BEE.ThuVien.Common.PerID, 92);

            return o.SDB.SDBID;
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            SDBID = GetAccessData();
            SetDate(4);

            var frm = new frmFind();
            frm.SDBID = SDBID;
            frm.Show(this.FindForm());
        }

        void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                switch (SDBID)
                {
                    case 1://Tat ca
                        gcCustomer.DataSource = db.KhachHang_transacrion(tuNgay, denNgay, 0, 0, 0);
                        break;
                    case 2://Theo phong ban
                        gcCustomer.DataSource = db.KhachHang_transacrion(tuNgay, denNgay, BEE.ThuVien.Common.DepartmentID, 0, 0);
                        break;
                    case 3://Theo nhom
                        gcCustomer.DataSource = db.KhachHang_transacrion(tuNgay, denNgay, 0, BEE.ThuVien.Common.GroupID, 0);
                        break;
                    case 4://Theo nhan vien
                        gcCustomer.DataSource = db.KhachHang_transacrion(tuNgay, denNgay, 0, 0, BEE.ThuVien.Common.StaffID);
                        break;
                    default://Tat ca
                        gcCustomer.DataSource = null;
                        break;
                }
            }
        }

        private void itmeRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmFind();
            frm.SDBID = SDBID;
            frm.Show(this.FindForm());
        }

        public void Search(List<KhachHang_marketingResult> listCustomer)
        {
            gcCustomer.DataSource = listCustomer;
            if (gvCustomer.FocusedRowHandle == 0) gvCustomer.FocusedRowHandle = -1;
        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] Rows = gvCustomer.GetSelectedRows();
            if (Rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                return;
            }

            var ListReminder = new List<bdsSanPham>();
            bdsSanPham o;
            foreach (int r in Rows)
            {
                o = new bdsSanPham();
                o.MaKH = Convert.ToInt32(gvCustomer.GetRowCellValue(r, "MaKH"));
                o.ThanhTien = 0;
                ListReminder.Add(o);
            }

            var f = new QuangCao.Mail.frmGroupReminders();
            f.IsCare = true;
            f.ListReminder = ListReminder;
            f.ShowDialog();
            ListReminder = null;
            System.GC.Collect();
        }

        private void itemSendSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] Rows = gvCustomer.GetSelectedRows();
            if (Rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                return;
            }

            var ListReminder = new List<bdsSanPham>();
            bdsSanPham o;
            foreach (int r in Rows)
            {
                o = new bdsSanPham();
                o.MaKH = Convert.ToInt32(gvCustomer.GetRowCellValue(r, "MaKH"));
                o.ThanhTien = 0;
                ListReminder.Add(o);
            }

            var f = new QuangCao.SMS.frmGroupReminders();
            f.IsCare = true;
            f.ListReminder = ListReminder;
            f.ShowDialog();
            ListReminder = null;
            System.GC.Collect();
        }
    }
}
