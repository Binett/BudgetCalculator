using NUnit.Framework;
using BudgetCalculator;
using BudgetCalculator.Models;
using System.Collections.Generic;

namespace NUnitTests
{
    public class Tests
    {
        Calculator calc;

        [SetUp]
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

        [Test]
        public void GetTotalIncomeTest()
        {
            Assert.AreEqual(0.0, calc.GetTotalIncome());
        }
    }
}