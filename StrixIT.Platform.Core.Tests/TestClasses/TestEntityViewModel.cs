﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StrixIT.Platform.Core.Tests
{
    public class TestEntityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [StrixRequired]
        public long Number { get; set; }

        public string NotAvailable { get; set; }
    }
}