using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class IncomeCategory
    {
        public IncomeCategory()
        {
            Incomes = new HashSet<Income>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int IdUser { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }
    }
}
