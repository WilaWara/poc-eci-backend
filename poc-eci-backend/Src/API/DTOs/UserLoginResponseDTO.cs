using Domain.Entities;

namespace API.DTOs
{
    public class UserLoginResponseDTO
    {
        public User User { get; set; }

        public string Token { get; set; }
    }
}
