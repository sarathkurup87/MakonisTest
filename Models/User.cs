using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Makonis.Models
{
    public class User
    {
        [Required]
        [DisplayName("First Name")]
        [StringLength(maximumLength: 4, MinimumLength = 2,
        ErrorMessage = "Server Side Validation: The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string? FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(maximumLength: 4, MinimumLength = 2,
        ErrorMessage = "Server Side Validation: The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string? LastName { get; set; }

    }
}