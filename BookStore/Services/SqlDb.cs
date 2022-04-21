using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public abstract class SqlDb : ISqlDb
    {
        protected DbConnection connection;

        public async Task OpenConnectionAsync(CancellationToken cancellationToken)
        {
            var connectionString = await GetDefaultConnectionString();

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Default connection string not defined.");

            await OpenConnectionAsync(connectionString, cancellationToken);
        }

        public async Task OpenConnectionAsync(string connectionString, CancellationToken cancellationToken)
        {
            connection = await OpenConnectionCoreAsync(connectionString, cancellationToken);
        }

        protected abstract Task<DbConnection> OpenConnectionCoreAsync(string connectionString, CancellationToken cancellationToken);

        protected abstract Task<string> GetDefaultConnectionString();

        protected bool ActiveConnection
        {
            get
            {
                return connection != null && connection.State != ConnectionState.Closed;
            }
        }

        public void Dispose()
        {
            if (ActiveConnection)
                connection.Close();
        }

        public async ValueTask DisposeAsync()
        {
            if (ActiveConnection)
                await connection.CloseAsync();
        }
    }
}
