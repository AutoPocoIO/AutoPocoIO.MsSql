using AutoPocoIO.MsSql.DynamicSchema.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
#if EF22
using Microsoft.EntityFrameworkCore.Query.Sql;
#else
using Microsoft.EntityFrameworkCore.Query;
#endif
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoPocoIO.test.DynamicSchema.Runtime
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class EfServiceReplacementTests
    {

        [TestMethod]
        public void VerifyEfSqlCachingREmoved()
        {
            var optionBuilder = new Mock<DbContextOptionsBuilder>();
            optionBuilder.Setup(c => c.ReplaceService<It.IsAnyType, It.IsAnyType>());

            optionBuilder.Object.ReplaceSqlServerEFCachingServices();

            optionBuilder.Verify(c => c.ReplaceService<It.IsAnyType, It.IsAnyType>(), Times.Exactly(1));
            optionBuilder.Verify(c => c.ReplaceService<IRelationalTypeMappingSource, AutoPocoIO.DynamicSchema.Services.NoCache.SqlServerTypeMappingSource>(), Times.Once);
        }

        [TestMethod]
        public void VerifyEfCrossDb()
        {
            var optionBuilder = new Mock<DbContextOptionsBuilder>();
            optionBuilder.Setup(c => c.ReplaceService<It.IsAnyType, It.IsAnyType>());

            optionBuilder.Object.ReplaceSqlServerEFCrossDbServices();

            optionBuilder.Verify(c => c.ReplaceService<It.IsAnyType, It.IsAnyType>(), Times.Exactly(2));
            optionBuilder.Verify(c => c.ReplaceService<IQuerySqlGeneratorFactory, AutoPocoIO.DynamicSchema.Services.CrossDb.SqlServerQuerySqlGeneratorFactory>(), Times.Once);
            optionBuilder.Verify(c => c.ReplaceService<IModelValidator, AutoPocoIO.DynamicSchema.Services.CrossDb.SqlServerModelValidator>(), Times.Once);
        }
    }
}
