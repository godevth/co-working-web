using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCMS.Domain
{
    public class SystemVariable
    {
        [Key, StringLength (50)]
        public string SystemVariableCode { get; set; }

        [ForeignKey ("SystemVariableCategory"), Required]
        public string SystemVariableCategoryCode { get; set; }

        [Required, StringLength (450)]
        public string SystemVariableName { get; set; }

        public int? Ordering { get; set; }

        public virtual SystemVariableCategory SystemVariableCategory { get; set; }
    }
}