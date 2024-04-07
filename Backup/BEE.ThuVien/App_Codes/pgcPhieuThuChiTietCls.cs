using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class pgcPhieuThuChiTietCls
    {
        public int KeyID;
        public int MaPT;
        public int MaPGC;
        public int MaHDMB;
        public byte DotTT;
        public double SoTien;

        public pgcPhieuThuChiTietCls()
        {
        }

        public pgcPhieuThuChiTietCls(int _KeyID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuThuChiTiet_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@KeyID", _KeyID);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                KeyID = int.Parse(dread["KeyID"].ToString());
                MaPT = int.Parse(dread["MaPT"].ToString());
                MaPGC = int.Parse(dread["MaPGC"].ToString());
                MaHDMB = int.Parse(dread["MaHDMB"].ToString());
                DotTT = byte.Parse(dread["DotTT"].ToString());
                SoTien = double.Parse(dread["SoTien"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuThuChiTiet_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPT", MaPT);
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuThuChiTiet_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@KeyID", KeyID);
            sqlCmd.Parameters.AddWithValue("@MaPT", MaPT);
            sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgcPhieuThuChiTiet_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcPhieuThuChiTiet_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@KeyID", KeyID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}