using BudgetCalculator.Controllers;
using BudgetCalculator.Helpers;
using BudgetCalculator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

            string errormsg = $"{this} GetTotalIncome got double.maxvalue";
            Debug.WriteLine(errormsg);
            ErrorLogger.Add(errormsg);
            return 0;
        }

        /// <summary>
        /// Returns all expenses from the economic Object list in this calculator class.
        /// </summary>
        /// <returns>double as total expenses</returns>
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

            string errormsg = $"{this} GetTotalExpenses got double.maxvalue";
            Debug.WriteLine(errormsg);
            ErrorLogger.Add(errormsg);
            return 0;
        }

        /// <summary>
        /// Method for calculating the sum of all savings in percentage.
        /// </summary>
        /// <returns>The sum of all savings if the total percentage is less than 100%</returns>
        public double GetTotalSaving()
        {
            var totalSavingInPercentage = 0d;
            string errormsg;
            if (IsMoreIncomeThanExpenses())
            {
                foreach (var s in economicObjectList)
                {
                    if (s.Type == EconomicType.Saving)
                    {
                        if (s.Amount < double.MaxValue)
                        {
                            totalSavingInPercentage += s.Amount;
                        }
                        else
                        {
                            errormsg = $"{this} Saving amount was more than double.MaxValue";
                            Debug.WriteLine(errormsg);
                            ErrorLogger.Add(errormsg);
                        }
                    }
                }

                if (CheckPercentageNeverExceedMax(totalSavingInPercentage))
                {
                    return totalSavingInPercentage;
                }

                errormsg = $"{this} Total saving percentage was over 100";
                Debug.WriteLine(errormsg);
                ErrorLogger.Add(errormsg);
                return 0;
            }

            errormsg = $"{this} Less income than expenses";
            Debug.WriteLine(errormsg);
            ErrorLogger.Add(errormsg);
            return 0;
        }

        /// <summary>
        /// Calculates the remaining balance when all expenses has been made
        /// </summary>
        /// <returns>double, remaining balance</returns>
        public double GetRemainingBalance(out List<EconomicObject> listOfPaidExpenses, out List<EconomicObject> listOfUnpaidExpenses)
        {
            listOfPaidExpenses = GetPaidExpensesList();
            listOfUnpaidExpenses = GetUnpaidExpensesList();
            string errormsg;
            if (IsMoreIncomeThanExpenses())
            {
                var income = GetTotalIncome();
                var expenses = GetTotalExpenses();
                var savings = GetTotalSavingToMoney();

                var remainingBalance = income - expenses;
                if (IsSavingPossible())
                {
                    remainingBalance -= savings;
                    return remainingBalance;
                }
                else
                {
                    errormsg = $"{this} Saving is not possible";
                    Debug.WriteLine(errormsg);
                    ErrorLogger.Add(errormsg);
                    return 0;
                }
            }

            errormsg = $"{this} Expenses exceed income";
            Debug.WriteLine(errormsg);
            ErrorLogger.Add(errormsg);
            return 0;
        }

        /// <summary>
        /// Convert the total percentage of saving into money.
        /// </summary>
        /// <returns>The sum of Saving value.</returns>
        public double GetTotalSavingToMoney() => Math.Round(GetTotalIncome() * GetTotalSaving(), 2);

        #region Private

        /// <summary>
        /// Checks when the total income is exceeded and adds seperate object to list when it does not exceed income
        /// </summary>
        /// <returns>list of economic objects</returns>
        private List<EconomicObject> GetPaidExpensesList()
        {
            var listOfPaidExpenses = new List<EconomicObject>();
            string errormsg;
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
                        break;
                    }

                    listOfPaidExpenses.Add(ecoObj);
                }
                if (ecoObj.Type == EconomicType.Saving)
                {
                    amountSavings = ecoObj.Amount * income;
                    expenses += amountSavings;
                    if (expenses > income)
                    {
                        errormsg = $"{this} Savings exceed income";
                        Debug.WriteLine(errormsg);
                        ErrorLogger.Add(errormsg);
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
                if (ecoObj.Type == EconomicType.Saving)
                {
                    double amountSavings = ecoObj.Amount * income;
                    expenses += amountSavings;
                    if (income < expenses)
                    {
                        listUnpaidExpenses.Add(ecoObj);
                    }
                }
            }

            return listUnpaidExpenses;
        }

        /// <summary>
        /// Check if the sum of income is more than the sum of expenses.
        /// </summary>
        /// <returns>True if income is more than expenses.</returns>
        private bool IsMoreIncomeThanExpenses() => GetTotalIncome() > GetTotalExpenses();

        /// <summary>
        /// Check if saving is possible.
        /// </summary>
        /// <returns>True if the remaining is greater than the sum of saving.</returns>
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
        /// <returns>True if remaining is more than saving.</returns>
        private bool CheckRemindingIsMoreThanSaving() => GetTotalIncome() - GetTotalExpenses() > GetTotalSavingToMoney();

        #endregion Private
    }
}