using System;
using System.Collections.Generic;

namespace BudgetCalculator.Helpers
{
    public static class ErrorLogger
    {
        private static readonly List<string> logList = new();

        public static List<string> GetLogList()
        {
            return logList;
        }

        public static string GetSummarizedLogAsString()
        {
            int counter = 1;

            string stringToSend = string.Empty;
            if (logList == null || logList.Count == 0)
            {
                return "No Logs";
            }
            foreach(var s in logList)
            {
                stringToSend += $"{counter}: {DateTime.Now} {s}\n";
                counter++;
            }
            return stringToSend;
        }

        public static void Add(string text)
        {
            if(logList.Contains(text))
            {
                return;
            }

            logList.Add(text);
        }
    }
}