using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npgsql.CrateDb
{
    /// <summary>
    /// Provides extension methods for NpgSqlDataReader specific to CrateDb.
    /// </summary>
    public static class NpgsqlDataReaderExtensions
    {
        /// <summary>
        /// Gets the value of the specified column as a byte array.
        /// </summary>
        /// <param name="reader">The current NpgSqlDataReader instance.</param>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column as a byte array.</returns>
        public static byte[] GetBytes(this NpgsqlDataReader reader, int ordinal)
        {
            return reader.GetFieldValue<char[]>(ordinal).Select(c => Convert.ToByte(c)).ToArray();
        }

        /// <summary>
        /// Gets the value of the specified column as a object of the given type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="reader">The current NpgSqlDataReader instance.</param>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column as a object of the given type.</returns>
        public static T GetObject<T>(this NpgsqlDataReader reader, int ordinal)
        {
            return reader.GetFieldValue<T>(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as an array of objects of the given type T.
        /// </summary>
        /// <typeparam name="T">The element type of the array to return.</typeparam>
        /// <param name="reader">The current NpgSqlDataReader instance.</param>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column as an array of objects of the given type.</returns>
        public static T[] GetObjectArray<T>(this NpgsqlDataReader reader, int ordinal)
        {
            return reader.GetFieldValue<T[]>(ordinal);
        }
    }
}
