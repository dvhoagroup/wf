using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdgvLichThanhToanCls
	{
        public hdGopVonCls HDGV = new hdGopVonCls();
		public byte DotTT;
		public DateTime NgayTT;
		public byte TyLeTT;
		public double TuongUng;
		public double ThueVAT;
		public string DienGiai;
        public double SoTien, TienSDDat;
		public bool IsPay;

		public hdgvLichThanhToanCls()
		{
		}

        public hdgvLichThanhToanCls(int _MaHDGV, byte _DotTT)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvLichThanhToan_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDGV", _MaHDGV);
			sqlCmd.Parameters.AddWithValue("@DotTT", _DotTT);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
                HDGV.MaHDGV = int.Parse(dread["MaHDGV"].ToString());
				DotTT = byte.Parse(dread["DotTT"].ToString());
				NgayTT = (DateTime)dread["NgayTT"];
				TyLeTT = byte.Parse(dread["TyLeTTTT"].ToString());
				TuongUng = double.Parse(dread["TuongUng"].ToString());
				ThueVAT = double.Parse(dread["ThueVAT"].ToString());
				DienGiai = dread["DienGiai"] as string;
				SoTien = double.Parse(dread["SoTien"].ToString());
				IsPay = (bool)dread["IsPay"];
                TienSDDat = dread["TienSDDat"].ToString() == "" ? 0 : double.Parse(dread["TienSDDat"].ToString());
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvLichThanhToan_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
			sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
			sqlCmd.Parameters.AddWithValue("@TuongUng", TuongUng);
			sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
			sqlCmd.Parameters.AddWithValue("@IsPay", IsPay);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvLichThanhToan_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
			sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
			sqlCmd.Parameters.AddWithValue("@TuongUng", TuongUng);
			sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
			sqlCmd.Parameters.AddWithValue("@IsPay", IsPay);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void Update2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvLichThanhToan_update2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", HDGV.MaHDGV);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvLichThanhToan_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdgvLichThanhToan_getByMaHDGV " + HDGV.MaHDGV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void SelectNextPay()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvLichThanhToan_getNextPay", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
            sqlCmd.Parameters.Add("@DotTT", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@SoTien", SqlDbType.Money).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            SoTien = double.Parse(sqlCmd.Parameters["@SoTien"].Value.ToString());
            DotTT = byte.Parse(sqlCmd.Parameters["@DotTT"].Value.ToString());
        }

        public void SelectNextPay2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdgvLichThanhToan_getNextPay2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.Add("@DotTT", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            DotTT = byte.Parse(sqlCmd.Parameters["@DotTT"].Value.ToString());
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdgvLichThanhToan_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDGV", HDGV.MaHDGV);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
