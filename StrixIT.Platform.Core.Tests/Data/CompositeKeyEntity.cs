﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core.Tests
{
    public class CompositeKeyEntity : ValidationBase
    {
        public Guid Id { get; set; }

        public string Culture { get; set; }

        public int VersionNumber { get; set; }

        public string Data { get; set; }

        public PersonalInfo Info { get; set; }

        public ICollection<CompositeKeyEntity> Siblings { get; set; }

        public ICollection<CompositeKeyEntity> OtherSiblings { get; set; }
    }
}