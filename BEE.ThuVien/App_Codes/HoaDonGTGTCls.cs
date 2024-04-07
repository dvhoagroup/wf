using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class HoaDonGTGTCls
    {
        public int MaHD;
        public string SoHD;
        public DateTime NgayKy;
        public int MaKH;
        public byte DotTT;
        public int MaHDMB;
        public int MaHDGV, MaNV;

        public HoaDonGTGTCls()
        {
        }

        public HoaDonGTGTCls(int _MaHD)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HoaDonGTGT_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHD", _MaHD);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaHD = int.Parse(dread["MaHD"].ToString());
                SoHD = dread["SoHD"] as string;
                NgayKy = (DateTime)dread["NgayKy"];
                MaKH = int.Parse(dread["MaKH"].ToString());
                DotTT = byte.Parse(dread["DotTT"].ToString());
                MaHDMB = dread["MaHDMB"].ToString() == "" ? 0 : int.Parse(dread["MaHDMB"].ToString());
                MaHDGV = dread["MaHDGV"].ToString() == "" ? 0 : int.Parse(dread["MaHDGV"].ToString());
                MaNV = int.Parse(dread["MaNV"].ToString());
            }
            sqlCon.Close();
        }

        public int Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HoaDonGTGT_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaHD", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@SoHD", SoHD);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            if (MaHDMB != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            if (MaHDGV != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDGV", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaHD"].Value.ToString());
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HoaDonGTGT_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHD", MaHD);
            sqlCmd.Parameters.AddWithValue("@SoHD", SoHD);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            if (MaHDMB != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            if (MaHDGV != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDGV", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HoaDonGTGT_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByHDMB()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HoaDonGTGT_getByMaHDMB " + MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByHDGV()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HoaDonGTGT_getByMaHDGV " + MaHDGV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HoaDonGTGT_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHD", MaHD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HoaDonGTGT_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }

        public bool Check()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HoaDonGTGT_check", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckHDGV()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("HoaDonGTGT_checkHDGV", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }
    }
}