using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class KyKinhDoanh_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        public KyKinhDoanh_ctl()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            it.KyKinhDoanhCls o = new it.KyKinhDoanhCls();
            gridControl1.DataSource = o.Select();
            lookUpDuAn.DataSource = o.ChiTieu.SelectShow();
        }

        private void Huong_ctl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KyKinhDoanh_frm frm = new KyKinhDoanh_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaKKD) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa chỉ tiêu: <" + gridView1.GetFocusedRowCellValue(colSTT).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.KyKinhDoanhCls o = new it.KyKinhDoanhCls();
                        o.ChiTieu.MaCT = int.Parse(gridView1.GetFocusedRowCellValue(colMaKKD).ToString());
                        o.STT = byte.Parse(gridView1.GetFocusedRowCellValue(colSTT).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì chỉ tiêu: <" + gridView1.GetFocusedRowCellValue(colSTT).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn chỉ tiêu cần xóa. Xin cảm ơn");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colMaKKD) != null)
            {
                KyKinhDoanh_frm frm = new KyKinhDoanh_frm();
                frm.KeyID = int.Parse(gridView1.GetFocusedRowCellValue(colMaKKD).ToString());
                frm.STT = byte.Parse(gridView1.GetFocusedRowCellValue(colSTT).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn chỉ tiêu cần sửa. Xin cảm ơn");
        }
    }
}
