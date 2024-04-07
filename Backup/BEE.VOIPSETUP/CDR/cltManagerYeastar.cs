using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;
//using Renci.SshNet;
//using Renci.SshNet.Sftp;
using BEE.ThuVien;
using Renci.SshNet;
using BEEREMA;

namespace BEE.VOIPSETUP.CDR
{
    public partial class cltManagerYeastar : DevExpress.XtraEditors.XtraUserControl
    {
        public MySqlConnection connection;
        byte SDBID = 0;
        MasterDataContext db = new MasterDataContext();

        public string path { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Port { get; set; }

        public string accesssftp()
        {
            var url = "";
            string remoteDirectory = path;


            string host = this.Server;
            string username = this.Username;
            string password = this.Password;

            //Thread myThread = new System.Threading.Thread(delegate ()
            //{

            //using (SftpClient sftp = new SftpClient(host, username, password))
            //{
            //    try
            //    {
            //        sftp.Connect();
            //        remoteDirectory = remoteDirectory.Substring(0, 39);
            //        var files = sftp.ListDirectory(remoteDirectory);
            //        var name = path.Replace(remoteDirectory, "");

            //        foreach (var file in files)
            //        {
            //            if (name == file.Name)
            //            {
            //                url = file.Name;
            //            }

            //        }
            //        sftp.Disconnect();
            //    }

            //    catch (Exception er)
            //    {
            //        Console.WriteLine("An exception has been caught " + er.ToString());
            //    }
            //}
            return url;
        }

        public void downloadFile()
        {
            string host = this.Server;
            string username = this.Username;
            string password = this.Password;
            string path = gvLichSu.GetFocusedRowCellValue("recordingfile") == null ? "" : gvLichSu.GetFocusedRowCellValue("recordingfile").ToString();
            if (path == "")
            {
                
                MessageBox.Show("Không tồn tại file ghi âm, vui lòng chọn lại!");
                return;
            } 
            DateTime calldate = (DateTime)gvLichSu.GetFocusedRowCellValue("calldate");
            // Path to file on SFTP server
            string remoteDirectory = path;
            // Path where the file should be saved once downloaded (locally)
            var s = path.Substring(0, 3);
            if (path.Substring(0, 3) == "out")
            {
                string d;
                string m;
                int date1 = calldate.Day;
                int month1 = calldate.Month;
                if (date1 < 10)
                {
                    d = "0" + date1.ToString();
                }
                else
                {
                    d = date1.ToString();
                }

                if (month1 < 10)
                {
                    m = "0" + month1.ToString();
                }
                else
                {
                    m = month1.ToString();
                }

                remoteDirectory = "/var/spool/asterisk/monitor/" + calldate.Year + "/" + m + "/" + d + "/" + path;
                var name = path.Replace(remoteDirectory, "");
            }
            else
            {
                 remoteDirectory = path;
                var name = path.Replace(remoteDirectory, "");
            }
           
           
          
            this.saveFileDialog1.DefaultExt = "wav";
            this.saveFileDialog1.Filter = "Wav file |*.wav|Wav file (*.wav)|*.*";
            this.saveFileDialog1.AddExtension = true;
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.Title = "Chọn thư mục bạn muốn lưu file ghi âm";
            this.saveFileDialog1.InitialDirectory = "C:/";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string pathLocalFile = this.saveFileDialog1.FileName;
                using (SftpClient sftp = new SftpClient(host, username, password))
                {
                    try
                    {

                        sftp.Connect();

                        Console.WriteLine("Downloading {0}", remoteDirectory);

                        using (Stream fileStream = File.OpenWrite(pathLocalFile))
                        {
                            sftp.DownloadFile(remoteDirectory, fileStream);
                        }

                        sftp.Disconnect();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("File không tồn tại!");
                        return;
                    }
                    if (DialogBox.Question("Đã tải xuống, bạn có mở file ghi âm không?") == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo("wmplayer.exe", pathLocalFile));
                    }
                    else
                    {
                        return;
                    }
                }
               
                
            }
           

           
        }

        PopupUCMThienVM.PopupUCMThienVM m;
        private void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();

            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }
        public void LoadCDRYeaStar()
        {
            List<itemCDR> list = new List<itemCDR>();
            clsCDRMySQL cls = new clsCDRMySQL();
            DataTable obj = new DataTable();
            List<BEE.VOIPSETUP.CDR.itemCDR> datalist = new List<BEE.VOIPSETUP.CDR.itemCDR>();
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            try
            {
                obj = cls.Select(tuNgay, denNgay);
            }
            catch
            {

                return;
            }

            foreach (DataRow row in obj.Rows)
            {
                itemCDR s = new itemCDR();

                if (row["calldate"] != null && row["calldate"] != DBNull.Value)
                    s.calldate = (DateTime?)row["calldate"];
                else
                    s.calldate = null;


                if (row["clid"] != null && row["clid"] != DBNull.Value)
                    s.clid = (string)row["clid"] ?? "";
                else
                    s.clid = null;



                if (row["src"] != null && row["src"] != DBNull.Value)
                    s.src = (string)row["src"] ?? "";
                else
                    s.src = null;

                if (row["dst"] != null && row["dst"] != DBNull.Value)
                    s.dst = (string)row["dst"] ?? "";
                else
                    s.dst = null;


                if (row["dcontext"] != null && row["dcontext"] != DBNull.Value)
                    s.dcontext = (string)row["dcontext"] ?? "";
                else
                    s.dcontext = null;


                if (row["dstchannel"] != null && row["dstchannel"] != DBNull.Value)
                    s.dstchannel = (string)row["dstchannel"] ?? "";
                else
                    s.dstchannel = null;



                if (row["lastapp"] != null && row["lastapp"] != DBNull.Value)
                    s.lastapp = (string)row["lastapp"] ?? "";
                else
                    s.lastapp = null;



                if (row["lastdata"] != null && row["lastdata"] != DBNull.Value)
                    s.lastdata = (string)row["lastdata"] ?? "";
                else
                    s.lastdata = null;

                s.duration = (int)row["duration"];
                s.billsec = (int)row["billsec"];


                if (row["disposition"] != null && row["disposition"] != DBNull.Value)
                    s.disposition = (string)row["disposition"] ?? "";
                else
                    s.lastdata = null;

                s.amaflags = (int)row["amaflags"];

                if (row["accountcode"] != null && row["accountcode"] != DBNull.Value)
                    s.accountcode = (string)row["accountcode"] ?? "";
                else
                    s.accountcode = null;

                if (row["uniqueid"] != null && row["uniqueid"] != DBNull.Value)
                    s.uniqueid = (string)row["uniqueid"] ?? "";
                else
                    s.uniqueid = null;

                if (row["userfield"] != null && row["userfield"] != DBNull.Value)
                    s.userfield = (string)row["userfield"] ?? "";
                else
                    s.userfield = null;


                if (row["recordingfile"] != null && row["recordingfile"] != DBNull.Value)
                    s.recordingfile = (string)row["recordingfile"] ?? "";
                else
                    s.recordingfile = null;


                if (row["cnum"] != null && row["cnum"] != DBNull.Value)
                    s.cnum = (string)row["cnum"] ?? "";
                else
                    s.cnum = null;


                if (row["cnam"] != null && row["cnam"] != DBNull.Value)
                    s.cnam = (string)row["cnam"] ?? "";
                else
                    s.cnam = null;

                if (row["outbound_cnum"] != null && row["outbound_cnum"] != DBNull.Value)
                    s.outbound_cnum = (string)row["outbound_cnum"] ?? "";
                else
                    s.outbound_cnum = null;



                if (row["outbound_cnam"] != null && row["outbound_cnam"] != DBNull.Value)
                    s.outbound_cnam = (string)row["outbound_cnam"] ?? "";
                else
                    s.outbound_cnam = null;


                if (row["dst_cnam"] != null && row["dst_cnam"] != DBNull.Value)
                    s.dst_cnam = (string)row["dst_cnam"] ?? "";
                else
                    s.dst_cnam = null;

                //if (row["did"] != null && row["did"] != DBNull.Value)
                //    s.did = (string)row["did"] ?? "";
                //else
                //    s.did = null;

                datalist.Add(s);

            }
            gcLICHSU.DataSource = datalist;
        }

        //private void LoadHis()
        //{

        //    try
        //    {
        //        var tuNgay = (DateTime?)itemTuNgay.EditValue;
        //        var denNgay = (DateTime?)itemDenNgay.EditValue;
        //        if (gvLichSu.FocusedRowHandle == 0) gvLichSu.FocusedRowHandle = -1;
        //        //  var obj1 = db.rptGet_CDRHis(tuNgay, denNgay).First().dst;

        //        var obj = db.rptGet_CDRMySQL(tuNgay, denNgay).Select(
        //            p => new
        //            {
        //                p.accountcode,
        //                p.clid,
        //                p.amaflags,
        //                p.billable,
        //                p.datetime,
        //                p.dcontext,
        //                disposition = p.disposition == "ANSWERED" ? "Thành công" : (p.disposition == "NO ANSWER" ? "Không trả lời" : (p.disposition == "FAILED" ? "Gọi nhỡ" : (p.disposition == "BUSY" ? "Số máy bận" : (p.disposition)))),
        //                p.dst,
        //                //dstchannel = p.dst.Length > 8 ? p.dst : (p.dstchannel == "" ? p.dst : (p.dst.Length == p.src.Length ? p.dst : p.dstchannel.Substring(4, p.dstchannel.IndexOf("-") - p.dstchannel.IndexOf("/") - 1))),
        //                p.duration,
        //                p.lastapp,
        //                p.lastdata,
        //                //p.note,
        //                p.src,
        //              //  p.trungke,
        //                p.uniqueid,
        //              //  p.userfield,
        //                p.GhiChu,
        //                p.TenKH,
        //                p.MaNV,
        //                p.NgayHenGL,
        //                p.DaGoiLai,
        //                p.recordfile,
        //                p.TenCongTy,
        //                p.HoTen,
        //                p.usercost,
        //                p.extfield1,
        //                p.extfield2,
        //                p.extfield3,
        //                p.extfield4,
        //                p.srctrunk,
        //                calltype  = p.calltype == "Inbound" ? "Gọi đến" : "Gọi đi"

        //            }).OrderByDescending(p => p.datetime).ToList();

        //        gcLICHSU.DataSource = obj;

        //    }
        //    catch (Exception ex)
        //    {
        //        DialogBox.Error(ex.Message);
        //    }
        //}

        public cltManagerYeastar()
        {
            InitializeComponent();
            db = new MasterDataContext();

            this.Load += new EventHandler(ctlManager_Load);
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
            itemDenNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
            cmbKyBaoCao.EditValueChanged += new EventHandler(cmbKyBaoCao_EditValueChanged);
            itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemRefresh_ItemClick);

        }

        void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // LoadHis();
            LoadCDRYeaStar();
        }

        void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadCDRYeaStar();
        }

        void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadCDRYeaStar();
        }

        void ctlManager_Load(object sender, EventArgs e)
        {
            // this.SDBID = Common.Permission(barManager1, 202);

            //lookNV.DataSource = lookNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
            //lookLoaiHoaDon.DataSource = db.LoaiHoaDons.ToList();
            //cmbCongTy.DataSource = db.CongTies.Select(p => new { p.ID, p.TenCT }).ToList();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(1);
            timer1.Start();

            try
            {
                voipServerConfig objConfig = db.voipServerConfigs.FirstOrDefault();
                string host = objConfig.Host;
                int port = Convert.ToInt32(objConfig.Port);
                string user = objConfig.UserName;
                string password = objConfig.Pass;
                m = new PopupUCMThienVM.PopupUCMThienVM(host, port, user, password);
            }
            catch
            {
                
                
            }
           
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcLICHSU);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            it.CommonCls.ExportExcel(gcLICHSU);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //LoadCDRYeaStar();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            PopupUCMThienVM.PopupUCMThienVM m = new PopupUCMThienVM.PopupUCMThienVM();
            if (gvLichSu.FocusedRowHandle < 0)
                return;
            string Audio = gvLichSu.GetFocusedRowCellValue("recordfiles") == null ? "" : gvLichSu.GetFocusedRowCellValue("recordfiles").ToString();
            if (Audio == "")
                return;
            List<string> list = new List<string>();
            var obj = Audio.Split('@');

            var objConfig = new voipServerConfig();
            if (db.voipServerConfigs.ToList().Count <= 0)
                return;
            objConfig = db.voipServerConfigs.FirstOrDefault();
            m.host = objConfig.Host;
            m.port = Convert.ToInt32(objConfig.Port);
            m.username = objConfig.UserName;
            m.password = objConfig.Pass;
            if (m.xacthuc(objConfig.KeyConnect))
            {
                m.SetUCM_Download(objConfig.Host, 8443, objConfig.UserCDR, objConfig.PassCDR);//can setup cai nay
                if (obj.Count() == 2)
                {
                    m.download("monitor", obj[0]);
                }
                if (obj.Count() == 3)
                    m.download("monitor", obj[1]);

            }

        }

        private void btnGhiAm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            PopupUCMThienVM.PopupUCMThienVM m = new PopupUCMThienVM.PopupUCMThienVM();
            if (gvLichSu.FocusedRowHandle < 0)
                return;
            string Audio = gvLichSu.GetFocusedRowCellValue("recordfiles") == null ? "" : gvLichSu.GetFocusedRowCellValue("recordfiles").ToString();
            if (Audio == "")
                return;
            List<string> list = new List<string>();
            var obj = Audio.Split('@');

            var objConfig = new voipServerConfig();
            if (db.voipServerConfigs.ToList().Count <= 0)
                return;
            objConfig = db.voipServerConfigs.FirstOrDefault();
            m.host = objConfig.Host;
            m.port = Convert.ToInt32(objConfig.Port);
            m.username = objConfig.UserName;
            m.password = objConfig.Pass;
            if (m.xacthuc(objConfig.KeyConnect))
            {
                m.SetUCM_Download(objConfig.Host, 8443, objConfig.UserCDR, objConfig.PassCDR);//can setup cai nay
                if (obj.Count() == 2)
                {
                    m.download("monitor", obj[0]);
                }
                if (obj.Count() == 3)
                    m.download("monitor", obj[1]);

            }
        }

        private void itemRecord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string obj = gvLichSu.GetFocusedRowCellValue("recordingfilename") == null ? "" : gvLichSu.GetFocusedRowCellValue("recordingfilename").ToString();
            var linenumber = gvLichSu.GetFocusedRowCellValue("dst").ToString();
            if (obj == "")
            {
                MessageBox.Show("Không tồn tại file ghi âm, vui lòng chọn lại!");
            }

            else
            {
                var objConfig = new voipServerConfig();
                if (db.voipServerConfigs.ToList().Count <= 0)
                    return;
                objConfig = db.voipServerConfigs.FirstOrDefault();
                m.host = objConfig.Host;
                m.port = Convert.ToInt32(objConfig.Port);
                m.username = objConfig.UserName;
                m.password = objConfig.Pass;
                //  m.UserNameCDR = objConfig.UserCDR;
                //m.PassCDR = objConfig.PassCDR;
                //m. = linenumber;
                if (m.xacthuc(objConfig.KeyConnect))
                {
                    m.SetUCM_Download(objConfig.Host, 9999, objConfig.UserCDR, objConfig.PassCDR);//can setup cai nay

                    m.download("monitor", obj);


                }
            }

        }

        private void btnListen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            var objConfig = db.voipServerConfigs.FirstOrDefault();


            string obj = gvLichSu.GetFocusedRowCellValue("recordingfilename") == null ? "" : gvLichSu.GetFocusedRowCellValue("recordingfilename").ToString();
            if (obj == "")
            {
                MessageBox.Show("Không tồn tại file ghi âm, vui lòng chọn lại!");
            }
            try
            {
                string url = "https://" + objConfig.Host + ":9999" + "/cgi-bin/fileget.cgi?username=" + objConfig.UserCDR + "&password=" + objConfig.PassCDR + "&action=rcdownload&filename=recording/" + obj;
                //WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                //wplayer.URL = url;
                //wplayer.controls.play();
                Process.Start(new ProcessStartInfo("wmplayer.exe", url));
            }
            catch { }
        }

        private void btnDownload_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MasterDataContext db = new MasterDataContext();
            var obj = db.DatabaseConfigs.SingleOrDefault(p => p.ID == 2);
            this.Server = obj.Server;
            this.Port = obj.Port;
            this.Username = obj.Username;
            this.Password = obj.Password;
            downloadFile();
        }
    }
}
