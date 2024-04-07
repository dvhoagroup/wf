using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class hdmbLichThanhToanCls
    {
        public HopDongMuaBanCls HDMB = new HopDongMuaBanCls();
        public int ID;
        public byte DotTT;
        public DateTime NgayTT;
        public byte TyLeTT;
        public double TuongUng;
        public double ThueVAT;
        public string DienGiai;
        public double SoTien, LaiSuat, TienSDDat;
        public bool IsPay;

        public hdmbLichThanhToanCls()
        {
        }

        public hdmbLichThanhToanCls(int _MaHDMB, byte _DotTT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbLichThanhToan_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", _MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", _DotTT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                HDMB.MaHDMB = int.Parse(dread["MaHDMB"].ToString());
                DotTT = byte.Parse(dread["DotTT"].ToString());
                NgayTT = (DateTime)dread["NgayTT"];
                TyLeTT = byte.Parse(dread["TyLeTT"].ToString());
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
            SqlCommand sqlCmd = new SqlCommand("hdmbLichThanhToan_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
            sqlCmd.Parameters.AddWithValue("@TuongUng", TuongUng);
            sqlCmd.Parameters.AddWithValue("@ThueVAT", ThueVAT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@TienSDDat", TienSDDat);
            //sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            //sqlCmd.Parameters.AddWithValue("@IsPay", IsPay);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbLichThanhToan_update", sqlCon);
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
            //sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            //sqlCmd.Parameters.AddWithValue("@IsPay", IsPay);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateLaiSuat()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbLichThanhToan_LaiSuat", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdmbLichThanhToan_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbLichThanhToan_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", this.ID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbLichThanhToan_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                HDMB.MaHDMB = int.Parse(dread["MaHDMB"].ToString());
                DotTT = byte.Parse(dread["DotTT"].ToString());
                NgayTT = (DateTime)dread["NgayTT"];
                TyLeTT = byte.Parse(dread["TyLeTT"].ToString());
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

        public void SelectNextPay()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbLichThanhToan_getNextPay", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
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
            SqlCommand sqlCmd = new SqlCommand("hdmbLichThanhToan_getNextPay2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.Add("@DotTT", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            DotTT = byte.Parse(sqlCmd.Parameters["@DotTT"].Value.ToString());
        }

        public DataTable LichTTByProject(int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("HopDongMuaBan_getLichTTByProject " + _MaDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void UpdateNgayTT()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbLichThanhToan_updateNgayTT", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@MaNV", BEE.ThuVien.Properties.Settings.Default.StaffID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}