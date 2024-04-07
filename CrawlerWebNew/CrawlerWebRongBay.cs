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


namespace CrawlerWebRongBay
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
                    var footer = url.LinkCat.Substring(url.LinkCat.LastIndexOf('.'));
                    var LinkCat = url.LinkCat.Remove(url.LinkCat.LastIndexOf('.'));
                    var LinkCat2 = url.LinkCat.Remove(url.LinkCat.LastIndexOf('.'));
                    while (CheckStop == false && ((ident <= numPage && numPage > 0) | numPage == 0))
                    {
                        if (ident != 1)
                            LinkCat2 = LinkCat + "-trang" + ident.ToString() + footer;
                        else
                            LinkCat2 = LinkCat + footer;
                        var webGet = new HtmlWeb();
                        var document = webGet.Load(LinkCat2);
                        var getLink = document.DocumentNode.SelectNodes("//a");
                        var ContenDate = document.DocumentNode.SelectNodes("//tr");
                        if (ContenDate != null)
                        {
                            var lt = new List<HtmlNode>();
                            foreach (var tag1 in ContenDate)
                            {
                                if (tag1.Attributes["class"] != null &&
                                    (tag1.Attributes["class"].Value == "\r\n\t\t\t \t\t\t\t \t\tOdd2\r\n\t\t\t \t" ||
                                    tag1.Attributes["class"].Value == "Odd  show_hide_ad" ||
                                    tag1.Attributes["class"].Value == "Even  show_hide_ad" ||
                                    tag1.Attributes["class"].Value == "\r\n\t\t\t \t\t\t\t \t\tEven2\r\n\t\t\t \t"))
                                    lt.Add(tag1);
                            }
                            if (lt.Count == 0)
                                CheckStop = true;
                            else
                            {
                                foreach (var tag in lt)
                                {
                                    var tagchild = tag.ChildNodes[1].ChildNodes[3].InnerHtml;
                                    var tagchild1 = tag.ChildNodes[1].InnerHtml;
                                    int index1 = -1, index2 = -1;
                                    string str = "";
                                    //var index1 = tagchild.IndexOf("href=");
                                    //var index2 = tagchild.IndexOf("onmouseout=");
                                    //var index3 = tagchild1.IndexOf("href=");
                                    //var index4 = tagchild1.LastIndexOf("onmouseout=");
                                    if (tagchild.IndexOf("href=") != -1 && tagchild1.IndexOf("href=") != -1)
                                    {
                                        str = tagchild;
                                        index1 = tagchild.IndexOf("href=");
                                        index2 = tagchild.IndexOf("onmouseout=");

                                        LinkItem link = new LinkItem();
                                        link.Link = tagchild.Substring(index1 + 6, index2 - index1 - 8);
                                        var date = tag.ChildNodes[7].InnerText;//01-01-15
                                        date = date.Insert(6, "20").Replace("-", "/");
                                        link.NgayDang = Convert.ToDateTime(date);
                                        link.NhomTin = url.GroupID;
                                        link.CateID = url.ID;
                                        listLink.Add(link);
                                    }
                                    else if (tagchild1.IndexOf("href=") != -1 && tagchild1.LastIndexOf("onmouseout=") != -1)
                                    {
                                        str = tagchild1;
                                        index1 = tagchild1.IndexOf("href=");
                                        index2 = tagchild1.LastIndexOf("onmouseout=");

                                        LinkItem link = new LinkItem();
                                        link.Link = tagchild1.Substring(index1 + 6, index2 - index1 - 8);
                                        var date = tag.ChildNodes[7].InnerText;//01-01-15
                                        date = date.Insert(6, "20").Replace("-", "/");
                                        link.NgayDang = Convert.ToDateTime(date);
                                        link.NhomTin = url.GroupID;
                                        link.CateID = url.ID;
                                        listLink.Add(link);
                                    }
                                }
                            }

                            //tang bien ident de sang page khac trong mot chuyen muc
                            var ngaydang = listLink.Last().NgayDang;
                            if (SqlMethods.DateDiffDay(ngaydang, DateTo) > 0)
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

        public crlNew GetWebData_RongBay(string url, DateTime NgayDang, short GroupID, int CateID)
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
                var User = document.DocumentNode.SelectNodes("//div");
                foreach (var p in User)
                {
                    if (p.Attributes["class"] != null)
                    {
                        switch (p.Attributes["class"].Value)
                        {
                            case "user_img":
                                objNew.KhachHang = p.InnerText.Replace("\r\n", "").Trim();
                                break;
                            case "cl_333 user_phone font_14 font_700":
                                objNew.DienThoaiKH = System.Web.HttpUtility.HtmlDecode(p.InnerText).Replace("\r\n", "").Trim();// p.InnerText;
                                break;
                            case "content_input_editior":
                                objNew.NoiDung = System.Web.HttpUtility.HtmlDecode(p.InnerText.Trim());
                                MatchCollection mc = Regex.Matches(objNew.NoiDung.Replace(".", "").Replace(" ", "").Replace("-", ""), @"0\d{9,10}", RegexOptions.Singleline);
                                if (mc.Count > 0)
                                    objNew.DienThoaiCont = mc[0].ToString();
                                break;
                        }
                    }
                    //cl_333 user_phone font_14 font_700
                }


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

            try
            {
                foreach (var i in ListLink)
                {
                    var obj = new crlNew();
                    bool chk = false;
                    obj = GetWebData_RongBay(i.Link.ToString(), i.NgayDang ?? DateTime.Now, (short)i.NhomTin, (int)i.CateID);
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
    }

    public class LinkItem
    {
        public string Link { get; set; }
        public DateTime? NgayDang { get; set; }
        public short? NhomTin { get; set; }
        public int? CateID { get; set; }
    }
}
