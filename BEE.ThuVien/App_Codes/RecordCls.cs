using System;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class RecordCls
    {
        private byte fSTT;
		private object fImageUrl;

        public RecordCls(byte STT, object ImageUrl)
        {
			this.fSTT = STT;
            this.fImageUrl = ImageUrl;
		}

        public byte STT
        {
            get { return fSTT; }
		}

        public object ImageUrl
        {
            get { return fImageUrl; }
            set { fImageUrl = value; }
		}
    }
}
