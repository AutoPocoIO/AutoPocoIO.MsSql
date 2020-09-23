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

        protected override Expression VisitExtension(Expression extensionExpression)
        {
            if (extensionExpression is TableExpression tableExpressionWithDb)
                return VisitTable(tableExpressionWithDb);
            else
                return base.VisitExtension(extensionExpression);
        }

        protected Expression VisitTable(TableExpression tableExpression)
        {
            Sql.Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(tableExpression.DatabaseName) + ".");

            Sql.Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(tableExpression.Name, tableExpression.Schema))
                .Append(AliasSeparator)
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(tableExpression.Alias));

            return tableExpression;
        }
    }
}
