using System;
using BudgetCalculator;
using BudgetCalculatorTests1.Seeder;
using BudgetCalculator.Controllers;
using BudgetCalculator.Helpers;
using BudgetCalculator.Models;
using BudgetCalculator.Tests;

namespace TestConsoleEnviorment
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            //Skapa en ecocontroller.
            EconomicController ecoController = new EconomicController();

            //För in data i ecocontrollern
            ecoController.AddEconomicObjectToList("Giraffe", EconomicType.Saving, 9999.9);
            ecoController.AddEconomicObjectToList("Income", EconomicType.Income, 5000);
            ecoController.AddEconomicObjectToList(" ", EconomicType.Expense, double.MaxValue);
            ecoController.AddEconomicObjectToList(null, EconomicType.Income, -2222.222);

            //Skapa en rapport med ecocontrollern
            BudgetReport report = new BudgetReport(ecoController);

            //Skapa en writer
            WriteToFile writer = new WriteToFile();

            //Skriv errorloggern till fil
            writer.WriteStringToFile("the error log", ErrorLogger.GetSummarizedLogAsString());

            //Ta bort giraffen, för dyr.
            ecoController.RemoveEconomicObjectFromList("Giraffe");

            //Lägg till lite grejjer
            ecoController.AddEconomicObjectToList("Salary", EconomicType.Income, 14000);
            ecoController.AddEconomicObjectToList("Rent", EconomicType.Expense, 2000);
            ecoController.AddEconomicObjectToList("Subscription", EconomicType.Expense, 99);
            ecoController.AddEconomicObjectToList("Food", EconomicType.Expense, 1500);
            ecoController.AddEconomicObjectToList("Savings", EconomicType.Saving, 0.1);

            //Ny rapport
            report = new BudgetReport(ecoController);

            //Skicka till consolen
            Console.WriteLine(report.GetCalculatedDataToString());

            //Skriv rapporten till fil
            writer.WriteStringToFile("Private economy report", report.GetCalculatedDataToString());
        }
    }
}
