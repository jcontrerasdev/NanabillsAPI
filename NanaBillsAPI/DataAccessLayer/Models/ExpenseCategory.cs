using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class ExpenseCategory
    {
        public ExpenseCategory()
        {
            Expenses = new HashSet<Expense>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
