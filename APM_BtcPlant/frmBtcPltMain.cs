using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;
using System.Diagnostics;
using System.Data.SqlClient;

namespace APM_BtcPlant
{
    public partial class frmBtcPltMain : Form
    {
        public clsAppConfigs mobjAppConfigs ;

        public frmBtcPltMain()
        {
            InitializeComponent();
            Load += new EventHandler(frmBtcPltMain_Load);
        }

        private void frmBtcPltMain_Load(object sender, System.EventArgs e)
        {
            updateProgressToForm("Loading configuration file");
            updateProgressToForm("Incoming file folder is set to >> " + mobjAppConfigs.strIncomingFileFolder);

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;

            this.Text = "Batching Plant Integration " + version;
        }

        public void PassingOverAppConfigs(clsAppConfigs objAppConfigs)
        {
            mobjAppConfigs = objAppConfigs;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmBtcPltCfg fBtcPltConfig = new frmBtcPltCfg();
            fBtcPltConfig.PassingOverAppConfigs(mobjAppConfigs);
            fBtcPltConfig.ShowDialog();
            fBtcPltConfig.Dispose();

            //refresh the configuration file
            mobjAppConfigs.LoadDefaultValueFromConfig();

            updateProgressToForm("Reloading configuration file");
            updateProgressToForm("Incoming file folder is set to >> " + mobjAppConfigs.strIncomingFileFolder);
            updateProgressToForm("Complete file folder is set to >> " + mobjAppConfigs.strCompleteFileFolder);
            updateProgressToForm("Errors file folder is set to >> " + mobjAppConfigs.strErrorsFileFolder);
        }

        private void btnImports_Click(object sender, EventArgs e)
        {
            clsBtcPltFiles mobjBtcPltFiles = new clsBtcPltFiles();

            if (mobjBtcPltFiles.checkCompany(mobjAppConfigs))
            {
                MessageBox.Show("Batching plant currently in use.");
            }
            else
            {
                //mobjBtcPltFiles.updateCompany(true, mobjAppConfigs);
                this.btnImports.Enabled = false;
                beginImportBatchingPlantFiles();
                this.btnImports.Enabled = true;
                //mobjBtcPltFiles.updateCompany(false, mobjAppConfigs);
            }
        }

        private void updateProgressToForm(string strText)
        {
            string strTemp;

            if (String.Compare(strText, "", true) != 0)
            {
                strTemp = txtProgress.Text;
                txtProgress.Text = strTemp + System.Environment.NewLine + strText;
            }
        }

        private void beginImportBatchingPlantFiles()
        {
            clsBtcPltFiles mobjBtcPltFiles = new clsBtcPltFiles();
            List<clsBtcPltFileAttr> oAdxFiles = new List<clsBtcPltFileAttr>();
            clsEpicor oEpicor = new clsEpicor();

            // begin to scan the batching plant system output files
            updateProgressToForm("Begin scanning folder " + mobjAppConfigs.strIncomingFileFolder);
            mobjBtcPltFiles.scanBtcPltFolder(mobjAppConfigs.strIncomingFileFolder);

            updateProgressToForm(mobjBtcPltFiles.intBtcPltFileCount.ToString() + " files detected");

            foreach (clsBtcPltFileAttr oBtcPltFileAttr in mobjBtcPltFiles.mobjBtcPltFileAttrs)
            {
                // read text file data and store in the class
                updateProgressToForm("Import file " + oBtcPltFileAttr.BtcPltFileName.ToString());

                //check if file content is empty then skip the rest
                if (mobjBtcPltFiles.checkIsFileContentEmpty(oBtcPltFileAttr) == false)
                {
                    //mobjBtcPltFiles.readBtcPltFile(oBtcPltFileAttr);

                    // read the text file and load into the temp object                   
                        if (mobjBtcPltFiles.verifyDuplicateBtcPltFile(oBtcPltFileAttr, mobjAppConfigs))
                        {
                            mobjBtcPltFiles.readADXBtcPltFile(oBtcPltFileAttr, ref oAdxFiles);

                            foreach (clsBtcPltFileAttr oAdx in oAdxFiles)
                            {
                                mobjBtcPltFiles.genProcessID(oAdx, mobjAppConfigs);

                                // map the batch plant with epicor company, warehouse, plant & bin
                                if (mobjBtcPltFiles.mapBtcPltWithEpic(oAdx, mobjAppConfigs))
                                {
                                    //mobjBtcPltFiles.loadPackingMaterial(oAdx, mobjAppConfigs);
                                // map the item code with epicor item code
                                //if (mobjBtcPltFiles.loadPackingMaterial(oBtcPltFileAttr, mobjAppConfigs))
                                //{
                                // load mapping from ud table for fg
                                if (mobjBtcPltFiles.mapBtcPltFGWithEpic(oAdx, mobjAppConfigs))
                                        {
                                            // load part from epicor
                                            if (mobjBtcPltFiles.loadPartInfoForFG(oAdx, mobjAppConfigs))
                                            {
                                                mobjBtcPltFiles.loadPackingMaterial(oAdx, mobjAppConfigs);
                                        // load material from epicor
                                                if (mobjBtcPltFiles.loadPartInfoForMaterial(oAdx, mobjAppConfigs))
                                                {
                                                    

                                            // material on hand
                                                    if (mobjBtcPltFiles.chkMaterialOnHandQty(oAdx, mobjAppConfigs))
                                                        {
                                                        
                                                        mobjBtcPltFiles.genJobNum(oAdx, mobjAppConfigs);
                                                        
                                                        // start create job
                                                        if (oEpicor.createJobEntry(oAdx, mobjAppConfigs))
                                                        {
                                                            // start create quantity adjustment
                                                            if (oEpicor.quantityAdjustment(oAdx, mobjAppConfigs))
                                                            {

                                                                oAdx.EpicStatus = "Success";
                                                                oEpicor.dumpToLog_EpicorUD38(oAdx, mobjAppConfigs);
                                                                mobjBtcPltFiles.moveBtcPltFile(oAdx, mobjAppConfigs.strCompleteFileFolder);
                                                            }
                                                            else
                                                            {
                                                                oAdx.EpicStatus = "Failed";
                                                                oEpicor.dumpToLog_EpicorUD38(oAdx, mobjAppConfigs);
                                                                mobjBtcPltFiles.moveBtcPltFile(oAdx, mobjAppConfigs.strErrorsFileFolder);

                                                            }
                                                        }
                                                        else
                                                        {
                                                            oAdx.EpicStatus = "Failed";
                                                            oEpicor.dumpToLog_EpicorUD38(oAdx, mobjAppConfigs);
                                                            mobjBtcPltFiles.moveBtcPltFile(oAdx, mobjAppConfigs.strErrorsFileFolder);
                                                        }
                                                    }
                                                    else
                                                    // material on hand
                                                    {
                                                        oAdx.EpicStatus = "Failed";
                                                        oEpicor.dumpToLog_EpicorUD38(oAdx, mobjAppConfigs);
                                                        mobjBtcPltFiles.moveBtcPltFile(oAdx, mobjAppConfigs.strErrorsFileFolder);
                                                    }
                                                }
                                                else
                                                {
                                                    // load part from epicor
                                                    oAdx.EpicStatus = "Failed";
                                                    oEpicor.dumpToLog_EpicorUD38(oAdx, mobjAppConfigs);
                                                    mobjBtcPltFiles.moveBtcPltFile(oAdx, mobjAppConfigs.strErrorsFileFolder);
                                                }
                                            }
                                            else
                                            {
                                                oAdx.EpicStatus = "Failed";
                                                oEpicor.dumpToLog_EpicorUD38(oAdx, mobjAppConfigs);
                                                mobjBtcPltFiles.moveBtcPltFile(oAdx, mobjAppConfigs.strErrorsFileFolder);
                                            }

                                        }
                                        else
                                        {
                                            oAdx.EpicStatus = "Failed";
                                            oEpicor.dumpToLog_EpicorUD38(oAdx, mobjAppConfigs);
                                            mobjBtcPltFiles.moveBtcPltFile(oAdx, mobjAppConfigs.strErrorsFileFolder);

                                        }

                                    //}
                                    //else
                                    //{
                                    //    oBtcPltFileAttr.EpicStatus = "Failed";
                                    //    oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, mobjAppConfigs);
                                    //    mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, mobjAppConfigs.strErrorsFileFolder);
                                    //}
                                }
                                else
                                {
                                    oBtcPltFileAttr.EpicStatus = "Failed";
                                    oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, mobjAppConfigs);
                                    mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, mobjAppConfigs.strErrorsFileFolder);
                                }


                                // start create job in epicor
                                //bool execResult05 = oEpicor.createJobEntry(objBtcFileProp, mobjAppConfigs);
                            }
                        }
                        else
                        {
                            oBtcPltFileAttr.EpicStatus = "Failed";
                            oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, mobjAppConfigs);
                            mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, mobjAppConfigs.strErrorsFileFolder);
                        }

                }
                else
                {
                    oBtcPltFileAttr.EpicStatus = "Failed";
                    oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, mobjAppConfigs);
                    mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, mobjAppConfigs.strErrorsFileFolder);
                }    
                
            } // foreach
            oEpicor.Dispose();
            mobjBtcPltFiles.Dispose();

        }// beginImportBatchingPlantFiles

    }

}
