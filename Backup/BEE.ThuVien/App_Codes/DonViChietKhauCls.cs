using System;
using System.Data;

namespace it
{
    public class DonViChietKhauCls
    {
        public static DataTable getList()
        {
            return CommonCls.Table("DonViChietKhau_getList");
        }
    }
}
