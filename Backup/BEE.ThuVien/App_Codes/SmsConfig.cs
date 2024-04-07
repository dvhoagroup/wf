using System;
using System.Data;
using System.Data.SqlClient;

namespace BEEREMA
{
    public class SmsConfig
    {
        public string ClientNo { get; set; }
        public string ClientPass { get; set; }

        public void setAccount()
        {
            string sqlString = string.Format("ALTER PROCEDURE Sms_getAccount @ClientNo	nvarchar(50) output, " +
                "@ClientPass nvarchar(300) output WITH encryption AS set @ClientNo=N'{0}'; set @ClientPass=N'{1}';", ClientNo, ClientPass);
            it.CommonCls.sqlCommand(sqlString);
        }

        public void getAccount()
        {
            SqlConnection sqlConn = new SqlConnection(it.CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Sms_getAccount", sqlConn);

            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("ClientNo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("ClientPass", SqlDbType.NVarChar, 300).Direction = ParameterDirection.Output;

            sqlConn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();

            this.ClientNo = sqlCmd.Parameters["ClientNo"].Value.ToString();
            this.ClientPass = sqlCmd.Parameters["ClientPass"].Value.ToString();
            this.ClientPass = it.EncDec.Decrypt(this.ClientPass);
        }
    }
}
