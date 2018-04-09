using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.PostgresTypes;
using Npgsql.TypeHandlers;
using Npgsql.TypeMapping;
using NpgsqlTypes;

namespace Npgsql.CrateDb
{
    /// <summary>
    /// Represents a NpgsqlDatabaseInfo-class for usage with CrateDB.
    /// </summary>
    public class CrateDbDatabaseInfo : NpgsqlDatabaseInfo
    {
        static readonly IDictionary<string, uint> CrateDbBaseTypes = new Dictionary<string, uint>
        {
            { "bool", 16 },
            { "int2", 21 },
            { "int4", 23 },
            { "json", 114 },
            { "float4", 700 },
            { "char", 18 },
            { "int8", 20 },
            { "timestampz", 1184 },
            { "float8", 701 },
            { "varchar", 1043 }
        };

        static readonly IDictionary<string, uint> CrateDbArrayTypes = new Dictionary<string, uint>
        {
            { "_bool", 1000 },
            { "_int2", 1005 },
            { "_int4", 1007 },
            { "_json", 199 },
            { "_float4", 1021 },
            { "_char", 1002 },
            { "_int8", 1016 },
            { "_timestampz", 1185 },
            { "_float8", 1022 },
            { "_varchar", 1015 }
        };

        NpgsqlConnection conn;

        /// <summary>
        /// Creates an instance of the CrateDbDatabaseInfo class.
        /// </summary>
        /// <param name="conn"></param>
        public CrateDbDatabaseInfo(NpgsqlConnection conn)
        {
            this.conn = conn;
        }

        /// <summary>
        /// Returns the data types supported by CrateDB.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<PostgresType> GetTypes()
        {
            IEnumerable<PostgresType> baseTypes = CrateDbBaseTypes.Select(p => new CrateDbBaseType(p.Key, p.Value)).ToList();
            IEnumerable<PostgresType> arrayTypes = CrateDbArrayTypes.Select(p => new CrateDbArrayType(p.Key, p.Value, baseTypes.FirstOrDefault(t => String.Equals(t.Name, p.Key.Substring(1)))));

            return baseTypes.Concat(arrayTypes);
        }

        /// <summary>
        /// Returns a boolean value that signals if transactions are supported by the current database.
        /// </summary>
        public override bool SupportsTransactions => false;
        /// <summary>
        /// Returns a boolean value that signals if the DISCARD command is supported by the current database.
        /// </summary>
        public override bool SupportsDiscard => false;
        /// <summary>
        /// Returns a boolean value that signals if the DISCARD TEMP subcommand is supported by the current database.
        /// </summary>
        public override bool SupportsDiscardTemp => false;
        /// <summary>
        /// Returns a boolean value that signals if the DISCARD SEQUENCES subcommand is supported by the current database.
        /// </summary>
        public override bool SupportsDiscardSequences => false;
        /// <summary>
        /// Returns a boolean value that signals if the CLOSE ALL command is supported by the current database.
        /// </summary>
        public override bool SupportsCloseAll => false;
        /// <summary>
        /// Returns a boolean value that signals if the UNLISTEN command is supported by the current database.
        /// </summary>
        public override bool SupportsUnlisten => false;
        /// <summary>
        /// Returns a boolean value that signals if the current database supports range types.
        /// </summary>
        public override bool SupportsRangeTypes => false;
        /// <summary>
        /// Returns a boolean value that signals if the current database supports advisory lock functions.
        /// </summary>
        public override bool SupportsAdvisoryLocks => false;

        /// <summary>
        /// Adds type mappings specific to CrateDB to the given INpgsqlTypeMapper.
        /// </summary>
        /// <param name="mapper"></param>
        public static void AddCrateDbSpecificTypeMappings(INpgsqlTypeMapper mapper)
        {
            RemoveUnsupportedDataTypes(mapper);

            // Map CrateDB varchar type to the Npgsql TextHandler.
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

            // Map CrateDB timestampz type to the CrateDBTimestampHandler.
            mapper.AddMapping(new NpgsqlTypeMappingBuilder
            {
                PgTypeName = "timestampz",
                NpgsqlDbType = NpgsqlDbType.TimestampTz,
                DbTypes = new[] { DbType.DateTime },
                ClrTypes = new[] { typeof(DateTime) },
                TypeHandlerFactory = new CrateDbTimestampHandlerFactory()
            }
            .Build());

            //mapper.UseCrateDBObjectHandler();
        }

        /// <summary>
        /// Removes mappings for data types not supported by CrateDB.
        /// </summary>
        /// <param name="mapper"></param>
        public static void RemoveUnsupportedDataTypes(INpgsqlTypeMapper mapper)
        {
            var unsupportedTypes = mapper.Mappings.Where(m => !CrateDbBaseTypes.ContainsKey(m.PgTypeName) && !CrateDbArrayTypes.ContainsKey(m.PgTypeName)).ToList();
            foreach (var t in unsupportedTypes)
            {
                mapper.RemoveMapping(t.PgTypeName);
            }
        }
    }

    /// <summary>
    /// Represents a base type supported by CrateDB.
    /// </summary>
    public class CrateDbBaseType : PostgresBaseType
    {
        /// <summary>
        /// Creates an instance of the CrateDbBaseType class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oid"></param>
        public CrateDbBaseType(string name, uint oid)
            : base("pg_catalog", name, oid)
        {
        }
    }

    /// <summary>
    /// Represents a array type supported by CrateDB.
    /// </summary>
    public class CrateDbArrayType : PostgresArrayType
    {
        /// <summary>
        /// Creates an instance of the CrateDbArrayType class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oid"></param>
        /// <param name="elementType"></param>
        public CrateDbArrayType(string name, uint oid, PostgresType elementType) 
            : base("pg_catalog", name, oid, elementType)
        {
        }
    }
}
