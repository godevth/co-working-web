namespace SBP.Application.Shared.Models
{
    public class SelectOption<TId>
    {
        public TId Id { get; set; }

        public string Text { get; set; }

        public object Data { get; set; }
    }


    public class SelectOption : SelectOption<string>
    {
    }
}