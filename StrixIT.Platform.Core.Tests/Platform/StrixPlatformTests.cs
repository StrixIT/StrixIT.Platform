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
    public class StrixPlatformTests
    {
        #region Public Methods

        [Ignore]
        [TestMethod]
        public void PlatformShouldReturnFirstConfiguredCultureAsDefault()
        {
            var result = StrixPlatform.DefaultCultureCode;
            Assert.AreEqual("en", result);
        }

        [Ignore]
        [TestMethod]
        public void PlatformShouldReturnListOfCultures()
        {
            var list = StrixPlatform.Cultures;
            Assert.AreEqual(3, list.Count);
            Assert.IsTrue(list.Any(c => c.Code == "nl" && c.NativeName == "Nederlands"));
        }

        #endregion Public Methods
    }
}