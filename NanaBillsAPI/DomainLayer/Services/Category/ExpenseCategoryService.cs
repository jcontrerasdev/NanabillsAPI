using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace DomainLayer.Services.Category
{
    public class ExpenseCategoryService : GenericService<ExpenseCategory, long>, IExpenseCategoryService
    {
        public ExpenseCategoryService(IGenericRepository<ExpenseCategory, long> repository) : base(repository)
        {

        }
    }
}
