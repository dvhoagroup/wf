using System;
using System.Linq;
using System.Net;
using System.IO;
using BEE.ThuVien;

namespace BEE.FTP
{
    public class FtpClient
    {
        private string ftpAddress, ftpUser, ftpPass, webUrl;
        private FtpWebRequest request;
        public string Url { get; set; }

        public FtpClient()
        {
            using (var db = new MasterDataContext())
            {
                var objConfig = db.tblConfigs.First();
                ftpAddress = objConfig.FtpUrl;
                ftpUser = objConfig.FtpUser;
                ftpPass = it.CommonCls.GiaiMa(objConfig.FtpPass);
                webUrl = objConfig.WebUrl;
            }
        }

        public string WebUrl
        {
            get { return webUrl; }
            set { this.Url = value.Replace(webUrl, "").Trim('/'); }
        }

        public long GetFileSize()
        {
            var response = getResponse(WebRequestMethods.Ftp.GetFileSize);
            return response.ContentLength;
        }

        public FtpClient(string fileName)
        {
            string ftpAddress, ftpUser, ftpPass;
            using (var db = new MasterDataContext())
            {
                var objConfig = db.tblConfigs.First();
                ftpAddress = objConfig.FtpUrl;
                ftpUser = objConfig.FtpUser;
                ftpPass = it.CommonCls.GiaiMa(objConfig.FtpPass);
                WebUrl = objConfig.WebUrl.TrimEnd('/') + "/" + fileName;
            }

            request = FtpWebRequest.Create(ftpAddress.TrimEnd('/') + "/" + fileName) as FtpWebRequest;
            request.Credentials = new NetworkCredential(ftpUser, ftpPass);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
        }

        public long getFileSize()
        {
            var req = FtpWebRequest.Create(request.RequestUri) as FtpWebRequest;
            req.Credentials = request.Credentials;
            req.UsePassive = true;
            req.UseBinary = true;
            req.KeepAlive = true;
            req.Method = WebRequestMethods.Ftp.GetFileSize;
            var response = req.GetResponse() as FtpWebResponse;
            return response.ContentLength;
        }

        public Stream Download()
        {
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            var response = request.GetResponse() as FtpWebResponse;
            return response.GetResponseStream();
        }

        public Stream Upload(long length)
        {
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.ContentLength = length;
            return request.GetRequestStream();
        }

        public void DeleteFile()
        {
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            var response = request.GetResponse() as FtpWebResponse;
            response.GetResponseStream();
            response.Close();
            response = null;
        }

        public void CreateFolder()
        {
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            var response = request.GetResponse() as FtpWebResponse;
            response.Close();
            response = null;
        }

        public Stream DownloadFile()
        {
            var response = getResponse(WebRequestMethods.Ftp.DownloadFile);
            return response.GetResponseStream();
        }

        public Stream UploadFile(long length)
        {
            var request = getRequest(WebRequestMethods.Ftp.UploadFile);
            request.ContentLength = length;
            return request.GetRequestStream();
        }

        private FtpWebRequest getRequest(string method)
        {
            var request = FtpWebRequest.Create(ftpAddress.TrimEnd('/') + "/" + this.Url.Trim('/')) as FtpWebRequest;
            request.Credentials = new NetworkCredential(ftpUser, ftpPass);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Method = method;
            return request;
        }

        private FtpWebResponse getResponse(string method)
        {
            var request = getRequest(method);
            return request.GetResponse() as FtpWebResponse;
        }

        public void MakeDirectory()
        {
            var folders = this.Url.Split('/');
            var temp = this.Url;
            this.Url = "";
            foreach (var name in folders)
            {
                try
                {
                    this.Url += "/" + name;
                    //  WebRequest request = WebRequest.Create("ftp://Quanlykinhdoanh.com.vn/httpdocs/Upload/doc/directory123");
                    var response = getResponse(WebRequestMethods.Ftp.MakeDirectory);
                    response.Close();
                }
                catch { }
            }
            this.Url = temp;
        }

        public void RemoveDirectory()
        {
            var response = getResponse(WebRequestMethods.Ftp.RemoveDirectory);
            response.Close();
        }
    }
}
