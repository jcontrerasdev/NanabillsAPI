using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class User
    {
        public User()
        {
            Expenses = new HashSet<Expense>();
            Incomes = new HashSet<Income>();
            Investments = new HashSet<Investment>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
        public virtual ICollection<Investment> Investments { get; set; }
    }
}
