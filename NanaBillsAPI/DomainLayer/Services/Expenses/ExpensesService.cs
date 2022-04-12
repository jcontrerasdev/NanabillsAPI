using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.Expenses
{
    public class ExpensesService : GenericService<Expense, long>, IExpensesService
    {
        public ExpensesService(IGenericRepository<Expense, long> repository) : base(repository)
        {
        }
    }
}
