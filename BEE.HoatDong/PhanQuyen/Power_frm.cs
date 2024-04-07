using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace BEE.HoatDong.PhanQuyen
{
    public partial class Power_frm : DevExpress.XtraEditors.XtraForm
    {
        public int PerID = 0, FormID = 0;
        public Power_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void Power_frm_Load(object sender, EventArgs e)
        {
            LoadFormFeature();
            it.ModulesCls o = new it.ModulesCls();
            gridControl1.DataSource = o.SelectAllPer();
            gridView1.ExpandAllGroups();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colFormID) != null)
            {
                FormID = int.Parse(gridView1.GetFocusedRowCellValue(colFormID).ToString());
                radioAccess.Enabled = true;
            }
            else
            {
                FormID = 0;
                radioAccess.Enabled = false;
            }

            LoadFormFeature();
            LoadActionData();
            SetValueCheckListBox();
            //if (gridView1.GetFocusedRowCellValue("FormID") == null)
            //{
            //    FormID = 0;
            //    radioAccess.Enabled = false;
            //    chkListBox.Enabled = false;
            //    return;
            //}

            //radioAccess.Enabled = true;
            //FormID = int.Parse(gridView1.GetFocusedRowCellValue(colFormID).ToString());
            //LoadFormFeature(); 
            //LoadActionData();                       
            //SetValueCheckListBox();            
        }

        void LoadFormFeature()
        {
            it.FormFeaturesCls o = new it.FormFeaturesCls();
            o.FormID = FormID;
            chkListBox.DataSource = o.SelectBy();
        }

        void LoadActionData()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = PerID;
            o.AccessData.Form.FormID = FormID;
            o.AccessData.Detail();
            if (o.AccessData.SDB.SDBID > 0)
            {
                radioAccess.SelectedIndex = o.AccessData.SDB.SDBID - 1;
                chkListBox.Enabled = true;
            }
            else
            {
                chkListBox.Enabled = false;
                radioAccess.SelectedIndex = 5;
            }
        }

        void SaveAccessData()
        {
            if (FormID != 0 && PerID != 0)
            {
                it.AccessDataCls o = new it.AccessDataCls();
                o.Form.FormID = FormID;
                o.Per.PerID = PerID;
                o.SDB.SDBID = (byte)(radioAccess.SelectedIndex + 1);
                o.Update();
            }
        }

        void SetValueCheckListBox()
        {
            DataTable tblFormFeature;
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Form.FormID = FormID;
            o.AccessData.Per.PerID = PerID;
            tblFormFeature = o.SelectBy();
            foreach(DataRow r in tblFormFeature.Rows)
            {
                for (int j = 0; j < chkListBox.ItemCount; j++)
                {
                    if (byte.Parse(r["FeatureID"].ToString()) == byte.Parse(chkListBox.GetItemValue(j).ToString()))
                        chkListBox.SetItemChecked(j, true);
                }
            }
        }

        void SetCheckAllItem()
        {
            DataTable tblFormFeature;
            it.FormFeaturesCls o = new it.FormFeaturesCls();
            o.FormID = FormID;
            tblFormFeature = o.SelectBy();
            it.ActionDataCls obj = new it.ActionDataCls();

            for (int j = 0; j < tblFormFeature.Rows.Count; j++)
                chkListBox.SetItemChecked(j, true);
        }

        void SetUnCheckAllItem()
        {
            DataTable tblFormFeature;
            it.FormFeaturesCls o = new it.FormFeaturesCls();
            o.FormID = FormID;
            tblFormFeature = o.SelectBy();
            for (int j = 0; j < tblFormFeature.Rows.Count; j++)
                chkListBox.SetItemChecked(j, false);
        }

        private void radioAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveAccessData();
            //if (radioAccess.SelectedIndex == 0)
            //    SetCheckAllItem();
            //if (radioAccess.SelectedIndex == 5)
            //    SetUnCheckAllItem();
            //chkListBox.Enabled = (radioAccess.SelectedIndex != 5);
            if (radioAccess.SelectedIndex == 5)
            {
                chkListBox.UnCheckAll();
                chkListBox.Enabled = false;
            }
            else
            {
                chkListBox.Enabled = true;
                //chkListBox.CheckAll();
            }
            //LoadActionData();
            //LoadFormFeature();
            //LoadActionData();
            //SetValueCheckListBox();
        }

        private void chkListBox_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            try
            {
                it.ActionDataCls o = new it.ActionDataCls();
                o.AccessData.Per.PerID = PerID;
                o.AccessData.Form.FormID = FormID;
                o.Feature.FeatureID = byte.Parse(chkListBox.GetItemValue(e.Index).ToString());
                if (chkListBox.GetItemChecked(e.Index))
                    o.Insert();
                else
                    o.Delete();
            }
            catch { }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}