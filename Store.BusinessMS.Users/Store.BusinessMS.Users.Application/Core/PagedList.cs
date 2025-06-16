namespace Store.BusinessMS.Users.Application.Core
{
    public class PagedList<T> : PagingParams
    {
        public PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items.AddRange(items);
        }

        public int TotalCount { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public List<T> Items { get; set; } = new List<T>();
    }
}