using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Xml.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.IO;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;
using System.Web;


namespace CrawlerWebNhaDat24
{
    public class CrawlerWeb
    {
        MasterDataContext db = new MasterDataContext();

        public List<LinkItem> GetDataCategory(int WebID, DateTime DateTo, int numPage)
        {
            //lay het link trong mot chuyen muc
            List<LinkItem> listLink = new List<LinkItem>();
            var listCat = db.crlCategories.Where(p => p.WebID == WebID && p.IsGetData.GetValueOrDefault() == true).ToList();
            if (listCat.Count() <= 0)
                return null;
            bool CheckStop = false;
            int ident = 1;
            foreach (var url in listCat)
            {
                try
                {
                    var LinkCat = url.LinkCat;
                    var LinkCat2 = url.LinkCat;
                    while (CheckStop == false && ((ident <= numPage && numPage > 0) | numPage == 0))
                    {
                        if (ident != 1)
                            LinkCat2 = LinkCat + ident.ToString();
                        var webGet = new HtmlWeb();
                        var document = webGet.Load(LinkCat2);
                        var content = document.DocumentNode.SelectNodes("//a");
                        var ContenDate = document.DocumentNode.SelectNodes("//div");
                        if (ContenDate != null)
                        {
                            foreach (var tag in ContenDate)
                            {
                                if (tag.Attributes["class"] != null && tag.Attributes["class"].Value == "dv-txt")
                                {
                                    LinkItem link = new LinkItem();

                                    link.Link = "http://nhadat24h.net" + tag.ChildNodes[1].ChildNodes[0].Attributes["href"].Value;//.Attributes["href"].Value;

                                    link.NhomTin = url.GroupID;
                                    link.CateID = url.ID;
                                    var xt = tag.ChildNodes[3].InnerText.IndexOf("lúc");
                                    if (tag.ChildNodes[3].InnerText.IndexOf("lúc") == -1)
                                        link.NgayDang = DateTime.Now;
                                    else
                                    {
                                        var date = tag.ChildNodes[3].InnerText.Replace("\r\n", "").Trim().Substring(0, 10);
                                        link.NgayDang = Convert.ToDateTime(date);
                                    }
                                    listLink.Add(link);
                                }

                            }

                            //tang bien ident de sang page khac trong mot chuyen muc
                            var ngaydang = listLink.Last().NgayDang;
                            if (SqlMethods.DateDiffDay(ngaydang, DateTo) >= 0)
                                CheckStop = true;
                            else
                                ident++;
                        }
                        else CheckStop = true;
                    }
                    CheckStop = false;
                    ident = 1;
                }
                catch { }
            }
            return listLink;
        }

        public crlNew GetWebData_NhaDat24(string url, DateTime NgayDang, short GroupID, int CateID)
        {
            //khai baos object chua tin(new)
            try
            {
                var objNew = new crlNew();
                objNew.NgayDang = NgayDang;
                objNew.GroupID = GroupID;
                objNew.CategoryID = CateID;
                objNew.Website = url;
                objNew.MaTT = 1;
                //
                var webGet = new HtmlWeb();
                var document = webGet.Load(url);
                //Get Title
                var Title = document.DocumentNode.SelectNodes("//h1");
                if (Title != null)
                    objNew.TieuDe = Title[0].InnerText.Trim();// HttpUtility.HtmlDecode(Title[0].InnerText.Trim());
                //Get content + SP
                var Content = document.DocumentNode.SelectNodes("//div");
                var KhachHang = document.DocumentNode.SelectNodes("//label");
                var DiaChi = document.DocumentNode.SelectNodes("//span");
                try
                {
                    if (Content != null)
                    {
                        foreach (var tag in Content)
                        {
                            //get content ContentPlaceHolder2_lbGiaTien
                            #region getcontent
                            if (tag.Attributes["id"] != null)
                            {
                                switch (tag.Attributes["id"].Value)
                                {
                                    case "ContentPlaceHolder2_divContent":
                                        objNew.NoiDung = tag.InnerText;
                                        MatchCollection mc = Regex.Matches(tag.InnerText.Replace(".", "").Replace(" ", "").Replace("-", ""), @"0\d{9,10}", RegexOptions.Singleline);
                                        if (mc.Count > 0)
                                            objNew.DienThoaiCont = mc[0].ToString(); ;
                                        break;
                                    case "ContentPlaceHolder2_lbGiaTien":
                                        objNew.KhoangGia = tag.InnerText.Trim();
                                        break;
                                }

                            }
                            #endregion
                        }
                    }
                    if (KhachHang != null)
                    {
                        foreach (var tag in KhachHang)
                        {
                            if (tag.Attributes["id"] != null)
                            {
                                switch (tag.Attributes["id"].Value)
                                {
                                    case "ContentPlaceHolder2_viewInfo1_lbHoTen": //khách hàng
                                        objNew.KhachHang = tag.InnerText;
                                        break;
                                    case "ContentPlaceHolder2_viewInfo1_lbPhone": // điện thoại
                                        var dt = tag.InnerText.Substring(12);
                                        if (dt.IndexOf('-') == -1)//có 1 sđt
                                        {
                                            objNew.DiDongKH = dt;
                                        }
                                        else
                                        {
                                            var lt = dt.Split('-');
                                            objNew.DiDongKH = lt[0].Trim();
                                            objNew.DienThoaiKH = lt[1].Trim();
                                        }
                                        break;
                                    case "ContentPlaceHolder2_viewInfo1_lbDiaChi":
                                        objNew.DiaChi = tag.InnerText.Trim();
                                        break;

                                }

                            }
                        }
                    }
                }
                catch { }

                return objNew;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void GetAllData(int WebID, DateTime DateTo, int numPage)
        {
            db.CommandTimeout = 2000000000;
            var ListNewsBefore = db.crlNews.Where(p => SqlMethods.DateDiffDay(p.NgayDang, DateTo) <= 0);
            var ListLink = new List<LinkItem>();
            ListLink = GetDataCategory(WebID, DateTo, numPage);
            if (ListLink == null)
                return;
            var ListObj = new List<crlNew>();

            var objMG = db.mglNguoiMoiGiois;
            foreach (var i in ListLink)
            {
                try
                {
                    var obj = new crlNew();
                    bool chk = false;
                    obj = GetWebData_NhaDat24(i.Link.ToString(), i.NgayDang ?? DateTime.Now, (short)i.NhomTin, (int)i.CateID);
                    foreach (var t in objMG)
                    {
                        if (obj.DiDongKH == t.SDT | obj.DienThoaiKH == t.SDT | obj.DienThoaiCont == t.SDT)
                        {
                            chk = true;
                            break;
                        }
                        else
                            chk = false;
                    }
                    if (!chk)
                    {
                        if (ListNewsBefore.Select(p => p.TieuDe).Contains(obj.TieuDe) == false)
                            ListObj.Add(obj);
                    }
                }
                catch { }
            }
            try
            {

                List<crlNew> ltCheck = new List<crlNew>();
                if (ListObj.Count > 0)
                {
                    int count = 0;
                    var listck = ListObj.OrderBy(p => p.DiDongKH).ToList();
                    for (int i = 1; i < listck.Count; i++)
                    {
                        if (!(listck[count].TieuDe == listck[i].TieuDe && listck[count].DiDongKH == listck[i].DiDongKH))
                        {
                            using (var db1 = new MasterDataContext())
                            {

                                db1.crlNews.InsertOnSubmit(listck[count]);
                                db1.SubmitChanges();
                            }
                            count = i;
                        }
                    }

                }
            }
            catch
            { }
        }
    }

    public class LinkItem
    {
        public string Link { get; set; }
        public DateTime? NgayDang { get; set; }
        public short? NhomTin { get; set; }
        public int? CateID { get; set; }
    }
}
