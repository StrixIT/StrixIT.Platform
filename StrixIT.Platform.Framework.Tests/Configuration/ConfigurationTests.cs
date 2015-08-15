using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrixIT.Platform.Framework.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        #region Public Methods

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            new Configuration().LoadConfigurations();
        }

        [TestMethod]
        public void ConfigurationShouldReturnPropertySettingForModuleWithCorrectType()
        {
            var config = new Configuration();
            var settings = config.GetConfiguration<TestConfiguration>();
            Assert.AreEqual(true, settings.BoolValue);
            Assert.AreEqual(10, settings.IntValue);
            Assert.AreEqual("Test", settings.StringValue);
        }

        [TestMethod]
        [ExpectedException(typeof(StrixConfigurationException))]
        public void ConfigurationShouldThrowErrorWhenASettingCannotBeParsedToTheCorrectType()
        {
            var config = new Configuration();
            try
            {
                var settings = config.GetConfiguration<TestConfiguration>("InvalidConfig");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Error in configuration with key InvalidConfig. String was not recognized as a valid Boolean.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StrixConfigurationException))]
        public void ConfigurationShouldThrowErrorWhenInvalidKeyIsSupplied()
        {
            var config = new Configuration();
            try
            {
                var settings = config.GetConfiguration<TestConfiguration>("Invalid");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("No configuration with key Invalid found.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void CustomAppSettingShouldHaveBeenLoaded()
        {
            var config = new Configuration();
            Assert.IsNotNull(config.GetConfiguration<TestConfiguration>().StringValue);
        }

        [TestMethod]
        public void CustomConnectionStringShouldHaveBeenLoaded()
        {
            var config = new Configuration();
            Assert.IsNotNull(config.GetConnectionString("CustomConnection"));
        }

        #endregion Public Methods
    }
}