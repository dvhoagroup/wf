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
    public partial class OptionMonth_frm : DevExpress.XtraEditors.XtraForm
    {
        string QueryString = "";
        int MaDA = 0, BlockID = 0;
        public OptionMonth_frm()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (cmbYear.Text == "<Chọn năm>")
            {
                DialogBox.Infomation("Vui lòng chọn <Năm>, xin cảm ơn.");
                cmbYear.Focus();
                return;
            }

            if (cmbToDay.Text == "<Chọn tháng>")
            {
                DialogBox.Infomation("Vui lòng chọn <Từ tháng>, xin cảm ơn.");
                cmbToDay.Focus();
                return;
            }

            if (cmbFromDay.Text == "<Chọn tháng>")
            {
                DialogBox.Infomation("Vui lòng chọn <Đến tháng>, xin cảm ơn.");
                cmbFromDay.Focus();
                return;
            }

            if (cmbToDay.SelectedIndex > cmbFromDay.SelectedIndex)
            {
                DialogBox.Infomation("<Từ ngày> phải lớn hơn hoặc bằng <Đến ngày>. Vui lòng chọn lại, xin cảm ơn.");
                return;
            }
            QueryString += Convert.ToString(cmbToDay.SelectedIndex + 1);
            QueryString += "," + Convert.ToString(cmbFromDay.SelectedIndex + 1);
            QueryString += "," + cmbYear.SelectedItem.ToString();
            QueryString += lookUpDuAn2.Text == "<Tất cả>" ? ",'%%'" : ",'" + lookUpDuAn2.EditValue.ToString() + "'";
            QueryString += lookUpBlock.Text == "<Tất cả>" ? ",'%%'" : ",'" + lookUpBlock.EditValue.ToString() + "'";
            QueryString += lookUpTang.Text == "<Tất cả>" ? ",'%%'" : ",'" + lookUpTang.EditValue.ToString() + "'";
            QueryString += lookUpDaiLy.Text == "<Tất cả>" ? ",'%%'" : ",'" + lookUpDaiLy.EditValue.ToString() + "'";
            CongNoThang_frm frm = new CongNoThang_frm();
            frm.TuThang = cmbToDay.SelectedIndex + 1;
            frm.DenThang = cmbFromDay.SelectedIndex + 1;
            frm.Nam = int.Parse(cmbYear.SelectedItem.ToString());
            frm.QueryString = QueryString;
            frm.ShowDialog();
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void AddDay(int val)
        {
            cmbToDay.Properties.Items.Clear();
            cmbFromDay.Properties.Items.Clear();
            for (int i = 1; i <= val; i++)
            {
                cmbToDay.Properties.Items.Add(i);
                cmbFromDay.Properties.Items.Add(i);
            }
            cmbToDay.Text = "<Chọn tháng>";
            cmbFromDay.Text = "<Chọn tháng>";
        }

        private void Option_frm_Shown(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year + 5; i >= DateTime.Now.Year - 5; i--)
                cmbYear.Properties.Items.Add(i);
            AddDay(12);
            cmbYear.Text = "<Chọn năm>";
        }

        private void OptionMonth_frm_Load(object sender, EventArgs e)
        {
            LoadDuAn();
            lookUpDuAn2.ItemIndex = 0;
            LoadDaiLy();
        }

        void LoadDaiLy()
        {
            it.DaiLyCls o = new it.DaiLyCls();
            lookUpDaiLy.Properties.DataSource = o.SelectNPPShow2();
            lookUpDaiLy.ItemIndex = 0;
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
    }
}