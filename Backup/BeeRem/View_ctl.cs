using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BEEREMA
{
    public partial class View_ctl : UserControl
    {
        public View_ctl()
        {
            InitializeComponent();
        }

        private void itemDefault_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // pictureEdit1.Image = global::BEEREMA.Properties.Resources.BeeSky_Background;
            Properties.Settings.Default.ImageUrl = "";
            Properties.Settings.Default.Save();
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadImage(GetImage());
        }

        string GetImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Select image file";
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.PNG;*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            try
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    return dialog.FileName;
            }
            catch { }
            return null;
        }

        void LoadImage(string fileName)
        {
            try
            {
                Bitmap image = DevExpress.Utils.Controls.ImageHelper.LoadImageFromFileEx(fileName) as Bitmap;
                if (image != null)
                {
                   // pictureEdit1.Image = image;

                    Properties.Settings.Default.ImageUrl = fileName;
                    Properties.Settings.Default.Save();
                }
            }
            catch
            {
               // pictureEdit1.Image = global::BEEREMA.Properties.Resources.BeeSky_Background;
            }
        }

        private void View_ctl_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var ctl = new BEE.CongCu.Report.ctlKhachHangDoTuoi();
                ctl.Dock = DockStyle.Fill;
                pnCustomerBy.Controls.Add(ctl);

                var ctl2 = new BaoCao.BieuDo.ctlDoanhThuTheoNam();
                ctl2.Dock = DockStyle.Fill;
                splitContainerReport.Panel2.Controls.Add(ctl2);
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
            
        }

        private void View_ctl_SizeChanged(object sender, EventArgs e)
        {
            splitContainerReport.SplitterPosition = (this.Width / 2);
        }

        private void itemBCCapDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                pnCustomerBy.Controls.Clear();
                var ctl = new BEE.CongCu.Report.ctlKhachHangLevel();
                ctl.Dock = DockStyle.Fill;
                pnCustomerBy.Controls.Add(ctl);
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void itemBCMucDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                pnCustomerBy.Controls.Clear();
                var ctl = new BEE.CongCu.Report.ctlKhachHangPurpose();
                ctl.Dock = DockStyle.Fill;
                pnCustomerBy.Controls.Add(ctl);
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void itemBCNguonDen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                pnCustomerBy.Controls.Clear();
                var ctl = new BEE.CongCu.Report.ctlKhachHangHowToKnow();
                ctl.Dock = DockStyle.Fill;
                pnCustomerBy.Controls.Add(ctl);
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void itemBCKhuVuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var wait = DialogBox.WaitingForm();
            //try
            //{
            //    pnCustomerBy.Controls.Clear();
            //    var ctl = new BEE.CongCu.Report.ctlKhachHang();
            //    ctl.Dock = DockStyle.Fill;
            //    pnCustomerBy.Controls.Add(ctl);
            //}
            //catch { }
            //finally { wait.Close(); wait.Dispose(); }
        }

        private void itemBCNhom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var wait = DialogBox.WaitingForm();
            //try
            //{
            //    pnCustomerBy.Controls.Clear();
            //    var ctl = new BEE.CongCu.Report.ctlKhachHang();
            //    ctl.Dock = DockStyle.Fill;
            //    pnCustomerBy.Controls.Add(ctl);
            //}
            //catch { }
            //finally { wait.Close(); wait.Dispose(); }
        }

        private void itemBCDoTuoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                pnCustomerBy.Controls.Clear();
                var ctl = new BEE.CongCu.Report.ctlKhachHangDoTuoi();
                ctl.Dock = DockStyle.Fill;
                pnCustomerBy.Controls.Add(ctl);
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }
    }
}
