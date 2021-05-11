using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetCalculator.Models;

namespace BudgetCalculator.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        Calculator calc;

        [TestInitialize]
        public void Setup()
        {
            var plist = new List<IPrivateEconomy>()
            {
                new IncomesDto { Salary = 2000, OtherIncomes = 200 },
                new ExpensesDto { },
                new SavingsDto { },
            };

            calc = new Calculator(plist);
        }

        [TestMethod()]
        public void GetTotalIncomeTest()
        {
            Assert.AreEqual(0.0, calc.GetTotalIncome());
        }

        [TestMethod()]
        public void GetTotalExpensesTest()
        {
            Assert.Fail();
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