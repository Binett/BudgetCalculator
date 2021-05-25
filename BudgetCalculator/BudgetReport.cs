using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetCalculator.Models;
using BudgetCalculator.Controllers;
using BudgetCalculator.Helpers;

namespace BudgetCalculator
{
    public class BudgetReport
    {
        // This is a POCO not a DTO, we got logic in here.

        public double TotalIncome { get; }
        public double TotalExpenses { get; }
        public double TotalMoneyForSavings { get; }
        public List<EconomicObject> PaidExpenses { get; }
        public List<EconomicObject> UnpaidExpenses { get; }
        public double Balance { get; }

        private Calculator calc;
        public  EconomicController ecoController;
        private ErrorLogger errorLogger;

        public BudgetReport(EconomicController _ecoController)
        {
            ecoController = _ecoController;
            errorLogger = new ErrorLogger();

            calc = new Calculator(ecoController);

            TotalIncome = calc.GetTotalIncome();
            TotalExpenses = calc.GetTotalExpenses();
            TotalMoneyForSavings = calc.GetTotalSavingToMoney();

            Balance = calc.GetRemainingBalance(out List<EconomicObject> paidExpenses, out List<EconomicObject> unpayedExpenses);

            PaidExpenses = paidExpenses;
            UnpaidExpenses = unpayedExpenses;

            errorLogger.Log.Add(calc.GetErrorLog().GetErrorsAsString());
        }

        /// <summary>
        /// Collect all calculated data from calculator to a string. 
        /// </summary>
        /// <param name="ecoController"></param>
        /// <returns>A string of all calculated data.</returns>
        public string GetCalculatedDataToString(EconomicController ecoController)
        {
            BudgetReport report = new BudgetReport(ecoController);
            List<string> listOfPaidExpenses = new List<string>(UnWrapExpenses(EconomicType.Expense, report.PaidExpenses));
            List<string> listOfUnpaidExpenses = new List<string>(UnWrapExpenses(EconomicType.Expense, report.UnpaidExpenses));
            List<string> listOfPaidSavings = new List<string>(UnWrapExpenses(EconomicType.Saving, report.PaidExpenses));
            List<string> listOfUnpaidSavings = new List<string>(UnWrapExpenses(EconomicType.Saving, report.UnpaidExpenses));

            string reportString = string.Empty;
            reportString = $"Total Income:       {report.TotalIncome}\n"+ 
                           $"Total Expenses:     {report.TotalExpenses}\n"+ 
                           $"Total Saving:       {report.TotalMoneyForSavings}\n"+
                           $"Cash:               {report.Balance}\n"+
                           $"Expenses (paid):    {GetStringFromList(listOfPaidExpenses)}\n" +
                           $"Expenses (unpaid):  {GetStringFromList(listOfUnpaidExpenses)}\n"+
                           $"Savings (paid):     {GetStringFromList(listOfPaidSavings)}\n"+
                           $"Savings (unpaid):   {GetStringFromList(listOfUnpaidSavings)}";
            return reportString;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="list"></param>
        /// <returns>List of string</returns>
        private List<string> UnWrapExpenses(EconomicType type, List<EconomicObject> list)
        {
            List<string> listToSend = new List<string>();

            foreach(var exp in list)
            {
                if(exp.Type == type)
                {
                    listToSend.Add($"Name: {exp.Name} Amount: {exp.Amount}\n");
                }
            }

            return listToSend;
        }

        /// <summary>
        /// Set up one single string from a list of string
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetStringFromList(List<string> list)
        {
            string dataTxt = string.Empty;
            foreach (var s in list)
            {
                dataTxt += s;
            }
            return dataTxt;
        }
     
    }
}
