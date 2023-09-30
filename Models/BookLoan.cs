namespace Library.Models
{
    public class BookLoan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTimeOffset? LoanDate { get; set; }
        public DateTimeOffset? ReturnDate { get; set; }
    }
}
