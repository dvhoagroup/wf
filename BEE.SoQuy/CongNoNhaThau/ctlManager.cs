using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.THUVIEN;
using BEE;
using BEE.DULIEU;
using BEEREM;

namespace BEE.SoQuy.CongNoNhaThau
{
    public partial class ctlManager : BControl
    {
        MasterDataContext db = new MasterDataContext();
        int SDBID = 6;
        public ctlManager()
        {
            InitializeComponent();
        }

        void CongNo_Load()
        {
            var wait = BEE.DialogBox.WaitingForm();
            try
            {
                DateTime? tuNgay = (DateTime?)itemTuNgay.EditValue;
                DateTime? denNgay = (DateTime?)itemDenNgay.EditValue;
                int maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
                gcCongNo.DataSource = db.cTongHopCongNoNT_getAll(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                BEE.DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        void LoadPermission()
        {
            var ltAction = db.ActionDatas.Where(p => p.PerID == BEE.THUVIEN.Common.PerID & p.FormID == 184).Select(p => p.FeatureID).ToList();

            itemNhacNoEmail.Enabled = ltAction.Contains(47);
            btnThuTien.Enabled = ltAction.Contains(201);
          //  itemExport1.Enabled = ltAction.Contains(46);
            var ltAccess = db.AccessDatas.Where(p => p.PerID == BEE.THUVIEN.Common.PerID & p.FormID == 184).Select(p => p.SDBID).ToList();
            if (ltAccess.Count > 0)
                this.SDBID = ltAccess[0];
            var listGH = db.ActionDatas.Where(p => p.PerID == BEE.THUVIEN.Common.PerID & p.FormID == 184).Select(p => p.FeatureID).ToList();
            itemSuaNgayTT.Enabled = listGH.Contains(1);
        }

        void CongNo_Click()
        {
            if (grvCongNo.FocusedRowHandle < 0)
            {
                gcPhieuChi.DataSource = null;
                gcLSCN.DataSource = null;
                gcGiaHan.DataSource = null;
                return;
            }
            var id = Convert.ToInt32(grvCongNo.GetFocusedRowCellValue("ID"));
            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                     gcPhieuChi.DataSource = (from pc in db.cNTPhieuChis
                                         join nv in db.NhanViens on pc.MaNV equals nv.MaNV into nhanvien
                                         from nv in nhanvien.DefaultIfEmpty()
                                         where pc.DotTT == id
                                         orderby pc.NgayChi descending
                                         select new
                                         {
                                             pc.ID,
                                             pc.SoPC,
                                             pc.NgayChi,
                                             pc.NguoiNhan,
                                             pc.MaNV,
                                             pc.SoTien,
                                           //  SoTien = ct.SoTien * pc.TyGia,
                                             pc.MaLT,
                                             pc.LyDo,
                                             pc.MaNVN,
                                             pc.NgayNhap,
                                             pc.MaNVS,
                                             pc.NgaySua
                                         }).ToList();
                     if (grvChi.FocusedRowHandle == 0) grvChi.FocusedRowHandle = -1;
                    break;
                case 1:
                    if (grvCongNo.GetFocusedRowCellValue("MaPGC") != DBNull.Value)
                    {
                        gcLSCN.DataSource = BEE.LttLichSuCapNhatCls.getByPGC(
                            (byte)grvCongNo.GetFocusedRowCellValue("DotTT"),
                            (int)grvCongNo.GetFocusedRowCellValue("MaPGC"));
                    }
                    else
                    {
                        gcLSCN.DataSource = BEE.LttLichSuCapNhatCls.getByPGC(
                            (byte)grvCongNo.GetFocusedRowCellValue("DotTT"),
                             (int)grvCongNo.GetFocusedRowCellValue("MaHDMB"));
                    }
                    break;
                case 2:
                    gcGiaHan.DataSource = db.nnGiaHan_selectBy((int)grvCongNo.GetFocusedRowCellValue("ID"));
                    break;
                case 3:
                    gcNhatKy.DataSource = db.pgcNhatKies.Where(p => p.MaPGC == (int)grvCongNo.GetFocusedRowCellValue("MaPGC"))
                            .Select(p => new { p.MaNK, p.NgayXL, p.NhanVien.HoTen, p.DienGiai, p.NgayNhap })
                            .OrderByDescending(p => p.NgayXL);
                    break;
                case 4:
                    gcSMS.DataSource = db.smsCampaign_Customers.Where(p => p.MaLichTT == (int)grvCongNo.GetFocusedRowCellValue("ID")).Select(p => new
                    {
                        p.IsSucces,
                        p.Number,
                        p.ContentSend,
                        p.smsCampaign.DateCreate,
                        p.smsCampaign.NhanVien.HoTen
                    });
                    break;
            }
        }

        private void SetDate(int index)
        {
            BEE.KyBaoCaoCls objKBC = new BEE.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            LoadPermission();
            lookNhanVienNhap.DataSource = db.NhanViens.Select(p => new { p.HoTen, p.MaNV });
            lookDuAn.DataSource = db.DuAn_getList();
            lookUpEditTinhTrang.DataSource = db.nnghTinhTrangs;
            BEE.KyBaoCaoCls objKBC = new BEE.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
            CongNo_Load();

            TranslateUserControl(this, barManager1);
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CongNo_Load();
        }

        private void btnThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            if (grvCongNo.GetFocusedRowCellValue("ID") == null)
            {
                BEE.DialogBox.Infomation("Vui lòng chọn [Đợt thanh toán], xin cảm ơn.");
                return;
            }
            decimal cn = Convert.ToDecimal(grvCongNo.GetFocusedRowCellValue("ConLai"));
            var cn2 = grvCongNo.GetFocusedRowCellValue("ConLai");
            if (cn > 0 | cn2 == null)
            {
                BEE.SoQuy.PhieuChiHDNhaThau.frmEditV2 frm = new BEE.SoQuy.PhieuChiHDNhaThau.frmEditV2();
                frm.ContractID = Convert.ToInt32(grvCongNo.GetFocusedRowCellValue("ContractID"));
                //  frm.ActionID = 8;
                try
                {
                    // frm.SoTien = Convert.ToDecimal(grvLichTT.GetFocusedRowCellValue("TuongUng")) + Convert.ToDecimal(grvLichTT.GetFocusedRowCellValue("ThueVAT"));
                    frm.SoTien = Convert.ToInt32(grvCongNo.GetFocusedRowCellValue("SoTien"));
                    frm.DotThanhToan = grvCongNo.GetFocusedRowCellValue("DotThanhToan").ToString();
                    frm.DotTT = Convert.ToInt32(grvCongNo.GetFocusedRowCellValue("ID"));
                    frm.ConLai = Convert.ToDecimal(grvCongNo.GetFocusedRowCellValue("ConLai"));
                    frm.TienChi = Convert.ToDecimal(grvCongNo.GetFocusedRowCellValue("TienChi"));
                    frm.PhanTramTT = Convert.ToInt32(grvCongNo.GetFocusedRowCellValue("PhanTramTT"));
                }
                catch
                {
                    frm.SoTien = Convert.ToInt32(grvCongNo.GetFocusedRowCellValue("SoTien"));
                    frm.DotThanhToan = grvCongNo.GetFocusedRowCellValue("DotThanhToan").ToString();
                    frm.ConLai = Convert.ToDecimal(grvCongNo.GetFocusedRowCellValue("ConLai"));
                    frm.DotTT = Convert.ToInt32(grvCongNo.GetFocusedRowCellValue("ID"));
                    frm.TienChi = Convert.ToDecimal(grvCongNo.GetFocusedRowCellValue("TienChi"));
                    frm.PhanTramTT = Convert.ToInt32(grvCongNo.GetFocusedRowCellValue("PhanTramTT"));
                }
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    CongNo_Load();
            }
            else
                BEE.DialogBox.Infomation("đã chi xong tiền đợt này nên không thể thực hiện thao tác này.\r\nVui lòng chọn đợt thanh toán muốn khác, xin cảm ơn.");


        }

        private void itemSuaNgayTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int[] indexs = grvCongNo.GetSelectedRows();
            //if (indexs.Length <= 0)
            //{
            //    BEE.DialogBox.Error("Vui lòng chọn [Đợt thanh toán], xin cảm ơn.");
            //    return;
            //}

            //var f = new GiaHan.frmEdit();
            //f.MaPGC = (int)grvCongNo.GetFocusedRowCellValue("MaPGC");
            //f.MaKH = (int)grvCongNo.GetFocusedRowCellValue("MaKH");
            //f.MaLTT = (int)grvCongNo.GetFocusedRowCellValue("ID");
            //f.MaDA = (int)grvCongNo.GetFocusedRowCellValue("MaDA");
            //f.SoHD = grvCongNo.GetFocusedRowCellValue("MaSoGD").ToString();
            //f.ShowDialog();

            //frmHenTra frm = new frmHenTra();
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    var wait = BEE.DialogBox.WaitingForm();
            //    foreach (int i in indexs)
            //    {
            //        if (grvCongNo.GetRowCellValue(i, "MaPGC") != DBNull.Value)
            //        {
            //            BEE.pgcLichThanhToanCls objLTT = new BEE.pgcLichThanhToanCls();
            //            objLTT.PGC.MaPGC = (int)grvCongNo.GetRowCellValue(i, "MaPGC");
            //            objLTT.DotTT = (byte)grvCongNo.GetRowCellValue(i, "DotTT");
            //            objLTT.NgayTT = frm.NgayTT;
            //            objLTT.DienGiai = frm.DienGiai;
            //            objLTT.UpdateNgayTT();
            //        }
            //        else
            //        {
            //            BEE.hdmbLichThanhToanCls objLTT = new BEE.hdmbLichThanhToanCls();
            //            objLTT.HDMB.MaHDMB = (int)grvCongNo.GetRowCellValue(i, "MaHDMB");
            //            objLTT.DotTT = (byte)grvCongNo.GetRowCellValue(i, "DotTT");
            //            objLTT.NgayTT = frm.NgayTT;
            //            objLTT.DienGiai = frm.DienGiai;
            //            objLTT.UpdateNgayTT();
            //        }
            //    }
            //    wait.Close();
            //    wait.Dispose();

            //    CongNo_Load();
            //}
        }

        private void grvCongNo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CongNo_Click();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            CongNo_Click();
        }

        private void grvPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            if (grvChi.FocusedRowHandle >= 0)
            {
                if (grvChi.FocusedRowHandle < 0) return;
                SoQuy.PhieuThuBanHang.frmEdit frm = new SoQuy.PhieuThuBanHang.frmEdit();
                frm.MaPT = (int)grvChi.GetFocusedRowCellValue("MaPT");
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    CongNo_Load();
            }
        }

        private void grvPhieuThu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var indexs = grvChi.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    BEE.DialogBox.Error("Vui lòng chọn phiếu thu");
                    return;
                }
                if (BEE.DialogBox.Question() == DialogResult.No) return;
                foreach (var i in indexs)
                {
                    pgcPhieuThu objPT = db.pgcPhieuThus.Single(p => p.MaPT == (int)grvChi.GetRowCellValue(i, "MaPT"));
                    db.pgcPhieuThus.DeleteOnSubmit(objPT);
                }
                db.SubmitChanges();

                CongNo_Load();
            }
        }

        private void itemNhacNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (grvCongNo.FocusedRowHandle < 0)
            //    return;
            //var objLTT = (CongNo_getAllResult)grvCongNo.GetFocusedRow();
            //string s = "";
            //s += string.Format("<P>Kính gửi Quý khách hàng: {0} </P>\r\n", objLTT.HoTenKH);
            //s += string.Format("<P>Căn cứ vào hợp đồng giao dịch bất động sản số {0} về căn hộ {1} </P> \r\n", objLTT.MaSoGD, objLTT.KyHieu);
            //s += string.Format("<P>Công ty cổ phần Tập đoàn FLC Xin trân trọng thông báo tới quý khách hàng tình hình dư nợ còn tồn đọng như sau: </P> \r\n");
            //s += string.Format("<P>Tổng số tiền phải thu: \n\n\n\n {0:#,0.##} </P>\r\n", objLTT.SoTien);
            //s += string.Format("<P>Tổng số tiền đã thu:   \n\n\n\n\n {0:#,0.##} </P>\r\n", objLTT.DaThu);
            //s += string.Format("<P>Tổng số tiền còn phải thu: {0:#,0.##} </P>\r\n", objLTT.ConNo);
            //s += string.Format("<P>Kính mong quý khách hàng thu xếp thời gian thanh toán đúng hạn vào ngày {0:dd/MM/yyyy}!</P> \r\n", objLTT.NgayTT);
            //s += string.Format("<P>Trân trọng!</P>");
            //using (var frm = new BEE.QuangCao.MailV2.frmContentEdit() { noiDung = s, MaLichTT = (int?)grvCongNo.GetFocusedRowCellValue("ID"), Email = grvCongNo.GetFocusedRowCellValue("Email").ToString() })
            //{
            //    frm.ShowDialog();
            //}

        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.CommonCls.ExportExcel(gcCongNo);
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //try
            //{
            //    int[] indexs = grvCongNo.GetSelectedRows();
            //    if (indexs.Length <= 0)
            //    {
            //        BEE.DialogBox.Error("Vui lòng chọn công nợ");
            //        return;
            //    }

            //    var frm = new BEEREM.NghiepVu.CongNo.frmXuLyNK();
            //    frm.objNK = new pgcNhatKy();
            //    frm.ShowDialog();

            //    if (frm.DialogResult != DialogResult.OK) return;

            //    foreach (var i in indexs)
            //    {
            //        var objNK = new pgcNhatKy();
            //        objNK.MaPGC = (int)grvCongNo.GetRowCellValue(i, "MaPGC");
            //        objNK.NgayXL = frm.objNK.NgayXL;
            //        objNK.DienGiai = frm.objNK.DienGiai;
            //        objNK.MaNVN = BEE.THUVIEN.Common.MaNV;
            //        objNK.NgayNhap = DateTime.Now;
            //        db.pgcNhatKies.InsertOnSubmit(objNK);
            //    }

            //    db.SubmitChanges();

            //    CongNo_Load();
            //}
            //catch (Exception ex)
            //{
            //    BEE.DialogBox.Error(ex.Message);
            //}
        }
        private static readonly string[] VietnameseSigns = new string[]
        {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"
        };
        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;
        }
        private void itemNhacNoSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var indexs = grvCongNo.GetSelectedRows();
            //if (indexs.Length <= 0)
            //{
            //    BEE.DialogBox.Error("Vui lòng chọn đợt thanh toán");
            //    return;
            //}
            //var lst = new List<BEE.SendSMS.ItemSend>();
            //foreach (var i in indexs)
            //{
            //    string Cont = string.Format("Kinh gui (Ong/Ba): {0}", RemoveSign4VietnameseString(grvCongNo.GetRowCellValue(i, "HoTenKH").ToString()));
            //    Cont += string.Format("-Chu can ho: {0}-Du an The Legend.", grvCongNo.GetRowCellValue(i, "KyHieu").ToString());
            //    Cont += string.Format("Chu dau tu thong bao da den han nop tien dot {0}. ", grvCongNo.GetRowCellValue(i, "DotTT").ToString());
            //    Cont += string.Format("Quy khach can dong tiep so tien la {0:#,0.##}d.", (decimal)grvCongNo.GetRowCellValue(i, "ConNo"));
            //    Cont += string.Format("De nghi Quy khach vui long thanh toan truoc ngay {0}. ", string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddDays(15)));
            //    Cont += string.Format("Chi tiet lien he: Phong nghiep vu 04 22410909.");
            //    //Cont += string.Format("Voi ty le {0:#,0}%, tuong ung {1:#,0.##}", grvCongNo.GetRowCellValue(i, "TyLeTT").ToString(), (decimal)grvCongNo.GetRowCellValue(i, "ConNo"));
            //    var obj = new BEE.SendSMS.ItemSend();
            //    obj.MaKH = (int)grvCongNo.GetRowCellValue(i, "MaKH");
            //    obj.SDT = grvCongNo.GetRowCellValue(i, "DiDong").ToString();
            //    obj.MaLTT = (int)grvCongNo.GetRowCellValue(i, "ID");
            //    obj.Content = Cont;
            //    lst.Add(obj);
            //}
            //using (var frm = new BEE.SendSMS.frmNhacNo() { LSTSend = lst })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void xtraTabPage4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvCongNo_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CongNo_Click();
        }

    }
}

