﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core.Tests
{
    [Serializable]
    public class TestEntity : ValidationBase
    {
        #region Public Properties

        public ICollection<TestCustomFieldValue> CustomFields { get; set; }

        [StrixNotDefault]
        public DateTime? Date { get; set; }

        public string Description { get; set; }
        public int Id { get; set; }

        public bool IsActive { get; set; }
        public string Name { get; set; }

        public string NotInDb
        {
            get { return "NotInDb"; }
        }

        [StrixRequired]
        public long Number { get; set; }

        public decimal Price { get; set; }
        public double Value { get; set; }

        #endregion Public Properties
    }
}