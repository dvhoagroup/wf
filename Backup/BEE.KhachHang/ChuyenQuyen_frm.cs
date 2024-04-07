using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class ChuyenQuyen_frm : DevExpress.XtraEditors.XtraForm
    {
        public it.ChuyenQuyenKHCls objCQ;

        public ChuyenQuyen_frm()
        {
            InitializeComponent();
            objCQ = new it.ChuyenQuyenKHCls();
        }

        private void ChuyenQuyen_ctl_Load(object sender, EventArgs e)
        {
            lookNhanVien.Properties.DataSource = new it.NhanVienCls().Select();

            if (objCQ.MaCQ != null)
            {
                DataRow r = objCQ.getDetail();
                lookNhanVien.EditValue = r["MaNVMoi"];
                lookNhanVien.Enabled = false;
                txtLyDo.Text = r["LyDo"].ToString();
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (lookNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhân viên mới");
                return;
            }            
            
            objCQ.LyDo = txtLyDo.Text;
            objCQ.MaNV = BEE.ThuVien.Common.StaffID;

            if (objCQ.MaCQ != null)
            {
                objCQ.Update();
            }
            else
            {
                objCQ.MaNVMoi = (int)lookNhanVien.EditValue;
                objCQ.Insert();
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}