using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using projektSBD.Models.insurance_pkg;
using System.Data;

namespace projektSBD.Controllers
{
    public class ClaimController : Controller
    {
        private readonly AppDbContext _context;

        public ClaimController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("insurance/{carId}")]
        public async Task<ActionResult<IEnumerable<InsurancePolicyDto>>> GetInsuranceHistory(int carId)
        {
            var result = new List<InsurancePolicyDto>();

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "CLAIM_PKG.GetInsuranceHistory";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("p_car_id", OracleDbType.Int32, carId, ParameterDirection.Input));
            cmd.Parameters.Add(new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output));

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new InsurancePolicyDto
                {
                    POLICYID = reader.GetInt32(0),
                    COVERAGEAMOUNT = reader.GetDecimal(1),
                    PREMIUM = reader.GetDecimal(2),
                    STARTDATE = reader.GetDateTime(3),
                    ENDDATE = reader.GetDateTime(4),
                    POLICYSTATUS = reader.GetString(5)
                });
            }
            return Ok(result);
        }

        [HttpGet("claims/{carId}")]
        public async Task<ActionResult<IEnumerable<ClaimDto>>> GetClaimHistory(int carId)
        {
            var result = new List<ClaimDto>();

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "CLAIM_PKG.GetClaimHistory";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("p_car_id", OracleDbType.Int32, carId, ParameterDirection.Input));
            cmd.Parameters.Add(new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output));

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new ClaimDto
                {
                    CLAIMID = reader.GetInt32(0),
                    CLAIMDATE = reader.GetDateTime(1),
                    STATUS = reader.GetString(2),
                    PAYOUTAMOUNT = reader.GetDecimal(3)
                });
            }
            return Ok(result);
        }
    }
}
