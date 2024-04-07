using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class AccessDataCls
    {
        public PermissionsCls Per = new PermissionsCls();
        public FormsCls Form = new FormsCls();
        public ShowDataByCls SDB = new ShowDataByCls();

        public AccessDataCls()
        {
        }

        public AccessDataCls(int _PerID, int _FormID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("AccessData_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PerID", _PerID);
            sqlCmd.Parameters.AddWithValue("@FormID", _FormID);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                Per.PerID = int.Parse(dread["PerID"].ToString());
                Form.FormID = int.Parse(dread["FormID"].ToString());
                SDB.SDBID = byte.Parse(dread["SDBID"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("AccessData_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PerID", Per.PerID);
            sqlCmd.Parameters.AddWithValue("@FormID", Form.FormID);
            sqlCmd.Parameters.AddWithValue("@SDBID", SDB.SDBID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("AccessData_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PerID", Per.PerID);
            sqlCmd.Parameters.AddWithValue("@FormID", Form.FormID);
            sqlCmd.Parameters.AddWithValue("@SDBID", SDB.SDBID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("AccessData_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByPerID()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("AccessData_getByPerID " + Per.PerID, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByPerIDNotAccess()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("AccessData_getByPerIDNotAccess " + Per.PerID, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("AccessData_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PerID", Per.PerID);
            sqlCmd.Parameters.AddWithValue("@FormID", Form.FormID);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                Per.PerID = int.Parse(dread["PerID"].ToString());
                Form.FormID = int.Parse(dread["FormID"].ToString());
                SDB.SDBID = byte.Parse(dread["SDBID"].ToString());
            }
            sqlCon.Close();
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("AccessData_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PerID", Per.PerID);
            sqlCmd.Parameters.AddWithValue("@FormID", Form.FormID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}