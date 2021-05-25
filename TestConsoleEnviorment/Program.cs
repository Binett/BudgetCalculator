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

            ecoTest.AddEconomicObjectToList("SalaSADry", EconomicType.Income, 14000);
            ecoTest.AddEconomicObjectToList("ReASDnt", EconomicType.Expense, 2000);
            ecoTest.AddEconomicObjectToList("SubscSADription", EconomicType.Expense, 99);
            ecoTest.AddEconomicObjectToList("FoASDod", EconomicType.Expense, 1500);
            ecoTest.AddEconomicObjectToList("SavASDings", EconomicType.Saving, 0.05);


            BudgetReport report = new BudgetReport(ecoTest);


            Console.WriteLine(report.GetCalculatedDataToString());

        }
    }
}
