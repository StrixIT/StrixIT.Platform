﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Core.Tests
{
    public class IntEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Range(5, 25)]
        public int Number { get; set; }

        public bool IsRequired { get; set; }

        public DateTime? StartDate { get; set; }
    }
}
