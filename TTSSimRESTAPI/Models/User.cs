using System.ComponentModel.DataAnnotations;

namespace TTSSimRESTAPI.Models
{
    public enum UserType
    {
        User,
        Admin
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public UserType UserType { get; set; } = 0;
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string? SaveFile { get; set; } = string.Empty;
    }
}
