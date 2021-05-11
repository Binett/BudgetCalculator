namespace BudgetCalculator.Models
{
    public struct IncomesDto : IPrivateEconomy
    {
        public double Salary;
        public double OtherIncomes;
        public double Balance { get; set; }
    }
}
