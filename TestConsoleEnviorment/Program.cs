using BudgetCalculatorTests1.Seeder;
using BudgetCalculator.Controllers;
using BudgetCalculator.Models;
using BudgetCalculator;

namespace TestConsoleEnviorment
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args is null)
            {
                throw new System.ArgumentNullException(nameof(args));
            }
            //Console.WriteLine("Hello Test Enviorment!");

            ////Testing the Budgetcalculator Library

            //EconomicController ecoController = new EconomicController();

            //ecoController.AddEconomicObjectToList("Salary", EconomicType.Income, 14000);
            //ecoController.AddEconomicObjectToList("Rent", EconomicType.Expense, 2000);
            //ecoController.AddEconomicObjectToList("Subscription", EconomicType.Expense, 99);
            //ecoController.AddEconomicObjectToList("Food", EconomicType.Expense, 1500);
            //ecoController.AddEconomicObjectToList("Savings", EconomicType.Saving, 0.1);

            //BudgetReport report = new BudgetReport(ecoController);

            //Console.WriteLine(ErrorLogger.GetSummarizedLogAsString());

            TestSeeder seeder = new TestSeeder();

            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Ima buy me some Solar cells", EconomicType.Saving, 0.7);
            
            System.Console.WriteLine(new BudgetReport(seeder.ecoController).GetCalculatedDataToString());
        }
    }
}