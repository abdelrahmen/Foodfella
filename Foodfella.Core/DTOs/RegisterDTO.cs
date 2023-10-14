using System.ComponentModel.DataAnnotations;

namespace Foodfella.Core.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
		[MaxLength(50)]
		public string FullName { get; set; }
		[Required]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


	}
}