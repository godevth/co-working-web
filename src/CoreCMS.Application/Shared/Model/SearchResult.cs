namespace CoreCMS.Application.Shared.Model
{
    public class SearchResult<T>
    {
        public int Total { get; set; }

        public T[] Items { get; set; }
    }
}