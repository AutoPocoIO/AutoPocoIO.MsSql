﻿using AutoPocoIO.DynamicSchema.Db;
using AutoPocoIO.DynamicSchema.Enums;
using AutoPocoIO.DynamicSchema.Models;
using AutoPocoIO.Factories;
using AutoPocoIO.Models;
using AutoPocoIO.Resources;
using AutoPocoIO.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AutoPocoIO.test.Factories
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class ResourceFactoryTests
    {
        private IResourceFactory _resourceFactory;
        private Guid id = Guid.NewGuid();

        public void Init(string resouceType)
        {
            var appAdminService = new Mock<IAppAdminService>();
            appAdminService.Setup(c => c.GetConnection("conn1"))
                .Returns(new Connector
                {
                    Id = id,
                    Name = "conn1",
                    ResourceType = resouceType,
                    ConnectionStringDecrypted = "connStr1"

                });

            appAdminService.Setup(c => c.GetConnectionById(id))
              .Returns(new Connector
              {
                  Id = id,
                  Name = "conn1",
                  ResourceType = resouceType,
                  ConnectionStringDecrypted = "connStr1"

              });

            var connStringFactory = new Mock<IConnectionStringFactory>();
            connStringFactory.Setup(c => c.GetConnectionInformation(resouceType, "connStr1"))
                .Returns(new ConnectionInformation());

            var resourceServices = new ServiceCollection()
                .AddSingleton(new Config())
                .AddTransient(c => connStringFactory.Object)
                .AddSingleton(Mock.Of<ISchemaInitializer>())
                .BuildServiceProvider();


            PrivateObject authProvider = new PrivateObject(ServiceProviderCache.Instance);
            var dictionary = (ConcurrentDictionary<string, IServiceProvider>)authProvider.GetField("_configurations");
            dictionary.Clear();
            dictionary.GetOrAdd("Microsoft.EntityFrameworkCore.SqlServer", resourceServices);

            var list = new List<IOperationResource> { new MsSqlResource(resourceServices) };

            _resourceFactory = new ResourceFactory(appAdminService.Object, list);
        }
        [TestMethod]
        public void GetSqlResource()
        {
            Init("Microsoft.EntityFrameworkCore.SqlServer");
            var resource = _resourceFactory.GetResource("conn1", OperationType.read, "obj1");
            Assert.IsInstanceOfType(resource, typeof(MsSqlResource));
        }
    }
}