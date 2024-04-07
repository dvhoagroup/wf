using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class NguoiMoiGioiCls
	{
		public int MaNMG;
		public string HoTen;
		public DateTime NgaySinh;
		public string DiaChi;
		public string DienThoai;
		public string Email;
		public string SoCMND;
		public DateTime NgayCap;
		public string NoiCap;
		public string GhiChu, NoiSinh;
        public XaCls Xa = new XaCls();
        public double Rose;

		public NguoiMoiGioiCls()
		{
		}

		public NguoiMoiGioiCls(int _MaNMG)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("NguoiMoiGioi_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNMG", _MaNMG);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
                MaNMG = int.Parse(dread["MaNMG"].ToString());
				HoTen = dread["HoTen"] as string;
                if (dread["NgaySinh"].ToString() != "")
                    NgaySinh = (DateTime)dread["NgaySinh"];
				DiaChi = dread["DiaChi"] as string;
				DienThoai = dread["DienThoai"] as string;
				Email = dread["Email"] as string;
				SoCMND = dread["SoCMND"] as string;
                if (dread["NgayCap"].ToString() != "")
                    NgayCap = (DateTime)dread["NgayCap"];
				NoiCap = dread["NoiCap"] as string;
				GhiChu = dread["GhiChu"] as string;
                NoiSinh = dread["NoiSinh"] as string;
                Xa.MaXa = dread["MaXa"].ToString() == "" ? 0 : int.Parse(dread["MaXa"].ToString());
			}
			sqlCon.Close();
		}

        public void SetRose(string QueryString)
        {
            SqlConnection sqlCon = new SqlConnection(it.CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand(QueryString, sqlCon);
            sqlCmd.CommandType = CommandType.Text;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void GetRose()
        {
            SqlConnection sqlCon = new SqlConnection(it.CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("GetRose", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@Rose", SqlDbType.Float).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            Rose = double.Parse(sqlCmd.Parameters["@Rose"].Value.ToString());
        }

		public int Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("NguoiMoiGioi_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaNMG", SqlDbType.Int).Direction = ParameterDirection.Output;
			sqlCmd.Parameters.AddWithValue("@HoTen", HoTen);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
			sqlCmd.Parameters.AddWithValue("@DienThoai", DienThoai);
			sqlCmd.Parameters.AddWithValue("@Email", Email);
			sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
			sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiSinh", NoiSinh);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaNMG"].Value.ToString());
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("NguoiMoiGioi_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNMG", MaNMG);
			sqlCmd.Parameters.AddWithValue("@HoTen", HoTen);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@DienThoai", DienThoai);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
			sqlCmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            if (Xa.MaXa != 0)
                sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            else
                sqlCmd.Parameters.AddWithValue("@MaXa", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiSinh", NoiSinh);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public bool CheckSoCMND()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NguoiMoiGioi_checkSoCMND", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public bool CheckSoCMND2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NguoiMoiGioi_checkSoCMND2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("NguoiMoiGioi_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("NguoiMoiGioi_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNMG", MaNMG);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
