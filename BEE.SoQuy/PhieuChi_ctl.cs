using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LandSoft.NghiepVu.Quy
{
    public partial class PhieuChi_ctl : UserControl
    {
        bool KT = false, KT1 = false;
        public PhieuChi_ctl()
        {
            InitializeComponent();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBC);
        }

        private void PhieuThu_ctl_Load(object sender, EventArgs e)
        {
            cmbKyBC.SelectedIndex = 0;
            LoadData();
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            dateTuNgay.EditValueChanged -= new EventHandler(dateTuNgay_EditValueChanged);
            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
            dateTuNgay.EditValueChanged += new EventHandler(dateTuNgay_EditValueChanged);
        }

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate(cmbKyBC.SelectedIndex);
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            it.pgcPhieuChiCls o = new it.pgcPhieuChiCls();
            gridControlPC.DataSource = o.SelectAll(dateTuNgay.DateTime, dateDenNgay.DateTime);
            lookUpNhanVienPC.DataSource = o.NhanVien.SelectShow();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void Edit()
        {
            if (gridView3.GetFocusedRowCellValue(colMaPC) != null)
            {
                LandSoft.GiuCho.PhieuChi_frm frm = new LandSoft.GiuCho.PhieuChi_frm();
                frm.MaPC = int.Parse(gridView3.GetFocusedRowCellValue(colMaPC).ToString());
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu chi cần sửa. Xin cảm ơn");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView3.GetFocusedRowCellValue(colMaPC) != null)
            {
                Report.Quy.PhieuChi_rpt rpt = new LandSoft.Report.Quy.PhieuChi_rpt(int.Parse(gridView3.GetFocusedRowCellValue(colMaPC).ToString()));
                rpt.ShowPreviewDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu chi cần in. Xin cảm ơn");
        }
    }
}
