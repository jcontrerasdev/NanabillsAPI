using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Responses
{
    public class IncomeResponse
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public int IdCurrency { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int IdCategory { get; set; }
        public int IdUser { get; set; }
    }
}
