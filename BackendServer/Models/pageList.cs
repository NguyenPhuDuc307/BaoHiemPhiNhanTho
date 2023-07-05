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
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }

        public PagedList(bool isSuccess, string message, List<T> resultObj, int totalCount, int skipCount, int maxResultCount)
        {
            IsSuccess = isSuccess;
            Message = message;
            ResultObj = resultObj;
            TotalCount = totalCount;
            SkipCount = skipCount;
            MaxResultCount = maxResultCount;
        }
    }
}