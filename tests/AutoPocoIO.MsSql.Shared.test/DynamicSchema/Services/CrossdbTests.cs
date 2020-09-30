using AutoPocoIO.Context;
using AutoPocoIO.DynamicSchema.Runtime;
using AutoPocoIO.MsSql.DynamicSchema.Runtime;
using AutoPocoIO.test;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AutoPocoIO.MsSql.DynamicSchema.Services
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class CrossdbTests
    {
        //[TestMethod]
        //public void MyTestMethod()
        //{
        //    DbContextOptions<AppDbContext> AppDbOptions = (DbContextOptions < AppDbContext > ) new DbContextOptionsBuilder<AppDbContext>()
        //       .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=autopocoCore;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework")
        //       .ReplaceEFCrossDbServices()
        //       .ReplaceSqlServerEFCrossDbServices()
        //       .ReplaceEFCachingServices()
        //       .ReplaceSqlServerEFCachingServices()
        //       .Options;

        //    var context = new AppDbContext(AppDbOptions);

        //    var a = context.Connector.ToList();
        //}
    }
}
