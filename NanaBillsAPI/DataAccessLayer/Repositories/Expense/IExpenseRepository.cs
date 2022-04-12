using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Expense
{
    public interface IExpenseRepository : IGenericRepository<DataAccessLayer.Models.Expense, long>
    {
    }
}
