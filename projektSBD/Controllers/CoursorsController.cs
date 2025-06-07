/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OracleApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-accident-counts")]
        public async Task<IActionResult> GetAccidentCounts()
        {
            try
            {
                var c_car_accident = _context.Cars
                    .Select(c => c.CARID);

                var results = new List<string>();
                foreach (var carId in await c_car_accident.ToListAsync())
                {
                    var v_count = await _context.Accidents
                        .CountAsync(a => a.CARID == carId);

                    if (v_count > 0)
                    {
                        results.Add($"Samochód {carId} miał {v_count} wypadków");
                    }
                }

                return Ok(new { Message = results.ToArray() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Failed to fetch accident counts: {ex.Message}" });
            }
        }


    }
}
*/