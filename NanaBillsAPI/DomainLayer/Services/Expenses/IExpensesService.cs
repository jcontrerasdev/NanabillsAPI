using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.Expenses
{
    public interface IExpensesService : IGenericService<Expense, long>
    {
    }
}
