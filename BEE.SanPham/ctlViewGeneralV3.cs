using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using LandSoft.Library;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace LandSoft.SanPham
{
    public partial class ctlViewGeneralV3 : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable tblMB;
        MasterDataContext db = new MasterDataContext();
        int? MaSP, MaPGC, MaKHBH;
        byte? MaTT;
        int maxCol = 0;
        List<ItemData> listProduct;
        List<bdsTrangThai> listStatus;
        int amountColumn = 100, widthCell = 60, maDA = -1, maKhu = -1;
        Library.DuAn objProject;
        public ctlViewGeneralV3()
        {
            InitializeComponent();

            Permission();
        }

        private void Permission()
        {
            try
            {
                var listAction = db.ActionDatas.Where(p => p.FormID == 4 & p.PerID == LandSoft.Library.Common.PerID)
                    .Select(p => p.FeatureID).ToList();

                itemEdit.Enabled = listAction.Contains(2);
                itemDeleteMap.Enabled = listAction.Contains(3);
                itemImport.Enabled = listAction.Contains(12);
                itemExport.Enabled = listAction.Contains(46);

                //Vay von
                listAction = db.ActionDatas.Where(p => p.FormID == 26 & p.PerID == LandSoft.Library.Common.PerID)
                    .Select(p => p.FeatureID).ToList();
                itemGopVon.Enabled = listAction.Contains(1);
                //Giu cho
                listAction = db.ActionDatas.Where(p => p.FormID == 27 & p.PerID == LandSoft.Library.Common.PerID)
                    .Select(p => p.FeatureID).ToList();
                itemGiuCho.Enabled = listAction.Contains(1);
                //Dat coc
                listAction = db.ActionDatas.Where(p => p.FormID == 28 & p.PerID == LandSoft.Library.Common.PerID)
                    .Select(p => p.FeatureID).ToList();
                itemDatCoc.Enabled = listAction.Contains(1);
                //Hop dong
                listAction = db.ActionDatas.Where(p => p.FormID == 29 & p.PerID == LandSoft.Library.Common.PerID)
                    .Select(p => p.FeatureID).ToList();
                itemMuaBan.Enabled = listAction.Contains(1);
            }
            catch
            {
            }
        }

        string GetRowIndex(int valMax, int focusIndex)
        {
            try
            {
                int realIndex = Convert.ToInt32(grvMatBang.GetRowCellValue(focusIndex, (maxCol + 1).ToString()));
                if (realIndex > 0)
                {
                    //if (valMax - focusIndex > 0)
                        return "Tầng " + realIndex.ToString();
                }
                else
                {
                    if (realIndex == 0)
                        return "Tầng lửng";
                    else
                        return "Tầng trệt";
                }
            }
            catch { }

            return "";
        }

        int GetRowIndexInt(int valMax, int valMaxFloor, int focusIndex)
        {
            if (valMax - valMaxFloor == 1)
            {
                if (valMax - focusIndex >= 0)
                    return valMax - (focusIndex + 1);
                else
                    return focusIndex; 
            }
            else
            {
                if (valMax < valMaxFloor)
                {
                    if (valMax - focusIndex > 0)
                        return (valMax - focusIndex) + 1;
                    else
                    {
                        if (valMax - focusIndex == 0)
                            return 1;

                        if (valMax - focusIndex < 0)
                            return 0;
                        return focusIndex;
                    }
                }
                else
                {
                    if (valMax - focusIndex >= 0)
                        return valMax - focusIndex;
                    else
                    {
                        if (valMax - focusIndex < 0)
                            return 0;
                        return focusIndex;
                    }
                }
            }
        }

        void LoadDataV2()
        {
            var wait = DialogBox.WaitingForm();

            try
            {
                grvMatBang.Columns.Clear();
                gcMatBang.DataSource = null;
                db = new MasterDataContext();

                maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
                maKhu = itemKhu.EditValue != null ? (int)itemKhu.EditValue : -1;
                var objKhu = db.Khus.SingleOrDefault(p => p.MaKhu == maKhu);
                if (objKhu != null)
                {
                    amountColumn = objKhu.AmountColumn ?? 100;
                    widthCell = objKhu.WidthCell ?? 0;
                }

                for (int i = 1; i <= amountColumn; i++)
                {
                    var colMaSo = new DevExpress.XtraGrid.Columns.GridColumn();
                    colMaSo.Caption = "Col" + i.ToString();
                    colMaSo.FieldName = "Col" + i.ToString();
                    colMaSo.VisibleIndex = i - 1;
                    colMaSo.Visible = true;
                    colMaSo.Width = (i == 2 | i == 5) ? 80 : widthCell;
                    grvMatBang.Columns.Add(colMaSo);
                }

                var colProject = new DevExpress.XtraGrid.Columns.GridColumn();
                colProject.Caption = "ProjectID";
                colProject.FieldName = "ProjectID";
                colProject.VisibleIndex = 101;
                colProject.Visible = false;
                grvMatBang.Columns.Add(colProject);

                var colZone = new DevExpress.XtraGrid.Columns.GridColumn();
                colZone.Caption = "ZoneID";
                colZone.FieldName = "ZoneID";
                colZone.VisibleIndex = 102;
                colZone.Visible = false;
                grvMatBang.Columns.Add(colZone);


                if (objProject.IsProject.GetValueOrDefault())
                {
                    var listView = db.ProductViewGenerals.Where(p => p.ProjectID == maDA);
                    gcMatBang.DataSource = listView;
                }
                else
                {
                    var listView = db.ProductViewGenerals.Where(p => p.ProjectID == maDA & p.ZoneID == maKhu);
                    gcMatBang.DataSource = listView;
                }
                
                if(objProject.IsProject.GetValueOrDefault())
                    listProduct = db.bdsSanPhams.Where(p => p.MaDA == maDA).Select(p => new ItemData { MaSP = p.MaSP, KyHieu = p.KyHieuSALE, MaDA = p.MaDA.Value, MaKhu = p.MaKhu.Value, MauNen = p.bdsTrangThai.MauNen.Value, DienTichCH = p.DienTichCH ?? 0, LauSo = p.LauSo ?? 0, TenTT = p.bdsTrangThai.TenTT, MaTT = p.MaTT ?? 1 }).ToList();
                else
                    listProduct = db.bdsSanPhams.Where(p => p.MaDA == maDA & p.MaKhu == maKhu).Select(p => new ItemData { MaSP = p.MaSP, KyHieu = p.KyHieuSALE, MaDA = p.MaDA.Value, MaKhu = p.MaKhu.Value, MauNen = p.bdsTrangThai.MauNen.Value, DienTichCH = p.DienTichCH ?? 0, LauSo = p.LauSo ?? 0, TenTT = p.bdsTrangThai.TenTT, MaTT = p.MaTT ?? 1 }).ToList();

                this.Report();
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void Report()
        {
            int total = listProduct.Count;
            for(int i = 0; i < 10; i++)
            {
                try
                {
                    var str = grvMatBang.GetRowCellValue(i, "Col3").ToString();
                    switch (str)
                    {
                        case "[SLCMB]":
                            grvMatBang.SetRowCellValue(i, "Col3", listProduct.Where(p => p.MaTT == 1).ToList().Count);
                            decimal money = listProduct.Where(p => p.MaTT == 1).ToList().Count;
                            grvMatBang.SetRowCellValue(i, "Col4", string.Format("{0:#,0.##}%", money / total * 100));
                            break;
                        case "[SLMB]":
                            grvMatBang.SetRowCellValue(i, "Col3", listProduct.Where(p => p.MaTT == 2).ToList().Count);
                            decimal money2 = listProduct.Where(p => p.MaTT == 2).ToList().Count;
                            grvMatBang.SetRowCellValue(i, "Col4", string.Format("{0:#,0.##}%", money2 / total * 100));
                            break;
                        case "[SLGC]":
                            grvMatBang.SetRowCellValue(i, "Col3", listProduct.Where(p => p.MaTT == 3).ToList().Count);
                            decimal money3 = listProduct.Where(p => p.MaTT == 3).ToList().Count;
                            grvMatBang.SetRowCellValue(i, "Col4", string.Format("{0:#,0.##}%", money3 / total * 100));
                            break;
                        case "[SLDC]":
                            grvMatBang.SetRowCellValue(i, "Col3", listProduct.Where(p => p.MaTT == 4).ToList().Count);
                            decimal money4 = listProduct.Where(p => p.MaTT == 4).ToList().Count;
                            grvMatBang.SetRowCellValue(i, "Col4", string.Format("{0:#,0.##}%", money4 / total * 100));
                            break;
                        case "[SLVV]":
                            grvMatBang.SetRowCellValue(i, "Col3", listProduct.Where(p => p.MaTT == 5).ToList().Count);
                            decimal money5 = listProduct.Where(p => p.MaTT == 5).ToList().Count;
                            grvMatBang.SetRowCellValue(i, "Col4", string.Format("{0:#,0.##}%", money5 / total * 100));
                            break;
                        case "[SLDB]":
                            grvMatBang.SetRowCellValue(i, "Col3", listProduct.Where(p => p.MaTT == 6).ToList().Count);
                            decimal money6 = listProduct.Where(p => p.MaTT == 6).ToList().Count;
                            grvMatBang.SetRowCellValue(i, "Col4", string.Format("{0:#,0.##}%", money6 / total * 100));
                            break;
                        case "[SLBG]":
                            grvMatBang.SetRowCellValue(i, "Col3", listProduct.Where(p => p.MaTT == 8).ToList().Count);
                            decimal money8 = listProduct.Where(p => p.MaTT == 8).ToList().Count;
                            grvMatBang.SetRowCellValue(i, "Col4", string.Format("{0:#,0.##}%", money8 / total * 100));
                            break;
                        case "[Total]":
                            grvMatBang.SetRowCellValue(i, "Col3", total);
                            grvMatBang.SetRowCellValue(i, "Col4", "100%");
                            break;
                    }
                }
                catch { }
            }
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();

            try
            {
                grvMatBang.Columns.Clear();
                gcMatBang.DataSource = null;

                List<ItemData> listData = new List<ItemData>();

                int maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
                int maKhu = itemKhu.EditValue != null ? (int)itemKhu.EditValue : -1;

                var listProduct = db.bdsSanPhams.Where(p => p.MaDA == maDA)
                    .Select(p => new { 
                        p.MaSP, 
                        p.KyHieu, 
                        p.MaKH, 
                        p.MaPGC, 
                        p.LauSo, 
                        p.ViTri, 
                        p.GiaBan, 
                        p.DienTichCH, 
                        p.TongGiaBan, 
                        KhachHang = "",//p.MaKH == null ? "" : p.KhachHang.HoTenKH, 
                        p.MaTT, 
                        p.bdsTrangThai.MauNen, 
                        p.bdsTrangThai.TenTT, 
                        YearPay = p.YearPay ?? 0 
                    }).ToList();

                int maxFloor = listProduct.Max(p => p.LauSo) ?? 0;
                int maxItemOfFloor = listProduct.Max(p => p.ViTri) ?? 0;

                for (int i = 1; i <= maxItemOfFloor; i++)
                {
                    for (int j = maxFloor; j > 0; j--)
                    {
                        try
                        {
                            var obj = listProduct.Where(p => p.LauSo == j && p.ViTri == i).FirstOrDefault();
                            if (obj != null)
                            {
                                var item = new ItemData();
                                item.DienTichCH = obj.DienTichCH ?? 0;
                                item.GiaBan = obj.GiaBan ?? 0;
                                item.KhachHang = obj.KhachHang;
                                item.KyHieu = obj.KyHieu;
                                item.MaSP = obj.MaSP;
                                item.MaTT = obj.MaTT ?? 0;
                                item.MauNen = obj.MauNen ?? 0;
                                item.TenTT = obj.TenTT;
                                item.ThanhTien = obj.TongGiaBan ?? 0;
                                listData.Add(item);
                            }
                            else
                            {
                                var item = new ItemData();
                                item.MaSP = 0;
                                item.MaTT = 0;
                                listData.Add(item);
                            }
                        }
                        catch { }
                    }
                }

                gcMatBang.DataSource = listData;
                cardView1.MaximumCardRows = maxFloor;
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void ctlViewGeneral_Load(object sender, EventArgs e)
        {
            lookUpEditDuAn.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });

            listStatus = db.bdsTrangThais.ToList();

            LandSoft.Translate.Language.TranslateUserControl(this, barManager1);
        }

        private void grvMatBang_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //    e.Info.DisplayText = GetRowIndex(grvMatBang.RowCount, e.RowHandle);
        }

        private void grvMatBang_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                string maSoMB = grvMatBang.GetRowCellValue(e.RowHandle, e.Column).ToString();

                if (maSoMB != "")
                {
                    var obj = listProduct.SingleOrDefault(p => p.KyHieu == maSoMB);
                    if (obj != null)
                    {
                        int MauNen = obj.MauNen;
                        e.Appearance.BackColor = Color.FromArgb(MauNen);
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        switch (maSoMB)
                        {
                            case ".":
                                int c1 = listStatus.SingleOrDefault(p => p.MaTT == 1).MauNen ?? -1;
                                e.Appearance.BackColor = Color.FromArgb(c1);
                                e.Appearance.ForeColor = Color.Black;
                                break;

                            case "..":
                                int c2 = listStatus.SingleOrDefault(p => p.MaTT == 2).MauNen ?? -1;
                                e.Appearance.BackColor = Color.FromArgb(c2);
                                e.Appearance.ForeColor = Color.Black;
                                break;

                            case "...":
                                int c3 = listStatus.SingleOrDefault(p => p.MaTT == 3).MauNen ?? -1;
                                e.Appearance.BackColor = Color.FromArgb(c3);
                                e.Appearance.ForeColor = Color.Black;
                                break;

                            case "....":
                                int c4 = listStatus.SingleOrDefault(p => p.MaTT == 4).MauNen ?? -1;
                                e.Appearance.BackColor = Color.FromArgb(c4);
                                e.Appearance.ForeColor = Color.Black;
                                break;

                            case ".....":
                                int c5 = listStatus.SingleOrDefault(p => p.MaTT == 5).MauNen ?? -1;
                                e.Appearance.BackColor = Color.FromArgb(c5);
                                e.Appearance.ForeColor = Color.Black;
                                break;

                            case "......":
                                int c6 = listStatus.SingleOrDefault(p => p.MaTT == 6).MauNen ?? -1;
                                e.Appearance.BackColor = Color.FromArgb(c6);
                                e.Appearance.ForeColor = Color.Black;
                                break;

                            case "........":
                                int c8 = listStatus.SingleOrDefault(p => p.MaTT == 8).MauNen ?? -1;
                                e.Appearance.BackColor = Color.FromArgb(c8);
                                e.Appearance.ForeColor = Color.Black;
                                break;

                            default:
                                e.Appearance.BackColor = Color.FromArgb(-1);
                                e.Appearance.ForeColor = Color.Black;
                                break;
                        }        
                    }
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(-1);        
                }
            }
            catch { }
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            objProject = db.DuAns.SingleOrDefault(p => p.MaDA == (int?)itemDuAn.EditValue);
            if (objProject == null)
            {
                DialogBox.Infomation("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            var ltKhu = db.Khus.Where(p => p.MaDA == (int?)itemDuAn.EditValue).OrderBy(p => p.STT).ToList();
            lookUpEditKhu.DataSource = ltKhu;
            itemKhu.EditValue = null;
        }

        private void itemKhu_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemGiuCho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SanPham_GiaoDich(1);
        }

        private void itemDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SanPham_GiaoDich(2);
        }

        private void itemGopVon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SanPham_GiaoDich(3);
        }

        private void itemMuaBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SanPham_GiaoDich(4);
        }

        void SanPham_GiaoDich(int type)
        {
            MaSP = (int?)cardView1.GetFocusedRowCellValue("MaSP");
            MaTT = (byte?)cardView1.GetFocusedRowCellValue("MaTT");
            if (MaSP == null)
            {
                DialogBox.Error("Vui lòng chọn [Sản phẩm], xin cảm ơn.");
                return;
            }

            if (MaTT != 2)
            {
                DialogBox.Error("[Sản phẩm] này chưa mở bán hoặc đã giao dịch. Vui lòng kiểm tra lại, xin cảm ơn.");
                return;
            }

            var objSP = db.bdsSanPhams.Single(p => p.MaSP == MaSP);
            if (objSP.MaTT != MaTT)
            {
                DialogBox.Error("[Sản phẩm] này chưa mở bán hoặc đã giao dịch. Vui lòng kiểm tra lại, xin cảm ơn.");
                LoadData();
                return;
            }

            if (objSP.MaTT == 2)
            {
                var obj = db.pgcPhieuGiuChos.Where(p => p.MaSP == MaSP).Select(p => new { p.MaTT, p.NgayKy, p.MaPGC, p.MaKGD }).OrderByDescending(p => p.NgayKy).ToList();
                if (obj.Count > 0)
                {
                    if (obj[0].MaTT == 1 | obj[0].MaTT == 3)
                    {
                        goto doo;
                        //DialogBox.Infomation("[Sản phẩm] này đã làm [Phiếu giữ chỗ] và đang trong tình trạng [Chờ duyệt].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                        //return;
                    }
                    else
                    {
                        if (obj[0].MaTT == 8)
                        {
                            var objPDC = db.pdcPhieuDatCocs.Where(p => p.MaPDC == obj[0].MaPGC).Select(p => new { p.MaTT }).ToList();
                            if (objPDC.Count > 0)
                                if (objPDC[0].MaTT != 10)
                                {
                                    DialogBox.Infomation("Sản phẩm này đã làm [Phiếu đặt cọc] và đang trong tình trạng [Chờ duyệt].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                                    return;
                                }
                        }

                        if (obj[0].MaTT == 9)
                        {
                            var objVV = db.vvbhHopDongs.Where(p => p.MaHDVV == obj[0].MaPGC).Select(p => new { p.MaTT }).ToList();
                            if (objVV.Count > 0)
                                if (objVV[0].MaTT != 9)
                                {
                                    DialogBox.Infomation("Sản phẩm này đã làm [Hợp đồng góp vốn] và đang trong tình trạng [Chờ duyệt].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                                    return;
                                }
                        }

                        if (obj[0].MaTT == 10)
                        {
                            var objMB = db.HopDongMuaBans.Where(p => p.MaHDMB == obj[0].MaPGC).Select(p => new { p.MaTT }).ToList();
                            if (objMB.Count > 0)
                                if (objMB[0].MaTT != 9)
                                {
                                    DialogBox.Infomation("Sản phẩm này đã làm [Hợp đồng mua bán] và đang trong tình trạng [Chờ duyệt].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                                    return;
                                }
                        }
                    }
                }
            }

        doo:
            switch (type)
            {
                case 1: //Giu cho
                    NghiepVu.GiuCho.frmEdit frmPGC = new NghiepVu.GiuCho.frmEdit();
                    frmPGC.MaSP = MaSP.Value;
                    frmPGC.ShowDialog();
                    break;
                case 2: //Dat coc
                    NghiepVu.DatCoc.frmEdit frmPDC = new NghiepVu.DatCoc.frmEdit();
                    frmPDC.MaSP = MaSP.Value;
                    frmPDC.ShowDialog();
                    break;
                case 3: //vay von
                    NghiepVu.VayVon.frmEdit frmVayVon = new NghiepVu.VayVon.frmEdit();
                    frmVayVon.MaSP = MaSP.Value;
                    frmVayVon.ShowDialog();
                    break;
                case 4: //mua ban
                    NghiepVu.HDMB.frmEdit frmHDMB = new NghiepVu.HDMB.frmEdit();
                    frmHDMB.MaSP = MaSP.Value;
                    frmHDMB.ShowDialog();
                    break;
            }
        }

        string GetRowInfo(string id)
        {
            string infoText = "";
            try
            {
                var obj = listProduct.SingleOrDefault(p => p.KyHieu == id);
                if (obj != null)
                {
                    infoText = string.Format(" Code: <b>{0}</b>", id);
                    infoText += string.Format("\r\n Area: <b>{0:#,0.##} m2</b>", obj.DienTichCH);
                    infoText += string.Format("\r\n Floor: <b>{0}</b>", obj.LauSo);
                    infoText += string.Format("\r\n Status: <b>{0}</b>", obj.TenTT);
                }
            }
            catch { }
            return infoText;
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gcMatBang) return;

            ToolTipControlInfo info = null;

            try
            {
                GridHitInfo hi = grvMatBang.CalcHitInfo(e.ControlMousePosition);
                if (hi.RowHandle < 0) return;

                if (hi.InRowCell)
                {
                    string id = grvMatBang.GetRowCellValue(hi.RowHandle, hi.Column).ToString();
                    if (id == "") return;
                    info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), GetRowInfo(id));
                }
            }
            catch { }
            finally
            {
                e.Info = info;
            }
        }

        private void grvMatBang_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 | e.CellValue == null)
            {
                MaSP = null;
                MaTT = null;
                MaKHBH = null;
                MaPGC = null;
                return;
            }
            string id = e.CellValue.ToString();

            try
            {
                var objMB = db.bdsSanPhams.Where(p => p.KyHieuSALE == id & p.MaDA == maDA & p.MaKhu == maKhu).FirstOrDefault();
                MaSP = objMB.MaSP;
                MaTT = objMB.MaTT;
                MaKHBH = objMB.MaKHBH;
                MaPGC = objMB.MaPGC;
            }
            catch
            {
                MaSP = null;
                MaTT = null;
                MaKHBH = null;
                MaPGC = null;
            }

            Clicks();
        }

        void Clicks()
        {
            if (splitContainerControl1.Collapsed)
            {
                gcLTT.DataSource = null;
                gcLSGD.DataSource = null;
                gcPhieuThu.DataSource = null;
                ctlChinhSachChung.ChinhSach_Clear();
                gcChuyenQuyen.DataSource = null;
                return;
            }

            var wait = DialogBox.WaitingForm();
            try
            {
                if (MaSP == null)
                {
                    gcLTT.DataSource = null;
                    gcLSGD.DataSource = null;
                    gcPhieuThu.DataSource = null;
                    ctlChinhSachChung.ChinhSach_Clear();
                    gcChuyenQuyen.DataSource = null;
                    return;
                }

                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0: //Lich thanh toan
                        if (MaTT < 3)
                        {
                            gcLTT.DataSource = db.bdsSanPham_getLTT(MaSP);
                        }
                        else
                        {
                            //if (LandSoft.Library.Common.IsCDT)
                            gcLTT.DataSource = db.pgcLichThanhToan_Select(MaPGC);
                            //else//Theo phiếu thanh toán
                            //    gcLTT.DataSource = db.pgcLichThanhToan_SelectPTT(MaPGC);
                        }
                        break;
                    case 1:
                        gcLSGD.DataSource = db.bdsSanPham_getHistory(MaSP);
                        break;
                    case 2:
                        gcLSCN.DataSource = db.bdsLichSuCapNhats.Where(p => p.MaSP == MaSP)
                            .Select(p => new
                            {
                                p.ID,
                                p.NgayCN,
                                p.bdsTrangThai.TenTT,
                                p.DienTichXD,
                                p.DonGiaXD,
                                p.ThanhTienXD,
                                p.DienTichKV,
                                p.DonGiaKV,
                                p.ThanhTienKV,
                                p.ThanhTienHM,
                                p.ThanhTien,
                                p.NhanVien.HoTen
                            }).ToList();
                        break;
                    case 3:
                        gcChuyenQuyen.DataSource = db.bdsChuyenQuyens.Where(p => p.MaSP == MaSP)
                            .Select(p => new
                            {
                                p.ID,
                                p.NgayCQ,
                                p.DienGiai,
                                HoTenNVQL = p.NhanVien.HoTen,
                                HoTenNVCQ = p.NhanVien1.HoTen
                            }).ToList();
                        break;
                    case 4://Phieu thu
                        if (MaPGC != null)
                        {
                            gcPhieuThu.DataSource = gcPhieuThu.DataSource = db.pgcPhieuThu_SelectByPGC(MaPGC);
                        }
                        break;
                    case 5://Phieu chi
                        break;
                    case 6://Chinh sách
                        ctlChinhSachChung.MaKHBH = MaKHBH;
                        ctlChinhSachChung.MaDA = itemDuAn.EditValue == null ? -1 : Convert.ToInt32(itemDuAn.EditValue);
                        ctlChinhSachChung.ChinhSach_Load();
                        break;
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Clicks();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcMatBang);
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (maDA == -1)
            {
                DialogBox.Infomation("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            
            var f = new LandSoft.SanPham.frmImportProductViewGeneral();
            f.ProjectID = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
            f.ZoneID = itemKhu.EditValue != null ? (int?)itemKhu.EditValue : null;
            f.Show();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (maDA == -1)
            {
                DialogBox.Infomation("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            grvMatBang.OptionsBehavior.Editable = true;
            grvMatBang.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            grvMatBang.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            itemSave.Enabled = true;
            itemCancel.Enabled = true;
            itemEdit.Enabled = false;
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvMatBang.SetRowCellValue(1, "Col3", "[SLCMB]");
            grvMatBang.SetRowCellValue(2, "Col3", "[SLMB]");
            grvMatBang.SetRowCellValue(3, "Col3", "[SLGC]");
            grvMatBang.SetRowCellValue(4, "Col3", "[SLDC]");
            grvMatBang.SetRowCellValue(5, "Col3", "[SLVV]");
            grvMatBang.SetRowCellValue(6, "Col3", "[SLDB]");
            grvMatBang.SetRowCellValue(7, "Col3", "[SLBG]");
            grvMatBang.SetRowCellValue(8, "Col3", "[Total]");
                        
            db.SubmitChanges();

            grvMatBang.OptionsBehavior.Editable = false;
            grvMatBang.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            grvMatBang.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            itemSave.Enabled = false;
            itemCancel.Enabled = false;
            itemEdit.Enabled = true;

            this.LoadDataV2();
        }

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvMatBang.OptionsBehavior.Editable = false;
            grvMatBang.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            grvMatBang.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            itemSave.Enabled = false;
            itemCancel.Enabled = false;
            itemEdit.Enabled = true;
        }

        private void grvMatBang_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvMatBang.SetFocusedRowCellValue("ProjectID", maDA);
            grvMatBang.SetFocusedRowCellValue("ZoneID", maKhu);
        }

        private void itemDeleteMap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question("Bạn có chắc chắn muốn xóa [Dòng] này không?") == DialogResult.Yes)
            {
                try
                {
                    var obj = db.ProductViewGenerals.SingleOrDefault(p => p.ID == (int?)grvMatBang.GetFocusedRowCellValue("ID"));
                    db.ProductViewGenerals.DeleteOnSubmit(obj);
                    db.SubmitChanges();

                    grvMatBang.DeleteRow(grvMatBang.FocusedRowHandle);
                    //LoadDataV2();
                }
                catch { }
            }
        }

        private void cardView1_CustomDrawCardField(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                int mauNen = Convert.ToInt32(cardView1.GetRowCellValue(e.RowHandle, "MauNen"));
                e.Appearance.BackColor = Color.FromArgb(mauNen);
                e.Appearance.ForeColor = Color.Black;
            }
            catch { }
        }

        private void cardView1_CustomDrawCardFieldValue(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                int mauNen = Convert.ToInt32(cardView1.GetRowCellValue(e.RowHandle, "MauNen"));
                e.Appearance.BackColor = Color.FromArgb(mauNen);
                e.Appearance.ForeColor = Color.Black;
            }
            catch { }
        }
    }
}
