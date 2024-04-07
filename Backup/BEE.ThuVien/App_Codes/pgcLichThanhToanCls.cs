using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class pgcLichThanhToanCls
	{
        public int ID;
        public pgcPhieuGiuChoCls PGC = new pgcPhieuGiuChoCls();
		public byte DotTT;
		public DateTime NgayTT;
		public byte TyLeTT;
		public double TuongUng;
		public double ThueVAT;
		public string DienGiai;
        public double SoTien, LaiSuat, TienSDDat;
		public bool IsPay;

		public pgcLichThanhToanCls()
		{
		}

		public pgcLichThanhToanCls(int _MaPGC, byte _DotTT)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaPGC", _MaPGC);
			sqlCmd.Parameters.AddWithValue("@DotTT", _DotTT);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
                PGC.MaPGC = int.Parse(dread["MaPGC"].ToString());
				DotTT = byte.Parse(dread["DotTT"].ToString());
				NgayTT = (DateTime)dread["NgayTT"];
				TyLeTT = byte.Parse(dread["TyLeTTTT"].ToString());
				TuongUng = double.Parse(dread["TuongUng"].ToString());
				ThueVAT = double.Parse(dread["ThueVAT"].ToString());
				DienGiai = dread["DienGiai"] as string;
				SoTien = double.Parse(dread["SoTien"].ToString());
				IsPay = (bool)dread["IsPay"];
                LaiSuat = double.Parse(dread["LaiSuat"].ToString());
                TienSDDat = dread["TienSDDat"].ToString() == "" ? 0 : double.Parse(dread["TienSDDat"].ToString());
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
			sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
			sqlCmd.Parameters.AddWithValue("@TuongUng", TuongUng);
			sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
			sqlCmd.Parameters.AddWithValue("@IsPay", IsPay);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            //sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", this.ID);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
			sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
			sqlCmd.Parameters.AddWithValue("@TuongUng", TuongUng);
			sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
			//sqlCmd.Parameters.AddWithValue("@IsPay", IsPay);
            //sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void UpdateLaiSuat()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_LaiSuat", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_update2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateNgayTT()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_updateNgayTT", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@MaNV", BEE.ThuVien.Properties.Settings.Default.StaffID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("pgcLichThanhToan_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public void SelectNextPay()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_getNextPay", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
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
            SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_getNextPay2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
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
			SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_delete", sqlCon);

			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", this.ID);

			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgcLichThanhToan_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPGC", PGC.MaPGC);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                PGC.MaPGC = int.Parse(dread["MaPGC"].ToString());
                DotTT = byte.Parse(dread["DotTT"].ToString());
                NgayTT = (DateTime)dread["NgayTT"];
                TyLeTT = Convert.ToByte(dread["TyLeTT"]);
                TuongUng = double.Parse(dread["TuongUng"].ToString());
                ThueVAT = double.Parse(dread["ThueVAT"].ToString());
                DienGiai = dread["DienGiai"] as string;
                //SoTien = double.Parse(dread["SoTien"].ToString());
                IsPay = (bool)dread["IsPay"];
                LaiSuat = dread["LaiSuat"].ToString() == "" ? 0 : double.Parse(dread["LaiSuat"].ToString());
                TienSDDat = dread["TienSDDat"].ToString() == "" ? 0 : double.Parse(dread["TienSDDat"].ToString());
            }
            sqlCon.Close();
        }
	}
}
