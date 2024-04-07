using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class SendMailCls
    {
        public int KeySend;
        public string Title;
        public string Contents;
        public string MailFrom;
        public DateTime SendDate;
        public NhanVienCls NhanVien = new NhanVienCls();
        public string FileAtach;
        public int SendCount;

        public SendMailCls()
        {
        }

        public SendMailCls(int _KeySend)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SendMail_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@KeySend", _KeySend);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                KeySend = int.Parse(dread["KeySend"].ToString());
                Title = dread["Title"] as string;
                Contents = dread["Contents"] as string;
                MailFrom = dread["MailFrom"] as string;
                SendDate = (DateTime)dread["SendDate"];
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                FileAtach = dread["FileAtach"] as string;
                SendCount = int.Parse(dread["SendCount"].ToString());
            }
            sqlCon.Close();
        }

        public int Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SendMail_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@KeySend", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@Title", Title);
            sqlCmd.Parameters.AddWithValue("@Contents", Contents);
            sqlCmd.Parameters.AddWithValue("@MailFrom", MailFrom);
            sqlCmd.Parameters.AddWithValue("@SendDate", SendDate);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@FileAtach", FileAtach);
            sqlCmd.Parameters.AddWithValue("@SendCount", SendCount);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@KeySend"].Value.ToString());
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SendMail_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@KeySend", KeySend);
            sqlCmd.Parameters.AddWithValue("@Title", Title);
            sqlCmd.Parameters.AddWithValue("@Contents", Contents);
            sqlCmd.Parameters.AddWithValue("@MailFrom", MailFrom);
            sqlCmd.Parameters.AddWithValue("@SendDate", SendDate);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@FileAtach", FileAtach);
            sqlCmd.Parameters.AddWithValue("@SendCount", SendCount);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateCount()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SendMail_updateCount", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@KeySend", KeySend);
            sqlCmd.Parameters.AddWithValue("@SendCount", SendCount);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("SendMail_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SendMail_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@KeySend", KeySend);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}