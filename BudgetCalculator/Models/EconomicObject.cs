using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator.Models
{
    public class EconomicOjbect
    {
        public EconomicType Type { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}
