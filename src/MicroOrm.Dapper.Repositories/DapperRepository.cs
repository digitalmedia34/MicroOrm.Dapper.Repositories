﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using MicroOrm.Dapper.Repositories.Extensions;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace MicroOrm.Dapper.Repositories
{
    /// <summary>
    ///     Base Repository
    /// </summary>
    public partial class DapperRepository<TEntity> : IDapperRepository<TEntity>
        where TEntity : class
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        ///     Constructor
        /// </summary>
        public DapperRepository(IDbConnection connection)
        {
            Connection = connection;
            SqlGenerator = new SqlGenerator<TEntity>(ESqlConnector.MSSQL);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public DapperRepository(IDbConnection connection, ESqlConnector sqlConnector)
        {
            Connection = connection;
            SqlGenerator = new SqlGenerator<TEntity>(sqlConnector);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public DapperRepository(IDbConnection connection, ISqlGenerator<TEntity> sqlGenerator)
        {
            Connection = connection;
            SqlGenerator = sqlGenerator;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public DapperRepository(IDbConnection connection, SqlGeneratorConfig config)
        {
            Connection = connection;
            SqlGenerator = new SqlGenerator<TEntity>(config);
        }


        /// <inheritdoc />
        public IDbConnection Connection { get; }

        /// <inheritdoc />
        public ISqlGenerator<TEntity> SqlGenerator { get; }


        /// <inheritdoc />
        public virtual TEntity FindById(object id, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id);
            return Connection.QuerySingleOrDefault<TEntity>(queryResult.GetSql(), queryResult.Param, transaction);
        }

        /// <inheritdoc />
        public virtual TEntity FindById<TChild1>(object id,
            Expression<Func<TEntity, object>> tChild1,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1);
            return ExecuteJoinQuery<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual TEntity FindById<TChild1, TChild2>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2);
            return ExecuteJoinQuery<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1, tChild2).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual TEntity FindById<TChild1, TChild2, TChild3>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2, tChild3);
            return ExecuteJoinQuery<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1, tChild2, tChild3).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual TEntity FindById<TChild1, TChild2, TChild3, TChild4>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2, tChild3, tChild4);
            return ExecuteJoinQuery<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(queryResult, transaction, tChild1, tChild2, tChild3, tChild4).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual TEntity FindById<TChild1, TChild2, TChild3, TChild4, TChild5>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2, tChild3, tChild4, tChild5);
            return ExecuteJoinQuery<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(queryResult, transaction, tChild1, tChild2, tChild3, tChild4, tChild5).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual TEntity FindById<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            Expression<Func<TEntity, object>> tChild6,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6);
            return ExecuteJoinQuery<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(queryResult, transaction, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindByIdAsync(object id, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id);
            return await Connection.QuerySingleOrDefaultAsync<TEntity>(queryResult.GetSql(), queryResult.Param, transaction);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindByIdAsync<TChild1>(object id,
            Expression<Func<TEntity, object>> tChild1,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1);
            return (await ExecuteJoinQueryAsync<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindByIdAsync<TChild1, TChild2>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1, tChild2)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindByIdAsync<TChild1, TChild2, TChild3>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2, tChild3);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1, tChild2, tChild3)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindByIdAsync<TChild1, TChild2, TChild3, TChild4>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2, tChild3, tChild4);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(queryResult, transaction, tChild1, tChild2, tChild3, tChild4)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2, tChild3, tChild4, tChild5);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(queryResult, transaction, tChild1, tChild2, tChild3, tChild4, tChild5)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindByIdAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(object id,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            Expression<Func<TEntity, object>> tChild6,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectById(id, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(queryResult, transaction, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectFirst(predicate);
            return (await FindAllAsync(queryResult, transaction)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync(IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectFirst(null);
            return (await FindAllAsync(queryResult, transaction)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync<TChild1>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> tChild1, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectFirst(predicate, tChild1);
            return (await ExecuteJoinQueryAsync<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync<TChild1, TChild2>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectFirst(predicate, tChild1, tChild2);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1, tChild2)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync<TChild1, TChild2, TChild3>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectFirst(predicate, tChild1, tChild2, tChild3);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1, tChild2, tChild3)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync<TChild1, TChild2, TChild3, TChild4>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectFirst(predicate, tChild1, tChild2, tChild3, tChild4);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(queryResult, transaction, tChild1, tChild2, tChild3, tChild4)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectFirst(predicate, tChild1, tChild2, tChild3, tChild4, tChild5);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(queryResult, transaction, tChild1, tChild2, tChild3, tChild4, tChild5)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            Expression<Func<TEntity, object>> tChild6,
            IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectFirst(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6);
            return (await ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(queryResult, transaction, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> FindAll(IDbTransaction transaction = null)
        {
            return FindAll(null, transaction);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectAll(predicate);
            return Connection.Query<TEntity>(queryResult.GetSql(), queryResult.Param, transaction);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> FindAll<TChild1>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> tChild1, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectAll(predicate, tChild1);
            return ExecuteJoinQuery<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> FindAll<TChild1, TChild2>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2);
            return ExecuteJoinQuery<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(sqlQuery, transaction, tChild1, tChild2);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> FindAll<TChild1, TChild2, TChild3>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2, tChild3);
            return ExecuteJoinQuery<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(sqlQuery, transaction, tChild1, tChild2, tChild3);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> FindAll<TChild1, TChild2, TChild3, TChild4>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2, tChild3, tChild4);
            return ExecuteJoinQuery<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(sqlQuery, transaction, tChild1, tChild2, tChild3, tChild4);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> FindAll<TChild1, TChild2, TChild3, TChild4, TChild5>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2, tChild3, tChild4, tChild5);
            return ExecuteJoinQuery<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(sqlQuery, transaction, tChild1, tChild2, tChild3, tChild4, tChild5);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> FindAll<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            Expression<Func<TEntity, object>> tChild6,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6);
            return ExecuteJoinQuery<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(sqlQuery, transaction, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6);
        }

        /// <inheritdoc />
        public virtual Task<IEnumerable<TEntity>> FindAllAsync(IDbTransaction transaction = null)
        {
            return FindAllAsync(predicate: null, transaction: transaction);
        }

        /// <inheritdoc />
        public virtual Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectAll(predicate);
            return FindAllAsync(queryResult, transaction);
        }

        /// <inheritdoc />
        public virtual Task<IEnumerable<TEntity>> FindAllAsync<TChild1>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> tChild1, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectAll(predicate, tChild1);
            return ExecuteJoinQueryAsync<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1);
        }


        /// <inheritdoc />
        public virtual Task<IEnumerable<TEntity>> FindAllAsync<TChild1, TChild2>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2);
            return ExecuteJoinQueryAsync<TChild1, TChild2, DontMap, DontMap, DontMap, DontMap>(sqlQuery, transaction, tChild1, tChild2);
        }

        /// <inheritdoc />
        public virtual Task<IEnumerable<TEntity>> FindAllAsync<TChild1, TChild2, TChild3>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2, tChild3);
            return ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, DontMap, DontMap, DontMap>(sqlQuery, transaction, tChild1, tChild2, tChild3);
        }

        /// <inheritdoc />
        public virtual Task<IEnumerable<TEntity>> FindAllAsync<TChild1, TChild2, TChild3, TChild4>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2, tChild3, tChild4);
            return ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, TChild4, DontMap, DontMap>(sqlQuery, transaction, tChild1, tChild2, tChild3, tChild4);
        }

        /// <inheritdoc />
        public virtual Task<IEnumerable<TEntity>> FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2, tChild3, tChild4, tChild5);
            return ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, TChild4, TChild5, DontMap>(sqlQuery, transaction, tChild1, tChild2, tChild3, tChild4, tChild5);
        }

        /// <inheritdoc />
        public virtual Task<IEnumerable<TEntity>> FindAllAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> tChild1,
            Expression<Func<TEntity, object>> tChild2,
            Expression<Func<TEntity, object>> tChild3,
            Expression<Func<TEntity, object>> tChild4,
            Expression<Func<TEntity, object>> tChild5,
            Expression<Func<TEntity, object>> tChild6,
            IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetSelectAll(predicate, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6);
            return ExecuteJoinQueryAsync<TChild1, TChild2, TChild3, TChild4, TChild5, TChild6>(sqlQuery, transaction, tChild1, tChild2, tChild3, tChild4, tChild5, tChild6);
        }

        /// <inheritdoc />
        public virtual bool Insert(TEntity instance, IDbTransaction transaction = null)
        {
            bool added;

            var queryResult = SqlGenerator.GetInsert(instance);

            if (SqlGenerator.IsIdentity)
            {
                var newId = Connection.Query<long>(queryResult.GetSql(), queryResult.Param, transaction).FirstOrDefault();
                added = newId > 0;

                if (added)
                {
                    var newParsedId = Convert.ChangeType(newId, SqlGenerator.IdentitySqlProperty.PropertyInfo.PropertyType);
                    SqlGenerator.IdentitySqlProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                added = Connection.Execute(queryResult.GetSql(), instance, transaction) > 0;
            }

            return added;
        }

        /// <inheritdoc />
        public virtual async Task<bool> InsertAsync(TEntity instance, IDbTransaction transaction = null)
        {
            bool added;

            var queryResult = SqlGenerator.GetInsert(instance);

            if (SqlGenerator.IsIdentity)
            {
                var newId = (await Connection.QueryAsync<long>(queryResult.GetSql(), queryResult.Param, transaction)).FirstOrDefault();
                added = newId > 0;

                if (added)
                {
                    var newParsedId = Convert.ChangeType(newId, SqlGenerator.IdentitySqlProperty.PropertyInfo.PropertyType);
                    SqlGenerator.IdentitySqlProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                added = await Connection.ExecuteAsync(queryResult.GetSql(), instance, transaction) > 0;
            }

            return added;
        }

        /// <inheritdoc />
        public virtual int BulkInsert(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetBulkInsert(instances);
            var count = Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction);
            return count;
        }

        /// <inheritdoc />
        public virtual async Task<int> BulkInsertAsync(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetBulkInsert(instances);
            var count = await Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction);
            return count;
        }

        /// <inheritdoc />
        public bool BulkUpdate(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetBulkUpdate(instances);
            var result = Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction) > 0;
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> BulkUpdateAsync(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetBulkUpdate(instances);
            var result = await Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction) > 0;
            return result;
        }

        #region UpSert

        /// <inheritdoc />
        public virtual bool UpSert(TEntity instance, IDbTransaction transaction = null)
        {
            var sqlQuery = SqlGenerator.GetUpSert(instance);
            bool modified;

            if (SqlGenerator.IsIdentity)
            {
                var newId = (Connection.Query<long>(sqlQuery.GetSql(), sqlQuery.Param, transaction)).FirstOrDefault();
                modified = newId > 0;

                if (modified)
                {
                    var newParsedId = Convert.ChangeType(newId, SqlGenerator.IdentitySqlProperty.PropertyInfo.PropertyType);
                    SqlGenerator.IdentitySqlProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                modified = Connection.Execute(sqlQuery.GetSql(), instance, transaction) > 0;
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
                var newId = (await Connection.QueryAsync<long>(sqlQuery.GetSql(), sqlQuery.Param, transaction)).FirstOrDefault();
                modified = newId > 0;

                if (modified)
                {
                    var newParsedId = Convert.ChangeType(newId, SqlGenerator.IdentitySqlProperty.PropertyInfo.PropertyType);
                    SqlGenerator.IdentitySqlProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                modified = Connection.Execute(sqlQuery.GetSql(), instance, transaction) > 0;
            }

            return modified;
        }

        #endregion


        /// <inheritdoc />
        public virtual bool Delete(TEntity instance, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetDelete(instance);
            var deleted = Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction) > 0;
            return deleted;
        }

        /// <inheritdoc />
        public virtual async Task<bool> DeleteAsync(TEntity instance, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetDelete(instance);
            var deleted = await Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction) > 0;
            return deleted;
        }

        /// <inheritdoc />
        public virtual bool Delete(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetDelete(predicate);
            var deleted = Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction) > 0;
            return deleted;
        }

        /// <inheritdoc />
        public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetDelete(predicate);
            var deleted = await Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction) > 0;
            return deleted;
        }

       
        /// <inheritdoc />
        public IEnumerable<TEntity> FindAllBetween(object from, object to, Expression<Func<TEntity, object>> btwField, IDbTransaction transaction = null)
        {
            return FindAllBetween(from, to, btwField, null, transaction);
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> FindAllBetween(object from, object to, Expression<Func<TEntity, object>> btwField, Expression<Func<TEntity, bool>> predicate = null, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectBetween(from, to, btwField, predicate);
            return Connection.Query<TEntity>(queryResult.GetSql(), queryResult.Param, transaction);
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> FindAllBetween(DateTime from, DateTime to, Expression<Func<TEntity, object>> btwField, IDbTransaction transaction = null)
        {
            return FindAllBetween(from, to, btwField, null, transaction);
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> FindAllBetween(DateTime from, DateTime to, Expression<Func<TEntity, object>> btwField, Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            var fromString = from.ToString(DateTimeFormat);
            var toString = to.ToString(DateTimeFormat);
            return FindAllBetween(fromString, toString, btwField, predicate);
        }

        /// <inheritdoc />
        public Task<IEnumerable<TEntity>> FindAllBetweenAsync(object from, object to, Expression<Func<TEntity, object>> btwField, IDbTransaction transaction = null)
        {
            return FindAllBetweenAsync(from, to, btwField, null, transaction);
        }

        /// <inheritdoc />
        public Task<IEnumerable<TEntity>> FindAllBetweenAsync(object from, object to, Expression<Func<TEntity, object>> btwField, Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectBetween(from, to, btwField, predicate);
            return Connection.QueryAsync<TEntity>(queryResult.GetSql(), queryResult.Param, transaction);
        }

        /// <inheritdoc />
        public Task<IEnumerable<TEntity>> FindAllBetweenAsync(DateTime from, DateTime to, Expression<Func<TEntity, object>> btwField, IDbTransaction transaction = null)
        {
            return FindAllBetweenAsync(from, to, btwField, null, transaction);
        }

        /// <inheritdoc />
        public Task<IEnumerable<TEntity>> FindAllBetweenAsync(DateTime from, DateTime to, Expression<Func<TEntity, object>> btwField, Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            return FindAllBetweenAsync(from.ToString(DateTimeFormat), to.ToString(DateTimeFormat), btwField, predicate, transaction);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync<TChild1>(Expression<Func<TEntity, object>> tChild1, IDbTransaction transaction = null)
        {
            var queryResult = SqlGenerator.GetSelectFirst(null, tChild1);
            return (await ExecuteJoinQueryAsync<TChild1, DontMap, DontMap, DontMap, DontMap, DontMap>(queryResult, transaction, tChild1)).FirstOrDefault();
        }

        /// <inheritdoc />
        private Task<IEnumerable<TEntity>> FindAllAsync(SqlQuery sqlQuery, IDbTransaction transaction = null)
        {
            return Connection.QueryAsync<TEntity>(sqlQuery.GetSql(), sqlQuery.Param, transaction);
        }

    }
}