using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class hdcnLichThanhToanCls
	{
        public hdChuyenNhuongCls HDCN = new hdChuyenNhuongCls();
		public byte DotTT;
		public DateTime NgayTT;
		public byte TyLeTT;
		public double TuongUng;
		public double ThueVAT;
		public string DienGiai;
		public double SoTien;
		public bool IsPay;

		public hdcnLichThanhToanCls()
		{
		}

		public hdcnLichThanhToanCls(int _MaHDCN, byte _DotTT)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnLichThanhToan_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaHDCN", _MaHDCN);
			sqlCmd.Parameters.AddWithValue("@DotTT", _DotTT);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				HDCN.MaHDCN = int.Parse(dread["MaHDCN"].ToString());
				DotTT = byte.Parse(dread["DotTT"].ToString());
				NgayTT = (DateTime)dread["NgayTT"];
				TyLeTT = byte.Parse(dread["TyLeTT"].ToString());
				TuongUng = double.Parse(dread["TuongUng"].ToString());
				ThueVAT = double.Parse(dread["ThueVAT"].ToString());
				DienGiai = dread["DienGiai"] as string;
				SoTien = double.Parse(dread["SoTien"].ToString());
				IsPay = (bool)dread["IsPay"];
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnLichThanhToan_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDCN", HDCN.MaHDCN);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
			sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
			sqlCmd.Parameters.AddWithValue("@TuongUng", TuongUng);
			sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
			sqlCmd.Parameters.AddWithValue("@IsPay", IsPay);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnLichThanhToan_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDCN", HDCN.MaHDCN);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
			sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
			sqlCmd.Parameters.AddWithValue("@TuongUng", TuongUng);
			sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
			sqlCmd.Parameters.AddWithValue("@IsPay", IsPay);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("hdcnLichThanhToan_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable Select(int _MaHDCN)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdcnLichThanhToan_getAllByMaHDCN " + _MaHDCN, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void SelectNextPay()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdcnLichThanhToan_getNextPay", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDCN", HDCN.MaHDCN);
            sqlCmd.Parameters.Add("@DotTT", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@SoTien", SqlDbType.Money).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            SoTien = double.Parse(sqlCmd.Parameters["@SoTien"].Value.ToString());
            DotTT = byte.Parse(sqlCmd.Parameters["@DotTT"].Value.ToString());
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("hdcnLichThanhToan_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDCN", HDCN.MaHDCN);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
