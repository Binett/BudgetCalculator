using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator.Models
{
    public interface IPrivateEconomy
    {
        public double Balance { get; set; }
    }
}
