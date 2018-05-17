using Newtonsoft.Json;
using Npgsql.PostgresTypes;
using Npgsql.TypeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.BackendMessages;
using Npgsql.TypeHandling;

namespace Npgsql.CrateDb
{
    class CrateDbObjectHandlerFactory : NpgsqlTypeHandlerFactory<string>
    {
        protected override NpgsqlTypeHandler<string> Create(NpgsqlConnection conn)
            => new CrateDbObjectHandler(conn);
    }

    class CrateDbObjectHandler : TextHandler
    {
        internal CrateDbObjectHandler(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override int ValidateObjectAndGetLength(object value, ref NpgsqlLengthCache lengthCache, NpgsqlParameter parameter)
        {
            var s = value as string;
            if (s == null)
            {
                s = JsonConvert.SerializeObject(value);
                if (parameter != null)
                    parameter.ConvertedValue = s;
            }
            return base.ValidateObjectAndGetLength(s, ref lengthCache, parameter);
        }

        protected override Task WriteObjectWithLength(object value, NpgsqlWriteBuffer buf, NpgsqlLengthCache lengthCache, NpgsqlParameter parameter, bool async)
        {
            if (parameter?.ConvertedValue != null)
                value = parameter.ConvertedValue;
            var s = value as string ?? JsonConvert.SerializeObject(value);

            return base.WriteObjectWithLength(s, buf, lengthCache, parameter, async);
        }

        protected override async ValueTask<T> Read<T>(NpgsqlReadBuffer buf, int len, bool async, FieldDescription fieldDescription = null)
        {
            var s = await base.Read<string>(buf, len, async, fieldDescription);
            if (typeof(T) == typeof(string))
                return (T)(object)s;
            try
            {
                return JsonConvert.DeserializeObject<T>(s);
            }
            catch (Exception e)
            {
                throw new NpgsqlSafeReadException(e);
            }
        }

        /// <summary>
        /// Creates a type handler for arrays of this handler's type.
        /// </summary>
        /// <remarks>
        /// The CrateObjectHandler requires a special array handler that returns arrays of arbitrary clr types.
        /// </remarks>
        public override ArrayHandler CreateArrayHandler(PostgresType arrayBackendType) => 
             new CrateDbObjectArrayHandler(this);
    }
}
