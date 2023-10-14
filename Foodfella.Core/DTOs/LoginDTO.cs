using System.ComponentModel.DataAnnotations;

namespace Foodfella.Core.DTO
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email {  get; set; }
		[Required]
		public string Password { get; set; }
    }
}