using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MicroOrm.Dapper.Repositories.Extensions;

namespace MicroOrm.Dapper.Repositories
{
    /// <summary>
    ///     Base Repository
    /// </summary>
    public partial class DapperRepository<TEntity>
        where TEntity : class
    {
        /// <inheritdoc />
        public bool BulkUpdate(IEnumerable<TEntity> instances)
        {
            return BulkUpdate(instances, null);
        }

        /// <inheritdoc />
        public bool BulkUpdate(IEnumerable<TEntity> instances, IDbTransaction transaction)
        {
            var queryResult = SqlGenerator.GetBulkUpdate(instances);
            var result = TransientDapperExtentions.ExecuteWithRetry(() => Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction)) > 0;
            return result;
        }

        /// <inheritdoc />
        public Task<bool> BulkUpdateAsync(IEnumerable<TEntity> instances)
        {
            return BulkUpdateAsync(instances, null);
        }

        /// <inheritdoc />
        public async Task<bool> BulkUpdateAsync(IEnumerable<TEntity> instances, IDbTransaction transaction)
        {
            var queryResult = SqlGenerator.GetBulkUpdate(instances);
            var result = (await TransientDapperExtentions.ExecuteWithRetryAsync(() => Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction))) > 0;
            return result;
        }
    }
}