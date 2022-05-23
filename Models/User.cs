using System.ComponentModel.DataAnnotations;

namespace Makonis.Models
{
    public class User
    {
        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 3,
        ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 3,
        ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string? LastName { get; set; }

    }
}