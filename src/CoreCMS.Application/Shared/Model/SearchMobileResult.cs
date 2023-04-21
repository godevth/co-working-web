namespace CoreCMS.Application.Shared.Model
{
    public class SearchMobileResult<T>
    {
        public int Total { get; set; }
        public bool More { get; set; }

        public T[] Items { get; set; }
    }
}