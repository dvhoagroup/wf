using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class ctlListConfirm : UserControl
    {
        MasterDataContext db;
        public ctlListConfirm()
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

            itemTuNgay.EditValueChanged -= new EventHandler(itemDenNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
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

        void LoadData()
        {
            try
            {
                db = new MasterDataContext();
                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                gcCustomer.DataSource = db.aCustomer_getNotConfirm(tuNgay, denNgay, itemTinhTrang.EditValue.ToString() == "Chờ duyệt" ? false : true);
            }
            catch { }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ctlListConfirm_Load(object sender, EventArgs e)
        {
            SetDate(4);

            LoadData();
        }

        private void gvCustomer_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadHistory();
        }

        void LoadHistory()
        {
            if (gvCustomer.FocusedRowHandle >= 0)
            {
                gcQTTH.DataSource = db.aCustomerHistory_getByCusID(Convert.ToInt32(gvCustomer.GetFocusedRowCellValue("CustomerID")));
            }
            else
                gcQTTH.DataSource = null;
        }

        void Confirm(bool isConfirm)
        {
            if (gvCustomer.FocusedRowHandle >= 0)
            {
                var f = new frmConfirm();
                f.CustomerID = Convert.ToInt32(gvCustomer.GetFocusedRowCellValue("CustomerID"));
                f.IsConfirm = isConfirm;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadHistory();
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn Khách hàng, xin cảm ơn.");
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Confirm(true);
        }

        private void itemKoDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Confirm(false);
        }

        private void cmbTinhTrang_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void itemTinhTrang_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
