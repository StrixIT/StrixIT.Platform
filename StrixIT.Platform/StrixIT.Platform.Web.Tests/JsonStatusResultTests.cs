﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StrixIT.Platform.Web.Tests
{
    [TestClass]
    public class JsonStatusResultTests
    {
        [TestMethod]
        public void CreatingANewJsonStatusResultShouldInitializeResultCorrecty()
        {
            var result = new JsonStatusResult();
            Assert.IsNotNull(((dynamic)result.Data));
            Assert.IsFalse(((dynamic)result.Data).Success);
            Assert.IsNull(((dynamic)result.Message));
        }
    }
}
