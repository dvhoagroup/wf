using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.NhanVien
{
    public partial class ChoiceStaff_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsUpdate = false;
        public byte CateID = 0;
        public int MaNV = 0, MaDL = 0;
        public decimal Rose = 0;
        public ChoiceStaff_frm()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNV) == null)
            {
                DialogBox.Infomation("Vui lòng chọn [Nhân viên], xin cảm ơn.");
                return;
            }
            else
            {
                MaNV = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colMaNV));
                MaDL = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colMaDL));
                Rose = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("Rose"));
            }
            
            IsUpdate = true;
            this.Close();
        }

        private void ChoiceStaff_frm_Load(object sender, EventArgs e)
        {
            LoadStaff();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
                LoadStaff();
            else
                LoadStaffAgent();
        }

        void LoadStaff()
        {
            it.NhanVienCls o = new it.NhanVienCls();
            gridControl1.DataSource = o.SelectChoiceChange();
        }

        void LoadStaffAgent()
        {
            it.NhanVienDaiLyCls o = new it.NhanVienDaiLyCls();
            gridControl1.DataSource = o.SelectChoiceChange();
        }
    }
}