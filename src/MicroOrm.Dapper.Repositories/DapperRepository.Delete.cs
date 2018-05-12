using System;
using System.Data;
using System.Linq.Expressions;
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
        public virtual bool Delete(TEntity instance, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetDelete(instance);
            var deleted = (TransientDapperExtentions.ExecuteWithRetry(() => Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction))) > 0;
            return deleted;
        }

        /// <inheritdoc />
        public virtual async Task<bool> DeleteAsync(TEntity instance, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetDelete(instance);
            var deleted = (await TransientDapperExtentions.ExecuteWithRetryAsync(() => Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction))) > 0;
            return deleted;
        }

        /// <inheritdoc />
        public virtual bool Delete(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetDelete(predicate);
            var deleted = (TransientDapperExtentions.ExecuteWithRetry(() => Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction))) > 0;
            return deleted;
        }

        /// <inheritdoc />
        public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetDelete(predicate);
            var deleted = (await TransientDapperExtentions.ExecuteWithRetryAsync(() => Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction))) > 0;
            return deleted;
        }
    }
}
