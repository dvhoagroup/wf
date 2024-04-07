using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class SetHappyBirthdayCls
    {
        public byte SetID;
        public string TieuDe;
        public string NoiDung;
        public byte SoNgay, GiaHan;
        public string MaNHK, MaNKH2;
        public string FileAttach;
        public byte MaThiep;
        public DateTime ThoiGianGui;

        public SetHappyBirthdayCls()
        {
        }

        public SetHappyBirthdayCls(byte _SetID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SetHappyBirthday_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SetID", _SetID);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                SetID = byte.Parse(dread["SetID"].ToString());
                TieuDe = dread["TieuDe"] as string;
                NoiDung = dread["NoiDung"] as string;
                SoNgay = byte.Parse(dread["SoNgay"].ToString());
                GiaHan = dread["GiaHan"].ToString() == "" ? (byte)0 : byte.Parse(dread["GiaHan"].ToString());
                MaNKH2 = dread["MaNHK"] as string;
                MaNHK = dread["MaNKH2"] as string;
                FileAttach = dread["FileAttach"] as string;
                MaThiep = dread["MaThiep"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaThiep"].ToString());
                if (dread["ThoiGianGui"].ToString() != "")
                    ThoiGianGui = DateTime.Parse(dread["ThoiGianGui"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SetHappyBirthday_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
            sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@SoNgay", SoNgay);
            sqlCmd.Parameters.AddWithValue("@SoNgay", SoNgay);
            sqlCmd.Parameters.AddWithValue("@GiaHan", GiaHan);
            sqlCmd.Parameters.AddWithValue("@MaNHK", MaNHK);
            sqlCmd.Parameters.AddWithValue("@MaNHK2", MaNKH2);
            if (ThoiGianGui.Year != 1)
                sqlCmd.Parameters.AddWithValue("@ThoiGianGui", ThoiGianGui);
            else
                sqlCmd.Parameters.AddWithValue("@ThoiGianGui", DBNull.Value);
            if (MaThiep != 0)
                sqlCmd.Parameters.AddWithValue("@MaThiep", MaThiep);
            else
                sqlCmd.Parameters.AddWithValue("@MaThiep", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SetHappyBirthday_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SetID", SetID);
            sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
            sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@SoNgay", SoNgay);
            sqlCmd.Parameters.AddWithValue("@GiaHan", GiaHan);
            sqlCmd.Parameters.AddWithValue("@MaNHK", MaNHK);
            sqlCmd.Parameters.AddWithValue("@MaNHK2", MaNKH2);
            if (ThoiGianGui.Year != 1)
                sqlCmd.Parameters.AddWithValue("@ThoiGianGui", ThoiGianGui);
            else
                sqlCmd.Parameters.AddWithValue("@ThoiGianGui", DBNull.Value);
            if (MaThiep != 0)
                sqlCmd.Parameters.AddWithValue("@MaThiep", MaThiep);
            else
                sqlCmd.Parameters.AddWithValue("@MaThiep", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("SetHappyBirthday_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SetHappyBirthday_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SetID", SetID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}