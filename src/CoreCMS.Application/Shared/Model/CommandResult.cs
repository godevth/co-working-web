using System.Collections.Generic;

namespace CoreCMS.Application.Shared.Model
{
    public class CommandResult<TKey> {
        public TKey Data { get; set; }

        public bool Status { get; set; }

        public string Message { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }
    }
}