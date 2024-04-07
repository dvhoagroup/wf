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


namespace CrawlerWebNew
{
    public class CrawlerWeb
    {
        MasterDataContext db = new MasterDataContext();
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
                    obj = GetWebData_BDS(i.Link.ToString(), i.NgayDang ?? DateTime.Now, (short)i.NhomTin, (int)i.CateID);
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
                    // ListObj.RemoveAt(ltCheck);

                }

            }
            catch
            { }

        }
        //public List<LinkItem> GetDataCategory(int WebID, DateTime DateTo, int numPage)
        //{
        //    using (MasterDataContext db = new MasterDataContext())
        //    {
        //        //lay het link trong mot chuyen muc
        //        List<LinkItem> listLink = new List<LinkItem>();
        //        var listCat = db.crlCategories.Where(p => p.WebID == WebID && p.IsGetData.GetValueOrDefault() == true).ToList();
        //        if (listCat.Count() <= 0)
        //            return null;
        //        bool CheckStop = false;
        //        int ident = 1;
        //        foreach (var url in listCat)
        //        {

        //            var LinkCat = url.LinkCat;
        //            var LinkCat2 = url.LinkCat;
        //            while (CheckStop == false && ((ident <= numPage && numPage > 0) | numPage == 0))
        //            {
        //                if (ident != 1)
        //                    LinkCat2 = LinkCat + "/p" + ident.ToString();
        //                var webGet = new HtmlWeb();
        //                var document = webGet.Load(LinkCat2);
        //                var content = document.DocumentNode.SelectNodes("//div");
        //                if (content != null)
        //                {
        //                    foreach (var tag in content)
        //                    {
        //                        if (tag.Attributes["class"] != null && (tag.Attributes["class"].Value == "vip1 search-productItem" || tag.Attributes["class"].Value == "vip3 search-productItem" || tag.Attributes["class"].Value == "vip0 search-productItem" || tag.Attributes["class"].Value == "vip5"))
        //                        {
        //                            LinkItem link = new LinkItem();
        //                            //foreach (var div in tag.ChildNodes)
        //                            //{
        //                            var mc = Regex.Match(tag.InnerHtml, "<a href='/.+?title=", RegexOptions.Singleline).ToString();
        //                            if (mc != "")
        //                                link.Link = (string.Format("http://batdongsan.com.vn{0}", mc.Replace("<a href='", "").Replace("' title=", "")));

        //                            var mc2 = Regex.Match(tag.InnerHtml, "<div class='floatright mar-right-10 bot-right-abs'.+?</div>", RegexOptions.Singleline).ToString();
        //                            var mc3 = Regex.Match(tag.InnerHtml, "<div class='floatright mar-right-10'.+?</div>", RegexOptions.Singleline).ToString();
        //                            DateTime NgayDang = new DateTime();
        //                            NgayDang = Convert.ToDateTime(Regex.Replace(mc2 == "" ? mc3 : mc2, "<div.+?>", "", RegexOptions.Singleline).Trim().Remove(10));
        //                            link.NgayDang = NgayDang;
        //                            link.NhomTin = url.GroupID;
        //                            link.CateID = url.ID;
        //                            listLink.Add(link);
        //                        }
        //                    }

        //                    //tang bien ident de sang page khac trong mot chuyen muc
        //                    var ngaydang = listLink.Last().NgayDang;
        //                    if (SqlMethods.DateDiffDay(ngaydang, DateTo) >= 0)
        //                        CheckStop = true;
        //                    else
        //                        ident++;
        //                }
        //                else CheckStop = true;
        //            }
        //            CheckStop = false;
        //            ident = 1;
        //        }
        //        return listLink;
        //    }
        //}

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
                            LinkCat2 = LinkCat + "/p" + ident.ToString();
                        var webGet = new HtmlWeb();
                        var document = webGet.Load(LinkCat2);
                        var content = document.DocumentNode.SelectNodes("//div");
                        if (content != null)
                        {
                            foreach (var tag in content)
                            {
                                if (tag.Attributes["class"] != null && (tag.Attributes["class"].Value == "vip1 search-productItem" || tag.Attributes["class"].Value == "vip3 search-productItem" || tag.Attributes["class"].Value == "vip0 search-productItem" || tag.Attributes["class"].Value == "vip5"))
                                {
                                    LinkItem link = new LinkItem();
                                    //foreach (var div in tag.ChildNodes)
                                    //{
                                    var mc = Regex.Match(tag.InnerHtml, "<a href='/.+?title=", RegexOptions.Singleline).ToString();
                                    if (mc != "")
                                        link.Link = (string.Format("http://batdongsan.com.vn{0}", mc.Replace("<a href='", "").Replace("' title=", "")));

                                    var mc2 = Regex.Match(tag.InnerHtml, "<div class='floatright mar-right-10 bot-right-abs'.+?</div>", RegexOptions.Singleline).ToString();
                                    var mc3 = Regex.Match(tag.InnerHtml, "<div class='floatright mar-right-10'.+?</div>", RegexOptions.Singleline).ToString();
                                    DateTime NgayDang = new DateTime();
                                    NgayDang = Convert.ToDateTime(Regex.Replace(mc2 == "" ? mc3 : mc2, "<div.+?>", "", RegexOptions.Singleline).Trim().Remove(10));
                                    link.NgayDang = NgayDang;
                                    link.NhomTin = url.GroupID;
                                    link.CateID = url.ID;
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

        public crlNew GetWebData_BDS(string url, DateTime NgayDang, short GroupID, int CateID)
        {
            //khai baos object chua tin(new)
            var objNew = new crlNew();
            objNew.NgayDang = NgayDang;
            objNew.GroupID = GroupID;
            objNew.CategoryID = CateID;
            //
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            objNew.Website = url;
            objNew.MaTT = 1;
            //Get Title
            var Title = document.DocumentNode.SelectNodes("//h1");
            if (Title != null)
                objNew.TieuDe = Title[0].InnerText.Trim();
            //Get content + SP
            var Content = document.DocumentNode.SelectNodes("//div");
            if (Content != null)
            {
                try
                {
                    foreach (var tag in Content)
                    {
                        //get content
                        #region getcontent
                        if (tag.Attributes["class"] != null && tag.Attributes["cid"] != null)
                        {
                            if (tag.Attributes["class"].Value == "pm-content stat")
                            {
                                var contentNew = tag.InnerText.Trim();
                                MatchCollection mc = Regex.Matches(contentNew, "<.+?>", RegexOptions.Singleline);
                                contentNew = contentNew.Replace("<br>", "\r\n");
                                //  Match match = Regex.Match(contentNew, @"<.+?>", RegexOptions.IgnoreCase);

                                foreach (var m in mc)
                                {
                                    contentNew = contentNew.Replace(m.ToString(), "");
                                }
                                Match mcdt = Regex.Match(contentNew.Replace(".", "").Replace(" ", "").Replace("-", ""), @"0\d{9,10}", RegexOptions.Singleline);
                                if (mcdt.Value != "")
                                    objNew.DienThoaiCont = mcdt.ToString();
                                objNew.NoiDung = contentNew;
                            }
                        }
                        #endregion
                        // get product info
                        #region getProducinfo
                        if (tag.Attributes["class"] != null)
                            if (tag.Attributes["class"].Value == "kqchitiet")
                            {
                                foreach (var span in tag.ChildNodes)
                                {
                                    // Lay dia chi sp
                                    if (span.Attributes["class"] != null && span.Attributes["class"].Value == "diadiem-title mar-right-15")
                                    {
                                        var address = span.InnerText.Trim();
                                        address = address.Replace("<br>", "\r\n");
                                        // lay danh sach cac the 
                                        MatchCollection mc = Regex.Matches(address, "<.+?>", RegexOptions.Singleline);
                                        foreach (var m in mc)
                                        {
                                            address = address.Replace(m.ToString(), "");
                                        }
                                        objNew.DiaChi = address;
                                    } // lay gia + dien tich
                                    if (span.Attributes["style"] != null && span.Attributes["style"].Value == "display: inline-block;")
                                    {
                                        var gia = span.ChildNodes[0].InnerText.Replace("\r\n", "").Trim().Substring(4).TrimStart();
                                        objNew.KhoangGia = gia.Remove(gia.IndexOf("&"), 6);
                                        var DienTich = span.ChildNodes[2].InnerText.Replace("\r\n", "").Trim().Substring(10).TrimStart();
                                        objNew.DienTich = DienTich;
                                    }


                                }
                            }
                        #endregion
                        // get contact
                        #region Getcontact
                        if (tag.Attributes["id"] != null)
                        {
                            switch (tag.Attributes["id"].Value)
                            {
                                case "LeftMainContent__productDetail_contactName":
                                case "LeftMainContent__detail_contactName":
                                    objNew.KhachHang = Regex.Split(tag.InnerText, "\r\n")[5].Trim();
                                    break;
                                case "LeftMainContent__productDetail_contactAddress":
                                    objNew.DiaChiKH = Regex.Split(tag.InnerText, "\r\n")[5].Trim();
                                    break;
                                case "LeftMainContent__productDetail_contactPhone":
                                case "LeftMainContent__detail_contactPhone":
                                    objNew.DienThoaiKH = Regex.Split(tag.InnerText, "\r\n")[5].Trim();
                                    break;
                                case "LeftMainContent__productDetail_contactMobile":
                                case "LeftMainContent__detail_contactMobile":
                                    objNew.DiDongKH = Regex.Split(tag.InnerText, "\r\n")[5].Trim();
                                    break;
                                //case "LeftMainContent__productDetail_contactEmail":
                                //  = tag.InnerText;
                                //    break;
                            }

                        }
                        #endregion
                        //Get ngay dang
                        #region GetNgayDang
                        if (tag.Attributes["style"] != null && tag.Attributes["style"].Value == "background: #ededed; padding-left: 10px;")
                        {
                            var t = tag.InnerText.Replace("\r\n", "").Trim();
                            var s = t.Substring(t.Length - 10);
                            DateTime time = new DateTime();
                            if (DateTime.TryParse(s, out time))
                            {
                                objNew.NgayDang = time;
                            }
                        }
                        #endregion
                    }
                }
                catch { }
            }

            return objNew;
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
