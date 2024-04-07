using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LandSoft.NghiepVu.Quy
{
    public partial class PhieuThu_ctl : UserControl
    {
        bool KT = false, KT1 = false;
        public PhieuThu_ctl()
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
            it.pgcPhieuThuCls o = new it.pgcPhieuThuCls();
            gridControlPT.DataSource = o.SelectAll(dateTuNgay.DateTime, dateDenNgay.DateTime);

            lookUpLoaiTien2.DataSource = o.LoaiTien.Select();
            lookUpNhanVien2.DataSource = o.NhanVien.SelectShow();
        }

        void Edit()
        {
            if (gridPhieuThu.GetFocusedRowCellValue(colMaPT) != null)
            {
                if (int.Parse(gridPhieuThu.GetFocusedRowCellValue(colMaLGD).ToString()) == 1)
                {
                    LandSoft.GiuCho.PhieuThu_frm frm = new LandSoft.GiuCho.PhieuThu_frm();
                    frm.MaPT = int.Parse(gridPhieuThu.GetFocusedRowCellValue(colMaPT).ToString());
                    frm.MaKH = int.Parse(gridPhieuThu.GetFocusedRowCellValue(colMaKH).ToString());
                    frm.DotTT = byte.Parse(gridPhieuThu.GetFocusedRowCellValue(colDotTT).ToString());
                    frm.ShowDialog();
                }
                else
                {
                    HDGopVon.PhieuThu_frm frm = new LandSoft.NghiepVu.HDGopVon.PhieuThu_frm();
                    frm.MaPT = int.Parse(gridPhieuThu.GetFocusedRowCellValue(colMaPT).ToString());
                    frm.MaKH = int.Parse(gridPhieuThu.GetFocusedRowCellValue(colMaKH).ToString());
                    frm.DotTT = byte.Parse(gridPhieuThu.GetFocusedRowCellValue(colDotTT).ToString());
                    frm.ShowDialog();
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu thu cần sửa. Xin cảm ơn");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void gridPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnInPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridPhieuThu.GetFocusedRowCellValue(colMaPT) != null)
            {
                Report.Quy.PhieuThu_rpt rpt = new LandSoft.Report.Quy.PhieuThu_rpt(int.Parse(gridPhieuThu.GetFocusedRowCellValue(colMaPT).ToString()), int.Parse(gridPhieuThu.GetFocusedRowCellValue(colMaLGD).ToString()));
                rpt.ShowPreviewDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu thu cần in. Xin cảm ơn");
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}
