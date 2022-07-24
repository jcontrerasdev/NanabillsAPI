using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Category
{
    public class ExpenseCategoryRepository : GenericRepository<ExpenseCategory, long>, IExpenseCategoryRepository
    {
        public ExpenseCategoryRepository(NanaBillsContext context) : base(context)
        {
        }
    }
}
