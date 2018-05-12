using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MicroOrm.Dapper.Repositories.Extensions;

namespace MicroOrm.Dapper.Repositories
{
    public partial class DapperRepository<TEntity>
        where TEntity : class
    {
        /// <inheritdoc />
        public bool BulkUpsert(IEnumerable<TEntity> instances)
        {
            return BulkUpsert(instances, null);
        }

        /// <inheritdoc />
        public async Task<bool> BulkUpsertAsync(IEnumerable<TEntity> instances)
        {
            return await BulkUpsertAsync(instances, null);
        }

        /// <inheritdoc />
        public bool BulkUpsert(IEnumerable<TEntity> instances, IDbTransaction transaction)
        {
            var queryResult = SqlGenerator.GetBulkUpSert(instances);
            var result = TransientDapperExtentions.ExecuteWithRetry(() => Connection.Execute(queryResult.GetSql(), null, transaction)) > 0;
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> BulkUpsertAsync(IEnumerable<TEntity> instances, IDbTransaction transaction)
        {
            var queryResult = SqlGenerator.GetBulkUpSert(instances);
            var result = (await TransientDapperExtentions.ExecuteWithRetryAsync(() => Connection.ExecuteAsync(queryResult.GetSql(), null, transaction))) > 0;
            return result;
        }
    }
}
