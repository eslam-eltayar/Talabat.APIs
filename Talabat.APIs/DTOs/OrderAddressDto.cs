using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class OrderAddressDto
    {
        [Required]

        public required string FirstName { get; set; }
        [Required]

        public string LastName { get; set; } = null!;
        [Required]

        public string Street { get; set; } = null!;
        [Required]

        public string City { get; set; } = null!;
        [Required]

        public string Country { get; set; } = null!;
    }
}
