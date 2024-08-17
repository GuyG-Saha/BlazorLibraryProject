namespace BlazorServerApp.Shared
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long AuthorId { get; set; }
        public int? Quantity { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }
        public Author Author { get; set; }
    }

}
