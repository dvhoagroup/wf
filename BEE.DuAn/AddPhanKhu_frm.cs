﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.DuAn
{
    public partial class AddPhanKhu_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKhu = 0, MaPK = 0;
        public bool IsUpdate = false;
        public AddPhanKhu_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadData()
        {
            it.PhanKhuCls o = new it.PhanKhuCls(MaPK);
            txtTenKhu.Text = o.TenPK;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenKhu.Text.Trim() == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên phân khu. Xin cảm ơn.");
                txtTenKhu.Focus();
                return;
            }

            it.PhanKhuCls o = new it.PhanKhuCls();
            o.TenPK = txtTenKhu.Text;
            o.Khu.MaKhu = MaKhu;

            if (MaPK != 0)
            {
                o.MaPK = MaPK;
                o.Update();
            }
            else
                o.Insert();

            IsUpdate = true;
            //DialogBox.Infomation("Dữ liệu đã được cập nhật.");
            this.Close();
        }

        private void AddKhu_frm_Load(object sender, EventArgs e)
        {
            if (MaKhu != 0)
                LoadData();
        }
    }
}