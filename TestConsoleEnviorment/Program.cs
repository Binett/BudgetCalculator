using System;
using BudgetCalculator.Controllers;
using BudgetCalculator.Models;
using BudgetCalculator.Helpers;
using BudgetCalculator;

namespace TestConsoleEnviorment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Test Enviorment!");


            //Testing the Budgetcalculator Library

            EconomicController ecoTest = new EconomicController();

            ecoTest.AddEconomicObjectToList("Salary", EconomicType.Income, 14000);
            ecoTest.AddEconomicObjectToList("Rent", EconomicType.Expense, 2000);
            ecoTest.AddEconomicObjectToList("Subscription", EconomicType.Expense, 99);
            ecoTest.AddEconomicObjectToList("Food", EconomicType.Expense, 1500);
            ecoTest.AddEconomicObjectToList("Savings", EconomicType.Saving, 0.1);

            BudgetReport report = new BudgetReport(ecoTest);
            report.GetCalculatedDataToString(ecoTest);

            //WriteToFile writer = new WriteToFile();

            //writer.WriteReportToFile(report);

            //Console.WriteLine(report);
        }
    }
}
