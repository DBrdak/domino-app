using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Money;

namespace Shared.Domain.Tests.PagedList
{
    public class PagedListTestData
    {
        private static List<int> GetList(int count)
        {
            var arr = new int[count];

            for (int i = 1; i <= count; i++)
            {
                arr[i - 1] = i;
            }

            return arr.ToList();
        }

        internal class PagedListCreateValidTestData : TheoryData<List<int>, int, int>
        {
            public PagedListCreateValidTestData()
            {
                Add(GetList(5), 1, 5);
                Add(GetList(9), 2, 5);
                Add(GetList(16), 2, 5);
            }
        }

        internal class PagedListCreateInvalidTestData : TheoryData<List<int>, int, int>
        {
            public PagedListCreateInvalidTestData()
            {
                Add(GetList(5), -1, 5);
                Add(GetList(9), 2, -5);
            }
        }
    }
}
