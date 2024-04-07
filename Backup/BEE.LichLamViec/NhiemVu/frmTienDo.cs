﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;

namespace BEEREMA.CongViec.NhiemVu
{
    public partial class frmTienDo : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmTienDo()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
        }

        private void frmTienDo_Load(object sender, EventArgs e)
        {
            lookLoaiNV.DataSource = db.NhiemVu_Loais;
        }

        private void itemLoaiNV_EditValueChanged(object sender, EventArgs e)
        {
            gcTienDo.DataSource = db.NhiemVu_TienDos.Where(p => p.MaLNV == (int)itemLoaiNV.EditValue).OrderBy(p => p.STT);
        }

        private void grvTienDo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question() == DialogResult.No) return;
                grvTienDo.DeleteSelectedRows();
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã được lưu");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }            
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void grvTienDo_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvTienDo.SetFocusedRowCellValue("MaLNV", itemLoaiNV.EditValue);
        }
    }
}