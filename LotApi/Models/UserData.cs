using System.ComponentModel.DataAnnotations.Schema;

namespace LotApi.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
