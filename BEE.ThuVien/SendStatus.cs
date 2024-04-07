using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEE.ThuVien
{
    public class SendStatus
    {
        public byte ID { get; set; }
        public string Name { get; set; }

        public SendStatus()
        {
        }

        public SendStatus(byte _ID, string _Name)
        {
            this.ID = _ID;
            this.Name = _Name;
        }

        public List<SendStatus> List()
        {
            var lt = new List<SendStatus>();
            lt.Add(new SendStatus(1, "Chưa gửi"));
            lt.Add(new SendStatus(2, "Đã gửi"));
            lt.Add(new SendStatus(3, "Không gửi được"));
            return lt;
        }
    }
}
