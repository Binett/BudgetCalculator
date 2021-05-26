using BudgetCalculator.Models;
using BudgetCalculatorTests1.Seeder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BudgetCalculator.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        private Calculator calc;
        private TestSeeder seeder;

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

            const int expected = 14000;
            var actual = calc.GetTotalIncome();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalIncomeTest_DoubleMaxValue_ShouldReturnZero()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);
            seeder.ecoController.UpdateEconomicObjectAmount("Salary", double.MaxValue);

            const int expected = 0;
            var actual = calc.GetTotalIncome();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalExpenses_PassValidSum_ShouldReturnSum_3599()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);
            const int expected = 3599;
            var actual = calc.GetTotalExpenses();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalExpenses_PassDoubleMaxValue_ShouldReturnZero()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);
            seeder.ecoController.UpdateEconomicObjectAmount("Food", double.MaxValue);
            const int expected = 0;
            var actual = calc.GetTotalExpenses();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalSavingTest_PositiveAmount_ShouldReturnSum()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Buffer", EconomicType.Saving, 0.15);
            calc = new Calculator(seeder.ecoController);
            const double expected = 0.25;
            var actual = calc.GetTotalSaving();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalSavingTest_NegativeAmount_ShouldReturnZero()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Buffer", EconomicType.Saving, -0.15);
            calc = new Calculator(seeder.ecoController);

            const double expected = 0.1d;
            var actual = calc.GetTotalSaving();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalSavingTest_MaxValue_ShouldReturnZero()
        {
            seeder.InitList();
            seeder.ecoController.UpdateEconomicObjectAmount("Saving", double.MaxValue);
            calc = new Calculator(seeder.ecoController);

            const int expected = 0;
            var actual = calc.GetTotalSaving();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetRemainingBalanceTest_SeederList_ShouldReturnSum()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);

            const int expected = 9001;
            var actual = calc.GetRemainingBalance(out _, out _);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetRemainingBalanceTest_NegativeHighPercentageSavings_ShouldReturnSum()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Fun stuff", EconomicType.Expense, 1000);
            seeder.ecoController.AddEconomicObjectToList("Motorcycle", EconomicType.Saving, 0.7);
            calc = new Calculator(seeder.ecoController);

            const int expected = 9401;
            var actual = calc.GetRemainingBalance(out _, out _);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetRemainingBalanceTest_NegativeHighValueExpense_ShouldReturnZero()
        {
            seeder.InitList();
            seeder.ecoController.UpdateEconomicObjectAmount("Salary", 10000);
            seeder.ecoController.UpdateEconomicObjectAmount("Food", 8500);
            calc = new Calculator(seeder.ecoController);

            const int expected = 0;
            var actual = calc.GetRemainingBalance(out _, out _);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetRemainingBalanceTest_NegativeList_ShouldContain1Unpaid()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Electric bill", EconomicType.Expense, 9001);
            seeder.ecoController.AddEconomicObjectToList("Fun stuff", EconomicType.Expense, 1000);
            calc = new Calculator(seeder.ecoController);
            calc.GetRemainingBalance(out _, out List<EconomicObject> listOfUnpaidExpenses);
            Assert.AreEqual(1, listOfUnpaidExpenses.Count);
        }

        [TestMethod()]
        public void GetRemainingBalanceTest_PositiveList_ShouldContain5Paid()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Electric bill", EconomicType.Expense, 500);
            calc = new Calculator(seeder.ecoController);
            calc.GetRemainingBalance(out List<EconomicObject> listOfPaidExpenses, out _);
            Assert.AreEqual(5, listOfPaidExpenses.Count);
        }

        [TestMethod()]
        public void GetRemainingBalanceTest_NegativeListSavings_ShouldContain1Unpaid()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Ima buy me some Solar cells", EconomicType.Saving, 0.70);
            calc = new Calculator(seeder.ecoController);
            calc.GetRemainingBalance(out _, out List<EconomicObject> listOfUnpaidExpenses);
            Assert.AreEqual("Ima buy me some Solar cells", listOfUnpaidExpenses[0].Name);
        }

        [TestMethod()]
        public void GetTotalSavingToMoneyTest_PassValue_ShouldReturn_3500()
        {
            seeder.InitList();
            seeder.ecoController.AddEconomicObjectToList("Buffer", EconomicType.Saving, 0.15);
            calc = new Calculator(seeder.ecoController);
            const int expected = 3500;
            var actual = calc.GetTotalSavingToMoney();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalSavingToMoneyTest_NegativeValue_ShouldReturnZero()
        {
            seeder.InitList();
            seeder.ecoController.UpdateEconomicObjectAmount("Saving", -0.15);
            calc = new Calculator(seeder.ecoController);
            const int expected = 1400;
            var actual = calc.GetTotalSavingToMoney();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalSavingToMoneyTest_MaxValue_ShouldReturnZero()
        {
            seeder.InitList();
            seeder.ecoController.UpdateEconomicObjectAmount("Saving", double.MaxValue);
            calc = new Calculator(seeder.ecoController);
            const int expected = 0;
            var actual = calc.GetTotalSavingToMoney();
            Assert.AreEqual(expected, actual);
        }
    }
}