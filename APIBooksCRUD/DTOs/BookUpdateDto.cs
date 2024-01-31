namespace APIBooksCRUD.DTOs
{
    public class BookUpdateDto
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateOnly PublicationDate { get; set; }
        public int PageCount { get; set; }
        public string Synopsis { get; set; }
        public decimal Value { get; set; }
    }
}
