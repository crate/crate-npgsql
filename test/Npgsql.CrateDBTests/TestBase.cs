using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npgsql.CrateDBTests
{
    public abstract class TestBase
    {
        /// <summary>
        /// The connection string that will be used when opening the connection to the tests database.
        /// May be overridden in fixtures, e.g. to set special connection parameters
        /// </summary>
        public static string ConnectionString =>
            Environment.GetEnvironmentVariable("NPGSQL_CRATE_TEST_DB") ?? DefaultConnectionString;

        /// <summary>
        /// Unless the NPGSQL_CRATE_TEST_DB environment variable is defined, this is used as the connection string for the
        /// test database.
        /// </summary>
        const string DefaultConnectionString = "Server=localhost;User ID=npgsql_tests;Password=npgsql_tests;Database=npgsql_tests;Timeout=0;Command Timeout=0";

        protected virtual NpgsqlConnection OpenConnection(string connectionString = null)
        {
            if (connectionString == null)
                connectionString = ConnectionString;
            var conn = new NpgsqlConnection(connectionString);

            conn.Open();

            return conn;
        }
    }
}
