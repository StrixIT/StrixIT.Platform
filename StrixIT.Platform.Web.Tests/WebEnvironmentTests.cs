﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using StrixIT.Platform.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrixIT.Platform.Web.Tests
{
    [TestClass]
    public class WebEnvironmentTests
    {
        #region Private Fields

        private Mock<HttpContextBase> _httpContextMock = new Mock<HttpContextBase>();
        private Mock<HttpSessionStateBase> _sessionMock = new Mock<HttpSessionStateBase>();

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void GetDictionaryFromSessionWhenStoredInSessionAsSerializedStringShouldReturnTheDictionary()
        {
            var environment = GetEnvironment();
            var expected = new Dictionary<Guid, string>();
            expected.Add(Guid.NewGuid(), "Main");
            expected.Add(Guid.NewGuid(), "Sub");
            _sessionMock.Setup(s => s["testdictionary"]).Returns(JsonConvert.SerializeObject(expected));
            var result = environment.GetFromSession<Dictionary<Guid, string>>("testdictionary");
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.First().Key, result.First().Key);
            Assert.AreEqual(expected.Last().Value, result.Last().Value);
        }

        [TestMethod]
        public void GetGuidFromSessionWhenStoredInSessionAsSerializedStringShouldReturnParsedGuid()
        {
            var environment = GetEnvironment();
            var expected = Guid.NewGuid();
            _sessionMock.Setup(s => s[PlatformConstants.CURRENTGROUPID.ToLower()]).Returns(JsonConvert.SerializeObject(expected));
            var result = environment.GetFromSession<Guid>(PlatformConstants.CURRENTGROUPID);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetStringFromSessionWhenStoredInSessionAsSerializedStringShouldReturnString()
        {
            var environment = GetEnvironment();
            var expected = "admin@strixit.com";
            _sessionMock.Setup(s => s[PlatformConstants.CURRENTUSEREMAIL.ToLower()]).Returns(JsonConvert.SerializeObject(expected));
            var result = environment.GetFromSession<string>(PlatformConstants.CURRENTUSEREMAIL);
            Assert.AreEqual(expected, result);
        }

        #endregion Public Methods

        #region Private Methods

        private WebEnvironment GetEnvironment()
        {
            var environment = new WebEnvironment();
            environment.HttpContext = _httpContextMock.Object;
            _httpContextMock.Setup(m => m.Session).Returns(_sessionMock.Object);
            return environment;
        }

        #endregion Private Methods
    }
}