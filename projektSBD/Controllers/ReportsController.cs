using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using projektSBD.Models.plsql;
using System.Threading.Tasks;

namespace projektSBD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("accidents-ref/{carId}")]
        public async Task<ActionResult<IEnumerable<CarAccident>>> GetCarAccidentsRef(int carId)
        {
            var result = new List<CarAccident>();

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "REPORT_PKG.GetCarAccidents";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("p_car_id", OracleDbType.Int32)
            {
                Direction = System.Data.ParameterDirection.Input,
                Value = carId
            });

            var cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            cmd.Parameters.Add(cursor);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new CarAccident
                {
                    ACCIDENTDATE = reader.GetDateTime(0),
                    LOCATION = reader.GetString(1),
                    DAMAGEDETAILS = reader.GetString(2)
                });
            }

            return Ok(result);
        }

        [HttpGet("repairs-ref/{carId}")]
        public async Task<ActionResult<IEnumerable<CarRepair>>> GetCarRepairsRef(int carId)
        {
            var result = new List<CarRepair>();

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "REPORT_PKG.GetCarRepairs";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("p_car_id", OracleDbType.Int32)
            {
                Direction = System.Data.ParameterDirection.Input,
                Value = carId
            });

            var cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            cmd.Parameters.Add(cursor);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new CarRepair
                {
                    SERVICEDATE = reader.GetDateTime(0),
                    DESCRIPTION = reader.GetString(1),
                    COST = reader.GetDecimal(2)
                });
            }

            return Ok(result);
        }


        [HttpGet("cars-with-accidents-ref")]
        public async Task<ActionResult<IEnumerable<CarWithAccidents>>> GetCarsWithAccidentsRef()
        {
            var result = new List<CarWithAccidents>();

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "REPORT_PKG.GetCarsWithAccidents";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            var cursorParam = new OracleParameter
            {
                ParameterName = "p_cursor",
                OracleDbType = OracleDbType.RefCursor,
                Direction = System.Data.ParameterDirection.Output
            };
            cmd.Parameters.Add(cursorParam);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new CarWithAccidents
                {
                    CARID = reader.GetInt32(0),
                    BRAND = reader.GetString(1),
                    MODEL = reader.GetString(2),
                    YEAR = reader.GetInt32(3),
                    ACCIDENT_COUNT = reader.GetInt32(4)
                });
            }

            return Ok(result);
        }


        [HttpGet("expired-tech-check")]
        public async Task<ActionResult<IEnumerable<CarExpiredTechCheck>>> GetExpiredTechnicalCheck()
        {
            var result = new List<CarExpiredTechCheck>();

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "REPORT_PKG.GetCarsWithExpiredTechnicalCheck";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            var cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            cmd.Parameters.Add(cursor);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new CarExpiredTechCheck
                {
                    CARID = reader.GetInt32(0),
                    BRAND = reader.GetString(1),
                    MODEL = reader.GetString(2),
                    LAST_SERVICE = reader.IsDBNull(3) ? null : reader.GetDateTime(3)
                });
            }

            return Ok(result);
        }




    }
}
