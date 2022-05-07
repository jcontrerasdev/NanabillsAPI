using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Expense
{
    public class ExpenseRepository : GenericRepository<DataAccessLayer.Models.Expense, long>, IExpenseRepository
    {
        public ExpenseRepository(NanaBillsContext context) : base(context)
        {
        }
    }
}
