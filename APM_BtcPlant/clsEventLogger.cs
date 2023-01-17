using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace APM_BtcPlant
{
    public class clsEventLogger
    {
        public void beginLogging(string strEvents, EventLogEntryType ltEntryType)
        {
            //if (!System.Diagnostics.EventLog.SourceExists("BatchingPlantToEpicorApp"))
            //{
            //    System.Diagnostics.EventLog.CreateEventSource("BatchingPlantToEpicorApp", "EpicorExternalApp");
            //}

            //System.Diagnostics.EventLog epicEvent = new System.Diagnostics.EventLog("EpicorExternalApp");
            //epicEvent.Source = "BatchingPlantToEpicorApp";

            //epicEvent.WriteEntry(strEvents, ltEntryType);

            //epicEvent.Close();
            //epicEvent.Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
