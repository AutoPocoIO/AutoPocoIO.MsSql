using Microsoft.EntityFrameworkCore.Query;

namespace AutoPocoIO.DynamicSchema.Services.CrossDb
{

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class SqlServerQuerySqlGeneratorFactory : Microsoft.EntityFrameworkCore.Query.Internal.QuerySqlGeneratorFactory, IQuerySqlGeneratorFactoryWithCrossDb
    {
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

        public QuerySqlGeneratorWithCrossDb CreateWithCrossDb()
             => new SqlServerQuerySqlGenerator(_dependencies);
    }
}
