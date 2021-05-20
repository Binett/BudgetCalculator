using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetCalculator.Models;
using BudgetCalculator.Controllers;
using BudgetCalculatorTests1.Seeder;

namespace BudgetCalculator.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        Calculator calc;
        EconomicController ecoController;
        TestSeeder testSeeder;

        [TestInitialize]
        public void Setup()
        {
            testSeeder = new TestSeeder();
            
        }

        [TestMethod()]
        public void GetTotalIncomeTest_Pass_ShouldReturnSum_14000() 
        {
            testSeeder.InitList();
            calc = new Calculator(testSeeder.ecoController);

            var expected = 14000;
            var actual = calc.GetTotalIncome();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalExpenses_Pass_ShouldReturnSum_3599()
        {
            testSeeder.InitList();
            calc = new Calculator(testSeeder.ecoController);

            var expected = 3599;
            var actual = calc.GetTotalExpenses();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalExpenses_PassDoubleMaxValue_ShouldReturnZero()
        {
            testSeeder.InitList();
            calc = new Calculator(testSeeder.ecoController);
            testSeeder.ecoController.UpdateEconomicObjectAmount("Food", double.MaxValue);
            var expected = 3599;
            var actual = calc.GetTotalExpenses();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalExpensesTest_Fail_Should()
        {
            testSeeder.InitList();
            testSeeder.ecoController.UpdateEconomicObjectAmount("Food", 999999);
            calc = new Calculator(testSeeder.ecoController);

            var expected = 3599;
            var actual = calc.GetTotalExpenses();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalSavingTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetRemainingBalanceTest()
        {
            Assert.Fail();
        }
    }
}