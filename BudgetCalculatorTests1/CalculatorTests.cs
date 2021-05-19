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
        public void GetTotalIncomeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTotalExpenses_Pass_ShouldReturnSum_3599()
        {
            testSeeder.InitList();
            calc = new Calculator(testSeeder.ecoController.GetList);

            var expected = 3599;
            var actual = calc.GetTotalExpenses();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalExpensesTest_Fail_Should()
        {
            testSeeder.InitList();
            testSeeder.ecoController.UpdateEconomicObjectAmount("Food", 999999);
            calc = new Calculator(testSeeder.ecoController.GetList);

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
        public void GetRemainingBalanceTest_Pass_ShouldReturnSum()
        {
            testSeeder.InitList();
            calc = new Calculator(testSeeder.ecoController.GetList);

            var expected = 9001;
            var actual = calc.GetRemainingBalance();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetRemainingBalanceTest_Fail_ShouldReturnZero()
        {
            testSeeder.InitList();
            testSeeder.ecoController.UpdateEconomicObjectAmount("Salary", 10000);
            testSeeder.ecoController.UpdateEconomicObjectAmount("Food", 8500);
            calc = new Calculator(testSeeder.ecoController.GetList);

            var expected = 0;
            var actual = calc.GetRemainingBalance();
            Assert.AreEqual(expected, actual);
        }
    }
}