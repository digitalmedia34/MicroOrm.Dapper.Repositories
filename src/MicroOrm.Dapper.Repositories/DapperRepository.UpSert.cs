using System;
using System.Data;
using System.Linq;
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
        public virtual bool UpSert(TEntity instance, IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetUpSert(instance);
            bool modified;

            if (SqlGenerator.IsIdentity)
            {
                var newId = (TransientDapperExtentions.QueryWithRetry(() => Connection.Query<long>(sqlQuery.GetSql(), sqlQuery.Param, transaction)).FirstOrDefault());
                modified = newId > 0;

                if (modified)
                {
                    var newParsedId = Convert.ChangeType(newId, SqlGenerator.IdentitySqlProperty.PropertyInfo.PropertyType);
                    SqlGenerator.IdentitySqlProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                modified = TransientDapperExtentions.ExecuteWithRetry(() => Connection.Execute(sqlQuery.GetSql(), instance, transaction)) > 0;
            }

            return modified;
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpSertAsync(TEntity instance, IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetUpSert(instance);
            bool modified;

            if (SqlGenerator.IsIdentity)
            {
                var newId = (await TransientDapperExtentions.QueryWithRetryAsync(() => Connection.QueryAsync<long>(sqlQuery.GetSql(), sqlQuery.Param, transaction))).FirstOrDefault();
                modified = newId > 0;

                if (modified)
                {
                    var newParsedId = Convert.ChangeType(newId, SqlGenerator.IdentitySqlProperty.PropertyInfo.PropertyType);
                    SqlGenerator.IdentitySqlProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                modified = (await TransientDapperExtentions.ExecuteWithRetryAsync(() => Connection.ExecuteAsync(sqlQuery.GetSql(), instance, transaction))) > 0;
            }

            return modified;
        }
    }
}