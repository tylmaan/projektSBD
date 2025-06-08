using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projektSBD.Models.Pagination;
using projektSBD.Models.Views;
using System.Reflection;

namespace projektSBD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ViewsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("car-lookup")]
        public async Task<ActionResult<IEnumerable<CarOwnerLookupView>>> GetCarLookup([FromQuery] string brand = "", [FromQuery] string model = "")
        {
            var query = _context.CarOwnerLookupView.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
                query = query.Where(x => x.BRAND.ToLower().Contains(brand.ToLower()));

            if (!string.IsNullOrWhiteSpace(model))
                query = query.Where(x => x.MODEL.ToLower().Contains(model.ToLower()));

            return await query.ToListAsync();
        }

        
        [HttpGet("claims-with-accidents")]
        public async Task<ActionResult<PagedResult<ClaimWithAccidentView>>> GetClaimsWithAccidents(
            [FromQuery] string status = "",
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.ClaimWithAccidentView.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(c => c.STATUS.ToLower() == status.ToLower());

            var totalCount = await query.CountAsync();

            var paginatedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<ClaimWithAccidentView>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = paginatedData
            };

            return Ok(result);
        }


        [HttpGet("owner-car-history")]
        public async Task<ActionResult<IEnumerable<OwnerCarHistoryView>>> GetOwnerCarHistory([FromQuery] int ownerId)
        {
            try
            {
                var results = await _context.OwnerCarHistoryView
                    .Where(x => x.OWNERID == ownerId)
                    .ToListAsync();

                if (!results.Any())
                    return NotFound($"No history found for owner ID {ownerId}");

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
    }
}
