namespace WebApp.Composite.Models
{
    public sealed class Book
    {
        public  int  Id { get; set; }

        public string Title { get; set; } = null!;

        public Category Category { get; set; } = null!;

        public int CategoryId { get; set; }
    }
}
