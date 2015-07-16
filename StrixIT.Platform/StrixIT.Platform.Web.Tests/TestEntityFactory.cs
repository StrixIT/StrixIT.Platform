﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web.Tests
{
    public static class TestEntityFactory
    {
        public static TestEntity GetEntity()
        {
            var entity = new TestEntity();
            entity.Name = "Test";
            entity.Number = 5;
            entity.Value = 2.5;
            entity.Date = DateTime.Now.Date;
            entity.IsActive = true;
            return entity;
        }

        public static List<TestEntity> GetEntityList()
        {
            List<TestEntity> list = new List<TestEntity>();

            var entity = new TestEntity();
            entity.Id = 1;
            entity.Name = "Rutger";
            entity.Number = 5;
            entity.Value = 2.5;
            entity.Price = 2.75m;
            entity.Date = DateTime.Now.Date;
            entity.IsActive = true;
            list.Add(entity);

            entity = new TestEntity();
            entity.Id = 2;
            entity.Name = "Sanne";
            entity.Number = 10;
            entity.Value = 5;
            entity.Price = 3.40m;
            entity.Date = DateTime.Now.AddDays(5).Date;
            entity.IsActive = false;
            list.Add(entity);

            entity = new TestEntity();
            entity.Id = 3;
            entity.Name = "Magnus";
            entity.Number = 15;
            entity.Value = 7.5;
            entity.Price = 4.80m;
            entity.Date = DateTime.Now.AddDays(10).Date;
            entity.IsActive = true;
            list.Add(entity);

            entity = entity = new TestEntity();
            entity.Id = 4;
            entity.Name = "Dagmar";
            entity.Number = 20;
            entity.Value = 10;
            entity.Price = 5.95m;
            entity.Date = DateTime.Now.AddDays(15).Date;
            entity.IsActive = false;
            list.Add(entity);

            entity = entity = new TestEntity();
            entity.Id = 5;
            entity.Name = "Sam";
            entity.Number = 25;
            entity.Value = 12.5;
            entity.Price = 6.50m;
            entity.Date = DateTime.Now.AddDays(20).Date;
            entity.IsActive = true;
            list.Add(entity);

            entity = entity = new TestEntity();
            entity.Id = 6;
            entity.Name = "Sien";
            entity.Number = 30;
            entity.Value = 15;
            entity.Price = 7.70m;
            entity.Date = DateTime.Now.AddDays(25).Date;
            entity.IsActive = false;
            list.Add(entity);

            return list;
        }
    }
}