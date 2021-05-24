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
            string reportString = string.Empty;
            reportString = $"Total Income:       {report.TotalIncome}\n" + 
                           $"Total Expenses:     {report.TotalExpenses}\n" +
                           $"Total Saving:       {report.TotalMoneyForSavings}\n" +
                           $"Cash:               {report.Balance}\n" +
                           $"Expenses (paid):    {UnWrapExpenses(EconomicType.Expense, report.PaidExpenses)}\n" +
                           $"Expenses (unpaid):  {UnWrapExpenses(EconomicType.Expense, report.UnpaidExpenses)}\n" +
                           $"Savings (paid):     {UnWrapExpenses(EconomicType.Saving, report.PaidExpenses)}\n" +
                           $"Savings (unpaid):   {UnWrapExpenses(EconomicType.Saving, report.UnpaidExpenses)}\n";
            return reportString;
        }

        private List<string> UnWrapExpenses(EconomicType type, List<EconomicObject> list)
        {
            List<string> listToSend = new List<string>();

            foreach(var exp in list)
            {
                if(exp.Type == type)
                {
                    listToSend.Add($"Name: {exp.Name} Amount: {exp.Amount}");
                }
            }

            return listToSend;
        }
    }
}
