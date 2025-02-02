namespace WebApp.Composite.Composite
{
    public class BookComponent : IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public BookComponent(int id, string name)
        {
            Id = id;
            Name = name;
        }


        public int Count()
        {
            return 1;
        }

        public string Display()
        {
            return $"<li class='list-item-group'>{Name}</li>";
        }
    }
}
