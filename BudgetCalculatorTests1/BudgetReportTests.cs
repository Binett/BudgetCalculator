using BudgetCalculator.Helpers;
using BudgetCalculatorTests1.Seeder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BudgetCalculator.Tests
{
    [TestClass()]
    public class BudgetReportTests
    {
        private TestSeeder seeder;

        [TestInitialize]
        public void SetUp()
        {
            seeder = new TestSeeder();
        }

        [TestMethod()]
        public void GetCalculatedDataToStringTest_SendInEcoController_ReturnsCollectedDataAsString()
        {
            seeder.InitList();
            BudgetReport report = new(seeder.ecoController);
            var actual = report.GetCalculatedDataToString().Trim();
            WriteToFile writer = new();

            //In case file already exists--
            writer.WriteStringToFile("test file", report.GetCalculatedDataToString());
            File.Delete(writer.PathAndFileName);
            //--

            writer.WriteStringToFile("test file", report.GetCalculatedDataToString());
            string expected = File.ReadAllText(writer.PathAndFileName).Trim();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetCalculatedDataToStringTest_Null_ReturnsEmptyListOfString()
        {           
            BudgetReport report = new(null);
            var actual = report.GetCalculatedDataToString().Trim();
            WriteToFile writer = new();

            //In case file already exists--
            writer.WriteStringToFile("test file", report.GetCalculatedDataToString());
            File.Delete(writer.PathAndFileName);
            //--

            writer.WriteStringToFile("test file", report.GetCalculatedDataToString());
            string expected = File.ReadAllText(writer.PathAndFileName).Trim();

            Assert.AreEqual(expected, actual);
        }
    }
}