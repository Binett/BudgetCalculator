﻿using System;
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
            if (IsMoreIncomeThanExpenses())
            {
                double amountLeftAfterExpenses = GetTotalIncome() - GetTotalExpenses();
                double totalSaving = 0;
                foreach (var p in economicObjectList)
                {
                    if (p.Type == EconomicType.Saving)
                    {
                        totalSaving += p.Amount;
                    }
                }

                double amountToSave = GetTotalIncome() * totalSaving;
                if (amountToSave > amountLeftAfterExpenses)
                {
                    return Math.Round(amountLeftAfterExpenses * totalSaving, 2);
                }
                else
                {
                    return Math.Round(amountToSave, 2);
                }
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

        private bool IsMoreIncomeThanExpenses() => GetTotalIncome() > GetTotalExpenses();
    }
}
