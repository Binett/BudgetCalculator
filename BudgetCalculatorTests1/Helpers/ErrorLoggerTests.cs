using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetCalculatorTests1.Seeder;
using System;

namespace BudgetCalculator.Helpers.Tests
{
    [TestClass()]
    public class ErrorLoggerTests
    {
        private Calculator calc;
        private TestSeeder seeder;

        [TestInitialize]
        public void SetUp()
        {
            seeder = new TestSeeder();
        }

        [TestMethod()]
        public void GetSummarizedLogAsStringTest_NoErrors_ShouldReturnNoLogs()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);
            BudgetReport report = new BudgetReport(seeder.ecoController);

            var expected = "No Logs";
            var actual = ErrorLogger.GetSummarizedLogAsString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetSummarizedLogAsStringTest_UpdateEconomicObjectWithEmptyName_ShouldReturnLogMessageStringWasEmpty()
        {
            seeder.InitList();
            seeder.ecoController.UpdateEconomicObjectName("Rent", "");
            calc = new Calculator(seeder.ecoController);
            BudgetReport report = new BudgetReport(seeder.ecoController);

            var expected = "1: " + DateTime.Now + " BudgetCalculator.Controllers.EconomicController String name was empty\n";
            var actual = ErrorLogger.GetSummarizedLogAsString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddTest_AddEconomicObjectWithDoubleMaxValue_ShouldReturnAnErrorInLogList()
        {
            seeder.InitList();
            seeder.ecoController.UpdateEconomicObjectAmount("Rent", double.MaxValue);
            calc = new Calculator(seeder.ecoController);
            BudgetReport report = new BudgetReport(seeder.ecoController);

            var expected = "BudgetCalculator.Controllers.EconomicController String name was empty";
            var actual = ErrorLogger.GetLogList()[0];
            Assert.AreEqual(expected, actual);
        }
    }
}