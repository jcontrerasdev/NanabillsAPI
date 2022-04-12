using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Expense
{
    public class ExpenseRepository : GenericRepository<DataAccessLayer.Models.Expense, long>, IExpenseRepository
    {
        public ExpenseRepository(NanaBillsContext context) : base(context)
        {
        }
    }
}
