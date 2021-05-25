using BudgetCalculator.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetCalculator.Controllers.Tests
{
    [TestClass()]
    public class EconomicControllerTests
    {
        private EconomicController ecoController;

        [TestInitialize]
        public void Setup()
        {
            ecoController = new EconomicController();
            ecoController.AddEconomicObjectToList("Salary", EconomicType.Income, 2000);
        }

        [TestMethod()]
        public void AddEconomicObjectToListTest()
        {
        }

        [TestMethod()]
        [DataRow(null, 200, false)]
        [DataRow("", 200, false)]
        [DataRow(" ", 200, false)]
        [DataRow("Salary", -1200, false)]
        public void AddEconomicObjectToList_Negative_ShouldReturnFalse(
            string name, double amount, bool expected)
        {
            bool actual = ecoController.AddEconomicObjectToList(name, EconomicType.Income, amount);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddEconomicObjectToList_Pass_ShouldReturnTrue()
        {
            var actual = ecoController.AddEconomicObjectToList("Salad", EconomicType.Income, 200);
            const bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddEconomicObjectToList_PassNameThatExist_ShouldReturnFalse()
        {
            var actual = ecoController.AddEconomicObjectToList("Salary", EconomicType.Expense, 200);
            const bool expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(null, false)]
        [DataRow("", false)]
        [DataRow(" ", false)]
        public void RemoveEconomicObjectFromListTest_NameIsEmptyOrNull_ShouldReturnFalse(
            string name, bool expected)
        {
            var actual = ecoController.RemoveEconomicObjectFromList(name);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RemoveEconomicObjectFromListTest_Pass_ShouldReturnTrue()
        {
            var actual = ecoController.RemoveEconomicObjectFromList("Salary");
            const bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(null, 200, false)]
        [DataRow("", 200, false)]
        [DataRow(" ", 200, false)]
        [DataRow("Salary", -200, false)]
        public void UpdateEconomicObjectAmountTest_Negative_ShouldReturnFalse(
            string name, double amount, bool expected)
        {
            var actual = ecoController.UpdateEconomicObjectAmount(name, amount);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UpdateEconomicObjectAmountTest_Pass_ShouldReturnTrue()
        {
            var actual = ecoController.UpdateEconomicObjectAmount("Salary", 200);
            const bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(null, "Winnings", false)]
        [DataRow("", "Winnings", false)]
        [DataRow(" ", "Winnings", false)]
        [DataRow("Salary", null, false)]
        [DataRow("Salary", "", false)]
        [DataRow("Salary", " ", false)]
        [DataRow("Winnings", "Salary", false)]
        public void UpdateEconomicObjectNameTest_Negative_ShouldReturnFalse(
            string oldName, string newName, bool expected)
        {
            var actual = ecoController.UpdateEconomicObjectName(oldName, newName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UpdateEconomicObjectNameTest_Pass_ShouldReturnTrue()
        {
            var actual = ecoController.UpdateEconomicObjectName("Salary", "Winnings");
            const bool expected = true;

            Assert.AreEqual(expected, actual);
        }
    }
}