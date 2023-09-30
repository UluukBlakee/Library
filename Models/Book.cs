namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public int? YearRelease { get; set; }
        public string? Description { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
