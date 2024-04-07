using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.KhachHang
{
    public partial class SelectReferrer_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH = 0;
        public SelectReferrer_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChon_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                ReferrerList_frm frm = new ReferrerList_frm();
                frm.ShowDialog();
                if (frm.MaNMG != 0)
                {
                    btnChon.Text = frm.HoTen;
                    btnChon.Tag = frm.MaNMG;
                }
            }
            else
            {
                btnChon.Text = "";
                btnChon.Tag = 0;
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.MaKH = MaKH;
            o.MaNMG = int.Parse(btnChon.Tag.ToString());
            o.UpdateReferrer();

            this.Close();
        }

        private void SelectReferrer_frm_Load(object sender, EventArgs e)
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.MaKH = MaKH;
            o.GetReferrer();
            btnChon.Text = o.HoKH;
            btnChon.Tag = o.MaNMG;
        }

        private void btnChon_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}