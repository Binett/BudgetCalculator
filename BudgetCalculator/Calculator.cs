using System;
using System.Collections.Generic;
using BudgetCalculator.Models;

namespace BudgetCalculator
{
    public class Calculator
    {
        List<IPrivateEconomy> privateEconomyList;

        public Calculator(List<IPrivateEconomy> list)
        {
            privateEconomyList = list;
        }

        public double GetTotalIncome()
        {
            throw new NotImplementedException();
        }

        public double GetTotalExpenses()
        {
            throw new NotImplementedException();
        }

        public double GetTotalSaving()
        {
            throw new NotImplementedException();
        }

        public double GetRemainingBalance()
        {
            throw new NotImplementedException();
        }


    }
}
