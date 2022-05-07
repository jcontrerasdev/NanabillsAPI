using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Income
{
    public class IncomeRepository : GenericRepository<DataAccessLayer.Models.Income, long>, IIncomeRepository
    {
        public IncomeRepository(NanaBillsContext context) : base(context)
        {

        }
    }
}
