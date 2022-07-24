using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Category
{
    public class IncomeCategoryRepository : GenericRepository<IncomeCategory, long>, IIncomeCategoryRepository
    {
        public IncomeCategoryRepository(NanaBillsContext context) : base(context)
        {
        }
    }
}
