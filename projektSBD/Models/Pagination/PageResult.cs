﻿namespace projektSBD.Models.Pagination
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<T> Data { get; set; }
    }
}
