using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class aCustomerCls
    {
        public int StaffID { get; set; }
        public int CustomerID { get; set; }
        public bool IsNew { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsConfirm2 { get; set; }
        public string CustomerName { get; set; }
        public string IdentityCard { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int StaffIDConfirm { get; set; }
        public DateTime DateCreate { get; set; }

        public aCustomerCls()
        {
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("aCustomer_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@StaffID", StaffID);
            sqlCmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Share()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("aCustomer_addShare", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@StaffID", StaffID);
            sqlCmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            sqlCmd.Parameters.AddWithValue("@StaffIDConfirm", StaffIDConfirm);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void InsertConfirm()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("aCustomer_add2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@StaffID", StaffID);
            sqlCmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            sqlCmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            sqlCmd.Parameters.AddWithValue("@Address", Address);
            sqlCmd.Parameters.AddWithValue("@IdentityCard", IdentityCard);
            sqlCmd.Parameters.AddWithValue("@Phone", Phone);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("aCustomer_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@StaffID", StaffID);
            sqlCmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            sqlCmd.Parameters.AddWithValue("@StaffIDConfirm", StaffIDConfirm);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("aCustomer_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@StaffID", StaffID);
            sqlCmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable ListNotConfirm()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("aCustomer_getNotConfirm", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }
    }
}