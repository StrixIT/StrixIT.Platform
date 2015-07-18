﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web.Tests
{
    [Serializable]
    public class TestEntity : ValidationBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [StrixRequired]
        public long Number { get; set; }

        public double Value { get; set; }

        public decimal Price { get; set; }

        [StrixNotDefault]
        public DateTime? Date { get; set; }

        public bool IsActive { get; set; }
    }
}