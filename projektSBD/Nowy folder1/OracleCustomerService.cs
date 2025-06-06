using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace CustomerService.Services
{
    public class OracleCustomerService
    {
        private readonly string _connectionString;

        public OracleCustomerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDB");
        }
    }
}