using Moq;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Tests.Helpers
{
    public static class EntityFramework
    {
        public static DbSet<T> ListToDbSet<T>(List<T> list) where T : class
        {
            var queryableList = list.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableList.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

            return mockSet.Object;
        }
    }
}