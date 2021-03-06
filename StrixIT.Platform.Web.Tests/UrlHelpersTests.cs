﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace StrixIT.Platform.Web.Tests
{
    [TestClass]
    public class UrlHelpersTests
    {
        #region Clean Urls

        [TestMethod()]
        public void CreateCleanUrlWithApostophesShouldReturnCleanedUrl()
        {
            var path = " Th`s ì's @ Tëst #PÂTH# ";
            var result = UrlHelpers.CreateCleanUrl(path);
            Assert.AreEqual("ths-is-test-path", result);
        }

        [TestMethod()]
        public void CreateCleanUrlWithIntermediateReplacementAtStartAndEndShouldReturnCleanedUrlWithNoReplacementOnStartOrEnd()
        {
            var path = "... 2 ...";
            var result = UrlHelpers.CreateCleanUrl(path);
            Assert.AreEqual("2", result);
        }

        [TestMethod()]
        public void CreateCleanUrlWithLeadingTrailingAndIntermediateSpacesDiacriticsAndNonAlphanumericInPathShouldReturnCleanedUrl()
        {
            var path = " Th!s is @ Tëst #PÂTH# ";
            var result = UrlHelpers.CreateCleanUrl(path);
            Assert.AreEqual("ths-is-test-path", result);
        }

        [TestMethod()]
        public void CreateCleanUrlWithSpacesAndNonAlphanumericInPathShouldReturnCleanedUrl()
        {
            var path = "Th!s is @ Test #PATH#";
            var result = UrlHelpers.CreateCleanUrl(path);
            Assert.AreEqual("ths-is-test-path", result);
        }

        #endregion Clean Urls

        #region Unique Urls

        [TestMethod()]
        public void CreateUniqueUrlShouldAddIndexForAlreadyExistingUrl()
        {
            var url = UrlHelpers.CreateUniqueUrl(TestEntityFactory.GetEntityList().AsQueryable(), "Rutger", 0, "Name");
            Assert.AreEqual("rutger-2", url);
        }

        [TestMethod()]
        public void CreateUniqueUrlShouldAddNextIndexForAlreadyExistingUrlWithIndex()
        {
            var entities = TestEntityFactory.GetEntityList();
            entities.Add(new TestEntity { Name = "Rutger-2", Id = 7 });
            var url = UrlHelpers.CreateUniqueUrl(entities.AsQueryable(), "Rutger", 0, "Name");
            Assert.AreEqual("rutger-3", url);
        }

        [TestMethod()]
        public void CreateUniqueUrlShouldCreateCorrectUrl()
        {
            var url = UrlHelpers.CreateUniqueUrl(TestEntityFactory.GetEntityList().AsQueryable(), "Test", 0, "Name");
            Assert.AreEqual("test", url);
        }

        [TestMethod()]
        public void CreateUniqueUrlShouldReturnUrlForEntityWithSameId()
        {
            var url = UrlHelpers.CreateUniqueUrl(TestEntityFactory.GetEntityList().AsQueryable(), "Rutger", 1, "Name");
            Assert.AreEqual("rutger", url);
        }

        #endregion Unique Urls
    }
}