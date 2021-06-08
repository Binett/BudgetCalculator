using System;
using System.Diagnostics;
using System.IO;

namespace BudgetCalculator.Helpers
{
    /// <summary>
    /// Class for writing strings to file.
    /// </summary>
    public class WriteToFile
    {
        private string fileName = "default";

        /// <summary>
        /// Generates file for todays date and sets path to desktop
        /// </summary>
        public string PathAndFileName
        {
            get
            {
                var desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string temp = fileName + ".txt";
                return Path.Combine(desktop, DateTime.Now.ToString("yyyy-MM-dd") + temp);
            }
        }

        /// <summary>
        /// Recieves string txt and writes to file
        /// </summary>
        /// <param name="txt">text that should be added to file</param>
        public void WriteStringToFile(string name, string txt)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(txt))
            {
                if (txt != "")
                {
                    fileName = name;
                    File.AppendAllText(PathAndFileName, txt + "\n");
                }
                else
                {
                    Debug.WriteLine("Text is empty");
                }
            }
            else
            {
                Debug.WriteLine("String is null or empty");
                ErrorLogger.Add("String is null or empty in write to file call to write to string file");
            }
        }
    }
}