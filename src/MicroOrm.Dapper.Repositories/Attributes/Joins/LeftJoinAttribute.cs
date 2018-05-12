﻿namespace MicroOrm.Dapper.Repositories.Attributes.Joins
{
    /// <summary>
    ///     Generate LEFT JOIN
    /// </summary>
    public sealed class LeftJoinAttribute : JoinAttributeBase
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public LeftJoinAttribute()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="tableName">Name of external table</param>
        /// <param name="key">ForeignKey of this table</param>
        /// <param name="externalKey">Key of external table</param>
        public LeftJoinAttribute(string tableName, string key, string externalKey)
            : base(tableName, key, externalKey, string.Empty, string.Empty)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="tableName">Name of external table</param>
        /// <param name="key">ForeignKey of this table</param>
        /// <param name="externalKey">Key of external table</param>
        /// <param name="tableSchema">Name of external table schema</param>
        public LeftJoinAttribute(string tableName, string key, string externalKey, string tableSchema)
            : base(tableName, key, externalKey, tableSchema, string.Empty)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="tableName">Name of external table</param>
        /// <param name="key">ForeignKey of this table</param>
        /// <param name="externalKey">Key of external table</param>
        /// <param name="tableSchema">Name of external table schema</param>
        /// <param name="tableAbbreviation">External table alias</param>
        public LeftJoinAttribute(string tableName, string key, string externalKey, string tableSchema, string tableAbbreviation)
            : base(tableName, key, externalKey, tableSchema, tableAbbreviation)
        {
        }
    }
}