using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Threading;

namespace BEE.NghiepVuKhac
{
    public partial class InsertImage_frm : DevExpress.XtraEditors.XtraForm
    {
        public int SpaceFile = 0, Count = 0;
        public string FileName = "", Directory = "";
        public bool IsGallery = false, IsLoading = false;

        public InsertImage_frm()
        {
            InitializeComponent();
        }

        private void InsertImage_frm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Focus();
                if (FileName != "")
                {
                    it.FTPCls o = new it.FTPCls();
                    o.GetAccountFTP();
                    if (IsGallery)
                        FileName = o.Upload2(FileName, Directory);
                    else
                        FileName = o.Upload(FileName, Directory);
                    this.Close();
                }
            }
            catch { }
        }

        private void InsertImage_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}