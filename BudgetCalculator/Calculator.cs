using BudgetCalculator.Controllers;
using BudgetCalculator.Helpers;
using BudgetCalculator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BudgetCalculator
{
    /// <summary>
    /// Class is used to calculate income, expenses and savings in varies ways.
    /// </summary>
    public class Calculator
    {
        private readonly List<EconomicObject> economicObjectList;
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
            if (GetTotalIncome() > GetTotalExpenses())
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

                        if(totalSavingInPercentage >= 1)
                        {
                            errormsg = $"{this} Total saving percentage was over 100";
                            Debug.WriteLine(errormsg);
                            ErrorLogger.Add(errormsg);
                            return 0;
                        }
                    }
                }

                return totalSavingInPercentage;
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
            var income = GetTotalIncome();
            var expenses = GetTotalExpenses();
            var savings = GetTotalMoneyForSaving();
            if (income > expenses)
            {
                var remainingBalance = income - expenses;

                if (remainingBalance >= savings)
                {
                    remainingBalance -= savings;
                }
                else
                {
                    errormsg = $"{this} Saving is not possible";
                    Debug.WriteLine(errormsg);
                    ErrorLogger.Add(errormsg);
                }

                return remainingBalance;
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
        public double GetTotalMoneyForSaving() => Math.Round(GetTotalIncome() * GetTotalSaving(), 2);

        #region Private

        /// <summary>
        /// Checks when the total income is exceeded and adds seperate object to list when it does not exceed income
        /// </summary>
        /// <returns>list of economic objects</returns>
        private List<EconomicObject> GetPaidExpensesList()
        {
            //TODO refaktorera.
            //Kolla om ett objekt är BETALBART, om så lägg i lista betalade och minus income, annars ej betalt -> unpayed list


            var listOfPaidExpenses = new List<EconomicObject>();
            string errormsg;
            var income = GetTotalIncome();
            var expenses = 0d;
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
                    double amountSavings = ecoObj.Amount * income;
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

        #endregion Private
    }
}