namespace SAOnlineMartAPI.Models
{
    public class UserRegistrationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User"; // Default role is "User"
    }
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}