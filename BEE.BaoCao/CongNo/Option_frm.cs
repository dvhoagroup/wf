using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.BaoCao.CongNo
{
    public partial class Option_frm : DevExpress.XtraEditors.XtraForm
    {
        int MaDA = 0, BlockID;
        public byte LoaiHD = 0;
        public Option_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadDA()
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

        void LoadDaiLy()
        {
            it.DaiLyCls o = new it.DaiLyCls();
            lookUpDaiLy.Properties.DataSource = o.SelectNPPShow2();
            lookUpDaiLy.ItemIndex = 0;
        }

        void LoadTangNha()
        {
            it.TangNhaCls o = new it.TangNhaCls();
            lookUpTang.Properties.DataSource = o.SelectAll(BlockID);
            lookUpTang.ItemIndex = 0;
        }

        private void Option_frm_Load(object sender, EventArgs e)
        {
            LoadDA();
            lookUpDuAn2.ItemIndex = 0;
            LoadDaiLy();
        }

        private void lookUpDuAn_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _DA = (LookUpEdit)sender;
            MaDA = int.Parse(_DA.EditValue.ToString());
            LoadBlock();
            LoadTangNha();
        }

        private void lookUpBlock_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _Block = (LookUpEdit)sender;
            BlockID = int.Parse(_Block.EditValue.ToString());
            LoadTangNha();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            switch (LoaiHD)
            {
                case 1:
                    CongNo.TienDoHDMB_rpt rpt = new BEE.BaoCao.CongNo.TienDoHDMB_rpt(int.Parse(lookUpBlock.EditValue.ToString()), lookUpBlock.Text, lookUpDuAn2.Text);
                    rpt.ShowPreviewDialog();
                    this.Close();
                    break;
                case 2:
                    CongNo.TienDoHDGV_rpt hdgv = new BEE.BaoCao.CongNo.TienDoHDGV_rpt(int.Parse(lookUpBlock.EditValue.ToString()), lookUpBlock.Text, lookUpDuAn2.Text);
                    hdgv.ShowPreviewDialog();
                    this.Close();
                    break;
            }
        }
    }
}