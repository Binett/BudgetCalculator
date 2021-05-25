using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetCalculator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetCalculator.Controllers;
using BudgetCalculator.Helpers;
using BudgetCalculatorTests1.Seeder;

namespace BudgetCalculator.Tests
{
    [TestClass()]
    public class BudgetReportTests
    {
        Calculator calc;
        TestSeeder seeder;

        [TestInitialize]
        public void SetUp()
        {
            seeder = new TestSeeder();
        }
        
        [TestMethod()]
        public void GetCalculatedDataToStringTest_BudgetReport_ShouldWriteToFile()
        {
            seeder.InitList();
            calc = new Calculator(seeder.ecoController);
            BudgetReport report = new BudgetReport(seeder.ecoController);

            var filetxt = new WriteToFile();
            var reportString = report.GetCalculatedDataToString(seeder.ecoController);
            filetxt.WriteStringToFile("Budget", reportString);

            var expected = File.ReadAllLines(filetxt.PathAndFileName);
            Assert.AreEqual(expected, reportString);
        }
    }
}