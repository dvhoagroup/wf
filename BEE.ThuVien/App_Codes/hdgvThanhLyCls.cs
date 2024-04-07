using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class hdgvThanhLyCls
    {
        public int MaHDGV;
        public DateTime NgayTL;
        public int MaKH;
        public string MaBDS;
        public string FileAttach;
        public string NoiDung;
        public int MaNV;
        public double GiaTriHD;
        public double SoTienGop;
        public double LaiSuat;
        public double ThueTNCN, LaiSuat2, LoiNhuan, GiaTriHoanTra;

        public hdgvThanhLyCls()
        {
        }

        public hdgvThanhLyCls(int _MaHDGV)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvThanhLy_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", _MaHDGV);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaHDGV = int.Parse(dread["MaHDGV"].ToString());
                NgayTL = (DateTime)dread["NgayTL"];
                MaKH = int.Parse(dread["MaKH"].ToString());
                MaBDS = dread["MaBDS"] as string;
                FileAttach = dread["FileAttach"] as string;
                NoiDung = dread["NoiDung"] as string;
                MaNV = int.Parse(dread["MaNV"].ToString());
                GiaTriHD = double.Parse(dread["GiaTriHD"].ToString());
                SoTienGop = double.Parse(dread["SoTienGop"].ToString());
                LaiSuat = double.Parse(dread["LaiSuat"].ToString());
                ThueTNCN = double.Parse(dread["ThueTNCN"].ToString());
                LaiSuat2 = double.Parse(dread["LaiSuat2"].ToString());
                LoiNhuan = double.Parse(dread["LoiNhuan"].ToString());
                GiaTriHoanTra = double.Parse(dread["GiaTriHoanTra"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvThanhLy_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCmd.Parameters.AddWithValue("@NgayTL", NgayTL);
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@GiaTriHD", GiaTriHD);
            sqlCmd.Parameters.AddWithValue("@SoTienGop", SoTienGop);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@ThueTNCN", ThueTNCN);
            sqlCmd.Parameters.AddWithValue("@LaiSuat2", LaiSuat2);
            sqlCmd.Parameters.AddWithValue("@LoiNhuan", LoiNhuan);
            sqlCmd.Parameters.AddWithValue("@GiaTriHoanTra", GiaTriHoanTra);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvThanhLy_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCmd.Parameters.AddWithValue("@NgayTL", NgayTL);
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@GiaTriHD", GiaTriHD);
            sqlCmd.Parameters.AddWithValue("@SoTienGop", SoTienGop);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@ThueTNCN", ThueTNCN);
            sqlCmd.Parameters.AddWithValue("@LaiSuat2", LaiSuat2);
            sqlCmd.Parameters.AddWithValue("@LoiNhuan", LoiNhuan);
            sqlCmd.Parameters.AddWithValue("@GiaTriHoanTra", GiaTriHoanTra);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvThanhLy_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvThanhLy_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}