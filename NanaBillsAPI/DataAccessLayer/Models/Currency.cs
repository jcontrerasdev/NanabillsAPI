using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Expenses = new HashSet<Expense>();
            Incomes = new HashSet<Income>();
            Investments = new HashSet<Investment>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
        public virtual ICollection<Investment> Investments { get; set; }
    }
}
