using System;
using System.Data;
using System.Data.SqlClient;

namespace it
{
    public class LttLichSuCapNhatCls
    {
        public string MaLS { get; set; }
        public byte DotTT { get; set; }
        public int MaPGC { get; set; }
        public int MaHDMB { get; set; }
        public DateTime NgayTT { get; set; }
        public string DienGiai { get; set; }

        public static DataTable getByPGC(byte dotTT, int maPGC)
        {
            return SqlCommon.getData(string.Format("lttLichSuCapNhat_getByPGC {0}, {1}", maPGC, dotTT));
        }

        public static DataTable getByHDMB(byte dotTT, int maHDMB)
        {
            return SqlCommon.getData(string.Format("lttLichSuCapNhat_getByHDMB {0}, {1}", maHDMB, dotTT));
        }
    }
}