using DataAccessLayer.Models;
using DTOs.Requests;
using DTOs.Responses;

namespace NanaBillsAPI.Profiles
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Expense, ExpenseResponse>();
            CreateMap<ExpenseRequest, Expense> ();
            CreateMap<Income, IncomeResponse>();
            CreateMap<IncomeRequest, Income>();
        }
    }
}
