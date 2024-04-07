using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class bdsLichSuThayDoiDTCls
	{
		public string MaBDS;
		public int DotTT;
		public double DienTichCu;
		public double DienTichMoi;

		public bdsLichSuThayDoiDTCls()
		{
		}

		public bdsLichSuThayDoiDTCls(string _MaBDS, int _DotTT)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("bdsLichSuThayDoiDT_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaBDS", _MaBDS);
			sqlCmd.Parameters.AddWithValue("@DotTT", _DotTT);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaBDS = dread["MaBDS"] as string;
				DotTT = int.Parse(dread["DotTT"].ToString());
				DienTichCu = double.Parse(dread["DienTichCu"].ToString());
				DienTichMoi = double.Parse(dread["DienTichMoi"].ToString());

			}
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("bdsLichSuThayDoiDT_getByMABDS '" + MaBDS + "'", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}
	}
}
