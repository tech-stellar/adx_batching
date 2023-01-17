using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APM_BtcPlant
{
    public class clsError
    {
        private string _ErrNum;
        private string _ErrDescription;

        public string ErrNum
        {
            get { return _ErrNum; }
            set { _ErrNum = value; }
        }

        public string ErrDescription
        {
            get { return _ErrDescription;  }
            set { _ErrDescription = value; }
        }
    }
}
