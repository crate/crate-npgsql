using Newtonsoft.Json;
using Npgsql.BackendMessages;
using Npgsql.PostgresTypes;
using Npgsql.TypeHandlers;
using Npgsql.TypeHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Npgsql.CrateDb
{
    class CrateDbObjectArrayHandler : ArrayHandler<string>
    {
        public CrateDbObjectArrayHandler(NpgsqlTypeHandler elementHandler)
            : base(elementHandler)
        {
        }

        protected async override ValueTask<TAny> Read<TAny>(NpgsqlReadBuffer buf, int len, bool async, FieldDescription fieldDescription = null)
        {
            if (typeof(TAny).IsArray)
            {
                var method = typeof(CrateDbObjectArrayHandler).GetMethod("ReadArray",
                    BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    CallingConventions.Any,
                    new Type[] { typeof(NpgsqlReadBuffer), typeof(int), typeof(bool), typeof(FieldDescription) },
                    null);
                var genericMethod = method.MakeGenericMethod(typeof(TAny).GetElementType());
                var task = (ValueTask<Array>)genericMethod.Invoke(this, new object[] { buf, len, async, fieldDescription });
                object o = await task;
                return (TAny)o;
            }
            return await base.Read<TAny>(buf, len, async, fieldDescription);
        }

        internal async ValueTask<Array> ReadArray<TElement>(NpgsqlReadBuffer buf, int len, bool async, FieldDescription fieldDescription = null)
        {
            var asTypedHandler = this as ArrayHandler;
            if (asTypedHandler != null)
            {
                return await ReadArray<TElement>(buf, async);
            }
            return default(TElement[]);
        }
    }
}
