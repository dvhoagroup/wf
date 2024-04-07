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


namespace CrawlerWebMuaBan
{
    
    public class CrawlerWeb
    {
        MasterDataContext db = new MasterDataContext();

        public void GetAllData(int WebID, DateTime DateTo, int numPage)
        {
            db.CommandTimeout = 1000 * 100000;
            var ListNewsBefore = db.crlNews.Where(p => SqlMethods.DateDiffDay(p.NgayDang, DateTo) <= 0);
            var ListLink = new List<LinkItem>();
            ListLink = GetDataCategory(WebID, DateTo, numPage);
            if (ListLink == null)
                return;
            var ListObj = new List<crlNew>();

            var objMG = db.mglNguoiMoiGiois;
            try
            {
                foreach (var i in ListLink)
                {
                    var obj = new crlNew();
                    bool chk = false;
                    obj = GetWebData_MuaBan(i.Link.ToString(), i.NgayDang ?? DateTime.Now, (short)i.NhomTin, (int)i.CateID);
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
                                LinkCat2 = LinkCat + "?cp=" + ident.ToString();
                            var webGet = new HtmlWeb();
                            var document = webGet.Load(LinkCat2);
                            var content = document.DocumentNode.SelectNodes("//a");
                            var ContenDate = document.DocumentNode.SelectNodes("//div");
                            if (ContenDate != null)
                            {
                                foreach (var tag in ContenDate)
                                {
                                    if (tag.Attributes["class"] != null && tag.Attributes["class"].Value == "mbn-content ")
                                    {
                                        LinkItem link = new LinkItem();

                                        link.Link = tag.ChildNodes[1].Attributes["href"].Value;

                                        link.NhomTin = url.GroupID;
                                        link.CateID = url.ID;
                                        foreach (var tag1 in tag.ChildNodes)
                                        {
                                            if (tag1.Attributes["class"] != null && tag1.Attributes["class"].Value == "mbn-item-991")
                                            {
                                                DateTime NgayDang = new DateTime();
                                                var t = tag1.InnerText.Replace("\r\n", "").Trim();
                                                if (t != "")
                                                    t = t.Substring(0, 10);
                                                if (DateTime.TryParse(t, out NgayDang))
                                                    link.NgayDang = NgayDang;
                                            }
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

        public crlNew GetWebData_MuaBan(string url, DateTime NgayDang, short GroupID, int CateID)
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
                    objNew.TieuDe = HttpUtility.HtmlDecode(Title[0].InnerText.Trim());
                //Get content + SP
                var Content = document.DocumentNode.SelectNodes("//div");
                var SP = document.DocumentNode.SelectNodes("//i");
                var DiaChi = document.DocumentNode.SelectNodes("//span");
                if (Content != null)
                {
                    foreach (var tag in Content)
                    {
                        //get content
                        #region getcontent
                        if (tag.Attributes["class"] != null)
                        {
                            switch (tag.Attributes["class"].Value)
                            {
                                case "ct-body clearfix":
                                    if (tag.InnerHtml != "")
                                    {
                                        var contentNew = System.Web.HttpUtility.HtmlDecode(tag.InnerHtml).Replace(@"\r\n", "").Trim().Replace("<br>", "\r\n");
                                        objNew.NoiDung = contentNew;
                                        //lấy sđt
                                        MatchCollection mc = Regex.Matches(contentNew.Replace(".", "").Replace(" ", "").Replace("-", ""), @"0\d{9,10}", RegexOptions.Singleline);
                                        switch (mc.Count)
                                        {
                                            case 0:
                                                break;
                                            case 1:
                                                objNew.DienThoaiCont = mc[0].ToString();
                                                break;
                                            case 2:
                                                objNew.DienThoaiCont = mc[0].ToString();
                                                objNew.DiDongKH = mc[1].ToString();
                                                break;
                                            default:
                                                objNew.DienThoaiCont = mc[0].ToString();
                                                objNew.DiDongKH = mc[1].ToString();
                                                objNew.DienThoaiKH = mc[2].ToString();
                                                break;
                                        }
                                    }
                                    break;
                                case "col-md-10 col-sm-10 col-xs-9 contact-name":
                                    try
                                    {
                                        if (tag.ChildNodes.Count == 1)
                                            objNew.KhachHang = System.Web.HttpUtility.HtmlDecode(tag.InnerText);
                                    }
                                    catch
                                    { }
                                    break;
                                case "cl-price-sm clearfix":
                                    objNew.DiaChi = System.Web.HttpUtility.HtmlDecode(tag.ChildNodes[3].InnerText.Trim());
                                    objNew.KhoangGia = tag.ChildNodes[1].InnerText.Replace("\r\n", "").Replace(@"&nbsp;", "").Trim();
                                    objNew.NgayDang = NgayDang;
                                    break;
                            }
                        }
                        #endregion
                    }
                }

                return objNew;
            }
            catch (Exception ex)
            {
                return null;
            }
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
