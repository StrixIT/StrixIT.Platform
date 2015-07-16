﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StrixIT.Platform.Core;
using System;

namespace StrixIT.Platform.Core.Tests
{  
    [TestClass()]
    public class StrixNotDefaultAttributeTest
    {
        [TestMethod()]
        public void NullableIntIsValidWhenNull()
        {
            StrixNotDefaultAttribute target = new StrixNotDefaultAttribute();
            int? value = null;
            bool expected = true;
            bool actual;
            actual = target.IsValid(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NullableIntIsNotValidWhen0()
        {
            StrixNotDefaultAttribute target = new StrixNotDefaultAttribute();
            int? value = 0;
            bool expected = false;
            bool actual;
            actual = target.IsValid(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NullableIntIsValidWhenGreaterThan0()
        {
            StrixNotDefaultAttribute target = new StrixNotDefaultAttribute();
            int? value = 1;
            bool expected = true;
            bool actual;
            actual = target.IsValid(value);
            Assert.AreEqual(expected, actual);
        }
    }
}
