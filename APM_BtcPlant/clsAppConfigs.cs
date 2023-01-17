using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace APM_BtcPlant
{

    public class clsAppConfigs
    {
        private string strPrivateIncomingFileFolder;
        private string strPrivateCompleteFileFolder;
        private string strPrivateErrorsFileFolder;

        private string strPrivateEpicUser;
        private string strPrivateEpicPass;
        private string strPrivateEpicURL;
        private string strPrivateEpicConfig;

        private string strPrivateEpicorDB;

        public string strIncomingFileFolder
        {
            get
            { return strPrivateIncomingFileFolder; }
            set
            { strPrivateIncomingFileFolder = value; }
        }

        public string strCompleteFileFolder
        {
            get
            { return strPrivateCompleteFileFolder; }
            set
            { strPrivateCompleteFileFolder = value; }
        }

        public string strErrorsFileFolder
        {
            get
            { return strPrivateErrorsFileFolder; }
            set
            { strPrivateErrorsFileFolder = value; }
        }

        public string strEpicUser
        {
            get
            { return strPrivateEpicUser; }
            set
            { strPrivateEpicUser = value; }
        }

        public string strEpicPass
        {
            get
            { return strPrivateEpicPass; }
            set
            { strPrivateEpicPass = value; }
        }

        public string strEpicURL
        {
            get
            { return strPrivateEpicURL; }
            set
            { strPrivateEpicURL = value; }
        }

        public string strEpicConfig
        {
            get
            { return strPrivateEpicConfig; }
            set
            { strPrivateEpicConfig = value; }
        }

        public string strEpicorDB
        {
            get
            { return strPrivateEpicorDB; }
            set
            { strPrivateEpicorDB = value; }
        }


        public void LoadDefaultValueFromConfig()
        {
            strPrivateIncomingFileFolder = ReadAppSettingSection("Incoming");
            strPrivateCompleteFileFolder = ReadAppSettingSection("Complete");
            strPrivateErrorsFileFolder = ReadAppSettingSection("Errors");

            strPrivateEpicUser = ReadAppSettingSection("EpicUser");
            strPrivateEpicPass = ReadAppSettingSection("EpicPass");
            strPrivateEpicURL = ReadAppSettingSection("EpicURL");
            strPrivateEpicConfig = ReadAppSettingSection("EpicConfig");
            strPrivateEpicorDB = ReadAppSettingSection("EpicDBString");
        }

        public void SaveValueToConfig()
        {
            SaveAppSettingSection("Incoming", strPrivateIncomingFileFolder);
            SaveAppSettingSection("Complete", strPrivateCompleteFileFolder);
            SaveAppSettingSection("Errors", strPrivateErrorsFileFolder);

            SaveAppSettingSection("EpicUser", strPrivateEpicUser);
            SaveAppSettingSection("EpicPass", strPrivateEpicPass);
            SaveAppSettingSection("EpicURL", strPrivateEpicURL);
            SaveAppSettingSection("EpicConfig", strPrivateEpicConfig);
            SaveAppSettingSection("EpicDBString", strPrivateEpicorDB);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private string ReadAppSettingSection(string strKey)
        {

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = "";

                if (appSettings[strKey] == null)
                { result = ""; }
                else
                { result = appSettings[strKey]; }

                return (result);
            }
            catch (ConfigurationErrorsException)
            {
                return ("");
            }
        }

        private void SaveAppSettingSection(string strKey, string strValue)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[strKey] == null)
                {
                    settings.Add(strKey, strValue);
                }
                else
                {
                    settings[strKey].Value = strValue;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

            }
            catch (ConfigurationErrorsException)
            { }
        }
            
    }
}
