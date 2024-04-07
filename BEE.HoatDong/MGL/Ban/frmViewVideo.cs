using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmViewVideo : DevExpress.XtraEditors.XtraForm
    {
        public string FileName  { get; set; }
        public frmViewVideo()
        {
            InitializeComponent();
        }

        private void frmViewVideo_Load(object sender, EventArgs e)
        {
            try
            {
                //axWindowsMediaPlayer2.URL = FileName;
                axWindowsMediaPlayer2.openPlayer(FileName);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + FileName);
            }
           
        }
    }
}