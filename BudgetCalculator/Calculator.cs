using System;
using System.Collections.Generic;
using System.Diagnostics;
using BudgetCalculator.Controllers;
using BudgetCalculator.Models;


namespace BudgetCalculator
{
    public class Calculator
    {
        private List<EconomicObject> economicObjectList;
        private const double maxPercentage = 1d;

        public Calculator(EconomicController ecoController)
        {
            economicObjectList = ecoController.GetList;
        }

        /// <summary>
        /// Method that calculates the total sum of incomes 
        /// </summary>
        /// <returns>the sum of all incomes</returns>
        public double GetTotalIncome()
        {
            double totalIncomes = 0;
            foreach (var p in economicObjectList)
            {
                if (p.Type == EconomicType.Income)
                {
                    totalIncomes += p.Amount;
                }
            }
            if (totalIncomes < double.MaxValue)
            {
                return totalIncomes;
            }

            return 0;
        }

        public double GetTotalExpenses()
        {
            double totalExpenses = 0;
            foreach (var p in economicObjectList)
            {
                if (p.Type == EconomicType.Expense)
                {
                    totalExpenses += p.Amount;
                }
            }
            if (totalExpenses < double.MaxValue)
            {
                return totalExpenses;
            }

            return 0;
        }


        /// <summary>
        /// Method for calculating the sum of all savings in percentage.
        /// </summary>
        /// <returns>The sum of all savings if the total percentage is less than 100%</returns>
        public double GetTotalSaving()
        {
            var totalSavingInPercentage = 0d;
            if (IsMoreIncomeThanExpenses())
            {
                foreach (var s in economicObjectList)
                {
                        if (s.Type == EconomicType.Saving && s.Amount < double.MaxValue)
                        {
                            totalSavingInPercentage += s.Amount;
                        }
                }

                if (CheckPercentageNeverExceedMax(totalSavingInPercentage) && totalSavingInPercentage < double.MaxValue)
                {
                    return totalSavingInPercentage;
                }
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// Calculates the remaining balance when all expenses has been made
        /// </summary>
        /// <returns>double, remaining balance</returns>
        public double GetRemainingBalance()
        {
            if (IsMoreIncomeThanExpenses())
            {
                var income = GetTotalIncome();
                var expenses = GetTotalExpenses();
                var savings = GetTotalSaving();

                var remainingBalance = income - expenses;
                if (remainingBalance > savings)
                {
                    remainingBalance -= savings;
                    return remainingBalance;
                }
            }

            Debug.WriteLine("Expenses exceed income");
            return 0;
        }

        /// <summary>
        /// Checks when the total income is exceeded and adds seperate object to list when it does not exceed income
        /// </summary>
        /// <returns>list of economic objects</returns>
        private List<EconomicObject> GetPaidExpensesList()
        {
            var listOfPaidExpenses = new List<EconomicObject>();

            var income = GetTotalIncome();
            var expenses = 0d;
            var amountSavings = 0d;
            foreach (var ecoObj in economicObjectList)
            {
                if (ecoObj.Type == EconomicType.Expense)
                {
                    expenses += ecoObj.Amount;

                    if (expenses > income)
                    {
                        Debug.WriteLine("expense exceeds income");
                        break;
                    }

                    listOfPaidExpenses.Add(ecoObj);
                }
                if(ecoObj.Type == EconomicType.Saving)
                {
                    amountSavings = ecoObj.Amount * income;
                    expenses += amountSavings;
                    if(expenses > income)
                    {
                        Debug.WriteLine("savings exceeds income");
                        break;
                    }

                    listOfPaidExpenses.Add(ecoObj);
                }
            }

            return listOfPaidExpenses;
        }

        /// <summary>
        /// Checks when total income is exceeded, when it is, it'll will add the exceeding expenses to list
        /// </summary>
        /// <returns>list of economic objects</returns>
        private List<EconomicObject> GetUnpaidExpensesList()
        {
            var listUnpaidExpenses = new List<EconomicObject>();

            var income = GetTotalIncome();
            var expenses = 0d;
            var amountSavings = 0d;

            foreach (var ecoObj in economicObjectList)
            {
                if (ecoObj.Type == EconomicType.Expense)
                {
                    expenses += ecoObj.Amount;
                    if (income < expenses)
                    {
                        listUnpaidExpenses.Add(ecoObj);
                    }
                }
                if(ecoObj.Type == EconomicType.Saving)
                {
                    amountSavings = ecoObj.Amount * income;
                    expenses += amountSavings;
                    if(income < expenses)
                    {
                        listUnpaidExpenses.Add(ecoObj);
                    }
                }
            }

            return listUnpaidExpenses;
        }

        #region Private

        /// <summary>
        /// Check if the sum of income is more than the sum of expenses.
        /// </summary>
        /// <returns>True if income is more than expenses.</returns>
        private bool IsMoreIncomeThanExpenses() => GetTotalIncome() > GetTotalExpenses();

        /// <summary>
        /// Check if saving is possible.
        /// </summary>
        /// <returns>True if the reminding is greater than the sum of saving.</returns>
        private bool IsSavingPossible() => CheckRemindingIsMoreThanSaving() && CheckPercentageNeverExceedMax(GetTotalSaving());

        /// <summary>
        /// Check if the percentage in parameter is exceeded maximum allowed.
        /// </summary>
        /// <param name="totalPercentage"></param>
        /// <returns>True if parameter is less than max.</returns>
        private bool CheckPercentageNeverExceedMax(double totalPercentage) => totalPercentage < maxPercentage;
        
        /// <summary>
        /// Check if reminding is more than the total value of saving.
        /// </summary>
        /// <returns>True if reminding is more than saving.</returns>
        private bool CheckRemindingIsMoreThanSaving() =>
            GetTotalIncome() - GetTotalExpenses() > GetTotalSavingToMoney();
        
        /// <summary>
        /// Convert from percentage to money in one specified saving post.
        /// </summary>
        /// <param name="name of saving"></param>
        /// <returns>The value in money of saving.</returns>
        private double GetSavingPercentageToMoney(string name)
        {
            var savingMoney = 0d;
            foreach (var o in economicObjectList)
            {
                if (o.Type == EconomicType.Saving && o.Name == name)
                {
                    savingMoney += GetTotalIncome() * o.Amount;
                    return savingMoney;
                }
            }
            return 0;
        }

        /// <summary>
        /// Convert the total percentage of saving into money.
        /// </summary>
        /// <returns>The sum of Saving value.</returns>
        private double GetTotalSavingToMoney() => GetTotalIncome() * GetTotalSaving();

        #endregion
    }
}
