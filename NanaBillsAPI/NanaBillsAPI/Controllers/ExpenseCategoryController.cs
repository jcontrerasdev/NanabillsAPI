using AutoMapper;
using DataAccessLayer.Models;
using DomainLayer.Services.Category;
using DTOs.Requests;
using DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NanaBillsAPI.Controllers
{
    public class ExpenseCategoryController : BaseSecureController
    {
        IExpenseCategoryService ExpenseCategoryService { get; set; }
        private readonly IMapper _mapper;

        public ExpenseCategoryController(IExpenseCategoryService expensesService, IMapper mapper)
        {
            ExpenseCategoryService = expensesService;
            _mapper = mapper;
        }
        // GET: api/ExpenseCategory
        [HttpGet]
        public async Task<IActionResult> GetExpenseCategory()
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var expenseCategory = await ExpenseCategoryService.GetByUserId(x => x.IdUser == int.Parse(claim.Value));

                if (expenseCategory == null || !expenseCategory.Any())
                {
                    return NotFound($"No expenses category were found");
                }
                var CategoriesDTO = _mapper.Map<IEnumerable<CategoryResponse>>(expenseCategory);
                return Ok(CategoriesDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/ExpenseCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetExpenseCategory(long id)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var category = await ExpenseCategoryService.GetById(x => x.Id == id && x.IdUser == int.Parse(claim.Value));

                if (category == null) return NotFound();

                return Ok(_mapper.Map<CategoryResponse>(category));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST: api/ExpenseCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateExpenseCategory([FromBody] CategoryRequest category)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null || !int.Parse(claim.Value).Equals(category.IdUser))
                {
                    return Unauthorized();
                }

                if (category == null) return BadRequest();

                var createdCategory = await ExpenseCategoryService.Insert(_mapper.Map<ExpenseCategory>(category));

                return CreatedAtAction(nameof(GetExpenseCategory), new { id = createdCategory.Id }, _mapper.Map<CategoryResponse>(createdCategory));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new category record");
            }
        }

        // PUT: api/ExpenseCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpenseCategory(long id, CategoryRequest category)
        {
            var claim = GetClaim();

            if (claim == null || !int.Parse(claim.Value).Equals(category.IdUser))
            {
                return Unauthorized();
            }

            if (id != category.Id)
            {
                return BadRequest("Category ID mismatch");
            }

            try
            {
                await ExpenseCategoryService.Update(_mapper.Map<ExpenseCategory>(category));
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }

            return NoContent();
        }
        // DELETE: api/ExpenseCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(long id)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var expenseCategoryToDelete = await ExpenseCategoryService.GetById(x => x.Id == id && x.IdUser == int.Parse(claim.Value));

                if (expenseCategoryToDelete == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }
                await ExpenseCategoryService.Delete(x => x.Id == id && x.IdUser == int.Parse(claim.Value));
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // Get: api/ExpenseCategory/search/text
        [HttpGet("{search}/{description}")]
        public async Task<IActionResult> Search(string description)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var result = await ExpenseCategoryService.Search(x => x.Name.Contains(description) && x.IdUser == int.Parse(claim.Value));

                if (result.Any()) return Ok(_mapper.Map<IEnumerable<CategoryResponse>>(result));

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
    }
}
