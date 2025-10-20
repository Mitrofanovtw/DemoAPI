namespace DemoAPI.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string RegistrationDateFormatted { get; set; } = string.Empty;
        public bool IsAdult => Age >= 18;
    }
}
