using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetCalculator.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetCalculatorTests1.Seeder;
using BudgetCalculator.Models;

namespace BudgetCalculator.Controllers.Tests
{
    [TestClass()]
    public class EconomicControllerTests
    {
        [TestMethod()]
        public void AddEconomicObjectToListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddEconomicObjectToList_PassInvalidNullString_ShouldReturnFalse()
        {
            EconomicController ecoC = new EconomicController();
            bool actual = ecoC.AddEconomicObjectToList(null, Models.EconomicType.Expense, 0.1);
            Assert.AreEqual(false, actual);
        }
    }
}