using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using projektSBD.Models;
using System.Data;

namespace OracleApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly OracleConnection _connection;

        public DatabaseController(OracleConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    await _connection.OpenAsync();

                using var cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT 'Connected to Oracle DB' AS Result FROM dual";
                var result = await cmd.ExecuteScalarAsync();

                return Ok(new { Message = result?.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Connection failed: {ex.Message}" });
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    await _connection.CloseAsync();
            }
        }
    }
}
