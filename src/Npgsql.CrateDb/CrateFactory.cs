using System;
using System.Data.Common;

namespace Npgsql.CrateDb
{
    /// <summary>
    /// A factory to create instances of various Npgsql objects.
    /// </summary>
    [Serializable]
    public sealed class CrateFactory : DbProviderFactory, IServiceProvider
    {
        /// <summary>
        /// Gets an instance of the <see cref="NpgsqlFactory"/>.
        /// This can be used to retrieve strongly typed data objects.
        /// </summary>
        public static readonly CrateFactory Instance = new CrateFactory();

        static CrateFactory() => NpgsqlDatabaseInfo.RegisterFactory(new CrateDbDatabaseInfoFactory());

        CrateFactory() { }

        /// <summary>
        /// Returns a strongly typed <see cref="DbCommand"/> instance.
        /// </summary>
        public override DbCommand CreateCommand() => NpgsqlFactory.Instance.CreateCommand();

        /// <summary>
        /// Returns a strongly typed <see cref="DbConnection"/> instance.
        /// </summary>
        public override DbConnection CreateConnection() => NpgsqlFactory.Instance.CreateConnection();

        /// <summary>
        /// Returns a strongly typed <see cref="DbParameter"/> instance.
        /// </summary>
        public override DbParameter CreateParameter() => NpgsqlFactory.Instance.CreateParameter();

        /// <summary>
        /// Returns a strongly typed <see cref="DbConnectionStringBuilder"/> instance.
        /// </summary>
        public override DbConnectionStringBuilder CreateConnectionStringBuilder() => NpgsqlFactory.Instance.CreateConnectionStringBuilder();

        /// <summary>
        /// Returns a strongly typed <see cref="DbCommandBuilder"/> instance.
        /// </summary>
        public override DbCommandBuilder CreateCommandBuilder() => NpgsqlFactory.Instance.CreateCommandBuilder();

        /// <summary>
        /// Returns a strongly typed <see cref="DbDataAdapter"/> instance.
        /// </summary>
        public override DbDataAdapter CreateDataAdapter() => NpgsqlFactory.Instance.CreateDataAdapter();

        #region IServiceProvider Members

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>A service object of type serviceType, or null if there is no service object of type serviceType.</returns>
        public object GetService(Type serviceType) => NpgsqlFactory.Instance.GetService(serviceType);

        #endregion
    }
}
