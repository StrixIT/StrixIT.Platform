﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrixIT.Platform.Core.Tests
{
    [TestClass()]
    public class DateTimeExtensionTests
    {
        [TestMethod]
        public void WhenADateTimeIsInRangeTheCheckForRangeShouldReturnTrue()
        {
            var dateTime = DateTime.Now;
            var result = dateTime.IsInRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(2));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenADateTimeIsNotInRangeTheCheckForRangeShouldReturnFalse()
        {
            var dateTime = DateTime.Now;
            var result = dateTime.IsInRange(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-5));
            Assert.IsFalse(result);
        }
    }
}