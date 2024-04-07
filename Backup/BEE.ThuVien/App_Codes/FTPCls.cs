using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace it
{
    public class FTPCls
    {
        public string FTPServerIP;
        public string FTPUserID;
        public string FTPPassword;

        public FTPCls()
        {
        }

        public void SetAccountFTP(string QueryString)
        {
            SqlConnection sqlCon = new SqlConnection(it.CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand(QueryString, sqlCon);
            sqlCmd.CommandType = CommandType.Text;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void GetAccountFTP()
        {
            SqlConnection sqlCon = new SqlConnection(it.CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("GetAccountFTP", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@ServerIP", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();                            
            sqlCon.Close();

            FTPServerIP = sqlCmd.Parameters["@ServerIP"].Value.ToString();
            FTPUserID = sqlCmd.Parameters["@UserName"].Value.ToString();
            FTPPassword = sqlCmd.Parameters["@Password"].Value.ToString();
        }

        public string Upload(string filename, string directory)
        {
            CreateDirectory("ftp://" + FTPServerIP + string.Format("/{0}/", directory));
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + FTPServerIP + string.Format("/{0}/{1}", directory, it.CommonCls.getKey() + filename.Substring(filename.LastIndexOf(".")));
            FtpWebRequest ftp;
            
            ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            ftp.Credentials = new NetworkCredential(FTPUserID, FTPPassword);
            ftp.KeepAlive = false;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;
            ftp.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = ftp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);

                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                strm.Close();
                fs.Close();
                return uri;//.Replace("ftp://", "http://").Replace("/httpdocs/", "/");
            }
            catch
            {
                return "";
            }
        }

        public string Upload2(string filename, string directory)
        {
            CreateDirectory("ftp://" + FTPServerIP + string.Format("/{0}/", directory));
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + FTPServerIP + string.Format("/{0}/{1}", directory, it.CommonCls.getKey() + filename.Substring(filename.LastIndexOf(".")));
            FtpWebRequest ftp;

            ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            ftp.Credentials = new NetworkCredential(FTPUserID, FTPPassword);
            ftp.KeepAlive = false;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;
            ftp.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = ftp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);

                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                strm.Close();
                fs.Close();
                return uri.Replace("ftp://", "http://").Replace("/httpdocs/", "/");
            }
            catch
            {
                return "";
            }
        }

        public bool Delete(string fileName)
        {
            try
            {
                string uri = fileName;//.Replace("http://", "ftp://").Replace("/upload/", "/httpdocs/upload/");
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                ftp.Credentials = new NetworkCredential(FTPUserID, FTPPassword);
                ftp.KeepAlive = false;
                ftp.Method = WebRequestMethods.Ftp.DeleteFile;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();

                long size = response.ContentLength;

                Stream datastream = response.GetResponseStream();
                StreamReader sre = new StreamReader(datastream);

                result = sre.ReadToEnd();
                sre.Close();
                datastream.Close();
                response.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete2(string fileName)
        {
            try
            {
                string uri = fileName.Replace("http://", "ftp://").Replace("/upload/", "/httpdocs/upload/");
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                ftp.Credentials = new NetworkCredential(FTPUserID, FTPPassword);
                ftp.KeepAlive = false;
                ftp.Method = WebRequestMethods.Ftp.DeleteFile;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();

                long size = response.ContentLength;

                Stream datastream = response.GetResponseStream();
                StreamReader sre = new StreamReader(datastream);

                result = sre.ReadToEnd();
                sre.Close();
                datastream.Close();
                response.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool CreateDirectory(string directory)
        {
            try
            {
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(directory);
                ftp.Credentials = new NetworkCredential(FTPUserID, FTPPassword);
                ftp.UsePassive = true;
                ftp.UseBinary = true;
                ftp.KeepAlive = false;
                ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse(); 
                Stream ftpStream = response.GetResponseStream(); 
                ftpStream.Close(); 
                response.Close();

                return true;
            }
            catch (WebException ex) 
            { 
                FtpWebResponse response = (FtpWebResponse)ex.Response; 
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable) 
                { 
                    response.Close(); 
                    return true; 
                } 
                else 
                {
                    response.Close(); 
                    return false; 
                } 
            }
        }

        public void Download(string filePath, string fileName)
        {
            FtpWebRequest ftp;
            try
            {
                //Nhập đường dẩn đầy đủ của tập tin
                string uri = fileName;//.Replace("http://", "ftp://").Replace("/upload/", "/httpdocs/upload/");
                FileStream outputStream = new FileStream(filePath + "\\" + fileName.Substring(fileName.LastIndexOf("/") + 1), FileMode.Create);
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                ftp.Method = WebRequestMethods.Ftp.DownloadFile;
                ftp.UseBinary = true;
                ftp.Credentials = new NetworkCredential(FTPUserID, FTPPassword);
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch{                
            }
        }
    }
}
