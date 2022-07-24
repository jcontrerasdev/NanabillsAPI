using DataAccessLayer.Models;
using DataAccessLayer.Repositories;


namespace DomainLayer.Services.Expenses
{
    public class ExpensesService : GenericService<Expense, long>, IExpensesService
    {
        public ExpensesService(IGenericRepository<Expense, long> repository) : base(repository)
        {
        }
    }
}
