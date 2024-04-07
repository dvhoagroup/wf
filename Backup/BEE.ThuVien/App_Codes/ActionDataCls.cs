using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class ActionDataCls : List<AccessDataCls>
    {
        //public PermissionsCls Per = new PermissionsCls();
        //public FormsCls Form = new FormsCls();
        public FeaturesCls Feature = new FeaturesCls();
        public AccessDataCls AccessData = new AccessDataCls();

        public ActionDataCls()
        {
        }

        public ActionDataCls(int _PerID, int _FormID, byte _FeatureID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ActionData_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PerID", _PerID);
            sqlCmd.Parameters.AddWithValue("@FormID", _FormID);
            sqlCmd.Parameters.AddWithValue("@FeatureID", _FeatureID);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                AccessData.Per.PerID = int.Parse(dread["PerID"].ToString());
                AccessData.Form.FormID = int.Parse(dread["FormID"].ToString());
                Feature.FeatureID = byte.Parse(dread["FeatureID"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ActionData_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PerID", AccessData.Per.PerID);
            sqlCmd.Parameters.AddWithValue("@FormID", AccessData.Form.FormID);
            sqlCmd.Parameters.AddWithValue("@FeatureID", Feature.FeatureID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ActionData_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PerID", AccessData.Per.PerID);
            sqlCmd.Parameters.AddWithValue("@FormID", AccessData.Form.FormID);
            sqlCmd.Parameters.AddWithValue("@FeatureID", Feature.FeatureID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("ActionData_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ActionData_getByPerID_FormID", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("PerID", AccessData.Per.PerID);
            sqlCmd.Parameters.AddWithValue("FormID", AccessData.Form.FormID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy(int _FeatureID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ActionData_getByPerID_FormID_FeatureID", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("PerID", AccessData.Per.PerID);
            sqlCmd.Parameters.AddWithValue("FormID", AccessData.Form.FormID);
            sqlCmd.Parameters.AddWithValue("FeatureID", _FeatureID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("ActionData_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PerID", AccessData.Per.PerID);
            sqlCmd.Parameters.AddWithValue("@FormID", AccessData.Form.FormID);
            sqlCmd.Parameters.AddWithValue("@FeatureID", Feature.FeatureID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}