using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Domain
{
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }

        [StringLength(256), Required]
        public string Name { get; set; }

        [StringLength(256), Required]
        public string NormalizedName { get; set; }

        [StringLength(450), Required]
        public string Description { get; set; }

        public bool InActiveStatus { get; set; }
    }
}