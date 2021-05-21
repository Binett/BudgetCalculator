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
    public class    CalculatorTests
    {
        Calculator calc;
        TestSeeder seeder;

        [TestInitialize]
        public void Setup()
        {
            seeder = new TestSeeder();
            
        }

        [TestMethod()]
        public void GetTotalIncomeTest_Pass_ShouldReturnSum_14000() 
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);

            var expected = 14000;
            var actual = calc.GetTotalIncome();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalIncomeTest_DoubleMaxValue_ShouldReturnZero()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);
            seeder.ecoController.UpdateEconomicObjectAmount("Salary", double.MaxValue);

            var expected = 0;
            var actual = calc.GetTotalIncome();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalExpenses_PassValidSum_ShouldReturnSum_3599()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);

            var expected = 3599;
            var actual = calc.GetTotalExpenses();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalExpenses_PassDoubleMaxValue_ShouldReturnZero()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);
            seeder.ecoController.UpdateEconomicObjectAmount("Food", double.MaxValue);
            var expected = 0;
            var actual = calc.GetTotalExpenses();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalSavingTest_PositiveAmount_ShouldReturnSum()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Buffer", EconomicType.Saving, 0.15);
            calc = new Calculator(seeder.ecoController);
            var expected = 3500;
            var actual = calc.GetTotalSaving();
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void GetTotalSavingTest_NegativeAmount_ShouldReturnZero()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Buffer", EconomicType.Saving, -0.15);
            calc = new Calculator(seeder.ecoController);

            var expected = 1400;
            var actual = calc.GetTotalSaving();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalSavingTest_MaxValue_ShouldReturnZero()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Buffer", EconomicType.Saving, Double.MaxValue);
            calc = new Calculator(seeder.ecoController);

            var expected = 0;
            var actual = calc.GetTotalSaving();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetRemainingBalanceTest_Pass_ShouldReturnSum()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);

            var expected = 9001;
            var actual = calc.GetRemainingBalance();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetRemainingBalanceTest_Fail_ShouldReturnZero()
        {
            seeder.InitList();
            seeder.ecoController.UpdateEconomicObjectAmount("Salary", 10000);
            seeder.ecoController.UpdateEconomicObjectAmount("Food", 8500);
            calc = new Calculator(seeder.ecoController);

            var expected = 0;
            var actual = calc.GetRemainingBalance();
            Assert.AreEqual(expected, actual);
        }
    }
}