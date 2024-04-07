using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using BEE.BanDo.Library;

namespace BEE.BanDo
{
    public partial class _Default : System.Web.UI.Page
    {
        private const int RF_MESSAGE = 0xA125;
        		
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        
        private static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, IntPtr wParam, IntPtr lParam);

        protected string WidthDiv = "9000px";
        protected string ProjectName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var projectID = -1;
                var staffID = -1;
                var keycode = "";
                try
                {
                    projectID = Request["projectID"] != null ? Convert.ToInt32(Request["projectID"]) : -1;
                    staffID = Request["staffID"] != null ? Convert.ToInt32(Request["staffID"]) : -1;
                    keycode = Request["keycode"];
                }
                catch { Response.End(); }
                List<ItemData> listData = new List<ItemData>();
                using (var db = new MasterDataContext())
                {
                    var objProject = db.DuAns.SingleOrDefault(p => p.MaDA == projectID);
                    if (objProject != null)
                    {
                        ProjectName = objProject.TenDA;
                    }
                    else
                        Response.End();

                    var objStaff = db.NhanViens.SingleOrDefault(p => p.MaNV == staffID && p.KeyCode == keycode);
                    if (objStaff != null)
                    {
                        objStaff.KeyCode = "";
                        db.SubmitChanges();
                    }
                    else
                        Response.End();

                    var listProduct = db.bdsSanPhams.Where(p => p.MaDA == projectID)
                            .Select(p => new
                            {
                                p.MaSP,
                                KyHieu = p.KyHieuSALE,
                                p.MaKH,
                                p.MaPGC,
                                p.LauSo,
                                p.ViTri,
                                p.GiaBan,
                                p.DienTichCH,
                                p.TongGiaBan,
                                KhachHang = p.MaKH == null ? "" : (p.KhachHang.IsPersonal.GetValueOrDefault() ? p.KhachHang.HoKH +" "+ p.KhachHang.TenKH : p.KhachHang.TenCongTy), 
                                IsPersonal = p.MaKH != null ? p.KhachHang.IsPersonal : null,
                                p.MaTT,
                                p.bdsTrangThai.MauNen,
                                TenTT = p.bdsTrangThai.TenTTEN,
                                p.Size,
                                IsUse = p.IsUse.GetValueOrDefault(),
                                p.TenLMK,
                                p.PhongNgu,
                                p.PaymentTerm,
                                p.Discount,
                                p.DiscountMoney,
                                p.CodeColor,
                                p.Khu.TenKhu
                            }).ToList();

                    int maxFloor = listProduct.Max(p => p.LauSo) ?? 0;
                    int maxItemOfFloor = listProduct.Max(p => p.ViTri) ?? 0;

                    string content = "";
                    for (int i = maxFloor; i > 0; i--)
                    {
                        bool floorNotUse = false;
                        string floorRight = "";
                        if (listProduct.Where(p => p.IsUse & p.LauSo == i && p.TenKhu == "B").Count() > 0)
                            content += floorRight = string.Format("<div class=\"floor\">{0}F</div>", i);
                        else
                        {
                            content += floorRight = string.Format("<div class=\"floor-not-use\">{0}F</div>", i);
                            floorNotUse = true;
                        }

                        #region Tower B
                        for (int j = maxItemOfFloor; j >= 1; j--)
                        {
                            try
                            {
                                var obj = listProduct.Where(p => p.LauSo == i && p.ViTri == j && p.TenKhu == "B").FirstOrDefault();
                                if (obj != null)
                                {
                                    if (obj.IsUse)
                                    {
                                        content += string.Format("<div class=\"itemBox{0}\"><div class=\"caption\"><div class=\"no\">{1}</div>", GetSize(obj.Size ?? 1), obj.KyHieu);
                                        content += string.Format("<div class=\"type\">{0}</div></div>", obj.TenLMK);
                                        content += string.Format("<div class=\"area-br\"><div class=\"area\">{0:#,0.##}m2</div>", obj.DienTichCH);
                                        content += string.Format("<div class=\"br\">{0}BR</div></div>", obj.PhongNgu);
                                        content += string.Format("<div class=\"price\">{0:#,0.##}</div>", obj.TongGiaBan);
                                        content += string.Format("<div class=\"discount-year\"><div class=\"discount{0}\">{1:#,0.##}</div>", (obj.Discount ?? 0) != 0 ? "-y" : "", (obj.Discount ?? 0) != 0 ? string.Format("▲ {0:#,0.##}%", Math.Abs(obj.Discount ?? 0)) : "Original");
                                        content += string.Format("<div class=\"year\">{0}</div></div>", obj.PaymentTerm);
                                        string status = GetStatus(obj.MaTT ?? 1);
                                        content += string.Format("<div class=\"discountprice{0}\">{1:#,0.##}</div>", status, obj.TongGiaBan + obj.DiscountMoney);
                                        content += string.Format("<div class=\"status{0}\">{1}</div>", status, obj.TenTT);
                                        content += string.Format("<div class=\"customer{0}\">{1}</div>", status, obj.KhachHang);
                                        content += string.Format("<div class=\"cate-customer{0}\">{1}</div>", status, GetTypeCustomer(obj.IsPersonal));
                                        if (obj.MaTT == 2)
                                        {
                                            content += "<div class=\"booking\">";
                                            content += string.Format("<a href=\"javascript:void();\" onclick=\"checkBooking('{0}:2')\">Đặt cọc</a> | ", obj.MaSP);
                                            content += string.Format("<a href=\"javascript:void();\" onclick=\"checkBooking('{0}:4')\">Hợp đồng</a></div>", obj.MaSP);
                                        }
                                        content += "</div>";
                                    }
                                    else
                                    {
                                        if (floorNotUse)
                                            content += string.Format("<div class=\"itemBox-not-use{0}\" {1}>", GetSize(obj.Size ?? 1), GetColor(obj.CodeColor));
                                        else
                                            content += string.Format("<div class=\"itemBox{0}\" {1}>", GetSize(obj.Size ?? 1), GetColor(obj.CodeColor));
                                        content += string.Format("<div class=\"not-use\">{0}</div></div>", obj.KyHieu);
                                    }
                                }
                            }
                            catch { }
                        }
                        #endregion

                        content += "<div class=\"tower-separator\"></div>";
                        #region Tower A
                        if (listProduct.Where(p => p.IsUse & p.LauSo == i && p.TenKhu == "A").Count() <= 0)
                            floorNotUse = true;

                        for (int j = maxItemOfFloor; j >= 1; j--)
                        {
                            try
                            {
                                var obj = listProduct.Where(p => p.LauSo == i && p.ViTri == j && p.TenKhu == "A").FirstOrDefault();
                                if (obj != null)
                                {
                                    if (obj.IsUse)
                                    {
                                        content += string.Format("<div class=\"itemBox{0}\"><div class=\"caption\"><div class=\"no\">{1}</div>", GetSize(obj.Size ?? 1), obj.KyHieu);
                                        content += string.Format("<div class=\"type\">{0}</div></div>", obj.TenLMK);
                                        content += string.Format("<div class=\"area-br\"><div class=\"area\">{0:#,0.##}m2</div>", obj.DienTichCH);
                                        content += string.Format("<div class=\"br\">{0}BR</div></div>", obj.PhongNgu);
                                        content += string.Format("<div class=\"price\">{0:#,0.##}</div>", obj.TongGiaBan);
                                        content += string.Format("<div class=\"discount-year\"><div class=\"discount{0}\">{1:#,0.##}</div>", (obj.Discount ?? 0) != 0 ? "-y" : "", (obj.Discount ?? 0) != 0 ? string.Format("▲ {0:#,0.##}%", Math.Abs(obj.Discount ?? 0)) : "Original");
                                        content += string.Format("<div class=\"year\">{0}</div></div>", obj.PaymentTerm);
                                        string status = GetStatus(obj.MaTT ?? 1);
                                        content += string.Format("<div class=\"discountprice{0}\">{1:#,0.##}</div>", status, obj.TongGiaBan + obj.DiscountMoney);
                                        content += string.Format("<div class=\"status{0}\">{1}</div>", status, obj.TenTT);
                                        content += string.Format("<div class=\"customer{0}\">{1}</div>", status, obj.KhachHang);
                                        content += string.Format("<div class=\"cate-customer{0}\">{1}</div>", status, GetTypeCustomer(obj.IsPersonal));
                                        if (obj.MaTT == 2)
                                        {
                                            content += "<div class=\"booking\">";
                                            content += string.Format("<a href=\"javascript:void();\" onclick=\"checkBooking('{0}:2')\">Đặt cọc</a> | ", obj.MaSP);
                                            content += string.Format("<a href=\"javascript:void();\" onclick=\"checkBooking('{0}:4')\">Hợp đồng</a></div>", obj.MaSP);
                                        }
                                        content += "</div>";
                                    }
                                    else
                                    {
                                        if (floorNotUse)
                                            content += string.Format("<div class=\"itemBox-not-use{0}\" {1}>", GetSize(obj.Size ?? 1), GetColor(obj.CodeColor));
                                        else
                                            content += string.Format("<div class=\"itemBox{0}\" {1}>", GetSize(obj.Size ?? 1), GetColor(obj.CodeColor));
                                        content += string.Format("<div class=\"not-use\">{0}</div></div>", obj.KyHieu);
                                    }
                                }
                            }
                            catch { }
                        }
                        #endregion

                        content += floorRight;

                        content += "<div style=\"clear: both\"></div>";
                    }

                    divContent.InnerHtml = content;
                    WidthDiv = string.Format("{0}px", maxItemOfFloor * 140);
                }
            }
        }

        protected string SetColor(int color)
        {
            string Col = "";

            try
            {
                Col = ColorTranslator.ToHtml(Color.FromArgb(color));
            }
            catch { }

            return Col;
        }

        private string GetSize(decimal val)
        {
            if (val == 0 | val == 1)
                return "";
            else
            {
                if (val > 1 & val < 2) return "15";
                else
                {
                    if (val == 2) return "2";
                    else
                    {
                        if (val == 3) return "3";
                        else if (val == 4) return "4";
                    }
                }
            }

            return "";
        }

        private string GetStatus(byte val)
        {
            switch (val)
            {
                case 2:
                    return "-sale";
                case 3:
                case 4:
                    return "-da";
                case 5:
                case 6:
                    return "-spa";
                default:
                    return "";
            }
        }

        private string GetTypeCustomer(bool? val)
        {
            if (val == null) return "";
            else
            {
                if (val == true) return "Individual";
                else return "Entity";
            }
        }

        private string GetColor(string val)
        {
            if (val == null | val == "")
                return "";
            else
                return string.Format("style=\"background: #{0}\"", val);
        }

        protected void callBooking_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            //e.Parameter = ProductID +":"+ ActionID
            var paras = e.Parameter.Split(':');

            //get process BEEREMA
            Process[] processes = Process.GetProcessesByName("BEEREMA");

            //ProductID
            IntPtr wParam = new IntPtr(Convert.ToInt32(paras[0]));

            //ActionID
            IntPtr lParam = new IntPtr(Convert.ToInt32(paras[1]));

            if (processes.Length > 0)
                SendMessage(processes[0].MainWindowHandle, RF_MESSAGE, wParam, lParam);
            else
                Response.Write("No other running applications found.");
        }
    }

    class ItemData
    {
        public int MaSP { get; set; }
        public int MaDA { get; set; }
        public int MaKhu { get; set; }
        public int MauNen { get; set; }
        public string KyHieu { get; set; }
        public string TenTT { get; set; }
        public int LauSo { get; set; }
        public byte MaTT { get; set; }
        public decimal DienTichCH { get; set; }
        public decimal GiaBan { get; set; }
        public decimal ThanhTien { get; set; }
        public string KhachHang { get; set; }
        public int YearPay { get; set; }
    }
}