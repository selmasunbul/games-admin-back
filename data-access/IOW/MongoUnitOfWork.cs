using data_access.context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.IOW
{
    public sealed class MongoUnitOfWork : IUnitOfWork
    {
        public IMongoDatabase MongoDatabase { get; }
        private readonly MongoClient client;
        private IClientSessionHandle? _session = null;

        public MongoUnitOfWork(MongoDbContext context)
        {
            this.MongoDatabase = context.MongoDatabase;
            this.client = context.MongoClient;
        }

        #region IUnitOfWork Members

        #endregion

        public async Task BeginAsync()
        {
            _session = await client.StartSessionAsync();
            _session.StartTransaction();
        }

        public async Task CommitAsync()
        {
            if (_session != null)
            {
                await _session.CommitTransactionAsync();
                Dispose();
            }
        }

        public async Task RollbackAsync()
        {
            if (_session != null)
            {
                await _session.AbortTransactionAsync();
                Dispose();
            }
        }

        public void Dispose()
        {
            //
            if (_session != null)
                _session.Dispose();
            _session = null;
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        IMongoDatabase MongoDatabase { get; }
        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
