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
    public partial class Option_frm : DevExpress.XtraEditors.XtraForm
    {
        int MaDA = 0, BlockID = 0;
        string QueryString = "";
        public Option_frm()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (cmbMonth.Text == "<Chọn tháng>")
            {
                DialogBox.Infomation("Vui lòng chọn <Tháng>, xin cảm ơn.");
                cmbMonth.Focus();
                return;
            }

            if (cmbYear.Text == "<Chọn năm>")
            {
                DialogBox.Infomation("Vui lòng chọn <Năm>, xin cảm ơn.");
                cmbYear.Focus();
                return;
            }

            if (cmbToDay.Text == "<Chọn ngày>")
            {
                DialogBox.Infomation("Vui lòng chọn <Từ ngày>, xin cảm ơn.");
                cmbToDay.Focus();
                return;
            }

            if (cmbFromDay.Text == "<Chọn ngày>")
            {
                DialogBox.Infomation("Vui lòng chọn <Đến ngày>, xin cảm ơn.");
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
            QueryString += "," + Convert.ToString(cmbMonth.SelectedIndex + 1);
            QueryString += "," + cmbYear.SelectedItem.ToString();
            QueryString += lookUpDuAn2.Text == "<Tất cả>" ? ",'%%'" : ",'" + lookUpDuAn2.EditValue.ToString() + "'";
            QueryString += lookUpBlock.Text == "<Tất cả>" ? ",'%%'" : ",'" + lookUpBlock.EditValue.ToString() + "'";
            QueryString += lookUpTang.Text == "<Tất cả>" ? ",'%%'" : ",'" + lookUpTang.EditValue.ToString() + "'";
            QueryString += lookUpDaiLy.Text == "<Tất cả>" ? ",'%%'" : ",'" + lookUpDaiLy.EditValue.ToString() + "'";
            CongNo_frm frm = new CongNo_frm();
            frm.TuNgay = cmbToDay.SelectedIndex + 1;
            frm.DenNgay = cmbFromDay.SelectedIndex + 1;
            frm.Nam = int.Parse(cmbYear.SelectedItem.ToString());
            frm.Thang = cmbMonth.SelectedIndex + 1;
            frm.QueryString = QueryString;
            frm.ShowDialog();
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
            cmbToDay.Text = "<Chọn ngày>";
            cmbFromDay.Text = "<Chọn ngày>";
        }

        private void Option_frm_Shown(object sender, EventArgs e)
        {
            for (int i = 1; i <= 12; i++)
                cmbMonth.Properties.Items.Add("Tháng " + i);

            for (int i = DateTime.Now.Year + 5; i >= DateTime.Now.Year - 5; i--)
                cmbYear.Properties.Items.Add(i);

            cmbMonth.Text = "<Chọn tháng>";
            cmbYear.Text = "<Chọn năm>";
            LoadDuAn();
            lookUpDuAn2.ItemIndex = 0;
            LoadDaiLy();
        }

        void SetDay(int Month, int Year)
        {
            switch (Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 10:
                case 12://31 ngay
                    AddDay(31);
                    break;
                case 4:
                case 6:
                case 9:
                case 11://30 ngay
                    AddDay(30);
                    break;
                case 2:
                    if (DateTime.IsLeapYear(Year))
                        AddDay(29);
                    else
                        AddDay(28);
                    break;
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit _cmbMonth = (ComboBoxEdit)sender;
            if (cmbYear.Text != "<Chọn năm>")
                SetDay(_cmbMonth.SelectedIndex + 1, int.Parse(cmbYear.SelectedItem.ToString()));
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit _cmbYear = (ComboBoxEdit)sender;
            if (cmbYear.Text != "<Chọn năm>")
                SetDay(cmbMonth.SelectedIndex + 1, int.Parse(_cmbYear.SelectedItem.ToString()));
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