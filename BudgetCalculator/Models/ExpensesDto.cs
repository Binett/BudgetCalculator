using System.Collections.Generic;

namespace BudgetCalculator.Models
{
    public struct ExpensesDto : IPrivateEconomy
    {
        public double Rent;
        public double MobileSub;
        public double OtherSubscriptions;
        public double Food;
        public double Insurances;
        public double Loans;
        public double GymSub;
        public double Balance { get; set; }
    }
}