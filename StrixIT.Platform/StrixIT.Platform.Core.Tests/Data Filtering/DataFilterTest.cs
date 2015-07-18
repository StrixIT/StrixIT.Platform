﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StrixIT.Platform.Core;
using Moq;
using System.Collections.Generic;
using System.Reflection;

namespace StrixIT.Platform.Core.Tests.Tools
{
    [TestClass]
    public class DataFilterTest
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            DataFilter.RegisterFilterMap<TestEntity>(new FilterSortMap { FieldToMap = "Test", FilterMap = (x, y) => { return "Test"; }, SortMap = (x, y) => { return x; } });
        }

        #region Process types

        [TestMethod()]
        public void FilterShouldCorrectlyProcessStringEquals()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Equals, "Name", "Rutger"));
            int expected = 1;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        [TestMethod()]
        public void FilterShouldCorrectlyProcessStringContains()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Contains, "Name", "G"));
            int expected = 3;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        [TestMethod()]
        public void FilterShouldCorrectlyProcessStringStartsWith()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.StartsWith, "Name", "S"));
            int expected = 3;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        [TestMethod()]
        public void FilterShouldCorrectlyProcessStringEndsWith()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.EndsWith, "Name", "E"));
            int expected = 1;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        [TestMethod()]
        public void FilterShouldCorrectlyProcessInt()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Equals, "Number", "10"));
            int expected = 1;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        [TestMethod()]
        public void FilterShouldCorrectlyProcessDouble()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Equals, "Value", "7.5"));
            int expected = 1;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        [TestMethod()]
        public void FilterShouldCorrectlyProcessPrice()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Equals, "Price", "7.70"));
            int expected = 1;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        [TestMethod()]
        public void FilterShouldCorrectlyProcessBool()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Equals, "IsActive", "True"));
            int expected = 3;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        [TestMethod()]
        public void FilterShouldCorrectlyProcessDateTime()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Equals, "Date", DateTime.Now.AddDays(5).Date.ToString()));
            int expected = 1;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        #endregion

        #region Sort by types

        [TestMethod()]
        public void FilterShouldCorrectlySortByString()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Sort.Add(new SortField { Field = "Name", Dir = "asc" });
            var expected = "Dagmar";
            query = query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, query.First().Name);
        }

        [TestMethod()]
        public void FilterShouldCorrectlySortByInt()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Sort.Add(new SortField { Field = "Number", Dir = "desc" });
            var expected = 30;
            query = query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, query.First().Number);
        }

        [TestMethod()]
        public void FilterShouldCorrectlySortByDouble()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Sort.Add(new SortField { Field = "Value", Dir = "desc" });
            var expected = 15;
            query = query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, query.First().Value);
        }

        [TestMethod()]
        public void FilterShouldCorrectlySortByDecimal()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Sort.Add(new SortField { Field = "Price", Dir = "desc" });
            var expected = 7.70m;
            query = query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, query.First().Price);
        }

        [TestMethod()]
        public void FilterShouldCorrectlySortByBool()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Sort.Add(new SortField { Field = "IsActive", Dir = "asc" });
            var expected = false;
            query = query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, query.First().IsActive);
        }

        [TestMethod()]
        public void FilterShouldCorrectlySortByDateTime()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Sort.Add(new SortField { Field = "Date", Dir = "asc" });
            var expected = DateTime.Now.Date;
            query = query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, query.First().Date);
        }

        #endregion

        #region FilterUsingAndOr

        [TestMethod()]
        public void GetFilteredListOfTestEntitiesOnTwoFieldsUsingAnd()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Logic = FilterType.And;
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Contains, "Name", "s"));
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Equals, "Number", "10"));
            int expected = 1;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        [TestMethod()]
        public void GetFilteredListOfTestEntitiesOnTwoFieldsUsingOr()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions();
            filter.Filter.Logic = FilterType.Or;
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Contains, "Name", "s"));
            filter.Filter.Filters.Add(new FilterField(FilterFieldOperator.Equals, "Number", "10"));
            int expected = 4;
            query.Filter<TestEntity>(filter);
            Assert.AreEqual(expected, filter.Total);
        }

        #endregion

        #region Page

        [TestMethod()]
        public void PagingShouldReturnProperSubsetOfEntities()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions { PageSize = 3, Page = 2 };
            var result = query.Page(filter).Cast<TestEntity>().ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(4, result.First().Id);
            Assert.AreEqual(6, result.Last().Id);
        }

        #endregion

        #region TraverseList

        [TestMethod()]
        public void IncludingTraverseListInFilterShouldReturnTraverseList()
        {
            var query = TestEntityFactory.GetEntityList().AsQueryable<TestEntity>();
            FilterOptions filter = new FilterOptions { IncludeTraverseList = true };
            var result = query.Filter<TestEntity>(filter);
            Assert.IsNotNull(filter.TraverseList);
            Assert.AreEqual(6, filter.TraverseList.Count);
            Assert.AreEqual(1, filter.TraverseList.First().Id);
            Assert.AreEqual(6, filter.TraverseList.Last().Id);
        }

        #endregion
    }
}