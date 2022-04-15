using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicSun.Data.Models
{
    public class PageInfo
    {
        public int PageNumber { get;  set; }
        public int PageSize { get;  set; }
        public int TotalItems { get; set; }
        public int StartPage
        {
            get { return 1; }
        }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }

        public int MaximumPageNumbersToDisplay
        {
            get { return 8; }
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }

        public int FirstPageToDisplay
        {
            get
            {
                var firstPageToDisplay = 1;
                var lastPageToDisplay = TotalPages;
                firstPageToDisplay = PageNumber - MaximumPageNumbersToDisplay / 2;
                if (firstPageToDisplay < 1)
                {
                    firstPageToDisplay = 1;
                }

                lastPageToDisplay = firstPageToDisplay + MaximumPageNumbersToDisplay - 1;

                if (lastPageToDisplay > TotalPages)
                {
                    firstPageToDisplay = TotalPages - MaximumPageNumbersToDisplay + 1;
                }

                return firstPageToDisplay;
            }
        }
    }
}
