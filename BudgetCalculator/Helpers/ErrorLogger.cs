using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator.Helpers
{
    public class ErrorLogger
    {
        public List<string> Log { get; set; }

        public string GetErrorsAsString()
        {
            string stringToSend = null;
            foreach(string error in Log)
            {
                stringToSend += $"\n{error} {DateTime.Now}";
            }
            return stringToSend;
        }
    }
}