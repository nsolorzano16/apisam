using System;
using System.Collections.Generic;

namespace apisam.entities
{
    public class PageResponse<T> where T : class

    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int ItemCount { get; set; }
        public List<T> Items { get; set; }
    }
}
