/*using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Threading.Tasks;

namespace OracleApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursorsController : ControllerBase
    {
        private readonly OracleConnection _connection;

        public CoursorsController(OracleConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("get-accident-counts")]
        public async Task<IActionResult> GetAccidentCounts()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    await _connection.OpenAsync();

                // Blok PL/SQL do zliczania wypadków dla każdego samochodu
                string plsqlBlock = @"
                    DECLARE
                      CURSOR c_car_accident IS
                        SELECT CarID FROM Cars;

                      v_count NUMBER;
                      v_result VARCHAR2(32767);
                    BEGIN
                      FOR i_car IN c_car_accident LOOP
                        SELECT COUNT(*) INTO v_count
                        FROM Accidents
                        WHERE CarID = i_car.CarID;

                        IF v_count > 0 THEN
                          v_result := v_result || 'Samochód ' || i_car.CarID || ' miał ' || v_count || ' wypadków' || CHR(10);
                        END IF;
                      END LOOP;

                      :output := v_result;
                    END;";

                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = plsqlBlock;

                    var outputParam = new OracleParameter(":output", OracleDbType.Varchar2, ParameterDirection.Output)
                    {
                        Size = 32767
                    };
                    cmd.Parameters.Add(outputParam);

                    await cmd.ExecuteNonQueryAsync();

                    var result = outputParam.Value.ToString();

                    var resultsArray = result.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    return Ok(new { Message = resultsArray });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Failed to execute cursor block: {ex.Message}" });
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    await _connection.CloseAsync();
            }
        }


    }
}
*/