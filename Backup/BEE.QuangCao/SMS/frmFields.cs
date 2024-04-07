using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace BEE.QuangCao.SMS
{
    public partial class frmFields : DevExpress.XtraEditors.XtraForm
    {
        public MemoEdit txtContent { get; set; }

        public frmFields()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmFields_Load(object sender, EventArgs e)
        {
            using (ThuVien.MasterDataContext db = new ThuVien.MasterDataContext())
            {
                lbField.DataSource = db.smsFields;
            }
        }

        private void lbField_DoubleClick(object sender, EventArgs e)
        {
            txtContent.Text = txtContent.Text.Insert(txtContent.SelectionStart, lbField.SelectedValue.ToString());
        }
    }
}