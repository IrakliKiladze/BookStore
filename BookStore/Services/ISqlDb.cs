using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface ISqlDb : IDisposable, IAsyncDisposable
    {
        Task OpenConnectionAsync(string connectionString, CancellationToken cancellationToken);

        Task OpenConnectionAsync(CancellationToken cancellationToken);
    }
}
