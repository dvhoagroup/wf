using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Microsoft.ConsultingServices.HtmlEditor;

namespace BEE.QuangCao.Mail
{
    public partial class frmFields : DevExpress.XtraEditors.XtraForm
    {
        public HtmlEditorControl txtContent { get; set; }

        public frmFields()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmFields_Load(object sender, EventArgs e)
        {
            using (ThuVien.MasterDataContext db = new ThuVien.MasterDataContext())
            {
                lbField.DataSource = db.mailFields;
            }
        }

        private void lbField_DoubleClick(object sender, EventArgs e)
        {
            txtContent.SelectedText = lbField.SelectedValue.ToString();
        }
    }
}