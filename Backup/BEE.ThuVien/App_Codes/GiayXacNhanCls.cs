using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class GiayXacNhanCls
    {
        public int MaGXN;
        public string SoPhieu;
        public DateTime NgayXN;
        public string DienGiai, MaBDS, Template;
        public int MaHDMB;
        public int MaKH;
        public int MaNV;
        public int MaGD1, MaGD2;

        public GiayXacNhanCls()
        {
        }

        public GiayXacNhanCls(int _MaGXN)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("GiayXacNhan_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGXN", _MaGXN);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaGXN = int.Parse(dread["MaGXN"].ToString());
                SoPhieu = dread["SoPhieu"] as string;
                NgayXN = (DateTime)dread["NgayXN"];
                DienGiai = dread["DienGiai"] as string;
                MaBDS = dread["MaBDS"] as string;
                MaHDMB = dread["MaHDMB"].ToString() == "" ? 0 : int.Parse(dread["MaHDMB"].ToString());
                MaKH = int.Parse(dread["MaKH"].ToString());
                MaNV = int.Parse(dread["MaNV"].ToString());
                MaGD1 = dread["MaGD1"].ToString() == "" ? 0 : int.Parse(dread["MaGD1"].ToString());
                MaGD2 = dread["MaGD2"].ToString() == "" ? 0 : int.Parse(dread["MaGD2"].ToString());
            }
            sqlCon.Close();
        }

        public int Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("GiayXacNhan_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGXN", MaGXN).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayXN", NgayXN);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            if (MaHDMB != 0)
                sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            else
                sqlCmd.Parameters.AddWithValue("@MaHDMB", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            if (MaGD1 != 0)
                sqlCmd.Parameters.AddWithValue("@MaGD1", MaGD1);
            else
                sqlCmd.Parameters.AddWithValue("@MaGD1", DBNull.Value);
            if (MaGD1 != 0)
                sqlCmd.Parameters.AddWithValue("@MaGD2", MaGD2);
            else
                sqlCmd.Parameters.AddWithValue("@MaGD2", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaGXN"].Value.ToString());
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("GiayXacNhan_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGXN", MaGXN);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayXN", NgayXN);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateTemplate()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("GiayXacNhan_updateTemplate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGXN", MaGXN);
            sqlCmd.Parameters.AddWithValue("@Template", Template);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("GiayXacNhan_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByMaHDMB()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("GiayXacNhan_getByMaHDMB " + MaHDMB + "," + MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("GiayXacNhan_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGXN", MaGXN);
            sqlCon.Open();
            sqlCmd.CommandTimeout = 0;
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("GiayXacNhan_TaoSoPhieu", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("GiayXacNhan_check", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }
    }
}