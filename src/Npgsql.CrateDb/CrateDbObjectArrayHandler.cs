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

        protected async override ValueTask<T2> Read<T2>(NpgsqlReadBuffer buf, int byteLen, bool async, FieldDescription fieldDescription = null)
        {
            if (typeof(T2).IsArray)
            {
                var method = typeof(CrateDbObjectArrayHandler).GetMethod("ReadArray", BindingFlags.Instance | BindingFlags.NonPublic);
                var genericMethod = method.MakeGenericMethod(typeof(T2).GetElementType());
                var task = (ValueTask<Array>)genericMethod.Invoke(this, new object[] { buf, byteLen, async, fieldDescription });
                object o = await task;
                return (T2)o;
            }

            return await base.Read<T2>(buf, byteLen, async, fieldDescription);
        }

        internal async ValueTask<Array> ReadArray<TElement>(NpgsqlReadBuffer buf, int byteLen, bool async, FieldDescription fieldDescription = null)
        {
            var asTypedHandler = this as INpgsqlTypeHandler<Array>;
            if (asTypedHandler != null)
            {
                var stringArray = await asTypedHandler.Read(buf, byteLen, async, fieldDescription);
                return stringArray.OfType<string>().Select(s => JsonConvert.DeserializeObject<TElement>(s)).ToArray();
            }
            return default(TElement[]);
        }
    }
}
