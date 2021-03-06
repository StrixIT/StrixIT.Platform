﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StrixIT.Platform.Web;

namespace StrixIT.Platform.Core.UnitTests.Resources
{
    [TestClass]
    public class ResourceTests
    {
        #region Public Methods

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            StrixPlatform.Environment = new DefaultEnvironment();
            ModuleManager.LoadAssemblies();
        }

        [TestCleanup]
        public void Cleanup()
        {
            StrixPlatform.CurrentCultureCode = null;
            StrixPlatform.Environment = null;
        }

        [TestMethod]
        public void GetResourcesForEnShouldReturnResourceStringsForEn()
        {
            var controller = new HomeController(new ResourceService());
            var result = (ClientResourceCollection)controller.GetResources("tests").Data;
            Assert.AreEqual("User", result["membership"]["user"]);
            Assert.AreEqual("Permission", result["membership"]["permission"]);
            Assert.AreEqual("News", result["cms"]["news"]);
            Assert.AreEqual("Comment", result["cms"]["comment"]);
        }

        [TestMethod]
        public void GetResourcesForNlShouldReturnResourceStringsForNl()
        {
            StrixPlatform.CurrentCultureCode = "nl";
            var controller = new HomeController(new ResourceService());
            var result = (ClientResourceCollection)controller.GetResources("tests").Data;
            StrixPlatform.CurrentCultureCode = null;
            Assert.AreEqual("Gebruiker", result["membership"]["user"]);
            Assert.AreEqual("Recht", result["membership"]["permission"]);
            Assert.AreEqual("Nieuws", result["cms"]["news"]);
            Assert.AreEqual("Commentaar", result["cms"]["comment"]);
        }

        [TestInitialize]
        public void Init()
        {
            StrixPlatform.CurrentCultureCode = "en";
            StrixPlatform.Environment = new DefaultEnvironment();
        }

        #endregion Public Methods
    }
}