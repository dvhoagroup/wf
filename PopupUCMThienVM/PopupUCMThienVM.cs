// Decompiled with JetBrains decompiler
// Type: PopupUCMThienVM.PopupUCMThienVM
// Assembly: PopupUCMThienVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2737CBA4-BC99-4B40-889E-0BED30659740
// Assembly location: C:\Users\Admin\Desktop\VOIP-ZYCCO\Tooltest_UCM\PopupUCMThienVM.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace PopupUCMThienVM
{
    public class PopupUCMThienVM
    {
        public EventArgs e = (EventArgs)null;
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        public string host;
        public int port;
        public string username;
        public string password;
        private string key_input;
        private TcpClient server;
        private string strconnect_download;

        public event PopupUCMThienVM.TickHandler Tick;

        public PopupUCMThienVM(string _host, int _port, string _user, string _pass)
        {
            this.host = _host;
            this.port = _port;
            this.username = _user;
            this.password = _pass;
        }

        public PopupUCMThienVM()
        {
        }

        public bool xacthuc(string _key)
        {
            this.key_input = _key;
            return this.key_input == this.taokey();
        }

        public void Start()
        {
            new Thread(new ThreadStart(this.listener))
            {
                ApartmentState = ApartmentState.MTA,
                IsBackground = true
            }.Start();
        }

        private void listener()
        {
            if (this.key_input == "" || !this.xacthuc(this.key_input))
                return;
            byte[] numArray = new byte[1024];
            string input1 = "";
            while (true)
            {
                bool flag = true;
                try
                {
                    this.server = new TcpClient(this.host, this.port);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Unable to connect to server");
                    continue;
                }
                NetworkStream stream = this.server.GetStream();
                int count1 = stream.Read(numArray, 0, numArray.Length);
                Console.WriteLine(Encoding.ASCII.GetString(numArray, 0, count1));
                string s1 = string.Format("Action: Login\r\nUsername: {0}\r\nSecret: {1}\r\nEvents: call,hud\r\n\r\n", (object)this.username, (object)this.password);
                stream.Write(Encoding.ASCII.GetBytes(s1), 0, s1.Length);
                stream.Flush();
                Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                dictionary2.Add("0", "0");
                while (true)
                {
                    flag = true;
                    numArray = new byte[1];
                    int count2 = stream.Read(numArray, 0, numArray.Length);
                    if (count2 != 0)
                    {
                        string str1 = Encoding.ASCII.GetString(numArray, 0, count2);
                        input1 += str1;
                        if (input1.IndexOf("\r\n\r\n") != -1)
                        {
                            foreach (string input2 in Regex.Split(input1, "\r\n"))
                            {
                                Console.WriteLine(input2);
                                if (input2.IndexOf(":") != -1)
                                {
                                    string[] strArray = Regex.Split(input2, ": ");
                                    string key = strArray[0].Trim();
                                    string str2 = strArray[1].Trim();
                                    dictionary1.Add(key, str2);
                                }
                            }
                            foreach (KeyValuePair<string, string> keyValuePair in dictionary1)
                                Console.WriteLine(keyValuePair.Key.ToString() + "  -  " + keyValuePair.Value.ToString());

                            //grandstream
                            // grandstream
                            if (dictionary1.ContainsValue("DialBegin") && dictionary1.ContainsKey("CallerIDNum") && dictionary1["Event"] == "DialBegin" && dictionary1.ContainsKey("Privilege") && dictionary1["Privilege"] == "call,all")

                            // elastix
                            // if (dictionary1.ContainsValue("Dial") && dictionary1.ContainsKey("CallerIDNum") && dictionary1["Event"] == "Dial" && dictionary1.ContainsKey("Privilege") && dictionary1["Privilege"] == "call,all")
                            {
                                Console.WriteLine("Callling Exten,Group\r\n");
                                string s2 = dictionary1["CallerIDNum"];
                                string s3 = dictionary1["DestCallerIDNum"];
                                string str2 = dictionary1["DestUniqueid"];
                                string str4 = dictionary1["Channel"];
                                string str7 = dictionary1["ConnectedLineNum"];

                                // in call
                                long result;
                                if (long.TryParse(s2, out result) && s2.Length > 6 && s3.Length < 6 && long.TryParse(s3, out result))
                                {
                                    Console.WriteLine("sdt=" + s2 + "-mayle=" + s3 + "-uniqueid=" + str2 + "-flag=0");
                                    if (this.Tick != null)
                                        this.Tick(this, new popup()
                                        {
                                            sdt = s2,
                                            mayle = s3,
                                            uniqueid = str2,
                                            flag = 0
                                        });
                                }

                                if (long.TryParse(s2, out result) && s2.Length <= 4 && long.TryParse(str7, out result) && str7.Length > 4 & str4.Contains("SIP"))
                                {
                                    Console.WriteLine("sdt=" + s2 + "-mayle=" + s3 + "-uniqueid=" + str2 + "-flag=0");
                                    if (this.Tick != null)
                                        this.Tick(this, new popup()
                                        {
                                            sdt = str7,
                                            mayle = s2,
                                            uniqueid = str2,
                                            flag = 0,
                                            type = 1
                                        });
                                }


                                // click to call
                                if (long.TryParse(str7, out result) && s3.Length > 19 && long.TryParse(s3, out result) == false && str4.Contains("Local"))
                                {
                                    var str3 = s3.Split('/');

                                    // string str4 = dictionary1["Channel"];
                                    string str5 = str4.Substring(str4.IndexOf("/") + 1, str4.IndexOf("-") - str4.IndexOf("/") - 6);
                                    if (str5.Length <= 3)
                                    {
                                        Console.WriteLine("sdt=" + str3 + "-mayle=" + str5 + "-uniqueid=" + str2 + "-flag=1");
                                        if (this.Tick != null)
                                            this.Tick(this, new popup()
                                            {
                                                sdt = str3[1],
                                                mayle = str5,
                                                uniqueid = str2,
                                                flag = 1
                                            });
                                    }

                                }
                                if (long.TryParse(str7, out result) && s3.Length > 19 && long.TryParse(s3, out result) == false && str4.Contains("SIP"))
                                {
                                    var str3 = s3.Split('/');

                                    // string str4 = dictionary1["Channel"];
                                    string str5 = str4.Substring(str4.IndexOf("/") + 1, str4.IndexOf("-") - str4.IndexOf("/") - 1);
                                    if (str5.Length <= 3)
                                    {
                                        Console.WriteLine("sdt=" + str3 + "-mayle=" + str5 + "-uniqueid=" + str2 + "-flag=1");
                                        if (this.Tick != null)
                                            this.Tick(this, new popup()
                                            {
                                                sdt = str3[1],
                                                mayle = str5,
                                                uniqueid = str2,
                                                flag = 1,
                                                type = 1
                                            });
                                    }

                                }

                            }
                            input1 = "";
                            dictionary1.Clear();
                        }
                    }
                    else
                        break;
                }
                Console.WriteLine("Khong co due lieu de doc");
                dictionary2.Clear();
                Console.WriteLine("Cho thiet lap ket noi ....");
                Thread.Sleep(3000);
            }
        }

        private void listener2()
        {
            if (this.key_input == "" || !this.xacthuc(this.key_input))
                return;
            byte[] numArray = new byte[1024];
            string input1 = "";
            while (true)
            {
                bool flag = true;
                try
                {
                    this.server = new TcpClient(this.host, this.port);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Unable to connect to server");
                    continue;
                }
                NetworkStream stream = this.server.GetStream();
                int count1 = stream.Read(numArray, 0, numArray.Length);
                Console.WriteLine(Encoding.ASCII.GetString(numArray, 0, count1));
                string s1 = string.Format("Action: Login\r\nUsername: {0}\r\nSecret: {1}\r\nEvents: call,hud\r\n\r\n", (object)this.username, (object)this.password);
                stream.Write(Encoding.ASCII.GetBytes(s1), 0, s1.Length);
                stream.Flush();
                Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                dictionary2.Add("0", "0");
                while (true)
                {
                    flag = true;
                    numArray = new byte[1];
                    int count2 = stream.Read(numArray, 0, numArray.Length);
                    if (count2 != 0)
                    {
                        string str1 = Encoding.ASCII.GetString(numArray, 0, count2);
                        input1 += str1;
                        if (input1.IndexOf("\r\n\r\n") != -1)
                        {
                            foreach (string input2 in Regex.Split(input1, "\r\n"))
                            {
                                Console.WriteLine(input2);
                                if (input2.IndexOf(":") != -1)
                                {
                                    string[] strArray = Regex.Split(input2, ": ");
                                    string key = strArray[0].Trim();
                                    string str2 = strArray[1].Trim();
                                    dictionary1.Add(key, str2);
                                }
                            }
                            foreach (KeyValuePair<string, string> keyValuePair in dictionary1)
                                Console.WriteLine(keyValuePair.Key.ToString() + "  -  " + keyValuePair.Value.ToString());
                            if (dictionary1.ContainsValue("DialBegin") && dictionary1.ContainsKey("CallerIDNum") && dictionary1.ContainsKey("DestCallerIDNum") && dictionary1["Event"] == "DialBegin")
                            //  if (dictionary1.ContainsValue("Dial") && dictionary1.ContainsKey("CallerIDNum") && dictionary1.ContainsKey("Dialstring") && dictionary1["Event"] == "Dial")
                            // if (dictionary1.ContainsValue("Dial") && dictionary1.ContainsKey("CallerIDNum") && dictionary1.ContainsKey("DestCallerIDNum") && dictionary1["Event"] == "Dial")
                            {
                                Console.WriteLine("Callling Exten,Group\r\n");
                                string s2 = dictionary1["CallerIDNum"].Replace("+84", "0");

                                string s3 = dictionary1["Dialstring"];
                                string str2 = dictionary1["UniqueID"];
                                long result;
                                var a = long.TryParse(s2, out result);
                                var b = s2.Length;
                                var c = s3.Length;
                                var d = long.TryParse(s3, out result);

                                // goi di

                                if (dictionary1["Dialstring"].Contains("/"))
                                {
                                    string[] s8 = dictionary1["Dialstring"].ToString().Split('/'); // 0912
                                    string s9 = dictionary1["CallerIDNum"]; // 800
                                    if (long.TryParse(s9, out result) && s8[1].Length > 6 && s9.Length < 6 && long.TryParse(s8[1], out result))
                                    {
                                        // Console.WriteLine("sdt=" + s2 + "-mayle=" + s3 + "-uniqueid=" + str2 + "-flag=0");
                                        if (this.Tick != null)
                                            this.Tick(this, new popup()
                                            {
                                                sdt = s8[1].Replace("84", "0"),
                                                mayle = s9,
                                                uniqueid = str2,
                                                flag = 0
                                            });
                                    }
                                }
                                else
                                {
                                    if (long.TryParse(s2, out result) && s2.Length > 6 && s3.Length < 6 && long.TryParse(s3, out result))
                                    {
                                        Console.WriteLine("sdt=" + s2 + "-mayle=" + s3 + "-uniqueid=" + str2 + "-flag=0");
                                        if (this.Tick != null)
                                            this.Tick(this, new popup()
                                            {
                                                sdt = s2,
                                                mayle = s3,
                                                uniqueid = str2,
                                                flag = 0
                                            });
                                    }
                                    if (long.TryParse(s2, out result) && s3.Length > 6 && long.TryParse(s3, out result))
                                    {
                                        string str3 = s3;
                                        string str4 = dictionary1["Channel"];
                                        string str5 = str4.Substring(str4.IndexOf("/") + 1, str4.IndexOf("-") - str4.IndexOf("/") - 1);
                                        Console.WriteLine("sdt=" + str3 + "-mayle=" + str5 + "-uniqueid=" + str2 + "-flag=1");
                                        if (this.Tick != null)
                                            this.Tick(this, new popup()
                                            {
                                                sdt = str3,
                                                mayle = str5,
                                                uniqueid = str2,
                                                flag = 1
                                            });
                                    }
                                }


                            }
                            input1 = "";
                            dictionary1.Clear();
                        }
                    }
                    else
                        break;
                }
                Console.WriteLine("Khong co due lieu de doc");
                dictionary2.Clear();
                Console.WriteLine("Cho thiet lap ket noi ....");
                Thread.Sleep(3000);
            }
        }

        private string taokey()
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(this.host + this.port.ToString() + this.username + this.password));
            StringBuilder stringBuilder = new StringBuilder();
            int num1 = 0;
            foreach (byte num2 in hash)
            {
                if (num1 < 8)
                {
                    byte num3 = (byte)((uint)num2 ^ 3U);
                    if ((int)num3 <= 9)
                        stringBuilder.Append("0");
                    stringBuilder.Append(num3.ToString("x1").ToUpper());
                    if (num1 == 1 || num1 == 3 || num1 == 5)
                        stringBuilder.Append("-");
                    ++num1;
                }
                else
                    break;
            }
            return stringBuilder.ToString();
        }

        public void Clicktocall(string sdtkh, string extension)
        {
            if (this.key_input == "" || !this.xacthuc(this.key_input))
                return;
            byte[] numArray1 = new byte[1024];
            string str1 = "";
            TcpClient tcpClient;
            try
            {
                tcpClient = new TcpClient(this.host, this.port);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Unable to connect to server");
                return;
            }
            NetworkStream stream = tcpClient.GetStream();
            int count1 = stream.Read(numArray1, 0, numArray1.Length);
            Console.WriteLine(Encoding.ASCII.GetString(numArray1, 0, count1));
            string s1 = string.Format("Action: Login\r\nUsername: {0}\r\nSecret: {1}\r\nEvents: call,hud\r\n\r\n", (object)this.username, (object)this.password);
            stream.Write(Encoding.ASCII.GetBytes(s1), 0, s1.Length);
            stream.Flush();
            do
            {
                byte[] numArray2 = new byte[1];
                int count2 = stream.Read(numArray2, 0, numArray2.Length);
                if (count2 != 0)
                {
                    string str2 = Encoding.ASCII.GetString(numArray2, 0, count2);
                    str1 += str2;
                }
                else
                    goto label_6;
            }
            while (str1.IndexOf("\r\n\r\n") == -1);
            goto label_9;
        label_6:
            Console.WriteLine("Khong co due lieu de doc");
        label_9:
            //  string s2 = "Action: Originate\r\nChannel: Local/" + extension + "@from-internal\r\nContext: from-internal\r\nExten: " + sdtkh + "\r\nPriority: 1\r\nTimeout: 30000 \r\nCallerid: " + extension + "\r\n\r\n";
            string s2 = "Action: Originate\r\nChannel: Local/" + extension + "@from-internal\r\nContext: from-internal\r\nExten: " + sdtkh + "\r\nPriority: 1\r\nTimeout: 30000 \r\nCallerid: " + extension + "\r\n\r\n";
            stream.Write(Encoding.ASCII.GetBytes(s2), 0, s2.Length);
            stream.Flush();
            Console.WriteLine("Disconnecting from server...");
            stream.Close();
            tcpClient.Close();
        }

        public void download(string filedir, string filename)
        {
            try
            {
                if (this.key_input == "" || !this.xacthuc(this.key_input))
                    return;
                string infoUcm = this.GetInfoUCM();
                if (infoUcm == "")
                {
                    Console.WriteLine("Error: Chua cau hinh thong tin tong dai UCM");
                }
                else
                {
                    string[] strArray = infoUcm.Split('|');
                    string str = strArray[0];
                    string user = strArray[1];
                    string pass = strArray[2];
                    string url = "https://" + str + "/recapi?filedir=" + filedir + "&filename=" + filename;
                    this.saveFileDialog1.DefaultExt = "wav";
                    this.saveFileDialog1.Filter = "Wav file |*.wav|Wav file (*.wav)|*.*";
                    this.saveFileDialog1.AddExtension = true;
                    this.saveFileDialog1.RestoreDirectory = true;
                    this.saveFileDialog1.Title = "Chọn thư mục bạn muốn lưu file ghi âm";
                    this.saveFileDialog1.InitialDirectory = "C:/";
                    if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = this.saveFileDialog1.FileName;
                        this.downloadproges(url, user, pass, fileName);
                        Process.Start(new ProcessStartInfo("wmplayer.exe", fileName));
                    }
                    this.saveFileDialog1.Dispose();
                    this.saveFileDialog1 = (SaveFileDialog)null;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string downloadproges(string url, string user, string pass, string pathdownload)
        {
            ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)delegate
            {
                return true;
            };
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Credentials = (ICredentials)this.GetCredential(url, user, pass);
            webRequest.PreAuthenticate = true;
            using (WebResponse response = webRequest.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                MemoryStream memoryStream = new MemoryStream();
                byte[] buffer = new byte[4097];
                while (true)
                {
                    int count = responseStream.Read(buffer, 0, buffer.Length);
                    memoryStream.Write(buffer, 0, count);
                    if (count == 0)
                        break;
                }
                byte[] array = memoryStream.ToArray();
                FileStream fileStream = new FileStream(pathdownload, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fileStream.Write(array, 0, array.Length);
                fileStream.Close();
                memoryStream.Close();
                responseStream.Close();
            }
            return pathdownload;
        }

        private CredentialCache GetCredential(string url, string user, string pass)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            return new CredentialCache()
      {
        {
          new Uri(url),
          "Basic",
          new NetworkCredential(user, pass)
        }
      };
        }

        public void SetUCM_Download(string _IP, int _Port, string _username, string _password)
        {
            if (_IP == "" || _username == "" || _password == "" || _Port == 0)
                this.strconnect_download = "";
            else
                this.strconnect_download = _IP + ":" + _Port.ToString() + "|" + _username + "|" + _password;
        }

        private string GetInfoUCM()
        {
            return this.strconnect_download;
        }

        public delegate void TickHandler(PopupUCMThienVM m, popup e);
    }
}
