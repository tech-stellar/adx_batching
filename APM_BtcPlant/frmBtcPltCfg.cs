using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APM_BtcPlant
{
    public partial class frmBtcPltCfg : Form
    {
        public clsAppConfigs mobjConfigs;


        public frmBtcPltCfg()
        {
            InitializeComponent();
            Load += new EventHandler(frmBtcPltCfg_Load);
        }

        private void frmBtcPltCfg_Load(object sender, System.EventArgs e)
        {
            txtIncoming.Text = mobjConfigs.strIncomingFileFolder;
            txtProcess.Text = mobjConfigs.strCompleteFileFolder;
            txtError.Text = mobjConfigs.strErrorsFileFolder;

            txtEpicUser.Text = mobjConfigs.strEpicUser;
            txtEpicPass.Text = mobjConfigs.strEpicPass;
            txtEpicURL.Text = mobjConfigs.strEpicURL;
            txtConfig.Text = mobjConfigs.strEpicConfig;
            txtDBString.Text = mobjConfigs.strEpicorDB;
        }

        public void PassingOverAppConfigs(clsAppConfigs objAppConfigs)
        {
            mobjConfigs = objAppConfigs;
        }

        private void btnIncoming_Click(object sender, EventArgs e)
        {
            txtIncoming.Text = getFolderPathValue();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            txtProcess.Text = getFolderPathValue();
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            txtError.Text = getFolderPathValue();
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            mobjConfigs.SaveValueToConfig();
            Cursor.Current = Cursors.Default;
        }

        private string getFolderPathValue()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                return (folderBrowser.SelectedPath.ToString());
            }
            else
            {
                return ("");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtIncoming_TextChanged(object sender, EventArgs e)
        {
            mobjConfigs.strIncomingFileFolder = txtIncoming.Text;
        }

        private void txtProcess_TextChanged(object sender, EventArgs e)
        {
            mobjConfigs.strCompleteFileFolder = txtProcess.Text;
        }

        private void txtError_TextChanged(object sender, EventArgs e)
        {
            mobjConfigs.strErrorsFileFolder = txtError.Text;
        }

        private void txtEpicUser_TextChanged(object sender, EventArgs e)
        {
            mobjConfigs.strEpicUser = txtEpicUser.Text;
        }

        private void txtEpicPass_TextChanged(object sender, EventArgs e)
        {
            mobjConfigs.strEpicPass = txtEpicPass.Text;
        }

        private void txtEpicURL_TextChanged(object sender, EventArgs e)
        {
            mobjConfigs.strEpicURL = txtEpicURL.Text;
        }

        private void txtConfig_TextChanged(object sender, EventArgs e)
        {
            mobjConfigs.strEpicConfig = txtConfig.Text;
        }


        private void txtDBString_TextChanged(object sender, EventArgs e)
        {
            mobjConfigs.strEpicorDB = txtDBString.Text;
        }


    }
}
