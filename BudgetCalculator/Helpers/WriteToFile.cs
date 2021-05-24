using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator.Helpers
{
    public class WriteToFile
    {
        /// <summary>
        /// Generates file for todays date and sets path to desktop
        /// </summary>
        public string FileName
        {
            get
            {
                var desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                return Path.Combine(desktop, DateTime.Now.ToString("yyyy-mm-dd") + ".log"); ;
            }
        }

        /// <summary>
        /// Recieves string txt and writes to file
        /// </summary>
        /// <param name="txt">text that should be added to file</param>
        public void WriteStringToFile(string txt)
        {
            File.AppendAllText(FileName, DateTime.Now + ":" + txt + "\r\n");
        }

    }
}
