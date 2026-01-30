namespace IdentityService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
