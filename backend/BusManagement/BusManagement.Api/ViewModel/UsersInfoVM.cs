namespace BusManagement.Api.ViewModel

{
using System.Text.Json.Serialization;
public class UsersInfoVM
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }   // From frontend
        public string? PasswordHash { get; set; } // Internal use
        public string? Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
    }
    public class LoginVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterVM
    {

        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public required string Phone { get; set; }

        public string Role { get; set; } = "User";
    }
}
