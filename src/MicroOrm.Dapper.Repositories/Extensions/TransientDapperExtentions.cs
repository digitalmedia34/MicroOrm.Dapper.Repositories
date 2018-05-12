using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Polly;
using Polly.Wrap;

namespace MicroOrm.Dapper.Repositories.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TransientDapperExtentions
    {
        /// <summary>
        /// Queries database with retry policy
        /// </summary>
        public static IEnumerable<T> QueryWithRetry<T>(Func<IEnumerable<T>> f)
        {
            return Policy.Handle<SqlException>(e => e.Number == -2)
                        .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)))
                        .Execute(f);
            //return SqlCommandRetryPolicy.ExecuteAction(f);
        }

        /// <summary>
        /// Queries single or default with retry policy
        /// </summary>
        public static T QuerySingleOrDefaultWithRetry<T>(Func<T> f)
        {
            return Policy.Handle<SqlException>(e => e.Number == -2)
                        .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)))
                        .Execute(f);
        }

        /// <summary>
        /// Queries single or default with retry policy
        /// </summary>
        public static T QueryFirstOrDefaultWithRetry<T>(Func<T> f)
        {
            return Policy.Handle<SqlException>(e => e.Number == -2)
                        .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)))
                        .Execute(f);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static int ExecuteWithRetry(Func<int> f)
        {
            return Policy.Handle<SqlException>(e => e.Number == -2)
                        .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)))
                        .Execute(f);
        }

        /// <summary>
        /// Queries database with retry policy async
        /// </summary>
        public static async Task<IEnumerable<T>> QueryWithRetryAsync<T>(Func<Task<IEnumerable<T>>> f)
        {
            return await Policy.Handle<SqlException>(e => e.Number == -2)
                                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)))
                                .ExecuteAsync(f);
        }

        /// <summary>
        /// Queries single or default with retry policy
        /// </summary>
        public static async Task<T> QueryFirstOrDefaultWithRetryAsync<T>(Func<Task<T>> f)
        {
            return await Policy.Handle<SqlException>(e => e.Number == -2)
                                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)))
                                .ExecuteAsync(f);
        }

        /// <summary>
        /// Queries database with retry policy
        /// </summary>
        public static async Task<T> QuerySingleOrDefaultWithRetryAsync<T>(Func<Task<T>> f)
        {
            return await Policy.Handle<SqlException>(e => e.Number == -2)
                                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)))
                                .ExecuteAsync(f);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task<int> ExecuteWithRetryAsync(Func<Task<int>> f)
        {
            return await Policy.Handle<SqlException>(e => e.Number == -2)
                                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)))
                                .ExecuteAsync(f);
        }
    }
}
