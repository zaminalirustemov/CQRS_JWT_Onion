namespace JwtApp.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public int AppRoleId { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }

        public AppRole? AppRole { get; set; }
    }
}
