﻿namespace projektSBD.Models
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // 👉 TO MUSI BYĆ:
        public List<T> Data { get; set; }
    }
}
