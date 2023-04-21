namespace CoreCMS.Application.Shared.Model
{
    public class OptionViewModel<T>
    {
        public T Value { get; set; }

        public string Text { get; set; }

        public object Data { get; set; }
    }

    public class OptionViewModel : OptionViewModel<string>
    {
        
    }
}