using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LandSoft;

namespace BEE.BaoCao.ThongKe.DuAn
{
    public partial class OptionBlock_frm : DevExpress.XtraEditors.XtraForm
    {
        string QueryString = "";
        public OptionBlock_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            QueryString = "";
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
            QueryString += "," + lookUpDuAn2.EditValue.ToString();

            ChartBlock_frm frm = new ChartBlock_frm();
            frm.QueryString = QueryString;
            frm.Year = int.Parse(cmbYear.SelectedItem.ToString());
            frm.ToMonth = cmbToDay.SelectedIndex + 1;
            frm.FromMonth = cmbFromDay.SelectedIndex + 1;
            frm.TenDa = lookUpDuAn2.Text.ToUpper(); ;
            frm.ShowDialog();
        }

        private void Option_frm_Load(object sender, EventArgs e)
        {

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
            LoadDuAn();
        }

        void LoadDuAn()
        {
            it.DuAnCls o = new it.DuAnCls();
            lookUpDuAn2.Properties.DataSource = o.SelectShow();
            lookUpDuAn2.ItemIndex = 0;
        }
    }
}