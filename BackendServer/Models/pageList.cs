using System;
using System.Collections.Generic;

namespace Models
{
    public class PagedList<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<T> ResultObj { get; set; }
        public int TotalCount { get; set; }
        //public int Page { get; set; }
        //public int PageSize { get; set; }
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / MaxResultCount);

        public PagedList(bool isSuccess, string message, List<T> resultObj, int totalCount, int skipCount, int maxResultCount)
        {
            IsSuccess = isSuccess;
            Message = message;
            ResultObj = resultObj;
            TotalCount = totalCount;
            //Page = page;
            //PageSize = pageSize;
            SkipCount = skipCount;
            MaxResultCount = maxResultCount;
        }
    }
}