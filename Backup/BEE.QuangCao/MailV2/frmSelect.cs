using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LinqToExcel;
using BEE.KhachHang;

namespace BEE.QuangCao.MailV2
{
    public partial class frmSelect : DevExpress.XtraEditors.XtraForm
    {
        public List<ItemSelect> ListKH = new List<ItemSelect>();
        public frmSelect()
        {
            InitializeComponent();
        }
        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;
                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));
                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
        }

        private void itemGetData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemSheet.EditValue != null)
            {
                var excel = new ExcelQueryFactory(this.Tag.ToString());
                var list = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new
                {
                    Email = p[0].ToString().Trim(),
                    NoiDung = p[1].ToString().Trim(),
                }).ToList();

                foreach (var r in list)
                {
                    var o = new ItemSelect();
                    o.Email = r.Email;
                    o.NoiDung = r.NoiDung;

                    ListKH.Add(o);
                }
            }
            this.Close();
        }
    }
}
