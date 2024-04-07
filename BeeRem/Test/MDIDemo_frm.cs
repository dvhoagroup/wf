using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.Test
{
    public partial class MDIDemo_frm : DevExpress.XtraEditors.XtraForm
    {
        const string imageFormName = "image";
        const string textFormName = "text";
        const string textRTFFormName = "rtf";
        Cursor currentCursor;
        public MDIDemo_frm()
        {
            InitializeComponent();
        }

        private void RefreshForm(bool b)
        {
            if (b)
            {
                currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                Refresh();
            }
            else
                Cursor.Current = currentCursor;
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshForm(true);
            CreateMDIForm("Chart ");
            RefreshForm(false);
        }

        private void CreateMDIForm(string s)
        {
            //BEE.DuAn.Khu_frm frm = new BEE.DuAn.Khu_frm();
            //frm.Text = s + xtraTabbedMdiManager1.Pages.Count;
            //frm.Tag = s + xtraTabbedMdiManager1.Pages.Count;
            //frm.Dock = DockStyle.Fill;
            //frm.AccessibleName = textFormName;
            
            //frm.MdiParent = this;
            //frm.Show();
        }
    }
}