﻿using BudgetCalculator.Helpers;
using BudgetCalculatorTests1.Seeder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BudgetCalculator.Tests
{
    [TestClass()]
    public class BudgetReportTests
    {
        private Calculator calc;
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
            calc = new Calculator(seeder.ecoController);
            BudgetReport report = new BudgetReport(seeder.ecoController);
            var actual = report.GetCalculatedDataToString().Trim();
            WriteToFile writer = new WriteToFile();

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