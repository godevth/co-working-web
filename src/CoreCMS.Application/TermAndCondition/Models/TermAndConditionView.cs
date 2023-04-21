using System;

namespace CoreCMS.Application.TermAndCondition.Models
{
    public class TermAndConditionView
    {
        public int TermId { get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string Name { get; set; }
        public string TermTH { get; set; }
        public string TermEN { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public bool InActiveStatus { get; set; }

        public string CreateDateString
        {
            get
            {
                return CreateDate.ToString("dd/MM/yyyy HH:mm");
            }
        }
    }
}