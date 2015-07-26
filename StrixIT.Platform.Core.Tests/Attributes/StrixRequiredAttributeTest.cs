﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StrixIT.Platform.Core.Tests
{
    [TestClass()]
    public class StrixRequiredAttributeTest
    {
        #region Public Methods

        [TestMethod()]
        public void DateTimeIsNotValidWhenInitial()
        {
            StrixRequiredAttribute target = new StrixRequiredAttribute();
            DateTime value = new DateTime();
            bool expected = false;
            bool actual;
            actual = target.IsValid(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DateTimeIsValidWhenNotInitial()
        {
            StrixRequiredAttribute target = new StrixRequiredAttribute();
            DateTime value = DateTime.Now;
            bool expected = true;
            bool actual;
            actual = target.IsValid(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GuidIsNotValidWhenEmpty()
        {
            StrixRequiredAttribute target = new StrixRequiredAttribute();
            Guid value = new Guid();
            bool expected = false;
            bool actual;
            actual = target.IsValid(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GuidIsValidWhenNotEmpty()
        {
            StrixRequiredAttribute target = new StrixRequiredAttribute();
            Guid value = Guid.NewGuid();
            bool expected = true;
            bool actual;
            actual = target.IsValid(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IntIsNotValidWhen0()
        {
            StrixRequiredAttribute target = new StrixRequiredAttribute();
            int value = 0;
            bool expected = false;
            bool actual;
            actual = target.IsValid(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IntIsValidWhenGreaterThan0()
        {
            StrixRequiredAttribute target = new StrixRequiredAttribute();
            int value = 1;
            bool expected = true;
            bool actual;
            actual = target.IsValid(value);
            Assert.AreEqual(expected, actual);
        }

        #endregion Public Methods
    }
}