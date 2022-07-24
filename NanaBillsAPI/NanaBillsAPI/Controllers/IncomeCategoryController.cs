using AutoMapper;
using DataAccessLayer.Models;
using DomainLayer.Services.Category;
using DTOs.Requests;
using DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NanaBillsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeCategoryController : BaseSecureController
    {
        IIncomeCategoryService IncomeCategoryService { get; set; }
        private readonly IMapper _mapper;

        public IncomeCategoryController(IIncomeCategoryService incomeService, IMapper mapper)
        {
            IncomeCategoryService = incomeService;
            _mapper = mapper;
        }
        // GET: api/IncomeCategory
        [HttpGet]
        public async Task<IActionResult> GetIncomeCategory()
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var incomeCategory = await IncomeCategoryService.GetByUserId(x => x.IdUser == int.Parse(claim.Value));

                if (incomeCategory == null || !incomeCategory.Any())
                {
                    return NotFound($"No income categories were found");
                }
                var CategoriesDTO = _mapper.Map<IEnumerable<CategoryResponse>>(incomeCategory);
                return Ok(CategoriesDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/IncomeCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(long id)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var category = await IncomeCategoryService.GetById(x => x.Id == id && x.IdUser == int.Parse(claim.Value));

                if (category == null) return NotFound();

                return Ok(_mapper.Map<CategoryResponse>(category));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST: api/IncomeCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest category)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null || !int.Parse(claim.Value).Equals(category.IdUser))
                {
                    return Unauthorized();
                }

                if (category == null) return BadRequest();

                var createdCategory = await IncomeCategoryService.Insert(_mapper.Map<IncomeCategory>(category));

                return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, _mapper.Map<CategoryResponse>(createdCategory));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new category record");
            }
        }

        // PUT: api/IncomeCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(long id, CategoryRequest category)
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
                await IncomeCategoryService.Update(_mapper.Map<IncomeCategory>(category));
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }

            return NoContent();
        }
        // DELETE: api/IncomeCategory/5
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

                var categoryToDelete = await IncomeCategoryService.GetById(x => x.Id == id && x.IdUser == int.Parse(claim.Value));

                if (categoryToDelete == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }
                await IncomeCategoryService.Delete(x => x.Id == id && x.IdUser == int.Parse(claim.Value));
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // Get: api/IncomeCategory/search/text
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

                var result = await IncomeCategoryService.Search(x => x.Name.Contains(description) && x.IdUser == int.Parse(claim.Value));

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
