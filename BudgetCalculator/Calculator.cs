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

        public Calculator(EconomicController ecoController)
        {
            economicObjectList = ecoController.GetList;
        }
        
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
            return totalIncomes;
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
            if(totalExpenses < double.MaxValue)
            {
                return totalExpenses;
            }

            return 0;
        }

        /// <summary>
        /// Method for calculating the sum of all savings.
        /// If the reminding after all bills paid is less than the sum of savings,
        /// the percentage of saving will be drawn from the reminding.
        /// </summary>
        /// <returns>the sum of all savings</returns>
        public double GetTotalSaving()
        {
            if (IsMoreIncomeThanExpenses())
            {
                var amountLeftAfterExpenses = GetTotalIncome() - GetTotalExpenses();
                var totalAmountToSaving = 0.00;
                var amountToSave = 0.00;
                foreach (var p in economicObjectList)
                {
                        if (p.Type == EconomicType.Saving)
                        {
                            amountToSave = GetTotalIncome() * p.Amount;
                            amountLeftAfterExpenses -= amountToSave;
                            if (amountLeftAfterExpenses > totalAmountToSaving)
                            {
                                totalAmountToSaving += amountToSave;
                            }
                            else
                            {
                                //Log the savings which can't be done
                                Debug.WriteLine($"Saving {p.Name} can't be done");
                            }
                        }
                }
                return totalAmountToSaving;

                //if (amountToSave < double.MaxValue)
                //{
                //    if (amountToSave > amountLeftAfterExpenses)
                //    {
                //          return Math.Round(amountLeftAfterExpenses * totalSaving, 2);
                //    }
                //    return Math.Round(amountToSave, 2);

                //}
                //return 0;
            }
            return 0;
        }


        public double GetRemainingBalance()
        {
            if(IsMoreIncomeThanExpenses())
            {
                
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if the sum of income is more than the sum of expenses.
        /// </summary>
        /// <returns>true if income is more than expenses</returns>
        private bool IsMoreIncomeThanExpenses() => GetTotalIncome() > GetTotalExpenses();
    }
}
