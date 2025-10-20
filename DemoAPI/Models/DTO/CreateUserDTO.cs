using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateUserDTO
    {
        public class CreateUserDto
        {
            [Required(ErrorMessage = "Имя обязательно для заполнения")]
            [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 50 символов")]
            public string FirstName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
            [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия должна быть от 2 до 50 символов")]
            public string LastName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email обязателен для заполнения")]
            [EmailAddress(ErrorMessage = "Некорректный формат email")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Возраст обязателен для заполнения")]
            [Range(1, 120, ErrorMessage = "Возраст должен быть от 1 до 120 лет")]
            public int Age { get; set; }
        }
    }
}
