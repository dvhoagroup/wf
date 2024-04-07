using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.HoatDong.Maketing
{
    public partial class BieuMau_frm : DevExpress.XtraEditors.XtraForm
    {
        public string TenDA = "";
        public int MaDA = 0, MaHDMB = 0;
        public string KhachHang = "";
        public BieuMau_frm()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            it.MauThiepCls o = new it.MauThiepCls();
            gridControl1.DataSource = o.Select();
        }

        private void BieuMau_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaBM) != null)
            {
                //HDMBCls o = new HDMBCls();
                //o.Print(MaHDMB, byte.Parse(gridView1.GetFocusedRowCellValue(colMaBM).ToString()), MaDA);
                //BEE.BaoCao.Marketing.MergeMail_rpt rpt = new BEE.BaoCao.Marketing.MergeMail_rpt(KhachHang, byte.Parse(gridView1.GetFocusedRowCellValue(colMaBM).ToString()), gridView1.GetFocusedRowCellValue(colTenBM).ToString());
                //rpt.ShowPreviewDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn mẫu thiệp - thư muốn in. Xin cảm ơn");
        }
    }
}