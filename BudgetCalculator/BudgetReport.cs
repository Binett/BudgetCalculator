using BudgetCalculator.Controllers;
using BudgetCalculator.Models;
using System.Collections.Generic;

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
        private readonly Calculator calc;
        public EconomicController ecoController;

        /// <summary>
        /// Constructor initializes all necessary objects which the class needs. Must have an economic object to function.
        /// </summary>
        /// <param name="_ecoController"></param>
        public BudgetReport(EconomicController _ecoController)
        {
            ecoController = _ecoController;
            calc = new Calculator(ecoController);
            TotalIncome = calc.GetTotalIncome();
            TotalExpenses = calc.GetTotalExpenses();
            TotalMoneyForSavings = calc.GetTotalSavingToMoney();
            Balance = calc.GetRemainingBalance(out List<EconomicObject> paidExpenses, out List<EconomicObject> unpayedExpenses);
            PaidExpenses = paidExpenses;
            UnpaidExpenses = unpayedExpenses;
        }

        /// <summary>
        /// Collect all calculated data from calculator to a string.
        /// </summary>
        /// <param name="ecoController"></param>
        /// <returns>A string of all calculated data.</returns>
        public string GetCalculatedDataToString()
        {
            List<string> listOfPaidExpenses = new(UnWrapExpenses(PaidExpenses));
            List<string> listOfUnpaidExpenses = new(UnWrapExpenses(UnpaidExpenses));
            return $"Total Income:       {TotalIncome}\n" +
               $"Total Expenses:     {TotalExpenses}\n" +
               $"Total Saving:       {TotalMoneyForSavings}\n" +
               $"Cash:               {Balance}\n\n" +
               $"Paid expenses:\n{GetStringFromList(listOfPaidExpenses)}\n" +
               $"Unpaid expenses:\n{GetStringFromList(listOfUnpaidExpenses)}\n";
        }

        /// <summary>
        /// Converts List of economic objects of certain type to string list
        /// </summary>
        /// <param name="list"></param>
        /// <returns>List of string</returns>
        private static List<string> UnWrapExpenses(List<EconomicObject> list)
        {
            List<string> listToSend = new();

            foreach (var exp in list)
            {
                if (exp.Type == EconomicType.Saving)
                {
                    listToSend.Add($"{exp.Name} Amount: {exp.Amount * 100} Percent\n");
                }
                else
                {
                    listToSend.Add($"{exp.Name} Amount: {exp.Amount}\n");
                }
            }

            return listToSend;
        }

        /// <summary>
        /// Set up one single string from a list of string
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static string GetStringFromList(List<string> list)
        {
            string dataTxt = string.Empty;
            foreach (var s in list)
            {
                dataTxt += s;
            }
            if (dataTxt?.Length == 0)
            {
                return "None\n";
            }
            return dataTxt;
        }
    }
}