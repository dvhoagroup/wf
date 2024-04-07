using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Tutorials;
using System.Threading;

namespace LandSoft.NghiepVu.GiaoDich
{
    public partial class Gallery_frm : DevExpress.XtraEditors.XtraForm
    {
        LandSoft.NghiepVu.Khac.InsertImage_frm frm;
        public int MaGD = 0;
        public Gallery_frm()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            it.pdkgdHinhAnhCls o = new it.pdkgdHinhAnhCls();
            o.MaGD = MaGD;
            DataTable tbl = o.SelectBy();

            it.RecordCls[] recs = new it.RecordCls[tbl.Rows.Count];
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                recs[i] = new it.RecordCls(byte.Parse(tbl.Rows[i]["STT"].ToString()), ImageToByteArray(GetImage(tbl.Rows[i]["ImageUrl"].ToString())));
            }

            gridControl1.DataSource = recs;

            Cursor.Current = currentCursor;
            
            wait.Close(); wait.Dispose();
            btnChoiceFile.Focus();
            this.TopMost = true;
            this.TopMost = false;
            this.Focus();
        }

        private void Gallery_frm_Load(object sender, EventArgs e)
        {
                   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChoiceFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Chọn file";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LandSoft.NghiepVu.Khac.InsertImage_frm frm = new LandSoft.NghiepVu.Khac.InsertImage_frm();
                    frm.FileName = ofd.FileName;
                    frm.IsGallery = true;
                    frm.Directory = "httpdocs/upload/pgd/" + MaGD;
                    frm.ShowDialog();
                    btnChoiceFile.Text = frm.FileName;
                }
            }
            else
            {
                if (btnChoiceFile.Text.Trim() != "")
                {
                    it.pdkgdHinhAnhCls o = new it.pdkgdHinhAnhCls();
                    o.MaGD = MaGD;
                    o.ImageUrl = btnChoiceFile.Text;
                    o.Insert();
                    btnChoiceFile.Text = "";

                    LoadData();
                }
                else
                    DialogBox.Infomation("Vui lòng chọn ảnh hoặc nhập địa chỉ (URL) của ảnh. Xin cảm ơn.");
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (layoutView1.GetFocusedRowCellValue(colSTT) != null)
            {
                it.pdkgdHinhAnhCls o = new it.pdkgdHinhAnhCls();
                o.MaGD = MaGD;
                o.STT = byte.Parse(layoutView1.GetFocusedRowCellValue(colSTT).ToString());
                o.Delete();

                LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn ảnh muốn xóa. Xin cảm ơn.");
        }

        private void Gallery_frm_Shown(object sender, EventArgs e)
        {
            LoadData();

            layoutView1.MoveBy(layoutView1.RowCount / 2 - 1); 
        }

        private void layoutView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewCustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "ImageUrl")
                e.RepositoryItem = picImage;
        }

        private Image GetImage(string name)
        {
            return new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(name)));
        }

        private byte[] ImageToByteArray(Image image)
        {
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            image.Save(mStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] ret = mStream.ToArray();
            mStream.Close();
            return ret;
        }
    }
}