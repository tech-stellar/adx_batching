using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APM_BtcPlant
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main(string[] args)
        {
            clsAppConfigs objAppConfigs = new clsAppConfigs();
            objAppConfigs.LoadDefaultValueFromConfig();

            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                frmBtcPltMain mfrmBtcPltMain = new frmBtcPltMain();
                mfrmBtcPltMain.PassingOverAppConfigs(objAppConfigs);

                Application.Run(mfrmBtcPltMain);
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    //System.Console.WriteLine("Arg[{0}] = [{1}]", i, args[i]);
                    if (string.Compare(args[i], "-AUTO", true) == 0)
                    { beginImportBatchingPlantFiles(objAppConfigs); }
                }

            }
        }

        static void beginImportBatchingPlantFiles(clsAppConfigs oAppConfigs)
        {
            clsBtcPltFiles mobjBtcPltFiles = new clsBtcPltFiles();
            clsEpicor oEpicor = new clsEpicor();

            // begin to scan the batching plant system output files
            mobjBtcPltFiles.scanBtcPltFolder(oAppConfigs.strIncomingFileFolder);

            foreach (clsBtcPltFileAttr oBtcPltFileAttr in mobjBtcPltFiles.mobjBtcPltFileAttrs)
            {

                if (mobjBtcPltFiles.checkIsFileContentEmpty(oBtcPltFileAttr) == false)
                {

                    mobjBtcPltFiles.readBtcPltFile(oBtcPltFileAttr);
                    mobjBtcPltFiles.genProcessID(oBtcPltFileAttr, oAppConfigs);

                    if (mobjBtcPltFiles.verifyDuplicateBtcPltFile(oBtcPltFileAttr, oAppConfigs))
                    {
                        // map the batch plant with epicor company, warehouse, plant & bin
                        if (mobjBtcPltFiles.mapBtcPltWithEpic(oBtcPltFileAttr, oAppConfigs))
                        {
                            // map the item code with epicor item code
                            if (mobjBtcPltFiles.mapBtcPltMaterialWithEpic(oBtcPltFileAttr, oAppConfigs))
                            {
                                // load mapping from ud table for fg
                                if (mobjBtcPltFiles.mapBtcPltFGWithEpic(oBtcPltFileAttr, oAppConfigs))
                                {
                                    // load part from epicor
                                    if (mobjBtcPltFiles.loadPartInfoForFG(oBtcPltFileAttr, oAppConfigs))
                                    {
                                        // load material from epicor
                                        if (mobjBtcPltFiles.loadPartInfoForMaterial(oBtcPltFileAttr, oAppConfigs))
                                        {
                                            // material on hand
                                            if (mobjBtcPltFiles.chkMaterialOnHandQty(oBtcPltFileAttr, oAppConfigs))
                                            {

                                                mobjBtcPltFiles.genJobNum(oBtcPltFileAttr, oAppConfigs);

                                                if (oEpicor.createJobEntry(oBtcPltFileAttr, oAppConfigs))
                                                {
                                                    oBtcPltFileAttr.EpicStatus = "Success";
                                                    oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                                                    mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strCompleteFileFolder);
                                                }
                                                else
                                                {
                                                    oBtcPltFileAttr.EpicStatus = "Failed";
                                                    oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                                                    mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strErrorsFileFolder);
                                                }
                                            }
                                            else
                                            // material on hand
                                            {
                                                oBtcPltFileAttr.EpicStatus = "Failed";
                                                oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                                                mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strErrorsFileFolder);
                                            }
                                        }
                                        else
                                        {
                                            // load part from epicor
                                            oBtcPltFileAttr.EpicStatus = "Failed";
                                            oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                                            mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strErrorsFileFolder);
                                        }
                                    }
                                    else
                                    {
                                        oBtcPltFileAttr.EpicStatus = "Failed";
                                        oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                                        mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strErrorsFileFolder);
                                    }

                                }
                                else
                                {
                                    oBtcPltFileAttr.EpicStatus = "Failed";
                                    oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                                    mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strErrorsFileFolder);

                                }

                            }
                            else
                            {
                                oBtcPltFileAttr.EpicStatus = "Failed";
                                oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                                mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strErrorsFileFolder);
                            }
                        }
                        else
                        {
                            oBtcPltFileAttr.EpicStatus = "Failed";
                            oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                            mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strErrorsFileFolder);
                        }


                        // start create job in epicor
                        //bool execResult05 = oEpicor.createJobEntry(objBtcFileProp, mobjAppConfigs);

                    }
                    else
                    {
                        oBtcPltFileAttr.EpicStatus = "Failed";
                        oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                        mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strErrorsFileFolder);
                    }
                }
                else
                {
                    oBtcPltFileAttr.EpicStatus = "Failed";
                    oEpicor.dumpToLog_EpicorUD38(oBtcPltFileAttr, oAppConfigs);
                    mobjBtcPltFiles.moveBtcPltFile(oBtcPltFileAttr, oAppConfigs.strErrorsFileFolder);
                }

            }

            oEpicor.Dispose();
            mobjBtcPltFiles.Dispose();

        }



    }

}
