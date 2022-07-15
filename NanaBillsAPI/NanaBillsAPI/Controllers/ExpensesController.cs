using AutoMapper;
using DataAccessLayer.Models;
using DomainLayer.Services.Expenses;
using DTOs.Requests;
using DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace NanaBillsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : BaseSecureController
    {
        IExpensesService ExpensesService { get; set; }
        private readonly IMapper _mapper;

        public ExpensesController(IExpensesService expensesService, IMapper mapper)
        {
            ExpensesService = expensesService;
            _mapper = mapper;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var expenses = await ExpensesService.GetByUserId(x => x.IdUser == int.Parse(claim.Value));

                if (expenses == null || !expenses.Any())
                {
                    return NotFound($"No expenses were found");
                }
                var expenseDTO = _mapper.Map<IEnumerable<ExpenseResponse>>(expenses);
                return Ok(expenseDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseResponse>> GetExpense(long id)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var expense = await ExpensesService.GetById(x => x.Id == id && x.IdUser == int.Parse(claim.Value));

                if (expense == null) return NotFound();

                return Ok(_mapper.Map<ExpenseResponse>(expense));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(long id, ExpenseRequest expense)
        {
            var claim = GetClaim();

            if (claim == null || !int.Parse(claim.Value).Equals(expense.IdUser))
            {
                return Unauthorized();
            }

            if (id != expense.Id)
            {
                return BadRequest("Expense ID mismatch");
            }

            try
            {

                //var expenseToUpdate = await ExpenseService.GetById(x => x.Id == id && x.IdUser == int.Parse(claim.Value));

                //if (expenseToUpdate == null)
                //    return NotFound($"Expense with Id = {id} not found");

                await ExpensesService.Update(_mapper.Map<Expense>(expense));
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }

            return NoContent();
        }

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseRequest expense)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null || !int.Parse(claim.Value).Equals(expense.IdUser))
                {
                    return Unauthorized();
                }

                if (expense == null) return BadRequest();

                var createdExpense = await ExpensesService.Insert(_mapper.Map<Expense>(expense));

                return CreatedAtAction(nameof(GetExpense), new { id = createdExpense.Id }, _mapper.Map<ExpenseResponse>(createdExpense));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new expense record");
            }
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpense(long id)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var expenseToDelete = await ExpensesService.GetById(x => x.Id == id && x.IdUser == int.Parse(claim.Value));

                if (expenseToDelete == null)
                {
                    return NotFound($"Expense with Id = {id} not found");
                }
                await ExpensesService.Delete(x => x.Id == id && x.IdUser == int.Parse(claim.Value));
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // Get: api/Expenses/search/text
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

                var result = await ExpensesService.Search(x => x.Description.Contains(description) && x.IdUser == int.Parse(claim.Value));

                if (result.Any()) return Ok(_mapper.Map<IEnumerable<ExpenseResponse>>(result));

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
