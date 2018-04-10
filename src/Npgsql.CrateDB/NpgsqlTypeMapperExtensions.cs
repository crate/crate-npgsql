using Npgsql.TypeHandlers;
using Npgsql.TypeMapping;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npgsql.CrateDb
{
    /// <summary>
    /// Implements extension methods to register features specific to CrateDB.
    /// </summary>
    public static class NpgsqlTypeMapperExtensions
    {
        /// <summary>
        /// Maps the CrateObjectHandler to the json database type.
        /// </summary>
        /// <param name="mapper">The type mapper to set up (global or connection-specific)</param>
        public static INpgsqlTypeMapper UseCrateDbObjectHandler(this INpgsqlTypeMapper mapper)
        {
            // Map CrateDb-specific json-Handler.
            mapper.AddMapping(new NpgsqlTypeMappingBuilder
            {
                PgTypeName = "json",
                NpgsqlDbType = NpgsqlDbType.Json,
                TypeHandlerFactory = new CrateDbObjectHandlerFactory()
            }
            .Build());

            return mapper;
        }
    }
}
