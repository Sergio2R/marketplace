using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Model
{
    public class Page<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }

        public Page(List<T> items, int pageIndex, int pageCount)
        {
            Items = items ?? new List<T>();
            PageIndex = pageIndex;
            PageCount = pageCount;
        }

        public int? NextPageIndex
        {
            get { return PageIndex < PageCount - 1 ? PageIndex + 1 : (int?)null; }
        }

        public int? PreviousPageIndex
        {
            get { return PageIndex > 0 ? PageIndex - 1 : (int?)null; }
        }

        public List<int> GetNextPageIndexes()
        {
            var nextIndexes = new List<int>();
            for (int i = 1; i <= 3; i++)
            {
                int nextPageIndex = PageIndex + i;
                if (nextPageIndex < PageCount)
                {
                    nextIndexes.Add(nextPageIndex);
                }
                else
                {
                    break;
                }
            }
            return nextIndexes;
        }

        public List<int> GetPreviousPageIndexes()
        {
            var previousIndexes = new List<int>();
            for (int i = 1; i <= 3; i++)
            {
                int previousPageIndex = PageIndex - i;
                if (previousPageIndex >= 0)
                {
                    previousIndexes.Insert(0, previousPageIndex);
                }
                else
                {
                    break;
                }
            }
            return previousIndexes;
        }
    }

}
