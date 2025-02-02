namespace WebApp.Observer.Models
{
    public sealed class Discount
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int Rate { get; set; }
    }
}
