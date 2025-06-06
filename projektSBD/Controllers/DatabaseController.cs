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

        [HttpGet("get-cars")]
        public async Task<IActionResult> GetCars()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    await _connection.OpenAsync();

                using var cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT CarID, Brand, Model, Year, Price, EngineSize FROM Cars";
                using var reader = await cmd.ExecuteReaderAsync();

                var cars = new List<Car>();
                while (await reader.ReadAsync())
                {
                    cars.Add(new Car
                    {
                        CarID = reader.GetInt32(0),
                        Brand = reader.GetString(1),
                        Model = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Price = reader.GetDecimal(4),
                        EngineSize = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                    });
                }

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Failed to fetch cars: {ex.Message}" });
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    await _connection.CloseAsync();
            }
        }
    }
}
