﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace StrixIT.Platform.Core.Tests
{
    [TestClass]
    public class ModuleManagerTests
    {
        #region Public Methods

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            ModuleManager.LoadAssemblies();
            ModuleManager.LoadConfigurations();
        }

        [TestMethod]
        public void CustomAppSettingShouldHaveBeenLoaded()
        {
            Assert.IsNotNull(ModuleManager.AppSettings["TestModule"]["testSetting"]);
        }

        [TestMethod]
        public void CustomConnectionStringShouldHaveBeenLoaded()
        {
            Assert.IsNotNull(ModuleManager.ConnectionStrings["CustomConnection"]);
        }

        [TestMethod]
        public void GetObjectListShouldReturnInitializedObjectsBasedOnRequestedType()
        {
            var result = ModuleManager.GetObjectList<ValidationBase>();
            Assert.AreEqual(7, result.Count);
        }

        [TestMethod]
        public void GetTypeListShouldReturnAllTypesBasedOnRequestedType()
        {
            var result = ModuleManager.GetTypeList(typeof(ValidationBase));
            Assert.IsTrue(result.Any(r => r == typeof(TestEntity)));
            Assert.AreEqual(8, result.Count);
        }

        #endregion Public Methods
    }
}