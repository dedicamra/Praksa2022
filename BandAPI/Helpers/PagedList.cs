using System;
using System.Collections.Generic;
using System.Linq;

namespace BandAPI.Helpers
{
    public class PagedList<T>:List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalPages);

        
        //List<T> will be the collection od IQueryable 
        public PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T>Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count= source.Count();
            var items= source.Skip((pageNumber-1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
        
    }

}
