using System;
using System.Collections.Generic;

namespace BudgetCalculator.Helpers
{
    /// <summary>
    /// Static class that contains a list of errors.
    /// </summary>
    public static class ErrorLogger
    {
        private static readonly List<string> logList = new();

        /// <summary>
        /// Returns the list of errors. 
        /// </summary>
        /// <returns>A list of string type</returns>
        public static List<string> GetLogList()
        {
            return logList;
        }

        /// <summary>
        /// Returns a summarize of the LogList as a string.
        /// </summary>
        /// <returns>String of summarized log errors</returns>
        public static string GetSummarizedLogAsString()
        {
            int counter = 1;
            string stringToSend = string.Empty;
            if (logList == null || logList.Count == 0)
            {
                return "No Logs";
            }

            foreach (var s in logList)
            {
                stringToSend += $"{counter}: {DateTime.Now} {s}\n";
                counter++;
            }

            return stringToSend;
        }

        /// <summary>
        /// Adds a error to the LogList
        /// </summary>
        /// <param name="text"></param>
        public static void Add(string text)
        {
            if (logList.Contains(text))
            {
                return;
            }

            logList.Add(text);
        }
    }
}