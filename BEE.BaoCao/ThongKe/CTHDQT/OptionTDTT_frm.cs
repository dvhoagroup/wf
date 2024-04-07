﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LandSoft;

namespace BEE.BaoCao.ThongKe.CTHDQT
{
    public partial class OptionTDTT_frm : DevExpress.XtraEditors.XtraForm
    {
        int MaDA = 0;
        public byte LoaiKH = 0;
        public OptionTDTT_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Option_frm_Load(object sender, EventArgs e)
        {
            LoadDuAn();
        }

        void LoadDuAn()
        {
            it.DuAnCls o = new it.DuAnCls();
            lookUpDuAn2.Properties.DataSource = o.SelectShow();
            lookUpDuAn2.ItemIndex = 0;
        }

        void LoadBlock()
        {
            it.BlocksCls o = new it.BlocksCls();
            lookUpBlock.Properties.DataSource = o.Select(MaDA);
            lookUpBlock.ItemIndex = 0;
        }

        private void lookUpDuAn_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _DA = (LookUpEdit)sender;
            MaDA = int.Parse(_DA.EditValue.ToString());
            LoadBlock();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (lookUpBlock.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn <Block>. Xin cảm ơn.");
                lookUpBlock.Focus();
                return;
            }

            ThongKe.CTHDQT.ThayDoiThongTinBDS_rpt rpt = new ThayDoiThongTinBDS_rpt(int.Parse(lookUpBlock.EditValue.ToString()), lookUpBlock.Text);
            rpt.ShowPreviewDialog();
        }
    }
}