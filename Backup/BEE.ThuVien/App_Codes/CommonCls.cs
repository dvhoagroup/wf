using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Linq;

namespace it
{
	public class CommonCls
	{
		public static string Conn = "";

        public static int GetAccessData(int perID, int formID)
        {
            it.AccessDataCls o = new it.AccessDataCls(perID, formID);

            return o.SDB.SDBID;
        }

        public static DataRow Row(string Str)
        {
            SqlConnection SqlConn = new SqlConnection(Conn);
            SqlDataAdapter Ad;
            DataTable Dt = new DataTable();
            try
            {
                Ad = new SqlDataAdapter(Str, SqlConn);
                SqlConn.Open();
                Ad.Fill(Dt);
                SqlConn.Close();
                return Dt.Rows[0];
            }
            catch
            {
                SqlConn.Close();
                return null;
            }
        }

        public static bool TestConnect()
        {
            SqlConnection SqlConn = new SqlConnection(Conn);
            SqlCommand sqlCmd;
            try
            {
                sqlCmd = new SqlCommand("select top 1 * from TinhTrangBDS", SqlConn);
                SqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                SqlConn.Close();
                return true;
            }
            catch {
                SqlConn.Close();
                return false;
            }
        }

        public static bool TestConnect(string Connection)
        {
            Conn = Connection;
            SqlConnection SqlConn = new SqlConnection(Conn);
            SqlCommand sqlCmd;
            try
            {
                sqlCmd = new SqlCommand("select top 1 * from TinhTrangBDS", SqlConn);
                SqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                SqlConn.Close();
                return true;
            }
            catch
            {
                SqlConn.Close();
                return false;
            }
        }
        
        //Blink form
        [DllImport("user32.dll")]
        public static extern int FlashWindow(IntPtr Hwnd, bool Revert);
        [DllImport("user32.dll")]
        public extern static IntPtr GetForegroundWindow();

        //Check connect internet
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);
        public static bool IsConnectedToInternet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }

        public static DataTable Table(string Str)
        {
            SqlConnection SqlConn = new SqlConnection(Conn);
            SqlDataAdapter Ad;
            DataTable Dt = new DataTable();
            try
            {
                Ad = new SqlDataAdapter(Str, SqlConn);
                SqlConn.Open();
                Ad.Fill(Dt);
                SqlConn.Close();
            }
            catch
            {
                SqlConn.Close();
            }
            return Dt;
        }        

        static public string MaHoa(string inputString)
        {
            CspParameters _cpsParameter;
            RSACryptoServiceProvider RSAProvider;
            _cpsParameter = new CspParameters();
            _cpsParameter.Flags = CspProviderFlags.UseMachineKeyStore;
            RSAProvider = new RSACryptoServiceProvider(1024, _cpsParameter); 

            // TODO: Add Proper Exception Handlers
            CspParameters CSPParam = new CspParameters();
            CSPParam.Flags = CspProviderFlags.UseMachineKeyStore;

            RSACryptoServiceProvider rsaCryptoServiceProvider;
            //if (System.Web.HttpContext.Current == null) // WinForm
            rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            //else // WebForm - Uses Machine store for keys
            //rsaCryptoServiceProvider = new RSACryptoServiceProvider(CSPParam);

            rsaCryptoServiceProvider.FromXmlString("<RSAKeyValue><Modulus>rxZwQi8PwO9vGKVxGFTzuehApb0MpO92N/HOAMe0Ib7VkS6++gDtrFiotHWPzUjUklKa2hJjmG+6Sh74c+iwJpU7dQGRxvoXYuF+m9r4lyGzXTrRP4Wt16SmbF8Pm6jaw9JPu1Xy+8sVBxYq8B5jyI5aaZ7aKvSBuJGLMtv/wcE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);
            // The hash function in use by the .NET RSACryptoServiceProvider here is SHA1
            // int maxLength = ( keySize ) - 2 - ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
            int dataLength = bytes.Length;
            int iterations = dataLength / 86;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= iterations; i++)
            {
                byte[] tempBytes = new byte[(dataLength - 86 * i > 86) ? 86 : dataLength - 86 * i];
                Buffer.BlockCopy(bytes, 86 * i, tempBytes, 0, tempBytes.Length);
                byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes, true);
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the DecryptString function.
                Array.Reverse(encryptedBytes);
                // Why convert to base 64?
                // Because it is the largest power-of-two base printable using only ASCII characters
                stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
            }
            return stringBuilder.ToString();
        }

        static public string GiaiMa(string inputString)
        {
            // TODO: Add Proper Exception Handlers
            CspParameters CSPParam = new CspParameters();
            CSPParam.Flags = CspProviderFlags.UseMachineKeyStore;

            RSACryptoServiceProvider rsaCryptoServiceProvider;
            //if (System.Web.HttpContext.Current == null) // WinForm
            rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            //else // WebForm - Uses Machine store for keys
            //rsaCryptoServiceProvider = new RSACryptoServiceProvider(CSPParam);

            rsaCryptoServiceProvider.FromXmlString("<RSAKeyValue><Modulus>rxZwQi8PwO9vGKVxGFTzuehApb0MpO92N/HOAMe0Ib7VkS6++gDtrFiotHWPzUjUklKa2hJjmG+6Sh74c+iwJpU7dQGRxvoXYuF+m9r4lyGzXTrRP4Wt16SmbF8Pm6jaw9JPu1Xy+8sVBxYq8B5jyI5aaZ7aKvSBuJGLMtv/wcE=</Modulus><Exponent>AQAB</Exponent><P>5nR8EplxlG0uPVGorn8OkMXZ9TF7BPa5wZs1vL4JPsxZv8D+UjufUsGrHOQmZRxvFe4J/1/iZI/6m+nHOcFk1w==</P><Q>wn7R12szMYoIMFN8UEXcEmamO7PSELqhV+qe9a/7N6G1pKG1xU3AZpkfW0E/GJZGl7pA9UQNQZTxS/LSv0AjJw==</Q><DP>inrSl4aXBp6422X3W6vDv+D0AO+Twb7Ujm9K0jjLa232PFCnQhjLuznfLcQ3Aikc42ufnFIsw0r1R70p1x3MDw==</DP><DQ>lYaKLOLtaJiF0yFb4RrUJhFkm2GTjejtQXnO23N/3zUjQH5SEG3GDRqLUMzIhU6C1wMKDYVT66dmGs2D2CSm4Q==</DQ><InverseQ>eXW6RmvwuAoo52IAnv9dBq+ixrZqhDKyFRYusjuUpFggPw7A4OknUNwJtCHeQecOCmKNTo0T+AmGfq530XnDqg==</InverseQ><D>RTclocRhAfClhqTAlNHgl/nMtLiLqxhPL8aTnZNVDpIWc5J7RPHhA2T5LH3dH1ZPUpj9RoBGhxiEGJEtvwSZvb76txmEXaUlou0ZZveeJe7O+crWT70dn06Qz+Ua7F6uwpVCQr7VmTEY4qXFowvrdH8Haz/2uHM+FFpv/1idD9E=</D></RSAKeyValue>");
            int iterations = inputString.Length / 172;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(inputString.Substring(172 * i, 172));
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the EncryptString function.
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        }

        public static string getKey()
        {
            Random random = new Random();
            return DateTime.Now.Year.ToString() +
                DateTime.Now.Month.ToString() +
                DateTime.Now.Day.ToString() +
                DateTime.Now.Hour.ToString() +
                DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() +
                DateTime.Now.Millisecond.ToString() +
                random.Next(0, 1000);
        }

        public static string getRandomChar()
        {
            string[] KyTu = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                "k", "l", "m", "n","o", "p", "q", "r", "s", "t", 
                "K", "L", "M", "N","O", "P", "Q", "R", "S", "T", 
                "a", "A", "B", "b", "c", "C", "d", "D", "e", "E",
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                "k", "l", "m", "n","o", "p", "q", "r", "s", "t", 
                "u", "v", "w", "x", "z","y", "U", "V", "W", "X", "Z","Y",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j"};
            string result = "";
            int index;
            System.Random ojRandom = new System.Random();
            for (byte i = 0; i < 90; i++)
            {
                index = ojRandom.Next(0, 111);
                result += KyTu[index];
            }
            return result;
        }

        public static void ExportExcel(DevExpress.XtraGrid.GridControl gc)
        {
            SaveFileDialog frm = new SaveFileDialog();
            frm.Filter = "Excel|*.xls";
            frm.ShowDialog();
            if (frm.FileName != "")
            {
                gc.ExportToXls(frm.FileName);
                if (BEEREMA.DialogBox.Question("Đã xử lý xong, bạn có muốn xem lại không?") == DialogResult.Yes)
                    System.Diagnostics.Process.Start(frm.FileName);
            }
        }

        public static void ExportExcel(DevExpress.XtraCharts.ChartControl chart)
        {
            SaveFileDialog frm = new SaveFileDialog();
            frm.Filter = "Excel|*.xls";
            frm.ShowDialog();
            if (frm.FileName != "")
            {
                chart.ExportToXls(frm.FileName);
                if (BEEREMA.DialogBox.Question("Đã xử lý xong, bạn có muốn xem lại không?") == DialogResult.Yes)
                    System.Diagnostics.Process.Start(frm.FileName);
            }
        }

        public static void sqlCommand(string sqlString)
        {
            SqlConnection sqlConn = new SqlConnection(it.CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand(sqlString, sqlConn);
            sqlCmd.CommandType = CommandType.Text;

            sqlConn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static void DeleteDirOfHot(int DirID)
        {
            var db = new BEE.ThuVien.MasterDataContext();
            try
            {
                var keyCode = it.CommonCls.RandomString(30, true);
                var webSite = db.webConfigs.SingleOrDefault().WebSite;
                db.NhanViens.Single(p => p.MaNV == BEE.ThuVien.Common.StaffID).KeyCode = keyCode;
                db.SubmitChanges();

                var objDir = db.webThuMucs.Single(p => p.DirID == DirID);

                Uri url = new Uri(string.Format("{0}/service/deletedir.aspx?keycode={1}&dir={2}", webSite, keyCode, objDir.DirName));
                var cmd = new System.Net.WebClient();
                cmd.DownloadString(url);

                db.webThuMucs.DeleteOnSubmit(objDir);
                db.SubmitChanges();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        public static void DeleteFileOfHot(System.Collections.Generic.List<string> files)
        {
            try
            {
                var webSite = "";
                var keyCode = it.CommonCls.RandomString(30, true);
                using (var db = new BEE.ThuVien.MasterDataContext())
                {
                    webSite = db.webConfigs.SingleOrDefault().WebSite;
                    db.NhanViens.Single(p => p.MaNV == BEE.ThuVien.Properties.Settings.Default.StaffID).KeyCode = keyCode;
                    db.SubmitChanges();
                }
                foreach (var fn in files)
                {
                    try
                    {
                        Uri url = new Uri(string.Format("{0}/service/deletefile.aspx?keycode={1}&fn={2}", webSite, keyCode, fn));
                        var cmd = new System.Net.WebClient();
                        cmd.DownloadString(url);
                    }
                    catch { }
                }
            }
            catch { }
        }

        //URL khong dau
        public static string TiegVietKhongDauURL(string str)
        {
            string KyTuDacBiet = "~!@#$%^&*()+=|\\'\",.?/:;`";
            for (byte i = 0; i < KyTuDacBiet.Length; i++)
            {
                str = str.Replace(KyTuDacBiet.Substring(i, 1), "");
            }
            str = str.Replace(" ", "-");
            str = str.Replace("--", "-");
            return str.ToLower();
        }

        public static string TiegVietKhongDau(string str)
        {
            string[,] arr = new string[14, 18]; //Tạo mảng có 14 hàng và 18 cột, mỗi hàng chứa các ký tự cùng nhóm
            string chuoi;
            string Thga, Thge, Thgo, Thgu, Thgi, Thgd, Thgy;
            string HoaA, HoaE, HoaO, HoaU, HoaI, HoaD, HoaY;
            chuoi = "aAeEoOuUiIdDyY";
            Thga = "áàạảãâấầậẩẫăắằặẳẵ";
            HoaA = "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ";
            Thge = "éèẹẻẽêếềệểễeeeeee";
            HoaE = "ÉÈẸẺẼÊẾỀỆỂỄEEEEEE";
            Thgo = "óòọỏõôốồộổỗơớờợởỡ";
            HoaO = "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ";
            Thgu = "úùụủũưứừựửữuuuuuu";
            HoaU = "ÚÙỤỦŨƯỨỪỰỬỮUUUUUU";
            Thgi = "íìịỉĩiiiiiiiiiiii";
            HoaI = "ÍÌỊỈĨIIIIIIIIIIII";
            Thgd = "đdddddddddddddddd";
            HoaD = "ĐDDDDDDDDDDDDDDDD";
            Thgy = "ýỳỵỷỹyyyyyyyyyyyy";
            HoaY = "ÝỲỴỶỸYYYYYYYYYYYY";

            //Nạp vào trong Mảng các ký tự
            //Nạp vào từng đầu hàng các ký tự không dấu
            //Nạp vào cột đầu tiên
            for (byte i = 0; i < 14; i++)
                arr[i, 0] = chuoi.Substring(i, 1);

            //Nạp vào từng ô các ký tự có dấu
            for (byte j = 1; j < 18; j++)
                for (byte i = 1; i < 18; i++)
                {
                    arr[0, i] = Thga.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thga vào từng ô trong hàng 0
                    arr[1, i] = HoaA.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaA vào từng ô trong  hàng 1
                    arr[2, i] = Thge.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thge vào từng ô trong  hàng 2
                    arr[3, i] = HoaE.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaE vào từng ô trong  hàng 3
                    arr[4, i] = Thgo.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgo vào từng ô trong  hàng 4
                    arr[5, i] = HoaO.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaO vào từng ô trong  hàng 5
                    arr[6, i] = Thgu.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgu vào từng ô trong  hàng 6
                    arr[7, i] = HoaU.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaU vào từng ô trong  hàng 7
                    arr[8, i] = Thgi.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgi vào từng ô trong  hàng 8
                    arr[9, i] = HoaI.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaI vào từng ô trong  hàng 9
                    arr[10, i] = Thgd.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgd vào từng ô trong  hàng 10
                    arr[11, i] = HoaD.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaD vào từng ô trong  hàng 11
                    arr[12, i] = Thgy.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgy vào từng ô trong  hàng 12
                    arr[13, i] = HoaY.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaY vào từng ô trong  hàng 13
                }

            //Tiến hành thay thế
            for (byte j = 0; j < 14; j++)
                for (byte i = 1; i < 18; i++)
                    str = str.Replace(arr[j, i], arr[j, 0]);

            return str;
        }
	}
}
