using System;
using System.Data;
using System.Linq;
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
        public virtual TEntity Find()
        {
            return Find(null, null);
        }

        /// <inheritdoc />
        public virtual TEntity Find(IDbTransaction transaction)
        {
            return Find(null, transaction);
        }

        /// <inheritdoc />
        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Find(predicate, null);
        }

        /// <inheritdoc />
        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction)
        {
            var queryResult = SqlGenerator.GetSelectFirst(predicate, true);
            return TransientDapperExtentions.QueryFirstOrDefaultWithRetry(() => Connection.QueryFirstOrDefault<TEntity>(queryResult.GetSql(), queryResult.Param, transaction));
        }

        /// <inheritdoc />
        public virtual Task<TEntity> FindAsync()
        {
            return FindAsync(null, null);
        }

        /// <inheritdoc />
        public virtual Task<TEntity> FindAsync(IDbTransaction transaction)
        {
            return FindAsync(null, transaction);
        }

        /// <inheritdoc />
        public virtual Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAsync(predicate, null);
        }

        /// <inheritdoc />
        public virtual Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction)
        {
            var queryResult = SqlGenerator.GetSelectFirst(predicate, true);
            return TransientDapperExtentions.QueryFirstOrDefaultWithRetryAsync(() => Connection.QueryFirstOrDefaultAsync<TEntity>(queryResult.GetSql(), queryResult.Param, transaction));
        }
    }
}
