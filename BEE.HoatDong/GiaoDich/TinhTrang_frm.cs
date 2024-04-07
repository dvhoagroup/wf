using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.GiaoDich
{
    public partial class TinhTrang_frm : DevExpress.XtraEditors.XtraForm
    {
        public TinhTrang_frm()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            it.pdkgdTinhTrangCls o = new it.pdkgdTinhTrangCls();
            gridControl1.DataSource = o.Select();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TinhTrang_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            it.pdkgdTinhTrangCls o;
            DataTable tblState = (DataTable)gridControl1.DataSource;
            foreach (DataRow r in tblState.Rows)
            {
                if (r.RowState != DataRowState.Unchanged)
                {
                    o = new it.pdkgdTinhTrangCls();
                    if (r.RowState == DataRowState.Modified)
                    {
                        o.MaTT = byte.Parse(r["MaTT"].ToString());
                        o.MauSau = int.Parse(r["MauSac"].ToString());
                        o.TenTT = r["TenTT"].ToString();
                        o.Update();
                    }
                }
            }
            this.Close();
        }
    }
}