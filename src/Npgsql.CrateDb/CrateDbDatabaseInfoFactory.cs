using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npgsql.CrateDb
{
    /// <summary>
    /// Provides Methods to create CrateDbDatabaseInfo instances.
    /// </summary>
    public class CrateDbDatabaseInfoFactory : INpgsqlDatabaseInfoFactory
    {
        /// <summary>
        /// Returns a CrateDbDatabaseInfo instance if the given connection connects to a CrateDB cluster and null otherwhise.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="timeout"></param>
        /// <param name="async"></param>
        /// <returns></returns>
        public Task<NpgsqlDatabaseInfo> Load(NpgsqlConnection conn, NpgsqlTimeout timeout, bool async)
        {
            if (conn.PostgresParameters.ContainsKey("crate_version"))
            {
                return Task.FromResult((NpgsqlDatabaseInfo)new CrateDbDatabaseInfo(conn));
            }
            return null;
        }
    }
}
