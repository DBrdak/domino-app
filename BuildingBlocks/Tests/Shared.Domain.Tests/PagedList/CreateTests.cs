using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Exceptions;
using Shared.Domain.ResponseTypes;

namespace Shared.Domain.Tests.PagedList
{
    public class CreateTests
    {
        [Theory]
        [ClassData(typeof(PagedListTestData.PagedListCreateValidTestData))]
        public void Create_ValiData_ShouldCreatePagedList(List<int> items, int page, int pageSize)
        {
            // Arrange

            // Act
            var pagedList = PagedList<int>.Create(items, page, pageSize);

            // Assert
            Assert.Equal(page, pagedList.Page);
            Assert.Equal(pageSize, pagedList.PageSize);
            Assert.Equal(items.Count, pagedList.TotalCount);
            Assert.Equal(Math.Ceiling((decimal)items.Count / pageSize), pagedList.TotalPages);
        }

        [Theory]
        [ClassData(typeof(PagedListTestData.PagedListCreateInvalidTestData))]
        public void Create_InvaliData_ShouldThrow(List<int> items, int page, int pageSize)
        {
            // Arrange
            var exceptionTypes = new [] { 
                typeof(DomainException<PageSize>), 
                typeof(DomainException<Page>)
            };

            // Act
            var pagedListCreateFunc = () => PagedList<int>.Create(items, page, pageSize);

            // Assert
            var exception = Assert.ThrowsAny<DomainException>(pagedListCreateFunc);
            Assert.Contains(exception.GetType(), exceptionTypes);
        }
    }
}
