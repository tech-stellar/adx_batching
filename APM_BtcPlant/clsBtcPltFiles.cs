using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ExcelDataReader;

namespace APM_BtcPlant
{
    public class clsBtcPltFiles
    {
        private List<clsBtcPltFileAttr> objBtcPltFileAttrs = new List<clsBtcPltFileAttr>();

        public int intBtcPltFileCount
        {
            get
            { return objBtcPltFileAttrs.Count(); }

        }

        public List<clsBtcPltFileAttr> mobjBtcPltFileAttrs
        {
            get
            { return objBtcPltFileAttrs; }
        }

        public void scanBtcPltFolder(string strBtcPltFolderPath)
        {
            if (Directory.Exists(strBtcPltFolderPath ))
            {
                string[] fileEntries = Directory.GetFiles(strBtcPltFolderPath);

                foreach (string fileName in fileEntries)
                {
                    addBtcPltFile(Path.GetFileName(fileName), fileName);
                }
            }
        }

        public void moveBtcPltFile(clsBtcPltFileAttr oBtcPltFileAttr, string strFileMoveTo)
        {
            if(Directory.Exists(strFileMoveTo))
            {
                try
                {
                    File.Copy(oBtcPltFileAttr.BtcPltFilePath, strFileMoveTo + "\\" + oBtcPltFileAttr.BtcPltFileName, true);
                    File.Delete(oBtcPltFileAttr.BtcPltFilePath);
                }
                catch (Exception ex)
                {

                }
            }
            
        }

        private void addBtcPltFile(string strFileName, string strFilePath)
        {
            clsBtcPltFileAttr objBtcPltFileAttr = new clsBtcPltFileAttr();
            objBtcPltFileAttr.BtcPltFileName = strFileName;
            objBtcPltFileAttr.BtcPltFilePath = strFilePath;

            objBtcPltFileAttrs.Add(objBtcPltFileAttr);
        }

        public void readBtcPltFile( clsBtcPltFileAttr oBtcPltFileAttr)
        {

            string[] fileLines = File.ReadAllLines(oBtcPltFileAttr.BtcPltFilePath);

            if (fileLines.Count() == 2) 
            {
                string[] LineOne = fileLines[0].Split(',');
                string[] LineTwo = fileLines[1].Split(',');

                int intMaxCol = LineOne.Count() - 1;
                int intItemColPost = LineOne.Count() - 3;
                int intTimeColPost = LineOne.Count() - 2;
                int intBtcLineColPost = LineOne.Count() -1;

                for (int i = 0; i <= intMaxCol; i++)
                {
                    switch (i)
                    {
                        case 0:
                            oBtcPltFileAttr.BtcPltDate = LineTwo[i].ToString();
                            break;
                        case 1:
                            oBtcPltFileAttr.BtcPltItemCode = LineTwo[i].ToString();
                            break;
                        case 2:
                            oBtcPltFileAttr.BtcPltPlantCode = LineTwo[i].ToString();
                            break;
                        case 3:
                            oBtcPltFileAttr.BtcPltDocketNum = LineTwo[i].ToString();
                            break;
                        case 4:
                            oBtcPltFileAttr.BtcPltItemQty = Convert.ToDecimal(LineTwo[i].ToString());
                            break;
                        default:
                            if (i == intItemColPost)
                            {
                               // do nothing about it 
                            }
                            else if (i == intTimeColPost)
                            {
                                oBtcPltFileAttr.BtcPltBatchTime = LineTwo[i].ToString();
                            }
                            else if (i == intBtcLineColPost)
                            {
                                oBtcPltFileAttr.BtcPltBatchLine = LineTwo[i].ToString();
                            }
                            else
                            {
                                if (!LineOne[i].Equals("") && Convert.ToDecimal(LineTwo[i].ToString()) > 0)
                                {
                                    oBtcPltFileAttr.addBtcPltMat(LineOne[i].ToString(), Convert.ToDecimal(LineTwo[i].ToString()));
                                }

                            }



                            break;
                    }

                }

            }

            //return objBtcFileProp;
        }


        public void readADXBtcPltFile(clsBtcPltFileAttr oBtcPltFiles, ref List<clsBtcPltFileAttr> oAdxBtcPltFiles)
        {
            //int iRowCount = 1;
            IExcelDataReader excelReader;

            FileStream stream = File.Open(oBtcPltFiles.BtcPltFilePath, FileMode.Open, FileAccess.Read);

            if (oBtcPltFiles.BtcPltFilePath.EndsWith(".xls"))
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            DataSet result = excelReader.AsDataSet();

            //iRowCount = excelReader.RowCount;

            for (var i = 1; i < result.Tables[0].Rows.Count; i++)
            {
                clsBtcPltFileAttr oAdxFile = new clsBtcPltFileAttr();
                oAdxFile.BtcPltFilePath = oBtcPltFiles.BtcPltFilePath;
                oAdxFile.BtcPltFileName = oBtcPltFiles.BtcPltFileName;

                for (var j = 0; j < result.Tables[0].Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            oAdxFile.BtcPltDate = result.Tables[0].Rows[i].ItemArray[j].ToString(); 
                            break;
                        case 2:
                            oAdxFile.BtcPltItemCode = result.Tables[0].Rows[i].ItemArray[j].ToString();
                            break;
                        case 1:
                            oAdxFile.BtcPltPlantCode = result.Tables[0].Rows[i].ItemArray[j].ToString();
                            break;
                        case 3:
                            oAdxFile.BtcPltDocketNum = result.Tables[0].Rows[i].ItemArray[j].ToString();
                            break;
                        case 4:
                            oAdxFile.BtcPltItemQty = Convert.ToDecimal(result.Tables[0].Rows[i].ItemArray[j].ToString());
                            break;
                        case 35:
                            break;
                        case 36:
                            oAdxFile.BtcPltBatchTime = result.Tables[0].Rows[i].ItemArray[j].ToString();
                            break;
                        case 37:
                            break;

                        default:
                            if (!result.Tables[0].Rows[0].ItemArray[j].Equals("") )
                            {
                                if (!String.IsNullOrEmpty(result.Tables[0].Rows[i].ItemArray[j].ToString()))
                                {
                                    oAdxFile.addBtcPltMat(result.Tables[0].Rows[0].ItemArray[j].ToString(), Convert.ToDecimal(result.Tables[0].Rows[i].ItemArray[j].ToString()));
                                }
                            }
                            break;
                    }
                }

                oAdxBtcPltFiles.Add(oAdxFile);
                //result.Tables[0].Rows[i].ItemArray[1].ToString();
            }




           
            /*
            string[] fileLines = File.ReadAllLines(oBtcPltFiles.BtcPltFilePath);

            if (fileLines.Length > 0)
            {

                string[] LineOne = fileLines[0].Split(',');

                int intMaxCol = LineOne.Count() - 1;

                // loop row
                for (int iRow = 1; iRow < fileLines.Length; iRow++)
                {
                    clsBtcPltFileAttr oAdxFile = new clsBtcPltFileAttr();

                    string[] LineData = fileLines[iRow].Split(',');
                    oAdxFile.BtcPltFilePath = oBtcPltFiles.BtcPltFilePath;
                    oAdxFile.BtcPltFileName = oBtcPltFiles.BtcPltFileName;

                    // loop column
                    for (int i = 0; i <= intMaxCol; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                oAdxFile.BtcPltDate = LineData[i].ToString();
                                break;
                            case 2:
                                oAdxFile.BtcPltItemCode = LineData[i].ToString();
                                break;
                            case 1:
                                oAdxFile.BtcPltPlantCode = LineData[i].ToString();
                                break;
                            case 3:
                                oAdxFile.BtcPltDocketNum = LineData[i].ToString();
                                break;
                            case 4:
                                oAdxFile.BtcPltItemQty = Convert.ToDecimal(LineData[i].ToString());
                                break;
                            case 35:
                                break;
                            case 36:
                                break;
                            case 37:
                                break;

                            default:
                                if (!LineOne[i].Equals("") && Convert.ToDecimal(LineData[i].ToString()) > 0)
                                {
                                    oAdxFile.addBtcPltMat(LineOne[i].ToString(), Convert.ToDecimal(LineData[i].ToString()));
                                }
                                break;
                        }
                    }

                    oAdxBtcPltFiles.Add(oAdxFile);

                }

            }

            */

                //return objBtcFileProp;
        }



        public bool checkIsFileContentEmpty(clsBtcPltFileAttr oBtcPltFileAttr)
        {
            bool isEmptyFile =false;

            if (new FileInfo(oBtcPltFileAttr.BtcPltFilePath).Length == 0)
            {
                // file is empty
                isEmptyFile = true;            
                    }
           else
            {
                // there is something in it
                isEmptyFile = false;
            }

            return isEmptyFile;
       }

        public Boolean checkCompany(clsAppConfigs oConfig)
        {

            try
            {
                Boolean boolChecked = true;

                string strSql = "select CheckBox10 from Company where Company = 'APM'";
                boolChecked = returnBoolData(oConfig.strEpicorDB, strSql);

                DataTable dtBtcPlt = new DataTable();

                return boolChecked;

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                return false;
            }
        }

        public void updateCompany(Boolean boolChecked, clsAppConfigs oConfig)
        {

            try
            {
                string strSql = "update Company set CheckBox10 = " + (boolChecked?"1":"0") + " where Company = 'APM'";

                using (SqlConnection sqlConn = new SqlConnection(oConfig.strEpicorDB))
                {
                    SqlCommand sqlCmd = new SqlCommand(strSql, sqlConn);
                    sqlCmd.Connection.Open();
                    sqlCmd.ExecuteNonQuery();
                    
                    sqlCmd.Dispose();
                    sqlConn.Dispose();
                }

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();
            }
        }

        public Boolean mapBtcPltWithEpic(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {

            try
            {
                Boolean boolError;

                string strSql = String.Format("select top 1 ShortChar01,ShortChar02,ShortChar03,ShortChar04,ShortChar05 from ice.UD37 where key1 = '{0}'", oBtcPltFileAttr.BtcPltPlantCode);
                DataTable dtBtcPlt = new DataTable();

                dtBtcPlt = returnDataTable(oConfig.strEpicorDB, strSql);

                if (dtBtcPlt.Rows.Count != 0)
                {
                    foreach (DataRow row in dtBtcPlt.Rows)
                    {
                        oBtcPltFileAttr.EpicCompany = row["ShortChar01"].ToString();
                        oBtcPltFileAttr.EpicWarehouse = row["ShortChar03"].ToString();
                        oBtcPltFileAttr.EpicPlant = row["ShortChar02"].ToString();
                        oBtcPltFileAttr.EpicBin = row["ShortChar04"].ToString();
                        oBtcPltFileAttr.EpicJobOpr = row["ShortChar05"].ToString();
                    }

                    boolError = false;
                }

                else
                {
                    string strError = String.Format("Unable to find setup for mapping plant {0}.", oBtcPltFileAttr.BtcPltPlantCode);

                    clsEventLogger objELogger = new clsEventLogger();
                    objELogger.beginLogging(strError, EventLogEntryType.Error);
                    objELogger.Dispose();

                    clsError oError = new clsError();
                    oError.ErrDescription = strError;
                    oBtcPltFileAttr.addError(oError);

                    boolError = true;
                }

                return (boolError ? false : true);

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                return false;
            }
        }

        public Boolean mapBtcPltFGWithEpic(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {
            try
            {
                    int intErrorCount = 0;
                    string strSql = String.Format("select top 1 shortchar01 from ice.UD36 where key1 = '{0}'", oBtcPltFileAttr.BtcPltItemCode);
                    string strItem = returnStringData(oConfig.strEpicorDB, strSql);


                    if (strItem.ToString() != "")
                    {
                        oBtcPltFileAttr.EpicItemCode = strItem;                        
                    }
                    else
                    {
                        string strError = String.Format("Unable to find setup for mapping FG code {0}.", oBtcPltFileAttr.BtcPltItemCode);

                        clsError oError = new clsError();
                        oError.ErrDescription = strError;
                        oBtcPltFileAttr.addError(oError);

                        intErrorCount++;
                    }

                    return (intErrorCount > 0 ? false : true);
                
            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                return false;
            }
        }

        public Boolean mapBtcPltMaterialWithEpic(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {
            try
            {
                    int intErrorCount = 0;

                    foreach (clsBtcPltMat oBtcMat in oBtcPltFileAttr.mobjBtcPltMats)
                    {
                        
                        string strSql = String.Format("select top 1 shortchar01 from ice.UD36 where key1 = '{0}'", oBtcMat.strBtcPltMatCode);
                        DataTable dtBtcPltMat = new DataTable();

                        dtBtcPltMat = returnDataTable(oConfig.strEpicorDB, strSql);

                        if (dtBtcPltMat.Rows.Count != 0)
                        {
                            foreach (DataRow row in dtBtcPltMat.Rows)
                            {
                                oBtcMat.strEpicorMatCode = row["shortchar01"].ToString();
                            }
                            
                        }
                        else
                        {
                            string strError = String.Format("Unable to find setup for mapping material code {0}.", oBtcMat.strBtcPltMatCode);

                            clsError oError = new clsError();
                            oError.ErrDescription = strError;
                            oBtcPltFileAttr.addError(oError);

                            intErrorCount ++;
                        }

                    }

                    return (intErrorCount > 0 ? false : true);

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                return false;
            }
        }

        public Boolean loadPartInfoForFG(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {
            try
            {
                Boolean boolError;
                string strSql = String.Format("select top 1 partdescription, ium from erp.Part where company = '{0}' and partnum = '{1}'", oBtcPltFileAttr.EpicCompany,oBtcPltFileAttr.EpicItemCode);
                DataTable dtPart = new DataTable();

                dtPart = returnDataTable(oConfig.strEpicorDB, strSql);

                if (dtPart.Rows.Count != 0)
                {
                    foreach (DataRow row in dtPart.Rows)
                    {
                        oBtcPltFileAttr.EpicItemDesc = row["partdescription"].ToString();
                        oBtcPltFileAttr.EpicItemUOM = row["ium"].ToString();
                    }

                    boolError = false;
                }

                else
                {
                    string strError = String.Format("Unable to find details for item {0} in Epicor.", oBtcPltFileAttr.EpicItemCode);

                    clsError oError = new clsError();
                    oError.ErrDescription = strError;
                    oBtcPltFileAttr.addError(oError);

                    boolError = true;
                }

                return (boolError ? false : true);

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                return false;
            }

        }

        public Boolean loadPartInfoForMaterial(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {
            try
            {
                    int intErrorCount = 0;

                    foreach (clsBtcPltMat oBtcMat in oBtcPltFileAttr.mobjBtcPltMats)
                    {

                        string strSql = String.Format("select top 1 partdescription, ium from erp.Part where company = '{0}' and partnum = '{1}'",oBtcPltFileAttr.EpicCompany, oBtcMat.strEpicorMatCode);
                        DataTable dtPart = new DataTable();

                        dtPart = returnDataTable(oConfig.strEpicorDB, strSql);

                        if (dtPart.Rows.Count != 0)
                        {
                            foreach (DataRow row in dtPart.Rows)
                            {
                                oBtcMat.strEpicorMatDesc = row["partdescription"].ToString();
                                oBtcMat.strEpicorMatUOM = row["ium"].ToString();
                            }

                        }
                        else
                        {
                            string strError = String.Format("Unable to find part for code {0}.", oBtcMat.strEpicorMatCode);

                            clsError oError = new clsError();
                            oError.ErrDescription = strError;
                            oBtcPltFileAttr.addError(oError);

                            intErrorCount++;
                        }

                    }

                    return (intErrorCount > 0 ? false : true);

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                return false;
            }

        }

        public Boolean chkMaterialOnHandQty(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {
            try
            {

                    int intErrorCount = 0;
                    decimal qtyOnHand = 0;
                    string strNegQtyAction = "";
                    string strSql = string.Empty;
                    DataTable dtBtcPlt = new DataTable();
                    string strCompany = string.Empty;
                    string strWarehouse = string.Empty;
                    string strPlant = string.Empty;
                    string strBinNum = string.Empty;
                    string strJobOpr = string.Empty;

                    strSql = "select top 1 ShortChar01,ShortChar02,ShortChar03,ShortChar04,ShortChar05 from ice.UD37 where key1 = '{0}'";
                    strSql = string.Format(strSql, "ADSB");

                    dtBtcPlt = returnDataTable(oConfig.strEpicorDB, strSql);

                if (dtBtcPlt.Rows.Count != 0)
                {
                    foreach (DataRow row in dtBtcPlt.Rows)
                    {
                        strCompany = row["ShortChar01"].ToString();
                        strWarehouse = row["ShortChar03"].ToString();
                        strPlant = row["ShortChar02"].ToString();
                        strBinNum = row["ShortChar04"].ToString();
                        strJobOpr = row["ShortChar05"].ToString();
                    }
                }
                else
                {
                    clsError oError = new clsError();
                    oError.ErrDescription = string.Format("Unable to find mapping for the ADSB plant");
                    oBtcPltFileAttr.addError(oError);

                    intErrorCount++;
                }


                foreach (clsBtcPltMat oBtcMat in oBtcPltFileAttr.mobjBtcPltMats)
                    {
                        strSql = "select top 1 isnull(c.NegQtyAction,'None') ";
                        strSql += "from erp.Part p left join erp.PartClass c on p.Company = c.Company and p.ClassID = c.ClassID ";
                        strSql += "where p.Company = '{0}' and p.partnum = '{1}' ";
                        strSql = String.Format(strSql, oBtcPltFileAttr.EpicCompany, oBtcMat.strEpicorMatCode);

                        strNegQtyAction = returnStringData(oConfig.strEpicorDB, strSql);

                        if (String.IsNullOrEmpty(strNegQtyAction))
                        {
                            strNegQtyAction = "None";
                        }

                        strSql = String.Format("select isnull(sum(onhandqty),0) as qty from erp.PartBin where company = '{0}' and warehousecode='{1}' and binnum = '{2}' and partnum = '{3}'", oBtcPltFileAttr.EpicCompany, oBtcPltFileAttr.EpicWarehouse, oBtcPltFileAttr.EpicBin, oBtcMat.strEpicorMatCode);

                        qtyOnHand = returnDecimalData(oConfig.strEpicorDB, strSql);

                        if (qtyOnHand < oBtcMat.strBtcPltMatUsage)
                        {
                            if (strNegQtyAction.ToUpper() == "STOP")
                            {
                                clsError oError = new clsError();
                                oError.ErrDescription = string.Format("Insufficient quantity on hand for material {0}", oBtcMat.strEpicorMatCode);
                                oBtcPltFileAttr.addError(oError);

                                intErrorCount++;
                            }
                        }



                        strSql = "select top 1 isnull(c.NegQtyAction,'None') ";
                        strSql += "from erp.Part p left join erp.PartClass c on p.Company = c.Company and p.ClassID = c.ClassID ";
                        strSql += "where p.Company = '{0}' and p.partnum = '{1}' ";
                        strSql = String.Format(strSql, strCompany, oBtcMat.strEpicorMatCode);

                        strNegQtyAction =  returnStringData(oConfig.strEpicorDB, strSql);

                        if (String.IsNullOrEmpty(strNegQtyAction))
                        {
                        strNegQtyAction = "None";
                        }


                        strSql = String.Format("select isnull(sum(onhandqty),0) as qty from erp.PartBin where company = '{0}' and warehousecode='{1}' and binnum = '{2}' and partnum = '{3}'", strCompany, strWarehouse, strBinNum, oBtcMat.strEpicorMatCode);

                        qtyOnHand = returnDecimalData(oConfig.strEpicorDB, strSql);

                        if (qtyOnHand < oBtcMat.strBtcPltMatUsage)
                        {
                            if (strNegQtyAction.ToUpper() == "STOP")
                            {
                                clsError oError = new clsError();
                                oError.ErrDescription = string.Format("Insufficient quantity on hand for material {0}", oBtcMat.strEpicorMatCode);
                                oBtcPltFileAttr.addError(oError);

                                intErrorCount++;
                            }
                        }

                    }
                

                    return (intErrorCount > 0 ? false : true);


            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                return false;
            }

        }

        public void genJobNum(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {
            try
            {
                string strTempJobNum = "B" + DateTime.Now.ToString("yyyyMMdd");
                string strLastJobNum = "";
                string strSql = String.Format("select isnull(max(jobnum),'') from erp.jobhead where company = '{0}' and jobnum like '{1}%'", oBtcPltFileAttr.EpicCompany, strTempJobNum);

                using (SqlConnection sqlConn = new SqlConnection(oConfig.strEpicorDB))
                {
                    SqlCommand sqlCmd = new SqlCommand(strSql, sqlConn);
                    sqlCmd.Connection.Open();

                    strLastJobNum = (string)sqlCmd.ExecuteScalar();

                    if (strLastJobNum.Equals(""))
                    { strTempJobNum = strTempJobNum + string.Format("{0:0000}", 1); }
                    else
                    { 
                        int iLastNum = int.Parse(  strLastJobNum.Substring(9));
                        strTempJobNum = strTempJobNum + string.Format("{0:0000}", iLastNum + 1);
                    }

                    oBtcPltFileAttr.EpicJobNum = strTempJobNum;

                    sqlCmd.Dispose();
                    sqlConn.Dispose();

                }

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

            }

        }

        public Boolean verifyDuplicateBtcPltFile(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {
            try
            {
                int iRowCount = 0;
                string strSql = String.Format("select isnull(count(1),0) from ice.UD38 where Character04 = '{0}' and ShortChar02 ='Success' ", oBtcPltFileAttr.BtcPltFileName);

                using (SqlConnection sqlConn = new SqlConnection(oConfig.strEpicorDB))
                {
                    SqlCommand sqlCmd = new SqlCommand(strSql, sqlConn);
                    sqlCmd.Connection.Open();

                    iRowCount = (int)sqlCmd.ExecuteScalar();

                    sqlCmd.Dispose();
                    sqlConn.Dispose();

                }

                if (iRowCount == 0)
                { return true; }
                else
                {
                    clsError oError = new clsError();
                    oError.ErrDescription = string.Format("Error. File: {0} has been imported into the Epicor before.", oBtcPltFileAttr.BtcPltFileName);
                    oBtcPltFileAttr.addError(oError);

                    return false;
                }


            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();
                return false;
            }

        
        }

        public void genProcessID(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {
            try
            {
                string strTempProcessID = "P" + DateTime.Now.ToString("yyyyMMdd") + "-";
                string strLastProcessID = "";
                string strSql = String.Format("select isnull(max(key1),'') from ice.UD38 where key1 like '{0}%'",  strTempProcessID);

                using (SqlConnection sqlConn = new SqlConnection(oConfig.strEpicorDB))
                {
                    SqlCommand sqlCmd = new SqlCommand(strSql, sqlConn);
                    sqlCmd.Connection.Open();

                    strLastProcessID = (string)sqlCmd.ExecuteScalar();

                    if (strLastProcessID.Equals(""))
                    { strTempProcessID = strTempProcessID + string.Format("{0:00000}", 1); }
                    else
                    {
                        int iLastNum = int.Parse(strLastProcessID.Substring(10, 5).ToString());
                        strTempProcessID = strTempProcessID + string.Format("{0:00000}", iLastNum + 1);
                    }

                    oBtcPltFileAttr.ProcessID = strTempProcessID;

                    sqlCmd.Dispose();
                    sqlConn.Dispose();

                }

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

            }
        
        }

        public Boolean loadPackingMaterial(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfig)
        {
            try
            {
                int intErrorCount = 0;
                string strMaterial = "";
                int iRow = 0;

                foreach (clsBtcPltMat oBtcMat in oBtcPltFileAttr.mobjBtcPltMats)
                {
                    if (iRow == 0)
                    { strMaterial = "'" + oBtcMat.strBtcPltMatCode + "'" ; }
                    else
                    { strMaterial += ",'" + oBtcMat.strBtcPltMatCode + "'";  }

                    iRow++;
                }

                string strSql = "select pm.PartNum, pm.RevisionNum, pm.MtlPartNum, p.PartDescription, pm.UOMCode, pm.QtyPer ";
                strSql += "from erp.partmtl pm ";
                strSql += "inner join erp.part p on pm.Company = p.Company and pm.MtlPartNum = p.PartNum ";
                strSql += "where pm.partnum = '" + oBtcPltFileAttr.EpicItemCode + "' ";
                strSql += "and pm.RevisionNum = (select top 1 pr.RevisionNum from erp.PartRev pr where pr.partnum = '" + oBtcPltFileAttr.EpicItemCode + "') ";
                strSql += "and pm.MtlPartNum not in (" + strMaterial + ")";

                DataTable dtPackMaterial = new DataTable();
                dtPackMaterial = returnDataTable(oConfig.strEpicorDB, strSql);

                if (dtPackMaterial.Rows.Count != 0)
                {
                    foreach (DataRow row in dtPackMaterial.Rows)
                    {
                        oBtcPltFileAttr.addBtcPltMat(row["MtlPartNum"].ToString(), Math.Round(oBtcPltFileAttr.BtcPltItemQty * (decimal)row["QtyPer"],6));
                    }
                }


                return (intErrorCount > 0 ? false : true);

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                return false;
            }



        }

        public DataTable returnDataTable(string strConnectionString, string strSqlText)
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection sqlConn = new SqlConnection(strConnectionString))
            {
                SqlCommand sqlCmd = new SqlCommand(strSqlText, sqlConn);
                sqlCmd.Connection.Open();

                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = sqlCmd;
                sqlAdapter.Fill(dtResult);

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

            }

            return dtResult;
        }

        public string returnStringData(string strConnectionString, string strSqlText)
        {
            string strResult = string.Empty;

            using (SqlConnection sqlConn = new SqlConnection(strConnectionString))
            {
                SqlCommand sqlCmd = new SqlCommand(strSqlText, sqlConn);
                sqlCmd.Connection.Open();

                strResult = (string)sqlCmd.ExecuteScalar();

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

            }

            return strResult;

        }

        public decimal returnDecimalData(string strConnectionString, string strSqlText)
        {
            decimal decResult = 0M;

            using (SqlConnection sqlConn = new SqlConnection(strConnectionString))
            {
                SqlCommand sqlCmd = new SqlCommand(strSqlText, sqlConn);
                sqlCmd.Connection.Open();

                decResult = (decimal)sqlCmd.ExecuteScalar();

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

            }

            return decResult;

        }

        public bool returnBoolData(string strConnectionString, string strSqlText)
        {
            bool blResult = false;

            using (SqlConnection sqlConn = new SqlConnection(strConnectionString))
            {
                SqlCommand sqlCmd = new SqlCommand(strSqlText, sqlConn);
                sqlCmd.Connection.Open();

                blResult = (bool)sqlCmd.ExecuteScalar();

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

            }

            return blResult;

        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
