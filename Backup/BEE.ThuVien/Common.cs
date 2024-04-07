using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Linq;

namespace BEE.ThuVien
{
    public static class Common
    {
        public static int StaffID
        {
            get { return ThuVien.Properties.Settings.Default.StaffID; }
            set { Properties.Settings.Default.StaffID = value; Properties.Settings.Default.Save(); }
        }
        public static string StaffName
        {
            get { return Properties.Settings.Default.StaffName; }
            set { Properties.Settings.Default.PerName = value; Properties.Settings.Default.Save(); }
        }
        public static bool IsHide
        {
            get { return Properties.Settings.Default.IsHide; }
            set { Properties.Settings.Default.IsHide = value; Properties.Settings.Default.Save(); }
        }
        public static byte DepartmentID
        {
            get { return Properties.Settings.Default.DepartmentID; }
            set { Properties.Settings.Default.DepartmentID = value; Properties.Settings.Default.Save(); }
        }

        public static byte GroupID
        {
            get { return Properties.Settings.Default.GroupID; }
            set { Properties.Settings.Default.GroupID = value; Properties.Settings.Default.Save(); }
        }

        public static int PerID
        {
            get { return Properties.Settings.Default.PerID; }
            set { Properties.Settings.Default.PerID = value; Properties.Settings.Default.Save(); }
        }

        public static string PerName
        {
            get { return Properties.Settings.Default.PerName; }
            set { Properties.Settings.Default.PerName = value; Properties.Settings.Default.Save(); }
        }

        public static short LangID
        {
            get { return Properties.Settings.Default.LangID; }
            set { Properties.Settings.Default.LangID = value; Properties.Settings.Default.Save(); }
        }

        public static string Skins
        {
            get { return Properties.Settings.Default.Skins; }
            set { Properties.Settings.Default.Skins = value; Properties.Settings.Default.Save(); }
        }

        public static string SqlConnString
        {
            get { return Properties.Settings.Default.BEEREMA_HOALANDConnectionString; }
            set { Properties.Settings.Default.BEEREMA_HOALANDConnectionString = value; Properties.Settings.Default.Save(); }
        }

        public static string Conn
        {
            get { return Properties.Settings.Default.Conn; }
            set { Properties.Settings.Default.Conn = value; Properties.Settings.Default.Save(); }
        }

        public static string CodeCustomer { get { return Properties.Settings.Default.StaffName; } }

        public static int KeyCustomer { get { return Properties.Settings.Default.StaffID; } }

        public static string Department { get { return Properties.Settings.Default.Department; } }

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        public static string Right(this string sValue, int iMaxLength)
        {
            try
            {
                string SubString = sValue.Substring(sValue.Length - 3);
                var legFull = sValue.Length;
                var legSub = SubString.Length;
                var legEnd = legFull - legSub;
                string strHead = sValue.Substring(0, legEnd);
                var rep = "";
                int[] arr = new int[legEnd];
                foreach (var i in arr)
                {
                    rep += "x";
                }
                return rep + SubString;
            }
            catch { return sValue; }

        }

        public static string Right1(this string sValue, int iMaxLength)
        {
            try
            {
                string SubString = sValue.Substring(sValue.Length - 3);
                string strHead = sValue.Substring(0, 3);
                var legFull = sValue.Length;
                var legSub = SubString.Length;
                var legEnd = legFull - (legSub + strHead.Length);
                var rep = "";
                int[] arr = new int[legEnd];
                foreach (var i in arr)
                {
                    rep += "x";
                }
                return strHead + rep + SubString;
            }
            catch { return sValue; }

        }

        public static string SoNhaNEW(string sValue)
        {
            try
            {
                sValue += "";
                int cong = 10 - (sValue.Length);
                for (int i = 0; i < cong; i++)
                {
                    sValue = "0" + sValue;
                }
                return sValue;
            }
            catch { return sValue; }

        }

        #region Client support
        public static string ClientNo
        {
            get { return Properties.Settings.Default.ClientNo; }
            set { Properties.Settings.Default.ClientNo = value; Properties.Settings.Default.Save(); }
        }

        public static string ClientPass
        {
            get { return Properties.Settings.Default.ClientPass; }
            set { Properties.Settings.Default.ClientPass = value; Properties.Settings.Default.Save(); }
        }

        public static string ClientEmail
        {
            get { return Properties.Settings.Default.ClientEmail; }
            set { Properties.Settings.Default.ClientEmail = value; Properties.Settings.Default.Save(); }
        }

        public static string ClientName
        {
            get { return Properties.Settings.Default.ClientName; }
            set { Properties.Settings.Default.ClientName = value; Properties.Settings.Default.Save(); }
        }
        #endregion

        #region Load hinh anh
        public static System.Drawing.Bitmap ImageLoad(string imgUrl)
        {
            try
            {
                string http = "";
                using (var db = new MasterDataContext())
                {
                    http = db.tblConfigs.FirstOrDefault().WebUrl;
                    imgUrl = http + imgUrl;
                }
                return new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(imgUrl)));
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
