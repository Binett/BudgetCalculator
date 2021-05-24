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

        public string GetCalculatedDataToString(EconomicController ecoController)
        {
            BudgetReport report = new BudgetReport(ecoController);
            string reportString = null;
            reportString = $"Total Income:       {report.TotalIncome}\n" + 
                           $"Total Expenses:     {report.TotalExpenses}\n" +
                           $"Total Saving:       {report.TotalMoneyForSavings}\n" +
                           $"Cash:               {report.Balance}\n" +
                           $"Expenses (paid):    {UnWrapExpenses(report.PaidExpenses)}\n" +
                           $"Expenses (unpaid):  {UnWrapExpenses(report.UnpaidExpenses)}\n" +
                           $"Savings (paid);     {}";
            return reportString;
        }

        private List<string> UnWrapExpenses(List<EconomicObject> list)
        {
            List<string> listToSend = new List<string>();

            foreach(var exp in list)
            {
                if(exp.Type == EconomicType.Expense)
                {
                    listToSend.Add($"Name: {exp.Name} Amount: {exp.Amount}");
                }
            }

            return listToSend;
        }
    }
}
