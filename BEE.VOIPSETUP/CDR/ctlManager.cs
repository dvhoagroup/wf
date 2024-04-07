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
using BEE.ThuVien;
using BEEREMA;

namespace BEE.VOIPSETUP.CDR
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        byte SDBID = 0;
        MasterDataContext db = new MasterDataContext();

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

        private void LoadHis()
        {
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                if (gvLichSu.FocusedRowHandle == 0) gvLichSu.FocusedRowHandle = -1;
              //  var obj1 = db.rptGet_CDRHis(tuNgay, denNgay).First().dst;

                //var obj = db.rptGet_CDRHis(tuNgay, denNgay).Select(
                //    p => new
                //    {
                //        p.accountcode,
                //        p.acctid_ucm,
                //        p.amaflags,
                //        p.billsec,
                //        p.calldate,
                //        p.channel,
                //        p.clid,
                //        p.dcontext,
                //        disposition = p.disposition == "ANSWERED" ? "Thành công" : (p.disposition == "NO ANSWER" ? "Không trả lời" : (p.disposition == "FAILED" ? "Gọi nhỡ" : (p.disposition == "BUSY" ? "Số máy bận" : (p.disposition)))),
                //        p.dst,
                //        dstchannel = p.dst.Length > 8 ? p.dst : (p.dstchannel == "" ? p.dst : (p.dst.Length == p.src.Length ? p.dst : p.dstchannel.Substring(4, p.dstchannel.IndexOf("-") - p.dstchannel.IndexOf("/") - 1))),
                //        p.duration,
                //        p.lastapp,
                //        p.lastdata,
                //        p.note,
                //        p.src,
                //        p.trungke,
                //        p.uniqueid,
                //        p.userfield,
                //        p.GhiChu,
                //        p.TenKH,
                //        p.MaNV,
                //        p.NgayHenGL,
                //        p.DaGoiLai
                //    }).OrderByDescending(p => p.calldate).ToList();

                //gcLICHSU.DataSource = obj;

            }
            catch(Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        public ctlManager()
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
            LoadHis();
        }

        void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadHis();
        }

        void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadHis();
        }

        void ctlManager_Load(object sender, EventArgs e)
        {
          //  this.SDBID = Common.Permission(barManager1, 115);

            lookNV.DataSource = lookNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
            
           
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(1);
            timer1.Start();

            voipServerConfig objConfig = db.voipServerConfigs.FirstOrDefault();
            string host = objConfig.Host;
            int port = Convert.ToInt32(objConfig.Port);
            string user = objConfig.UserName;
            string password = objConfig.Pass;
            m = new PopupUCMThienVM.PopupUCMThienVM(host, port, user, password);
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
            LoadHis();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvLichSu.FocusedRowHandle < 0)
                return;
            string Audio = gvLichSu.GetFocusedRowCellValue("recordfiles") == null ? "" : gvLichSu.GetFocusedRowCellValue("recordfiles").ToString();
            if (Audio == "")
                return;
            var objConfig = new voipServerConfig();
            if (db.voipServerConfigs.ToList().Count <= 0)
                return;
            objConfig = db.voipServerConfigs.FirstOrDefault();
            if (m.xacthuc("E2E-43E9-7B71-81F7"))
            {
                m.SetUCM_Download(objConfig.Host, 8443, objConfig.UserCDR, objConfig.PassCDR);//can setup cai nay
                m.download("monitor", Audio);
            }
        }

    }
}
