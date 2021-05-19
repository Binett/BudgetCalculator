using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetCalculator.Controllers;
using BudgetCalculator.Models;

namespace BudgetCalculatorTests1.Seeder
{
    public class TestSeeder
    {
        public EconomicController ecoController;
        public TestSeeder()
        {
            ecoController = new EconomicController();
        }

        public void InitList()
        {
            ecoController.AddEconomicObjectToList("Salary", EconomicType.Income, 14000);
            ecoController.AddEconomicObjectToList("Rent", EconomicType.Expense, 2000);
            ecoController.AddEconomicObjectToList("Subscription", EconomicType.Expense, 99);
            ecoController.AddEconomicObjectToList("Food", EconomicType.Expense, 1500);
            ecoController.AddEconomicObjectToList("Savings", EconomicType.Saving, 0.1);
        }
    }
}
