﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;

namespace StrixIT.Platform.Core.Tests
{
    [ComplexType]
    public class PersonalInfo
    {
        #region Public Properties

        public Address Address { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        #endregion Public Properties
    }
}