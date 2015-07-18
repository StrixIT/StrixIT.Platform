﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StrixIT.Platform.Core.Tests
{
    [TestClass]
    public class CustomFieldTests
    {
        [TestMethod()]
        public void GetTestFieldsForPropertyShouldReturnFieldsWithAllData()
        {
            var id = AdminId;
            var result = CustomFields.GetCustomFieldsList<TestCustomField, TestCustomFieldValue>(GetCustomFields.AsQueryable().Where(x => x.UserId == id && x.Culture == "en"), "UserId").First();
            Assert.AreEqual(new DateTime(1980, 8, 26), result.BirthDate);
            Assert.AreEqual("Leusden", result.City);
            Assert.AreEqual(5, result.Stars);
            Assert.AreEqual(false, result.FalseOrTrue);
        }

        [TestMethod()]
        public void GetTestFieldsForListShouldReturnListOfFieldsWithAllData()
        {
            var id = AdminId;
            var result = CustomFields.GetCustomFieldsList<TestCustomField, TestCustomFieldValue>(GetCustomFields.AsQueryable(), "UserId");
            var first = result[0];
            var second = result[1];
            Assert.AreEqual(new DateTime(1980, 8, 26), first.BirthDate);
            Assert.AreEqual("Leusden", first.City);
            Assert.AreEqual(5, first.Stars);
            Assert.AreEqual(false, first.FalseOrTrue);
            Assert.AreEqual(new DateTime(1982, 7, 7), second.BirthDate);
            Assert.AreEqual("Leusden", second.City);
            Assert.AreEqual(4, second.Stars);
            Assert.AreEqual(true, second.FalseOrTrue);
        }

        private static Guid AdminId
        {
            get
            {
                return Guid.Parse("BE65198E-C609-4D90-BBB3-805BE0B6B1C2");
            }
        }

        private static Guid UserId
        {
            get
            {
                return Guid.Parse("D632CA83-A3F3-4925-B5B8-8A21D64F3CD7");
            }
        }

        public static Guid MainGroupId
        {
            get
            {
                return Guid.Parse("DF54143F-8A89-4CA9-9B28-4E1C665D24A4");
            }
        }

        private static List<TestCustomFieldValue> GetCustomFields
        {
            get
            {
                return new List<TestCustomFieldValue>
                {
                    new TestCustomFieldValue
                    {
                        CustomField = new TestCustomField()
                        {
                            GroupId = MainGroupId,
                            Name = "BirthDate",
                            FieldType = CustomFieldType.DateTime
                        },
                        UserId = AdminId,
                        Culture = "en",
                        NumberValue = new DateTime(1980, 8, 26).Ticks
                    },
                    new TestCustomFieldValue
                    {
                        CustomField = new TestCustomField()
                        {
                            GroupId = MainGroupId,
                            Name = "City",
                            FieldType = CustomFieldType.String
                        },
                        UserId = AdminId,
                        Culture = "en",
                        StringValue = "Leusden"
                    },
                    new TestCustomFieldValue
                    {
                        CustomField = new TestCustomField()
                        {
                            GroupId = MainGroupId,
                            Name = "Stars",
                            FieldType = CustomFieldType.Integer
                        },
                        UserId = AdminId,
                        Culture = "en",
                        NumberValue = 5
                    },
                    new TestCustomFieldValue
                    {
                        CustomField = new TestCustomField()
                        {
                            GroupId = MainGroupId,
                            Name = "FalseOrTrue",
                            FieldType = CustomFieldType.Boolean
                        },
                        UserId = AdminId,
                        Culture = "en",
                        NumberValue = 0
                    },
                    new TestCustomFieldValue
                    {
                        CustomField = new TestCustomField()
                        {
                            GroupId = MainGroupId,
                            Name = "BirthDate",
                            FieldType = CustomFieldType.DateTime
                        },
                        UserId = UserId,
                        Culture = "en",
                        NumberValue = new DateTime(1982, 7, 7).Ticks
                    },
                    new TestCustomFieldValue
                    {
                        CustomField = new TestCustomField()
                        {
                            GroupId = MainGroupId,
                            Name = "City",
                            FieldType = CustomFieldType.String
                        },
                        UserId = UserId,
                        Culture = "en",
                        StringValue = "Leusden"
                    },
                    new TestCustomFieldValue
                    {
                        CustomField = new TestCustomField()
                        {
                            GroupId = MainGroupId,
                            Name = "Stars",
                            FieldType = CustomFieldType.Integer
                        },
                        UserId = UserId,
                        Culture = "en",
                        NumberValue = 4
                    },
                    new TestCustomFieldValue
                    {
                        CustomField = new TestCustomField()
                        {
                            GroupId = MainGroupId,
                            Name = "FalseOrTrue",
                            FieldType = CustomFieldType.Boolean
                        },
                        UserId = UserId,
                        Culture = "en",
                        NumberValue = 1
                    }
                };
            }
        }
    }
}