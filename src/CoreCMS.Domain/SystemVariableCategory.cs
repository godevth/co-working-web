using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Domain
{
    public class SystemVariableCategory {
        [Key, StringLength (50)]
        public string SystemVariableCategoryCode { get; set; }

        [Required, StringLength (450)]
        public string SystemVariableCategoryName { get; set; }
    }
}