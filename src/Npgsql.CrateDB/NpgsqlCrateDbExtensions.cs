using Npgsql.TypeHandlers;
using Npgsql.TypeMapping;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npgsql.CrateDB
{
    /// <summary>
    /// Extension allowing adding the CrateDb plugin to an Npgsql type mapper.
    /// </summary>
    public static class NpgsqlCrateDbExtensions
    {
        /// <summary>
        /// Sets up mappings for the types supported by CrateDb.
        /// </summary>
        /// <param name="mapper">The type mapper to set up (global or connection-specific)</param>
        public static INpgsqlTypeMapper UseCrateDb(this INpgsqlTypeMapper mapper)
        {
            // Map CrateDb varchar type to the Npgsql TextHandler.
            mapper.AddMapping(new NpgsqlTypeMappingBuilder
            {
                PgTypeName = "varchar",
                NpgsqlDbType = NpgsqlDbType.Varchar,
                DbTypes = new[] { DbType.String, DbType.StringFixedLength, DbType.AnsiString, DbType.AnsiStringFixedLength },
                ClrTypes = new[] { typeof(string), typeof(char[]), typeof(char) },
                InferredDbType = DbType.String,
                TypeHandlerFactory = new TextHandlerFactory()
            }
            .Build());

            // Map CrateDb timestampz type to the CrateTimestampHandler.
            mapper.AddMapping(new NpgsqlTypeMappingBuilder
            {
                PgTypeName = "timestampz",
                NpgsqlDbType = NpgsqlDbType.TimestampTz,
                DbTypes = new[] { DbType.DateTime },
                ClrTypes = new[] { typeof(DateTime) },
                TypeHandlerFactory = new CrateTimestampHandlerFactory()
            }
            .Build());

            // Map CrateDb-specific json-Handler.
            mapper.AddMapping(new NpgsqlTypeMappingBuilder
            {
                PgTypeName = "json",
                NpgsqlDbType = NpgsqlDbType.Json,
                TypeHandlerFactory = new CrateObjectHandlerFactory()
            }
            .Build());

            return mapper;
        }
    }
}
