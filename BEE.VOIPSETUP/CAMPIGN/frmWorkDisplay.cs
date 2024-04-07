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
using System.Data.Linq.SqlClient;


namespace BEE.VOIPSETUP.CAMPAIGN
{
    public partial class frmWorkDisplay : DevExpress.XtraEditors.XtraForm
    {
       // private AsteriskUCMSdk.AsteriskUCMSdk astUCM;
        MasterDataContext db = new MasterDataContext();
        public int? CamID { get; set; }
        voipCampaign objCP;
        public List<BEE.KHACHHANG.ItemSelect> ListKH = new List<BEE.KHACHHANG.ItemSelect>();
        public bool IsSave;
        public frmWorkDisplay()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }


        private void gvDoiTuong_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClickToCall();
            }
        }

        private void ClickToCall()
        {

            if (gvDoiTuong.FocusedRowHandle < 0)
                return;
            var line = db.voipLineConfigs.Where(p=>p.MaNV == Common.MaNV);
            string Phone =gvDoiTuong.GetFocusedRowCellValue("PhoneNumber")==null? "":gvDoiTuong.GetFocusedRowCellValue("PhoneNumber").ToString();
            if (line.Count() == 0)
            {
                DialogBox.Infomation("Tài khoản này chưa được cấp Line. Nên không thể thực hiện cuộc gọi!");
                return;
            }
            if(Phone =="")
            {
                DialogBox.Infomation("Khách hàng này chưa có số điện thoại, vui lòng kiểm tra lại!");
                return;
            }
            var objPhone = db.voipCampaign_ListNumbers.FirstOrDefault(p => p.ID == (int?)gvDoiTuong.GetFocusedRowCellValue("ID"));
            var pre=line.First().PreCall??"";
            var obj = line.First().LineNumber;
          //  UCMClickToCall.ClickToCall((Asterisk.NET.Manager.ManagerConnection)astUCM.ManagerConnection, obj, string.Format("{0}{1}", pre, Phone));
            var objConfig = db.voipServerConfigs.FirstOrDefault();

            PopupUCMThienVM.PopupUCMThienVM m = new PopupUCMThienVM.PopupUCMThienVM(objConfig.Host, 5038,objConfig.UserName, objConfig.Pass);
            if (m.xacthuc(objConfig.Key))
            {
                m.Clicktocall(string.Format("{0}{1}", pre, Phone), obj);
            }
            else
            {
                MessageBox.Show("Key kết nối tổng đài sai. Vui lòng kiểm tra lại");
            }
        }

        void LoadData()
        {

            gcDoiTuong.DataSource = db.voipCampaign_ListNumbers.Where(p => SqlMethods.DateDiffDay(p.voipCampaign.DenNgay, DateTime.Now) <= 0 && SqlMethods.DateDiffDay(p.voipCampaign.TuNgay, DateTime.Now) >= 0 && p.MaNV == Common.MaNV).Select(p => new
            {
                p.ID,
                p.DateCall,
                TrangThai = p.IsCall.GetValueOrDefault() == true ? "Đã gọi" : "Chưa gọi",
                p.PhoneNumber,
                KhachHang = p.MaKH == null ? "" : (p.KhachHang.TenCongTy == "" ? p.KhachHang.HoKH + " " + p.KhachHang.TenKH : p.KhachHang.TenCongTy),
                p.TenKH,
                p.voipCampaign.Name,
                p.NgayGoiThuc
            });
            gvAnswer.ExpandAllGroups();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnCall_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClickToCall();
        }

        private void frmWorkDisplay_Load(object sender, EventArgs e)
        {
            var objConfig = db.voipServerConfigs.FirstOrDefault();
            string host = objConfig.Host;
            int port = Convert.ToInt32(objConfig.Port);
            string user = objConfig.UserName;
            string password = objConfig.Pass;
            //astUCM = new AsteriskUCMSdk.AsteriskUCMSdk(host, port, user, password);
            //try
            //{
            //    astUCM.Connect();
            //}
            //catch
            //{ }
            LoadData();
        }

        private void btnCLose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gvDoiTuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvDoiTuong.FocusedRowHandle < 0)
            {
                return;
            }
            var id = (int?)gvDoiTuong.GetFocusedRowCellValue("ID");
            var objDT = db.voipCampaign_ListNumbers.FirstOrDefault(p => p.ID == (int?)gvDoiTuong.GetFocusedRowCellValue("ID"));
            lookCauHoi.DataSource = db.voipCampaign_Questions.Where(p => p.CamID == objDT.CamID).Select(p => new { p.ID, p.Question });
            lookCauTL.DataSource = db.voipCampaign_Answers.Where(p => p.voipCampaign_Question.CamID == objDT.CamID).Select(p => new { p.ID, p.Result });
            gcAnswer.DataSource = objDT.voipMap_Answers;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDoiTuong.FocusedRowHandle < 0)
            {
                return;
            }
            var id = (int?)gvDoiTuong.GetFocusedRowCellValue("ID");
            var objDT = db.voipCampaign_ListNumbers.FirstOrDefault(p => p.ID == (int?)gvDoiTuong.GetFocusedRowCellValue("ID"));
            objDT.NgayGoiThuc = DateTime.Now;
            objDT.IsCall = true;
            try
            {
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã lưu!");
                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error("Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}