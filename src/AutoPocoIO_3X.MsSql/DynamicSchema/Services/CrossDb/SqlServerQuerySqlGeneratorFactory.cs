﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace AutoPocoIO.DynamicSchema.Services.CrossDb
{

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class SqlServerQuerySqlGeneratorFactory : Microsoft.EntityFrameworkCore.SqlServer.Query.Internal.SqlServerQuerySqlGeneratorFactory
    {
        private readonly ISqlServerOptions _sqlServerOptions;
        private readonly QuerySqlGeneratorDependencies _dependencies;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public SqlServerQuerySqlGeneratorFactory(
             QuerySqlGeneratorDependencies dependencies)
            : base(dependencies)
        {
            _dependencies = dependencies;
        }

        public override QuerySqlGenerator Create()
                   => new SqlServerQuerySqlGenerator(_dependencies);
    }
}