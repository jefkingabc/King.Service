﻿namespace King.Service.Tests.Data.Azure
{
    using System;
    using System.Threading.Tasks;
    using King.Service.Data.Azure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TableStorageTests
    {
        [TestMethod]
        public void Constructor()
        {
            new TableStorage("TestTable", "UseDevelopmentStorage=true");
        }

        [TestMethod]
        public void IsITableStorage()
        {
            Assert.IsNotNull(new TableStorage("TestTable", "UseDevelopmentStorage=true") as ITableStorage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTableNull()
        {
            new TableStorage(null, "UseDevelopmentStorage=true");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorKeyNull()
        {
            new TableStorage("TestTable", null);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task CreateIfNotExists()
        {
            var table = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var connectionString = "UseDevelopmentStorage=true;";
            var storage = new TableStorage(table, connectionString);
            var created = await storage.CreateIfNotExists();

            Assert.IsTrue(created);
        }
    }
}