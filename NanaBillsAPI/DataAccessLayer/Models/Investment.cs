using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Investment
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public int IdCategory { get; set; }
        public int IdUser { get; set; }
        public bool Closed { get; set; }
        public decimal? ClosedAmount { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int IdCurrency { get; set; }

        public virtual InvestmentCategory IdCategoryNavigation { get; set; } = null!;
        public virtual Currency IdCurrencyNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
