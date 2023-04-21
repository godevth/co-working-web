using System.Collections.Generic;

namespace SBP.Application.Shared.Models
{
    public class SelectOptionResult<TId>
    {
        public SelectOption<TId>[] Results { get; set; }

        public int Page { get; set; }

        public bool More { get; set; }
    }


    public class SelectOptionResult : SelectOptionResult<string>
    {
    }
}