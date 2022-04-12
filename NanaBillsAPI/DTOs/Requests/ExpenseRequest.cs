using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Requests
{
    public class ExpenseRequest
    {
        [Required(ErrorMessage = "The expense Id is required")]
        public long Id { get; set; }
        [Required(ErrorMessage = "The expense amount is required")]
        public decimal Amount { get; set; }
        public int IdCurrency { get; set; }
        public bool Pay { get; set; }
        public string Description { get; set; }
        public int IdCategory { get; set; }
        public int IdUser { get; set; }
        public DateTime Date { get; set; }
    }
}
