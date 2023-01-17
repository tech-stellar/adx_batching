using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Diagnostics;

using Ice.Core;
using Erp.Common;
using Ice.Contracts;
using Ice.Adapters;
using Erp.BO;
using Erp.Adapters;
using Ice.Lib.Framework;

using System.Data;
using System.Data.SqlClient;

namespace APM_BtcPlant
{
    public class clsEpicor
    {

        public Boolean dumpToLog_EpicorUD38(clsBtcPltFileAttr oBtcPltFileAttr, clsAppConfigs oConfigs)
        {
            try {

                bool IsError;

                Session oEpicSession = new Session(oConfigs.strEpicUser, oConfigs.strEpicPass, oConfigs.strEpicURL, Session.LicenseType.Default, oConfigs.strEpicConfig,false,oBtcPltFileAttr.EpicCompany,oBtcPltFileAttr.EpicPlant);
                ILauncher oEpicLaunch = new ILauncher(oEpicSession);

                if (oEpicSession != null)
                {
                    //ILauncher EpiLaunch = new ILauncher(oEpicSession);
                    UD38Adapter adpUD38 = new UD38Adapter(oEpicLaunch);

                    adpUD38.BOConnect();

                    if (adpUD38 != null)
                    {
                        if (adpUD38.GetaNewUD38())
                        {
                            // set the key1 as primary key
                            adpUD38.UD38Data.Tables[0].Rows[0]["Key1"] = oBtcPltFileAttr.ProcessID;

                            // write the process date to log
                            adpUD38.UD38Data.Tables[0].Rows[0]["Date01"] = DateTime.Now.ToString("yyyy-MM-dd");

                            // write the status to log
                            adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar01"] = oBtcPltFileAttr.EpicJobNum.ToString() ;
                            adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar02"] = oBtcPltFileAttr.EpicStatus;
                            adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar03"] = oBtcPltFileAttr.BtcPltPlantCode;

                            //write the file content to log
                            string strBtcPltFile = "File : " + oBtcPltFileAttr.BtcPltFilePath;
                            adpUD38.UD38Data.Tables[0].Rows[0]["Character01"] = strBtcPltFile;

                            string strBtcPltFileContent = string.Format("Part Num = {0}", oBtcPltFileAttr.BtcPltItemCode);
                            foreach (clsBtcPltMat oBtcPltMat in oBtcPltFileAttr.mobjBtcPltMats)
                            {
                                strBtcPltFileContent = strBtcPltFileContent + Environment.NewLine + string.Format("Material = {0} ;Usage = {1}",oBtcPltMat.strBtcPltMatCode,oBtcPltMat.strBtcPltMatUsage);
                            }

                            adpUD38.UD38Data.Tables[0].Rows[0]["Character02"] = strBtcPltFileContent;

                            // write error to log
                            string strErr = "";
                            foreach (clsError oError in oBtcPltFileAttr.mobjErrors)
                            {
                                strErr = strErr + oError.ErrDescription + Environment.NewLine ;
                            }

                            adpUD38.UD38Data.Tables[0].Rows[0]["Character03"] = strErr;
                            adpUD38.UD38Data.Tables[0].Rows[0]["Character04"] = oBtcPltFileAttr.BtcPltFileName;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar05"] = oBtcPltFileProp.strMatCode05;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar06"] = oBtcPltFileProp.strMatCode06;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar07"] = oBtcPltFileProp.strMatCode07;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar08"] = oBtcPltFileProp.strMatCode08;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar09"] = oBtcPltFileProp.strMatCode09;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar10"] = oBtcPltFileProp.strMatCode10;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar11"] = oBtcPltFileProp.strMatCode11;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar12"] = oBtcPltFileProp.strMatCode12;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar13"] = oBtcPltFileProp.strMatCode13;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar14"] = oBtcPltFileProp.strMatCode14;
                            //adpUD38.UD38Data.Tables[0].Rows[0]["ShortChar15"] = oBtcPltFileProp.strMatCode15;

                            adpUD38.Update();
                        }
                    }


                    adpUD38.Dispose();

                    _DeleteEpicorSession(oConfigs, oEpicSession.SessionID);

                    IsError =false;
                }
                else
                {
                    clsEventLogger objELogger = new clsEventLogger();
                    objELogger.beginLogging("Unable connect to Epicor", EventLogEntryType.Error);
                    objELogger.Dispose();

                    IsError=true;
                }

                oEpicLaunch = null;

                

                oEpicSession.Dispose();
                oEpicSession = null;



                return IsError;

            }
            catch (Exception e)
            {

                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                return false;
            }
        }

        public Boolean createJobEntry(clsBtcPltFileAttr objBtcPltFileAttr, clsAppConfigs oConfigs)
        {
            Boolean _IsComplete = false;

            Session oEpicSession = new Session(oConfigs.strEpicUser, oConfigs.strEpicPass, oConfigs.strEpicURL, Session.LicenseType.Default, oConfigs.strEpicConfig, false, objBtcPltFileAttr.EpicCompany, objBtcPltFileAttr.EpicPlant);
            ILauncher oEpicLaunch = new ILauncher(oEpicSession);


            try
            {
                string strJob = objBtcPltFileAttr.EpicJobNum;
                string strPlant = objBtcPltFileAttr.EpicPlant;
                string strPart = objBtcPltFileAttr.EpicItemCode;
                string strPartDesc = objBtcPltFileAttr.EpicItemDesc;
                string strPartUOM = objBtcPltFileAttr.EpicItemUOM;
                

                //Session oEpicSession = new Session(oConfigs.strEpicUser, oConfigs.strEpicPass, oConfigs.strEpicURL, Session.LicenseType.Default, oConfigs.strEpicConfig,false, objBtcPltFileAttr.EpicCompany, objBtcPltFileAttr.EpicPlant);
                //ILauncher oEpicLaunch = new ILauncher(oEpicSession);

                if (oEpicSession != null)
                {
                    JobEntryAdapter adpJobEntry = new JobEntryAdapter(oEpicLaunch);
                    adpJobEntry.BOConnect();

                    if (adpJobEntry != null)
                    {
                        if (adpJobEntry.GetNewJobHead())
                        {
                            DataRow dr = adpJobEntry.JobEntryData.JobHead.Rows[0];
                            dr.BeginEdit();

                            dr["JobNum"] = strJob;
                            dr["JobType"] = "MFG";
                            dr["SchedCode"] = "NORMAL";
                            dr["Plant"] = strPlant;
                            dr["PartNum"] = strPart;
                            dr["PartDescription"] = strPartDesc;
                            dr["IUM"] = strPartUOM;
                            dr["CommentText"] = objBtcPltFileAttr.BtcPltDocketNum;
                            dr["UserChar1"] = objBtcPltFileAttr.BtcPltPlantCode;
                            dr["BatchLine_c"] = objBtcPltFileAttr.BtcPltBatchLine;
                            dr["BatchTime_c"] = Convert.ToDateTime(objBtcPltFileAttr.BtcPltBatchTime); //objBtcPltFileAttr.BtcPltDate.Substring(4, 4) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(2, 2) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(0, 2) + " " + objBtcPltFileAttr.BtcPltBatchTime; // objBtcPltFileAttr.BtcPltBatchTime;

                            dr.EndEdit();

                            adpJobEntry.Update();
                        }

                        if (adpJobEntry.GetByID(strJob))
                        {
                            if (adpJobEntry.GetNewJobOper(strJob, 0))
                            {
                                DataRow dr = adpJobEntry.JobEntryData.JobOper.Rows[0];
                                dr.BeginEdit();

                                dr["JobNum"] = strJob;
                                dr["AssemblySeq"] = 0;
                                dr["OprSeq"] = 10;

                                dr["PartNum"] = strPart;
                                dr["Description"] = strPartDesc;
                                dr["IUM"] = strPartUOM;
                                dr["OpCode"] = objBtcPltFileAttr.EpicJobOpr;
                                dr["OpDesc"] = "Batching";
                                dr["LaborEntryMethod"] = "Q";
                                dr.EndEdit();

                                adpJobEntry.Update();
                            
                            }

                            int iMatSeq = 10;
                            int iRowSeq = 0;

                            foreach (clsBtcPltMat oBtcMat in objBtcPltFileAttr.mobjBtcPltMats)
                            {
                                if (adpJobEntry.GetNewJobMtl(strJob, 0))
                                {
                                    DataRow dr = adpJobEntry.JobEntryData.JobMtl.Rows[iRowSeq];
                                    dr.BeginEdit();

                                    dr["JobNum"] = strJob;
                                    dr["AssemblySeq"] = 0;

                                    dr["MtlSeq"] = iMatSeq;
                                    dr["PartNum"] = oBtcMat.strEpicorMatCode;
                                    dr["Description"] = oBtcMat.strEpicorMatDesc;
                                    dr["IUM"] = oBtcMat.strEpicorMatUOM;
                                    dr["RelatedOperation"] = 10;
                                    dr["Plant"] = strPlant;
                                    dr["WarehouseCode"] = objBtcPltFileAttr.EpicWarehouse;
                                    dr["FixedQty"] = true;
                                    dr["QtyPer"] = (oBtcMat.strEpicorMatUOM == "PCS" ? Math.Round((oBtcMat.strBtcPltMatUsage / objBtcPltFileAttr.BtcPltItemQty), 2) : oBtcMat.strBtcPltMatUsage / objBtcPltFileAttr.BtcPltItemQty); 
                                    
                                    oBtcMat.strEpicorMatSeq = iMatSeq;

                                    dr.EndEdit();
                                    adpJobEntry.Update();

                                    iMatSeq = iMatSeq + 10;
                                    iRowSeq = iRowSeq + 1;
                                }
                                                        
                            }

                            if (adpJobEntry.GetNewJobProd(strJob, strPart, 0, 0, 0, "", "", 0))
                            {
                                DataRow dr = adpJobEntry.JobEntryData.JobProd.Rows[0];
                                dr.BeginEdit();
                                dr["JobNum"] = strJob;
                                dr["PartNum"] = strPart;
                                dr["IUM"] = strPartUOM; 
                                dr["DemandLinkSource"] = "Whse ";
                                dr["WarehouseCode"] = objBtcPltFileAttr.EpicWarehouse; 
                                dr["MakeToType"] = "STOCK";
                                dr["MakeToStockQty"] = objBtcPltFileAttr.BtcPltItemQty;
                                dr.EndEdit();
                                adpJobEntry.Update();

                            }

                            DataRow drow = adpJobEntry.JobEntryData.JobHead.Rows[0];
                            drow.BeginEdit();
                            drow["ReqDueDate"] = objBtcPltFileAttr.BtcPltDate; // objBtcPltFileAttr.BtcPltDate.Substring(4, 4) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(2, 2) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(0, 2);//DateTime.Now.ToString("yyyy-MM-dd"); 
                            drow["DueDate"] = objBtcPltFileAttr.BtcPltDate; // objBtcPltFileAttr.BtcPltDate.Substring(4, 4) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(2, 2) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(0, 2);// DateTime.Now.ToString("yyyy-MM-dd"); 
                            drow["JobEngineered"] = true;
                            drow["JobReleased"] = true;
                            drow["dspReadyCostProcess"] = true;
                            drow["ChangeDescription"] = "Concrete Batching";
                            drow.EndEdit();
                            adpJobEntry.Update();
                        }

                    }

                    adpJobEntry.Dispose();
                    adpJobEntry = null;


                    IssueReturnAdapter adpIssue = new IssueReturnAdapter(oEpicLaunch);
                    adpIssue.BOConnect();

                    if (adpIssue != null)
                    {
                        string strMsg;
                        bool reqInput;
                        string lgl;
                        string partTran;

                        foreach (clsBtcPltMat oBtcMat in objBtcPltFileAttr.mobjBtcPltMats)
                        {
                            if (adpIssue.GetNewIssueReturnToJob(strJob, 0, "STK-MTL", Guid.NewGuid(), out strMsg))
                            {
                                IssueReturnDataSet.IssueReturnRow row = adpIssue.IssueReturnData.IssueReturn[0];
                                
                                row.BeginEdit();

                                row.Plant = objBtcPltFileAttr.EpicPlant;
                                row.FromWarehouseCode = objBtcPltFileAttr.EpicWarehouse;
                                row.FromBinNum = objBtcPltFileAttr.EpicBin;
                                row.PartNum = oBtcMat.strEpicorMatCode;
                                row.PartIUM = oBtcMat.strEpicorMatUOM;
                                row.TranQty = (oBtcMat.strEpicorMatUOM == "PCS" ? Math.Round(oBtcMat.strBtcPltMatUsage,2) : oBtcMat.strBtcPltMatUsage);
                                row.TranDate = Convert.ToDateTime(objBtcPltFileAttr.BtcPltDate); // Convert.ToDateTime( objBtcPltFileAttr.BtcPltDate.Substring(4, 4) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(2, 2) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(0, 2));// DateTime.Now.ToString("yyyy-MM-dd"); 
                                
                                row.ToWarehouseCode = objBtcPltFileAttr.EpicWarehouse;
                                row.ToBinNum = objBtcPltFileAttr.EpicBin;
                                row.ToJobNum = strJob;

                                row.ToJobPartNum = strPart;
                                row.ToJobSeqPartNum = oBtcMat.strEpicorMatCode;
                                row.ToJobSeq = oBtcMat.strEpicorMatSeq;

                               
                                row.DimCode = oBtcMat.strEpicorMatUOM;
                                row.DUM = oBtcMat.strEpicorMatUOM;
                                row.UM = oBtcMat.strEpicorMatUOM;

                                row.RowMod = "U";

                                row.EndEdit();

                                adpIssue.PrePerformMaterialMovement(out reqInput);
                                adpIssue.PerformMaterialMovement(true, out lgl, out partTran);
                            }
                        }

                    }
                    
                    adpIssue.Dispose();
                    adpIssue = null;

                    ReceiptsFromMfgAdapter adpRecv = new ReceiptsFromMfgAdapter(oEpicLaunch);
                    adpRecv.BOConnect();

                    if (adpRecv != null)
                    {
                        if (adpRecv.GetNewReceiptsFromMfgJobAsm(strJob, 0, "MFG-STK", "RcptToInvEntry"))
                        {
                            ReceiptsFromMfgDataSet.PartTranRow x = adpRecv.ReceiptsFromMfgData.PartTran[0];
                            x.BeginEdit();
                            x["ActTranQty"] = objBtcPltFileAttr.BtcPltItemQty;
                            x["TranQty"] = objBtcPltFileAttr.BtcPltItemQty;
                            x["ThisTranQty"] = objBtcPltFileAttr.BtcPltItemQty;
                            x["InventoryTrans"] = true;
                            x["EmpID"] = 105;
                            x["CostID"] = 1;
                            x["WareHouseCode"] = objBtcPltFileAttr.EpicWarehouse;
                            x["BinNum"] = objBtcPltFileAttr.EpicBin;
                            x["WareHouse2"] = objBtcPltFileAttr.EpicWarehouse;
                            x["BinNum2"] = objBtcPltFileAttr.EpicBin;
                            x["RowMod"] = "U";
                            x["TranDate"] = objBtcPltFileAttr.BtcPltDate; // objBtcPltFileAttr.BtcPltDate.Substring(4, 4) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(2, 2) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(0, 2);
                            x.EndEdit();
                            
                        }

                        bool reqInput;
                        string lgl;
                        string partTran;
                        string strMsg;
                        adpRecv.OnChangeWareHouseCode("RcptToInvEntry");
                        adpRecv.OnChangeActTranQty(out strMsg);
                        adpRecv.PreUpdate(out reqInput);
                        adpRecv.ReceiveMfgPartToInventory( 0, true, out lgl, out partTran, "RcptToInvEntry");

                    }

                    adpRecv.Dispose();
                    adpRecv = null;

                    // close the job
                    JobClosingAdapter adpClose = new JobClosingAdapter(oEpicLaunch);
                    adpClose.BOConnect();

                    if (adpClose != null)
                    {
                  
                        bool reqInput = false;
                        string strMsg;

                        adpClose.GetNewJobClosing();
                        adpClose.OnChangeJobNum(strJob,out strMsg);
                        JobClosingDataSet.JobClosingRow x = adpClose.JobClosingData.JobClosing[0];
                        x.BeginEdit();
                        
                        x["JobComplete"] = true;
                        x["JobCompletionDate"] = objBtcPltFileAttr.BtcPltDate; // objBtcPltFileAttr.BtcPltDate.Substring(4, 4) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(2, 2) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(0, 2);//DateTime.Now.ToString("yyyy-MM-dd")
                        x["JobClosed"] = true;
                        x["ClosedDate"] = objBtcPltFileAttr.BtcPltDate; //objBtcPltFileAttr.BtcPltDate.Substring(4, 4) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(2, 2) + "-" + objBtcPltFileAttr.BtcPltDate.Substring(0, 2);//DateTime.Now.ToString("yyyy-MM-dd")
                        x["RowMod"] = "U";
                        x["QuantityContinue"] = 1;
                        x.EndEdit();


                        adpClose.OnChangeJobCompletion();
                        adpClose.OnChangeJobClosed();

                        adpClose.PreCloseJob(out reqInput);
                        adpClose.CloseJob(out strMsg);
                    }

                    adpClose.Dispose();
                    adpClose = null;


                    // add codes here for delete session
                    _DeleteEpicorSession(oConfigs, oEpicSession.SessionID);


                    _IsComplete =  true;
                }
                else
                {

                    clsError oError = new clsError();
                    oError.ErrDescription = string.Format("Unable connect to Epicor");
                    objBtcPltFileAttr.addError(oError);

                    _DeleteEpicorSession(oConfigs, oEpicSession.SessionID);

                    _IsComplete = false;
                }

                oEpicLaunch = null;

                oEpicSession.Dispose();
                oEpicSession = null;

            }
            catch (Exception e)
            {
                clsEventLogger objELogger = new clsEventLogger();
                objELogger.beginLogging(e.Message, EventLogEntryType.Error);
                objELogger.Dispose();

                clsError oError = new clsError();
                oError.ErrDescription = string.Format(e.Message);
                objBtcPltFileAttr.addError(oError);

                _DeleteEpicorSession(oConfigs, oEpicSession.SessionID);

                _IsComplete = false;            
            }


            return _IsComplete;


        }



        public Boolean quantityAdjustment(clsBtcPltFileAttr objBtcPltFileAttr, clsAppConfigs oConfigs)
        {
            Boolean _IsComplete = false;
            DataTable dtBtcPlt = new DataTable();

            string strCompany=string.Empty;
            string strWarehouse = string.Empty;
            string strPlant = string.Empty;
            string strBinNum = string.Empty;
            string strJobOpr = string.Empty;

            string strSql = "select top 1 ShortChar01,ShortChar02,ShortChar03,ShortChar04,ShortChar05 from ice.UD37 where key1 = '{0}'";
            strSql = string.Format(strSql, "ADSB");


            using (SqlConnection sqlConn = new SqlConnection(oConfigs.strEpicorDB))
            {
                    SqlCommand sqlCmd = new SqlCommand(strSql, sqlConn);
                    sqlCmd.Connection.Open();

                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlAdapter.SelectCommand = sqlCmd;
                    sqlAdapter.Fill(dtBtcPlt);

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

                    sqlCmd.Dispose();
                    sqlConn.Dispose();
            }


            Session oEpicSession = new Session(oConfigs.strEpicUser, oConfigs.strEpicPass, oConfigs.strEpicURL, Session.LicenseType.Default, oConfigs.strEpicConfig, false, strCompany, strPlant);
            ILauncher oEpicLaunch = new ILauncher(oEpicSession);

            try
            {
                if (oEpicSession != null)
                {
                    InventoryQtyAdjAdapter adpQtyAdjEntry = new InventoryQtyAdjAdapter(oEpicLaunch);
                    adpQtyAdjEntry.BOConnect();


                    if (adpQtyAdjEntry != null)
                    {
                        string question;
                        string serialWarning;
                        bool multipleMatches ;
                        string strPart;
                        string strUnitOfMeasure;
                        string strBin;
                        string strOutMessage;
                        string pcNeqQtyAction;
                        bool requiresUserInput;
                        string strTranNum;

                        foreach (clsBtcPltMat oBtcMat in objBtcPltFileAttr.mobjBtcPltMats)
                        {

                            strPart = oBtcMat.strEpicorMatCode;
                            strUnitOfMeasure = oBtcMat.strEpicorMatUOM;

                            var ds = adpQtyAdjEntry.GetInventoryQtyAdj(oBtcMat.strEpicorMatCode, oBtcMat.strEpicorMatUOM);

                            InventoryQtyAdjDataSet.InventoryQtyAdjRow x = ds.InventoryQtyAdj[0];
                            x.BeginEdit();
                            x.Company = strCompany;
                            x.TransDate = DateTime.Now;
                            x.PartNum = oBtcMat.strEpicorMatCode;
                            x.AdjustQuantity = (oBtcMat.strEpicorMatUOM == "PCS" ? Math.Round(oBtcMat.strBtcPltMatUsage,2) : oBtcMat.strBtcPltMatUsage);
                            x.UnitOfMeasure = oBtcMat.strEpicorMatUOM;
                            x.LotNum = "";
                            x.ReasonCode = "BP_INT";
                            x.WareHseCode = strWarehouse;
                            x.BinNum = strBinNum;
                            x.Reference = objBtcPltFileAttr.EpicJobNum;
                            x.RowMod = "U";
                            x.EndEdit();

                            adpQtyAdjEntry.GetPartXRefInfo(ref strPart, "", "",ref strUnitOfMeasure, out serialWarning, out question, out multipleMatches);
                            adpQtyAdjEntry.GetInventoryQtyAdjBrw(strPart, strWarehouse, out strBin);
                            adpQtyAdjEntry.NegativeInventoryTest(strPart, strWarehouse, strBinNum, "", "", oBtcMat.strEpicorMatUOM, 1, oBtcMat.strBtcPltMatUsage, out pcNeqQtyAction, out strOutMessage);
                            adpQtyAdjEntry.PreSetInventoryQtyAdj(out requiresUserInput);

                            adpQtyAdjEntry.SetInventoryQtyAdj(ds,out strTranNum);

                        }

                    }

                    adpQtyAdjEntry.Dispose();
                    adpQtyAdjEntry = null;

                    _DeleteEpicorSession(oConfigs, oEpicSession.SessionID);

                    _IsComplete = true;

                }
                else
                {
                    clsEventLogger objELogger = new clsEventLogger();
                    objELogger.beginLogging("Unable connect to Epicor", EventLogEntryType.Error);
                    objELogger.Dispose();

                    clsError oError = new clsError();
                    oError.ErrDescription = string.Format("Unable connect to Epicor");
                    objBtcPltFileAttr.addError(oError);

                    _DeleteEpicorSession(oConfigs, oEpicSession.SessionID);

                    _IsComplete = false;
                }

            }
            catch (Exception e)
            {
                clsError oError = new clsError();
                oError.ErrDescription = string.Format(e.Message);
                objBtcPltFileAttr.addError(oError);

                _DeleteEpicorSession(oConfigs, oEpicSession.SessionID);

                _IsComplete = false;
            }


            return _IsComplete;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        private void _DeleteEpicorSession(clsAppConfigs oConfigs, string strSessionId)
        {
            string strSql = "delete from ice.SessionState where sessionid = '{0}' ";
            strSql = string.Format(strSql, strSessionId);

            if (strSessionId.Trim() != "")
            {
                using (SqlConnection sqlConn = new SqlConnection(oConfigs.strEpicorDB))
                {
                    SqlCommand sqlCmd = new SqlCommand(strSql, sqlConn);
                    sqlCmd.Connection.Open();

                    sqlCmd.ExecuteNonQuery();

                    sqlCmd.Dispose();
                    sqlConn.Dispose();
                }
            }
        }




    }
}
