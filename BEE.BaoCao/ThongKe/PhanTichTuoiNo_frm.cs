using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LandSoft;

namespace BEE.BaoCao.ThongKe
{
    public partial class PhanTichTuoiNo_frm : DevExpress.XtraEditors.XtraForm
    {
        int MaDA = 0, BlockID = 0;
        public byte LoaiBC = 0;
        public PhanTichTuoiNo_frm()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (dateNgay.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng chọn thoi gian báo cáo. Xin cảm ơn.");
                dateNgay.Focus();
                return;
            }

            switch(LoaiBC)
            {
                case 1:
                PhanTichTuoiNo_rpt rpt = new PhanTichTuoiNo_rpt(dateNgay.DateTime, lookUpDuAn2.Text == "<Tất cả>" ? "%%" : lookUpDuAn2.EditValue.ToString(), lookUpBlock.Text == "<Tất cả>" ? "%%" : lookUpBlock.EditValue.ToString(), lookUpTang.Text == "<Tất cả>" ? "%%" : lookUpTang.EditValue.ToString(), lookUpNhomKH.Text == "<Tất cả>" ? "%%" : lookUpNhomKH.EditValue.ToString());
                rpt.ShowPreviewDialog();
                break;

                case 2://Cong no theo khach hang
                CongNoTheoKhachHang_rpt rpt2 = new CongNoTheoKhachHang_rpt(dateNgay.DateTime, lookUpDuAn2.Text == "<Tất cả>" ? "%%" : lookUpDuAn2.EditValue.ToString(), lookUpBlock.Text == "<Tất cả>" ? "%%" : lookUpBlock.EditValue.ToString(), lookUpTang.Text == "<Tất cả>" ? "%%" : lookUpTang.EditValue.ToString(), lookUpNhomKH.Text == "<Tất cả>" ? "%%" : lookUpNhomKH.EditValue.ToString());
                rpt2.ShowPreviewDialog();
                break;

                case 3://Ke hoach thu no
                CTHDQT.KeHoachThuNo_rpt rpt3 = new CTHDQT.KeHoachThuNo_rpt(dateNgay.DateTime, lookUpDuAn2.Text == "<Tất cả>" ? "%%" : lookUpDuAn2.EditValue.ToString(), lookUpBlock.Text == "<Tất cả>" ? "%%" : lookUpBlock.EditValue.ToString(), lookUpTang.Text == "<Tất cả>" ? "%%" : lookUpTang.EditValue.ToString(), lookUpNhomKH.Text == "<Tất cả>" ? "%%" : lookUpNhomKH.EditValue.ToString());
                rpt3.ShowPreviewDialog();
                break;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadNhomKH()
        {
            it.NhomKHCls o = new it.NhomKHCls();
            lookUpNhomKH.Properties.DataSource = o.SelectAll();
            lookUpNhomKH.ItemIndex = 0;
        }

        void LoadDuAn()
        {
            it.DuAnCls o = new it.DuAnCls();
            lookUpDuAn2.Properties.DataSource = o.SelectShow2();
        }

        void LoadBlock()
        {
            it.BlocksCls o = new it.BlocksCls();
            lookUpBlock.Properties.DataSource = o.SelectAll(MaDA);
            lookUpBlock.ItemIndex = 0;
        }

        private void lookUpDuAn_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _DA = (LookUpEdit)sender;
            MaDA = int.Parse(_DA.EditValue.ToString());
            LoadBlock();
            LoadTangNha();
        }

        void LoadTangNha()
        {
            it.TangNhaCls o = new it.TangNhaCls();
            lookUpTang.Properties.DataSource = o.SelectAll(BlockID);
            lookUpTang.ItemIndex = 0;
        }

        private void lookUpBlock_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _Block = (LookUpEdit)sender;
            BlockID = int.Parse(_Block.EditValue.ToString());
            LoadTangNha();
        }

        private void PhanTichTuoiNo_frm_Load(object sender, EventArgs e)
        {
            LoadDuAn();
            lookUpDuAn2.ItemIndex = 0;
            dateNgay.DateTime = DateTime.Now;
            LoadNhomKH();
        }
    }
}