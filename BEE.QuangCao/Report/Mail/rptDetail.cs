using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BEE.QuangCao.Report.Mail
{
    public partial class rptDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDetail(DateTime? tuNgay, string title, object obj)
        {
            InitializeComponent();

            cSTT.DataBindings.Add("Text", null, "STT");
            cXungHo.DataBindings.Add("Text", null, "Vocative");
            cHoTen.DataBindings.Add("Text", null, "FullName");
            cNgaySinh.DataBindings.Add("Text", null, "BirthDate");
            cDienThoai.DataBindings.Add("Text", null, "Phone");
            cDiaChi.DataBindings.Add("Text", null, "HomeAddress");
            cEmail.DataBindings.Add("Text", null, "Email");
            cPhongBan.DataBindings.Add("Text", null, "Department");
            cChucVu.DataBindings.Add("Text", null, "JobTitle");
            cTrangThai.DataBindings.Add("Text", null, "Status");

            this.DataSource = obj;

            lblDate.Text = string.Format(lblDate.Text, tuNgay);
            lblNgayLap.Text = string.Format(lblNgayLap.Text, DateTime.Now);
            lblTitle.Text = string.Format(lblTitle.Text, title);
        }
    }
}
