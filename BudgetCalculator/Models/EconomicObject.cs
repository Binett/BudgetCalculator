namespace BudgetCalculator.Models
{
    /// <summary>
    /// A DTO of economic objects
    /// </summary>
    public class EconomicObject
    {
        public EconomicType Type { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}