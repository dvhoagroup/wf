using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using StackExchange.Redis;

namespace BEEREMA
{

    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>\
        /// 


        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                client.Encoding = Encoding.UTF8;
                var newVersion = client.DownloadString("http://noibo.hoaland.com.vn/version19/version.txt");//.Trim();
                newVersion = newVersion.Split('|')[0].Trim();
                var oldVersion = System.IO.File.ReadAllText(Application.StartupPath + "\\version.txt", Encoding.UTF8);
                oldVersion = oldVersion.Split('|')[0].Trim();
                oldVersion = oldVersion.Trim();


                if (oldVersion.IndexOf(newVersion) < 0)//ss
                {
                    if (DialogBox.Question("Đã có phiên bản mới. Bạn có muốn cập nhật không?") == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\updater.exe");

                        return;
                    }
                    else
                    {
                       // MessageBox.Show("Phiên bản đã hết hạn vui lòng cập nhật phiên bản mới để truy cập phần mềm, hoặc liên hệ hỗ trợ kỹ thuật của HOALAND");
                        //System.Environment.Exit(1);
                    }
                }
               
            }
            catch (Exception ex)
            {
                //bá a comment tạm
                //System.Environment.Exit(1);
            }

            #region Check connect
            try
            {
                BEE.ThuVien.Common.SqlConnString = it.CommonCls.Conn = it.EncDec.Decrypt(BEE.ThuVien.Common.Conn);
            }
            catch(Exception ex)
            { }

            if (!it.CommonCls.TestConnect())
            {
                using (Connect_frm frmConnect = new Connect_frm())
                {
                    frmConnect.ShowDialog();
                }
            }

            using (HeThong.Login_frm frmLogin = new HeThong.Login_frm())
            {
                frmLogin.ShowDialog();
                if (frmLogin.DialogResult != DialogResult.OK)
                {
                    return;
                }
            }
            #endregion

            Application.Run(new frmMainNew());
        }

        public static string NumberToText(long number)
        {
            StringBuilder wordNumber = new StringBuilder();

            string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
            string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                   "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            if (number == 0) { return "Zero"; }
            if (number < 0)
            {
                wordNumber.Append("Negative ");
                number = -number;
            }

            long[] groupedNumber = new long[] { 0, 0, 0, 0 };
            int groupIndex = 0;

            while (number > 0)
            {
                groupedNumber[groupIndex++] = number % 1000;
                number /= 1000;
            }

            for (int i = 3; i >= 0; i--)
            {
                long group = groupedNumber[i];

                if (group >= 100)
                {
                    wordNumber.Append(ones[group / 100 - 1] + " Hundred ");
                    group %= 100;

                    if (group == 0 && i > 0)
                        wordNumber.Append(powers[i - 1]);
                }

                if (group >= 20)
                {
                    if ((group % 10) != 0)
                        wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");
                    else
                        wordNumber.Append(tens[group / 10 - 2] + " ");
                }
                else if (group > 0)
                    wordNumber.Append(ones[group - 1] + " ");

                if (group != 0 && i > 0)
                    wordNumber.Append(powers[i - 1]);
            }
            string str = wordNumber.ToString().Trim().ToLower();
            string A = str.Substring(0, 1).ToUpper();
            return A + str.Substring(1);
        }

        public static string IntegerToWords(long inputNum)
        {
            int dig1, dig2, dig3, level = 0, lasttwo, threeDigits;

            string retval = "";
            string x = "";
            string[] ones ={
                "zero",
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
                "ten",
                "eleven",
                "twelve",
                "thirteen",
                "fourteen",
                "fifteen",
                "sixteen",
                "seventeen",
                "eighteen",
                "nineteen"
              };
            string[] tens ={
                "zero",
                "ten",
                "twenty",
                "thirty",
                "forty",
                "fifty",
                "sixty",
                "seventy",
                "eighty",
                "ninety"
              };
            string[] thou ={
                "",
                "thousand",
                "million",
                "billion",
                "trillion",
                "quadrillion",
                "quintillion"
              };

            bool isNegative = false;
            if (inputNum < 0)
            {
                isNegative = true;
                inputNum *= -1;
            }

            if (inputNum == 0)
                return ("zero");

            string s = inputNum.ToString();

            while (s.Length > 0)
            {
                // Get the three rightmost characters
                x = (s.Length < 3) ? s : s.Substring(s.Length - 3, 3);

                // Separate the three digits
                threeDigits = int.Parse(x);
                lasttwo = threeDigits % 100;
                dig1 = threeDigits / 100;
                dig2 = lasttwo / 10;
                dig3 = (threeDigits % 10);

                // append a "thousand" where appropriate
                if (level > 0 && dig1 + dig2 + dig3 > 0)
                {
                    retval = thou[level] + " " + retval;
                    retval = retval.Trim();
                }

                // check that the last two digits is not a zero
                if (lasttwo > 0)
                {
                    if (lasttwo < 20) // if less than 20, use "ones" only
                        retval = ones[lasttwo] + " " + retval;
                    else // otherwise, use both "tens" and "ones" array
                        retval = tens[dig2] + " " + ones[dig3] + " " + retval;
                }

                // if a hundreds part is there, translate it
                if (dig1 > 0)
                    retval = ones[dig1] + " hundred " + retval;

                s = (s.Length - 3) > 0 ? s.Substring(0, s.Length - 3) : "";
                level++;
            }

            while (retval.IndexOf("  ") > 0)
                retval = retval.Replace("  ", " ");

            retval = retval.Trim();

            if (isNegative)
                retval = "negative " + retval;

            string First = retval.Substring(0, 1);
            retval = First.ToUpper() + retval.Substring(1);

            return (retval + " VND only.");
        } 
    }
}