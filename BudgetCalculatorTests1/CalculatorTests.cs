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
            List<EconomicOjbect> list = new List<EconomicOjbect>();

            list.Add(new EconomicOjbect
            {
                Name = "Rent",
                Amount = 200,
                Type = EconomicType.Expense,
            });

            calc = new Calculator(list);

        }

        [TestMethod()]
        public void GetTotalIncomeTest()
        {

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