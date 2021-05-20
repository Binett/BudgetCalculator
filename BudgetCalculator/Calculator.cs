using System;
using System.Collections.Generic;
using BudgetCalculator.Models;
using BudgetCalculator.Controllers;

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

        public double GetTotalSaving()
        {

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
