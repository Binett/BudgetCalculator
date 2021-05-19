using System;
using System.Collections.Generic;
using BudgetCalculator.Models;

namespace BudgetCalculator
{
    public class Calculator
    {
        private List<EconomicOjbect> economicObjectList;

        public Calculator(List<EconomicOjbect> list)
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
            //if (IsMoreIncomeThanExpenses())
            //{
            //    double totalSavings = 0;
            //    foreach (var p in economicObjectList)
            //    {
            //        if (p.Type == EconomicType.Saving)
            //        {
            //            totalSavings += p.Amount;
            //        }
            //    }

            //    double amountLeftAfterExpenses = GetTotalIncome() - GetTotalExpenses();
            //    double amountToSave = GetTotalIncome() * totalSavings;

            //    if(amountToSave > amountLeftAfterExpenses)
            //    {
            //        //no money to save
            //        return 0;
            //    }
            //    else
            //    {
            //        //money exist to for saving
            //        return amountToSave;
            //    }
            //}
            //else
            //{
            //    return 0;
            //}
            throw new NotImplementedException();
        }

        public double GetRemainingBalance()
        {
            if(IsMoreIncomeThanExpenses())
            {
                
            }
            throw new NotImplementedException();
        }

        private bool IsMoreIncomeThanExpenses() => GetTotalIncome() > GetTotalExpenses();
    }
}
