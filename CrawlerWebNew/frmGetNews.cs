using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using System.Collections;
using System.Threading;
using BEEREMA;

namespace CrawlerWebNew
{
    public partial class frmGetNews : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID { get; set; }
        public int ModuleID { get; set; }
        public int ProjectID { get; set; }
        public string SkinName { get; set; }

        DateTime Dateto { get; set; }
        int Num = 0;

        public frmGetNews()
        {
            InitializeComponent();
        }

        private void frmEditSkin_Load(object sender, EventArgs e)
        {
            dateDenNgay.EditValue = DateTime.Now;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(new ThreadStart(ThreadGetData));
            th.ApartmentState = ApartmentState.MTA;
            th.IsBackground = true;
            th.Start();      
        }

        public void GetNewBDS()
        {
            var crlBDS = new CrawlerWebNew.CrawlerWeb();
            crlBDS.GetAllData(1, Dateto, Num);
            DialogBox.Infomation("Đã hoàn thành việc lấy dữ liệu từ trang Batdongsan");
            Thread.Sleep(100);
        }
        public void GetNewMuaBan()
        {
            var crlMuaBan = new CrawlerWebMuaBan.CrawlerWeb();
            crlMuaBan.GetAllData(2, Dateto, Num);
            DialogBox.Infomation("Đã hoàn thành việc lấy dữ liệu từ trang Muaban.net");
            Thread.Sleep(100);
        }
        public void GetNewRongBay()
        {
            var crlRongBay = new CrawlerWebRongBay.CrawlerWeb();
            crlRongBay.GetAllData(3, Dateto, Num);
            DialogBox.Infomation("Đã hoàn thành việc lấy dữ liệu từ trang Rongbay");
            Thread.Sleep(100);
        }
        public void GetNewNhaDat24()
        {
            var crlNhaDat24h = new CrawlerWebNhaDat24.CrawlerWeb();
            crlNhaDat24h.GetAllData(4, Dateto, Num);
            DialogBox.Infomation("Đã hoàn thành việc lấy dữ liệu từ trang Nhadat24h");
            Thread.Sleep(100);
        }

        public void ThreadGetData()
        {
            Dateto = dateDenNgay.DateTime == null ? DateTime.Now : dateDenNgay.DateTime;
            try
            {
                Thread th1 = new Thread(new ThreadStart(GetNewBDS));
                th1.ApartmentState = ApartmentState.MTA;
                th1.IsBackground = true;
                th1.Start();
                Thread th2 = new Thread(new ThreadStart(GetNewMuaBan));
                th2.ApartmentState = ApartmentState.MTA;
                th2.IsBackground = true;
                th2.Start();
                Thread th3 = new Thread(new ThreadStart(GetNewRongBay));
                th3.ApartmentState = ApartmentState.MTA;
                th3.IsBackground = true;
                th3.Start();
                Thread th4 = new Thread(new ThreadStart(GetNewNhaDat24));
                th4.ApartmentState = ApartmentState.MTA;
                th4.IsBackground = true;
                th4.Start();
                //using (var db = new MasterDataContext())
                //{
                //    foreach (var i in db.crlWebsites.ToList())
                //    {
                //        switch (i.ID)
                //        { 
                //            case 1:
                //                Thread th1 = new Thread(new ThreadStart(GetNewBDS));
                //                th1.ApartmentState = ApartmentState.MTA;
                //                th1.IsBackground = true;
                //                th1.Start();
                //                break;
                //            case 2:
                //                Thread th2 = new Thread(new ThreadStart(GetNewMuaBan));
                //                th2.ApartmentState = ApartmentState.MTA;
                //                th2.IsBackground = true;
                //                th2.Start();
                //                break;
                //            case 3:
                //                Thread th3 = new Thread(new ThreadStart(GetNewRongBay));
                //                th3.ApartmentState = ApartmentState.MTA;
                //                th3.IsBackground = true;
                //                th3.Start();
                //                break;
                //            case 4:
                //                Thread th4 = new Thread(new ThreadStart(GetNewNhaDat24));
                //                th4.ApartmentState = ApartmentState.MTA;
                //                th4.IsBackground = true;
                //                th4.Start();
                //                break;
                //        }
                //    }
                //}
                //   MessageBox.Show("Quá trình lấy dữ liệu đã hoàn tất!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            Thread.Sleep(2);
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Num = Convert.ToInt32(spinEdit1.Value);
        }
    }
}