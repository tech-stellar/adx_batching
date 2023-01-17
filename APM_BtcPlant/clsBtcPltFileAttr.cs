using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APM_BtcPlant
{
    public class clsBtcPltFileAttr
    {
        private string _ProcessID;

        private string _BtcPltFileName;
        private string _BtcPltFilePath;
        private string _BtcPltItemCode;
        private string _BtcPltPlantCode;
        private string _BtcPltDocketNum;
        private string _BtcPltDate;
        private decimal _BtcPltItemQty;
        private string _BtcPltBatchTime;
        private string _BtcPltBatchLine;

        private string _EpicItemCode;
        private string _EpicItemDesc;
        private string _EpicItemUOM;
        private string _EpicCompany;
        private string _EpicPlant;
        private string _EpicWarehouse;
        private string _EpicBin;
        private string _EpicJobOpr;
        private string _EpicStatus;

        private string _EpicJobNum = "";
        private List<clsBtcPltMat> objBtcPltMats = new List<clsBtcPltMat>();
        private List<clsError> objErrors = new List<clsError>();

        public string ProcessID
        {
            get
            { return _ProcessID; }
            set
            { _ProcessID = value; }
        }

        public string BtcPltFileName
        {
            get
            { return _BtcPltFileName; }
            set
            { _BtcPltFileName = value; }
        }

        public string BtcPltFilePath
        {
            get
            { return _BtcPltFilePath; }
            set
            { _BtcPltFilePath = value; }
        }

        public string BtcPltItemCode
        {
            get
            { return _BtcPltItemCode; }
            set
            { _BtcPltItemCode = value; }
        }

        public string BtcPltPlantCode
        {
            get
            { return _BtcPltPlantCode; }
            set
            { _BtcPltPlantCode = value; }
        }

        public string BtcPltDocketNum
        {
            get
            { return _BtcPltDocketNum; }
            set
            { _BtcPltDocketNum = value; }
        }

        public string BtcPltDate
        {
            get
            { return _BtcPltDate; }
            set
            { _BtcPltDate = value; }
        }

        public decimal BtcPltItemQty
        {
            get
            { return _BtcPltItemQty; }
            set
            { _BtcPltItemQty = value; }
        }

        public string EpicItemCode
        {
            get
            { return _EpicItemCode; }
            set
            { _EpicItemCode = value; }
        }

        public string EpicItemDesc
        {
            get
            { return _EpicItemDesc; }
            set
            { _EpicItemDesc = value; }
        }

        public string EpicItemUOM
        {
            get
            { return _EpicItemUOM; }
            set
            { _EpicItemUOM = value; }
        }

        public string EpicCompany
        {
            get
            { return _EpicCompany; }
            set
            { _EpicCompany = value; }
        }

        public string EpicPlant
        {
            get
            { return _EpicPlant; }
            set
            { _EpicPlant = value; }
        }

        public string EpicWarehouse
        {
            get
            { return _EpicWarehouse; }
            set
            { _EpicWarehouse = value; }
        }

        public string EpicBin
        {
            get
            { return _EpicBin; }
            set
            { _EpicBin = value; }
        }

        public string EpicJobOpr
        {
            get
            { return _EpicJobOpr; }
            set
            { _EpicJobOpr = value; }
        }

        public string EpicJobNum
        {
            get
            { return _EpicJobNum; }
            set
            { _EpicJobNum = value; }
        }

        public string EpicStatus
        {
            get
            { return _EpicStatus; }
            set
            { _EpicStatus = value; }
        }

        public int BtcPltMatCount
        {
            get
            { return objBtcPltMats.Count(); }

        }

        public List<clsBtcPltMat> mobjBtcPltMats
        {
            get
            { return objBtcPltMats; }
        }

        public void addBtcPltMat(string strBtcPltMat, decimal iBtcPltQty)
        {
            clsBtcPltMat objBtcPltMat = new clsBtcPltMat();
            objBtcPltMat.strBtcPltMatCode = strBtcPltMat;
            objBtcPltMat.strBtcPltMatUsage = iBtcPltQty;
            objBtcPltMat.strEpicorMatCode = strBtcPltMat;

            objBtcPltMats.Add(objBtcPltMat);
        }

        public void addError(clsError oError)
        {
            objErrors.Add(oError);
        }

        public List<clsError> mobjErrors
        {
            get
            { return objErrors; }
        }

        public string BtcPltBatchTime
        {
            get
            { return _BtcPltBatchTime; }
            set
            { _BtcPltBatchTime = value; }
        }

        public string BtcPltBatchLine
        {
            get
            { return _BtcPltBatchLine; }
            set
            { _BtcPltBatchLine = value; }
        }

    }
}
