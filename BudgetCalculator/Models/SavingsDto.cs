namespace BudgetCalculator.Models
{
    public struct SavingsDto : IPrivateEconomy
    {
        public double SavingPercent;
        public double SavingsBuffer;
        public double Pension;
        public double Balance { get; set; }
    }
}
