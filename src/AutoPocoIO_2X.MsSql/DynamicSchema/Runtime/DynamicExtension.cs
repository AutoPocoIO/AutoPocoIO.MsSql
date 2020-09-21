using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Sql;

namespace AutoPocoIO.MsSql.DynamicSchema.Runtime
{
    internal static partial class DynamicExtension
    {
       
        public static DbContextOptionsBuilder ReplaceSqlServerEFCrossDbServices(this DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.ReplaceService<IQuerySqlGeneratorFactory, AutoPocoIO.DynamicSchema.Services.CrossDb.SqlServerQuerySqlGeneratorFactory>();
            optionBuilder.ReplaceService<IModelValidator, AutoPocoIO.DynamicSchema.Services.CrossDb.SqlServerModelValidator>();
            return optionBuilder;

        }
    }
}
