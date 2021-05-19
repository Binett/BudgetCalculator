using System;
using System.Collections.Generic;
using System.Diagnostics;
using BudgetCalculator.Models;

namespace BudgetCalculator
{
    public class Calculator
    {
        private List<EconomicObject> economicObjectList;

        public Calculator(List<EconomicObject> list)
        {
            economicObjectList = list;
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
            return totalExpenses;
        }

        public double GetTotalSaving()
        {

            throw new NotImplementedException();
        }

        public double GetRemainingBalance()
        {
            try
            {
                if (IsMoreIncomeThanExpenses())
                {
                    var totalSavings = 0d;
                    foreach (var p in economicObjectList)
                    {
                        if (p.Type == EconomicType.Saving)
                        {
                            totalSavings += p.Amount;
                        }
                    }

                    var remainingBalance = GetTotalIncome() - GetTotalExpenses() - totalSavings;
                    if (remainingBalance > 0)
                    {
                        return remainingBalance;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return 0;
        }

        private bool IsMoreIncomeThanExpenses() => GetTotalIncome() > GetTotalExpenses();
    }
}
