using System.ComponentModel.DataAnnotations;

namespace Foodfella.Core.DTO
{
	public class EditProfileDTO
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MaxLength(50)]
		public string FullName { get; set; }

		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Compare("NewPassword")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
