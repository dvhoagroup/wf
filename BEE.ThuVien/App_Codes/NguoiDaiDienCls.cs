using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class NguoiDaiDienCls
	{
        public int MaKH;
        public int MaNDD;
        public string HoTen;
        public DateTime NgaySinh;
        public string NoiSinh;
        public string SoCMND;
        public DateTime NgayCap;
        public string NoiCap;
        public string MaSoThue;
        public string DTCD;
        public string DTDD;
        public string Email;
        public string DiaChiLL;
        public string DiaChiTT;
        public XaCls Xa = new XaCls();
        public XaCls Xa2 = new XaCls();

        public NguoiDaiDienCls()
        {
        }

        public NguoiDaiDienCls(int _MaNDD)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NguoiDaiDien_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNDD", _MaNDD);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaKH = int.Parse(dread["MaKH"].ToString());
                MaNDD = byte.Parse(dread["MaNDD"].ToString());
                HoTen = dread["HoTen"] as string;
                if (dread["NgaySinh"].ToString() != "")
                    NgaySinh = (DateTime)dread["NgaySinh"];
                NoiSinh = dread["NoiSinh"].ToString();
                SoCMND = dread["SoCMND"] as string;
                if (dread["NgayCap"].ToString() != "")
                    NgayCap = (DateTime)dread["NgayCap"];
                NoiCap = dread["NoiCap"] as string;
                MaSoThue = dread["MaSoThue"] as string;
                DTCD = dread["DTCD"] as string;
                DTDD = dread["DTDD"] as string;
                Email = dread["Email"] as string;
                DiaChiLL = dread["DiaChiLL"] as string;
                DiaChiTT = dread["DiaChiTT"] as string;
                if (dread["MaXa"].ToString() != "")
                    Xa.MaXa = int.Parse(dread["MaXa"].ToString());
                if (dread["MaXa2"].ToString() != "")
                    Xa2.MaXa = int.Parse(dread["MaXa2"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NguoiDaiDien_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@HoTen", HoTen);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@MaSoThue", MaSoThue);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            sqlCmd.Parameters.AddWithValue("@DTDD", DTDD);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@DiaChiLL", DiaChiLL);
            sqlCmd.Parameters.AddWithValue("@DiaChiTT", DiaChiTT);
            sqlCmd.Parameters.AddWithValue("@NoiSinh", NoiSinh);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NguoiDaiDien_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNDD", MaNDD);
            sqlCmd.Parameters.AddWithValue("@HoTen", HoTen);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@MaSoThue", MaSoThue);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            sqlCmd.Parameters.AddWithValue("@DTDD", DTDD);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@DiaChiLL", DiaChiLL);
            sqlCmd.Parameters.AddWithValue("@DiaChiTT", DiaChiTT);
            sqlCmd.Parameters.AddWithValue("@NoiSinh", NoiSinh);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NguoiDaiDien_update2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNDD", MaNDD);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@DTCD", DTCD);
            sqlCmd.Parameters.AddWithValue("@DTDD", DTDD);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@DiaChiLL", DiaChiLL);
            sqlCmd.Parameters.AddWithValue("@DiaChiTT", DiaChiTT);
            sqlCmd.Parameters.AddWithValue("@NoiSinh", NoiSinh);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            if (Xa2.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa2", Xa2.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa2", DBNull.Value);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public bool Check()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NguoiDaiDien_check", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Result"].Value.ToString());
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("NguoiDaiDien_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaKH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NguoiDaiDien_getByMaKH " + _MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll(int _MaKH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NguoiDaiDien_getAllByMaKH " + _MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("NguoiDaiDien_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
