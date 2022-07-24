using DataAccessLayer.Models;
using DTOs.Requests;
using DTOs.Responses;

namespace NanaBillsAPI.Profiles
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            // Expense.
            CreateMap<Expense, ExpenseResponse>();
            CreateMap<ExpenseRequest, Expense> ();
            // Income.
            CreateMap<Income, IncomeResponse>();
            CreateMap<IncomeRequest, Income>();
            // Category.
            CreateMap<IncomeCategory, CategoryResponse>();
            CreateMap<CategoryRequest, IncomeCategory>();
            CreateMap<ExpenseCategory, CategoryResponse>();
            CreateMap<CategoryRequest, ExpenseCategory>();
        }
    }
}
