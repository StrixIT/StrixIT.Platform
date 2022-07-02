﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace StrixIT.Platform.Web.Tests
{
    [TestClass()]
    public class EntityTest
    {
        #region Public Methods

        [TestMethod()]
        public void UsingTryValidateModelOnAnEntityWithBothCustomValidationRulesAndBasicRulesInvalidShouldReturnBothErrorMessages()
        {
            TestController controller = new TestController();
            CustomValidateEntity entity = new CustomValidateEntity();
            bool result = controller.TryValidate(entity);
            Assert.IsFalse(result);
            Assert.AreEqual(2, entity.Validate(null).Count());
        }

        [TestMethod()]
        public void UsingTryValidateModelOnAnEntityWithCustomValidationRulesMetMustBeValid()
        {
            TestController controller = new TestController();
            CustomValidateEntity entity = new CustomValidateEntity();
            entity.Number = 1;
            entity.Name = "TestEntity";
            bool result = controller.TryValidate(entity);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UsingTryValidateModelOnAnEntityWithCustomValidationRulesNotMetMustBeInValid()
        {
            TestController controller = new TestController();
            CustomValidateEntity entity = new CustomValidateEntity();
            entity.Number = 1;
            entity.Name = "Just another entity";
            bool result = controller.TryValidate(entity);
            Assert.IsFalse(result);
            Assert.AreEqual(1, entity.Validate(null).Count());
        }

        [TestMethod()]
        public void UsingTryValidateModelOnAnEntityWithoutPropertyRequirementsMustBeInvalid()
        {
            TestController controller = new TestController();
            var entity = new TestEntity();
            bool result = controller.TryValidate(entity);
            Assert.IsFalse(result);
            Assert.AreEqual(1, entity.Validate(null).Count());
        }

        [TestMethod()]
        public void UsingTryValidateModelOnAnEntityWithPropertyRequirementsNotMetButCustomValidationRulesMetMustBeInvalid()
        {
            TestController controller = new TestController();
            CustomValidateEntity entity = new CustomValidateEntity();
            entity.Name = "TestEntity";
            bool result = controller.TryValidate(entity);
            Assert.IsFalse(result);
            Assert.AreEqual(1, entity.Validate(null).Count());
        }

        #endregion Public Methods
    }
}