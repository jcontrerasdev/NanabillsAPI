using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class InvestmentCategory
    {
        public InvestmentCategory()
        {
            Investments = new HashSet<Investment>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int IdUser { get; set; }

        public virtual ICollection<Investment> Investments { get; set; }
    }
}
