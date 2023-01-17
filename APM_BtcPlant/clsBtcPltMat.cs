using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APM_BtcPlant
{
    public class clsBtcPltMat
    {
        private string strPrivateBtcPltMatCode;
        private decimal strPrivateBtcPltMatUsage;
        private string strPrivateEpicorMatCode;
        private string strPrivateEpicorMatDesc;
        private string strPrivateEpicorMatUOM;
        private int intPrivateEpicorMatSeq;



        public string strBtcPltMatCode
        {
            get
            { return strPrivateBtcPltMatCode; }
            set
            { strPrivateBtcPltMatCode = value; }
        }

        public decimal strBtcPltMatUsage
        {
            get
            { return strPrivateBtcPltMatUsage; }
            set
            { strPrivateBtcPltMatUsage = value; }
        }

        public string strEpicorMatCode
        {
            get
            { return strPrivateEpicorMatCode; }
            set
            { strPrivateEpicorMatCode = value; }
        }

        public string strEpicorMatDesc
        {
            get
            { return strPrivateEpicorMatDesc; }
            set
            { strPrivateEpicorMatDesc = value; }
        }

        public string strEpicorMatUOM
        {
            get
            { return strPrivateEpicorMatUOM; }
            set
            { strPrivateEpicorMatUOM = value; }
        }

        public int strEpicorMatSeq
        {
            get
            { return intPrivateEpicorMatSeq; }
            set
            { intPrivateEpicorMatSeq = value; }
        }

    }
}
