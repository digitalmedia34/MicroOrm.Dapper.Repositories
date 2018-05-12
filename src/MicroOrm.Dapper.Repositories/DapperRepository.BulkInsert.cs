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
        public virtual int BulkInsert(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetBulkInsert(instances);
            var count = TransientDapperExtentions.ExecuteWithRetry(() => Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction));
            return count;
        }

        /// <inheritdoc />
        public virtual async Task<int> BulkInsertAsync(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetBulkInsert(instances);
            var count = await TransientDapperExtentions.ExecuteWithRetryAsync(() => Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction));
            return count;
        }
    }
}
