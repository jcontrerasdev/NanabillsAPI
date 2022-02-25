using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Expense
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public int IdCurrency { get; set; }
        public bool Pay { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public int IdCategory { get; set; }
        public int IdUser { get; set; }

        public virtual ExpenseCategory IdCategoryNavigation { get; set; } = null!;
        public virtual Currency IdCurrencyNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
