using AutoMapper;
using DataAccessLayer.Models;
using DomainLayer.Services.Income;
using DTOs.Requests;
using DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NanaBillsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : BaseSecureController
    {
        IIncomeService IncomeService { get; set; }
        private readonly IMapper _mapper;

        public IncomeController(IIncomeService incomeService, IMapper mapper)
        {
            IncomeService = incomeService;
            _mapper = mapper;
        }

        // Get: api/Income
        [HttpGet]
        public async Task<IActionResult> GetIncomes()
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var incomes = await IncomeService.GetByUserId(x => x.IdUser == int.Parse(claim.Value));

                if (incomes == null || !incomes.Any())
                {
                    return NotFound($"No incomes were found");
                }
                var incomeDTO = _mapper.Map<IEnumerable<IncomeResponse>>(incomes);
                return Ok(incomeDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        // GET: api/Income/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeResponse>> GetIncome(long id)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null)
                {
                    return Unauthorized();
                }

                var income = await IncomeService.GetById(x => x.Id == id && x.IdUser == int.Parse(claim.Value));

                if (income == null) return NotFound();

                return Ok(_mapper.Map<IncomeResponse>(income));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // PUT: api/Income/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(long id, IncomeRequest income)
        {
            var claim = GetClaim();

            if (claim == null || !int.Parse(claim.Value).Equals(income.IdUser))
            {
                return Unauthorized();
            }

            if (id != income.Id)
            {
                return BadRequest("Income ID mismatch");
            }

            try
            {
                await IncomeService.Update(_mapper.Map<Income>(income));
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }

            return NoContent();
        }
        // POST: api/Income
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] IncomeRequest income)
        {
            try
            {
                var claim = GetClaim();

                if (claim == null || !int.Parse(claim.Value).Equals(income.IdUser))
                {
                    return Unauthorized();
                }

                if (income == null) return BadRequest();

                var createdIncome = await IncomeService.Insert(_mapper.Map<Income>(income));

                return CreatedAtAction(nameof(GetIncome), new { id = createdIncome.Id }, _mapper.Map<IncomeResponse>(createdIncome));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new expense record");
            }
        }
    }
}
