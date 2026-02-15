namespace BusManagement.Api.ViewModel

{
using System.Text.Json.Serialization;
public class UsersInfoVM
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }  // Plain password from frontend
        [JsonIgnore]
        public string? PasswordHash { get; set; }  // Only used internally

        public string? UserType { get; set; }
    }
}
