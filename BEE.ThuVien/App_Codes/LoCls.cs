using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class LoCls
    {
        public int MaLo;
        public string TenLo;
        public BlocksCls Blocks = new BlocksCls();

        public LoCls()
        {
        }

        public LoCls(int _MaLo)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Lo_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLo", _MaLo);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaLo = int.Parse(dread["MaLo"].ToString());
                TenLo = dread["TenLo"] as string;
                Blocks.BlockID = int.Parse(dread["BlockID"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Lo_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenLo", TenLo);
            sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void InsertMulti(int Amount)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Lo_addMulti", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
            sqlCmd.Parameters.AddWithValue("@Amount", Amount);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Lo_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLo", MaLo);
            sqlCmd.Parameters.AddWithValue("@TenLo", TenLo);
            sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("Lo_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByBlock()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("Lo_getByBlockID " + Blocks.BlockID, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Lo_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLo", MaLo);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public bool Check()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Lo_check", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
            sqlCmd.Parameters.AddWithValue("@TenLo", TenLo);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public int GetMaLo()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Lo_getMaLo", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenLo", TenLo);
            sqlCmd.Parameters.AddWithValue("@BlockID", Blocks.BlockID);
            sqlCmd.Parameters.AddWithValue("@MaPK", Blocks.PhanKhu.MaPK);
            sqlCmd.Parameters.Add("@MaLo", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaLo"].Value.ToString());
        }
    }
}