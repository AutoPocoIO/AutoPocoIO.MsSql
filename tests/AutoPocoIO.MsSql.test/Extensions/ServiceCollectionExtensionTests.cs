using AutoPocoIO.Context;
using AutoPocoIO.DynamicSchema.Enums;
using AutoPocoIO.Extensions;
using AutoPocoIO.Resources;
using AutoPocoIO.test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace AutoPocoIO.MsSql.test.Extensions
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class ServiceCollectionExtensionTests
    {
        [TestMethod]
        public void AddSqlServerResourceType()
        {
            var resourceServices = new ServiceCollection();

            PrivateObject authProvider = new PrivateObject(ServiceProviderCache.Instance);
            var dictionary = (ConcurrentDictionary<string, IServiceProvider>)authProvider.GetField("_configurations");
            dictionary.Clear();
            dictionary.GetOrAdd("Microsoft.EntityFrameworkCore.SqlServer", resourceServices.BuildServiceProvider());

            var services = new ServiceCollection();
            services.WithSqlServerResources();

            Assert.AreEqual(2, services.Count());

            var provider = services.BuildServiceProvider();
            Assert.IsNotNull(provider.GetService<IOperationResource>());
            Assert.IsNotNull(provider.GetService<IConnectionStringBuilder>());
        }


        public class DbContextTest1: DbContext
        {

        }

        public class DbContextTest2 : DbContext
        {

        }
        [TestMethod]
        public void AddSqlServerDatabases()
        {
            var services = new ServiceCollection();
            services.AddScoped<DbContextTest1>();
            services.AddScoped<DbContextTest2>();
            services.ConfigureSqlServerApplicationDatabase("conn1");

            Assert.AreEqual(4, services.Count());

            var provider = services.BuildServiceProvider();

            //Dbs
            Assert.IsNotNull(provider.GetService<DbContextTest1>());
            Assert.IsNotNull(provider.GetService<DbContextTest2>());

            //ContextOptions
            Assert.IsNotNull(provider.GetService<DbContextOptions<DbContextTest1>>());
            Assert.IsNotNull(provider.GetService<DbContextOptions<DbContextTest2>>());

            DbContextOptions option = provider.GetService<DbContextOptions<DbContextTest1>>();
            Assert.IsInstanceOfType(option.Extensions.ElementAt(0), typeof(Microsoft.EntityFrameworkCore.Infrastructure.CoreOptionsExtension));
            Assert.IsInstanceOfType(option.Extensions.ElementAt(1), typeof(Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal.SqlServerOptionsExtension));

            option = provider.GetService<DbContextOptions<DbContextTest2>>();
            Assert.IsInstanceOfType(option.Extensions.ElementAt(0), typeof(Microsoft.EntityFrameworkCore.Infrastructure.CoreOptionsExtension));
            Assert.IsInstanceOfType(option.Extensions.ElementAt(1), typeof(Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal.SqlServerOptionsExtension));
        }
    }
}
