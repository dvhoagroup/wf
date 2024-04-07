using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;

namespace BEEREMA
{
    public partial class ctlFirstView : UserControl
    {
        public ctlFirstView()
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
                var ctl = new BEEREMA.CongViec.NhiemVu.View_ctl();
                ctl.Dock = DockStyle.Fill;
                split1.Panel1.Controls.Add(ctl);

                var ctl2 = new NewReports.ctlThongKeGiaoDich();
                ctl2.Dock = DockStyle.Fill;
                split2.Panel1.Controls.Add(ctl2);

                var ctl3 = new NewReports.ctlThongKeKhachHang();
                ctl3.Dock = DockStyle.Fill;
                split3.Panel2.Controls.Add(ctl3);

                using (var db = new MasterDataContext())
                {
                    var Firstday = db.GetSystemDate().AddDays(-(db.GetSystemDate().Day - 1));
                    var LastDay = db.GetSystemDate().AddDays(-(db.GetSystemDate().Day - 1)).AddMonths(1).AddDays(-1);

                    //var objNVBenMua = db.mglgdGiaoDiches.Where(p => SqlMethods.DateDiffDay(p.NgayGD, Firstday) <= 0 && SqlMethods.DateDiffDay(p.NgayGD, LastDay) >= 0).Select(p => new { p.NhanVien.HoTen }).ToList();
                    //var objNVBenBan = db.mglgdGiaoDiches.Where(p => SqlMethods.DateDiffDay(p.NgayGD, Firstday) <= 0 && SqlMethods.DateDiffDay(p.NgayGD, LastDay) >= 0).Select(p => new { p.NhanVien1.HoTen }).ToList();
                    var objNVLQ = db.mglgdHHNhanViens.Where(p =>p.mglgdGiaoDich.MaTT == 7 && SqlMethods.DateDiffDay(p.mglgdGiaoDich.NgayGD, Firstday) <= 0 && SqlMethods.DateDiffDay(p.mglgdGiaoDich.NgayGD, LastDay) >= 0).Select(p => new { p.NhanVien.HoTen }).ToList();

                    //var ltSource = objNVBenBan.Concat(objNVBenMua).Concat(objNVLQ).GroupBy(p => p.HoTen).Select(
                    var ltSource = objNVLQ.GroupBy(p => p.HoTen).Select(
                        p => new
                        {
                            HoTen = p.Key,
                            GiaoDich = p.Count()
                        }).OrderByDescending(p => p.GiaoDich).ToList();
                    switch (ltSource.Count)
                    {
                        case 0:
                            break;
                        case 1:
                            groupDiamond.Text = ltSource[0].HoTen.ToUpper();
                            break;
                        case 2:
                            groupDiamond.Text = ltSource[0].HoTen.ToUpper();
                            groupPlatinum.Text = ltSource[1].HoTen.ToUpper();
                            break;
                        case 3:
                            groupDiamond.Text = ltSource[0].HoTen.ToUpper();
                            groupPlatinum.Text = ltSource[1].HoTen.ToUpper();
                            groupSilver.Text = ltSource[2].HoTen.ToUpper();
                            break;
                    }
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }

        }

        private void View_ctl_SizeChanged(object sender, EventArgs e)
        {
           // split1.SplitterPosition = (this.Width / 2);
           // split1.Panel1.SplitterPosition = (this.Width / 2);
            //splitContainerReportMain.SplitterPosition = (this.Width / 2);
        }
    }
}
