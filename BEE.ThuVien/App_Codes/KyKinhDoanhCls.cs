using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class KyKinhDoanhCls
    {
        public ChiTieuBanHangCls ChiTieu = new ChiTieuBanHangCls();
        public byte STT;
        public int SLMin;
        public int SLMax;
        public float MucPhi;

        public KyKinhDoanhCls()
        {
        }

        public KyKinhDoanhCls(int _MaKKD, byte _STT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KyKinhDoanh_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKKD", _MaKKD);
            sqlCmd.Parameters.AddWithValue("@STT", _STT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                ChiTieu.MaCT = int.Parse(dread["MaKKD"].ToString());
                STT = byte.Parse(dread["STT"].ToString());
                SLMin = int.Parse(dread["SLMin"].ToString());
                SLMax = int.Parse(dread["SLMax"].ToString());
                MucPhi = float.Parse(dread["MucPhi"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KyKinhDoanh_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SLMin", SLMin);
            sqlCmd.Parameters.AddWithValue("@SLMax", SLMax);
            sqlCmd.Parameters.AddWithValue("@MucPhi", MucPhi);
            sqlCmd.Parameters.AddWithValue("@MaCT", ChiTieu.MaCT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KyKinhDoanh_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKKD", ChiTieu.MaCT);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@SLMin", SLMin);
            sqlCmd.Parameters.AddWithValue("@SLMax", SLMax);
            sqlCmd.Parameters.AddWithValue("@MucPhi", MucPhi);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KyKinhDoanh_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(int _MaCT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("KyKinhDoanh_getByMaCT " + _MaCT, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll(int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DaiLy_DangKy_getByMaDA " + _MaDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("KyKinhDoanh_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKKD", ChiTieu.MaCT);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}