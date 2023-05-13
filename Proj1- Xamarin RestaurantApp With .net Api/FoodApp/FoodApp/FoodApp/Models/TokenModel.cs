using System;
namespace FoodApp.Models
{
    public class TokenModel
    {
        public string Access_Token { get; set; }
        public string TokenType { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int ExperiesIn { get; set; }
        public int CreationTime { get; set; }
        public int ExpirationTime { get; set; }
    }
}
