﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace StrixIT.Platform.Core.Tests
{
    [TestClass()]
    public class ObjectExtensionsTest
    {
        #region Properties and Attributes

        [TestMethod()]
        public void GetPropertyValueShouldReturnProperValueForProperty()
        {
            var name = "Test";
            var entity = new TestEntity { Name = name };
            var result = ObjectExtensions.GetPropertyValue(entity, "Name");
            Assert.AreEqual(name, result);
        }

        [TestMethod()]
        public void SetPropertyValueShouldSetTheProperty()
        {
            var name = "Test";
            var entity = new TestEntity();
            ObjectExtensions.SetPropertyValue(entity, "Name", name);
            var result = entity.Name;
            Assert.AreEqual(name, result);
        }

        [TestMethod()]
        public void HasPropertyShouldReturnTrueWhenTheEntityHasAProperty()
        {
            var entity = new TestEntity();
            var result = ObjectExtensions.HasProperty(entity, "Name");
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void HasPropertyShouldReturnTrueWhenTheEntityTypeHasAProperty()
        {
            var entity = typeof(TestEntity);
            var result = ObjectExtensions.HasProperty(entity, "Description");
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void HasPropertyShouldReturnFalseWhenTheEntityDoesNotHaveAProperty()
        {
            var entity = new TestEntity();
            var result = ObjectExtensions.HasProperty(entity, "NotExist");
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void HasPropertyShouldReturnTrueWhenTheSpecifiedTypeHasAProperty()
        {
            var type = typeof(TestEntity);
            var result = ObjectExtensions.HasProperty(type, "Name");
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void HasAttributeShouldReturnTrueWhenTheSpecifiedTypeHasTheAttributeOnTheClass()
        {
            var type = typeof(TestEntity);
            var result = ObjectExtensions.HasAttribute(type, typeof(SerializableAttribute));
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void HasAttributeShouldReturnTrueWhenTheSpecifiedTypeHasTheAttributeOnAPropertyAndIncludePropertiesIsSetToTrue()
        {
            var type = typeof(TestEntity);
            var result = ObjectExtensions.HasAttribute(type, typeof(SerializableAttribute), true);
            Assert.AreEqual(true, result);
        }

        #endregion Properties and Attributes

        #region Typed Values

        [TestMethod()]
        public void TypedIntValueShouldReturnDefaultIntValueWhenFieldHasNoValue()
        {
            var result = Helpers.GetTypedValue("", typeof(int));
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void TypedStringValueShouldReturnAValidString()
        {
            var result = Helpers.GetTypedValue("Test", typeof(string));
            Assert.AreEqual("Test", result);
        }

        [TestMethod()]
        public void TypedLongValueShouldReturnAValidLong()
        {
            var result = Helpers.GetTypedValue(((long)Int32.MaxValue + 1000).ToString(), typeof(long));
            Assert.AreEqual((long)Int32.MaxValue + 1000, result);
        }

        [TestMethod()]
        public void TypedDoubleValueShouldReturnAValidDouble()
        {
            var result = Helpers.GetTypedValue("135.7", typeof(double));
            Assert.AreEqual(135.7d, result);
        }

        [TestMethod()]
        public void TypedDoubleValueShouldReturnAValidDoubleAlthoughDecimalSeparatorIsWrong()
        {
            var result = Helpers.GetTypedValue("135,7", typeof(double));
            Assert.AreEqual(1357d, result);
        }

        [TestMethod()]
        public void TypedBoolValueShouldReturnAValidBool()
        {
            var result = Helpers.GetTypedValue("true", typeof(bool));
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void TypedDateTimeValueShouldReturnAValidDateTime()
        {
            var result = Helpers.GetTypedValue(new DateTime(1980, 8, 26).ToShortDateString(), typeof(DateTime));
            Assert.AreEqual(new DateTime(1980, 8, 26), result);
        }

        [TestMethod()]
        public void TypedNullableDateTimeValueShouldReturnAValidDateTime()
        {
            var result = Helpers.GetTypedValue(new DateTime(1980, 8, 26).ToString(), typeof(DateTime?));
            Assert.AreEqual(((DateTime?)new DateTime(1980, 8, 26)), result);
        }

        [TestMethod()]
        public void GetTypedValueForIntShouldReturnInt()
        {
            var result = Helpers.GetTypedValue("5", typeof(int));
            Assert.IsTrue(result.GetType().Equals(typeof(int)));
            Assert.AreEqual(5, result);
        }

        [TestMethod()]
        public void GetTypedValueForEnumShouldReturnProperEnumValue()
        {
            var result = Helpers.GetTypedValue("contains", typeof(FilterFieldOperator));
            Assert.AreEqual(FilterFieldOperator.Contains, result);
        }

        #endregion Typed Values

        [TestMethod()]
        public void CreateGenericListShouldCreateANewList()
        {
            var list = Helpers.CreateGenericList(typeof(int), 5).Cast<int>();
            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void ToCamelCaseShouldCamelCaseAString()
        {
            var text = "TestResult";
            Assert.AreEqual("testResult", text.ToCamelCase());
        }

        [TestMethod()]
        public void ToTitleCaseShouldTitleCaseAString()
        {
            var text = "test results";
            Assert.AreEqual("Test Results", text.ToTitleCase());
        }
    }
}