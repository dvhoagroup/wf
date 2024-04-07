using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using BEE.ThuVien;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Windows.Forms;
using MySql;
namespace BEE.VOIPSETUP
{
    public class clsCDRMySQL
    {
        public MySqlConnection connection;

        public bool connect()
        {
            //connect
            var db = new MasterDataContext();
            var objCF = db.DatabaseConfigs.Where(p => p.Type == 1).FirstOrDefault();
            string connectionString;
            connectionString = "SERVER=" + objCF.Server + ";" + "DATABASE=" + objCF.Database + ";" + "UID=" + objCF.Username + ";" + "PASSWORD=" + objCF.Password + ";";
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                    case 1042:
                        MessageBox.Show("Lost connect Host");
                        break;

                }
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataTable Select(DateTime tungay, DateTime denngay)
        {
            if (this.connect() != true)
            {
                MessageBox.Show("Sai kết nối MySQL vui lòng kiểm tra lại!");
                return null;

            }
            else
            {
                string query = "SELECT * FROM cdr WHERE calldate BETWEEN '" + string.Format("{0:yyyy-MM-dd 00:00:00}", tungay) + "' AND '" + string.Format("{0:yyyy-MM-dd 23:59:59}", denngay) + "'ORDER BY calldate DESC";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(query, connection);
                da.SelectCommand = cmd;
                DataSet dset = new DataSet();
                da.Fill(dset);
                return dset.Tables[0];
            }
        }

        public DataTable SelectByPhone(string sdt)
        {
            if (this.connect() != true)
            {
                MessageBox.Show("Sai kết nối MySQL vui lòng kiểm tra lại!");
                return null;

            }
            else
            {
                string query = "SELECT * FROM cdr WHERE clid like '" + "%" + sdt + "%" + "' ORDER BY start DESC";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(query, connection);
                da.SelectCommand = cmd;
                DataSet dset = new DataSet();
                da.Fill(dset);
                return dset.Tables[0];
            }
        }
        //public List<BEE.VOIPSETUP.CDR.itemCDR> slectCDR()
        //{


        //else
        //{
        //    string query = "SELECT * FROM cdr";

        //    MySqlCommand cmd = new MySqlCommand(query, connection);
        //    MySqlDataAdapter da = new MySqlDataAdapter(query, connection);
        //    da.SelectCommand = cmd;
        //    DataTable dataTable = new DataTable();
        //    da.Fill(dataTable);
        //    foreach(DataRow row in ob




        //    return dataTable;



        //using (MySqlDataReader reader = cmd.ExecuteReader())
        //{
        //    if (reader.Read())
        //    {
        //        BEE.VOIPSETUP.CDR.itemCDR s = new BEE.VOIPSETUP.CDR.itemCDR();
        //        s.id = (int)reader["id"];
        //        s.datetime = (DateTime)reader["datetime"];
        //        s.clid = (string)reader["clid"];
        //        s.src = (string)reader["src"];
        //        s.dst = (string)reader["dst"];
        //        s.dcontext = (string)reader["dcontext"];
        //        s.srctrunk = (string)reader["srctrunk"];
        //        s.dstrunk = (string)reader["dstrunk"];
        //        s.lastapp = (string)reader["lastapp"];
        //        s.lastdata = (string)reader["lastdata"];
        //        s.duration = (int)reader["duration"];
        //        s.billable = (int)reader["billable"];
        //        s.disposition = (string)reader["disposition"]; ;
        //        s.amaflags = (int)reader["amaflags"]; ;
        //        s.calltype = (string)reader["calltype"]; ;
        //        s.accountcode = (string)reader["accountcode"];
        //        s.uniqueid = (string)reader["uniqueid"]; ;
        //        s.recordfile = (string)reader["recordfile"];
        //        s.recordpath = (string)reader["recordpath"];
        //        s.monitorfile = (string)reader["monitorfile"];
        //        s.monitorpath = (string)reader["monitorpath"];
        //        s.dstmonitorfile = (string)reader["dstmonitorfile"];
        //        s.dstmonitorpath = (string)reader["dstmonitorpath"];
        //        s.extfield1 = (string)reader["extfield1"];
        //        s.extfield2 = (string)reader["extfield2"];
        //        s.extfield3 = (string)reader["extfield3"];
        //        s.extfield4 = (string)reader["extfield4"];
        //        s.extfield5 = (string)reader["extfield5"];
        //        s.payaccount = (string)reader["payaccount"];
        //        s.usercost = (string)reader["usercost"];

        //        //s.id = (int)reader["id"];
        //        //s.datetime = (DateTime)reader["datetime"];
        //        //s.clid = (string)reader["clid"];
        //        //s.src = (string)reader["src"]; ;
        //        //s.dst = (string)reader["dst"]; ;
        //        //s.dcontext = (string)reader["dcontext"]; ;
        //        //s.srctrunk = (string)reader["srctrunk"]; ;
        //        //s.dstrunk = (string)reader["dstrunk"]; ;
        //        //s.lastapp = (string)reader["lastapp"]; ;
        //        //s.lastdata = (string)reader["lastdata"]; ;
        //        //s.duration = (int)reader["duration"]; ;
        //        //s.billable = (int)reader["billable"]; ;
        //        //s.disposition = (string)reader["disposition"]; ;
        //        //s.amaflags = (int)reader["amaflags"]; ;
        //        //s.calltype = (string)reader["calltype"]; ;
        //        //s.accountcode = (string)reader["accountcode"]; ;
        //        //s.uniqueid = (string)reader["uniqueid"]; ;
        //        //s.recordfile = (string)reader["recordfile"]; ;
        //        //s.recordpath = (string)reader["recordpath"]; ;
        //        //s.monitorfile = (string)reader["monitorfile"]; ;
        //        //s.monitorpath = (string)reader["monitorpath"]; ;
        //        //s.dstmonitorfile = (string)reader["dstmonitorfile"]; ;
        //        //s.dstmonitorpath = (string)reader["dstmonitorpath"]; ;
        //        //s.extfield1 = (string)reader["extfield1"]; ;
        //        //s.extfield2 = (string)reader["extfield2"]; ;
        //        //s.extfield3 = (string)reader["extfield3"]; ;
        //        //s.extfield4 = (string)reader["extfield4"]; ;
        //        //s.extfield5 = (string)reader["extfield5"]; ;
        //        //s.payaccount = (string)reader["payaccount"]; ;
        //        //s.usercost = (string)reader["usercost"]; ;
        //        reader.Close();
        //        this.CloseConnection();
        //    }

        //}
        //  return null;

        // }
    }
}
