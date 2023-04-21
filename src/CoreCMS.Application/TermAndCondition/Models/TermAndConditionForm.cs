using System;

namespace CoreCMS.Application.TermAndCondition.Models
{
    public class TermAndConditionForm
    {
        public string Name { get; set; }
        
        public Guid PlaceId { get; set; }

        public string TermTH { get; set; }

        public string TermEN { get; set; }
                
    }
}