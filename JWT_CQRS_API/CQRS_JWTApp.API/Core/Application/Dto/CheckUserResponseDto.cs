namespace CQRS_JWTApp.API.Core.Application.Dto
{
    public class CheckUserResponseDto
    {
        public int Id { get; set; }
        public string? Role { get; set; }

        public string? Username { get; set; }
        public bool isExist { get; set; }
    }
}
