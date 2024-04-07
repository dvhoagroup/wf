using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class khbhLichThanhToanCls
	{
		public int MaKHBH;
		public byte DotTT;
		public double TyLeTT;
		public string DienGiai;
        public DateTime NgayTT;
		public int SoNgay;
		public int SoThang;
        public bool IsProject;

		public khbhLichThanhToanCls()
		{
		}

		public khbhLichThanhToanCls(int _MaKHBH)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("khbhLichThanhToan_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKHBH", _MaKHBH);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaKHBH = int.Parse(dread["MaKHBH"].ToString());
				DotTT = byte.Parse(dread["DotTT"].ToString());
				TyLeTT = double.Parse(dread["TyLeTT"].ToString());
                if (dread["NgayTT"].ToString() != "")
                    NgayTT = DateTime.Parse(dread["NgayTT"].ToString());
				DienGiai = dread["DienGiai"] as string;
				SoNgay = int.Parse(dread["SoNgay"].ToString());
				SoThang = int.Parse(dread["SoThang"].ToString());
                //IsProject = (bool)dread["IsProject"];
			}
			sqlCon.Close();
		}
		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("khbhLichThanhToan_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKHBH", MaKHBH);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
            if (NgayTT.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            else
                sqlCmd.Parameters.AddWithValue("@NgayTT", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@SoNgay", SoNgay);
			sqlCmd.Parameters.AddWithValue("@SoThang", SoThang);
            //sqlCmd.Parameters.AddWithValue("@IsProject", IsProject);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("khbhLichThanhToan_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKHBH", MaKHBH);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
            if (NgayTT.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            else
                sqlCmd.Parameters.AddWithValue("@NgayTT", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
			sqlCmd.Parameters.AddWithValue("@SoNgay", SoNgay);
			sqlCmd.Parameters.AddWithValue("@SoThang", SoThang);
            sqlCmd.Parameters.AddWithValue("@IsProject", IsProject);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void UpdateTienDo()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("khbhLichThanhToan_updateTienDo", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKHBH", MaKHBH);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            if (NgayTT.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            else
                sqlCmd.Parameters.AddWithValue("@NgayTT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("khbhLichThanhToan_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(int _MaKHBH)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("khbhLichThanhToan_get " + _MaKHBH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("khbhLichThanhToan_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaKHBH", MaKHBH);
			sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
