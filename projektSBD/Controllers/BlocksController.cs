﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace OracleApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlocksController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BlocksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-accident-count")]
        public async Task<IActionResult> GetAccidentCount([FromQuery] int carId)
        {
            try
            {
                var accidentCount = await _context.Accidents
                    .Where(a => a.CARID == carId)
                    .CountAsync();

                return Ok(new { CarID = carId, AccidentCount = accidentCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Failed to fetch accident count: {ex.Message}" });
            }
        }

        [HttpGet("get-total-service-cost")]
        public async Task<IActionResult> GetTotalServiceCost([FromQuery] int year)
        {
            try
            {
                var totalCost = await _context.ServiceHistories
                    .Where(sh => sh.SERVICEDATE.Year == year)
                    .SumAsync(sh => sh.COST);

                return Ok(new { Year = year, TotalServiceCost = totalCost });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Failed to fetch total service cost: {ex.Message}" });
            }
        }
    }
}
