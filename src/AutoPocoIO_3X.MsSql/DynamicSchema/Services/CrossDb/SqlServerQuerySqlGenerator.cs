using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AutoPocoIO.DynamicSchema.Services.CrossDb
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class SqlServerQuerySqlGenerator : Microsoft.EntityFrameworkCore.SqlServer.Query.Internal.SqlServerQuerySqlGenerator
    {
        public SqlServerQuerySqlGenerator(
            QuerySqlGeneratorDependencies dependencies
          )
           : base(dependencies)
        {
        }

        protected override Expression VisitTable(Microsoft.EntityFrameworkCore.Query.SqlExpressions.TableExpression tableExpression)
        {
            //if (tableExpression is TableExpression tableExpressionWithDb)
              //  Sql.Append(SqlGenerator.DelimitIdentifier(tableExpressionWithDb.DatabaseName) + ".");

            return base.VisitTable(tableExpression);
        }
    }
}
