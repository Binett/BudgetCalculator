using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetCalculatorTests1.Seeder;

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
        public void AddTest()
        {
            seeder.InitList();
            seeder.ecoController.UpdateEconomicObjectAmount("Rent", double.MaxValue);
            calc = new Calculator(seeder.ecoController);
            BudgetReport report = new BudgetReport(seeder.ecoController);


            var expected = "No Logs";
            var actual = ErrorLogger.GetSummarizedLogAsString();

            Assert.AreEqual(expected, actual);
        }
    }
}