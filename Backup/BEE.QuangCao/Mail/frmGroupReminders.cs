﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;

namespace BEE.QuangCao.Mail
{
    public partial class frmGroupReminders : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public List<bdsSanPham> ListReminder;
        public bool IsCare = false;
        public frmGroupReminders()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmGroupReminders_Load(object sender, EventArgs e)
        {
            ctlReceive ctl = new ctlReceive();
            ctl.Dock = DockStyle.Fill;
            ctl.IsReminder = true;
            ctl.ListReminder = ListReminder;
            ctl.IsCare = IsCare;
            panelContent.Controls.Add(ctl);

            ListReminder = null;
            System.GC.Collect();
        }
    }
}