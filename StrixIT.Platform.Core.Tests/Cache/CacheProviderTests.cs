﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace StrixIT.Platform.Core.Tests
{
    [TestClass()]
    public class CacheProviderTests
    {
        [TestMethod()]
        public void SaveEntityInCacheTest()
        {
            ICacheService target = new CacheService();
            var value = TestEntityFactory.GetEntity();
            target["Test"] = value;
            bool result = target["Test"].Equals(value);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SaveAListOfEntitiesInCacheTest()
        {
            ICacheService target = new CacheService();
            var value = new List<TestEntity>();

            for (int i = 0; i < 3; i++)
            {
                value.Add(TestEntityFactory.GetEntity());
            }

            target["TestEntityList"] = value;
            var result = target["TestEntityList"] as List<TestEntity>;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void RemoveEntityFromCacheTest()
        {
            ICacheService target = new CacheService();
            var value = TestEntityFactory.GetEntity();
            target["TestEntity2"] = value;
            target.Delete("Test");
            var result = target["Test"];
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ClearCacheTest()
        {
            ICacheService target = new CacheService();
            var value = TestEntityFactory.GetEntity();
            target["TestEntity2"] = value;
            target.Clear();
            var result = target["Test"];
            Assert.IsNull(result);
        }
    }
}