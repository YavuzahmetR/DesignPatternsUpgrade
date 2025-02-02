namespace WebApp.Composite.Models
{
    public sealed class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public int ReferenceId { get; set; }

        // Name     Id ReferenceId
        // category 0  0
        // category 1  0
        // category 2  1
    }
}
