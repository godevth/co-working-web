using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace CoreCMS.Domain
{
    public class Badge
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        [JsonIgnore]
        public virtual Device Device { get; set; }
    }
}
