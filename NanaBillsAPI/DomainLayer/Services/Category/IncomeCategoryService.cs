using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace DomainLayer.Services.Category
{
    public class IncomeCategoryService : GenericService<IncomeCategory, long>, IIncomeCategoryService
    {
        public IncomeCategoryService(IGenericRepository<IncomeCategory, long> repository) : base(repository)
        {

        }
    }
}
