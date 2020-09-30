using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace AutoPocoIO.DynamicSchema.Services.CrossDb
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class SqlServerQuerySqlGenerator : QuerySqlGeneratorWithCrossDb
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
            else if(extensionExpression is Microsoft.EntityFrameworkCore.Query.SqlExpressions.TableExpression)
                return base.VisitExtension(extensionExpression);
            else
                return base.VisitExtension(extensionExpression);
        }

        protected Expression VisitTable(TableExpression tableExpression)
        {
            if(!string.IsNullOrEmpty(tableExpression.DatabaseName))
                Sql.Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(tableExpression.DatabaseName) + ".");

            Sql.Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(tableExpression.Name, tableExpression.Schema))
                .Append(AliasSeparator)
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(tableExpression.Alias));

            return tableExpression;
        }



        protected void GenerateTop(SelectExpression selectExpression)
        {
            if (selectExpression.Limit != null
                && selectExpression.Offset == null)
            {
                Sql.Append("TOP(");

                Visit(selectExpression.Limit);

                Sql.Append(") ");
            }
        }

        protected void GenerateLimitOffset(SelectExpression selectExpression)
        {
            // Note: For Limit without Offset, SqlServer generates TOP()
            if (selectExpression.Offset != null)
            {
                Sql.AppendLine()
                    .Append("OFFSET ");

                Visit(selectExpression.Offset);

                Sql.Append(" ROWS");

                if (selectExpression.Limit != null)
                {
                    Sql.Append(" FETCH NEXT ");

                    Visit(selectExpression.Limit);

                    Sql.Append(" ROWS ONLY");
                }
            }
        }

        protected override Expression VisitSqlFunction(SqlFunctionExpression sqlFunctionExpression)
        {
            if (!sqlFunctionExpression.IsBuiltIn
                && string.IsNullOrEmpty(sqlFunctionExpression.Schema))
            {
                sqlFunctionExpression = SqlFunctionExpression.Create(
                    schema: "dbo",
                    sqlFunctionExpression.Name,
                    sqlFunctionExpression.Arguments,
                    sqlFunctionExpression.Type,
                    sqlFunctionExpression.TypeMapping);
            }

            return base.VisitSqlFunction(sqlFunctionExpression);
        }
    }
}
