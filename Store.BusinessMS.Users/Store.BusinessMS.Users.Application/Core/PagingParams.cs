namespace Store.BusinessMS.Users.Application.Core
{
    public abstract class PagingParams
    {
        private int _pageNumber = 1;
        private int _pageSize = 15;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value > 0 ? value : 1;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 ? value : 15;
        }
    }
}