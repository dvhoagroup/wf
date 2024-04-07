﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
namespace LandSoft.Library
{
    class ctlLoaiCongVan : LookEdit
        {
        public ctlLoaiCongVan()
            {
                this.ButtonClick += new ButtonPressedEventHandler(ctlLoaiTienEdit_ButtonClick);

                this.Properties.DisplayMember = "TenLCV";
                this.Properties.ValueMember = "MaLCV";
                this.Properties.ShowLines = true;
            }

            void ctlLoaiTienEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
            {
                //switch (e.Button.Index)
                //{
                //    case 1:
                //        using (var frm = new Other.frmLoaiCongVan(1))
                //        {
                //            frm.ShowDialog();
                //            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                //            {
                //                this.LoadData();
                //            }
                //        }
                //        break;
                //}
            }

            public void LoadData()
            {
                this.Properties.Columns.Clear();
                this.Properties.Columns.Add(new LookUpColumnInfo("MaLCV", 30, "Mã Loại"));
                this.Properties.Columns.Add(new LookUpColumnInfo("TenLCV", 70, "Tên Loại"));
                this.Properties.ShowHeader = false;

                using (var db = new MasterDataContext())
                {
                    this.Properties.DataSource = null;
                    this.Properties.DataSource = db.LoaiCongVans.ToList();
                }
            }
        
    }
}