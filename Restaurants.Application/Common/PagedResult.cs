namespace Restaurants.API.Common
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
            TotalItemsCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ItemsFrom = (pageNumber - 1) * pageSize + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
        }

        public IEnumerable<T> Items { get; set; }
        public int TotalItemsCount { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
    }
}
