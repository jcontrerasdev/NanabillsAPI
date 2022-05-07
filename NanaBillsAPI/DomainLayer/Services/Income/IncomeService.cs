using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.Income
{
    public class IncomeService : GenericService<DataAccessLayer.Models.Income, long>, IIncomeService
    {
        public IncomeService(IGenericRepository<DataAccessLayer.Models.Income, long> repository) : base(repository)
        {

        }
    }
}
