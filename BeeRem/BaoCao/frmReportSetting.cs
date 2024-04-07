using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using System.Linq;
using BEE.ThuVien.Report;

namespace BEEREMA.BaoCao
{
    public partial class frmReportSetting : DevExpress.XtraEditors.XtraForm
    {
        public frmReportSetting()
        {
            InitializeComponent();
        }

        public Setting DefaultSetting { get; set; }
        public Setting Setting { get; set; }

        private void LoadSetting()
        {
            var ltFieldName = this.Setting.Fields.Select(s => s.FieldName).ToList();
            listField1.DataSource = this.DefaultSetting.Fields.Where(p => !ltFieldName.Contains(p.FieldName)).ToList();
            listField2.DataSource = this.Setting.Fields.OrderBy(p => p.Order).ToList();

            cmbPaperKind.EditValue = this.Setting.PaperKind;
            rdbOrientation.EditValue = this.Setting.Orientation;
            spinLeft.EditValue = this.Setting.Left;
            spinRight.EditValue = this.Setting.Right;
            spinTop.EditValue = this.Setting.Top;
            spinBottom.EditValue = this.Setting.Bottom;
        }

        private void frmReportSetting_Load(object sender, EventArgs e)
        {
            cmbPaperKind.Properties.Items.Add(PaperKind.Letter);
            cmbPaperKind.Properties.Items.Add(PaperKind.A2);
            cmbPaperKind.Properties.Items.Add(PaperKind.A3);
            cmbPaperKind.Properties.Items.Add(PaperKind.A4);
            cmbPaperKind.Properties.Items.Add(PaperKind.A5);
            cmbPaperKind.Properties.Items.Add(PaperKind.A6);

            cmbAlignment.Items.Add(TextAlignment.BottomCenter);
            cmbAlignment.Items.Add(TextAlignment.BottomJustify);
            cmbAlignment.Items.Add(TextAlignment.BottomLeft);
            cmbAlignment.Items.Add(TextAlignment.BottomRight);
            cmbAlignment.Items.Add(TextAlignment.MiddleCenter);
            cmbAlignment.Items.Add(TextAlignment.MiddleJustify);
            cmbAlignment.Items.Add(TextAlignment.MiddleLeft);
            cmbAlignment.Items.Add(TextAlignment.MiddleRight);
            cmbAlignment.Items.Add(TextAlignment.TopCenter);
            cmbAlignment.Items.Add(TextAlignment.TopJustify);
            cmbAlignment.Items.Add(TextAlignment.TopLeft);
            cmbAlignment.Items.Add(TextAlignment.TopRight);

            LoadSetting();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (listField1.ItemCount > 0)
            {
                var ltField1 = (List<Field>)listField1.DataSource;
                var ltField2 = (List<Field>)listField2.DataSource;

                var field = ltField1[listField1.SelectedIndex];
                field.Order = listField2.ItemCount;

                ltField2.Add(field);
                ltField1.RemoveAt(listField1.SelectedIndex);

                listField1.Refresh();
                listField2.Refresh();

                listField1.SelectedIndex = listField1.ItemCount - 1;
                listField2.SelectedIndex = listField2.ItemCount - 1;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listField2.ItemCount > 0)
            {
                var ltField1 = (List<Field>)listField1.DataSource;
                var ltField2 = (List<Field>)listField2.DataSource;

                ltField1.Add(ltField2[listField2.SelectedIndex]);
                ltField2.RemoveAt(listField2.SelectedIndex);

                for (int i = 0; i < ltField2.Count; i++)
                    ltField2[i].Order = i;

                listField1.Refresh();
                listField2.Refresh();

                listField1.SelectedIndex = listField1.ItemCount - 1;
                listField2.SelectedIndex = listField2.ItemCount - 1;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (listField2.SelectedIndex > 0)
            {
                var index = listField2.SelectedIndex;
                var ltField = (List<Field>)listField2.DataSource;

                var field1 = ltField[index];
                field1.Order -= 1;
                var field2 = ltField[index - 1];                
                field2.Order += 1;
                
                listField2.DataSource = ltField.OrderBy(p => p.Order).ToList();
                listField2.SelectedIndex = index - 1;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (listField2.SelectedIndex > -1 && listField2.SelectedIndex < listField2.ItemCount - 1)
            {
                var index = listField2.SelectedIndex;
                var ltField = (List<Field>)listField2.DataSource;

                var field1 = ltField[index];
                field1.Order += 1;
                var field2 = ltField[index + 1];
                field2.Order -= 1;

                listField2.DataSource = ltField.OrderBy(p => p.Order).ToList();
                listField2.SelectedIndex = index + 1;
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            this.Setting = this.DefaultSetting.Copy();
            LoadSetting();
        }

        private void listField2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listField2.SelectedValue != null)
            {
                var ltField = (List<Field>)listField2.DataSource;
                vgcField.DataSource = ltField.Where(p => p.FieldName == listField2.SelectedValue.ToString()).ToList();
            }
            else
            {
                vgcField.DataSource = null;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Setting.PaperKind = (System.Drawing.Printing.PaperKind)cmbPaperKind.EditValue;
            this.Setting.Orientation = (bool)rdbOrientation.EditValue;
            this.Setting.Bottom = Convert.ToInt32(spinBottom.EditValue);
            this.Setting.Left = Convert.ToInt32(spinLeft.EditValue);
            this.Setting.Right = Convert.ToInt32(spinRight.EditValue);
            this.Setting.Fields = (List<Field>)listField2.DataSource;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}