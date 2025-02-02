namespace WebApp.Observer.Models
{
    public sealed class UserCreateViewModel
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
